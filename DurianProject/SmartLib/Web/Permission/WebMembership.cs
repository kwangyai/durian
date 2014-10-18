using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SmartLib.Database;

namespace SmartLib.Web.Permission
{
	public class WebMembership : IMembership
	{
	
		private IList<String> _permissionList;
		private IList<String> _roleList;
		
		public WebMembership()
		{
			this._permissionList = new List<String>();
			this._roleList = new List<String>();
		}
	
		public String GetUserID(String loginName, String password)
		{
		
			QueryCommand cmd = new QueryCommand();
			cmd.CommandText = "SELECT ISNULL(USER_ID,'-1') FROM [USER_ACCOUNT] WHERE LOGIN_NAME=@LN AND LOGIN_PASSWORD=@PWD";
			cmd.SetParam( "@LN", loginName );
			cmd.SetParam( "@PWD", password );
			
			String userID = Convert.ToString(DataService.ExecuteScalar( cmd));
			
			if(  userID.Equals("-1") )
				return null;	
						
			return userID;
		}

		public Dictionary<String, Boolean> GetPermissionsByUserID(String userID)
		{
			QueryCommand cmd = new QueryCommand();
			cmd.CommandText = "SELECT PERMISSION_NAME, PERMISSION_VALUE FROM [USER_PERMISSTION] WHERE USER_ID=@ID";
			cmd.SetParam("@ID", userID);
			
			DataTable dt = DataService.ExecuteQuery( cmd);
			Dictionary<String, bool> pers = new Dictionary<string,bool>();
			
			foreach( DataRow dr in dt.Rows )
			{
				if( DataService.IsNotNull( dr["PERMISSION_NAME"] ) && DataService.IsNotNull( dr["PERMISSION_VALUE"] ))
				{
					pers.Add(Convert.ToString(dr["PERMISSION_NAME"]), Convert.ToBoolean(dr["PERMISSION_VALUE"]));
				}
			}

			return pers;
		}

		public IList<String> GetRolesByUserID(String userID)
		{
			QueryCommand cmd = new QueryCommand();
			cmd.CommandText = "SELECT ROLE_ID, ROLE_NAME FROM [USER_ROLES] WHERE USER_ID=@ID";
			cmd.SetParam("ID", userID);

			DataTable dt = DataService.ExecuteQuery(cmd);
			List<String> roles = new List<string>();

			foreach (DataRow dr in dt.Rows)
			{
				if (DataService.IsNotNull(dr["ROLE_NAME"]))
				{
					roles.Add(Convert.ToString(dr["ROLE_NAME"]));
				}
			}

			return roles;
		}
		
		public void RefreshUser(String userID)
		{
		}
		
		public IList<String> PermissionList
		{ 
			get{ return this._permissionList; } 
		}
		
		public IList<String> RoleList
		{ 
			get{ return this._roleList; } 
		}
		
	}
}
