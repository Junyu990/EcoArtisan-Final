<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminSphere.aspx.cs" Inherits="EcoArtisanFinal.AdminSphere" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
          <!-- Link to the stylesheet for the page -->
    <link href="<%= ResolveUrl("~/Content/ReadSphere.css") %>?v<%=DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/three.js/r128/three.min.js"></script>
    <style>
        #threejs-read-container {
            width: 600px; /* Adjust as needed */
            height: 400px; /* Adjust as needed */
            margin: auto;
        }
    </style>
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
                <asp:Button ID="Btn_read" runat="server"  Text="Read" CssClass="btn btn-read" OnClick="Btn_read_Click" />
            </div>
            <div class="form-group">
                <label for="tb_UserID">UserID</label>
                <asp:TextBox ID="tb_UserID" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="tb_SphereImage">Image</label>
                <asp:TextBox ID="tb_SphereImage" runat="server" Height="22px" CssClass="form-control" Readonly="true"></asp:TextBox>
                <div class="centered-container">
                    <div id="threejs-read-container">
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label for="tb_SphereDesc">Description</label>
                   <asp:TextBox ID="tb_SphereDesc" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="tb_SpherePrice">Estimated Price</label>
                <asp:TextBox ID="tb_SpherePrice" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                <asp:HiddenField ID="hf_SpherePrice" runat="server" />
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
                <asp:Button ID="Btn_update" runat="server"  Text="Update" CssClass="btn btn-update" OnClick="Btn_update_Click" />
            </div>
        </div>
    </div>
<script>
    document.addEventListener('DOMContentLoaded', function () {
        var container = document.getElementById('threejs-read-container');

        // Renderer
        var renderer = new THREE.WebGLRenderer({ antialias: true });
        renderer.setClearColor(0x000000); // Set background color to black
        renderer.setSize(container.offsetWidth, container.offsetHeight);
        container.innerHTML = ""; // Clear any existing content
        container.appendChild(renderer.domElement);

        // Scene
        var scene = new THREE.Scene();

        // Camera
        var aspectRatio = container.offsetWidth / container.offsetHeight;
        var camera = new THREE.PerspectiveCamera(75, aspectRatio, 0.1, 1000);
        camera.position.z = 5;

        // Sphere
        var geometry = new THREE.SphereGeometry(1, 32, 32); // Sphere radius set to 1
        var material = new THREE.MeshPhongMaterial({
            color: 0xFFFFFF,
            specular: 0x050505, // Specular color to add shiny effect
            shininess: 100 // Shininess for specular highlights
        });

        var sphere = new THREE.Mesh(geometry, material);
        scene.add(sphere);

        // Decode and parse the configuration
        var configString = document.getElementById('<%= tb_SphereImage.ClientID %>').value;
        var config = JSON.parse(decodeHtml(configString));

        // Apply the configuration to the sphere's scale
        sphere.scale.x = config.x;
        sphere.scale.y = config.y;
        sphere.scale.z = config.z;

        // Lighting
        var ambientLight = new THREE.AmbientLight(0x444444);
        scene.add(ambientLight);

        var pointLight = new THREE.PointLight(0xFFFFFF, 1, 100);
        pointLight.position.set(5, 5, 5); // Adjust light position to create the desired gradient effect
        scene.add(pointLight);

        // Render Loop
        function animate() {
            requestAnimationFrame(animate);
            renderer.render(scene, camera);
        }

        animate();
    });

    // Helper function to decode HTML entities
    function decodeHtml(html) {
        var txt = document.createElement('textarea');
        txt.innerHTML = html;
        return txt.value;
    }

</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
