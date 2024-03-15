<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmailVerification.aspx.cs" Inherits="EcoArtisanFinal.EmailVerification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group">
        <div class="form-group">
            <label for="tb_ConfirmationCode">A code has been sent to your email, please verify your email!</label>   
            <asp:TextBox ID="tb_ConfirmationCode" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:Button ID="btn_ConfirmCode" runat="server" Text="Verify Code!" CssClass="btn btn-primary" OnClick="btn_ConfirmCode_Click"/>
            <asp:Label ID="lbl_Message" runat="server" CssClass="text-danger"></asp:Label>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
