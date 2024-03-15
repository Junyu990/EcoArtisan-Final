<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminProductDetails.aspx.cs" Inherits="EcoArtisanFinal.AdminProductDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <script src="/Content/Product.js?v=<%=DateTime.Now.Ticks %>" type="text/javascript"></script>
    <link href="/Content/Product.css?v=<%=DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <h2>Product ID: <asp:Label ID="lbl_prodID" runat="server" Text=""></asp:Label></h2>
    <table class="nav-justified addProdForm">
        <tr>
            <td class="auto-style2">Product Name: </td>
            <td class="auto-style6">
                <asp:TextBox ID="tb_updProdName" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPName" runat="server" ControlToValidate="tb_updProdName" ErrorMessage="Empty Product Name" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style9">Product Description: </td>
            <td class="auto-style10">
                <asp:TextBox ID="tb_updProdDesc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" ReadOnly="true"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tb_updProdDesc" ErrorMessage="Empty Description" ForeColor="Red"></asp:RequiredFieldValidator>
                <br />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">Price: </td>
            <td class="auto-style6">
                <asp:TextBox ID="tb_updProdPrice" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tb_updProdPrice" ErrorMessage="Empty Price" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style3">Uploaded Image: </td>
            <td class="auto-style7">
                        <asp:Image ID="img_updProdimg" runat="server" Width="148px" Height="142px" CssClass="imgclass_ProdImg auto-style13" />

            </td>
        </tr>

       
        <tr>
            <td class="auto-style4">Product Quantity: </td>
            <td class="auto-style8" style="padding-top: 50px;">
                <asp:TextBox ID="tb_updProdQty" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tb_updProdQty" ErrorMessage="No Quantity Input" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style11">Product Impact: </td>
            <td class="auto-style12">
                <asp:TextBox ID="tb_updProdImpact" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tb_updProdImpact" ErrorMessage="No Impact Input" ForeColor="Red"></asp:RequiredFieldValidator>
                
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <asp:Button CssClass="btn btn-warning" ID="btn_UpdateProduct" runat="server" Text="Update" OnClick="btn_UpdateProduct_Click" />
    <asp:Button CssClass="btn btn-secondary" ID="btn_CancelUpdate" runat="server" Text="Cancel" CausesValidation="false" OnClick="btn_CancelUpdate_Click" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
