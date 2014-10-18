using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL = DurianWeb.DataAccess;
using System.Data;

namespace DurianWeb
{
    public partial class Course : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                string CourseId = Request.QueryString["CourseId"].ToString();
                DAL.Course _course = new DAL.Course();
                DataTable dtCourseData = _course.CourseData(CourseId);

                lblCourseName.Text = dtCourseData.Rows[0]["CourseName"].ToString();
                Page.Title = lblCourseName.Text;
                //Page.Title = lblCourseName.Text;
                CourseOverview1.SetCourseOverview(CourseId);
            }
            catch { }

        }
    }
}