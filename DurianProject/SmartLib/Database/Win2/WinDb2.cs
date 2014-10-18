using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using SmartLib.Database.Config;
using SmartLib.Logger;


namespace SmartLib.Database.Win
{
    public class WinDb2 : IDb
    {
        private static IDbConnection _dbConn;
        private static Dictionary<String, DbConfigInfo> _allDbCfgInfos;
        //private static Dictionary<String, DbConnection> _dbConnObjs;
        private ILogWriter logWriter = LogWriterFactory.Create(Db.DB_LOGGER_NAME, typeof(WinDb2));

        public WinDb2()
        {
            if (_allDbCfgInfos == null)
                _allDbCfgInfos = DbConfigManager.GetAllConfigInfos();

            //if (_dbConnObjs == null)
            //    this.CreateDbConnection();
        }


        private void CreateDbConnection()
        {

        }


        public string ConnectionString
        {
            get
            {
                if (DbConfigManager.GetDefaultConfigInfo() != null)
                    return DbConfigManager.GetDefaultConfigInfo().ConnectionString;

                return null;
            }
        }


        public String GetConnectionString(String configName)
        {
            String connStr = null;


            if (_allDbCfgInfos.ContainsKey(configName))
                connStr = _allDbCfgInfos[configName].ConnectionString;

            return connStr;
        }


        public DbConnection GetConnection()
        {
            DbConfigInfo defCfg = DbConfigManager.GetDefaultConfigInfo();
            DbConnection dbConn = null;

            if (defCfg.ConnectionClass.IndexOf("SqlClient") > -1)
                dbConn = new System.Data.SqlClient.SqlConnection();
            else if (defCfg.ConnectionClass.IndexOf("OleDb") > -1)
                dbConn = new System.Data.OleDb.OleDbConnection();
            else
                dbConn = (DbConnection)Activator.CreateInstance(defCfg.AssemblyName, defCfg.ConnectionClass).Unwrap();

            if (dbConn != null)
            {
                dbConn.ConnectionString = defCfg.ConnectionString;
            }

            return dbConn;
        }


        public DbConnection GetConnection(String configName)
        {
            DbConnection dbConn = null;

            if (_allDbCfgInfos.ContainsKey(configName))
            {
                DbConfigInfo defCfg = _allDbCfgInfos[configName];

                if (defCfg.ConnectionClass.IndexOf("SqlClient") > -1)
                    dbConn = new System.Data.SqlClient.SqlConnection();
                else if (defCfg.ConnectionClass.IndexOf("OleDb") > -1)
                    dbConn = new System.Data.OleDb.OleDbConnection();
                else
                    dbConn = (DbConnection)Activator.CreateInstance(defCfg.AssemblyName, defCfg.ConnectionClass).Unwrap();

                if (dbConn != null)
                {
                    dbConn.ConnectionString = defCfg.ConnectionString;

                    //if (dbConn.State == ConnectionState.Open)
                    //    dbConn.Close();

                    //dbConn.Open();

                    //if (DbConfigManager.LogConnection)
                    //    logWriter.LogDebug("Open Connection, HashCode=" + dbConn.GetHashCode().ToString());
                }

            }

            return dbConn;
        }


        public IDbCommand CreateCommand()
        {
            IDbCommand cmd = null;
            DbConfigInfo defCfg = DbConfigManager.GetDefaultConfigInfo();

            if (defCfg.ConnectionClass.IndexOf("SqlClient") > -1)
                cmd = new System.Data.SqlClient.SqlCommand();
            else if (defCfg.ConnectionClass.IndexOf("OleDb") > -1)
                cmd = new System.Data.OleDb.OleDbCommand();
            else
            {
                cmd = this.GetConnection().CreateCommand();
                cmd.Connection = null;
            }

            return cmd;
        }


        public IDbCommand CreateCommand(String configName)
        {
            IDbCommand cmd = null;

            if (_allDbCfgInfos.ContainsKey(configName))
            {
                DbConfigInfo defCfg = _allDbCfgInfos[configName];

                if (defCfg.ConnectionClass.IndexOf("SqlClient") > -1)
                    cmd = new System.Data.SqlClient.SqlCommand();
                else if (defCfg.ConnectionClass.IndexOf("OleDb") > -1)
                    cmd = new System.Data.OleDb.OleDbCommand();
                else
                {
                    cmd = this.GetConnection(configName).CreateCommand();
                    cmd.Connection = null;
                }
            }
            return cmd;
        }


        public void CloseConnection()
        {

        }


        public void CloseConnection(String configName)
        {

        }

        public void CloseAllConnections()
        {


        }
    }
}
