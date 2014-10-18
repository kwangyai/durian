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
	public class WinDb : IDb
	{
		private static DbConnection _dbConn;
		private static Dictionary<String, DbConfigInfo> _allDbCfgInfos;
		private static Dictionary<String, DbConnection> _dbConnObjs;
		private ILogWriter logWriter = LogWriterFactory.Create(Db.DB_LOGGER_NAME, typeof(WinDb));
		
		public WinDb()
		{
			if (_allDbCfgInfos == null )
				_allDbCfgInfos = DbConfigManager.GetAllConfigInfos();
			
			if( _dbConnObjs == null )
				this.CreateDbConnection();			
		}
		
		
		private void CreateDbConnection() {
			if( _dbConnObjs == null ) {
				_dbConnObjs = new Dictionary<String, DbConnection>();
				
				int i = 0;
				foreach( DbConfigInfo cfg in _allDbCfgInfos.Values ) {
					if( cfg != null ) {
						//DbConnection dbConn = (DbConnection)Activator.CreateInstance(cfg.AssemblyName, cfg.ConnectionClass).Unwrap();
						
						DbConnection dbConn = null;
						if (cfg.ConnectionClass.IndexOf("SqlClient") > -1) 
							dbConn = new System.Data.SqlClient.SqlConnection();
						else if (cfg.ConnectionClass.IndexOf("OleDb") > -1)
							dbConn = new System.Data.OleDb.OleDbConnection();
						else
							dbConn = (DbConnection)Activator.CreateInstance(cfg.AssemblyName, cfg.ConnectionClass).Unwrap();
						
						dbConn.ConnectionString = cfg.ConnectionString;
						_dbConnObjs.Add(cfg.ConfigurationName, dbConn);
						
						if( i == 0 )
							_dbConn = dbConn;
						
						if( cfg.ConfigurationName == DbConfigManager.DefaultConfigurationName )
							_dbConn = dbConn;
							
						i ++;
					}
				}
			}
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
		
		
		public String GetConnectionString( String configName )
		{
			String connStr = null;
			
			
			if( _allDbCfgInfos.ContainsKey(configName) )
				connStr = _allDbCfgInfos[configName].ConnectionString;
			
			return connStr;
		}
		
		
		public DbConnection GetConnection()
		{

            try
            {
                if (_dbConn.State == ConnectionState.Closed)
                {
                    _dbConn.Open();

                    if (DbConfigManager.LogConnection)
                        logWriter.LogDebug("Open Connection, HashCode=" + _dbConn.GetHashCode().ToString());
                }
            }
            catch (Exception ex)
            {
                DataService.RaiseEventOnDbConnectionError(DbConfigManager.DefaultConfigurationName, ex);
                throw ex;
            }
			
			return _dbConn;
		}
		
		
		public DbConnection GetConnection( String configName )
		{
			DbConnection conn = _dbConnObjs[configName];

            try
            {
                if( conn.State == ConnectionState.Closed ) {
                    conn.Open();

                    if (DbConfigManager.LogConnection)
                        logWriter.LogDebug("Open Connection, HashCode=" + conn.GetHashCode().ToString());
                }
            		
            }
            catch (Exception ex)
            {
                DataService.RaiseEventOnDbConnectionError(configName, ex);
                throw ex;
            }

			
			
			return conn;
		}
		
		
		public IDbCommand CreateCommand()
		{
			
			//if (_dbConn.State == ConnectionState.Closed) {
			//        _dbConn.Open();
			//}
			
			//return _dbConn.CreateCommand();
			
			return this.GetConnection().CreateCommand();
		}


		public IDbCommand CreateCommand(String configName)
		{

            return this.GetConnection(configName).CreateCommand();
		}
		
		
		public void CloseConnection()
		{
			if( _dbConn.State == ConnectionState.Open ) {
				_dbConn.Close();

				if (DbConfigManager.LogConnection)
					logWriter.LogDebug("Close Connection, HashCode=" + _dbConn.GetHashCode().ToString());
			}
		}
		
		
		public void CloseConnection( String configName )
		{
			DbConnection conn = _dbConnObjs[configName];

			if (conn.State == ConnectionState.Open) {
				conn.Close();

				if (DbConfigManager.LogConnection)
					logWriter.LogDebug("Close Connection, HashCode=" + conn.GetHashCode().ToString());
			}
		}
		
		public void CloseAllConnections()
		{
			foreach( DbConnection connObj in _dbConnObjs.Values )
			{
				if( connObj.State == ConnectionState.Open ) {
					connObj.Close();

					if (DbConfigManager.LogConnection)
						logWriter.LogDebug("Close Connection, HashCode=" + connObj.GetHashCode().ToString());
				}
			}
			
		}
	}
}