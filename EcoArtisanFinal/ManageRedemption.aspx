<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageRedemption.aspx.cs" Inherits="EcoArtisanFinal.ManageRedemption" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%= ResolveUrl("~/Content/redemption.css") %>?v<%=DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="/AdminHome">Home</a></li>
                <li class="breadcrumb-item active" aria-current="page">Manage Redemption</li>
            </ol>
        </nav>
    <div class="container">
            <h2>Manage Redemption</h2>
            <table class="table table-bordered table-contain">
                <thead class="tableHead">
                    <tr>
                        <th>Redemption ID</th>
                        <th>User Name</th>
                        <th>Redemption Date</th>
                        <th>Item Name</th>
                        <th>Item Description</th>
                        <th>Filter Class</th>
                        <th>Image URL</th>
                        <th>Points</th>
                        <th>Discount</th>
                    </tr>
                </thead>
                <tbody class="tableBody">
                    <asp:Literal ID="litRedemptions" runat="server"></asp:Literal>
                </tbody>
            </table>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
