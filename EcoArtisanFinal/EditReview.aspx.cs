using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class EditReview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Product prod = new Product();
                Review rev = new Review();
                if (Request.QueryString["reviewID"] != null)
                {
                    // Retrieve the value of the "ProdID" parameter from the query string
                    string reviewID = Request.QueryString["reviewID"];
                    rev = rev.GetReview(int.Parse(reviewID));
                    prod = prod.GetProduct(rev.ProdID);

                    img_ProdImg.ImageUrl = ResolveUrl("~/Content/Products/" + prod.ProdImg);
                    lbl_prodName.Text = prod.ProdName;

                    lbl_ratingValue.Text = rev.ReviewRating.ToString();

                    tb_editreviewDesc.Text = rev.ReviewDesc;
                }


            }
        }

        protected void btn_reviewUpdate_Click(object sender, EventArgs e)
        {
            // Retrieve the value of the "ProdID" parameter from the query string
            string reviewID = Request.QueryString["reviewID"];

            Review reviewDisplay = new Review();
            reviewDisplay = reviewDisplay.GetReview(int.Parse(reviewID));

            int prodID = reviewDisplay.ProdID;



            DateTime currentDateTime = DateTime.Now;
            string lastEdit = currentDateTime.ToString();
            string ratingValue = Request.Form["Rating"];

            string reviewDesc = tb_editreviewDesc.Text;
            int rate = 0;
            if (ratingValue == "")
            {
                rate = reviewDisplay.ReviewRating;
            }
            else
            {
                rate = int.Parse(ratingValue);
            }

            if (rate == reviewDisplay.ReviewRating && reviewDesc == reviewDisplay.ReviewDesc)
            {
                Response.Write("<script>alert('No Changes Made.Update Not Successful');</script>");
            }
            else
            {
                reviewDisplay.UpdateReview(reviewDisplay.ReviewID, rate, reviewDesc, lastEdit);

                Response.Write("<script>alert('Update Success');</script>");
                Response.Redirect("ProductDetails.aspx?prodID=" + prodID);

            }
        }

        protected void btn_reviewBack_Click(object sender, EventArgs e)
        {
            string reviewID = Request.QueryString["reviewID"];

            Review reviewDisplay = new Review();
            reviewDisplay = reviewDisplay.GetReview(int.Parse(reviewID));

            int prodID = reviewDisplay.ProdID;


            Response.Redirect("ProductDetails.aspx?prodID=" + prodID);
        }
    }
}