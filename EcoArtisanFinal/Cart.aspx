<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="EcoArtisanFinal.Cart1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href='<%= ResolveUrl("~/Content/Cart.css") %>?v=<%= DateTime.Now.Ticks %>' />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css">

    <style>
        .heartProd{
            color: black;
            font-size: 24px;
        }
    </style>


    <div class="cartContainer">
        <div class="clotheContainer">
            <div class="bag">
                Bag
            </div>
            <asp:Repeater ID="cartRepeater" runat="server" OnItemCommand="cartRepeater_ItemCommand1" OnItemDataBound="cartRepeater_ItemDataBound1">
                <ItemTemplate>
                    <div class="clothe">
                        <div class="clotheImageUrl">
                            <asp:Image CssClass="imgProduct" ID="imgProduct" runat="server" ImageUrl='<%# "../Content/Products/" + Eval("prodImage") %>' />
                        </div>
                        <div class="clotheNamePriceHeartTrash">
                            <div class="clotheNamePrice">
                                <div class="clotheName"> <%# Eval("prodName") %></div>
                                <div class="clothePrice">S$ <%# Eval("prodPrice", "{0:N2}") %></div>
                            </div>
                            <div class="clotheDesc"><%# Eval("prodDesc") %></div>
                            <div class="clotheQuantityContainer">
                                <div class="clotheQuantity">
                                    Quantity 
                                    <div class="clotheQuantityController">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="13" height="13" fill="currentColor" class="bi bi-dash-circle" viewBox="0 0 16 16" onclick="updateQuantity('<%# Eval("ProdID") %>', -1)">
                                            <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16"/>
                                            <path d="M4 8a.5.5 0 0 1 .5-.5h7a.5.5 0 0 1 0 1h-7A.5.5 0 0 1 4 8"/>
                                        </svg>
                                        <span class="quantity" data-prodID='<%# Eval("ProdID") %>' data-cartItemQty='<%# Eval("cartItemQty") %>'><%# Eval("cartItemQty") %></span>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="13" height="13" fill="currentColor" class="bi bi-plus-circle" viewBox="0 0 16 16" onclick="updateQuantity('<%# Eval("ProdID") %>', 1)">
                                            <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16"/>
                                            <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4"/>
                                        </svg>
                                    </div>
                                </div>
                            </div>
                            <div class="clotheHeartTrash">
                               <div>
                                    <asp:LinkButton CssClass="heartProd fa-regular fa-heart" ID="linkprod_Wishlist" runat="server" CommandName="AddWish" CommandArgument='<%# Eval("ProdID") %>' ></asp:LinkButton>
                                </div>
                                <div class="fa-regular fa-trash-can" onclick="deleteCartItem('<%# Eval("ProdID") %>')"></div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Repeater ID="cartRepeater1" runat="server">
                <ItemTemplate>
                    <div class="clothe">
                        <div class="clotheImageUrl">
                            <canvas id="canvas_<%# Eval("SphereID") %>" width="170" height="170" style="border-radius: 1rem"></canvas>
                            <input type="hidden" class="sphereImageData" value='<%# Eval("SphereImage") %>' />
                        </div>
                        <div class="clotheNamePriceHeartTrash">
                            <div class="clotheNamePrice">
                                <div class="clotheName"><%# Eval("SphereSize") %> <%# Eval("SphereColour") %>  <%# Eval("SphereMat") %></div>
                                <div class="clothePrice">S$ <%# Eval("SpherePrice", "{0:N2}") %></div>
                            </div>
                            <div class="clotheDesc"><%# Eval("SphereDesc") %></div>
                            <div class="clotheQuantityContainer">
                                <div class="clotheQuantity">
                                    Quantity: 
                                    <span class="quantity" data-prodID='<%# Eval("SphereID") %>' data-cartItemQty='<%# Eval("CustomCartItemQty") %>'><%# Eval("CustomCartItemQty") %></span>
                                </div>
                            </div>
                            <div class="clotheHeartTrash">
                                <div class="fa-regular fa-trash-can" onclick="deleteCustomCartItem('<%# Eval("SphereID") %>')"></div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div id="emptyCartMessage" runat="server">
                <img src="../Content/Images/cartempty.png">
            </div>
        </div>

        <div class="checkoutConatiner">
            <div class="summary">
                Summary
            </div>
            <div class="subtotal">
                Subtotal
                <div id="subtotal">Subtotal</div>
            </div>
            <div class="deliveryfee">
                Shipping Fee
                <div id="deliveryfee">S$ 2.00</div>
            </div>
            <hr class="beforetotal" />
            <div class="total">
                Total
                <div id="total"></div>
            </div>
            <hr class="aftertotal"/>
            <div class="checkout">
                <div class="checkoutButton"><button id="checkoutButton" type="button">Checkout</button></div>
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/three.js/r128/three.min.js"></script>

    <script>
        // Assume you have a button with id 'checkoutButton' that triggers the checkout
        $(document).ready(function () {
            $("#checkoutButton").click(function () {
                // Check if the cart is empty
                if ($('.clothe').length === 0) {
                    // Cart is empty, prevent checkout and show alert
                    alert("Your cart is empty. Please add items before proceeding to checkout.");
                } else {
                    // Cart is not empty, make an AJAX call to the server-side method
                    $.ajax({
                        type: "POST",
                        url: "Cart.aspx/CheckoutButtonClick",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            // 'result' contains the URL of the Stripe Checkout session
                            // Redirect the user to the Stripe Checkout page
                            window.location.href = result.d;
                        },
                        error: function (error) {
                            // Handle errors
                            console.log("Error: " + error.responseText);
                        }
                    });
                }
            });
        });

        function updateQuantity(productID, change) {
            var quantityElement = document.querySelector('[data-prodID="' + productID + '"]');
            if (quantityElement) {
                var currentQuantity = parseInt(quantityElement.innerText);

                // For dynamic limits, you could fetch the limit here based on the productID, or include it in the data attributes.
                var maxQuantity = 5; // Fixed limit example

                // Check if trying to increase beyond the maximum allowed quantity
                if (change > 0 && currentQuantity >= maxQuantity) {
                    alert("You can only purchase up to " + maxQuantity + " of the same product per purchase.");
                    return; // Exit the function to prevent increasing the quantity
                }

                // Prevent decrementing below 1
                if (change === -1 && currentQuantity === 1) {
                    return;
                }

                var newQuantity = currentQuantity + change;
                quantityElement.innerText = newQuantity;

                // Make an AJAX call to update the cartItemQty
                updateCartItemQuantity(productID, newQuantity);
            }
        }

        function updateCartItemQuantity(productID, requestedQuantity) {
            $.ajax({
                type: "POST",
                url: "Cart.aspx/UpdateCartItemQuantity",
                data: JSON.stringify({ productID: productID, requestedQuantity: requestedQuantity }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {
                    calculateMoney();
                },
            });
        }

        function deleteCartItem(productID) {
            // Display a confirmation dialog
            var confirmDelete = confirm("Are you sure you want to delete this item?");

            if (confirmDelete) {
                // Make an AJAX call to delete the item
                $.ajax({
                    type: "POST",
                    url: "Cart.aspx/DeleteCartItem",
                    data: JSON.stringify({ productID: productID }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {
                        var deletedCartItem = document.querySelector('[data-prodID="' + productID + '"]').closest('.clothe');
                        deletedCartItem.remove();

                        calculateMoney();

                        checkCartEmpty()
                    },
                });
            }
        }

        function deleteCustomCartItem(SphereID) {
            // Display a confirmation dialog
            var confirmDelete = confirm("Are you sure you want to delete this item?");

            if (confirmDelete) {
                // Make an AJAX call to delete the item
                $.ajax({
                    type: "POST",
                    url: "Cart.aspx/DeleteCustomCartItem",
                    data: JSON.stringify({ SphereID: SphereID }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {
                        var deletedCartItem = document.querySelector('[data-prodID="' + SphereID + '"]').closest('.clothe');
                        deletedCartItem.remove();

                        calculateMoney();

                        checkCartEmpty()
                    },
                });
            }
        }

        // JavaScript to calculate subtotal on the client side
        document.addEventListener("DOMContentLoaded", function () {
            calculateMoney();
            checkCartEmpty()
        });

        function calculateMoney() {
            let subtotal = 0;
            $('.clothePrice').each(function () {
                let priceText = $(this).text().replace('S$', '').trim();
                let price = parseFloat(priceText);
                if (isNaN(price)) {
                    console.error('Invalid price text:', priceText);
                    return; // Skip this iteration
                }
                let quantityText = $(this).closest('.clothe').find('.quantity').text();
                let quantity = parseInt(quantityText);
                if (isNaN(quantity)) {
                    console.error('Invalid quantity text:', quantityText);
                    return; // Skip this iteration
                }
                subtotal += price * quantity;
            });

            let deliveryFee = 2.00; // Assuming a fixed delivery fee for simplicity
            let total = subtotal + deliveryFee;

            $('#subtotal').text('S$ ' + subtotal.toFixed(2));
            $('#total').text('S$ ' + total.toFixed(2));
        }

        function checkCartEmpty() {
            // Check if there are any items in the cart
            if ($('.clothe').length === 0) {
                $('#MainContent_emptyCartMessage').show();
            } else {
                $('#MainContent_emptyCartMessage').hide();
            }
        }

        $(".sphereImageData").each(function () {
            var jsonData = $(this).val();
            var canvasId = "canvas_" + $(this).prev("canvas").attr("id").split("_")[1];
            initializeThreeJS(canvasId, jsonData);
        });

        function initializeThreeJS(canvasId, jsonData) {
            var data = JSON.parse(jsonData);

            var canvas = document.getElementById(canvasId);
            var renderer = new THREE.WebGLRenderer({ antialias: true, canvas: canvas });
            renderer.setSize(canvas.width, canvas.height);

            var scene = new THREE.Scene();

            // Set the camera's aspect ratio to match the canvas
            var camera = new THREE.PerspectiveCamera(75, canvas.width / canvas.height, 0.1, 1000);
            camera.position.z = 10;

            var geometry = new THREE.SphereGeometry(1, 64, 64); // Use more segments for a smoother sphere
            var material = new THREE.MeshPhongMaterial({
                color: 0xFFFFFF,
                specular: 0x111111,
                shininess: 100
            });
            var sphere = new THREE.Mesh(geometry, material);
            scene.add(sphere);

            // Ensure the sphere's scale is uniform in all dimensions
            var maxDimension = Math.max(data.x, data.y, data.z);
            sphere.scale.set(maxDimension, maxDimension, maxDimension); // This will make the sphere scale uniformly

            var ambientLight = new THREE.AmbientLight(0x404040);
            scene.add(ambientLight);

            var pointLight = new THREE.PointLight(0xffffff, 1, 100);
            pointLight.position.set(5, 5, 5);
            scene.add(pointLight);

            function animate() {
                requestAnimationFrame(animate);
                renderer.render(scene, camera);
            }

            animate();
        }
    </script>
</asp:Content>