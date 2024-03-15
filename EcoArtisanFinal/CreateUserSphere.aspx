<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateUserSphere.aspx.cs" Inherits="EcoArtisanFinal.CreateUserSphere" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <link href="<%= ResolveUrl("~/Content/ReadSphere.css") %>?v<%=DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/three.js/r128/three.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <div class="form-container">
        <div class="form-title">
            <h1>Fill in the Sphere Details</h1>
        </div>
        <div class="form-body">
            <table>
                <tr>
                    <td class="input-column">
                        <div class="form-group">
                            <asp:TextBox ID="tb_UserID" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="label-column">
                        <label for="tb_SphereImage">Image:</label>
                    </td>
                    <td class="input-column">
                        <div class="form-group">
                            <asp:TextBox ID="tb_SphereImage" runat="server" Height="22px" CssClass="form-control" Visible="false"></asp:TextBox>
                            <div class="centered-container">
                                <div id="threejs-read-container"></div>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="label-column">
                        <label for="tb_SphereDesc">Description:</label>
                    </td>
                    <td class="input-column">
                        <div class="form-group">
                            <asp:TextBox ID="tb_SphereDesc" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="label-column">
                        <label for="tb_SpherePrice">Estimated Price:</label>
                    </td>
                    <td class="input-column">
                        <div class="form-group">
                            <asp:TextBox ID="tb_SpherePrice" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="label-column">
                        <label for="rbl_SphereMat">Material:</label>
                    </td>
                    <td class="input-column">
                        <div class="centered-panel">
                                <asp:RadioButtonList ID="rbl_SphereMat" runat="server" RepeatDirection="Vertical">
                                    <asp:ListItem>Glass</asp:ListItem>
                                    <asp:ListItem>Silicone</asp:ListItem>
                                </asp:RadioButtonList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="label-column">
                        <label for="cbl_SphereColour">Colour:</label>
                    </td>
                    <td class="input-column">
                        <div class="centered-panel">
                                <asp:CheckBoxList ID="cbl_SphereColour" runat="server" RepeatDirection="Vertical">
                                    <asp:ListItem>Transparent</asp:ListItem>
                                    <asp:ListItem>Yellow</asp:ListItem>
                                </asp:CheckBoxList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="label-column">
                        <label for="ddl_SphereSize">Size:</label>
                    </td>
                    <td class="input-column">
                        <div class="centered-panel">
                                <asp:DropDownList ID="ddl_SphereSize" runat="server" RepeatDirection="Vertical">
                                    <asp:ListItem>XS</asp:ListItem>
                                    <asp:ListItem>S</asp:ListItem>
                                    <asp:ListItem>M</asp:ListItem>
                                    <asp:ListItem>L</asp:ListItem>
                                    <asp:ListItem>XL</asp:ListItem>
                                    <asp:ListItem>XXL</asp:ListItem>
                                    <asp:ListItem>XXXL</asp:ListItem>
                                </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="label-column">
                        <!-- Empty cell to maintain the column structure -->
                    </td>
                    <td class="input-column">
                        <div class="form-actions">
                            <asp:Button ID="Btn_create" runat="server" Text="Create" CssClass="btn btn-create" OnClick="Btn_create_Click" />
                            <asp:HyperLink ID="HyperLinkManageSphere" runat="server" NavigateUrl="~/ManageUserSphere.aspx" CssClass="btn btn-manage">View Glass Records</asp:HyperLink>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
<script>
    document.addEventListener('DOMContentLoaded', function () {
        var config = JSON.parse('<%= Server.HtmlDecode(Request.QueryString["sceneSetupCode"]) %>') || { x: 1, y: 1, z: 1 };
        var totalScaleChange = Math.abs(config.x - 1) + Math.abs(config.y - 1) + Math.abs(config.z - 1);
        var priceChange = totalScaleChange * 10; // For every 0.01 change in scale, add $0.10
        var basePrice = 10; // Assuming $10 is the base price for the 1:1:1 scale
        var totalPrice = basePrice + priceChange;

        // Update the Estimated Price TextBox
        var priceTextBox = document.getElementById('<%= tb_SpherePrice.ClientID %>');
            if (priceTextBox) {
                priceTextBox.value = totalPrice.toFixed(2); // Format to 2 decimal places
            }

        // Scene setup
        var scene = new THREE.Scene();
        var camera = new THREE.PerspectiveCamera(75, 300 / 225, 0.1, 1000); // Maintain the same aspect ratio
        var renderer = new THREE.WebGLRenderer({ antialias: true });
        renderer.setSize(300, 225); // Same size as in SphereMaker
        document.getElementById('threejs-read-container').appendChild(renderer.domElement);

        // Sphere setup
        var geometry = new THREE.SphereGeometry(1, 32, 32);
        var material = new THREE.MeshPhongMaterial({ color: 0xFFFFFF, transparent: true, opacity: 0.8, refractionRatio: 0.95 });
        var sphere = new THREE.Mesh(geometry, material);
        scene.add(sphere);

        // Set the scale based on the configuration
        sphere.scale.set(config.x, config.y, config.z);

        // Light setup
        var light = new THREE.PointLight(0xFFFFFF, 1, 1000);
        light.position.set(2, 2, 2);
        scene.add(light);

        // Camera position
        camera.position.z = 5;

        // Render the scene once without an animation loop to create a static image
        renderer.render(scene, camera);
    });
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
