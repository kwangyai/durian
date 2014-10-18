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
using System.Collections.Generic;
using System.Data;
using System.Text;
using SmartLib.Database.Config;
using SmartLib.Logger;

namespace SmartLib.Database
{

	public class SqlEasy
	{
        private ILogWriter _log = LogWriterFactory.Create(Db.DB_LOGGER_NAME, typeof(SqlEasy));
		
		private IList<QueryCommand> _queryCmds;
		private IList<SqlStatement> _sqlStatements;
		private String _configName;
		private int _lastResult;
		private Exception _lastException;
		private int _lastErrorIndex = -1;
		
		public SqlEasy() {
			this._queryCmds = new List<QueryCommand>();
			this._sqlStatements = new List<SqlStatement>();
		}
		
		public SqlEasy( String configName ) : this() {
			this._configName = configName;
		}
	
	
		public String ConfigName {
			get{ return this._configName; }
			set{ this._configName = value; }
		}
		
		public int LastResult {
			get{ return this._lastResult; }
			internal set{ this._lastResult = value; }
		}
		
		public int LastErrorIndex {
			get{ return this._lastErrorIndex; }
			internal set{ this._lastErrorIndex = value; }
		}
		
		public Exception LastException {
			get{ return this._lastException; }
			internal set{ this._lastException = value; }
		}
			

		public void NewInsertStatement(String tableName) {
			if( String.IsNullOrEmpty(this._configName) || this._configName == DbConfigManager.DefaultConfigurationName  )
                this.NewInsertStatement(tableName, DbConfigManager.DefaultConfigurationName);
			else if( ! String.IsNullOrEmpty( this._configName ) )
				this.NewInsertStatement(tableName, this._configName);
		}
		
		public void NewInsertStatement( String tableName, String configName ) {

			SqlStatement sqlSt = new SqlStatement();
			sqlSt.TableName = tableName;
			sqlSt.StatementType = SqlStatementType.Insert;
			
			if( ! String.IsNullOrEmpty( configName ) )
				sqlSt.ConfigName = configName;

			this._sqlStatements.Add(sqlSt);	
			
			this._lastException = null;
			this._lastResult = 0;
		}


		public WhereCriteria NewInsertOrUpdateStatement(String tableName)
		{

			WhereCriteria where = null;

			if (String.IsNullOrEmpty(this._configName) || this._configName == DbConfigManager.DefaultConfigurationName)
                where = this.NewInsertOrUpdateStatement(tableName, DbConfigManager.DefaultConfigurationName);
			else if (!String.IsNullOrEmpty(this._configName))
				where = this.NewInsertOrUpdateStatement(tableName, this._configName);

			this._lastException = null;
			this._lastResult = 0;

			return where;
		
		}


		public WhereCriteria NewInsertOrUpdateStatement(String tableName, String configName)
		{

			SqlStatement sqlSt = new SqlStatement();
			sqlSt.TableName = tableName;
			sqlSt.StatementType = SqlStatementType.InsertOrUpdate;

			if (!String.IsNullOrEmpty(configName))
				sqlSt.ConfigName = configName;

			WhereCriteria where = new WhereCriteria();
			where.Statement = sqlSt;

			this._sqlStatements.Add(sqlSt);

			this._lastException = null;
			this._lastResult = 0;

			return where;
		}
		

		public WhereCriteria NewUpdateStatement(String tableName) {

			WhereCriteria where = null;
			
			if (String.IsNullOrEmpty(this._configName) || this._configName == DbConfigManager.DefaultConfigurationName)
                where = this.NewUpdateStatement(tableName, DbConfigManager.DefaultConfigurationName);
			else if (!String.IsNullOrEmpty(this._configName))
				where = this.NewUpdateStatement(tableName, this._configName);

			this._lastException = null;
			this._lastResult = 0;
				
			return where;
		}
		
		
		public WhereCriteria NewUpdateStatement( String tableName, String configName ) {

			SqlStatement sqlSt = new SqlStatement();
			sqlSt.TableName = tableName;
			sqlSt.StatementType = SqlStatementType.Update;

			if (!String.IsNullOrEmpty(configName))
				sqlSt.ConfigName = configName;

			WhereCriteria where = new WhereCriteria();
			where.Statement = sqlSt;

			this._sqlStatements.Add(sqlSt);

			this._lastException = null;
			this._lastResult = 0;

			return where;
		}


		public WhereCriteria NewDeleteStatement(string tableName) {

			WhereCriteria where = null;

			if (String.IsNullOrEmpty(this._configName) || this._configName == DbConfigManager.DefaultConfigurationName)
                where = this.NewDeleteStatement(tableName, DbConfigManager.DefaultConfigurationName);
			else if (!String.IsNullOrEmpty(this._configName))
				where = this.NewDeleteStatement(tableName, this._configName);

			this._lastException = null;
			this._lastResult = 0;

			return where;
		}


		public WhereCriteria NewDeleteStatement(string tableName, String configName) {

			SqlStatement sqlSt = new SqlStatement();
			sqlSt.TableName = tableName;
			sqlSt.StatementType = SqlStatementType.Delete;

			if (!String.IsNullOrEmpty(configName))
				sqlSt.ConfigName = configName;

			WhereCriteria w = new WhereCriteria();
			w.Statement = sqlSt;

			this._sqlStatements.Add(sqlSt);

			this._lastException = null;
			this._lastResult = 0;

			return w;
		}


		public SqlCriteria Where(string pWhere) {
		
			SqlCriteria criteria = null;
			int lastIndex = this._sqlStatements.Count - 1;

			if (lastIndex > -1)
			{
				SqlStatement insSt = this._sqlStatements[lastIndex];

				if (insSt.StatementType == SqlStatementType.Insert) {
					throw new Exception("Can not to use criterias with a insertion statement.");
				}
				insSt.Where = pWhere;
				criteria = insSt.Criteria;
			}
			else
			{
				throw new Exception("Require New Sql Statement.");
			}

			return criteria;
		}


		public SqlCriteria Where(SqlCriteria criteria) {
		
			if (criteria == null)
			{
				throw new ArgumentException("Not Null parameter expected.");
			}
			else
			{
				int lastIndex = this._sqlStatements.Count - 1;

				if (lastIndex > -1)
				{
					SqlStatement insSt = this._sqlStatements[lastIndex];

					if (insSt.StatementType == SqlStatementType.Insert)
						throw new Exception("Can not to use criterias with a insertion statement.");
					
					insSt.Where = criteria.Where;
					insSt.Criteria = criteria;

					return insSt.Criteria;
				}
				else
				{
					throw new Exception("Require New Sql Statement.");
				}
			}
		}

		public void SetColumnValue<T>(string columnName, T value) {
		
			int lastIndex = this._sqlStatements.Count - 1;

			if (lastIndex > -1)
			{
				SqlStatement insSt = this._sqlStatements[lastIndex];

				if (!insSt.IsColumnDuplicated(columnName))
					insSt.SetValue(columnName, value);
				else
					throw new Exception("Duplicate ColumnName for a Insert statement.");
			}
			else
			{
				throw new Exception("Require Table Name.");
			}
		}

		public void SetColumnValue(string columnName, object value) {

            Object val = value;

            if (value != null)
            {
                if (value.GetType() == typeof(DateTime))
                {
                    DateTime dt = Convert.ToDateTime(value);
                    if( dt.Equals(DateTime.MinValue) )
                        val = null;
                }
            }


			this.SetColumnValue<object>(columnName, val);
		}


		public void Prepare() {
			int lastIndex = this._sqlStatements.Count - 1;

			if (lastIndex > -1)
			{
				SqlStatement insSt = new SqlStatement();
				insSt.TableName = this._sqlStatements[lastIndex].TableName;

				this._sqlStatements.Add(insSt);
			}
		}



		public IList<QueryCommand> ToListOfQueryCommands()
		{
			IList<QueryCommand> queryCmds = new List<QueryCommand>();
			DbConfigInfo dbEasyCfg = DbConfigManager.GetDefaultConfigInfo();

            if (dbEasyCfg == null)
            {
                this._log.LogError("CONFIG GURATION ERROR, CAN NOT GET DEFAULT CONFIGURATION INFO.");
                throw new Exception("CONFIG GURATION ERROR, CAN NOT GET DEFAULT CONFIGURATION INFO.");
            }

			foreach (SqlStatement insSt in this._sqlStatements) {
				if (((insSt.StatementType == SqlStatementType.Insert || insSt.StatementType == SqlStatementType.Update || insSt.StatementType == SqlStatementType.InsertOrUpdate) && insSt.Values.Count > 0) || insSt.StatementType == SqlStatementType.Delete)
				{
					QueryCommand queryCmd = new QueryCommand();
					
					if( ! String.IsNullOrEmpty( insSt.ConfigName ) )
						queryCmd.ConfigName = insSt.ConfigName;

					queryCmd.CommandText = insSt.ToSql(dbEasyCfg.DbParamPrefix);
					queryCmds.Add(queryCmd);

					foreach (KeyValuePair<string, object> kvp in insSt.Values) {
						queryCmd.SetParam(kvp.Key, kvp.Value);
					}

					if ((insSt.StatementType == SqlStatementType.Delete || insSt.StatementType == SqlStatementType.Update || insSt.StatementType == SqlStatementType.InsertOrUpdate) && !String.IsNullOrEmpty(insSt.Where))
					{
						IDictionary<string, object> d = insSt.Criteria.GetParams();

						foreach (KeyValuePair<string, object> kvp in d) {
							queryCmd.SetParam(kvp.Key, kvp.Value);
						}
					}
				}
			}

			return queryCmds;

		}





		public int[] ExecuteNonQuery() {

			int result = -1;
			List<Int32> results = new List<int>();
			
			IList<QueryCommand> queryCmds = this.ToListOfQueryCommands();
			
			try
			{
				result = DataService.ExecuteNonQuery(queryCmds);

				foreach (QueryCommand q in queryCmds)
					results.Add(q.LastResult);
			}
			catch( DbException ex )
			{
				this._lastResult = -1;
				
				if ( ex.ErrorIndex > - 1 && ex.ErrorIndex < queryCmds.Count)
				{
					this._lastException = queryCmds[ex.ErrorIndex].LastException;
					this._lastErrorIndex = ex.ErrorIndex;
				}
				
				results.Clear();
				foreach( QueryCommand q in queryCmds )
					results.Add(q.LastResult);
				
				throw ex;
			}
			
			
			return results.ToArray();
		}


        public int[] ExecuteNonQuery( AutoTransaction autoTrans )
        {

            int result = -1;
            List<Int32> results = new List<int>();

            IList<QueryCommand> queryCmds = this.ToListOfQueryCommands();

            try
            {
                result = DataService.ExecuteNonQuery(queryCmds, autoTrans);

                foreach (QueryCommand q in queryCmds)
                    results.Add(q.LastResult);
            }
            catch (DbException ex)
            {
                this._lastResult = -1;

                if (ex.ErrorIndex > -1 && ex.ErrorIndex < queryCmds.Count)
                {
                    this._lastException = queryCmds[ex.ErrorIndex].LastException;
                    this._lastErrorIndex = ex.ErrorIndex;
                }

                results.Clear();
                foreach (QueryCommand q in queryCmds)
                    results.Add(q.LastResult);

                throw ex;
            }


            return results.ToArray();
        }

		
	}

}
