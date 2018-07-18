using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace LoginPage
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
            conn.Open();
            string checkuser = "select count(*) from Username where Email='" + TextBoxLoginEmail.Text + "'";
            SqlCommand com = new SqlCommand(checkuser, conn);
            int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
            conn.Close();
            if (temp == 1)
            {
                conn.Open();
                string checkPasswordQuery = "select password from Username where Email='" + TextBoxLoginEmail.Text + "'";
                SqlCommand passComm = new SqlCommand(checkPasswordQuery, conn);
                string password = passComm.ExecuteScalar().ToString().Replace(" ","");
                if(password == TextBoxLoginPass.Text)
                {
                    Session["New"] = TextBoxLoginEmail.Text;
                    Response.Write("Password is correct");
                    Response.Redirect("User.aspx");
                }
                else
                {
                    Response.Write("<script LANGUAGE='Javascript' > alert('Password is incorrect')</script>");
                }
            }
            else
            {
                Response.Write("<script LANGUAGE='Javascript' > alert('Email ID is invalid')</script>");
            }
        }

    }
}