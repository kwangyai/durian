using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace SmartLib.Database.Config
{
	public class DbEasyConfigManager
	{
		private static Dictionary<string, DbConfigInfo> configs;
		public static DbConfigInfo SelectedConfig;
		public static string SelectedName = "";

		static DbEasyConfigManager()
		{
			GetConfigs();
		}

		public static Dictionary<string, DbConfigInfo> GetConfigs()
		{
			if (configs == null)
			{
				object obj = ConfigurationManager.GetSection("dbEasy");
				configs = obj as Dictionary<string, DbConfigInfo>;
			}

			return configs;
		}


		public static DbConfigInfo GetSelectedConfig()
		{
			return SelectedConfig;
		}
		

	}

}
