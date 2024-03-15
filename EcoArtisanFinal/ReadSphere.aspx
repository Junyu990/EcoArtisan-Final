<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReadSphere.aspx.cs" Inherits="EcoArtisanFinal.ReadSphere" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%= ResolveUrl("~/Content/ReadSphere.css") %>?v<%=DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="form-container">
        <div class="form-title">
            <h1>Fill in the Sphere Details</h1>
        </div>
        <div class="form-body">
            <div class="form-group">
                <label for="tb_SphereID">SphereID</label>
                <asp:TextBox ID="tb_SphereID" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Button ID="Btn_read" runat="server" OnClick="Btn_read_Click" Text="Read" CssClass="btn btn-read" />
            </div>
            <div class="form-group">
                <label for="tb_UserID">UserID</label>
                <asp:TextBox ID="tb_UserID" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="imgSphere">Image</label>
                <asp:Image ID="imgSphere" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="tb_SphereDesc">Description</label>
                   <asp:TextBox ID="tb_SphereDesc" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="tb_SpherePrice">Estimated Price</label>
                <asp:TextBox ID="tb_SpherePrice" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="centered-panel">
                <asp:Panel ID="Panel1" runat="server" GroupingText="Material">
                    <asp:RadioButtonList ID="rbl_SphereMat" runat="server" RepeatDirection="Vertical">
                        <asp:ListItem>Glass</asp:ListItem>
                        <asp:ListItem>Silicone</asp:ListItem>
                    </asp:RadioButtonList>
                </asp:Panel>
            </div>

            <div class="centered-panel">
                <asp:Panel ID="Panel2" runat="server" GroupingText="Colour">
                    <asp:CheckBoxList ID="cbl_SphereColour" runat="server" RepeatDirection="Vertical">
                        <asp:ListItem>Transparent</asp:ListItem>
                        <asp:ListItem>Yellow</asp:ListItem>
                    </asp:CheckBoxList>
                </asp:Panel>
            </div>
            <div class="centered-panel">
                <asp:Panel ID="Panel3" runat="server" GroupingText="Size">
                    <asp:DropDownList ID="ddl_SphereSize" runat="server" RepeatDirection="Vertical">
                        <asp:ListItem>XS</asp:ListItem>
                        <asp:ListItem>S</asp:ListItem>
                        <asp:ListItem>M</asp:ListItem>
                        <asp:ListItem>L</asp:ListItem>
                        <asp:ListItem>XL</asp:ListItem>
                        <asp:ListItem>XXL</asp:ListItem>
                        <asp:ListItem>XXXL</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
            </div>
            <div class="form-actions">
                <asp:Button ID="Btn_create" runat="server"  Text="Create" CssClass="btn btn-create" OnClick="Btn_create_Click" />
                <asp:Button ID="Btn_update" runat="server"  Text="Update" CssClass="btn btn-update" OnClick="Btn_update_Click" />
                <asp:HyperLink ID="HyperLinkManageSphere" runat="server" NavigateUrl="~/ManageSphere.aspx" CssClass="btn btn-manage">View Glass Records</asp:HyperLink>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
