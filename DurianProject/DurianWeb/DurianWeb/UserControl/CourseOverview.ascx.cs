using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL = DurianWeb.DataAccess;
using System.Data;

namespace DurianWeb.UserControl
{
    public partial class CourseOverview : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetCourseOverview(string CourseId) {
            try
            {
                DAL.Course _course = new DAL.Course();
                DataTable dtCourseData = _course.CourseData(CourseId);

                lblCourseName.Text = dtCourseData.Rows[0]["CourseName"].ToString();
                Page.Title = lblCourseName.Text;
                //Page.Title = lblCourseName.Text;

            }
            catch { }

        }
    }
}