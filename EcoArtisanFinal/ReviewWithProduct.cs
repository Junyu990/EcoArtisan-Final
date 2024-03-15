using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace EcoArtisanFinal
{
    public class ReviewWithProduct
    {
        string _connStr = ConfigurationManager.ConnectionStrings["EcoArtisanDBContext"].ConnectionString;
        public int ReviewID { get; set; }
        public int UserID { get; set; }
        public int ProdID { get; set; }
        public int ReviewRating { get; set; }
        public string ReviewDesc { get; set; }
        public string ReviewDateTime { get; set; }
        public string ReviewLastEdit { get; set; }

        // Product details
        public string ProdName { get; set; }
        public string ProdDesc { get; set; }
        public decimal ProdPrice { get; set; }
        public string ProdImg { get; set; }

        // User Details 
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public ReviewWithProduct(int reviewID, int userID, int prodID, int reviewRating, string reviewDesc, string reviewDateTime, string reviewLastEdit, string prodName, string prodDesc, decimal prodPrice, string prodImg, string userName, string userEmail)
        {
            ReviewID = reviewID;
            UserID = userID;
            ProdID = prodID;
            ReviewRating = reviewRating;
            ReviewDesc = reviewDesc;
            ReviewDateTime = reviewDateTime;
            ReviewLastEdit = reviewLastEdit;
            ProdName = prodName;
            ProdDesc = prodDesc;
            ProdPrice = prodPrice;
            ProdImg = prodImg;
            UserName = userName;
            UserEmail = userEmail;
        }

        public ReviewWithProduct()
        {
        }

        public List<ReviewWithProduct> getReviewWithProductAll()
        {
            List<ReviewWithProduct> reviewProdList = new List<ReviewWithProduct>();

            string queryStr = "SELECT r.*, p.ProdName, p.ProdDesc, p.ProdPrice, p.ProdImage,u.UserName, u.UserEmail, u.UserPassword, u.Points, u.SignUpDate FROM Review r INNER JOIN Product p ON r.ProdID = p.ProdID INNER JOIN[User] u ON r.UserID = u.UserID";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int reviewID = int.Parse(dr["ReviewID"].ToString());
                            int userID = int.Parse(dr["UserID"].ToString());
                            int prodID = int.Parse(dr["ProdID"].ToString());
                            int rating = int.Parse(dr["ReviewRating"].ToString());
                            string desc = dr["ReviewDesc"].ToString();
                            string datetime = dr["ReviewDateTime"].ToString();
                            string lastedit = dr["ReviewLastEdit"].ToString();
                            string productName = dr["ProdName"].ToString();
                            string productDesc = dr["ProdDesc"].ToString();
                            decimal productPrice = decimal.Parse(dr["ProdPrice"].ToString());
                            string productImg = dr["ProdImage"].ToString();

                            // Retrieve user details
                            string userName = dr["UserName"].ToString();
                            string userEmail = dr["UserEmail"].ToString();


                            ReviewWithProduct revProd = new ReviewWithProduct(reviewID, userID, prodID, rating, desc, datetime, lastedit, productName, productDesc, productPrice, productImg, userName, userEmail);

                            reviewProdList.Add(revProd);
                        }
                    }
                }
            }

            return reviewProdList;
        }

        public List<ReviewWithProduct> getReviewByUser(int ID)
        {
            List<ReviewWithProduct> reviewProdList = new List<ReviewWithProduct>();

            string queryStr = "SELECT r.*, p.ProdName, p.ProdDesc, p.ProdPrice, p.ProdImage,u.UserName, u.UserEmail, u.UserPassword, u.Points, u.SignUpDate FROM Review r INNER JOIN Product p ON r.ProdID = p.ProdID INNER JOIN[User] u ON r.UserID = u.UserID WHERE r.UserID = @userID";
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@userID", ID);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                int reviewID = int.Parse(dr["ReviewID"].ToString());
                int userID = int.Parse(dr["UserID"].ToString());
                int prodID = int.Parse(dr["ProdID"].ToString());
                int rating = int.Parse(dr["ReviewRating"].ToString());
                string desc = dr["ReviewDesc"].ToString();
                string datetime = dr["ReviewDateTime"].ToString();
                string lastedit = dr["ReviewLastEdit"].ToString();
                string productName = dr["ProdName"].ToString();
                string productDesc = dr["ProdDesc"].ToString();
                decimal productPrice = decimal.Parse(dr["ProdPrice"].ToString());
                string productImg = dr["ProdImage"].ToString();

                // Retrieve user details
                string userName = dr["UserName"].ToString();
                string userEmail = dr["UserEmail"].ToString();

                ReviewWithProduct revProd = new ReviewWithProduct(reviewID, userID, prodID, rating, desc, datetime, lastedit, productName, productDesc, productPrice, productImg, userName, userEmail);

                reviewProdList.Add(revProd);
            }

            return reviewProdList;
        }

        public List<ReviewWithProduct> getReviewByProduct(int ID)
        {
            List<ReviewWithProduct> reviewProdList = new List<ReviewWithProduct>();

            string queryStr = "SELECT r.*, p.ProdName, p.ProdDesc, p.ProdPrice, p.ProdImage,u.UserName, u.UserEmail, u.UserPassword, u.Points, u.SignUpDate FROM Review r INNER JOIN Product p ON r.ProdID = p.ProdID INNER JOIN[User] u ON r.UserID = u.UserID WHERE r.ProdID = @prodID";
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@prodID", ID);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                int reviewID = int.Parse(dr["ReviewID"].ToString());
                int userID = int.Parse(dr["UserID"].ToString());
                int prodID = int.Parse(dr["ProdID"].ToString());
                int rating = int.Parse(dr["ReviewRating"].ToString());
                string desc = dr["ReviewDesc"].ToString();
                string datetime = dr["ReviewDateTime"].ToString();
                string lastedit = dr["ReviewLastEdit"].ToString();
                string productName = dr["ProdName"].ToString();
                string productDesc = dr["ProdDesc"].ToString();
                decimal productPrice = decimal.Parse(dr["ProdPrice"].ToString());
                string productImg = dr["ProdImage"].ToString();

                // Retrieve user details
                string userName = dr["UserName"].ToString();
                string userEmail = dr["UserEmail"].ToString();

                ReviewWithProduct revProd = new ReviewWithProduct(reviewID, userID, prodID, rating, desc, datetime, lastedit, productName, productDesc, productPrice, productImg, userName, userEmail);

                reviewProdList.Add(revProd);
            }

            return reviewProdList;
        }
        public List<ReviewWithProduct> getReviewByProductAndUser(int prodID, int userID)
        {
            List<ReviewWithProduct> reviewProdList = new List<ReviewWithProduct>();

            string queryStr = "SELECT r.*, p.ProdName, p.ProdDesc, p.ProdPrice, p.ProdImage,u.UserName, u.UserEmail, u.UserPassword, u.Points, u.SignUpDate FROM Review r INNER JOIN Product p ON r.ProdID = p.ProdID INNER JOIN [User] u ON r.UserID = u.UserID WHERE r.ProdID = @prodID AND r.UserID = @userID";
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@prodID", prodID);
                    cmd.Parameters.AddWithValue("@userID", userID);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int reviewID = int.Parse(dr["ReviewID"].ToString());
                            int retrievedUserID = int.Parse(dr["UserID"].ToString());
                            int retrievedProdID = int.Parse(dr["ProdID"].ToString());
                            int rating = int.Parse(dr["ReviewRating"].ToString());
                            string desc = dr["ReviewDesc"].ToString();
                            string datetime = dr["ReviewDateTime"].ToString();
                            string lastedit = dr["ReviewLastEdit"].ToString();
                            string productName = dr["ProdName"].ToString();
                            string productDesc = dr["ProdDesc"].ToString();
                            decimal productPrice = decimal.Parse(dr["ProdPrice"].ToString());
                            string productImg = dr["ProdImage"].ToString();

                            // Retrieve user details
                            string userName = dr["UserName"].ToString();
                            string userEmail = dr["UserEmail"].ToString();

                            ReviewWithProduct revProd = new ReviewWithProduct(reviewID, retrievedUserID, retrievedProdID, rating, desc, datetime, lastedit, productName, productDesc, productPrice, productImg, userName, userEmail);

                            reviewProdList.Add(revProd);
                        }
                    }
                }
            }

            return reviewProdList;
        }
        public List<ReviewWithProduct> getReviewByProductExcludingUser(int prodID, int excludedUserID)
        {
            List<ReviewWithProduct> reviewProdList = new List<ReviewWithProduct>();

            string queryStr = "SELECT r.*, p.ProdName, p.ProdDesc, p.ProdPrice, p.ProdImage, u.UserName, u.UserEmail, u.UserPassword, u.Points, u.SignUpDate FROM Review r INNER JOIN Product p ON r.ProdID = p.ProdID INNER JOIN [User] u ON r.UserID = u.UserID WHERE r.ProdID = @prodID AND r.UserID != @excludedUserID";
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@prodID", prodID);
                    cmd.Parameters.AddWithValue("@excludedUserID", excludedUserID);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int reviewID = int.Parse(dr["ReviewID"].ToString());
                            int retrievedUserID = int.Parse(dr["UserID"].ToString());
                            int retrievedProdID = int.Parse(dr["ProdID"].ToString());
                            int rating = int.Parse(dr["ReviewRating"].ToString());
                            string desc = dr["ReviewDesc"].ToString();
                            string datetime = dr["ReviewDateTime"].ToString();
                            string lastedit = dr["ReviewLastEdit"].ToString();
                            string productName = dr["ProdName"].ToString();
                            string productDesc = dr["ProdDesc"].ToString();
                            decimal productPrice = decimal.Parse(dr["ProdPrice"].ToString());
                            string productImg = dr["ProdImage"].ToString();

                            // Retrieve user details
                            string userName = dr["UserName"].ToString();
                            string userEmail = dr["UserEmail"].ToString();

                            ReviewWithProduct revProd = new ReviewWithProduct(reviewID, retrievedUserID, retrievedProdID, rating, desc, datetime, lastedit, productName, productDesc, productPrice, productImg, userName, userEmail);

                            reviewProdList.Add(revProd);
                        }
                    }
                }
            }

            return reviewProdList;
        }

        public Dictionary<int, int> getTotalReviewCountPerProduct()
        {
            Dictionary<int, int> totalReviewCounts = new Dictionary<int, int>();

            string queryStr = "SELECT r.ProdID, COUNT(*) AS TotalReviews FROM Review r GROUP BY r.ProdID";
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int prodID = int.Parse(dr["ProdID"].ToString());
                            int totalReviews = int.Parse(dr["TotalReviews"].ToString());

                            totalReviewCounts.Add(prodID, totalReviews);
                        }
                    }
                }
            }

            return totalReviewCounts;
        }
        public Dictionary<string, double> getRatingAverageAndRateValueForProduct(int prodID)
        {
            Dictionary<string, double> ratingCounts = new Dictionary<string, double>() { { "Count", 0 }, { "Avg", 0 }, { "5", 0 }, { "4", 0 }, { "3", 0 }, { "2", 0 }, { "1", 0 }, { "0", 0 }, };
            int totalReviews = 0;
            int totalRatingSum = 0;
            double averageRating = 0;

            string queryStr = "SELECT ReviewRating FROM Review WHERE ProdID = @ProdID";
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@ProdID", prodID);
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int rating = int.Parse(dr["ReviewRating"].ToString());
                            string ratingKey = rating.ToString();
                            if (ratingCounts.ContainsKey(ratingKey))
                            {
                                ratingCounts[ratingKey]++;
                            }


                            totalReviews++;
                            totalRatingSum += rating;
                        }
                    }
                }
            }

            ratingCounts["Count"] = totalReviews;

            averageRating = totalReviews > 0 ? Math.Round((double)totalRatingSum / totalReviews, 1) : 0;
            ratingCounts["Avg"] = averageRating;
            return ratingCounts;
        }


    }


}
