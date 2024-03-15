<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EcoArtisanFinal._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <link rel="stylesheet" type="text/css" href="../Content/home.css?v=<%= DateTime.Now.Ticks %>"/>
     <script src="https://kit.fontawesome.com/db6fc7cda2.js" crossorigin="anonymous"></script>
    <link href="<%= ResolveUrl("~/Content/Product.css") %>?v<%=DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />
     <link href="<%= ResolveUrl("~/Content/AdminProduct.css") %>?v<%=DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />


<div class="slideshow-container">

<div class="mySlides fade">
  <img src="../Content/Banners/ecoartisan_banner.png" style="width:100%">
</div>

<div class="mySlides fade">
  <img src="../Content/Banners/ecoartisan_banner2.png" style="width:100%">
</div>

<div class="mySlides fade">
  <img src="../Content/Banners/ecoartisan_banner4.png" style="width:100%">
</div>

</div>
<br>

<div style="text-align:center">
  <span class="dot"></span> 
  <span class="dot"></span> 
  <span class="dot"></span> 
</div>

<!-- Popup container -->
<div id="popup" class="popup-container">
    <!-- Close button -->
    <span class="close-btn" onclick="hidePopup()">&times;</span>
    <!-- Image element with an ID to target it in JavaScript -->
    <img id="changingImage" src="../Content/Banners/hotsale1.png" class="ad1">
</div>

<div class="container-fluid">
  <div class="row">
    <div class="col-md-2">
      <div class="product">
          <a href="../Products">
        <img src="../Content/Products/highball.jpg" class="prod-cat">
        <p>Products</p>
          </a>
      </div>
    </div>
    <div class="col-md-2">
      <div class="product">
          <a href="../SphereMaker">
        <img src="../Content/Images/Sphere.png" class="prod-cat">
        <p>YourArtisan</p>
          </a>
      </div>
    </div>
    <div class="col-md-2">
      <div class="product">
          <a href="../Rewards">
        <img src="../Content/Products/highball.jpg" class="prod-cat">
            <% if (Session["UserID"] == null) { %>
                <p>Rewards <br />Log in to check coins!</p>
            <% } else { %>
                <p>Rewards <br /><asp:Label ID="pointsLabel" runat="server"></asp:Label> <img style="height:2rem;width:2rem;" src="../Content/Images/ecocoin.png" alt=""></p>
            <% } %>
          </a>
      </div>
    </div>
    <div class="col-md-2">
      <div class="product">
          <a href="../Products">
        <img src="../Content/Products/highball.jpg" class="prod-cat">
        <p>Make a Review!</p>
          </a>
      </div>
    </div>
    <div class="col-md-2">
      <div class="product">
          <a href="../Products">
        <img src="../Content/Products/highball.jpg" class="prod-cat">
        <p>Product 5</p>
          </a>
      </div>
    </div>
    <div class="col-md-2">
      <div class="product">
          <a href="../Products">
        <img src="../Content/Products/highball.jpg" class="prod-cat">
        <p>Product 6</p>
          </a>
      </div>
    </div>
  </div>
</div>


    <div class="cardcontainer">
        <asp:Repeater ID="repeater_Product" runat="server" OnItemCommand="repeater_Product_ItemCommand" OnItemDataBound="repeater_Product_ItemDataBound">
        <ItemTemplate>
                
                    <div class="card">
                        <a href='<%# "ProductDetails.aspx?prodID=" + Eval("ProdID") %>'>
                            <img src='../Content/Products/<%# Eval("ProdImg") %>' alt='<%# Eval("ProdName") %>'>
                            <div class="card-title" style="text-align:center;"><%# Eval("ProdName") %></div>
                            <div class="price" style="text-align:center;">$<%# Eval("ProdPrice") %></div>
                            <a href='<%# "ProductDetails.aspx?prodID=" + Eval("ProdID") %>' style="text-align:center;">More Information</a>
                                <div class="actIcon_div">
                                   
                                    <div class="cardbtn" data-prod-id='<%# Eval("ProdID") %>'><i class="icon fas fa-shopping-cart"></i> Add to Cart</div>
                                    <div style="margin-top: 10px;margin-left: 3px;">
                                        <asp:LinkButton CssClass="heartProd fa-regular fa-heart" ID="linkprod_Wishlist" runat="server" CommandName="AddWish" CommandArgument='<%# Eval("ProdID") %>' ></asp:LinkButton>
                                    </div>
                               </div>
                         </a>
                    &nbsp;&nbsp;&nbsp;</div>
    </ItemTemplate>
    </asp:Repeater>






    <!-- Add more product cards as needed -->

</div>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script>
    let slideIndex = 0;
    showSlides();

    function showSlides() {
        let i;
        let slides = document.getElementsByClassName("mySlides");
        let dots = document.getElementsByClassName("dot");
        for (i = 0; i < slides.length; i++) {
            slides[i].style.display = "none";
        }
        slideIndex++;
        if (slideIndex > slides.length) { slideIndex = 1 }
        for (i = 0; i < dots.length; i++) {
            dots[i].className = dots[i].className.replace(" active", "");
        }
        slides[slideIndex - 1].style.display = "block";
        dots[slideIndex - 1].className += " active";
        setTimeout(showSlides, 4000); // Change image every 2 seconds
    }

    // Array of image URLs
    const imageUrls = [
        '../Content/Banners/hotsale1.png',
        '../Content/Banners/hotsale2.png',
        // Add more image URLs as needed
    ];

    // Get the image element by ID
    const changingImage = document.getElementById('changingImage');

    // Counter to keep track of the current image index
    let currentIndex = 0;

    // Function to change the image every second
    function changeImage() {
        // Set the image source to the next URL in the array
        changingImage.src = imageUrls[currentIndex];

        // Increment the counter and reset it if it exceeds the array length
        currentIndex = (currentIndex + 1) % imageUrls.length;
    }

    // Call the changeImage function every second (1000 milliseconds)
    setInterval(changeImage, 1000);

    function showPopup() {
        document.getElementById('popup').style.display = 'block';
    }

    // Function to hide the popup
    function hidePopup() {
        document.getElementById('popup').style.display = 'none';
    }

    // Function to randomly show the popup after a delay
    function showPopupRandomly() {
        // Set a random delay between 1 and 20 seconds (adjust as needed)
        const randomDelay = Math.floor(Math.random() * (20000 - 5000 + 1)) + 1000;

        // Show the popup after the random delay
        setTimeout(showPopup, randomDelay);
    }

    // Call the function to show the popup randomly when the page loads
    showPopupRandomly();

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
