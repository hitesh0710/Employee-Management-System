using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


namespace LoginPage
{
    public partial class Manager : System.Web.UI.Page
    {

        string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Registration.mdf;Integrated Security = True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateGridview();
            }
            if (Session["New"] != null)
            {
                if((string)Session["New"]=="admin_net@gmail.com")
                {
                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Request Denied');window.location='User.aspx';", true);
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        void PopulateGridview()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Username", sqlCon);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                gvPhoneBook.DataSource = dtbl;
                gvPhoneBook.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                gvPhoneBook.DataSource = dtbl;
                gvPhoneBook.DataBind();
                gvPhoneBook.Rows[0].Cells.Clear();
                gvPhoneBook.Rows[0].Cells.Add(new TableCell());
                gvPhoneBook.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                gvPhoneBook.Rows[0].Cells[0].Text = "No Data Found ..!";
                gvPhoneBook.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }

        protected void gvPhoneBook_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        string checkuser = "select count(*) from Username where Email='" + (gvPhoneBook.FooterRow.FindControl("txtEmailFooter") as TextBox).Text.Trim() + "'";
                        SqlCommand come = new SqlCommand(checkuser, sqlCon);
                        int temp = Convert.ToInt32(come.ExecuteScalar().ToString());
                        if (temp == 0)
                        {


                            string query = "insert into Username (Username,Email,Address,Blood,Salary,Date_of_birth,Date_of_joining,Password,Section,Sex,Phone) values (@Uname,@Email,@Address,@Blood,@Salary,@Date_of_birth,@Date_of_joining,@Password,@Section,@Sex,@Phone)";
                            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                            sqlCmd.Parameters.AddWithValue("@Uname", (gvPhoneBook.FooterRow.FindControl("txtUsernameFooter") as TextBox).Text.Trim());
                            sqlCmd.Parameters.AddWithValue("@Email", (gvPhoneBook.FooterRow.FindControl("txtEmailFooter") as TextBox).Text.Trim());
                            sqlCmd.Parameters.AddWithValue("@Address", (gvPhoneBook.FooterRow.FindControl("txtAddressFooter") as TextBox).Text.Trim());
                            sqlCmd.Parameters.AddWithValue("@Blood", (gvPhoneBook.FooterRow.FindControl("txtBloodFooter") as TextBox).Text.Trim());
                            sqlCmd.Parameters.AddWithValue("@Salary", (gvPhoneBook.FooterRow.FindControl("txtSalaryFooter") as TextBox).Text.Trim());
                            sqlCmd.Parameters.AddWithValue("@Date_of_birth", (gvPhoneBook.FooterRow.FindControl("txtDate_of_birthFooter") as TextBox).Text.Trim());
                            sqlCmd.Parameters.AddWithValue("@Date_of_joining", (gvPhoneBook.FooterRow.FindControl("txtDate_of_joiningFooter") as TextBox).Text.Trim());
                            sqlCmd.Parameters.AddWithValue("@Password", (gvPhoneBook.FooterRow.FindControl("txtPasswordFooter") as TextBox).Text.Trim());
                            sqlCmd.Parameters.AddWithValue("@Section", (gvPhoneBook.FooterRow.FindControl("txtSectionFooter") as TextBox).Text.Trim());
                            sqlCmd.Parameters.AddWithValue("@Sex", (gvPhoneBook.FooterRow.FindControl("txtSexFooter") as TextBox).Text.Trim());
                            sqlCmd.Parameters.AddWithValue("@Phone", (gvPhoneBook.FooterRow.FindControl("txtPhoneFooter") as TextBox).Text.Trim());
                            sqlCmd.ExecuteNonQuery();
                            string Deptcount = "update Department Set DeptEmp = DeptEmp + 1 where DeptName ='" + (gvPhoneBook.FooterRow.FindControl("txtSectionFooter") as TextBox).Text.Trim() + "'";
                            SqlCommand Deptcom = new SqlCommand(Deptcount, sqlCon);
                            Deptcom.ExecuteNonQuery();
                            string IDQuery = "select ID from Username where Email='" + (gvPhoneBook.FooterRow.FindControl("txtEmailFooter") as TextBox).Text.Trim() + "'";
                            SqlCommand comID = new SqlCommand(IDQuery, sqlCon);
                            int tem = Convert.ToInt32(comID.ExecuteScalar().ToString());
                            string insertlogQuery = "insert into Login (ID,Loginname,Email,Password) values (@ID,@Uname,@Email,@Password)";
                            SqlCommand Logincom = new SqlCommand(insertlogQuery, sqlCon);
                            Logincom.Parameters.AddWithValue("@ID", tem);
                            Logincom.Parameters.AddWithValue("@Uname", (gvPhoneBook.FooterRow.FindControl("txtUsernameFooter") as TextBox).Text.Trim());
                            Logincom.Parameters.AddWithValue("@Email", (gvPhoneBook.FooterRow.FindControl("txtEmailFooter") as TextBox).Text.Trim());
                            Logincom.Parameters.AddWithValue("@Password", (gvPhoneBook.FooterRow.FindControl("txtPasswordFooter") as TextBox).Text.Trim());

                            Logincom.ExecuteNonQuery();
                            PopulateGridview();
                            lblSuccessMessage.Text = "New Record Added";
                            lblErrorMessage.Text = "";

                        }
                        else
                        {
                            Response.Write("<script LANGUAGE='Javascript' > alert('User already exists')</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void gvPhoneBook_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPhoneBook.EditIndex = e.NewEditIndex;
            PopulateGridview();
        }

        protected void gvPhoneBook_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPhoneBook.EditIndex = -1;
            PopulateGridview();
        }

        protected void gvPhoneBook_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
               

                using (SqlConnection sqlCon = new SqlConnection(connectionString))

                {
                    sqlCon.Open();
                    string checkuser = "select count(*) from Username where Email='" + (gvPhoneBook.Rows[e.RowIndex].FindControl("txtEmail") as TextBox).Text.Trim() + "'";
                    SqlCommand come = new SqlCommand(checkuser, sqlCon);
                    int temp = Convert.ToInt32(come.ExecuteScalar().ToString());
                    if (temp == 0)
                    {
                        string query = "UPDATE Username SET Username=@Uname,Email=@Email,Address=@Address,Blood=@Blood,Salary=@Salary,Date_of_birth=@Date_of_birth,Date_of_joining=@Date_of_joining,Password=@Password,Section=@Section,Phone=@Phone,Sex=@Sex WHERE ID = @ID";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@Uname", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtUsername") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Email", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtEmail") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Address", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtAddress") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Blood", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtBlood") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Salary", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtSalary") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Date_of_birth", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtDate_of_birth") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Date_of_joining", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtDate_of_joining") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Password", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtPassword") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Section", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtSection") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Sex", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtSex") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Phone", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtPhone") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@ID", Convert.ToInt32(gvPhoneBook.DataKeys[e.RowIndex].Value.ToString()));
                        sqlCmd.ExecuteNonQuery();
                        string queryLog = "UPDATE Login SET Loginname=@loginname,Email=@Email,Password=@Password WHERE ID = @ID";
                        SqlCommand sqlLog = new SqlCommand(queryLog, sqlCon);
                        sqlLog.Parameters.AddWithValue("@Loginname", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtUsername") as TextBox).Text.Trim());
                        sqlLog.Parameters.AddWithValue("@Email", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtEmail") as TextBox).Text.Trim());
                        sqlLog.Parameters.AddWithValue("@Password", (gvPhoneBook.Rows[e.RowIndex].FindControl("txtPassword") as TextBox).Text.Trim());
                        sqlLog.Parameters.AddWithValue("@ID", Convert.ToInt32(gvPhoneBook.DataKeys[e.RowIndex].Value.ToString()));
                        sqlLog.ExecuteNonQuery();
                        gvPhoneBook.EditIndex = -1;
                        PopulateGridview();
                        lblSuccessMessage.Text = "Selected Record Updated";
                        lblErrorMessage.Text = "";
                    }
                    else
                    {
                        Response.Write("<script LANGUAGE='Javascript' > alert('User already exists')</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void gvPhoneBook_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                   sqlCon.Open();
           
                    string query = "DELETE FROM Username WHERE ID = @ID";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@ID", Convert.ToInt32(gvPhoneBook.DataKeys[e.RowIndex].Value.ToString()));
                    string Dept = "Select Section from Username where ID=" + Convert.ToInt32(gvPhoneBook.DataKeys[e.RowIndex].Value.ToString()) + "";
                    SqlCommand comQuery = new SqlCommand(Dept, sqlCon);
                    string Deptquery = comQuery.ExecuteScalar().ToString();
                    string Deptcount = "update Department Set DeptEmp = DeptEmp - 1 where DeptName ='" +Deptquery+ "'";
                    SqlCommand Deptcom = new SqlCommand(Deptcount, sqlCon);
                    Deptcom.ExecuteNonQuery();
                    sqlCmd.ExecuteNonQuery();
                    PopulateGridview();
                    lblSuccessMessage.Text = "Selected Record Deleted";
                    lblErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void ButtonTable_Click(object sender, EventArgs e)
        {
            gvPhoneBook.Visible = false;
            DataTable dtblTable = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM " + DropDownListTable.SelectedItem.ToString(), sqlCon);
                sqlDa.Fill(dtblTable);
            }
            if (dtblTable.Rows.Count > 0)
            {
                GridViewGV.DataSource = dtblTable;
                GridViewGV.DataBind();
            }
            else
            {
                dtblTable.Rows.Add(dtblTable.NewRow());
                GridViewGV.DataSource = dtblTable;
                GridViewGV.DataBind();
                GridViewGV.Rows[0].Cells.Clear();
                GridViewGV.Rows[0].Cells.Add(new TableCell());
                GridViewGV.Rows[0].Cells[0].ColumnSpan = dtblTable.Columns.Count;
                GridViewGV.Rows[0].Cells[0].Text = "No Data Found ..!";
                GridViewGV.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }
        protected void ButtonDept_Click(object sender, EventArgs e)
        {
            gvPhoneBook.Visible = false;
            DataTable dtblTable = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Username " + "where Section ='" + DropDownListDept.SelectedItem.ToString() + "'", sqlCon);
                sqlDa.Fill(dtblTable);
            }
            if (dtblTable.Rows.Count > 0)
            {
                GridViewGV.DataSource = dtblTable;
                GridViewGV.DataBind();
            }
            else
            {
                dtblTable.Rows.Add(dtblTable.NewRow());
                GridViewGV.DataSource = dtblTable;
                GridViewGV.DataBind();
                GridViewGV.Rows[0].Cells.Clear();
                GridViewGV.Rows[0].Cells.Add(new TableCell());
                GridViewGV.Rows[0].Cells[0].ColumnSpan = dtblTable.Columns.Count;
                GridViewGV.Rows[0].Cells[0].Text = "No Data Found ..!";
                GridViewGV.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void GridViewGV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ButtonUname_Click(object sender, EventArgs e)
        {
            gvPhoneBook.Visible = false;
            DataTable dtblTable = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Username " +  "where Username like'%" + TextBoxUname.Text + "%'", sqlCon);
                sqlDa.Fill(dtblTable);
            }
            if (dtblTable.Rows.Count > 0)
            {
                GridViewGV.DataSource = dtblTable;
                GridViewGV.DataBind();
            }
            else
            {
                dtblTable.Rows.Add(dtblTable.NewRow());
                GridViewGV.DataSource = dtblTable;
                GridViewGV.DataBind();
                GridViewGV.Rows[0].Cells.Clear();
                GridViewGV.Rows[0].Cells.Add(new TableCell());
                GridViewGV.Rows[0].Cells[0].ColumnSpan = dtblTable.Columns.Count;
                GridViewGV.Rows[0].Cells[0].Text = "No Data Found ..!";
                GridViewGV.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }
    }
}