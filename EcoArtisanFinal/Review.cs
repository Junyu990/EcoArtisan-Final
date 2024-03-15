using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EcoArtisanFinal
{
    public class Review
    {
        string _connStr = ConfigurationManager.ConnectionStrings["EcoArtisanDBContext"].ConnectionString;
        private int _reviewID = 0;
        private int _userID = 0;
        private int _prodID = 0;
        private int _rating = 0;
        private string _reviewDesc = "";
        private string _reviewDateTime = "";
        private string _reviewLastEdit = "";


        public Review()
        {
        }

        public Review(int reviewID, int userID, int prodID, int rating, string reviewDesc, string reviewDateTime, string reviewLastEdit)
        {
            _reviewID = reviewID;
            _userID = userID;
            _prodID = prodID;
            _rating = rating;
            _reviewDesc = reviewDesc;
            _reviewDateTime = reviewDateTime;
            _reviewLastEdit = reviewLastEdit;
        }

        public Review(int userID, int prodID, int rating, string reviewDesc, string reviewDateTime, string reviewLastEdit)
        {
            _userID = userID;
            _prodID = prodID;
            _rating = rating;
            _reviewDesc = reviewDesc;
            _reviewDateTime = reviewDateTime;
            _reviewLastEdit = reviewLastEdit;
        }

        public int ReviewID
        {
            get { return _reviewID; }
            set { _reviewID = value; }
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

        public int ReviewRating
        {
            get { return _rating; }
            set { _rating = value; }
        }

        public string ReviewDesc
        {
            get { return _reviewDesc; }
            set { _reviewDesc = value; }
        }

        public string ReviewDateTime
        {
            get { return _reviewDateTime; }
            set { _reviewDateTime = value; }
        }

        public string ReviewLastEdit
        {
            get { return _reviewLastEdit; }
            set { _reviewLastEdit = value; }
        }

        public int AddReview()
        {
            int result = 0;
            string queryStr = "INSERT INTO Review(UserID, ProdID, ReviewRating, ReviewDesc, ReviewDateTime, ReviewLastEdit)" + "values(@userID, @prodID, @rating, @desc, @datetime, @lastedit)";

            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);


            cmd.Parameters.AddWithValue("@userID", this.UserID);
            cmd.Parameters.AddWithValue("@prodID", this.ProdID);
            cmd.Parameters.AddWithValue("@rating", this.ReviewRating);
            cmd.Parameters.AddWithValue("@desc", this.ReviewDesc);
            cmd.Parameters.AddWithValue("@datetime", this.ReviewDateTime);
            cmd.Parameters.AddWithValue("@lastedit", this.ReviewDateTime);

            conn.Open();
            result += cmd.ExecuteNonQuery();

            conn.Close();

            return result;

        }

        public Review GetReview(int reviewID)
        {
            Review reviewDetail = null;

            int userID, prodID, rating;
            string desc, datetime, lastedit;

            string queryStr = "SELECT * FROM Review WHERE ReviewID = @reviewID";

            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@reviewID", reviewID);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                userID = int.Parse(dr["UserID"].ToString());
                prodID = int.Parse(dr["ProdID"].ToString());
                rating = int.Parse(dr["ReviewRating"].ToString());
                desc = dr["ReviewDesc"].ToString();
                datetime = dr["ReviewDateTime"].ToString();
                lastedit = dr["ReviewLastEdit"].ToString();

                reviewDetail = new Review(reviewID, userID, prodID, rating, desc, datetime, lastedit);

            }
            else
            {
                reviewDetail = null;
            }

            conn.Close();
            dr.Close();
            dr.Dispose();

            return reviewDetail;
        }

        public int UpdateReview(int reviewID, int rating, string desc, string lastedit)
        {
            string queryStr = "UPDATE Review SET" + " ReviewRating = @rating, " + " ReviewDesc = @desc," + " ReviewLastEdit = @lastedit" + " WHERE ReviewID = @reviewID";

            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);

            cmd.Parameters.AddWithValue("@reviewID", reviewID);
            cmd.Parameters.AddWithValue("@rating", rating);
            cmd.Parameters.AddWithValue("@desc", desc);
            cmd.Parameters.AddWithValue("@lastedit", lastedit);

            conn.Open();
            int nofRow = 0;
            nofRow = cmd.ExecuteNonQuery();

            conn.Close();
            return nofRow;
        }

        public int DeleteReview(int reviewID)
        {
            string queryStr = "DELETE FROM Review WHERE ReviewID = @reviewID";

            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);

            cmd.Parameters.AddWithValue("@reviewID", reviewID);

            conn.Open();
            int nOfRow = 0;
            nOfRow = cmd.ExecuteNonQuery();

            conn.Close();

            return nOfRow;
        }

        public List<Review> getReviewAll()
        {
            List<Review> reviewList = new List<Review>();
            int reviewID, userID, prodID, rating;
            string desc, datetime, lastedit;

            string queryStr = "SELECT * FROM Review"; // SQL
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                reviewID = int.Parse(dr["ReviewID"].ToString());
                userID = int.Parse(dr["UserID"].ToString());
                prodID = int.Parse(dr["ProdID"].ToString());
                rating = int.Parse(dr["ReviewRating"].ToString());
                desc = dr["ReviewDesc"].ToString();
                datetime = dr["ReviewDateTime"].ToString();
                lastedit = dr["ReviewLastEdit"].ToString();

                Review rev = new Review(reviewID, userID, prodID, rating, desc, datetime, lastedit);


                reviewList.Add(rev);
            }
            conn.Close();
            dr.Close();
            dr.Dispose();

            return reviewList;
        }
        public int DeleteReviewsByUserID(int userID)
        {
            string queryStr = "DELETE FROM Review WHERE UserID = @userID";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@userID", userID);

                    conn.Open();
                    int nOfRowsAffected = cmd.ExecuteNonQuery();
                    return nOfRowsAffected;
                }
            }
        }

    }
}