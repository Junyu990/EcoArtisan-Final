using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace EcoArtisanFinal
{
    public class CustomCartItem
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public int SphereID { get; set; }
        public string SphereImage { get; set; }
        public string SphereDesc { get; set; }
        public decimal SpherePrice { get; set; }
        public string SphereMat { get; set; }
        public string SphereColour { get; set; }
        public string SphereSize { get; set; }
        public int CustomCartItemQty { get; set; }
    }

    public class CustomCart
    {
        string _connStr = ConfigurationManager.ConnectionStrings["EcoArtisanDBContext"].ConnectionString;

        // Method to insert a sphere into the CustomCart with default quantity of 1
        public int InsertIntoCustomCart(int userID, int sphereID, int customCartItemQty = 1)
        {
            int result = 0;
            string queryStr = "INSERT INTO CustomCart(UserID, SphereID, CustomCartItemQty) VALUES (@UserID, @SphereID, @CustomCartItemQty)";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@SphereID", sphereID);
                    cmd.Parameters.AddWithValue("@CustomCartItemQty", customCartItemQty);

                    conn.Open();
                    result = cmd.ExecuteNonQuery();
                }
            }

            return result;
        }

        // Method to delete a sphere from the CustomCart for a specific user
        public int DeleteFromCustomCart(int userID, int sphereID)
        {
            int result = 0;
            string queryStr = "DELETE FROM CustomCart WHERE SphereID = @SphereID AND UserID = @UserID";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@SphereID", sphereID);
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    conn.Open();
                    result = cmd.ExecuteNonQuery();
                }
            }

            return result;
        }

        public List<CustomCartItem> GetCustomCartItems(int userID)
        {
            List<CustomCartItem> cartItems = new List<CustomCartItem>();
            string queryStr = @"SELECT cc.CustomCartID, cc.UserID, cc.SphereID, s.SphereImage, s.SphereDesc, 
                        s.SpherePrice, s.SphereMat, s.SphereColour, s.SphereSize, cc.CustomCartItemQty
                        FROM CustomCart cc
                        INNER JOIN Sphere s ON cc.SphereID = s.SphereID
                        WHERE cc.UserID = @UserID";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            CustomCartItem item = new CustomCartItem
                            {
                                CartID = Convert.ToInt32(dr["CustomCartID"]),
                                UserID = Convert.ToInt32(dr["UserID"]),
                                SphereID = Convert.ToInt32(dr["SphereID"]),
                                SphereImage = dr["SphereImage"].ToString(),
                                SphereDesc = dr["SphereDesc"].ToString(),
                                SpherePrice = Convert.ToDecimal(dr["SpherePrice"]),
                                SphereMat = dr["SphereMat"].ToString(),
                                SphereColour = dr["SphereColour"].ToString(),
                                SphereSize = dr["SphereSize"].ToString(),
                                CustomCartItemQty = Convert.ToInt32(dr["CustomCartItemQty"])
                            };

                            cartItems.Add(item);
                        }
                    }
                }
            }

            return cartItems;
        }

        public void ClearCustomCart(int userID)
        {
            string queryStr = "DELETE FROM CustomCart WHERE UserID = @UserID";
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}