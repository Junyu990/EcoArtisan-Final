<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PasswordResetPage.aspx.cs" Inherits="EcoArtisanFinal.PasswordResetPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="text-center">
                            <h3><i class=""></i></h3>
                            <h2 class="text-center">Reset your Password!</h2>
                            <p>Please fill in your New Password</p>
                            <div class="panel-body">
                                <div class="form-group">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-arrow-right"></i></span>
                                        <asp:TextBox ID="UserEmail_TB" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                                        <asp:TextBox ID="passTB" placeholder="password" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator ID="rfv_passTB" runat="server" ControlToValidate="passTB"
                                        ErrorMessage="Password is required" ForeColor="Red" Display="Dynamic" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                                        <asp:TextBox ID="confirmpassTB" placeholder="confirm password" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator ID="rfv_confirmpassTB" runat="server" ControlToValidate="confirmpassTB"
                                        ErrorMessage="Confirm Password is required" ForeColor="Red" Display="Dynamic" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cv_confirmpassTB" runat="server" ControlToValidate="confirmpassTB"
                                        ControlToCompare="passTB" Operator="Equal" ErrorMessage="Passwords must match." ForeColor="Red" Display="Dynamic" CssClass="text-danger">
                                    </asp:CompareValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="verify_btn" runat="server" Text="Verify Code" class="btn btn-lg btn-primary btn-block" OnClick="verify_btn_Click" />
                                </div>
                                <input type="hidden" class="hide" name="token" id="token" value="">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
