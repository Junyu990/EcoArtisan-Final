using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class AdminProductDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the query string parameter "ProdID" exists in the URL
                if (Request.QueryString["ProdID"] != null)
                {
                    // Retrieve the value of the "ProdID" parameter from the query string
                    string prodID = Request.QueryString["ProdID"];
                    lbl_prodID.Text = prodID;


                    Product prodD = new Product();

                    prodD = prodD.GetProduct(int.Parse(prodID));


                    if (prodD != null)
                    {
                        tb_updProdName.Text = prodD.ProdName;
                        tb_updProdDesc.Text = prodD.ProdDesc;
                        tb_updProdPrice.Text = prodD.ProdPrice.ToString();
                        tb_updProdQty.Text = prodD.ProdQty.ToString();
                        tb_updProdImpact.Text = prodD.ProdImpact.ToString();

                        img_updProdimg.ImageUrl = ResolveUrl("~/Content/Products/" + prodD.ProdImg);
                    }
                    else
                    {
                        lbl_prodID.Text = "not found!";
                    }

                }
            }
        }

        protected void btn_CancelUpdate_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminViewProduct.aspx");
        }

        protected void btn_UpdateProduct_Click(object sender, EventArgs e)
        {
            string prodID = Request.QueryString["ProdID"];

            Response.Redirect("AdminUpdateProduct.aspx?prodID=" + prodID);
        }
    }
}