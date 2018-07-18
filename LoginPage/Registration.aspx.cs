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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            /*if (IsPostBack)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
                conn.Open();
                string checkuser = "select count(*) from Username where Username='" + TextBoxUser.Text + "'";
                SqlCommand com = new SqlCommand(checkuser, conn);
                int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
                conn.Close();
                if (temp >= 1)
                { 
                    Response.Write("User Already Exists!");

                }
                
            }*/
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TextBoxEmail_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                //Guid newGUID = Guid.NewGuid();

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
                conn.Open();
                string checkuser = "select count(*) from Username where Email='" + TextBoxEmail.Text + "'";
                SqlCommand come = new SqlCommand(checkuser, conn);
                int temp = Convert.ToInt32(come.ExecuteScalar().ToString());

                if (temp == 0)
                {


                    
                    string insertQuery = "insert into Username (Username,Email,Address,Blood,Salary,Date_of_birth,Date_of_joining,Password,Section,Sex,Phone) values (@Uname,@Email,@Address,@Blood,@Salary,@Date_of_birth,@Date_of_joining,@Password,@Section,@Sex,@Phone)";
                    SqlCommand com = new SqlCommand(insertQuery, conn);
                   //com.Parameters.AddWithValue("@ID", newGUID.ToString());
                    com.Parameters.AddWithValue("@Uname", TextBoxUser.Text);
                    com.Parameters.AddWithValue("@Email", TextBoxEmail.Text);
                    com.Parameters.AddWithValue("@Address", TextBoxAddress.Text);
                    com.Parameters.AddWithValue("@Blood", DropdownBlood.SelectedItem.ToString());
                    com.Parameters.AddWithValue("@Salary", TextBoxSalary.Text);
                    com.Parameters.AddWithValue("@Date_of_birth", TextBoxBirth.Text);
                    com.Parameters.AddWithValue("@Date_of_joining", TextBoxJoining.Text);
                    com.Parameters.AddWithValue("@Password", TextBoxPass.Text);
                    com.Parameters.AddWithValue("@Section", DropDownSection.SelectedItem.ToString());
                    com.Parameters.AddWithValue("@Sex", DropDownListSex.SelectedItem.ToString());
                    com.Parameters.AddWithValue("@Phone", TextBoxPhone.Text);
                   
                    string Deptcount = "update Department Set DeptEmp = DeptEmp + 1 where DeptName ='" + DropDownSection.SelectedItem.ToString() + "'";
                    SqlCommand Deptcom = new SqlCommand(Deptcount, conn);
                    Deptcom.ExecuteNonQuery();
                    com.ExecuteNonQuery();
                    string IDQuery = "select ID from Username where Email='" + TextBoxEmail.Text + "'";
                    SqlCommand comID = new SqlCommand(IDQuery, conn);
                    int tem = Convert.ToInt32(comID.ExecuteScalar().ToString());
                    string insertlogQuery = "insert into Login (ID,Loginname,Email,Password) values (@ID,@Uname,@Email,@Password)";
                    SqlCommand Logincom = new SqlCommand(insertlogQuery, conn);
                    Logincom.Parameters.AddWithValue("@ID", tem);
                    Logincom.Parameters.AddWithValue("@Uname", TextBoxUser.Text);
                    Logincom.Parameters.AddWithValue("@Email", TextBoxEmail.Text);
                    Logincom.Parameters.AddWithValue("@Password", TextBoxPass.Text);
                    
                    Logincom.ExecuteNonQuery();
                    
                    Response.Redirect("User.aspx");
                   // Response.Write("Registration is successful");
                    conn.Close();
                }
                else
                {
                    Response.Write("<script LANGUAGE='Javascript' > alert('User already exists')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.ToString());
            } 
        }
    }
}