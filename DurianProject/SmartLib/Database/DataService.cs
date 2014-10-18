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
using System.Data;
using System.Collections.Generic;
using System.Transactions;
using SmartLib.Database.Config;
using SmartLib.Logger;

namespace SmartLib.Database
{

    public delegate void OnDbConnectionErrorEventHandler(String configName, Exception exc);

	public class DataService
	{
		private static ILogWriter log = LogWriterFactory.Create(Db.DB_LOGGER_NAME, typeof(DataService));
		private static IDataService ds;
        public static event OnDbConnectionErrorEventHandler OnDbConnectionError;
		
		static DataService() {
		
			if( System.Web.HttpContext.Current != null )
				ds = new SmartLib.Database.Web.WebDataService();
			else
				ds = new SmartLib.Database.Win.WinDataService();
		}
		
		
		public static DataTable GetSchemaTable() {
			return ds.GetSchemaTable();
		}
		
		public static DataTable GetSchemaTable(String configName) {
			return ds.GetSchemaTable(configName);
		}
		
		public static DataTable ExecuteQuery(QueryCommand queryCmd) {
			return ds.ExecuteQuery(queryCmd);
		}
		
		
		public static DataTable ExecuteQuery(QueryCommand queryCmd, int rowIndex, int fetchSize, string orderBy ) {

			return ds.ExecuteQuery(queryCmd, rowIndex, fetchSize, orderBy );
		}

		
		public static int ExecuteNonQuery( IList<QueryCommand> queryCmds) {

			return ds.ExecuteNonQuery(queryCmds);
		}

		public static int ExecuteNonQuery(IList<SqlEasy> sqlEasies) {
			return ds.ExecuteNonQuery(sqlEasies);
		}

		public static int ExecuteNonQuery(params SqlEasy[] sqlEasies) {
			return ds.ExecuteNonQuery(sqlEasies);
		}

		public static int ExecuteNonQuery(params QueryCommand[] queryCmds) {
			return ds.ExecuteNonQuery(queryCmds);
		}
		
		public static int ExecuteNonQuery( QueryCommand queryCmd, AutoTransaction autoTranx )
		{
			return ds.ExecuteNonQuery( queryCmd, autoTranx);
		}

        public static int ExecuteNonQuery( IList<QueryCommand> queryCmds, AutoTransaction autoTranx)
        {
            int ret = 0;
            foreach (QueryCommand cmd in queryCmds)
            {
                ret += ds.ExecuteNonQuery(cmd, autoTranx);
            }

            return ret;
        }


		public static object ExecuteScalar(QueryCommand queryCmd) {
			return ds.ExecuteScalar(queryCmd);
		}
		
		public static bool IsNotNull( Object obj ) {
		
			 if( obj == null )
				return false;
			else if( obj is DBNull || obj.Equals(DBNull.Value) )
				return false;
			
			return true;
		
		}


        public static bool CanConnectDb() {
            String defaultCfg = DbConfigManager.DefaultConfigurationName;
            return CanConnectDb(defaultCfg);
        }

        public static bool CanConnectDb(String configName) {

            bool ableToConnect = false;
            Dictionary<String, DbConfigInfo> cfgInfos = DbConfigManager.GetAllConfigInfos();

            if (cfgInfos.ContainsKey(configName))
            {
                try
                {
                    Db.GetConnection(configName);
                    ableToConnect = true;
                    
                }
                catch (Exception ex)
                {
                    ableToConnect = false;
                   
                    
                }
            }

            return ableToConnect;
            
        }

        internal static void RaiseEventOnDbConnectionError(String configName, Exception ex)
        {
            OnDbConnectionError(configName, ex);
        }
		

	}
}

