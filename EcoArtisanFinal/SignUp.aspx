<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="EcoArtisanFinal.SignUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="https://www.google.com/recaptcha/enterprise.js" async defer></script>
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
                            <h2>Sign Up</h2>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 60rem;">
                        <div class="form-group">
                            <label for="tb_Username">Username:</label>
                            <asp:TextBox ID="tb_Username" runat="server" CssClass="form-control" Required="true" placeholder="username"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 120rem;">
                        <asp:RequiredFieldValidator ID="rv_Username" runat="server" ControlToValidate="tb_Username"
                            ErrorMessage="Username is required" ForeColor="Red" Display="Dynamic" CssClass="text-danger">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 60rem;">
                        <div class="form-group">
                            <label for="tb_Email">Email:</label>
                            <asp:TextBox ID="tb_Email" runat="server" CssClass="form-control" Required="true" placeholder="email address"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 120rem;">
                        <asp:RequiredFieldValidator ID="rfv_Email" runat="server" ControlToValidate="tb_Email" 
                            ErrorMessage="Please enter your email" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                        <br />
                        <asp:RegularExpressionValidator ID="rev_Email" runat="server" ControlToValidate="tb_Email" 
                            ErrorMessage="The email supplied is invalid." ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 60rem">
                        <div class="form-group">
                            <label for="tb_ConfirmEmail">Confirm Email:</label>
                            <asp:TextBox ID="tb_ConfirmEmail"  CssClass="form-control" Required="true" runat="server" placeholder="confirm email address"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 120rem">
                        <asp:RequiredFieldValidator ID="rfv_ConfirmEmail" runat="server" ControlToValidate="tb_ConfirmEmail"
                            ErrorMessage="Confirm Email is required" ForeColor="Red" Display="Dynamic" CssClass="text-danger">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cpv_ConfirmEmail" runat="server" ControlToCompare="tb_Email" ControlToValidate="tb_ConfirmEmail" 
                            ErrorMessage="Your email does not match." ForeColor="Red">
                        </asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 60rem">
                        <div class="form-group">
                            <label for="tb_Password">Password:</label>
                            <asp:TextBox ID="tb_Password" runat="server" TextMode="Password" CssClass="form-control" Required="true" placeholder="password"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 120rem">
                        <asp:RequiredFieldValidator ID="rfv_Password" runat="server" ControlToValidate="tb_Password"
                            ErrorMessage="Password is required" ForeColor="Red" Display="Dynamic" CssClass="text-danger">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rev_Password" runat="server" ControlToValidate="tb_Password"
                            ErrorMessage="Password must be at least 8 characters and include at least one uppercase letter, one lowercase letter, and one digit."
                            ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$" ForeColor="Red" CssClass="text-danger">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 60rem">
                    <div class="form-group">
                        <label for="tb_ConfirmPassword">Confirm Password:</label>
                        <asp:TextBox ID="tb_ConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" Required="true" placeholder="confirm password"></asp:TextBox>
                    </div>
                    </td>
                    <td style="width: 120rem">
                        <asp:RequiredFieldValidator ID="rfv_ConfirmPassword" runat="server" ControlToValidate="tb_ConfirmPassword"
                            ErrorMessage="Confirm Password is required" ForeColor="Red" Display="Dynamic" CssClass="text-danger">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cv_ConfirmPassword" runat="server" ControlToValidate="tb_ConfirmPassword"
                            ControlToCompare="tb_Password" Operator="Equal" ErrorMessage="Passwords must match." ForeColor="Red" Display="Dynamic" CssClass="text-danger">
                        </asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 60rem">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="#CC0000" ShowMessageBox="False" />
                    </td>
                    
                </tr>
                <tr>
                    <td>
                        <br />
                        <asp:Image ID="captchaImage" runat="server" Height="60px" Width="280px" ImageUrl="~/MyCaptcha.aspx" /><br />
                        <div class="form-group">
                            <label for="captchaCode">Enter Captcha Code:</label>
                                <asp:TextBox ID="captchacode" runat="server" Placeholder="Enter Captcha code" CssClass="form-control"></asp:TextBox><br />
                            </div>
                    </td>
                </tr>
                <tr> 
                    <td>
                        <div>
                        <asp:Button ID="btn_SignUp" runat="server" Text="Sign Up!" CssClass="btn btn-primary" OnClick="btn_SignUp_Click"/>
                        </div>
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
