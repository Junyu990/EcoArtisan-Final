<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RedemptionConfirmation.aspx.cs" Inherits="EcoArtisanFinal.RedemptionConfirmation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <style>
        .confirmation-container {
            margin-top: 50px;
            text-align: center;
        }

        .confirmation-message {
            font-size: 24px;
            margin-bottom: 20px;
        }

        .home-button {
            padding: 10px 20px;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            text-decoration: none;
        }

        .home-button:hover {
            background-color: #0056b3;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container confirmation-container">
        <h2 class="confirmation-message">Congratulations! Your reward has been redeemed successfully.</h2>
        <p>Your redemption details:</p>
        <p>Redemption ID: <asp:Label ID="lblRedemptionID" runat="server" Text=""></asp:Label></p>
        <p>Redemption Date: <asp:Label ID="lblRedemptionDate" runat="server" Text=""></asp:Label></p>
        <p><a href="/" class="home-button">Back to Home</a></p>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
