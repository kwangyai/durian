using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLib.Web.Permission
{
	public enum PermissionResultCode
	{
		UNKNOWN_RESULT = 0,
		OK_RESULT = 1,
		SIGN_IN_WRONG_AUTH_CODE_RESULT = 2,
		SIGN_IN_WRONG_CREDENTIAL_RESULT = 3,
		USER_IS_AUTHENTICATED_RESULT = 4,
		CANNOT_CREATE_AUTH_TICKET_RESULT = 5,
	}
}
