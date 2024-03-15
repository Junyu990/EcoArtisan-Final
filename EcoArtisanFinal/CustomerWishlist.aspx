<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerWishlist.aspx.cs" Inherits="EcoArtisanFinal.CustomerWishlist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <script src="https://kit.fontawesome.com/db6fc7cda2.js" crossorigin="anonymous"></script>
    <link href="<%= ResolveUrl("~/Content/Product.css") %>?v<%=DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />
    <div class="noWishes">
        <asp:Label CssClass="lbl_wishlistTrue center" ID="lbl_wishlistTrue" runat="server" Text=""></asp:Label><br />

        <asp:Button CssClass="btn_ViewProd" ID="btn_ViewProd" runat="server" Text="View Products!" OnClick="btn_ViewProd_Click" style="margin-top: 2"  />
    </div>
    <h3 class="text-center" style="text-align:center;">Your Wishlist</h3>
    <div class="wishlistDiv" style="display:flex;">
        
        <asp:Repeater ID="repeater_Wishlist" runat="server" OnItemCommand="repeater_Wishlist_ItemCommand">
                    <ItemTemplate>
                        <div class="card">
                 <a href='<%# "ProductDetails.aspx?prodID=" + Eval("ProdID") %>' class="more-info-btn" style="text-align:center;">
                <div class="card-details">

                    <img src='../Content/Products/<%# Eval("ProdImg") %>' alt='<%# Eval("ProdName") %>'>
                    <div class="card-title" style="text-align:center;"><%# Eval("ProdName") %></div>
                    <div class="card-price price" style="text-align:center;">$<%# Eval("ProdPrice") %></div>
                    
                    
                </div>
            </a>
                <div class="actIcon_div">
                           <div class="cardbtn"><i class="icon fas fa-shopping-cart"></i> Add to Cart</div>
                            <asp:LinkButton CssClass="action_icons" ID="link_wishlist" runat="server" CommandName="removeWish" CommandArgument='<%# Eval("ProdID") %>'><i class="fa-solid fa-heart"></i></asp:LinkButton>
                    </div>
            </div>
                    </ItemTemplate>

                </asp:Repeater>
    </div>
    <script>
        $(document).ready(function () {
            var wishlistTrue = $('.lbl_wishlistTrue').text().trim();
            if (wishlistTrue === "Wishlist is Empty...") {
                $('.wishlistDiv').hide();
            } else {
                $('.noWishes').hide();
            } 
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
