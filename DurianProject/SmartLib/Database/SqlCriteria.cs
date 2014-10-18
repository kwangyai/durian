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
	public class SqlCriteria
	{
		private string _paramName;
		private object _value;
		private SqlCriteria _next;
		private string _where;

		public string Where {
			get { return this._where; }
			set { this._where = value; }
		}

		public SqlCriteria Next {
			get { return this._next; }
			set { this._next = value; }
		}

		public string ParamName {
			get { return this._paramName; }
			set { this._paramName = value; }
		}

		public object Value {
			get { return this._value; }
			set { this._value = value; }
		}


		public SqlCriteria SetParam(string parameterName, object value)
		{
			return this.SetParam<object>(parameterName, value);
		}

		public SqlCriteria SetParam<T>(string parameterName, T value)
		{
			SqlCriteria ret = null;
			
			if (this._next == null)
			{
				this._paramName = DbUtil.GetParameterName(parameterName);
				this._value = value;
				this._next = new SqlCriteria();

				ret = this._next;
			}
			else
			{

				SqlCriteria c = this._next;

				while (c.Next != null) {
					c = c.Next;
				}

				c.ParamName = DbUtil.GetParameterName(parameterName);
				c.Value = value;

				c.Next = new SqlCriteria();

				ret = c.Next;
			}

			return ret;

		}

		public Dictionary<string, object> GetParams()
		{
			Dictionary<string, object> dic = new Dictionary<string, object>();

			SqlCriteria c = this;

			while (c != null && c.Next != null) {
				dic.Add(c.ParamName, c.Value);

				c = c.Next;
			}

			return dic;
		}
	}


	public class WhereCriteria
	{
		private SqlStatement _statement;

		internal SqlStatement Statement {
			get { return this._statement; }
			set { this._statement = value; }
		}

		public SqlCriteria Where(string whereStatement)
		{

			this._statement.Where = whereStatement;
			this._statement.Criteria.Where = whereStatement;

			return this._statement.Criteria;

		}

		public SqlCriteria Where(SqlCriteria criteria)
		{
			this._statement.Criteria = criteria;

			return this._statement.Criteria;
		}
	}
}
