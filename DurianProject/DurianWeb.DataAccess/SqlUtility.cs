using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Web.Configuration;
using SmartLib.Logger;
using SmartLib.Database;
using System.Web.UI.WebControls;

namespace DurianWeb.DataAccess
{
    public class SqlUtility
    {
        //Public Structure SqlUtility
        //    Dim conn As SqlConnection = GetConnection()

        //End Structure
        private static ILogWriter log = LogWriterFactory.Create(LoggerName.WebApplicationLog, typeof(SqlUtility));
        public static SqlConnection GetConnection()
        {
            ApplicationException er = new ApplicationException();
            string connectionString = Db.ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    log.LogError("GetConnection", ex);
                    throw ex;

                }
            }
            return conn;
        }



        public static void SqlExecute(string sql, SqlParameterCollection opc)
        {
            SqlConnection conn = null;
            try
            {
                conn = GetConnection();
                SqlCommand cmd = new SqlCommand(sql, conn);

                if (opc.Count > 0)
                {
                    foreach (SqlParameter op in opc)
                    {
                        cmd.Parameters.Add(op.ParameterName, op.SqlDbType, op.Size).Value = op.Value;
                    }
                }
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                log.LogError("SqlExecute", ex);
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }



        public static void SqlExecute(string sql, SqlParameterCollection opc, SqlTransaction tran, SqlConnection Con)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql, Con, tran);

                if (opc.Count > 0)
                {
                    foreach (SqlParameter op in opc)
                    {
                        cmd.Parameters.Add(op.ParameterName, op.SqlDbType, op.Size).Value = op.Value;
                    }
                }
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                log.LogError("SqlExecute", ex);
                throw ex;
            }
        }

        public static DataSet SqlExecuteSP(string sql, SqlParameterCollection opc, CommandType type = CommandType.StoredProcedure, SqlTransaction tran = null, SqlConnection con = null)
        {

            SqlCommand objCmd = new SqlCommand();
            SqlDataAdapter dtAdapter = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                if ((con == null))
                {
                    con = GetConnection();
                }
                var _with1 = objCmd;
                _with1.Connection = con;
                _with1.CommandText = sql;
                _with1.CommandType = type;
                _with1.Transaction = tran;

                if (opc.Count > 0)
                {
                    foreach (SqlParameter op in opc)
                    {
                        objCmd.Parameters.Add(op.ParameterName, op.SqlDbType, op.Size).Value = op.Value;
                    }
                }
                dtAdapter.SelectCommand = objCmd;
                dtAdapter.Fill(ds);

            }
            catch (Exception ex)
            {
                log.LogError("SqlExecute", ex);
                throw ex;
            }
            finally
            {
                dtAdapter.Dispose();
            }
            return ds;
        }

        public static object SqlExecuteScalar(string sql, SqlParameterCollection opc)
        {

            object obj = null;
            SqlConnection conn = null;

            try
            {
                conn = GetConnection();

                SqlCommand cmd = new SqlCommand(sql, conn);

                if (opc.Count > 0)
                {
                    foreach (SqlParameter op in opc)
                    {
                        cmd.Parameters.Add(op.ParameterName, op.SqlDbType, op.Size).Value = op.Value;
                    }
                }
                obj = cmd.ExecuteScalar();
                return obj;
            }
            catch (Exception ex)
            {
                log.LogError("SqlExecuteScalar", ex);
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public static object SqlExecuteScalar(string sql, SqlParameterCollection opc, SqlConnection conn, SqlTransaction tran)
        {

            object obj = null;
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn, tran);

                if (opc.Count > 0)
                {
                    foreach (SqlParameter op in opc)
                    {
                        cmd.Parameters.Add(op.ParameterName, op.SqlDbType, op.Size).Value = op.Value;
                    }
                }
                obj = cmd.ExecuteScalar();
                return obj;
            }
            catch (Exception ex)
            {
                log.LogError("SqlExecuteScalar", ex);
                throw ex;
            }
        }
        public static string SqlToTransaction(IList<string> SqlList)
        {

            SqlConnection objConn = GetConnection();
            SqlCommand objCmd = new SqlCommand();
            SqlTransaction Trans = null;
            Exception Excep = new Exception();

            Trans = objConn.BeginTransaction(IsolationLevel.ReadCommitted);
            //*** Start Transaction ***'	

            var _with2 = objCmd;
            _with2.Connection = objConn;
            _with2.Transaction = Trans;
            //*** Command & Transaction ***'

            try
            {
                foreach (string Sql in SqlList)
                {
                    objCmd.CommandText = Sql;
                    objCmd.CommandType = CommandType.Text;
                    objCmd.ExecuteNonQuery();
                }
                Trans.Commit();
                //*** Commit Transaction ***'	
            }
            catch
            {
                log.LogError("SqlToTransaction", Excep);
                Trans.Rollback();
                //*** RollBack Transaction ***'
            }
            finally
            {
                Trans.Dispose();
                objConn.Close();
                objConn.Dispose();
            }
            return Excep.Message;
        }

        public static string SqlToTransaction(IList<SqlParam> SqlList)
        {

            SqlConnection objConn = GetConnection();
            SqlCommand objCmd = new SqlCommand();
            SqlTransaction Trans = null;
            Exception Excep = new Exception();

            Trans = objConn.BeginTransaction(IsolationLevel.ReadCommitted);
            //*** Start Transaction ***'	

            var _with3 = objCmd;
            _with3.Connection = objConn;
            _with3.Transaction = Trans;
            //*** Command & Transaction ***'

            try
            {
                foreach (SqlParam Sql in SqlList)
                {
                    objCmd.CommandText = Sql.Sql;
                    objCmd.CommandType = CommandType.Text;

                    if (Sql.Opc.Count > 0)
                    {
                        foreach (SqlParameter op in Sql.Opc)
                        {
                            objCmd.Parameters.Add(op.ParameterName, op.SqlDbType, op.Size).Value = op.Value;
                        }
                    }

                    objCmd.ExecuteNonQuery();
                }
                Trans.Commit();
                //*** Commit Transaction ***'	

            }
            catch
            {
                log.LogError("SqlToTransaction", Excep);
                Trans.Rollback();
                //*** RollBack Transaction ***'
            }
            finally
            {
                Trans.Dispose();
                objConn.Close();
                objConn.Dispose();
            }
            return Excep.Message;
        }

        public static object SqlToValue(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            object obj = null;

            SqlConnection con = new SqlConnection();
            try
            {
                con = GetConnection();
                cmd.Connection = con;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                obj = cmd.ExecuteScalar();
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                log.LogError("SqlToValue", ex);
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }

            return obj;
        }

        public static object SqlToValue(string sql, SqlParameterCollection opc, CommandType type = CommandType.Text)
        {
            object objRes = null;
            SqlConnection objConn = GetConnection();
            SqlCommand objCmd = new SqlCommand();

            try
            {
                var _with4 = objCmd;
                _with4.Connection = objConn;
                _with4.CommandText = sql;
                _with4.CommandType = type;

                if (opc.Count > 0)
                {
                    foreach (SqlParameter op in opc)
                    {
                        objCmd.Parameters.Add(op.ParameterName, op.SqlDbType, op.Size).Value = op.Value;
                    }
                }

                objRes = objCmd.ExecuteScalar();


            }
            catch (Exception ex)
            {
                log.LogError("SqlToValue", ex);
                throw ex;

            }
            finally
            {
                objConn.Close();
                objConn.Dispose();
            }
            return objRes;
        }

        public static void TableToDropDown(DropDownList ddl, DataTable dt, string TextField, string ValueField, string EmptyText)
        {
            ddl.DataSource = dt;
            ddl.DataTextField = TextField;
            ddl.DataValueField = ValueField;
            ddl.DataBind();
            if (!string.IsNullOrEmpty(EmptyText))
            {
                ddl.Items.Insert(0, new ListItem(EmptyText, ""));
            }
        }

        public static DataTable SqlToTable(string sql)
        {

            SqlConnection objConn = GetConnection();
            SqlCommand objCmd = new SqlCommand();
            SqlDataAdapter dtAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                var _with5 = objCmd;
                _with5.Connection = objConn;
                _with5.CommandText = sql;
                _with5.CommandType = CommandType.Text;
                dtAdapter.SelectCommand = objCmd;
                dtAdapter.Fill(dt);


            }
            catch (Exception ex)
            {
                log.LogError("SqlToTable", ex);
                throw ex;
            }
            finally
            {
                dtAdapter.Dispose();
                objConn.Close();
                objConn.Dispose();
            }

            return dt;
        }

        public static DataTable SqlToTable(string sql, SqlParameterCollection opc, CommandType type = CommandType.Text)
        {

            SqlConnection objConn = GetConnection();
            SqlCommand objCmd = new SqlCommand();
            SqlDataAdapter dtAdapter = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                var _with6 = objCmd;
                _with6.Connection = objConn;
                _with6.CommandText = sql;
                _with6.CommandType = type;

                if (opc.Count > 0)
                {
                    foreach (SqlParameter op in opc)
                    {
                        objCmd.Parameters.Add(op.ParameterName, op.SqlDbType, op.Size).Value = op.Value;
                    }
                }
                dtAdapter.SelectCommand = objCmd;
                dtAdapter.Fill(ds);
            }
            catch (Exception ex)
            {
                log.LogError("SqlToTable", ex);
                throw ex;
            }
            finally
            {
                dtAdapter.Dispose();
                objConn.Close();
                objConn.Dispose();
            }
            return ds.Tables[0];
        }

        public static string TableToJson(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row = null;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }

        public static DataTable SqlToTable(string sql, SqlParameterCollection opc, SqlTransaction tran, SqlConnection Con)
        {
            DataTable dt = new DataTable();

            try
            {
                //Dim objConn As SqlConnection = GetConnection()
                SqlCommand objCmd = new SqlCommand();
                SqlDataAdapter dtAdapter = new SqlDataAdapter();

                var _with7 = objCmd;
                _with7.Connection = Con;
                _with7.CommandText = sql;
                _with7.Transaction = tran;
                _with7.CommandType = CommandType.Text;
                foreach (SqlParameter op in opc)
                {
                    objCmd.Parameters.Add(op.ParameterName, op.SqlDbType, op.Size).Value = op.Value;
                }

                dtAdapter.SelectCommand = objCmd;
                dtAdapter.Fill(dt);
                dtAdapter.Dispose();
            }
            catch (Exception ex)
            {
                log.LogError("SqlToTable", ex);
                throw ex;
            }
            return dt;
        }
        /*
        public static void TableToDropDown(DropDownList dropDownList, object p2 = null, DataTable dataTable = null, string p4 = null, string p5 = null, string p6 = null)
        {
            throw new NotImplementedException();
        }

        public static SqlDbType GetSqlType(string TableName, string ColumnName)
        {

            string sql = string.Format("SELECT t.name AS type_name FROM sys.columns AS c JOIN sys.types AS t ON c.user_type_id=t.user_type_id WHERE c.object_id = OBJECT_ID('{0}') AND c.name = '{1}'", TableName, ColumnName);
            string sqlType = this.SqlToValue(sql);
            SqlDbType _dbType = (SqlDbType)Enum.Parse(typeof(SqlDbType), sqlType, true);
            return _dbType;

        }*/

    }

    public class SqlParam
    {
        public string Sql { get; set; }
        public SqlParameterCollection Opc { get; set; }
    }

}
