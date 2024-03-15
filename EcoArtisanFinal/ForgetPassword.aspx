<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="EcoArtisanFinal.ForgetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
	<div class="row">
		<div class="col-md-4 col-md-offset-4">
            <div class="panel panel-default">
              <div class="panel-body">
                <div class="text-center">
                  <h3><i class="fa fa-lock fa-4x"></i></h3>
                  <h2 class="text-center">Forgot Password?</h2>
                  <p>You can reset your password through your email here. </br> Please enter email that your account is registered to.</p>
                  <div class="panel-body">
                      <div class="form-group">
                        <div class="input-group">
                          <span class="input-group-addon"><i class="glyphicon glyphicon-envelope color-blue"></i></span>
                            <asp:TextBox ID="email_TB" placeholder="email address" CssClass="form-control" runat="server" type="email"></asp:TextBox>
                        </div>
                      </div>
                      <div class="form-group">
                        
                        <asp:Button ID="resetpass_btn" runat="server" Text="Get Verification Code" class="btn btn-lg btn-primary btn-block" OnClick="resetpass_btn_Click"/>
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
