//============================================================================== 

// SOURCE CODE BY TREEBHOPH THOOMSAN / treebhoph@yahoo.co.th 
// This library is free software; you can redistribute it and/or 
// modify it under the terms of the GNU Lesser General Public 
// License as published by the Free Software Foundation; either 
// version 2.1 of the License, or (at your option) any later version. 
// 
// This library is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
// Lesser General Public License for more details. 
// 
// You should have received a copy of the GNU Lesser General Public 
// License along with this library; if not, write to the Free Software 
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA 
//============================================================================== 

using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Net.Mail;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using SmartLib.Database.Config;
using SmartLib.Logger;


namespace SmartLib.Database.Web
{
	public class WebDb : IDb
	{
		private ILogWriter logWriter = LogWriterFactory.Create(Db.DB_LOGGER_NAME, typeof(WebDb));
		private static Dictionary<String, DbConfigInfo> _allDbCfgInfos;
		
		internal WebDb() {
			if (_allDbCfgInfos == null)
			        _allDbCfgInfos = DbConfigManager.GetAllConfigInfos();
		}

		private void CreateDbConnection()
		{
			if( HttpContext.Current != null ) {

				String nameForAll = HttpContext.Current.Request.GetHashCode().ToString() + "_ALL";
				String nameForDef = HttpContext.Current.Request.GetHashCode().ToString() + "_DEF";


				Dictionary<String, DbConnection> dbConnObjs = HttpContext.Current.Application[nameForAll] as Dictionary<String, DbConnection>;
				DbConnection defaultDbConn = HttpContext.Current.Application[nameForDef] as DbConnection;
				
				if( dbConnObjs == null ) {
					dbConnObjs = new Dictionary<String, DbConnection>();
					int i = 0;
					
					foreach (DbConfigInfo cfg in _allDbCfgInfos.Values) {
						if (cfg != null) {
						
							DbConnection dbConn = null;
							if (cfg.ConnectionClass.IndexOf("SqlClient") > -1)
								dbConn = new System.Data.SqlClient.SqlConnection();
							else if (cfg.ConnectionClass.IndexOf("OleDb") > -1)
								dbConn = new System.Data.OleDb.OleDbConnection();
							else
								dbConn = (DbConnection)Activator.CreateInstance(cfg.AssemblyName, cfg.ConnectionClass).Unwrap();

							dbConn.ConnectionString = cfg.ConnectionString;
							dbConnObjs.Add(cfg.ConfigurationName, dbConn);
							
							if( i == 0 )
								defaultDbConn = dbConn;
							
							if (cfg.ConfigurationName == DbConfigManager.DefaultConfigurationName)
								defaultDbConn = dbConn;
							
							i ++;
						}
					}
										
					HttpContext.Current.Application.Add(nameForAll, dbConnObjs );
					HttpContext.Current.Application.Add(nameForDef, defaultDbConn);
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

		public String GetConnectionString(String configName)
		{
			String connStr = null;


			if (_allDbCfgInfos.ContainsKey(configName))
				connStr = _allDbCfgInfos[configName].ConnectionString;

			return connStr;
		}


		public DbConnection GetConnection()
		{
			
			DbConnection conn = null;

			if (HttpContext.Current != null) {
			
				String nameForDef = HttpContext.Current.Request.GetHashCode().ToString() + "_DEF";
				conn = HttpContext.Current.Application[nameForDef] as DbConnection;

				if (conn == null)
				{
					this.CreateDbConnection();
					conn = HttpContext.Current.Application[nameForDef] as DbConnection;

					if (conn.State == ConnectionState.Open)
						conn.Close();

					conn.Open();
				}

				if (conn.State == ConnectionState.Closed)
					conn.Open();
					
				
				if( DbConfigManager.LogConnection )
					logWriter.LogDebug("Open Connection, HashCode=" + conn.GetHashCode().ToString());
			}

			return conn;
		}


		public DbConnection GetConnection(String configName) {
		
			String nameForAll = HttpContext.Current.Request.GetHashCode().ToString() + "_ALL";
			Dictionary<String, DbConnection> dbConnObjs = HttpContext.Current.Application[nameForAll] as Dictionary<String, DbConnection>;		
			
			DbConnection conn = null;
			if( dbConnObjs != null && dbConnObjs.ContainsKey(configName) ) {
				conn = dbConnObjs[configName];
				
				if (conn.State == ConnectionState.Closed)
					conn.Open();

				if (DbConfigManager.LogConnection)
					logWriter.LogDebug("Open Connection, HashCode=" + conn.GetHashCode().ToString());
			}

			return conn;
		}
			

		public IDbCommand CreateCommand() {
			IDbConnection conn = GetConnection();
			return conn.CreateCommand();
		}


		public IDbCommand CreateCommand(String configName) {
			IDbConnection conn = GetConnection(configName);
			return conn.CreateCommand();
		}


		public void CloseConnection() {
			if (HttpContext.Current != null) {
			
				String nameForDef = HttpContext.Current.Request.GetHashCode().ToString() + "_DEF";
				DbConnection conn = HttpContext.Current.Application[nameForDef] as DbConnection;

				if (conn != null) {

					if (DbConfigManager.LogConnection)
						logWriter.LogDebug("Close Connection, HashCode=" + conn.GetHashCode().ToString());
					
					conn.Close();
					HttpContext.Current.Application.Remove(nameForDef);
				}
			}

		}


		public void CloseConnection(String configName) {
			String nameForAll = HttpContext.Current.Request.GetHashCode().ToString() + "_ALL";
			Dictionary<String, DbConnection> dbConnObjs = HttpContext.Current.Application[nameForAll] as Dictionary<String, DbConnection>;		
			
			if( dbConnObjs != null && dbConnObjs.ContainsKey(configName) ) {

				DbConnection conn = dbConnObjs[configName];

				if (DbConfigManager.LogConnection)
					logWriter.LogDebug("Close Connection, HashCode=" + conn.GetHashCode().ToString());
					
				conn.Close();
			}
			
		}

		public void CloseAllConnections() {

			String nameForAll = HttpContext.Current.Request.GetHashCode().ToString() + "_ALL";
			Dictionary<String, DbConnection> dbConnObjs = HttpContext.Current.Application[nameForAll] as Dictionary<String, DbConnection>;

			if (dbConnObjs != null) {
				foreach (DbConnection conn in dbConnObjs.Values) {
					if (conn.State == ConnectionState.Open) {
						conn.Close();

						if (DbConfigManager.LogConnection)
							logWriter.LogDebug("Close Connection, HashCode=" + conn.GetHashCode().ToString());
						
					}
				}

				HttpContext.Current.Application.Remove(nameForAll);
			}	
		}


	}


}



