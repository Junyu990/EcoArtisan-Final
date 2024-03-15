using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace EcoArtisanFinal
{
    public class Reward
    {
        string _connStr = ConfigurationManager.ConnectionStrings["EcoArtisanDBContext"].ConnectionString;
        private int _ItemID = 0;
        private string _ItemName = string.Empty;
        private string _ItemDescription = string.Empty;
        private string _FilterClass = string.Empty;
        private string _ImageURL = string.Empty;
        private int _Points = 0;
        private double _Discount = 0;

        public Reward(string ItemName, string Desc, string FilterClass, string ImageURL, int Points, double Discount)
        {
            _ItemName = ItemName;
            _ItemDescription = Desc;
            _FilterClass = FilterClass;
            _ImageURL = ImageURL;
            _Points = Points;
            _Discount = Discount;
        }

        public Reward()
        {
        }

        public int ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }

        public string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; }
        }

        public string Desc
        {
            get { return _ItemDescription; }
            set { _ItemDescription = value; }
        }

        public string FilterClass
        {
            get { return _FilterClass; }
            set { _FilterClass = value; }
        }

        public string ImageURL
        {
            get { return _ImageURL; }
            set { _ImageURL = value; }
        }

        public int Points
        {
            get { return _Points; }
            set { _Points = value; }
        }

        public double Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }

        public int InsertReward(string itemName, string Desc, string filterClass, string imageURL, int points, double discount)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    string query = @"INSERT INTO [Reward] (ItemName, ItemDescription, FilterClass, ImageURL, Points, Discount)
                                 VALUES (@ItemName, @ItemDescription, @FilterClass, @ImageURL, @Points, @Discount)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ItemName", itemName);
                        cmd.Parameters.AddWithValue("@ItemDescription", Desc);
                        cmd.Parameters.AddWithValue("@FilterClass", filterClass);
                        cmd.Parameters.AddWithValue("@ImageURL", imageURL);
                        cmd.Parameters.AddWithValue("@Points", points);
                        cmd.Parameters.AddWithValue("@Discount", discount);

                        conn.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine(ex.Message);
            }

            return rowsAffected;
        }

        public int UpdateReward(int id, string itemName, string Desc, string filterClass, string imageURL, int points, double discount)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    string query = @"UPDATE [Reward]
                             SET ItemName = @ItemName,
                                 ItemDescription = @ItemDescription,
                                 FilterClass = @FilterClass,
                                 ImageURL = @ImageURL,
                                 Points = @Points,
                                 Discount = @Discount
                             WHERE ID = @ID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ItemName", itemName);
                        cmd.Parameters.AddWithValue("@ItemDescription", Desc);
                        cmd.Parameters.AddWithValue("@FilterClass", filterClass);
                        cmd.Parameters.AddWithValue("@ImageURL", imageURL);
                        cmd.Parameters.AddWithValue("@Points", points);
                        cmd.Parameters.AddWithValue("@Discount", discount);
                        cmd.Parameters.AddWithValue("@ID", id);

                        conn.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine(ex.Message);
            }

            return rowsAffected;
        }

        public int DeleteReward(int id)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    string query = "DELETE FROM [Reward] WHERE ID = @ID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);

                        conn.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine(ex.Message);
            }

            return rowsAffected;
        }

        public Reward GetRewardItemById(int id)
        {
            Reward reward = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    string query = "SELECT * FROM [Reward] WHERE ID = @ID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                reward = new Reward
                                {
                                    ItemID = Convert.ToInt32(reader["ID"]),
                                    ItemName = reader["ItemName"].ToString(),
                                    Desc = reader["ItemDescription"].ToString(),
                                    FilterClass = reader["FilterClass"].ToString(),
                                    ImageURL = reader["ImageURL"].ToString(),
                                    Points = Convert.ToInt32(reader["Points"]),
                                    Discount = Convert.ToDouble(reader["Discount"])
                                };
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

            return reward;
        }

        public List<Reward> GetAllRewardItems()
        {
            List<Reward> rewards = new List<Reward>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    string query = "SELECT * FROM [Reward]";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Reward reward = new Reward
                                {
                                    ItemID = Convert.ToInt32(reader["ID"]),
                                    ItemName = reader["ItemName"].ToString(),
                                    Desc = reader["ItemDescription"].ToString(),
                                    FilterClass = reader["FilterClass"].ToString(),
                                    ImageURL = reader["ImageURL"].ToString(),
                                    Points = Convert.ToInt32(reader["Points"]),
                                    Discount = Convert.ToDouble(reader["Discount"])
                                };

                                rewards.Add(reward);
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

            return rewards;
        }
    }
}