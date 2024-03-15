<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerifyEmail.aspx.cs" Inherits="EcoArtisanFinal.VerifyEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
	<div class="row">
		<div class="col-md-4 col-md-offset-4">
            <div class="panel panel-default">
              <div class="panel-body">
                <div class="text-center">
                  <h3><i class="fa-regular fa-paper-plane fa-4x"></i></h3>
                  <h2 class="text-center">Enter Verification Code</h2>
                  <p>Please look in your inbox for your verification code.</p>
                  <div class="panel-body">
                      <div class="form-group">
                        <div class="input-group">
                          <span class="input-group-addon"><i class="glyphicon glyphicon-arrow-right"></i></span>
                            <asp:TextBox ID="code_TB" placeholder="verification code" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                      </div>
                      <div class="form-group">
                        
                        <asp:Button ID="verify_btn" runat="server" Text="Verify Code" class="btn btn-lg btn-primary btn-block" OnClick="verify_btn_click"/>
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
