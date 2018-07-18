using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace LoginPage
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            if (Session["New"] != null)
            {

            }
            else
            {
                Response.Redirect("Login.aspx");

            }
        }

        protected void ButtonChange_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
            con.Open();
            string checkPasswordQuery = "select Password from Username where Email='" + Session["New"] + "'";
            SqlCommand passComm = new SqlCommand(checkPasswordQuery, con);
            string password = passComm.ExecuteScalar().ToString().Replace(" ", "");
            if (password == TextBoxOldPass.Text)
            {
                SqlCommand com = new SqlCommand("UPDATE Username SET Password = @Password WHERE Email ='" + (string)Session["New"] + "'", con);
               

                com.Parameters.AddWithValue("@Password", TextBoxNewPass.Text);

                com.ExecuteNonQuery();
                string queryLog = "UPDATE Login SET Password=@Password WHERE Email = '" + (string)Session["New"] + "'";
                SqlCommand sqlLog = new SqlCommand(queryLog, con);

                sqlLog.Parameters.AddWithValue("@Password", TextBoxNewPass.Text);
                sqlLog.ExecuteNonQuery();
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password Changed');window.location='User.aspx';", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password is incorrect');window.location='User.aspx';", true);
            }
            
            

        }
    }
}