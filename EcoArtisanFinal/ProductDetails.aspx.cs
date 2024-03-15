using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        ReviewWithProduct revProd = new ReviewWithProduct();
        Review rev = new Review();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || string.IsNullOrEmpty(Session["UserID"].ToString()))
            {
                string script = "alert('Please log in first.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                // Redirect the user to the login page if UserID is not set in the session
                Response.Redirect("/Login");
            }

            if (!IsPostBack)
            {
                bind();
                string userID = Session["UserID"].ToString();
                // Check if the query string parameter "ProdID" exists in the URL
                if (Request.QueryString["ProdID"] != null)
                {
                    // Retrieve the value of the "ProdID" parameter from the query string
                    string prodID = Request.QueryString["ProdID"];
                    lbl_displayProdID.Text = prodID;


                    Product prodD = new Product();

                    prodD = prodD.GetProduct(int.Parse(prodID));


                    if (prodD != null)
                    {
                        lbl_displayProdName.Text = prodD.ProdName;
                        lbl_displayProdDesc.Text = prodD.ProdDesc;
                        lbl_displayProdPrice.Text = prodD.ProdPrice.ToString();
                        lbl_displayProdQty.Text = prodD.ProdQty.ToString();
                        lbl_displayProdImpact.Text = prodD.ProdImpact.ToString();
                        lbl_displayName.Text = prodD.ProdName;

                        img_prodImg.ImageUrl = ResolveUrl("~/Content/Products/" + prodD.ProdImg);
                    }
                    else
                    {
                        lbl_displayProdID.Text = "not found!";
                    }


                    WishListWithProduct wish = new WishListWithProduct();
                    List<WishListWithProduct> userWishes = wish.getWishListByUser(int.Parse(userID));
                    foreach (WishListWithProduct product in userWishes)
                    {
                        int productID = product.ProdID;
                        if (productID == int.Parse(prodID))
                        {
                            link_Wishlist.CommandName = "RemoveWish";
                            lbl_wishCheck.Text = "True";
                        }
                    }

                    string lblWishCheckText = lbl_wishCheck.Text.Trim();
                    if (lblWishCheckText == "True")
                    {
                        link_Wishlist.CssClass = "heartProdDetails fa-solid fa-heart";
                        link_Wishlist.CommandName = "RemoveWish";
                    }
                    else
                    {
                        link_Wishlist.CssClass = "heartProdDetails fa-regular fa-heart";
                        link_Wishlist.CommandName = "AddWish";
                    }

                    // Call the function and store the result into a variable
                    Dictionary<string, double> totalReviewCounts = revProd.getRatingAverageAndRateValueForProduct(int.Parse(prodID));
                    Dictionary<string, double> starCount = new Dictionary<string, double>();

                    // You can now use totalReviewCounts dictionary as needed
                    // For example, you can loop through it and print the values
                    foreach (var kvp in totalReviewCounts)
                    {
                        string revprodID = kvp.Key;
                        if (revprodID == "Count")
                        {
                            lbl_reviewCount.Text = totalReviewCounts[revprodID].ToString();
                        }
                        if (revprodID == "Avg")
                        {
                            lbl_avgReview.Text = totalReviewCounts[revprodID].ToString();
                        }
                        if (revprodID == "5")
                        {
                            lbl_5starCount.Text = totalReviewCounts[revprodID].ToString();
                        }
                        if (revprodID == "4")
                        {
                            lbl_4starCount.Text = totalReviewCounts[revprodID].ToString();
                        }
                        if (revprodID == "3")
                        {
                            lbl_3starCount.Text = totalReviewCounts[revprodID].ToString();
                        }
                        if (revprodID == "2")
                        {
                            lbl_2starCount.Text = totalReviewCounts[revprodID].ToString();
                        }
                        if (revprodID == "1")
                        {
                            lbl_1starCount.Text = totalReviewCounts[revprodID].ToString();
                        }
                        if (revprodID == "0")
                        {
                            lbl_0starCount.Text = totalReviewCounts[revprodID].ToString();
                        }


                    }

                }
            }
        }
        protected void bind()
        {
            List<ReviewWithProduct> reviewList = new List<ReviewWithProduct>();
            List<ReviewWithProduct> reviewProdList = new List<ReviewWithProduct>();
            List<ReviewWithProduct> userReview = new List<ReviewWithProduct>();
            string prodID = Request.QueryString["ProdID"];
            string userID = Session["UserID"].ToString();
            reviewList = revProd.getReviewByProduct(int.Parse(prodID));
            reviewProdList = revProd.getReviewByProductExcludingUser(int.Parse(prodID), int.Parse(userID));
            userReview = revProd.getReviewByProductAndUser(int.Parse(prodID), int.Parse(userID));

            if (reviewList.Count > 0)
            {
                UserReview_Repeater.DataSource = userReview;
                UserReview_Repeater.DataBind();
                Repeater_Reviews.DataSource = reviewProdList;
                Repeater_Reviews.DataBind();
            }
            else
            {
                lbl_reviewTrue.Text = "No reviews available.";
            }
        }

        protected void Repeater_Reviews_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Retrieve the review data item
                ReviewWithProduct review = e.Item.DataItem as ReviewWithProduct;

                // Find the LinkButton controls for edit and delete
                LinkButton btnEdit = e.Item.FindControl("btnEdit") as LinkButton;
                LinkButton btnDelete = e.Item.FindControl("btnDelete") as LinkButton;

                // Check if the userID of the review matches the session state
                if (review != null && btnEdit != null && btnDelete != null)
                {
                    if (review.UserID == Convert.ToInt32(Session["UserID"]))
                    {
                        // Show the edit and delete buttons
                        btnEdit.Visible = true;
                        btnDelete.Visible = true;
                    }
                    else
                    {
                        // Hide the edit and delete buttons
                        btnEdit.Visible = false;
                        btnDelete.Visible = false;
                    }
                }
            }
            if (e.CommandName == "Edit")
            {
                string reviewID = e.CommandArgument.ToString();
                Response.Redirect("EditReview.aspx?reviewID=" + reviewID);
            }
            else if (e.CommandName == "Delete")
            {
          
                string reviewID = e.CommandArgument.ToString();
                if (rev.GetReview(int.Parse(reviewID)) != null)
                {
                    rev.DeleteReview(int.Parse(reviewID));
                    bind(); // Rebind the data after deletion
                    Response.Write("<script>alert('Delete Success');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Delete Unsuccessful');</script>");
                }
            }
        }

        protected void Repeater_Reviews_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Retrieve the review data item
                ReviewWithProduct review = e.Item.DataItem as ReviewWithProduct;

                // Find the LinkButton controls for edit and delete
                LinkButton btnEdit = e.Item.FindControl("btnEdit") as LinkButton;
                LinkButton btnDelete = e.Item.FindControl("btnDelete") as LinkButton;

                // Check if the UserID of the review matches the UserID in the session state
                if (review != null && btnEdit != null && btnDelete != null)
                {
                    int sessionUserID = Convert.ToInt32(Session["UserID"]); // Retrieve the UserID from session state
                    string sessionID = Session["UserID"].ToString();

                    if (review.UserID == int.Parse(sessionID))
                    {
                        // Show the edit and delete buttons
                        btnEdit.Visible = true;
                        btnDelete.Visible = true;
                    }
                    else
                    {
                        // Hide the edit and delete buttons
                        btnEdit.Visible = false;
                        btnDelete.Visible = false;
                    }
                }
            }
        }

        protected void btn_addWish_Click(object sender, EventArgs e)
        {
            // Retrieve the product ID from the command argument
            string prodID = Request.QueryString["ProdID"];
            int userID = int.Parse(Session["UserID"].ToString());

            Wishlist wishCheck = new Wishlist();
            List<int> wishList = new List<int>();
            wishList = wishCheck.GetUserWishlist(userID);
            bool check = false;
            foreach (int product in wishList)
            {
                if (product == int.Parse(prodID))
                {
                    check = true;
                }
            }


            if (!check)
            {
                Wishlist wish = new Wishlist(userID, int.Parse(prodID));
                wish.AddWishList();
                Response.Write("<script>alert('Product Successfully Added To Wishlist!');</script>");
            }
            else
            {
                Response.Write("<script>alert('Product Already In Wishlist');</script>");
            }
        }

        protected void link_Wishlist_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "AddWish")
            {
                // Retrieve the product ID from the command argument
                string prodID = Request.QueryString["ProdID"];
                int userID = int.Parse(Session["UserID"].ToString());

                Wishlist wishCheck = new Wishlist();
                List<int> wishList = new List<int>();
                wishList = wishCheck.GetUserWishlist(userID);
                bool check = false;
                foreach (int product in wishList)
                {
                    if (product == int.Parse(prodID))
                    {
                        check = true;
                        break; // No need to continue checking once found
                    }
                }

                if (!check)
                {
                    Wishlist wish = new Wishlist(userID, int.Parse(prodID));
                    wish.AddWishList();
                    lbl_wishCheck.Text = "True"; // For example, if adding the wish was successful
                }
                else
                {
                    Response.Write("<script>alert('Product Already In Wishlist');</script>");
                }
            }
            if (e.CommandName == "RemoveWish")
            {
                string prodID = Request.QueryString["ProdID"];
                int userID = int.Parse(Session["UserID"].ToString());
                Wishlist wishCheck = new Wishlist();
                List<int> wishList = wishCheck.GetUserWishlist(userID);
                bool check = false;
                foreach (int product in wishList)
                {
                    if (product == int.Parse(prodID))
                    {
                        check = true;
                        break; // No need to continue checking once found
                    }
                }

                if (check)
                {
                    wishCheck.DeleteWishItem(userID, int.Parse(prodID));
                    lbl_wishCheck.Text = "False"; // For example, if removing the wish was successful
                }
                else
                {
                    Response.Write("<script>alert('Item Not Removed From Wishlist');</script>");
                }
            }
            // Update the class and command name of the heart icon based on lbl_wishCheck value
            string lblWishCheckText = lbl_wishCheck.Text.Trim();
            if (lblWishCheckText == "True")
            {
                link_Wishlist.CssClass = "heartProdDetails fa-solid fa-heart";
                link_Wishlist.CommandName = "RemoveWish";
            }
            else
            {
                link_Wishlist.CssClass = "heartProdDetails fa-regular fa-heart";
                link_Wishlist.CommandName = "AddWish";
            }

        }
    }
}