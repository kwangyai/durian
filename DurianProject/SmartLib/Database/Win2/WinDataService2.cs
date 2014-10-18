using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using SmartLib.Database.Config;
using SmartLib.Logger;

namespace SmartLib.Database.Win
{
    public class WinDataService2 : IDataService
    {
        private ILogWriter log = LogWriterFactory.Create(Db.DB_LOGGER_NAME, typeof(WinDataService2));

        public DataTable ExecuteQuery(QueryCommand cmdQuery)
        {
            IDbCommand dbCmd = cmdQuery.ToIDbCommand();
            dbCmd.CommandText = cmdQuery.CommandText;

            return this.GetDataTable(dbCmd, cmdQuery);
        }


        private bool IsMultipleDB(IList<QueryCommand> cmdQueries)
        {
            String lastConfigName = "";
            String currentConfigName = "";
            bool isMultipleConnection = false;

            int i = 0;

            foreach (QueryCommand cmd in cmdQueries)
            {

                if (i > 0)
                    lastConfigName = currentConfigName;

                if (String.IsNullOrEmpty(cmd.ConfigName) || cmd.ConfigName == DbConfigManager.DefaultConfigurationName)
                    currentConfigName = DbConfigManager.DefaultConfigurationName;
                else
                    currentConfigName = cmd.ConfigName;

                if (i > 0 && lastConfigName != currentConfigName)
                {
                    isMultipleConnection = true;
                    break;
                }

                i = i + 1;
            }

            return isMultipleConnection;
        }

        public DataTable GetSchemaTable(String configName)
        {

            DataTable tbl = new DataTable();
            IDbCommand cmd = Db.CreateCommand(configName);
            IDbConnection conn = Db.GetConnection(configName);
            cmd.Connection = conn;

            IDataReader dr = null;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                    if (DbConfigManager.LogConnection)
                        log.LogDebug("Open Connection, HashCode=" + conn.GetHashCode().ToString());
                }

                dr = cmd.ExecuteReader();
                tbl = dr.GetSchemaTable();
                dr.Close();

                if (DbConfigManager.LogQuery)
                    log.LogDebug("GetSchemaTable, HashCode=" + conn.GetHashCode().ToString() + ", Query : " + cmd.CommandText);
            }
            catch (Exception ex)
            {

                if ((dr != null))
                    dr.Close();

                if (DbConfigManager.LogError)
                    log.LogError("GetSchemaTable, HashCode=" + conn.GetHashCode().ToString() + ", Query : " + cmd.CommandText + ", Error : ", ex);


                throw new DbException(ex.Message, ex.InnerException, null, 0);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    if (DbConfigManager.LogConnection)
                        log.LogDebug("Close Connection, HashCode=" + conn.GetHashCode().ToString());

                    conn.Close();
                }
            }

            return tbl;
        }



        public DataTable GetSchemaTable()
        {
            DataTable tbl = new DataTable();
            IDbCommand cmd = Db.CreateCommand();
            IDbConnection conn = Db.GetConnection();
            cmd.Connection = conn;
            IDataReader dr = null;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                    if (DbConfigManager.LogConnection)
                        log.LogDebug("Open Connection, HashCode=" + conn.GetHashCode().ToString());
                }

                dr = cmd.ExecuteReader();
                tbl = dr.GetSchemaTable();
                dr.Close();

                if (DbConfigManager.LogQuery)
                    log.LogDebug("GetSchemaTable, HashCode=" + conn.GetHashCode().ToString() + ", Query : " + cmd.CommandText);
            }
            catch (Exception ex)
            {

                if ((dr != null))
                    dr.Close();

                if (DbConfigManager.LogError)
                    log.LogError("GetSchemaTable, HashCode=" + conn.GetHashCode().ToString() + ", Query : " + cmd.CommandText + ", Error : ", ex);


                throw new DbException(ex.Message, ex.InnerException, null, 0);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    if (DbConfigManager.LogConnection)
                        log.LogDebug("Close Connection, HashCode=" + conn.GetHashCode().ToString());

                    conn.Close();
                }
            }

            return tbl;
        }


        private DataTable GetDataTable(IDbCommand cmd, QueryCommand queryCmd)
        {

            DataTable tbl = new DataTable();
            IDataReader dr = null;
            IDbConnection conn = Db.GetConnection(queryCmd.ConfigName);
            cmd.Connection = conn;

            queryCmd.LastResult = -1;
            queryCmd.LastException = null;

            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                    if (DbConfigManager.LogConnection)
                        log.LogDebug("Open Connection, HashCode=" + conn.GetHashCode().ToString());
                }

                dr = cmd.ExecuteReader();
                tbl.Load(dr);
                dr.Close();

                queryCmd.LastResult = tbl.Rows.Count;

                if (DbConfigManager.LogQuery)
                    log.LogDebug("ExecuteQuery, HashCode=" + conn.GetHashCode().ToString() + ", Query : " + cmd.CommandText);
            }
            catch (Exception ex)
            {
                queryCmd.LastException = ex;

                if ((dr != null))
                    dr.Close();

                if (DbConfigManager.LogError)
                    log.LogError("GetDataTable, HashCode=" + conn.GetHashCode().ToString() + ", Query=" + cmd.CommandText + ", Error : ", ex);

                throw new DbException(ex.Message, ex.InnerException, queryCmd, 0);

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    if (DbConfigManager.LogConnection)
                        log.LogDebug("Close Connection, HashCode=" + conn.GetHashCode().ToString());

                    conn.Close();
                }
            }

            return tbl;
        }

        public DataTable ExecuteQuery(QueryCommand cmdQuery, int rowIndex, int fetchSize, string orderBy)
        {

            //CODE FOR MS SQL SERVER
            string cmdText = cmdQuery.CommandText;

            int x = cmdText.ToUpper().IndexOf("SELECT") + 6;
            cmdText = cmdText.Insert(x, " ROW_NUMBER() OVER( ORDER BY " + orderBy + " ) AS ROWNUM, ");
            cmdText = string.Format("SELECT * FROM( {0} ) AS QUERY1 WHERE ROWNUM BETWEEN {1} AND {2} ORDER BY QUERY1.{3}", cmdText, rowIndex, fetchSize + (rowIndex - 1), orderBy);

            IDbCommand cmd = cmdQuery.ToIDbCommand();
            cmd.CommandText = cmdText;

            return this.GetDataTable(cmd, cmdQuery);

        }


        public int ExecuteNonQuery(IList<QueryCommand> cmdQueries)
        {

            int rowEffect = -1;
            List<List<QueryCommand>> queryCmds = new List<List<QueryCommand>>();
            List<IDbConnection> conns = new List<IDbConnection>();
            List<QueryCommand> lstCmd = null;

            IDbConnection conn = null;
            String prevCfgName = String.Empty;


            foreach (QueryCommand cmd1 in cmdQueries)
            {
                if (prevCfgName != cmd1.ConfigName)
                {
                    prevCfgName = cmd1.ConfigName;

                    lstCmd = new List<QueryCommand>();
                    queryCmds.Add(lstCmd);
                    conn = Db.GetConnection(prevCfgName);
                    conns.Add(conn);
                }

                if (lstCmd != null && conn != null)
                {
                    IDbCommand cmd = cmd1.ToIDbCommand();
                    cmd.CommandText = cmd1.CommandText;
                    cmd.CommandType = cmd1.CommandType;
                    cmd.Connection = conn;
                    lstCmd.Add(cmd1);
                }
            }


            List<IDbTransaction> tranxs = new List<IDbTransaction>();
            QueryCommand lastQueryCmd = null;
            int errIndex = 0;
            try
            {
                foreach (List<QueryCommand> lstCmds in queryCmds)
                {
                    foreach (QueryCommand qCmd in lstCmds)
                    {

                        lastQueryCmd = qCmd;
                        IDbCommand cmdObj = qCmd.ToIDbCommand();

                        if (cmdObj.Connection != null && cmdObj.Connection.State == ConnectionState.Closed)
                        {
                            cmdObj.Connection.Open();
                            IDbTransaction tranx = cmdObj.Connection.BeginTransaction();
                            tranxs.Add(tranx);

                            if (DbConfigManager.LogConnection)
                                log.LogDebug("Open Connection, HashCode=" + cmdObj.Connection.GetHashCode().ToString() + ", Tranx=" + tranx.GetHashCode().ToString());
                        }

                        cmdObj.Transaction = tranxs[tranxs.Count - 1];
                        rowEffect = cmdObj.ExecuteNonQuery();
                        qCmd.LastResult = rowEffect;
                        qCmd.LastException = null;


                        if (DbConfigManager.LogQuery)
                            log.LogDebug("ExecuteScalar, HashCode=" + cmdObj.Connection.GetHashCode().ToString() + ",Tranx=" + cmdObj.Transaction.GetHashCode().ToString() + ", Query=" + cmdObj.CommandText);


                        errIndex++;
                    }
                }

                foreach (IDbTransaction tranx in tranxs)
                {
                    tranx.Commit();

                    if (DbConfigManager.LogQuery)
                        log.LogDebug("ExecuteScalar:Tranx Commit, Tranx=" + tranx.GetHashCode().ToString());
                }

            }
            catch (Exception ex)
            {

                if (lastQueryCmd != null)
                {
                    lastQueryCmd.LastException = ex;
                    lastQueryCmd.LastResult = -1;
                }

                if (DbConfigManager.LogError)
                {
                    if (lastQueryCmd.ToIDbCommand().Connection != null)
                        log.LogError("ExecuteNonQuery, HashCode=" + lastQueryCmd.ToIDbCommand().Connection.GetHashCode().ToString() + ", Query=" + lastQueryCmd.CommandText + ", Error=", ex);
                }

                foreach (IDbTransaction tranx in tranxs)
                {
                    tranx.Rollback();
                    if (DbConfigManager.LogQuery)
                        log.LogDebug("ExecuteScalar:Tranx Rollback, Tranx=" + tranx.GetHashCode().ToString());
                }

                throw new DbException(ex.Message, ex.InnerException, lastQueryCmd, errIndex);

            }
            finally
            {

                foreach (IDbConnection conn1 in conns)
                {
                    if (conn1 != null && conn1.State == ConnectionState.Open)
                    {
                        conn1.Close();

                        if (DbConfigManager.LogConnection)
                            log.LogDebug("Close Connection, HashCode=" + conn1.GetHashCode().ToString());
                    }
                }
            }

            return rowEffect;
        }


        public int ExecuteNonQuery(params QueryCommand[] cmdQueries)
        {
            int rowEffect = 0;

            IList<QueryCommand> lstCmds = new List<QueryCommand>();

            foreach (QueryCommand cmd in cmdQueries)
                lstCmds.Add(cmd);

            rowEffect = ExecuteNonQuery(lstCmds);

            return rowEffect;
        }

        public int ExecuteNonQuery(IList<SqlEasy> sqlEasies)
        {
            List<QueryCommand> cmds = new List<QueryCommand>();

            foreach (SqlEasy sqlEasy in sqlEasies)
                cmds.AddRange(sqlEasy.ToListOfQueryCommands());

            return this.ExecuteNonQuery(cmds);
        }

        public int ExecuteNonQuery(params SqlEasy[] sqlEasies)
        {
            List<QueryCommand> cmds = new List<QueryCommand>();

            foreach (SqlEasy sqlEasy in sqlEasies)
                cmds.AddRange(sqlEasy.ToListOfQueryCommands());


            return this.ExecuteNonQuery(cmds);
        }

        public int ExecuteNonQuery(QueryCommand queryCmd, AutoTransaction autoTranx)
        {

            int result = 0;

            if (autoTranx == AutoTransaction.Auto)
            {
                return this.ExecuteNonQuery(queryCmd);
            }
            else if (autoTranx == AutoTransaction.Manual)
            {

                IDbCommand cmd = queryCmd.ToIDbCommand();
                IDbConnection conn = null;

                if (cmd.Connection == null)
                    conn = Db.GetConnection(queryCmd.ConfigName);

                cmd.Connection = conn;

                try
                {
                    cmd.CommandText = queryCmd.CommandText;
                    cmd.CommandType = queryCmd.CommandType;

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                        if (DbConfigManager.LogConnection)
                            log.LogDebug("Open Connection ManualTransaction, HashCode=" + conn.GetHashCode().ToString());
                    }


                    result = cmd.ExecuteNonQuery();
                    queryCmd.LastException = null;
                    queryCmd.LastResult = result;

                    if (DbConfigManager.LogQuery && cmd.Connection != null)
                        log.LogDebug("ExecuteNonQuery ManualTransaction, HashCode=" + cmd.Connection.GetHashCode().ToString() + ", Query=" + cmd.CommandText);

                }
                catch (Exception ex)
                {
                    result = -1;

                    queryCmd.LastException = ex;
                    queryCmd.LastResult = result;

                    if (cmd.Connection == null)
                    {
                        log.LogError("ExecuteNonQuery ManualTransaction: Connection Object Can't be NULL");

                        throw new Exception("ManualTransaction, Connection Object Can't Be Null!");
                    }
                    else
                    {

                        if (DbConfigManager.LogError)
                            log.LogError("ExecuteNonQuery ManualTransaction, HashCode=" + cmd.Connection.GetHashCode().ToString() + ", Query=" + cmd.CommandText + ", Error : ", ex);

                        throw new DbException(ex.Message, ex.InnerException, queryCmd, 0);
                    }
                }
                finally
                {
                    if (conn != null && conn.State == ConnectionState.Open)
                    {
                        if (DbConfigManager.LogConnection)
                            log.LogDebug("Close Connection ManualTransaction, HashCode=" + conn.GetHashCode().ToString());

                        conn.Close();
                    }
                }

            }
            return result;
        }


        public object ExecuteScalar(QueryCommand queryCmd)
        {
            object result = null;
            IDbCommand cmd = queryCmd.ToIDbCommand();
            IDbConnection conn = Db.GetConnection(queryCmd.ConfigName);
            cmd.Connection = conn;

            queryCmd.LastResult = 0;

            try
            {

                cmd.CommandText = queryCmd.CommandText;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    if (DbConfigManager.LogConnection)
                        log.LogDebug("Open Connection, HashCode=" + conn.GetHashCode().ToString());
                }


                result = cmd.ExecuteScalar();

                if (result != null || result != DBNull.Value)
                    queryCmd.LastResult = 1;

                if (DbConfigManager.LogQuery)
                    log.LogDebug("ExecuteScalar, HashCode=" + cmd.Connection.GetHashCode().ToString() + ", Query=" + cmd.CommandText);
            }
            catch (Exception ex)
            {
                queryCmd.LastResult = -1;
                queryCmd.LastException = ex;

                if (DbConfigManager.LogError)
                    log.LogError("ExecuteScalar, HashCode=" + cmd.Connection.GetHashCode().ToString() + ", Query=" + cmd.CommandText + ", Error=", ex);

                throw new DbException(ex.Message, ex.InnerException, queryCmd, 0);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    if (DbConfigManager.LogConnection)
                        log.LogDebug("Close Connection, HashCode=" + conn.GetHashCode().ToString());

                    conn.Close();
                }
            }

            return result;
        }

    }
}
