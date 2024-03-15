using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class AdminViewProduct : System.Web.UI.Page
    {
        Product prod = new Product();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bind();
            }
        }
        protected void bind()
        {
            List<Product> prodList = new List<Product>();
            prodList = prod.getProductAll();
            repeater_AdminProduct.DataSource = prodList;
            repeater_AdminProduct.DataBind();
        }

        protected void btn_AddProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminProduct.aspx");
        }
    }
}