<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="EcoArtisanFinal.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Add CSS and JS links for styling and interactivity -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

    <style>
        /* Custom CSS for enhanced styling */
        .jumbotron {
            background-image: url('path/to/background-image.jpg');
            background-size: cover;
            color: white;
            text-align: center;
            padding: 100px 0;
        }
        .feature-icon {
            font-size: 3rem;
            margin-bottom: 20px;
        }
        .feature-description {
            margin-bottom: 50px;
        }
    </style>
     <div class="jumbotron" style="background-image: url('Content/Banners/ecoartisan_banner4.png');">
        <div class="container" style="color:black; background-color: rgba(240, 248, 255, 0.7);">
            <h1>Welcome to EcoArtisan</h1>
            <p class="lead">Transforming food waste into sustainable glassware</p>
        </div>
    </div>
    <!-- Features section with icons and descriptions -->
    <div class="container">
        <div class="row">
            <div class="col-md-4" style="text-align:center;">
                    <i class="fa fa-recycle feature-icon"  style="font-size:4rem;"></i>
                <h3>Environmental Sustainability</h3>
                <p class="feature-description">We are committed to reducing food waste and environmental degradation by transforming it into beautiful, recyclable glassware.</p>
            </div>
            <div class="col-md-4" style="text-align:center;">
                    <i class="fa fa-globe feature-icon"  style="font-size:4rem;"></i>
                <h3>Global Reach</h3>
                <p class="feature-description">Our aim is to become a global hub for glass manufacturing, serving customers worldwide through our advanced e-commerce platform.</p>
            </div>
            <div class="col-md-4" style="text-align:center;">
                    <i class="fa fa-users feature-icon" style="font-size:4rem;"></i>
                <h3>Inclusive Design</h3>
                <p class="feature-description">Our products cater to individuals with disabilities, offering customizable options to meet diverse needs.</p>
            </div>
        </div>
    </div>

    <!-- Call-to-action section -->
    <div class="container-fluid bg-dark text-white py-5">
        <div class="container text-center">
            <h2>Ready to join the eco-friendly movement?</h2>
            <p>Explore our collection of sustainable glassware today!</p>
            <a href="/Products" class="btn btn-primary btn-lg">Shop Now</a>
        </div>
    </div>
</asp:Content>
