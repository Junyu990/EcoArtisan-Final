using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class AddReview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime currentDateTime = DateTime.Now;
            string dateTime = currentDateTime.ToString();
            // PRODUCT NAME NOT INITIALIZED
            // PUT THE PRODUCT IMAGE TO SHOW 
            Product prod = new Product();
            if (Request.QueryString["ProdID"] != null)
            {
                // Retrieve the value of the "ProdID" parameter from the query string
                string prodID = Request.QueryString["ProdID"];
                prod = prod.GetProduct(int.Parse(prodID));

                img_ProdImg.ImageUrl = ResolveUrl("~/Content/Products/" + prod.ProdImg);
                lbl_prodName.Text = prod.ProdName;

            }
        }

        protected void btn_reviewSubmit_Click(object sender, EventArgs e)
        {
            // Retrieve the value of the "ProdID" parameter from the query string
            string prodID = Request.QueryString["ProdID"];
            string userID = Session["UserID"].ToString();

            DateTime currentDateTime = DateTime.Now;
            string dateTime = currentDateTime.ToString();
            string ratingValue = Request.Form["Rating"];
            int rate = 0;
            if (ratingValue == "")
            {
                rate = 0;
            }
            else
            {
                rate = int.Parse(ratingValue);
            }


            Review review = new Review();



            if (String.IsNullOrEmpty(tb_reviewDesc.Text))
            {
                Response.Write("<script>alert('Review Add Unsuccessful');</script>");
            }
            else
            {
                review = new Review(int.Parse(userID), int.Parse(prodID), rate, tb_reviewDesc.Text, dateTime, "-");
                review.AddReview();
                Response.Write("<script>alert('Review Successfully Uploaded');</script>");
                Response.Redirect("ProductDetails.aspx?prodID=" + prodID);
            }
        }
    }
}