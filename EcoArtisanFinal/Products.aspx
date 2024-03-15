<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="EcoArtisanFinal.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <script src="https://kit.fontawesome.com/db6fc7cda2.js" crossorigin="anonymous"></script>
    <link href="<%= ResolveUrl("~/Content/Product.css") %>?v<%=DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />
    <h3 style="text-align:center;">Our Products</h3>

    <div class="cardcontainer">
        <asp:Repeater ID="repeater_Product" runat="server" OnItemCommand="repeater_Product_ItemCommand" OnItemDataBound="repeater_Product_ItemDataBound">
            <ItemTemplate>
                <div class="card">
                    <a href='<%# "ProductDetails.aspx?prodID=" + Eval("ProdID") %>' class="more-info-btn" style="text-align:center;">
                        <div class="card-details">
                            <img src='../Content/Products/<%# Eval("ProdImg") %>' alt='<%# Eval("ProdName") %>'>
                            <div class="card-title" style="text-align:center; text-decoration: none;"><%# Eval("ProdName") %></div>
                            <div class="card-price price" style="text-align:center;">$<%# Eval("ProdPrice") %></div>
                        </div>
                    </a>
                    <div class="actIcon_div">
                        <div class="cardbtn" data-prod-id='<%# Eval("ProdID") %>'><i class="icon fas fa-shopping-cart"></i> Add to Cart</div>

                        <div style="margin-top: 10px;margin-left: 3px;">
                            <asp:LinkButton CssClass="heartProd fa-regular fa-heart" ID="linkprod_Wishlist" runat="server" CommandName="AddWish" CommandArgument='<%# Eval("ProdID") %>'></asp:LinkButton>
                                <div id="wishlistModal" class="modal">
                                    <p id="wishlistMessage"></p>
                                </div>
                        </div>
                        <script>
                             // JavaScript to handle the click event of the heart icon
                             const heartIcons = document.querySelectorAll('.heartProd');

                             heartIcons.forEach(heartIcon => {
                                 heartIcon.addEventListener('click', function () {
                                     const prodID = this.getAttribute('data-prod-id');
                                     const isAddedToWishlist = this.classList.contains('fa-solid'); // Check if the heart icon has the 'fa-solid' class

                                     // Display modal
                                     const wishlistModal = document.getElementById('wishlistModal');
                                     const wishlistMessage = document.getElementById('wishlistMessage');

                                     if (isAddedToWishlist) {
                                         wishlistMessage.textContent = 'Product added to wishlist';
                                     } else {
                                         wishlistMessage.textContent = 'Product removed from wishlist';
                                     }

                                     wishlistModal.style.display = 'block';

                                     // Hide modal after a few seconds
                                     setTimeout(function () {
                                         wishlistModal.style.display = 'none';
                                     }, 3000); // 3000 milliseconds = 3 seconds
                                 });
                             });
                        </script>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script>
        $('.cardbtn').on('click', function () {
            var prodId = $(this).attr('data-prod-id'); // Ensure this matches the data attribute exactly.

            $.ajax({
                type: 'POST',
                url: 'Products.aspx/AddToCart',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ productId: parseInt(prodId, 10) }), // Ensure parsing as integer if required.
                dataType: "json",
                success: function (response) {
                    // First, access the 'd' property
                    var data = response.d;
                    // Since 'd' is a JSON string, parse it into an object
                    var parsedData = JSON.parse(data);

                    // Now, you can access the properties of parsedData
                    console.log("Success:", parsedData.success);
                    console.log("User ID:", parsedData.userId);
                    console.log("Product ID:", parsedData.productId);
                    console.log("Is New Item:", parsedData.isNewItem);
                    console.log("Quantity:", parsedData.quantity);
                },
                error: function (error) {
                    alert('Error adding product to cart: ' + error.responseText);
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
