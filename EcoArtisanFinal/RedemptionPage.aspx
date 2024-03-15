<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RedemptionPage.aspx.cs" Inherits="EcoArtisanFinal.RedemptionPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href='<%= ResolveUrl("~/Content/redemption.css") %>?v=<%= DateTime.Now.Ticks %>' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container redemption-container">
            <div class="container redemption-container">
            <h2 class="mt-5">Redeem Your Reward</h2>
            <div class="row">
                <div class="col-md-6">
                    <image src="../Content/Rewards/<%: Session["SelectedRewardImageUrl"] %>" Class="img-fluid rounded reward-image"></image>
                </div> 
                <div class="col-md-6">
                    <div class="card reward-details">
                        <div class="card-body">
                            <div class="my-rewards">
                                 <asp:Label ID="pointsLabel" runat="server"></asp:Label>
                            </div>
                            <h5 class="card-title">
                                Reward Name: <asp:Label ID="lblRewardName" runat="server" CssClass="reward-name form-control"></asp:Label>

                            </h5>
                            <p class="card-text">
                                Reward Description: <br />
                                <asp:Label ID="lblRewardDescription" runat="server" CssClass="reward-description" TextMode="MultiLine" Row="4"></asp:Label>

                            </p>
                            <h3 class="card-text" style="">EcoCoins to Redeem: <asp:Label ID="lblRewardPoints" runat="server" CssClass="rewardtext"></asp:Label><img style="height:2rem; width:2rem;" src="../Content/Images/ecocoin.png" alt=""></h3>
                            <div class="redemption-actions mt-3">
                                <asp:Button ID="btnRedeem" runat="server" CssClass="btn btn-primary btn-redeem" Text="Redeem" OnClick="btnRedeem_Click" />
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
