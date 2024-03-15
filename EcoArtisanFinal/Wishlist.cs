using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace EcoArtisanFinal
{
    public class Wishlist
    {
        string _connStr = ConfigurationManager.ConnectionStrings["EcoArtisanDBContext"].ConnectionString;
        private int _userID = 0;
        private int _prodID = 0;

        public Wishlist(int userID, int prodID)
        {
            _userID = userID;
            _prodID = prodID;
        }

        public Wishlist()
        {
        }

        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }
        public int ProdID
        {
            get { return _prodID; }
            set { _prodID = value; }
        }

        public int AddWishList()
        {
            int result = 0;
            string queryStr = "INSERT INTO Wishlist(UserID, ProdID)" + "values(@userID, @prodID)";

            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);


            cmd.Parameters.AddWithValue("@userID", this.UserID);
            cmd.Parameters.AddWithValue("@prodID", this.ProdID);

            conn.Open();
            result += cmd.ExecuteNonQuery();

            conn.Close();

            return result;
        }
        public int DeleteWishItem(int userID, int prodID)
        {
            string queryStr = "DELETE FROM Wishlist WHERE UserID = @userID AND ProdID = @prodID";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.Parameters.AddWithValue("@prodID", prodID);

                    conn.Open();
                    int nOfRow = cmd.ExecuteNonQuery();

                    return nOfRow;
                }
            }
        }
        public Wishlist GetWishItem(int ID)
        {
            Wishlist wishItem = null;

            int userID, prodID;



            string queryStr = "SELECT * FROM Wishlist WHERE ProdID = @prodID";

            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@prodID", ID);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                userID = int.Parse(dr["UserID"].ToString());
                prodID = int.Parse(dr["ProdID"].ToString());

                Wishlist wishlist = new Wishlist(userID, prodID);


            }
            else
            {
                wishItem = null;
            }

            conn.Close();
            dr.Close();
            dr.Dispose();

            return wishItem;
        }
        public List<int> GetUserWishlist(int userID)
        {
            List<int> wishlist = new List<int>();

            string queryStr = "SELECT ProdID FROM Wishlist WHERE UserID = @userID";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        int prodID = int.Parse(dr["ProdID"].ToString());
                        wishlist.Add(prodID);
                    }
                }
            }

            return wishlist;
        }
        public List<Wishlist> getWishListByUser()
        {
            List<Wishlist> wishlistList = new List<Wishlist>();
            int userID, prodID;

            string queryStr = "SELECT * FROM Wishlist WHERE UserID = @userID"; // SQL
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                userID = int.Parse(dr["UserID"].ToString());
                prodID = int.Parse(dr["ProdID"].ToString());

                Wishlist wishlist = new Wishlist(userID, prodID);

                wishlistList.Add(wishlist);
            }
            conn.Close();
            dr.Close();
            dr.Dispose();

            return wishlistList;
        }
        public int DeleteWishlistItemsByUserID(int userID)
        {
            string queryStr = "DELETE FROM Wishlist WHERE UserID = @userID";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);

                    conn.Open();
                    int numberOfRowsAffected = cmd.ExecuteNonQuery();
                    return numberOfRowsAffected;
                }
            }
        }
    }
}