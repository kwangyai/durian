using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLib.Web.Permission
{
	public interface IMembership
	{
		String GetUserID( String loginName, String password );
		Dictionary<String, Boolean> GetPermissionsByUserID( String userID );
		void RefreshUser(String userID);
		IList<String> GetRolesByUserID( String userID );
		IList<String> PermissionList{ get; }
		IList<String> RoleList{ get; }
		
	}
}
