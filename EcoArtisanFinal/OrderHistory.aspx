<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" Inherits="EcoArtisanFinal.OrderHistory" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href='<%= ResolveUrl("~/Content/Order.css") %>?v=<%= DateTime.Now.Ticks %>' />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $(document).on('click', '.button-style', function (event) {
                event.preventDefault(); // Prevent the default action of the button

                // Toggle only if the clicked button is the angle-down or angle-up button
                if ($(this).hasClass('fa-angle-down') || $(this).hasClass('fa-angle-up')) {
                    var $currentAccordion = $(this).closest('.accordion');
                    var $currentPanel = $currentAccordion.next('.panel');
                    var $currentButton = $(this);

                    // Slide up any other panels, remove 'active' class from their accordion, and reset their buttons
                    $('.panel').not($currentPanel).slideUp().prev('.accordion').removeClass('active').find('.down').removeClass('fa-angle-up').addClass('fa-angle-down');

                    // Toggle the current panel
                    $currentPanel.stop(true, true).slideToggle(200, function () {
                        // Add 'active' class to the current accordion if the panel is now open, and toggle the button icon
                        if ($currentPanel.is(':visible')) {
                            $currentAccordion.addClass('active');
                            $currentButton.removeClass('fa-angle-down').addClass('fa-angle-up');
                        } else {
                            $currentAccordion.removeClass('active');
                            $currentButton.removeClass('fa-angle-up').addClass('fa-angle-down');
                        }
                    });
                }
            });
        });
    </script>

    <div class="header">Order History</div>
    <asp:Repeater ID="OrdersRepeater" runat="server">
        <ItemTemplate>
            <div class="accordion">
                <div class="table-style">
                    <table>
                        <tr>
                            <th>Order number</th>
                            <th>Estimated Arrival Date</th>
                            <th>Total amount</th>
                        </tr>
                        <tr>
                            <td>#<%# Eval("OrderID") %></td>
                            <td><%# Convert.ToDateTime(Eval("ETADate")).ToString("dd MMMMM yyyy") %></td>
                            <td><%# Eval("TotalAmount", "{0:C}") %></td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </div>
                <div class="buttons">
                    <asp:LinkButton ID="lnkDownloadReceipt" CssClass="download" runat="server" CommandName="Download" CommandArgument='<%# Eval("OrderID") %>' Text="View Receipt" OnCommand="lnkDownloadReceipt_Command"></asp:LinkButton>
                    <button type="button" class="button-style down fa-solid fa-angle-down"></button>
                </div>
            </div>
            <div class="panel">
                <asp:Repeater ID="OrderItemsRepeater" runat="server" DataSource='<%# Eval("OrderItems") %>'>
                    <ItemTemplate>
                        <div class="order-item">
                            <asp:Image CssClass="imgProduct" ID="imgProduct" runat="server" ImageUrl='<%# "../Content/Products/" + Eval("prodImage") %>' />
                            <div class="item-details">
                                <div class="name-impact">
                                    <div><%# Eval("ProdName") %></div>
                                    <div>Impact Made: <%# Eval("ProdImpact") %> 🍚</div>
                                </div>
                                <div><%# Eval("ProdDesc") %></div>
                                <div class="view-qty-total">
                                    <div class="links">
                                        <a href="<%# "ProductDetails.aspx?prodID=" + Eval("ProdID") %>">View Product</a> | <a href="<%# "AddReview.aspx?prodID=" + Eval("ProdID") %>">Add Review</a>
                                    </div>
                                    <div class="qty-total">
                                        <div>Quantity Purchased: <%# Eval("OrderItemQty") %></div>
                                        <div>Total: <%# ((decimal)Eval("ProdPrice") * (int)Eval("OrderItemQty")).ToString("C") %></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Repeater ID="CustomOrderItemsRepeater" runat="server" DataSource='<%# Eval("CustomOrderItems") %>'>
                    <ItemTemplate>
                        <div class="order-item">
                            <canvas id="canvas_<%# Eval("SphereID") %>" width="140" height="140" style="border-radius: 1rem"></canvas>
                            <input type="hidden" class="sphereImageData" value='<%# Eval("SphereImage") %>' />
                            <div class="item-details">
                                <div class="name-impact">
                                    <div> <%# Eval("SphereSize") %>  <%# Eval("SphereColour") %> <%# Eval("SphereMat") %></div>
                                </div>
                                <div><%# Eval("SphereDesc") %></div>
                                <div class="view-qty-total">
                                    <div class="links">
                                    </div>
                                    <div class="qty-total">
                                        <div>Quantity Purchased: <%# Eval("OrderItemQty") %></div>
                                        <div>Total: <%# ((decimal)Eval("SpherePrice") * (int)Eval("OrderItemQty")).ToString("C") %></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/three.js/r128/three.min.js"></script>
    <script>
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