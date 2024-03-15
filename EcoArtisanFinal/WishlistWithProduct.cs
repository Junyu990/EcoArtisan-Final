using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EcoArtisanFinal
{
    public class WishListWithProduct
    {
        string _connStr = ConfigurationManager.ConnectionStrings["EcoArtisanDBContext"].ConnectionString;

        public int UserID { get; set; }
        public int ProdID { get; set; }

        // Product details
        public string ProdName { get; set; }
        public string ProdDesc { get; set; }
        public decimal ProdPrice { get; set; }
        public string ProdImg { get; set; }

        private string _iconClassName = "";


        public string IconClassName
        {
            get { return _iconClassName; }
            set { _iconClassName = value; }
        }



        public WishListWithProduct()
        {
        }

        public WishListWithProduct(int userID, int prodID, string prodName, string prodDesc, decimal prodPrice, string prodImg)
        {
            UserID = userID;
            ProdID = prodID;
            ProdName = prodName;
            ProdDesc = prodDesc;
            ProdPrice = prodPrice;
            ProdImg = prodImg;
        }

        public WishListWithProduct(int userID, int prodID, string prodName, string prodDesc, decimal prodPrice, string prodImg, string iconClassName) : this(userID, prodID, prodName, prodDesc, prodPrice, prodImg)
        {
            UserID = userID;
            ProdID = prodID;
            ProdName = prodName;
            ProdDesc = prodDesc;
            ProdPrice = prodPrice;
            ProdImg = prodImg;
            IconClassName = iconClassName;
        }

        public List<WishListWithProduct> getWishListByUser(int ID)
        {
            List<WishListWithProduct> wishlistList = new List<WishListWithProduct>();
            string queryStr = "SELECT Wishlist.UserID, Wishlist.ProdID, Product.ProdName, Product.ProdDesc, Product.ProdPrice, Product.ProdImage FROM Wishlist INNER JOIN Product ON Wishlist.ProdID = Product.ProdID WHERE Wishlist.UserID = @userID";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@userID", ID);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int userID = int.Parse(dr["UserID"].ToString());
                            int prodID = int.Parse(dr["ProdID"].ToString());

                            string prodName = dr["ProdName"].ToString();
                            string prodDesc = dr["ProdDesc"].ToString();
                            decimal prodPrice = decimal.Parse(dr["ProdPrice"].ToString());
                            string prodImg = dr["ProdImage"].ToString();

                            WishListWithProduct wishlist = new WishListWithProduct(userID, prodID, prodName, prodDesc, prodPrice, prodImg);
                            wishlistList.Add(wishlist);
                        }
                    }
                }
            }
            return wishlistList;
        }

    }
}