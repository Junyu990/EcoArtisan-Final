using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class AdminProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_AddProduct_Click(object sender, EventArgs e)
        {
            Product prod = new Product();
            int validate_count = 0;
            string validate_string = "";
            List<Product> prodList = prod.getProductAll();

            foreach (var produ in prodList)
            {
                string prodName = produ.ProdName;
                if (tb_ProdName.Text == prodName)
                {
                    tb_ProdName.Text = "";
                    lbl_prodNameCheck.Text = "Existing Name.";
                    validate_count += 1;
                    validate_string += "Invalid Name;";
                    break;
                }
            }

            if (!decimal.TryParse(tb_ProdPrice.Text, out decimal _))
            {
                tb_ProdPrice.Text = "";
                validate_string += "Invalid Price;";
                validate_count += 1;
            }

            if (!decimal.TryParse(tb_ProdQty.Text, out decimal _))
            {
                tb_ProdQty.Text = "";
                validate_string += "Invalid Quantity;  ";
                validate_count += 1;
            }
            if (!decimal.TryParse(tb_ProdImpact.Text, out decimal _))
            {
                tb_ProdImpact.Text = "";
                validate_string += "Invalid Impact Amount; ";
                validate_count += 1;
            }

            if (validate_count > 0)
            {
                Response.Write("<script>alert('Insert Unsuccesful');</script>");
                Response.Write("<script>alert('" + validate_string + "');</script>");
                tb_ProdName.Text = "";
                tb_ProdDesc.Text = "";
                tb_ProdPrice.Text = "";
                tb_ProdQty.Text = "";
                tb_ProdImpact.Text = "";
            }
            else
            {
                Response.Write("<script>console.log('going to add product');</script>");
                prod = new Product(tb_ProdName.Text, tb_ProdDesc.Text, decimal.Parse(tb_ProdPrice.Text), file_ProdImg.FileName, int.Parse(tb_ProdQty.Text), int.Parse(tb_ProdImpact.Text));
                prod.AddProduct();
                tb_ProdName.Text = "";
                tb_ProdDesc.Text = "";
                tb_ProdPrice.Text = "";
                tb_ProdQty.Text = "";
                tb_ProdImpact.Text = "";
                Response.Write("<script>alert('Product Added!');</script>");
                Response.Redirect("AdminViewProduct.aspx");
            }
        }

        protected void btn_CancelProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminViewProduct.aspx");
        }
    }
}