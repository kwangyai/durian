using System;
using System.Collections.Generic;
using System.Text;
using SmartLib.Web.Permission;

namespace SmartLib.Web
{
	public class SiteSetting
	{
		private static IMembership _memberShipClass;
		
		public static IMembership MembershipClass
		{
			get{ return _memberShipClass; }
			set{ _memberShipClass = value; }
		}
		
	}
}
