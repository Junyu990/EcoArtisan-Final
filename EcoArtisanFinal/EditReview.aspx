﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditReview.aspx.cs" Inherits="EcoArtisanFinal.EditReview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <script src="https://kit.fontawesome.com/db6fc7cda2.js" crossorigin="anonymous"></script>
        <link href="/Content/Review.css?v=<%=DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />s
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="reviewForm">
        <h2>Edit Your Review!</h2>
    <div class="prodDetails">
        <div class="img_holder">
            <asp:Image ID="img_ProdImg" runat="server" />
        </div>
    <h4>You got <asp:Label CssClass="lbl_prodName" ID="lbl_prodName" runat="server"></asp:Label></h4>
    </div>
    
        <div class="reviewDetails">
            <table class="nav-justified">

        <tr>
            <td>

                    <h4>Rating</h4>
                    <span class="text-warning">
                        <i class="ratingStar fa-regular fa-star" data-value="1"></i>
                        <i class="ratingStar fa-regular fa-star" data-value="2"></i>
                        <i class="ratingStar fa-regular fa-star" data-value="3"></i>
                        <i class="ratingStar fa-regular fa-star" data-value="4"></i>
                        <i class="ratingStar fa-regular fa-star" data-value="5"></i>

                    </span>

                    <asp:Label CssClass="lbl_ratingValue" ID="lbl_ratingValue" runat="server" Text="0" Visible="True"></asp:Label>/5 Stars
                    &nbsp;
                    <button id="btn_StarReset">Reset Stars to 0</button>

                    <input id="ratingsValue" name="Rating" type="hidden" />

                    <script>
                        // Function to reset stars to fa-regular
                        function resetStars() {
                            $(".ratingStar").addClass("fa-regular").removeClass("fa-solid");
                            $("#ratingsValue").val(""); // Reset the hidden input value
                        }

                        // Event handler for Reset Stars button click
                        $("#btn_StarReset").on("click", function (e) {
                            e.preventDefault();
                            resetStars();
                            $("#ratingsValue").val(0);
                            $(".lbl_ratingValue").text(0);
                        });

                        $(document).ready(function () {
                            // Function to set the stars based on the rating value
                            function setRatingStars(rating) {
                                let i = 1;
                                while (i <= rating) {
                                    //console.log(i);
                                    $(".ratingStar[data-value='" + i + "']").addClass("fa-solid").removeClass("fa-regular");
                                    i++;
                                }

                            }
                            var rating = document.getElementById('<%= lbl_ratingValue.ClientID %>').innerText.trim();
                            setRatingStars(parseInt(rating));
                            // 
                            $(".ratingStar").click(function () {
                                var starValue = $(this).attr("data-value");

                                $(".ratingStar").addClass("fa-regular").removeClass("fa-solid");
                                $(this).addClass("fa-solid").removeClass("fa-regular");
                                $(this).prevAll(".ratingStar").addClass("fa-solid").removeClass("fa-regular");

                                $(".ratingStar").off('mouseenter mouseleave');


                                $(".lbl_ratingValue").text(starValue);
                                $("#ratingsValue").val(starValue);
                            });
                        });
                    </script>


        </td >
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <h4>Description </h4>
                <div class="reviewDesc">
                    <asp:TextBox ID="tb_editreviewDesc" runat="server" Height="100px" Width="500px" BorderWidth="1" BorderColor="Black" BorderStyle="Solid"></asp:TextBox><br />
                    <asp:RequiredFieldValidator  ID="rfv_reviewDesc" runat="server" ControlToValidate="tb_editreviewDesc" ErrorMessage="This is a Required Field." ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
        </div>
    

        <div class="actionbtn">
            <asp:Button CssClass="editBtn" ID="btn_reviewUpdate" runat="server" Text="Update Review" OnClick="btn_reviewUpdate_Click" />
    
            <asp:Button CssClass="editBtn" ID="btn_reviewBack" runat="server" Text="Cancel" OnClick="btn_reviewBack_Click" />
        </div>
    



    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
