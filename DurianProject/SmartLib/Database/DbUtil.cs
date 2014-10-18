using System;
using SmartLib.Database.Config;

namespace SmartLib.Database
{
	public class DbUtil
	{

		public const String PARAMETER_PREFIX = "@";

		public static bool IsNotNull(object obj)
		{

			bool ret = true;

			if (object.ReferenceEquals(obj, DBNull.Value))
			{
				ret = false;
			}

			return ret;

		}

		public static string GetParameterName(string paramName)
		{

			string param = paramName.Trim();


			//if (!string.IsNullOrEmpty(param))
			//{

			//        if ((param.StartsWith("?") | param.StartsWith("@") | param.StartsWith(":")))
			//        {
			//                DbConfigInfo dbEasyCfg = DbEasyConfigManager.SelectedConfig;
			//                param = param.Substring(1);
			//                param = string.Concat(dbEasyCfg.DbParamPrefix, param);

			//        }
			//}

			return param;

		}

		/*public static DateTime GetDateTimeFromString(string dtText, string dtFormat)
		{

			//dd/mm/yyyy
			//mm/dd/yyyy
			DateTime dt = DateTime.MinValue;
			string spliter = null;

			if ((dtFormat.IndexOf("/") > -1))
			{
				spliter = "/";
			}
else if ((dtFormat.IndexOf("-") > -1)) {
				spliter = "-";
			}

			if ((spliter != null))
			{
				string[] dtNumbers = dtText.Split(spliter);
				string[] dtFormats = dtFormat.Split(spliter);

				if ((dtNumbers.Length > 2))
				{
					dt = new DateTime(Convert.ToInt32(dtNumbers(2)), Convert.ToInt32(dtNumbers(1)), Convert.ToInt32(dtNumbers(0)));
				}

			}

			return dt;

		}*/


	}
}

