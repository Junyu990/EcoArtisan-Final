<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminViewReward.aspx.cs" Inherits="EcoArtisanFinal.AdminViewReward" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container">
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="/AdminHome">Home</a></li>
                <li class="breadcrumb-item"><a href="/AdminRewards">Rewards</a></li>
                <li class="breadcrumb-item active" aria-current="page">View Reward</li>
            </ol>
        </nav>
        <div class="row">
            <div class="col-md-6">
                <h2>View Reward</h2>
                <div class="form-group">
                    <label>Item Name:</label>
                    <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Description:</label>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Filter Class:</label>
                    <asp:TextBox ID="txtFilterClass" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Points:</label>
                    <asp:TextBox ID="txtPoints" runat="server" CssClass="form-control" ReadOnly="true" type="number"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Discount:</label>
                    <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control" ReadOnly="true" type="number" step="0.01"></asp:TextBox>
                </div>
                <asp:Button ID="update_reward_btn" runat="server" Text="Update"  CssClass="btn btn-primary" OnClick="update_reward_btn_Click" />
                <asp:Button ID="delete_reward_btn" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this reward?');" CssClass="btn btn-danger" OnClick="delete_reward_btn_Click" />
                <a href="/AdminRewards" class="btn btn-secondary">Cancel</a>
            </div>
            <div class="col-md-6">
                <h2>Image</h2>
                <div class="form-group">
                    <img src="../Content/Rewards/<%: Session["SelectedRewardImageUrl"] %>" class="img-fluid rounded reward-image" />
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
