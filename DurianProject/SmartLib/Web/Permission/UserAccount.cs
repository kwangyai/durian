using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLib.Web.Permission
{
	class UserAccount
	{
		private String _ID;
		private String _LoginName;
		
		public String ID{
			get{ return this._ID; }
			set{ this._ID = value; }
		}
		
		public String LoginName {
			get{ return this._LoginName; }
			set{ this._LoginName = value; }
		}
	}
}
