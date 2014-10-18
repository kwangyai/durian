using System;
using System.Collections.Generic;
using System.Text;

namespace SmartLib.Database.Config
{
	public class CustomDbConfigExample : CustomConfigLoader
	{
		
		public override void Load() {
		
			this.AddConfig("OnePos", "Persist Security Info=False;User ID=oneuser;Password=1608;Initial Catalog=OnePos;Data Source=(local)", typeof(System.Data.SqlClient.SqlConnection));
			this.AddConfig("Enabler", "Persist Security Info=False;User ID=oneuser;Password=1608#;Initial Catalog=EnablerDb;Data Source=(local)", typeof(System.Data.SqlClient.SqlConnection));
		}
	}
}
