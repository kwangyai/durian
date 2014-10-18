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

namespace SmartLib.Database
{

	internal enum SqlStatementType
	{
		Insert,
		Delete,
		Update,
		InsertOrUpdate
	}


	//Update To New Version
	public enum SqlOperation
	{
		SelfUpdate,
		SubQuery
	}

	internal class SqlStatement
	{
		private String _tableName;
		private IDictionary<string, object> _values;
		private SqlCriteria _criteria;
		private String _whereStatement;
		private SqlStatementType _statementType;
		private String _configName;


		public String TableName {
			get { return this._tableName; }
			set { this._tableName = value; }
		}
		
		
		public String ConfigName {
			get{ return this._configName; }
			set{ this._configName = value; }
		}


		public SqlCriteria Criteria {
			get { return this._criteria; }
			set { this._criteria = value; }
		}


		public String Where {
			get { return this._whereStatement; }
			set { this._whereStatement = value; }
		}


		public SqlStatementType StatementType {
			get { return this._statementType; }
			set { this._statementType = value; }
		}


		public void SetValue(string columnName, object data)
		{
			this._values.Add(columnName, data);
		}


		public IDictionary<string, object> Values {
			get { return this._values; }
		}


		public SqlStatement()
		{
			this._values = new Dictionary<string, object>();
			this._criteria = new SqlCriteria();
		}


		public string ToSql(string paramPrefix)
		{
			string sql = String.Empty;
			string where = GenCriteriaStatement(paramPrefix);

			if (this._statementType == SqlStatementType.Insert)
				sql = this.GenInsertStatement(paramPrefix);
			else if (this._statementType == SqlStatementType.Update)
				sql = this.GenUpdateStatement(paramPrefix) + where;
			else if (this._statementType == SqlStatementType.Delete)
				sql = this.GenDeleteStatement(paramPrefix) + where;
			else if ( this._statementType == SqlStatementType.InsertOrUpdate ) {
				if( String.IsNullOrEmpty(where) )
					sql = this.GenInsertStatement(paramPrefix);
				else
					sql = this.GenInsertOrUpdateStatement(paramPrefix, where);
			}

			//HttpContext.Current.Response.Write(sql)
			return sql;
		}


		private string GenInsertStatement(string paramPrefix)
		{
			string instSt = String.Empty;

			if (this._values.Count > 0)
			{
				StringBuilder strInst = new StringBuilder();
				StringBuilder strVals = new StringBuilder();

				foreach (KeyValuePair<string, object> kvp in this._values) {
					strInst.AppendFormat(", {0}", kvp.Key);
					strVals.AppendFormat(", {0}{1}", paramPrefix, kvp.Key);
				}

				instSt = String.Format("INSERT INTO {0}( {1} ) VALUES( {2} ) ", this._tableName, strInst.ToString().Substring(1), strVals.ToString().Substring(1));
			}


			return instSt;
		}
		
		private String GenInsertOrUpdateStatement(String paramPrefix, String where)
		{
			String instSt = this.GenInsertStatement(paramPrefix);
			String upSt = this.GenUpdateStatement(paramPrefix) + where;

			String InstOrUpSt = String.Format("{0} IF @@ROWCOUNT=0 BEGIN {1} END", upSt, instSt);

			return InstOrUpSt;
		}


		private string GenDeleteStatement(string paramPrefix)
		{
			return String.Format("DELETE FROM {0} ", this._tableName);
		}

		//Update To New Version
		private string GenUpdateStatement(string paramPrefix)
		{
			string sql = String.Empty;

			if (this._values.Count > 0)
			{
				StringBuilder st = new StringBuilder();

				foreach (KeyValuePair<string, object> kvp in this._values) {

                    if (kvp.Value != null)
                    {

                        if ((!object.ReferenceEquals(kvp.Value.GetType(), typeof(SqlOperation))))
                        {
                            st.AppendFormat(", {0}={1}{2}", kvp.Key, paramPrefix, kvp.Key);
                        }
                        else
                        {
                            SqlOperation sqlType = (SqlOperation)kvp.Value;

                            if (sqlType.Equals(SqlOperation.SelfUpdate))
                            {
                                st.AppendFormat(",{0}", kvp.Key);
                            }

                        }

                    }
                    else
                    {
                        st.AppendFormat(", {0}={1}{2}", kvp.Key, paramPrefix, kvp.Key);
                    }

				}

				sql = String.Format("UPDATE {0} SET {1} ", this._tableName, st.ToString().Substring(1));
			}

			return sql;
		}


		private string GenCriteriaStatement(string paramPrefix)
		{
			string where = String.Empty;

			if (!String.IsNullOrEmpty(this._whereStatement))
			{
				where = String.Format(" WHERE {0} ", this._whereStatement);
			}

			return where;
		}


		public bool IsColumnDuplicated(string columnName)
		{
			return this._values.ContainsKey(columnName);
		}
	}
}
