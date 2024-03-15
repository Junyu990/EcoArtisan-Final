<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EcoArtisanFinal.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container">
        <div class="col-md-8 col-md-offset-4">
            <div class="panel panel-default">
              <div class="panel-body">
                                
                  <div class="text-left">
        <table>
            <tr>
                <td>
                    <div class="text-center">
                        <h2>Log In</h2>
                    </div>
                </td>
            </tr>
            
            <tr>
                <td style="width: 60rem;">
                    <div class="form-group">
                        <label for="tb_LoginEmail">Email:</label>
                        <asp:TextBox ID="tb_LoginEmail" runat="server" CssClass="form-control" Required="true" placeholder="email address"></asp:TextBox>
                    </div>
                </td>
                <td style="width:120rem;">
                    <asp:RequiredFieldValidator ID="rfv_LoginEmail" runat="server" ControlToValidate="tb_LoginEmail"
                        ErrorMessage="Please enter your email" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="rev_LoginEmail" runat="server" ControlToValidate="tb_LoginEmail"
                        ErrorMessage="The email supplied is invalid." ForeColor="Red"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 60rem;">
                    <div class="form-group">
                        <label for="tb_LoginPassword">Password:</label>
                        <asp:TextBox ID="tb_LoginPassword" runat="server" TextMode="Password" CssClass="form-control" Required="true" placeholder="password"></asp:TextBox>
                    </div>
                </td>
                <td style="width:120rem;">
                    <asp:RequiredFieldValidator ID="rfv_LoginPassword" runat="server" ControlToValidate="tb_LoginPassword"
                        ErrorMessage="Password is required" ForeColor="Red" Display="Dynamic" CssClass="text-danger">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 60rem;">
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="#CC0000" ShowMessageBox="False" />
                </td>
            </tr>
            <tr>
                <td>
                    <div class="pt-1 mb-4">
                        <asp:Button ID="btn_Login" runat="server" Text="Login" CssClass="btn btn-primary btn-info btn-lg btn-block" OnClick="btn_Login_Click"/>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <p class="small mb-5 pb-lg-2"><a class="text-muted" href="/ForgetPassword">Forgot password?</a></p>
                    <p>Don't have an account? <a href="/SignUp" class="link-info">Register here</a></p>
                </td>
            </tr>
        </table>
    </div>
                  </div>
                </div>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
