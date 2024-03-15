<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReadReview.aspx.cs" Inherits="EcoArtisanFinal.ReadReview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
       <script src="https://kit.fontawesome.com/db6fc7cda2.js" crossorigin="anonymous"></script>
     <link href="<%= ResolveUrl("~/Content/Review.css") %>?v<%=DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />
        <link href="<%= ResolveUrl("~/Content/Product.css") %>?v<%=DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="noReviews">
        <asp:Label CssClass="lbl_reviewTrue center" ID="lbl_reviewTrue" runat="server" Text=""></asp:Label><br />

        <asp:Button CssClass="btn_ViewProd" ID="btn_ViewProd" runat="server" Text="View Products!" OnClick="btn_ViewProd_Click"/>
    </div>
     <div class="reviewAll">

    <asp:Repeater ID="Repeater_Reviews" runat="server" OnItemCommand="Repeater_Reviews_ItemCommand" >
    <ItemTemplate>
        <div class="mainCard">
            <div class="cardLeft">
                <h4>Review ID: <span><%# Eval("ReviewID") %></span></h4>
                <div class="img_holder">
                    <img src='../Content/Products/<%# Eval("ProdImg") %>' alt='<%# Eval("ProdName") %>'>
                </div>
                <span>Product ID: <%# Eval("ProdID") %></span> <br />
                 <span>Product Name: <%# Eval("ProdName") %></span><br />
                <asp:LinkButton CommandName="View" CommandArgument='<%# Eval("ReviewID") %>' Text="Click Here To View Product" runat="server" />
            </div>
            
            <div class="prodD">
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
                Description: <br /><span><%# Eval("ReviewDesc") %></span><br />

                <div class="userReview">
                    <span class="review_cmt">Review Was Created On <%# Eval("ReviewDateTime") %></span><br />
                    <span class="review_cmt">Review Was Last Edited On <%# Eval("ReviewLastEdit") %></span> <br />

                    <asp:LinkButton CommandName="Edit" CommandArgument='<%# Eval("ReviewID") %>' Text="Edit" runat="server" />
                    <asp:LinkButton CommandName="Delete" CommandArgument='<%# Eval("ReviewID") %>' Text="Delete" runat="server" />
                </div>
                
            </div>
            
        </div>

       <script>
           $(document).ready(function () {
               // Function to set the stars based on the rating value
               function setRatingStars(reviewCard, rating) {
                   $(reviewCard).find(".<%# Eval("ReviewID") %>ratingStar").removeClass("fa-solid").addClass("fa-regular"); // Reset stars
                   $(reviewCard).find(".<%# Eval("ReviewID") %>ratingStar[data-value='" + rating + "']").prevAll().addBack().removeClass("fa-regular").addClass("fa-solid"); // Set stars up to rating
        }

        // Loop through each review card and set its rating stars
        $(".mainCard").each(function () {
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
    <script>
        $(document).ready(function () {
            var wishlistTrue = $('.lbl_reviewTrue').text().trim();
            if (wishlistTrue === "No Reviews...") {
                $('.reviewAll').hide();
            } else {
                $('.noReviews').hide();
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
