<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/User.Master" CodeBehind="User.aspx.cs" Inherits="LoginPage.user" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    User
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 90%;
        }
        .auto-style2 {
            height: 34px;
        }
        .auto-style5 {
            width: 22%;
        }
        .auto-style6 {
            width: 27%;
        }
        .auto-style7 {
            width: 36%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentBody" runat="Server">
    <asp:Image ID="Image1" runat="server" BorderStyle="Solid" BorderWidth="150px" Height="175px" Width="175px" style="border-radius: 4px;
    padding: 5px; margin:10px;border: 1px solid #ddd;"  />
    <br />

    <table class="auto-style6">
        <tr>
            <td>

    &nbsp;&nbsp;

    <asp:Label ID="Label2" runat="server" Text="Label" Font-Bold="True" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
    &nbsp;&nbsp;
    <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
    &nbsp;&nbsp;
    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
    &nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" Text="Upload" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>
     <br />
        <table class="auto-style1">
            <tr>
                <td class="auto-style7">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name :&nbsp;&nbsp;&nbsp; </td>
                <td class="auto-style5">
                    <asp:TextBox ID="TextBoxUser" runat="server" MaxLength="30" Width="266px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save_Changes" runat="server" ControlToValidate="TextBoxUser" ErrorMessage="Username is empty" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; E-mail :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                <td class="auto-style5">
                    <asp:TextBox ID="TextBoxEmail" runat="server" MaxLength="20"  TextMode="Email" Enabled="False" Width="266px"></asp:TextBox>
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Address :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                <td class="auto-style5">
                    <asp:TextBox ID="TextBoxAddress" runat="server" MaxLength="50" Width="266px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Save_Changes" runat="server" ControlToValidate="TextBoxAddress" ErrorMessage="Address is empty" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Blood Group : </td>
                <td class="auto-style5">
                    <asp:TextBox ID="TextBox1" runat="server"  Enabled="False" ></asp:TextBox>
                </td>
                <td>
                   
                </td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Salary :</td>
                <td class="auto-style6">
                    <asp:TextBox ID="TextBoxSalary" runat="server" TextMode="Number"  ></asp:TextBox>
                </td>
                <td class="auto-style2">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBoxSalary" ErrorMessage="Salary is empty" ForeColor="Red" ValidationGroup="Save_Changes" ></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Section :</td>
                <td class="auto-style5">
                    <asp:TextBox ID="TextBox2" runat="server"  Enabled="False" ></asp:TextBox>
                </td>
                <td>
                   
                </td>
            </tr>
            <tr>
                <td class="auto-style7">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date of birth :</td>
                <td class="auto-style6">
                    <asp:TextBox ID="TextBoxBirth" runat="server" TextMode="Singleline"  Enabled="False" ></asp:TextBox>
                </td>
                <td class="auto-style2">
                    
                </td>
            </tr>
            
            <tr>
                <td class="auto-style7">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Phone no. :</td>
                <td class="auto-style6">
                    <asp:TextBox ID="TextBoxPhone" runat="server" MaxLength="10" TextMode="Phone" Width="120px"></asp:TextBox>
                </td>
                <td class="auto-style2">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="TextBoxPhone" ErrorMessage="Phone no. is empty" ForeColor="Red" ValidationGroup="Save_Changes"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style7">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Sex :&nbsp; </td>
                <td class="auto-style6">
                    <asp:TextBox ID="TextBox3" runat="server"  Enabled="False"></asp:TextBox>
                </td>
                <td class="auto-style2">
                    
                </td>
            </tr>
            <tr>
                <td class="auto-style7">
                    &nbsp;</td>
                <td class="auto-style6">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="ButtonSave" runat="server" Text="Save Changes" ValidationGroup="Save_Changes" OnClick="ButtonSave_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                <td class="auto-style2">&nbsp;</td>
            </tr>
        </table>
    
</asp:Content>   

