using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace EcoArtisanFinal
{
    public class Cart
    {
        string _connStr = ConfigurationManager.ConnectionStrings["EcoArtisanDBContext"].ConnectionString;
        private int _UserID = 0;
        private int _ProdID = 0;
        private int _CartItemQty = 0;
        private string _ProdName = "";
        private string _ProdDesc = "";
        private decimal _ProdPrice = 0;
        private string _ProdImage = "";
        private int _ProdQty = 0;
        private int _ProdImpact = 0;

        // Include a list of products in the cart
        public List<CartItem> CartItems { get; set; }

        public Cart(int UserID, int ProdID, int CartItemQty)
        {
            _UserID = UserID;
            _ProdID = ProdID;
            _CartItemQty = CartItemQty;
            CartItems = new List<CartItem>(); // Initialize the list
        }

        public Cart()
        {
            CartItems = new List<CartItem>(); // Initialize the list
        }

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public int ProdID
        {
            get { return _ProdID; }
            set { _ProdID = value; }
        }

        public int CartItemQty
        {
            get { return _CartItemQty; }
            set { _CartItemQty = value; }
        }

        public string ProdName
        {
            get { return _ProdName; }
            set { _ProdName = value; }
        }

        public string ProdDesc
        {
            get { return _ProdDesc; }
            set { _ProdDesc = value; }
        }

        public decimal ProdPrice
        {
            get { return _ProdPrice; }
            set { _ProdPrice = value; }
        }

        public string ProdImage
        {
            get { return _ProdImage; }
            set { _ProdImage = value; }
        }

        public int ProdQty
        {
            get { return _ProdQty; }
            set { _ProdQty = value; }
        }

        public int ProdImpact
        {
            get { return _ProdImpact; }
            set { _ProdImpact = value; }
        }

        public Cart getCart(int UserID)
        {
            Cart cartDetail = new Cart(UserID, 0, 0);

            string queryStr = "SELECT c.UserID, c.ProdID, c.CartItemQty, p.ProdName, p.ProdDesc, p.ProdPrice, p.ProdImage, p.ProdQty, p.ProdImpact " +
                              "FROM Cart c " +
                              "INNER JOIN Product p ON c.ProdID = p.ProdID " +
                              "WHERE c.UserID = @UserID";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID);

                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            CartItem product = new CartItem
                            {
                                ProdID = (int)dr["ProdID"],
                                ProdName = (string)dr["ProdName"],
                                ProdDesc = (string)dr["ProdDesc"],
                                ProdPrice = (decimal)dr["ProdPrice"],
                                ProdImage = (string)dr["ProdImage"],
                                ProdQty = (int)dr["ProdQty"],
                                ProdImpact = (int)dr["ProdImpact"],
                                CartItemQty = (int)dr["CartItemQty"]
                            };

                            cartDetail.CartItems.Add(product);
                        }
                    }
                }
            }

            return cartDetail;
        }

        public int CartInsert()
        {
            int result = 0;

            string queryStr = "INSERT INTO Cart(UserID, ProdID, CartItemQty) VALUES (@UserID, @ProdID, @CartItemQty)";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@ProdID", this.ProdID);
                    cmd.Parameters.AddWithValue("@CartItemQty", this.CartItemQty);

                    conn.Open();
                    result = cmd.ExecuteNonQuery();
                }
            }

            return result;
        }

        public int CartUpdate()
        {
            string queryStr = "UPDATE Cart SET CartItemQty = @CartItemQty WHERE UserID = @UserID AND ProdID = @ProdID";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@ProdID", this.ProdID);
                    cmd.Parameters.AddWithValue("@CartItemQty", this.CartItemQty);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int UpdateCartItem(CartItem cartItem)
        {
            int result = 0;
            string queryStr = "UPDATE Cart SET CartItemQty = @CartItemQty WHERE UserID = @UserID AND ProdID = @ProdID";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@ProdID", cartItem.ProdID);
                    cmd.Parameters.AddWithValue("@CartItemQty", cartItem.CartItemQty);

                    conn.Open();
                    result = cmd.ExecuteNonQuery();
                }
            }

            return result;
        }

        public int CartDelete()
        {
            string queryStr = "DELETE FROM Cart WHERE UserID = @UserID AND ProdID = @ProdID";
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@ProdID", this.ProdID);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int ClearCart(int userID)
        {
            string queryStr = "DELETE FROM Cart WHERE UserID = @UserID";
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    conn.Open();
                    return cmd.ExecuteNonQuery(); // This will delete all cart items for the user
                }
            }
        }

        // JING RONG ADDED FOR DELETE USER
        public int DeleteCartItemsByUserID(int userID)
        {
            int rowsAffected = 0;

            string queryStr = "DELETE FROM Cart WHERE UserID = @UserID";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    rowsAffected = cmd.ExecuteNonQuery(); // Execute the command and get the number of rows affected
                }
            }

            return rowsAffected;
        }
    }

    public class CartItem
    {
        public int ProdID { get; set; }
        public string ProdName { get; set; }
        public string ProdDesc { get; set; }
        public decimal ProdPrice { get; set; }
        public string ProdImage { get; set; }
        public int ProdQty { get; set; }
        public int ProdImpact { get; set; }
        public int CartItemQty { get; set; }
    }
}