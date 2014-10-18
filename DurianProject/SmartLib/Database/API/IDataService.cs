using System;
using System.Collections.Generic;
using System.Data;

namespace SmartLib.Database
{
	interface IDataService {
		DataTable ExecuteQuery(QueryCommand cmdQuery);
		DataTable ExecuteQuery(QueryCommand cmdQuery, int rowIndex, int fetchSize, string orderBy);
		DataTable GetSchemaTable();
		DataTable GetSchemaTable(String configName);
		int ExecuteNonQuery(IList<QueryCommand> queryCmds);
		int ExecuteNonQuery( IList<SqlEasy> sqlEasies );
		int ExecuteNonQuery( params SqlEasy[] sqlEasies );
		int ExecuteNonQuery(params QueryCommand[] queryCmds);
		int ExecuteNonQuery( QueryCommand queryCmd, AutoTransaction autoTranx );
		object ExecuteScalar(QueryCommand queryCmd);
	
	}
}
