using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;

namespace EcoArtisanFinal
{
    public partial class Products : System.Web.UI.Page
    {
        Product prod = new Product();
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
            }
        }
        protected void bind()
        {
            List<Product> prodList = new List<Product>();
            prodList = prod.getProductAll();
            repeater_Product.DataSource = prodList;
            repeater_Product.DataBind();
        }

        [WebMethod]
        public static string AddToCart(int productId)
        {
            try
            {
                if (HttpContext.Current.Session["UserID"] == null || string.IsNullOrEmpty(HttpContext.Current.Session["UserID"].ToString()))
                {
                    // Returning a JSON object indicating the user is not logged in
                    return JsonConvert.SerializeObject(new
                    {
                        success = false,
                        reason = "UserNotLoggedIn"
                    });
                }

                int userID = int.Parse(HttpContext.Current.Session["UserID"].ToString());
                Cart cart = new Cart();
                cart = cart.getCart(userID);

                var existingCartItem = cart.CartItems.FirstOrDefault(item => item.ProdID == productId);
                bool isExistingItem = existingCartItem != null;
                int newQuantity = 0;

                if (isExistingItem)
                {
                    existingCartItem.CartItemQty++;
                    cart.UpdateCartItem(existingCartItem);
                    newQuantity = existingCartItem.CartItemQty;
                }
                else
                {
                    cart.UserID = userID;
                    cart.ProdID = productId;
                    cart.CartItemQty = 1;
                    cart.CartInsert(); // Assume this inserts the item to the database
                    newQuantity = 1;
                }

                // Constructing and returning a detailed JSON response
                return JsonConvert.SerializeObject(new
                {
                    success = true,
                    userId = userID,
                    productId = productId,
                    isNewItem = !isExistingItem,
                    quantity = newQuantity
                });
            }
            catch (Exception ex)
            {
                // In case of an error, return a JSON object with success false and the error message
                return JsonConvert.SerializeObject(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }

        protected void repeater_Product_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "AddWish")
            {
                string prodID = e.CommandArgument.ToString();
                string userID = Session["UserID"].ToString();
                // Find the LinkButton control in the clicked Repeater item
                LinkButton linkprod_Wishlist = (LinkButton)e.Item.FindControl("linkprod_Wishlist");

                Wishlist wishItem = new Wishlist(int.Parse(userID), int.Parse(prodID));
                wishItem.AddWishList();

                linkprod_Wishlist.CssClass = "heartProd fa-solid"; // Set CSS class to fa-regular
                linkprod_Wishlist.CommandName = "RemoveWish"; // Set CommandName to AddWish

            }
            if (e.CommandName == "RemoveWish")
            {
                string prodID = e.CommandArgument.ToString();
                string userID = Session["UserID"].ToString();
                // Find the LinkButton control in the clicked Repeater item
                LinkButton linkprod_Wishlist = (LinkButton)e.Item.FindControl("linkprod_Wishlist");
                Wishlist wishItem = new Wishlist();
                wishItem.DeleteWishItem(int.Parse(userID), int.Parse(prodID));

                linkprod_Wishlist.CssClass = "heartProd fa-regular"; // Set CSS class to fa-regular
                linkprod_Wishlist.CommandName = "AddWish"; // Set CommandName to AddWish
            }
            bind();
        }

        protected void repeater_Product_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string userID = Session["UserID"].ToString();
                // Retrieve the current data item from the Repeater
                Product product = (Product)e.Item.DataItem;


                // Find the LinkButton and Label controls in the current Repeater item
                LinkButton linkprod_Wishlist = (LinkButton)e.Item.FindControl("linkprod_Wishlist");
                Label lbl_prodwishCheck = (Label)e.Item.FindControl("lbl_prodwishCheck");

                // Check if the current product is in the user's wishlist
                bool productInWishlist = CheckIfProductInWishlist(int.Parse(userID), product.ProdID); // Assuming ProductID is a property of your Product class

                // Update the CSS class and CommandName of the LinkButton based on the wishlist status
                if (productInWishlist)
                {
                    linkprod_Wishlist.CssClass = "heartProd fa-solid fa-heart"; // Set CSS class to fa-solid
                    linkprod_Wishlist.CommandName = "RemoveWish"; // Set CommandName to RemoveWish
                }
                else
                {
                    linkprod_Wishlist.CssClass = "heartProd fa-regular fa-heart"; // Set CSS class to fa-regular
                    linkprod_Wishlist.CommandName = "AddWish"; // Set CommandName to AddWish
                }
            }
        }
        private bool CheckIfProductInWishlist(int userID, int prodID)
        {
            WishListWithProduct wish = new WishListWithProduct();
            List<WishListWithProduct> Wishlist = wish.getWishListByUser(userID);

            foreach (WishListWithProduct prod in Wishlist)
            {
                int productid = prod.ProdID;
                if (productid == prodID)
                {
                    return true;
                }
            }
            return false;
        }
    }
}