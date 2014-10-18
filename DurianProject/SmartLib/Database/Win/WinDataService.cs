using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using SmartLib.Database.Config;
using SmartLib.Logger;

namespace SmartLib.Database.Win
{
	public class WinDataService : IDataService
	{
		private ILogWriter log = LogWriterFactory.Create(Db.DB_LOGGER_NAME, typeof(WinDataService));

		public DataTable ExecuteQuery(QueryCommand cmdQuery) {
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

		public DataTable GetSchemaTable( String configName )
		{
			DataTable tbl = new DataTable();
			IDbCommand cmd = Db.CreateCommand(configName);
			IDataReader dr = null;

			try
			{
				dr = cmd.ExecuteReader();
				tbl = dr.GetSchemaTable();
				dr.Close();

				if (DbConfigManager.LogQuery)
					log.LogDebug("GetSchemaTable, HashCode=" + cmd.Connection.GetHashCode().ToString() + ", Query : " + cmd.CommandText);
			}
			catch (Exception ex)
			{

				if ((dr != null))
					dr.Close();

				if (DbConfigManager.LogError)
					log.LogError("GetSchemaTable, HashCode=" + cmd.Connection.GetHashCode().ToString() + ", Query : " + cmd.CommandText + ", Error : ", ex);


				throw new DbException(ex.Message, ex.InnerException, null, 0);
			}

			return tbl;
		}



		public DataTable GetSchemaTable()
		{
			DataTable tbl = new DataTable();
			IDbCommand cmd = Db.CreateCommand();
			IDataReader dr = null;

			try
			{
				dr = cmd.ExecuteReader();
				tbl = dr.GetSchemaTable();
				dr.Close();

				if (DbConfigManager.LogQuery)
					log.LogDebug("GetSchemaTable, HashCode=" + cmd.Connection.GetHashCode().ToString() + ", Query : " + cmd.CommandText);
			}
			catch (Exception ex)
			{

				if ((dr != null))
					dr.Close();

				if (DbConfigManager.LogError)
					log.LogError("GetSchemaTable, HashCode=" + cmd.Connection.GetHashCode().ToString() + ", Query : " + cmd.CommandText + ", Error : ", ex);


				throw new DbException(ex.Message, ex.InnerException, null, 0);
			}

			return tbl;
		}


		private DataTable GetDataTable(IDbCommand cmd, QueryCommand queryCmd) {

			DataTable tbl = new DataTable();
			IDataReader dr = null;

			queryCmd.LastResult = -1;
			queryCmd.LastException = null;

			try
			{
				dr = cmd.ExecuteReader();
				tbl.Load(dr);
				dr.Close();

				queryCmd.LastResult = tbl.Rows.Count;

				if (DbConfigManager.LogQuery)
					log.LogDebug("ExecuteQuery, HashCode=" + cmd.Connection.GetHashCode().ToString() + ", Query : " + cmd.CommandText);
			}
			catch (Exception ex)
			{
				queryCmd.LastException = ex;
				
				if ((dr != null))
					dr.Close();

				if (DbConfigManager.LogError)
					log.LogError("ExecuteQuery, HashCode=" + cmd.Connection.GetHashCode().ToString() + ", Query=" + cmd.CommandText + ", Error : ", ex);

				throw new DbException( ex.Message, ex.InnerException, queryCmd, 0);
			}

			return tbl;
		}

		public DataTable ExecuteQuery(QueryCommand cmdQuery, int rowIndex, int fetchSize, string orderBy ) {

			//CODE FOR MS SQL SERVER
			string cmdText = cmdQuery.CommandText;

			int x = cmdText.ToUpper().IndexOf("SELECT") + 6;
			cmdText = cmdText.Insert(x, " ROW_NUMBER() OVER( ORDER BY " + orderBy + " ) AS ROWNUM, ");
			cmdText = string.Format("SELECT * FROM( {0} ) AS QUERY1 WHERE ROWNUM BETWEEN {1} AND {2} ORDER BY QUERY1.{3}", cmdText, rowIndex, fetchSize + (rowIndex - 1), orderBy);

			IDbCommand cmd = cmdQuery.ToIDbCommand();
			cmd.CommandText = cmdText;

			return this.GetDataTable(cmd, cmdQuery);

		}


		public int ExecuteNonQuery(IList<QueryCommand> cmdQueries) {

			int rowEffect = -1;
			bool isMultipleDbConnection = this.IsMultipleDB(cmdQueries);

			if (cmdQueries == null)
				throw new Exception("List of QueryCommand can not be NULL.");

			if (cmdQueries.Count == 0)
				throw new Exception("List of QueryCommand must have since 1 Object.");

			IDbCommand cmd = null;
			QueryCommand queryCmd = null;
			int errIndex = 0;
			
			try {
				using (TransactionScope tranxScope = new TransactionScope()) {

					foreach (QueryCommand query in cmdQueries)
					{
						queryCmd = query;
						queryCmd.LastException = null;
						
						cmd = query.ToIDbCommand();
						cmd.CommandText = query.CommandText;
						rowEffect = cmd.ExecuteNonQuery();
						queryCmd.LastResult = rowEffect;
						
						errIndex ++;

						if (DbConfigManager.LogQuery)
							log.LogDebug("ExecuteNonQuery, HashCode=" + cmd.Connection.GetHashCode().ToString() + ", Query=" + cmd.CommandText);
					}
					tranxScope.Complete();
				}
			}
			catch (Exception ex)
			{
				rowEffect = -1;

				if (queryCmd != null)
				{
					queryCmd.LastException = ex;
					queryCmd.LastResult = -1;
				}

				String hashCode = "";
				if (cmd != null)
					hashCode = cmd.Connection.GetHashCode().ToString();

				if (DbConfigManager.LogError)
					log.LogError("ExecuteNonQuery, HashCode=" + hashCode + ", Query=" + cmd.CommandText + ", Error=", ex);

				throw new DbException(ex.Message, ex.InnerException, queryCmd, errIndex);
			}
			

			return rowEffect;
		}


		public int ExecuteNonQuery(params QueryCommand[] cmdQueries) {
			int rowEffect = -1;
			bool isMultipleDbConnection = this.IsMultipleDB(cmdQueries);

			if (cmdQueries == null)
				throw new Exception("List of QueryCommand can not be NULL.");

			if (cmdQueries.Length == 0)
				throw new Exception("List of QueryCommand must have since 1 Object.");

			IDbCommand cmd = null;
			QueryCommand queryCmd = null;
			int errIndex = 0;

			try{
				using (TransactionScope tranxScope = new TransactionScope()){
					foreach (QueryCommand query in cmdQueries){
						queryCmd = query;
						queryCmd.LastException = null;
												
						cmd = query.ToIDbCommand();
						cmd.CommandText = query.CommandText;
						rowEffect = cmd.ExecuteNonQuery();
						queryCmd.LastResult = rowEffect;
						
						errIndex ++;

						if (DbConfigManager.LogQuery)
							log.LogDebug("ExecuteNonQuery, HashCode=" + cmd.Connection.GetHashCode().ToString() + ", Query=" + cmd.CommandText);
					}

					tranxScope.Complete();
				}
			}
			catch (Exception ex){
				rowEffect = -1;
				
				if( queryCmd != null ) {
					queryCmd.LastException = ex;
					queryCmd.LastResult = -1;
				}

				String hashCode = "";
				if (cmd != null)
					hashCode = cmd.Connection.GetHashCode().ToString();

				if (DbConfigManager.LogError)
					log.LogError("ExecuteNonQuery, HashCode=" + hashCode + ", Query=" + cmd.CommandText + ", Error=", ex);

				throw new DbException(ex.Message, ex.InnerException, queryCmd, errIndex);
			}
			

			return rowEffect;
		}
		
		public int ExecuteNonQuery( IList<SqlEasy> sqlEasies ) {
			List<QueryCommand> cmds = new List<QueryCommand>();

			foreach (SqlEasy sqlEasy in sqlEasies)
				cmds.AddRange(sqlEasy.ToListOfQueryCommands());

			return this.ExecuteNonQuery(cmds);
		}
		
		public int ExecuteNonQuery( params SqlEasy[] sqlEasies ) {
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

				try
				{
					cmd.CommandText = queryCmd.CommandText;
					cmd.CommandType = queryCmd.CommandType;
					result = cmd.ExecuteNonQuery();
					queryCmd.LastException = null;
					queryCmd.LastResult = result;
				}
				catch (Exception ex)
				{
					result = -1;

					queryCmd.LastException = ex;
					queryCmd.LastResult = result;

					throw new DbException(ex.Message, ex.InnerException, queryCmd, 0);

				}

			}

			return result;
		}






		public object ExecuteScalar(QueryCommand queryCmd)
		{
			object result = null;
			IDbCommand cmd = queryCmd.ToIDbCommand();
			queryCmd.LastResult = 0;
			
			try
			{

				cmd.CommandText = queryCmd.CommandText;
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
					log.LogError("ExecuteScalar, HashCode=" + cmd.Connection.GetHashCode().ToString() + ", Query=" + cmd.CommandText +", Error=", ex);

				throw new DbException(ex.Message, ex.InnerException, queryCmd, 0);
			}

			return result;
		}
		
	}
}
