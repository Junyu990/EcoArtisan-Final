using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class CustomerWishlist : System.Web.UI.Page
    {
        WishListWithProduct wishProd = new WishListWithProduct();
        List<WishListWithProduct> wishProdList = new List<WishListWithProduct>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bind();
            }
        }
        protected void bind()
        {
            string userID = Session["UserID"].ToString();
            wishProdList = wishProd.getWishListByUser(int.Parse(userID));


            if (wishProdList.Count > 0)
            {
                repeater_Wishlist.DataSource = wishProdList;
                repeater_Wishlist.DataBind();
            }
            else
            {
                lbl_wishlistTrue.Text = "Wishlist is Empty...";
            }

        }

        protected void btn_ViewProd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Products.aspx");
        }

        protected void repeater_Wishlist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "removeWish")
            {
                int prodID = int.Parse(e.CommandArgument.ToString());
                int userID = int.Parse(Session["UserID"].ToString());
                Wishlist wishCheck = new Wishlist();
                List<int> wishList = wishCheck.GetUserWishlist(userID);
                bool check = false;
                foreach (int product in wishList)
                {
                    if (product == prodID)
                    {
                        check = true;
                    }
                }

                if (check)
                {
                    wishCheck.DeleteWishItem(userID, prodID);
                    bind();
                    Response.Write("<script>alert('Item Removed From Wishlist');</script>");
                    Response.Redirect("CustomerWishlist.aspx");

                }
                else
                {
                    Response.Write("<script>alert('Item Not Removed From Wishlist');</script>");
                }
            }
        }
    }
}