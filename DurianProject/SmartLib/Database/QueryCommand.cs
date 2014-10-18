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

//UPDATE NEW VERSION
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SmartLib.Database.Config;


namespace SmartLib.Database
{

	public class QueryCommand
	{
		private IDbCommand _cmd;
		private string _cmdText;
		private String _configName;
		private int _lastResult;
		private Exception _lastException;


	#region "PROPERTIES"
		//EDIT FOR NEW VERSION
		public string CommandText {
				//Return Me._cmd.CommandText 
			get { return this._cmdText;  }
				//Me._cmd.CommandText = value 
			set { this._cmdText = value;}
		}
		
		public String ConfigName {
			get{ return this._configName; }
			internal set{ this._configName = value; }
		}

		public CommandType CommandType {
			get { return this._cmd.CommandType; }
			set { this._cmd.CommandType = value; }
		}
		
		public Int32 LastResult {
			get{ return this._lastResult; }
			internal set{ this._lastResult = value; }
		}
		
		public Exception LastException {
			get{ return this._lastException; }
			internal set{ this._lastException = value; }
		}

		/*public IDataParameterCollection Parameters {
			get { return this._cmd.Parameters; }
		}*/
	#endregion //ENDREGION PROPERTIES


	#region "CONSTRUCTORS"
		public QueryCommand() {
			this.ToIDbCommand();
            this._configName = DbConfigManager.DefaultConfigurationName;
		}
		
		
		public QueryCommand(String configName)  {
			this._configName = configName;
			this.ToIDbCommand();
		}

		public QueryCommand(IDbConnection conn) {
			if (conn != null) {
				this.CommandType = CommandType.Text;
				this._cmd = conn.CreateCommand();
			}
			else 
				throw new Exception("Connection Object must not be null.");
		}
	#endregion //ENDREGION CONSTRUCTORS


		public void SetParam(string paramName, object data) {

			bool isSqlOperation = false;
			if (data != null)
				isSqlOperation = object.ReferenceEquals(data.GetType(), typeof(SqlOperation));

			if (!isSqlOperation)
			{
				String pName =  DbUtil.GetParameterName(paramName);
				IDataParameter param = null;
				if( this._cmd.Parameters.Contains(pName) )
					param = this._cmd.Parameters[pName] as IDataParameter;
				else {
					param = this._cmd.CreateParameter();
					param.ParameterName = pName;
					this._cmd.Parameters.Add(param);
				}
								
				if (data == null)
					param.Value = DBNull.Value;
				else
					param.Value = data;
			}
		}


		public void SetParam(string paramName, ParameterDirection paramDirection) {

			String pName = DbUtil.GetParameterName(paramName);
			IDataParameter param = null;
			if (this._cmd.Parameters.Contains(pName))
				param = this._cmd.Parameters[pName] as IDataParameter;
			else {
				param = this._cmd.CreateParameter();
				param.ParameterName = pName;
				this._cmd.Parameters.Add(param);
			}
			
			param.Direction = paramDirection;			
		}



		public void SetParam(String paramName, object data, ParameterDirection paramDirection)
		{

			bool isSqlOperation = false;
			if (data != null)
				isSqlOperation = object.ReferenceEquals(data.GetType(), typeof(SqlOperation));

			if (!isSqlOperation)
			{
				String pName = DbUtil.GetParameterName(paramName);
				IDataParameter param = null;
				if (this._cmd.Parameters.Contains(pName))
					param = this._cmd.Parameters[pName] as IDataParameter;
				else {
					param = this._cmd.CreateParameter();
					param.ParameterName = pName;
					this._cmd.Parameters.Add(param);
				}
				
				param.Direction = paramDirection;

				if (data == null)
					param.Value = DBNull.Value;
				else
					param.Value = data;

			}
		}


		public void SetParam(string paramName, object data, DbType dbType) {

			bool isSqlOperation = false;
			if (data != null)
				isSqlOperation = object.ReferenceEquals(data.GetType(), typeof(SqlOperation));

			if (!isSqlOperation)
			{
				String pName = DbUtil.GetParameterName(paramName);
				IDataParameter param = null;
				if (this._cmd.Parameters.Contains(pName))
					param = this._cmd.Parameters[pName] as IDataParameter;
				else
				{
					param = this._cmd.CreateParameter();
					param.ParameterName = pName;
					this._cmd.Parameters.Add(param);
				} 
				param.DbType = dbType;
				
				if (data == null) 
					param.Value = DBNull.Value;
				else
					param.Value = data;

								
			}

		}


		public void SetParam(string paramName, object data, DbType dbType, ParameterDirection paramDirection) {

			bool isSqlOperation = false;
			if (data != null)
				isSqlOperation = object.ReferenceEquals(data.GetType(), typeof(SqlOperation));

			if (!isSqlOperation)
			{
				String pName = DbUtil.GetParameterName(paramName);
				IDataParameter param = null;
				if (this._cmd.Parameters.Contains(pName))
					param = this._cmd.Parameters[pName] as IDataParameter;
				else
				{
					param = this._cmd.CreateParameter();
					param.ParameterName = pName;
					this._cmd.Parameters.Add(param);
				} 
				
				param.DbType = dbType;
				param.Direction = paramDirection;

				if (data == null)
					param.Value = DBNull.Value;
				else
					param.Value = data;
			}

		}


		public void SetParam(string paramName, int size, ParameterDirection paramDirection)
		{
				String pName = DbUtil.GetParameterName(paramName);
				IDbDataParameter param = null;
				if (this._cmd.Parameters.Contains(pName))
					param = this._cmd.Parameters[pName] as IDbDataParameter;
				else
				{
					param = this._cmd.CreateParameter();
					param.ParameterName = pName;
					this._cmd.Parameters.Add(param);
				}

				param.Direction = paramDirection;
				param.Size = size;
		}


		public void SetParam(string paramName, object data, DbType dbType, int size, byte precision,  ParameterDirection paramDirection)
		{

			bool isSqlOperation = false;
			if (data != null)
				isSqlOperation = object.ReferenceEquals(data.GetType(), typeof(SqlOperation));

			if (!isSqlOperation)
			{

				String pName = DbUtil.GetParameterName(paramName);
				IDbDataParameter param = null;
				if (this._cmd.Parameters.Contains(pName))
					param = this._cmd.Parameters[pName] as IDbDataParameter;
				else
				{
					param = this._cmd.CreateParameter();
					param.ParameterName = pName;
					this._cmd.Parameters.Add(param);
				}

				param.DbType = dbType;
				param.Direction = paramDirection;
				param.Size = size;
				param.Precision = precision;
				
				if (data == null)
					param.Value = DBNull.Value;
				else
					param.Value = data;
			}

		}

		
		public IDbCommand ToIDbCommand() {
		
			if( this._cmd == null ) {
				if( String.IsNullOrEmpty( this._configName ) )
					this._cmd = Db.CreateCommand();
				else
					this._cmd = Db.CreateCommand( this._configName );
			}

            this.CommandType = this._cmd.CommandType;
			return this._cmd;
		}
		
		
		public Object GetParamValue( String paramName ) {
		
			Object ret = null;
			String pName = DbUtil.GetParameterName(paramName);

			if (this._cmd.Parameters.Contains(pName))
			{ 
				IDataParameter param = this._cmd.Parameters[paramName] as IDataParameter;			
				ret = param.Value;
				
			}else 
				throw new Exception("This QueryCommand object have no parameter named " + paramName);
				
			return ret;
		}


		public void Prepare() {

            if( this._cmd != null && this._cmd.Connection != null )
			    this._cmd.Prepare();
		}


		public void ClearParameter() {

            if( this._cmd != null )
			    this._cmd.Parameters.Clear();
		}


		//public DataTable ExecuteQuery() {
		//        return DataService.GetDataTable(this);
		//}

		//public int ExecuteNonQuery() {
		//        return DataService.ExecuteNonQuery(this);
		//}


		//public object ExecuteScalar() {
		//        return DataService.ExecuteScalar(this);
		//}

	}

}
