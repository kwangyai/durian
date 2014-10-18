using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace SmartLib.Database
{
	public interface IDb
	{
		String ConnectionString{ get; }
		
		String GetConnectionString( String configName );
		
		DbConnection GetConnection();
		DbConnection GetConnection( String configName );
		
		IDbCommand CreateCommand();
		IDbCommand CreateCommand(String configName);
		
		void CloseConnection();
		void CloseConnection(String configName);
		void CloseAllConnections();
			
		
	}
}
