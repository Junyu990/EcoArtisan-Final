<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminUpdateRewards.aspx.cs" Inherits="EcoArtisanFinal.AdminUpdateRewards" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="/AdminHome">Home</a></li>
                <li class="breadcrumb-item"><a href="/AdminRewards">Rewards</a></li>
                <li class="breadcrumb-item"><a href="/AdminViewReward">View Reward</a></li>
                <li class="breadcrumb-item active" aria-current="page">Update Reward</li>
            </ol>
        </nav>
        <h2>Update Reward</h2>
        <div>
            <div class="form-group">
                <label for="txtItemName">Item Name:</label>
                <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ControlToValidate="txtItemName" ErrorMessage="Item Name is required." Display="Dynamic" CssClass="text-danger" />
            </div>
            <div class="form-group">
                <label for="txtDescription">Description:</label>
                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="Description is required." Display="Dynamic" CssClass="text-danger" />
            </div>
            <div class="form-group">
                <label for="ddlFilterClass">Filter Class:</label>
                <asp:DropDownList ID="ddlFilterClass" runat="server" CssClass="form-control">
                    <asp:ListItem Text="-- Select --" Value=""></asp:ListItem>
                    <asp:ListItem Text="Voucher" Value="filter-vouchers"></asp:ListItem>
                    <asp:ListItem Text="Limited Time Item" Value="filter-limited"></asp:ListItem>
                    <asp:ListItem Text="Product" Value="filter-products"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvFilterClass" runat="server" ControlToValidate="ddlFilterClass" ErrorMessage="Filter Class is required." Display="Dynamic" CssClass="text-danger" InitialValue=""></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="fuImage">Select New Image:</label>
                <asp:FileUpload CssClass="fuImage form-control" ID="fuImage" runat="server" onchange="displayImage();"/>
            </div>
            <div class="form-group">
                <label for="txtPoints">Points:</label>
                <asp:TextBox ID="txtPoints" runat="server" CssClass="form-control" type="number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPoints" runat="server" ControlToValidate="txtPoints" ErrorMessage="Points is required." Display="Dynamic" CssClass="text-danger" />
            </div>
            <div class="form-group">
                <label for="txtDiscount">Discount:</label>
                <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control" type="number" step="0.01"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDiscount" runat="server" ControlToValidate="txtDiscount" ErrorMessage="Discount is required." Display="Dynamic" CssClass="text-danger" />
            </div>
            <div class="form-group">
                <br />
                <asp:Button ID="btnUpdate" runat="server" Text="Update"  CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary"  CausesValidation="false" OnClick="btnCancel_Click"/>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
