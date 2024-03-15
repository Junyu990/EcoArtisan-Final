<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="EcoArtisanFinal.ProductDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <script src="https://kit.fontawesome.com/db6fc7cda2.js" crossorigin="anonymous"></script>
    <link href="/Content/Product.css?v=<%=DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/Content/Review.css") %>?v<%=DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />

    <script src="Content/Product.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="mainContent">
        <div class="prodImage">
            <asp:Image ID="img_prodImg" runat="server" Height="583px" Width="623px" />
        </div>
        <div class="prodListing">
            <span class="prodQty">Product ID: 
            <asp:Label ID="lbl_displayProdID" runat="server" Text=""></asp:Label>
            </span>
            <div class="prodMain">
                <h2><asp:Label ID="lbl_displayProdName" runat="server" Text=""></asp:Label></h2>
                <asp:Label ID="lbl_displayProdDesc" runat="server" Text=""></asp:Label>
            </div>

            <div class="prodPrice">
                <h3>$<asp:Label ID="lbl_displayProdPrice" runat="server" Text=""></asp:Label></h3>
            </div>

            <span class="prodImpact">By buying this, you save 
            <asp:Label ID="lbl_displayProdImpact" runat="server" Text=""></asp:Label>
            bowls of rice today!
            </span>
            <br />

            <span class="prodQty">
            <asp:Label ID="lbl_displayProdQty" runat="server" Text=""></asp:Label> left in stock.
            </span>
            
            <div class="cartBtn" style="display:flex;">
                <button class="addCartBtn">Add to Cart</button>
                <div style="margin-top:25px; margin-left:10px;">
                    <asp:LinkButton CssClass="heartProdDetails fa-regular fa-heart" ID="link_Wishlist" runat="server" CommandName="AddWish" OnCommand="link_Wishlist_Command"></asp:LinkButton>
                    <asp:Label CssClass="lbl_wishCheck" ID="lbl_wishCheck" runat="server" Text="" Visible="False"></asp:Label>
                </div>
                
            </div>

       

        </div>

    </div>
    <div class="reviews">
        <h4>Reviews for <asp:Label ID="lbl_displayName" runat="server" Text=""></asp:Label></h4>
          <div class="reviewAll">
              <div class="TotalReview">
                  <div class="reviewCount">
                      Average Rating:
                      <h2>
                         <asp:Label CssClass="lbl_avgReview" ID="lbl_avgReview" runat="server" Text=""></asp:Label>
                      </h2>
                      <h4>
                          <span class="text-warning">
                            <i class="avgratingStar fa-regular fa-star" data-value="1"></i>
                            <i class="avgratingStar fa-regular fa-star" data-value="2"></i>
                            <i class="avgratingStar fa-regular fa-star" data-value="3"></i>
                            <i class="avgratingStar fa-regular fa-star" data-value="4"></i>
                            <i class="avgratingStar fa-regular fa-star" data-value="5"></i>
                        </span>
                      </h4>
                      
                      (<asp:Label CssClass="lbl_reviewCount" ID="lbl_reviewCount" runat="server" Text=""></asp:Label>)
                      <script>
                          // Get the average rating label element
                          var avgRatingLabel = $('.lbl_avgReview').text();

                          var roundedRating = Math.floor(avgRatingLabel);
                          console.log(roundedRating);

                          // Function to set the stars based on the rating value
                          function setRatingStars(rating) {
                              let i = 1;
                              while (i <= rating) {
                                  //console.log(i);
                                  $(".avgratingStar[data-value='" + i + "']").addClass("fa-solid").removeClass("fa-regular");
                                  i++;
                              }

                          }
                          setRatingStars(roundedRating);
                      </script>
                  </div>  
                  
                  <div class="reviewStarTotal">
                    <div class="barContainer">
                        <asp:Label ID="lbl_starratings" runat="server" Text=""></asp:Label>
                        <!-- Empty bars representing total star ratings -->
                        <div class="starRating">
                            <span data-rating="5">5</span>
                            <div class="bars">
                                <div class="bar">
                                  <div class="coloured-bar rated5bar" id="colored5star"></div>
                                </div>
                                ( <asp:Label CssClass="lbl_5starCount" ID="lbl_5starCount" runat="server" Text=""></asp:Label>)
                            </div>
                        </div>
                        <div class="starRating">
                            <span data-rating="4">4</span>
                            <div class="bars">
                                <div class="bar">
                                  <div class="coloured-bar rated4bar" id="colored4star"></div>
                                </div>
                                ( <asp:Label CssClass="lbl_4starCount" ID="lbl_4starCount" runat="server" Text=""></asp:Label>)

                            </div>
                        </div>
                        
                        <div class="starRating">
                             <span data-rating="3">3</span>
                            <div class="bars">
                               <div class="bar">
                                  <div class="coloured-bar rated3bar" id="colored3star"></div>
                                </div>
                                ( <asp:Label CssClass="lbl_3starCount" ID="lbl_3starCount" runat="server" Text=""></asp:Label>)
                            </div>               
                        </div>
                       
                        <div class="starRating">
                             <span data-rating="2">2</span>
                            <div class="bars">
                                <div class="bar">
                                  <div class="coloured-bar rated2bar" id="colored2star"></div>
                                </div>
                                ( <asp:Label CssClass="lbl_2starCount" ID="lbl_2starCount" runat="server" Text=""></asp:Label>)
                            </div>
                        </div>
                       
                        <div class="starRating">
                            <span data-rating="1">1</span>
                            <div class="bars">
                                <div class="bar">
                                  <div class="coloured-bar rated1bar" id="colored1star"></div>
                                </div>
                                ( <asp:Label CssClass="lbl_1starCount" ID="lbl_1starCount" runat="server" Text=""></asp:Label>)
                            </div>                       
                        </div>
                        
                        <div class="starRating">
                            <span data-rating="0">0</span>
                            <div class="bars">
                                <div class="bar">
                                  <div class="coloured-bar rated0bar" id="colored0star"></div>
                                </div>
                                ( <asp:Label CssClass="lbl_0starCount" ID="lbl_0starCount" runat="server" Text=""></asp:Label>)
                            </div>
                            
                        </div>
                        
                        <script>
                            $(document).ready(function () {
                                // Get the total review count
                                var starCount = "";
                                // Function to set the star rating bars based on the count
                                function setStarRatings() {
                                    var totalReviewCount = parseInt($('.lbl_reviewCount').text());
                                    var starList = [];
                                    starList.push(parseInt($.trim($('.lbl_5starCount').text())));
                                    starList.push(parseInt($('.lbl_4starCount').text()));
                                    starList.push(parseInt($('.lbl_3starCount').text()));
                                    starList.push(parseInt($('.lbl_2starCount').text()));
                                    starList.push(parseInt($('.lbl_1starCount').text()));
                                    starList.push(parseInt($('.lbl_0starCount').text()));

                                    console.log(starList);
                                    starList.reverse()


                                    for (var i = 0; i < starList.length; i++) {
                                        console.log('i', i);

                                        var item = starList[i];

                                        console.log('item', item);
                                        var proportion = item / totalReviewCount;
                                        console.log('proportion', proportion);

                                        // Set the width of the colored bar
                                        $('#colored' + i + 'star').css('width', (proportion * 100) + '%');
                                    }


                                }


                                // Call the function to set star ratings when the document is ready
                                setStarRatings();
                            });

                        </script>
                    </div>
                    </div>
               </div>
              <asp:Label CssClass="lbl_reviewTrue" ID="lbl_reviewTrue" runat="server" Text=""></asp:Label>
            <div class="reviewCards">
                 <div class="review-by-category">
                        <ul id="category-filters" class="d-flex justify-content-center">
                              <li data-filter="*" class="filter-active">
                              <div class="review-by-category-card">
                                <span><i class="fa-solid fa-star"></i></span><span>All Reviews</span>
                              </div>
                          </li>
                          <li data-filter=".5" class="filter-active">
                              <div class="review-by-category-card">
                                <span><i class="fa-solid fa-star"></i></span><span>5-Stars</span>
                              </div>
                          </li>
                            <li data-filter=".4" class="filter-active">
                              <div class="review-by-category-card">
                                <span><i class="fa-solid fa-star"></i></span><span>4-Stars</span>
                              </div>
                          </li>
                            <li data-filter=".3" class="filter-active">
                              <div class="review-by-category-card">
                                <span><i class="fa-solid fa-star"></i></span><span>3-Stars</span>
                              </div>
                          </li>
                            <li data-filter=".2" class="filter-active">
                              <div class="review-by-category-card">
                                <span><i class="fa-solid fa-star"></i></span><span>2-Stars</span>
                              </div>
                          </li>
                            <li data-filter=".1" class="filter-active">
                              <div class="review-by-category-card">
                                <span><i class="fa-solid fa-star"></i></span><span>1-Stars</span>
                              </div>
                          </li>
                            <li data-filter=".0" class="filter-active">
                              <div class="review-by-category-card">
                                <span><i class="fa-solid fa-star"></i></span><span>0-Stars</span>
                              </div>
                          </li>
                         
                        </ul>
                    </div>
        
                <div class="reviewsContain">
                    <asp:Repeater ID="UserReview_Repeater" runat="server" OnItemCommand="Repeater_Reviews_ItemCommand">
                    <ItemTemplate>
                        <div class="reviewCard <%# Eval("ReviewRating") %>">
                            <h4>Your Review ID: <span><%# Eval("ReviewID") %></span></h4>
                            Created By: <span><%# Eval("UserName") %></span><br />
                            Product Name: <span><%# Eval("ProdName") %></span><br />


                            Star Rating (<span class="ratingNo"><%# Eval("ReviewRating") %></span>/5): <br />
                            <span class="text-warning">
                                <i class="<%# Eval("ReviewID") %>ratingStar fa-regular fa-star" data-value="1"></i>
                                <i class="<%# Eval("ReviewID") %>ratingStar fa-regular fa-star" data-value="2"></i>
                                <i class="<%# Eval("ReviewID") %>ratingStar fa-regular fa-star" data-value="3"></i>
                                <i class="<%# Eval("ReviewID") %>ratingStar fa-regular fa-star" data-value="4"></i>
                                <i class="<%# Eval("ReviewID") %>ratingStar fa-regular fa-star" data-value="5"></i>
                            </span>

                            <div class="ratingValue"></div> <!-- Placeholder for the rating value -->
                            <br />
                            Description: <span><%# Eval("ReviewDesc") %></span><br />
                            <span class="review_cmt">Review Was Created On <%# Eval("ReviewDateTime") %></span><br />
                            <span class="review_cmt">Review Was Last Edited On <%# Eval("ReviewLastEdit") %></span> <br />

            
                            <asp:LinkButton CssClass="reviewAction" CommandName="Edit" CommandArgument='<%# Eval("ReviewID") %>' Text="Edit" runat="server"/>
                            <asp:LinkButton CssClass="reviewAction" CommandName="Delete" CommandArgument='<%# Eval("ReviewID") %>' Text="Delete" runat="server"/>

                           <script>
                               $(document).ready(function () {
                                   // Function to set the stars based on the rating value
                                   function setRatingStars(reviewCard, rating) {
                                       $(reviewCard).find(".<%# Eval("ReviewID") %>ratingStar").removeClass("fa-solid").addClass("fa-regular"); // Reset stars
                                       $(reviewCard).find(".<%# Eval("ReviewID") %>ratingStar[data-value='" + rating + "']").prevAll().addBack().removeClass("fa-regular").addClass("fa-solid"); // Set stars up to rating
                                    }

                                    // Loop through each review card and set its rating stars
                                    $(".reviewCard").each(function () {
                                        var rating = <%# Eval("ReviewRating") %>;
                                        console.log(rating);
                                        setRatingStars(this, rating); // Call setRatingStars for the current review card
                                    });
                               });
                           </script>
                            </div>
                    </ItemTemplate>
                </asp:Repeater>

                <asp:Repeater ID="Repeater_Reviews" runat="server">
                    <ItemTemplate>
                        <div class="reviewCard <%# Eval("ReviewRating") %>">
                            <h4>Review ID: <span><%# Eval("ReviewID") %></span></h4>
                            Created By: <span><%# Eval("UserName") %></span><br />
                            Product Name: <span><%# Eval("ProdName") %></span><br />


                            Star Rating (<span class="ratingNo"><%# Eval("ReviewRating") %></span>/5): <br />
                    <span class="text-warning">
                                <i class="<%# Eval("ReviewID") %>ratingStar fa-regular fa-star" data-value="1"></i>
                                <i class="<%# Eval("ReviewID") %>ratingStar fa-regular fa-star" data-value="2"></i>
                                <i class="<%# Eval("ReviewID") %>ratingStar fa-regular fa-star" data-value="3"></i>
                                <i class="<%# Eval("ReviewID") %>ratingStar fa-regular fa-star" data-value="4"></i>
                                <i class="<%# Eval("ReviewID") %>ratingStar fa-regular fa-star" data-value="5"></i>
                            </span>

                            <div class="ratingValue"></div> <!-- Placeholder for the rating value -->
                            <br />
                            Description: <span><%# Eval("ReviewDesc") %></span><br />
                            <span class="review_cmt">Review Was Created On <%# Eval("ReviewDateTime") %></span><br />
                            <span class="review_cmt">Review Was Last Edited On <%# Eval("ReviewLastEdit") %></span> <br />


                           <script>
                               $(document).ready(function () {
                                   // Function to set the stars based on the rating value
                                   function setRatingStars(reviewCard, rating) {
                                       $(reviewCard).find(".<%# Eval("ReviewID") %>ratingStar").removeClass("fa-solid").addClass("fa-regular"); // Reset stars
                                       $(reviewCard).find(".<%# Eval("ReviewID") %>ratingStar[data-value='" + rating + "']").prevAll().addBack().removeClass("fa-regular").addClass("fa-solid"); // Set stars up to rating
                                    }

                                    // Loop through each review card and set its rating stars
                                    $(".reviewCard").each(function () {
                                        var rating = <%# Eval("ReviewRating") %>;
                                        console.log(rating);
                                        setRatingStars(this, rating); // Call setRatingStars for the current review card
                                    });
                               });
                           </script>
                            </div>
                        </ItemTemplate>
                </asp:Repeater>
                </div>
                
            </div>
              <script>
                  $(document).ready(function () {
                      var reviewTrue = $('.lbl_reviewTrue').text().trim();
                      console.log(reviewTrue);
                      if (reviewTrue === "No reviews available.") {
                          $('.reviewCards').hide();
                      }
                  });
              </script>
             
        </div>
    </div>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            $('.addCartBtn').on('click', function () {
                // Use URLSearchParams to work with the query string
                var urlParams = new URLSearchParams(window.location.search);
                var prodId = urlParams.get('prodID'); // Get 'prodID' parameter from URL

                if (!prodId) {
                    alert("Product ID not found in URL.");
                    return; // Exit if no product ID is found
                }

                $.ajax({
                    type: 'POST',
                    url: 'Products.aspx/AddToCart',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ productId: parseInt(prodId, 10) }), // Ensure parsing as integer
                    dataType: "json",
                    success: function (response) {
                        // Access the 'd' property and parse it
                        var parsedData = JSON.parse(response.d);

                        // Log the response details
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
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
