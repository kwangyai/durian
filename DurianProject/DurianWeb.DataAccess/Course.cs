using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartLib.Database;
using System.Data;

namespace DurianWeb.DataAccess
{
    public class Course
    {
        public DataTable CourseData(string CourseId)
        {
            string sqlStr = string.Format("SELECT * FROM Course WHERE CourseId = '{0}'", CourseId);

            QueryCommand objCmd = new QueryCommand();
            objCmd.CommandText = sqlStr.ToString();

            DataTable dtCourseData = DataService.ExecuteQuery(objCmd);

            return dtCourseData;
        }
    }
}
