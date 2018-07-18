using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

namespace LoginPage
{
    public partial class user : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            if (Session["New"]!= null)
            {
               
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);

                DataTable dt = new DataTable();
                con1.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select * from Username where Email='" + Session["new"] + "'", con1);

                myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    TextBoxUser.Text = (myReader["Username"].ToString());
                    
                    TextBoxEmail.Text = (myReader["Email"].ToString());
                    TextBox1.Text = (myReader["Blood"].ToString());
                    TextBoxSalary.Text = (myReader["Salary"].ToString());
                    TextBoxAddress.Text = (myReader["Address"].ToString());
                    TextBox2.Text = (myReader["Section"].ToString());
                    TextBoxBirth.Text = (myReader["Date_of_birth"].ToString());
                    TextBox3.Text = (myReader["Sex"].ToString());
                    TextBoxPhone.Text = (myReader["Phone"].ToString());
                }
                con1.Close();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
                SqlDataAdapter sda = new SqlDataAdapter("Select * from Username where Email = '" + Session["New"] + "' ", con);
                DataTable dt2 = new DataTable();
                sda.Fill(dt2);
                Label2.Text = TextBoxUser.Text;
                if (dt2.Rows[0]["User_Image"].ToString().Length > 1)
                {
                    Image1.ImageUrl = dt2.Rows[0]["User_Image"].ToString();
                    
                }

                else
                {
                    Image1.ImageUrl = "Images/User.png";


                }

            }
                
        }

        protected void ButtonLogout_Click(object sender, EventArgs e)
        {
            Session["New"] = null ;
            Response.Redirect("Login.aspx");
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            
           
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
                SqlCommand com = new SqlCommand("UPDATE Username SET Username = @Uname,Address = @Address,Salary = @Salary, Phone = @Phone WHERE Email ='" + (string)Session["New"] + "'", con);
                con.Open();

                com.Parameters.AddWithValue("@Uname", TextBoxUser.Text);
                com.Parameters.AddWithValue("@Phone", TextBoxPhone.Text);
                com.Parameters.AddWithValue("@Address", TextBoxAddress.Text);

                com.Parameters.AddWithValue("@Salary", TextBoxSalary.Text);

                com.ExecuteNonQuery();
                string queryLog = "UPDATE Login SET Loginname=@Loginname WHERE Email = '" + (string)Session["New"] + "'";
                SqlCommand sqlLog = new SqlCommand(queryLog, con);

                sqlLog.Parameters.AddWithValue("@Loginname", TextBoxUser.Text);
                sqlLog.ExecuteNonQuery();
                con.Close();
            Response.Write("<script LANGUAGE='Javascript' > alert('Changes saved successfully')</script>");


        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
            string path = Server.MapPath("Images/");
            if (FileUpload1.HasFile)
            {
                string ext = Path.GetExtension(FileUpload1.FileName);
                if (ext == ".jpg" || ext == ".png")
                {
                    FileUpload1.SaveAs(path + FileUpload1.FileName);
                    string name = "Images/" + FileUpload1.FileName;
                    string ss = "UPDATE Username SET User_Image=@Image where Email = '" + Session["New"] + "'";
                    SqlCommand cmd = new SqlCommand(ss, con);
                    cmd.Parameters.AddWithValue("@Image", name);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Label1.Text = " Image uploaded successfully";
                    Label1.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    Label1.Text = "You have to upload jpg or png file";
                }
            }
            else
            {
                Label1.Text = "Please select file";
            }
        }
    }
}