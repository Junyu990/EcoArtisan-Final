<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SphereMaker.aspx.cs" Inherits="EcoArtisanFinal.SphereMaker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/three.js/r128/three.min.js"></script>
    <style>
        body, html {
            height: 100%;
            margin: 0;
        }
        #threejs-container {
            width: 800px;
            height: 600px; /* Fixed height for consistent aspect ratio */
            margin-left: auto;
            margin-right: auto;
            margin-bottom: 50px; /* Adjust margin as needed */
        }
        /* Ensure that the footer stays at the bottom */
        .footer {
            position: absolute;
            bottom: 0;
            width: 100%;
            /* Set the fixed height for your footer */
            height: 50px; /* Adjust as needed */
        }
        #infoButton {
            position: fixed; /* or absolute, depending on your layout */
            top: 20px; /* Adjust as needed */
            right: 20px; /* Adjust as needed */
            width: 50px; /* Adjust as needed */
            height: 50px; /* Adjust as needed */
            border-radius: 50%;
            font-weight: bold;
            font-size: 24px;
            cursor: pointer;
            z-index: 100;
            margin-top: 5rem;
        }

        #tooltip {
            position: fixed; /* or absolute */
            top: 80px; /* Adjust based on button position */
            right: 20px; /* Adjust to match button's right position */
            width: 300px; /* Adjust as needed */
            padding: 10px;
            background-color: white;
            border: 1px solid #ddd;
            border-radius: 5px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.2);
            z-index: 100;
        }

        #tooltip h4 {
            margin-top: 0;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <button id="infoButton" type="button">!</button>
    <div id="tooltip" style="display:none;">
        <h4>Pricing Details:</h4>
        <p>Our service offers tiered pricing to accommodate everyone from hobbyists to professionals:</p>
        <ul>
            <li><strong>Price:</strong> Prices will charge based off deviation from the original model</li>
        </ul>
        <h4>Navigation Instructions:</h4>
        <p>Our platform is designed for ease of use with intuitive controls:</p>
        <ul>
            <li><strong>Rotate:</strong> Click and drag with your mouse or use touch gestures to rotate the model in any direction.</li>
            <li><strong>Zoom:</strong> Scroll up or down with your mouse wheel, or use pinch gestures on touch devices, to zoom in or out.</li>
            <li><strong>Pan:</strong> Hold the right mouse button and drag to move the model position, or use two fingers on touch devices.</li>
        </ul>
        <p>For more detailed guidance, visit our help center or contact support.</p>
    </div>

    <!-- Container for the three.js visualization -->
    <div id="threejs-container"></div>

    <!-- Button to save the template -->
    <button id="btnSaveCylinder" type="button">Save Template</button>


<script>
    // Scene, camera, and renderer setup
    const scene = new THREE.Scene();
    const camera = new THREE.PerspectiveCamera(75, 800 / 600, 0.1, 1000);
    const renderer = new THREE.WebGLRenderer({ antialias: true });
    renderer.setSize(800, 600); // Match the size of the threejs-container
    document.getElementById('threejs-container').appendChild(renderer.domElement);

    // Resizing the renderer when window is resized
    window.addEventListener('resize', () => {
        const newWidth = window.innerWidth;
        const newHeight = window.innerHeight;
        renderer.setSize(newWidth, newHeight);
        camera.aspect = newWidth / newHeight;
        camera.updateProjectionMatrix();
    });

    // Basic sphere setup
    const geometry = new THREE.SphereGeometry(1, 32, 32);
    const material = new THREE.MeshPhongMaterial({ color: 0xFFFFFF, transparent: true, opacity: 0.8, refractionRatio: 0.95 });
    const sphere = new THREE.Mesh(geometry, material);
    scene.add(sphere);

    // Light setup for better appearance
    const light = new THREE.PointLight(0xFFFFFF, 1, 1000);
    light.position.set(2, 2, 2);
    scene.add(light);

    // Set initial camera position
    camera.position.z = 5;

    // Animation & Rendering loop
    function animate() {
        requestAnimationFrame(animate);
        renderer.render(scene, camera);
    }
    animate();

    // Mouse interaction to stretch the sphere
    let isDragging = false;
    let previousMousePosition = { x: 0, y: 0 };

    document.addEventListener('mousedown', (e) => {
        isDragging = true;
        // Set the starting mouse position when starting a new drag
        previousMousePosition = { x: e.clientX, y: e.clientY };
    });

    document.addEventListener('mouseup', () => isDragging = false);

    document.addEventListener('mousemove', (e) => {
        if (!isDragging) return;

        const deltaX = e.clientX - previousMousePosition.x;
        const deltaY = e.clientY - previousMousePosition.y;

        // Adjusting the sphere's scale based on mouse movement
        sphere.scale.x += deltaX * 0.01;
        sphere.scale.y += deltaY * 0.01;

        // Update the previous mouse position for the next movement
        previousMousePosition = { x: e.clientX, y: e.clientY };
    });

    document.getElementById('btnSaveCylinder').addEventListener('click', function (e) {
        e.preventDefault();

        const scaleData = {
            x: sphere.scale.x,
            y: sphere.scale.y,
            z: sphere.scale.z
        };

        const jsonString = JSON.stringify(scaleData);
        console.log(jsonString);

        const encodedData = encodeURIComponent(jsonString);
        window.location.href = `CreateUserSphere.aspx?sceneSetupCode=${encodedData}`;

    });

    const infoButton = document.getElementById('infoButton');
    const tooltip = document.getElementById('tooltip');

    infoButton.addEventListener('mouseover', function () {
        tooltip.style.display = 'block';
    });

    infoButton.addEventListener('mouseout', function () {
        tooltip.style.display = 'none';
    });


</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
