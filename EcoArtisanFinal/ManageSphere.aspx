<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageSphere.aspx.cs" Inherits="EcoArtisanFinal.ManageSphere" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%= ResolveUrl("~/Content/ManageSphere.css") %>?v<%=DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="search-container">
        <asp:TextBox ID="tbSearchID" runat="server" CssClass="form-control" />
        <asp:Button ID="BtnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="BtnSearch_Click" />
    </div>
    <div class="table-container">
        <div class="table-header">
            <div class="header-cell">Sphere ID</div>
            <div class="header-cell">User ID</div>
            <div class="header-cell">Image</div>
            <div class="header-cell">Description</div>
            <div class="header-cell">Estimated Price</div>
            <div class="header-cell">Material</div>
            <div class="header-cell">Colour</div>
            <div class="header-cell">Size</div>
            <div class="header-cell">Actions</div>
        </div>
        <asp:Repeater ID="RepeaterSpheres" runat="server" OnItemCommand="RepeaterSpheres_ItemCommand">
            <ItemTemplate>
                <div class="table-row">
                    <div class="data-cell"><%# Eval("SphereID") %></div>
                    <div class="data-cell"><%# Eval("UserID") %></div>
                    <div class="data-cell"><%# Eval("SphereImage") %></div>
                    <div class="data-cell"><%# Eval("SphereDesc") %></div>
                    <div class="data-cell"><%# Eval("SpherePrice") %></div>
                    <div class="data-cell"><%# Eval("SphereMat") %></div>
                    <div class="data-cell"><%# Eval("SphereColour") %></div>
                    <div class="data-cell"><%# Eval("SphereSize") %></div>
                    <div class="action-cell">
                        <asp:Button ID="BtnUpdate" runat="server" CommandName="Update" CommandArgument='<%# Eval("SphereID") %>' Text="Update" CssClass="btn btn-warning" />
                        <asp:Button ID="BtnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("SphereID") %>' Text="Delete" CssClass="btn btn-danger" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
