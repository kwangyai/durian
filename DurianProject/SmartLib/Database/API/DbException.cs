using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLib.Database
{
	public class DbException : Exception
	{
		private QueryCommand _queryCmd;
		private int _commandErrorIndex;
		
		internal DbException() : base()
		{
			this._commandErrorIndex = -1;
			this._queryCmd = null;
		}
		
		internal DbException( String message, Exception innerExc, QueryCommand queryCmd, int errIndex ) : base(message, innerExc)
		{
			this._commandErrorIndex = -1;
			this._queryCmd = null;
			
			this._queryCmd = queryCmd;
			this._commandErrorIndex = errIndex;
		}
		
		public QueryCommand QueryCommand
		{
			get{ return this._queryCmd; }
		}
		
		public int ErrorIndex
		{
			get{ return this._commandErrorIndex; }
		}
		
		
		
	}
}
