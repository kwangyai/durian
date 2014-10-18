using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Web.Security;
using System.Web;

namespace SmartLib.Web.Permission
{
	public class WebPermission
	{
		private static IMembership _member;
		
		static WebPermission() {
			_member = SiteSetting.MembershipClass;
		}
		
		public static void SignOut()
		{
			FormsAuthentication.SignOut();
		}
		
		public static bool SignIn( String loginName, String password, bool loginMemorial, int cookieAgeHrs, ref PermissionResultCode result )
		{
			return SignIn( loginName, password, loginMemorial, cookieAgeHrs, ref result );
		}
				
		public static bool SignIn( String loginName, String password, bool loginMemorial, int cookieAgeHrs, bool authCodeOK, ref PermissionResultCode result ) 
		{	
			bool loginOK = false;
			result = PermissionResultCode.UNKNOWN_RESULT;
			
			if( ! authCodeOK ) {
				result = PermissionResultCode.SIGN_IN_WRONG_AUTH_CODE_RESULT;
				return false;
			}
			
			if( ! IsAuthenticatedUser() ) {
				String userID = _member.GetUserID( loginName, password );
				loginOK = (!String.IsNullOrEmpty(userID));
				
				if( loginOK ) {
					IList<String> roles = _member.GetRolesByUserID(userID);
					String roleString = String.Join( ";", (roles as List<String>).ToArray() );
					
					try {
						CreateTicket(userID, roleString, loginMemorial, cookieAgeHrs);
						result = PermissionResultCode.OK_RESULT;
					}
					catch( Exception ex )
					{
						loginOK = false;
						result = PermissionResultCode.CANNOT_CREATE_AUTH_TICKET_RESULT;
					}
									
				}
				else
					result = PermissionResultCode.SIGN_IN_WRONG_CREDENTIAL_RESULT;
			}
			else
				result = PermissionResultCode.USER_IS_AUTHENTICATED_RESULT;
			
			return loginOK;			
		}

		private static void CreateTicket(String userID, String userRole, bool loginMemorial, int cookieAgeHrs)
		{
			try
			{
				FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
				1,				// เวอร์ชั่นของ FormsAuthenticationTicket
				userID,				// username
				DateTime.Now,			// เวลาที่ Login
				DateTime.Now.AddHours(cookieAgeHrs),
				loginMemorial,			// ต้องการให้ระบบจดจำ User หรือไม่
				userRole);			// กำหนด User Role

				// Serialize อ็อบเจ็ก ticket ให้เป็น String 
				String secureTicket = FormsAuthentication.Encrypt(ticket);

				//เก็บข้อมูลของ secureTicket  ไว้ใน Cookie  โดยตั้งชื่อให้ Cookie นี้เป็น  FormsAuthentication.FormsCookieName
				HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, secureTicket);

				//ถ้าให้ระบบจดจำ User  ก็จะต้องกำหนดอายุของ Cookie ให้เท่ากับอายุของ ticket 
				if (ticket.IsPersistent)
					cookie.Expires = ticket.Expiration;

				//เขียน Cookie ไว้ที่ Client
				HttpContext.Current.Response.Cookies.Add(cookie);
			}
			catch( Exception  ex)
			{
				throw ex;
			}
			
		}
		
		public static bool IsAuthenticatedUser() {
			return HttpContext.Current.User.Identity.IsAuthenticated;
		}
		
		public static String GetUserID() {
			String ret = null;

			if (HttpContext.Current.User.Identity.IsAuthenticated)
				ret = HttpContext.Current.User.Identity.Name;

			return ret;
		}
		
		public static bool HasPermission( String permissionName ) {
		
			bool hasPermission = false;
			String userID = GetUserID();
			
			if( ! String.IsNullOrEmpty( userID ) ) {
				IDictionary<String, bool> userPer = _member.GetPermissionsByUserID(GetUserID());
				
				if( userPer.ContainsKey( permissionName.ToUpper() ) ) {
					hasPermission = userPer[permissionName.ToUpper()];
				}
			}

			return hasPermission;
		}
		
		public static bool IsInRole( String roleName ) {
		
			if( _member.GetRolesByUserID( GetUserID() ).Contains( roleName.ToUpper() ) ) {
				return true;
			}
			
			return false;
		
		}
		
		public static List<String> ListOwnerRoles()
		{
			return _member.GetRolesByUserID(GetUserID()) as List<String>;
		}
		
		public static List<String> ListAllRoles()
		{
			return _member.RoleList as List<String>;
		}
		
		public static List<String> ListAllPermissions()
		{
			return _member.PermissionList as List<String>;
		}
		
		public static void SetAuthenticateRequest()
		{
			if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
			{
				//ดึง Authentication Cookie  ออกมา
				HttpCookie ticketCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

				if (ticketCookie != null)
				{
					try
					{
						//แปลงข้อมูลที่เก็บใน cookie  ให้เป็นอ็อบเจ็ก  FormsAuthemticationTicket
						FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(ticketCookie.Value);

						//สร้าง Identity แบบ FormsIdentity  ขึ้นมา
						FormsIdentity identity = new FormsIdentity(ticket);

						// แปลง Role ของ User ให้เป็น Array 
						String[] roles = ticket.UserData.Split(';');

						//สร้างอ็อบเจ็ก GenericPrincipal แล้วไปกำหนดให้กับพร็อบเพอร์ตี้ User ของคลาส HttpContext
						HttpContext.Current.User = new GenericPrincipal(identity, roles);


					}
					catch (Exception ex)
					{
						throw ex;
					}
				}
			}
		}
		
	}
}
