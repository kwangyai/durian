using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartLib.Database;
using System.Data;

namespace DurianWeb.DataAccess
{
    public class Authentication
    {

        public DataTable LogOn(string Username, string Password)
        {
            string sqlStr = string.Format("SELECT * FROM MemberUser WHERE Username = '{0}' AND Password = '{1}'", Username, Password);

            QueryCommand objCmd = new QueryCommand();
            objCmd.CommandText = sqlStr.ToString();

            DataTable dtUser = DataService.ExecuteQuery(objCmd);

            return dtUser;
        }
    }
}
