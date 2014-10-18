using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DurianWeb.DataAccess;
using System.Data;

namespace DurianWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btLogin_Click(object sender, EventArgs e)
        {
            Authentication Login = new Authentication();
            DataTable dtUser = Login.LogOn(TxUsername.Text, TxPassword.Text);

            if (dtUser.Rows.Count > 0)
            {
                Session["UserLogin"] = dtUser;
                Response.Redirect("~/Course.aspx?CourseId=1");
            }
            else
            {
                LbErr.Text = "Username หรือ Password ไม่ถูกต้อง";
            }
        }

        
    }
}