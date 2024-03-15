using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class ReadReview : System.Web.UI.Page
    {
        Review rev = new Review();
        ReviewWithProduct revProd = new ReviewWithProduct();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bind();
            }
        }
        protected void bind()
        {
            List<Review> reviewList = new List<Review>();
            List<ReviewWithProduct> reviewProdList = new List<ReviewWithProduct>();
            string userID = Session["UserID"].ToString();
            reviewProdList = revProd.getReviewByUser(int.Parse(userID));

            if (reviewProdList.Count > 0)
            {
                Repeater_Reviews.DataSource = reviewProdList;
                Repeater_Reviews.DataBind();
            }
            else
            {
                lbl_reviewTrue.Text = "No Reviews...";
            }

        }

        protected void btn_ViewProd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Products.aspx");
        }

        protected void Repeater_Reviews_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
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
            else if (e.CommandName == "View")
            {
                string reviewID = e.CommandArgument.ToString();
                Review rev = new Review();
                rev = rev.GetReview(int.Parse(reviewID));

                string prodID = rev.ProdID.ToString();
                Response.Redirect("ProductDetails.aspx?prodID=" + prodID);
            }
        }
    }
}