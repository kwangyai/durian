using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLib.Web.Permission
{
	public interface IWebPermissionTrigger
	{
		void AfterSignInFalse(String loginName, String password, PermissionResultCode result);
		void AfterSignInSuccess( String userID );
		void AfterSignOut( String userID, PermissionResultCode result );
	}
}
