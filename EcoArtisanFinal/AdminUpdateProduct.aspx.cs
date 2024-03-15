using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class AdminUpdateProduct : System.Web.UI.Page
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

        protected void btn_UpdateProduct_Click(object sender, EventArgs e)
        {
            string prodID = Request.QueryString["ProdID"];
            Product prod = new Product();
            prod = prod.GetProduct(int.Parse(prodID));

            string prodImg = "";

            int validate_count = 0;
            string validate_string = "";

            List<Product> prodList = prod.getProductAll();

            foreach (var produ in prodList)
            {
                string prodName = produ.ProdName;
                int productID = produ.ProdID;
                if (tb_updProdName.Text == prodName)
                {
                    if(lbl_prodID.Text != productID.ToString())
                    {
                        tb_updProdName.Text = "";
                        lbl_prodNameCheck.Text = "Existing Name.";
                        validate_count += 1;
                        validate_string += "Invalid Name;";
                        break;
                    }
                    
                }
            }

            if (!decimal.TryParse(tb_updProdPrice.Text, out decimal _))
            {
                tb_updProdPrice.Text = "";
                validate_string += "Invalid Price; ";
                validate_count += 1;
            }

            if (!int.TryParse(tb_updProdQty.Text, out int _))
            {
                tb_updProdQty.Text = "";
                validate_string += "Invalid Quantity; ";
                validate_count += 1;
            }
            if (!int.TryParse(tb_updProdImpact.Text, out int _))
            {
                tb_updProdImpact.Text = "";
                validate_string += "Invalid Imapact Amount; ";
                validate_count += 1;
            }

            if (!file_updProdImg.HasFile)
            {
                prodImg = prod.ProdImg;

            }
            else
            {

                prodImg = file_updProdImg.FileName;


            }

            if (validate_count > 0)
            {
                Response.Write("<script>alert('update Unsuccesful');</script>");
                Response.Write("<script>alert('" + validate_string + "');</script>");
            }
            else
            {
                Response.Write("<script>console.log('going to update product');</script>");
                prod.UpdateProduct(int.Parse(prodID), tb_updProdName.Text, tb_updProdDesc.Text, decimal.Parse(tb_updProdPrice.Text), prodImg, int.Parse(tb_updProdQty.Text), int.Parse(tb_updProdImpact.Text));
                Response.Write("<script>alert('Product Updated!');</script>");
                Response.Redirect("AdminViewProduct.aspx");
            }
        }

        protected void btn_DeleteProd_Click(object sender, EventArgs e)
        {
            string prodID = Request.QueryString["ProdID"];
            Product prod = new Product();

            prod.DeleteProduct(int.Parse(prodID));
            Response.Redirect("AdminViewProduct.aspx");

        }

        protected void btn_CancelUpdate_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminViewProduct.aspx");
        }
    }
}