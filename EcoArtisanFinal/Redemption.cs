using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EcoArtisanFinal
{
    public class Redemption
    {
        private string _connStr = ConfigurationManager.ConnectionStrings["EcoArtisanDBContext"].ConnectionString;

        public int RedemptionID { get; set; }
        public int UserID { get; set; }
        public int ItemID { get; set; }
        public DateTime RedemptionDate { get; set; }

        // Constructor
        public Redemption(int redemptionID, int userID, int itemID, DateTime redemptionDate)
        {
            RedemptionID = redemptionID;
            UserID = userID;
            ItemID = itemID;
            RedemptionDate = redemptionDate;
        }

        public Redemption()
        {
        }

        // Method to insert a new redemption record into the database
        public int InsertRedemption()
        {
            int redemptionID = -1; // Initialize with a default value

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    string query = @"INSERT INTO Redemption (UserID, ItemID, RedemptionDate)
                             VALUES (@UserID, @ItemID, @RedemptionDate);
                             SELECT SCOPE_IDENTITY();"; // Retrieve the ID of the inserted record

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@ItemID", ItemID);
                        cmd.Parameters.AddWithValue("@RedemptionDate", RedemptionDate);

                        conn.Open();
                        // ExecuteScalar is used to retrieve the first column of the first row of the result set
                        redemptionID = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine(ex.Message);
            }

            return redemptionID;
        }

        public Redemption GetDetailsByRedemptionID(int redemptionID)
        {
            Redemption redemption = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    string query = "SELECT * FROM Redemption WHERE RedemptionID = @RedemptionID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RedemptionID", redemptionID);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                redemption = new Redemption();
                                redemption.RedemptionID = Convert.ToInt32(reader["RedemptionID"]);
                                redemption.UserID = Convert.ToInt32(reader["UserID"]);
                                redemption.ItemID = Convert.ToInt32(reader["ItemID"]);
                                redemption.RedemptionDate = Convert.ToDateTime(reader["RedemptionDate"]);
                                // Add other columns as needed
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine(ex.Message);
            }

            return redemption;
        }

        public List<Reward> GetRedemptionsByUser(int userID)
        {
            List<Reward> rewards = new List<Reward>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    string query = @"SELECT r.RedemptionID, r.UserID, r.ItemID, r.RedemptionDate,
                                    rd.ItemName, rd.ItemDescription, rd.FilterClass, rd.ImageURL, rd.Points, rd.Discount
                             FROM Redemption r
                             JOIN Reward rd ON r.ItemID = rd.ID
                             WHERE r.UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Reward reward = new Reward
                            {
                                ItemID = Convert.ToInt32(reader["ItemID"]),
                                ItemName = reader["ItemName"].ToString(),
                                Desc = reader["ItemDescription"].ToString(),
                                FilterClass = reader["FilterClass"].ToString(),
                                ImageURL = reader["ImageURL"].ToString(),
                                Points = Convert.ToInt32(reader["Points"]),
                                Discount = Convert.ToDouble(reader["Discount"])
                            };

                            rewards.Add(reward);
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return rewards;
        }
        public int DeleteRedemptionByUserID(int userID)
        {
            string queryStr = "DELETE FROM Redemption WHERE UserID = @userID";

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