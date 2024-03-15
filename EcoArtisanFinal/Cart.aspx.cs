using System;
using System.IO; // For working with files
using System.Configuration; // For reading configuration from web.config
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class Cart1 : System.Web.UI.Page
    {
        private static readonly HttpClient client = new HttpClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || string.IsNullOrEmpty(Session["UserID"].ToString()))
            {
                string script = "alert('Please log in first.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                // Redirect the user to the login page if UserID is not set in the session
                Response.Redirect("/Login");
            };

            // Assuming you have an instance of the Cart class
            Cart cartInstance = new Cart();

            int userID = Convert.ToInt32(Session["UserID"]);

            // Call the getCart method to retrieve cart information
            Cart cartDetails = cartInstance.getCart(userID);

            // Check if cartDetails is not null
            if (cartDetails != null)
            {
                // Set the data source for the Repeater using the Products list
                cartRepeater.DataSource = cartDetails.CartItems;

                // Call DataBind to bind the data
                cartRepeater.DataBind();
            }

            CustomCart customCart = new CustomCart();

            if (customCart != null)
            {
                List<CustomCartItem> cartItems = customCart.GetCustomCartItems(userID);

                cartRepeater1.DataSource = cartItems;
                cartRepeater1.DataBind();
            }
        }

        [WebMethod]
        public static void UpdateCartItemQuantity(int productID, int requestedQuantity)
        {
            // Example to get the maximum quantity allowed (either from a fixed value or database)
            int maxQuantity = 5; // Or fetch from database based on productID

            if (requestedQuantity > maxQuantity)
            {
                // Handle the error, e.g., by throwing an exception or returning an error status
                throw new Exception($"Cannot purchase more than {maxQuantity} of this item per purchase.");
            }
            else
            {
                // Your existing logic to update the cartItemQty goes here
                Cart cartInstance = new Cart();
                int userID = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
                cartInstance.UserID = userID;
                cartInstance.ProdID = productID;
                cartInstance.CartItemQty = requestedQuantity;
                cartInstance.CartUpdate();
            }
        }

        [WebMethod]
        public static void DeleteCartItem(int productID)
        {
            Cart cartInstance = new Cart();
            int userID = Convert.ToInt32(Session["UserID"]);
            cartInstance.UserID = userID;
            cartInstance.ProdID = productID;
            cartInstance.CartDelete();
        }

        [WebMethod(EnableSession = true)]
        public static void DeleteCustomCartItem(int SphereID)
        {
            if (HttpContext.Current.Session["UserID"] != null)
            {
                int userID = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
                CustomCart customcartInstance = new CustomCart();
                customcartInstance.DeleteFromCustomCart(userID, SphereID);
            }
        }


        [WebMethod(EnableSession = true)]
        public static string CheckoutButtonClick()
        {
            // Get the Stripe secret key from web.config
            string stripeSecretKey = ConfigurationManager.AppSettings["StripeSecretKey"];
            StripeConfiguration.ApiKey = stripeSecretKey;

            // Create variables for session, totalAmount, and currency
            Session session = null;
            long totalAmount = 0;
            string currency = "sgd"; // Set the default currency, you can adjust this as needed

            int userID = int.Parse(HttpContext.Current.Session["UserID"].ToString());

            // Retrieve cart details from the database or wherever you store it
            Cart cartInstance = new Cart();
            Cart cartDetails = cartInstance.getCart(userID);

            CustomCart customCartInstance = new CustomCart();
            List<CustomCartItem> customCartItems = customCartInstance.GetCustomCartItems(userID);

            // Check if cartDetails is not null and has items
            if ((cartDetails != null && cartDetails.CartItems.Any()) || customCartItems.Any())
            {
                // Create a Stripe Checkout session
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    Mode = "payment",
                    LineItems = new List<SessionLineItemOptions>(),
                    SuccessUrl = "https://localhost:44369/OrderHistory?status=success&session_id={CHECKOUT_SESSION_ID}",
                    CancelUrl = "https://localhost:44369/cart?status=cancel",
                    BillingAddressCollection = "required", // Request billing address

                    // Include the ShippingAddressCollection option
                    ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                    {
                        AllowedCountries = new List<string> { "SG" }, // Specify allowed countries
                    },
                    ShippingOptions = new List<SessionShippingOptionOptions>
                    {
                        new SessionShippingOptionOptions
                        {
                            ShippingRateData = new SessionShippingOptionShippingRateDataOptions
                            {
                                Type = "fixed_amount",
                                FixedAmount = new SessionShippingOptionShippingRateDataFixedAmountOptions
                                {
                                    Amount = 200,
                                    Currency = "sgd",
                                },
                                DisplayName = "Shipping Fee",
                                DeliveryEstimate = new SessionShippingOptionShippingRateDataDeliveryEstimateOptions
                                {
                                    Minimum = new SessionShippingOptionShippingRateDataDeliveryEstimateMinimumOptions
                                    {
                                        Unit = "business_day",
                                        Value = 5,
                                    },
                                    Maximum = new SessionShippingOptionShippingRateDataDeliveryEstimateMaximumOptions
                                    {
                                        Unit = "business_day",
                                        Value = 7,
                                    },
                                },
                            },
                        },
                    }
                };

                foreach (var cartItem in cartDetails.CartItems)
                {
                    // Assuming your Product class has Price and Name properties
                    int unitAmount = (int)(cartItem.ProdPrice * 100);

                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = currency,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = cartItem.ProdName,
                                // Add other properties as needed
                            },
                            UnitAmount = unitAmount,
                        },
                        Quantity = cartItem.CartItemQty, // Use the quantity from the cart item
                    });

                    totalAmount += unitAmount * cartItem.CartItemQty; // Multiply by quantity
                }

                foreach (var customCartItem in customCartItems)
                {
                    int unitAmount = (int)(customCartItem.SpherePrice * 100);
                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = currency,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = customCartItem.SphereDesc,
                            },
                            UnitAmount = unitAmount,
                        },
                        Quantity = customCartItem.CustomCartItemQty,
                    });
                }

                var service = new SessionService();
                session = service.Create(options);
            }

            if (session != null)
            {
                // Return the URL as a string
                return session.Url;
            }
            else
            {
                // Handle cases where the session is not created successfully
                return "errorPage.aspx"; // Or any
            }
        }

        [WebMethod(EnableSession = true)]
        public static string SendMessageAsync(string message)
        {
            string response = GetChatbotResponse(message).Result;
            return response;
        }

        private static async Task<string> GetChatbotResponse(string userInput)
        {
            try
            {
                var response = await client.PostAsJsonAsync("http://localhost:5000/chat", new { message = userInput }).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();

                dynamic properresponse = JsonConvert.DeserializeObject(responseContent);

                return properresponse;
            }
            catch (HttpRequestException)
            {
                // Handle exception (e.g., API not reachable)
                return "Sorry, I couldn't reach the chatbot.";
            }
        }

        private new static HttpServerUtility Server => HttpContext.Current.Server;
        private new static HttpSessionState Session => HttpContext.Current.Session;

        protected void cartRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
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
            cartRepeater.DataBind();
        }

        protected void cartRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string userID = Session["UserID"].ToString();
                // Retrieve the current data item from the Repeater
                CartItem product = (CartItem)e.Item.DataItem;


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

        protected void cartRepeater_ItemCommand1(object source, RepeaterCommandEventArgs e)
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
            cartRepeater.DataBind();
        }

        protected void cartRepeater_ItemDataBound1(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string userID = Session["UserID"].ToString();
                // Retrieve the current data item from the Repeater
                CartItem product = (CartItem)e.Item.DataItem;


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


    }

}