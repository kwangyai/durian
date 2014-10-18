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
using System.Data.Common;

namespace SmartLib.Database
{
	public class Db
	{
		private static IDb _db;
		
		internal const String DB_LOGGER_NAME = "DatabaseLog";
		
		static Db() {
		
			if( _db == null ) {
				if( System.Web.HttpContext.Current != null )
					_db = new SmartLib.Database.Web.WebDb();
				else
					_db = new SmartLib.Database.Win.WinDb();
			}
		}

		public static string ConnectionString {
			get {
				return _db.ConnectionString;
			}
		}

		public static DbConnection GetConnection() {
			return _db.GetConnection();
		}
		
		public static DbConnection GetConnection( String configName ) {
			return _db.GetConnection(configName);
		}

		public static IDbCommand CreateCommand() {
			return _db.CreateCommand();
		}
		
		public static IDbCommand CreateCommand( String configName ) {
			return _db.CreateCommand(configName);
		}

		public static void CloseConnection() {
			 _db.CloseConnection();
		}
		
		public static void CloseConnection( String configName ) {
			_db.CloseConnection( configName );
		}
		
		public static void CloseAllConnections( ) {
			_db.CloseAllConnections();
		}
		
		
	}


}
