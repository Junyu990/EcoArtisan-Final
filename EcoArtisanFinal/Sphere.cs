using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EcoArtisanFinal
{
    public class Sphere
    {
        string _connStr = ConfigurationManager.ConnectionStrings["EcoArtisanDBContext"].ConnectionString;
        private int _sphereID = 0;
        private int _userID = 0;
        private string _sphereimage = "";
        private string _spheredesc = "";
        private decimal _sphereprice = 0;
        private string _spheremat = "";
        private string _spherecolour = "";
        private string _spheresize = "";

        public Sphere()
        {

        }

        public Sphere(int sphereID, int userID, string sphereimage, string spheredesc, decimal sphereprice, string spheremat, string spherecolour, string spheresize)
        {
            _sphereID = sphereID;
            _userID = userID;
            _sphereimage = sphereimage;
            _spheredesc = spheredesc;
            _sphereprice = sphereprice;
            _spheremat = spheremat;
            _spherecolour = spherecolour;
            _spheresize = spheresize;
        }

        // Constructor without SphereID
        public Sphere(int userID, string sphereImage, string sphereDesc, decimal spherePrice, string sphereMat, string sphereColour, string sphereSize)
        {
            this.UserID = userID;
            this.SphereImage = sphereImage;
            this.SphereDesc = sphereDesc;
            this.SpherePrice = spherePrice;
            this.SphereMat = sphereMat;
            this.SphereColour = sphereColour;
            this.SphereSize = sphereSize;
        }

        public int SphereID
        {
            get { return _sphereID; }
            set { _sphereID = value; }
        }
        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }
        public string SphereImage
        {
            get { return _sphereimage; }
            set { _sphereimage = value; }
        }
        public string SphereDesc
        {
            get { return _spheredesc; }
            set { _spheredesc = value; }
        }
        public decimal SpherePrice
        {
            get { return _sphereprice; }
            set { _sphereprice = value; }
        }
        public string SphereMat
        {
            get { return _spheremat; }
            set { _spheremat = value; }
        }
        public string SphereColour
        {
            get { return _spherecolour; }
            set { _spherecolour = value; }
        }
        public string SphereSize
        {
            get { return _spheresize; }
            set { _spheresize = value; }
        }

        public Sphere SphereRead(int sphereID)
        {
            Sphere sphereDetail = null;

            string SphereImage, SphereDesc, SphereMat, SphereColour, SphereSize;
            int UserID;
            decimal SpherePrice;
            string queryStr = "SELECT * FROM [Sphere] WHERE SphereID = @SphereID";

            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("SphereID", sphereID);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                UserID = int.Parse(dr["UserID"].ToString());
                SphereImage = dr["SphereImage"].ToString();
                SphereDesc = dr["SphereDesc"].ToString();
                SpherePrice = decimal.Parse(dr["SpherePrice"].ToString());
                SphereMat = dr["SphereMat"].ToString();
                SphereColour = dr["SphereColour"].ToString();
                SphereSize = dr["SphereSize"].ToString();

                sphereDetail = new Sphere(sphereID, UserID, SphereImage, SphereDesc, SpherePrice, SphereMat, SphereColour, SphereSize);
            }
            else
            {
                sphereDetail = null;
            }

            conn.Close();
            dr.Close();
            dr.Dispose();

            return sphereDetail;

        }
        public int SphereCreate()
        {
            int newSphereID = 0;
            // Adjusted query to capture the ID of the newly inserted sphere
            string queryStr = "INSERT INTO Sphere(UserID, SphereImage, SphereDesc, SpherePrice, SphereMat, SphereColour, SphereSize)"
                            + " OUTPUT INSERTED.SphereID"
                            + " VALUES(@UserID, @SphereImage, @SphereDesc, @SpherePrice, @SphereMat, @SphereColour, @SphereSize);";

            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            // Add parameters as before
            cmd.Parameters.AddWithValue("@UserID", this.UserID);
            cmd.Parameters.AddWithValue("@SphereImage", this.SphereImage);
            cmd.Parameters.AddWithValue("@SphereDesc", this.SphereDesc);
            cmd.Parameters.AddWithValue("@SpherePrice", this.SpherePrice);
            cmd.Parameters.AddWithValue("@SphereMat", this.SphereMat);
            cmd.Parameters.AddWithValue("@SphereColour", this.SphereColour);
            cmd.Parameters.AddWithValue("@SphereSize", this.SphereSize);

            conn.Open();
            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                newSphereID = Convert.ToInt32(result);
            }
            conn.Close();

            return newSphereID; // Returns the ID of the newly inserted sphere
        }
        public int SphereDelete(string sphereID)
        {
            string queryStr = "DELETE FROM Sphere WHERE SphereID=@SphereID";
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@SphereID", sphereID);
            conn.Open();
            int nofRow = 0;
            nofRow = cmd.ExecuteNonQuery();
            conn.Close();
            return nofRow;

        }
        public int SphereUpdate(int sphereID, int UserID, string SphereImage, string SphereDesc, decimal SpherePrice, string SphereMat, string SphereColour, string SphereSize)
        {
            string queryStr = "UPDATE Sphere SET " +
                      "UserID = @UserID, " +
                      "SphereImage = @SphereImage, " +
                      "SphereDesc = @SphereDesc, " +
                      "SpherePrice = @SpherePrice, " +
                      "SphereMat = @SphereMat, " +
                      "SphereColour = @SphereColour, " +
                      "SphereSize = @SphereSize " +
                      "WHERE SphereID = @SphereID";

            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@SphereID", sphereID);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@SphereImage", SphereImage);
            cmd.Parameters.AddWithValue("@SphereDesc", SphereDesc);
            cmd.Parameters.AddWithValue("@SpherePrice", SpherePrice);
            cmd.Parameters.AddWithValue("@SphereMat", SphereMat);
            cmd.Parameters.AddWithValue("@SphereColour", SphereColour);
            cmd.Parameters.AddWithValue("@SphereSize", SphereSize);

            conn.Open();
            int nofRow = 0;
            nofRow = cmd.ExecuteNonQuery();

            conn.Close();

            return nofRow;
        }

        public List<Sphere> GetAllSpheres()
        {
            List<Sphere> spheres = new List<Sphere>();
            string queryStr = "SELECT * FROM Sphere";
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand(queryStr, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Sphere sphere = new Sphere
                    {
                        SphereID = int.Parse(dr["SphereID"].ToString()),
                        UserID = int.Parse(dr["UserID"].ToString()),
                        SphereImage = dr["SphereImage"].ToString(),
                        SphereDesc = dr["SphereDesc"].ToString(),
                        SpherePrice = decimal.Parse(dr["SpherePrice"].ToString()),
                        SphereMat = dr["SphereMat"].ToString(),
                        SphereColour = dr["SphereColour"].ToString(),
                        SphereSize = dr["SphereSize"].ToString()
                    };
                    spheres.Add(sphere);
                }
            }

            return spheres;
        }

        public List<Sphere> GetSpheresByUserID(int userID)
        {
            List<Sphere> spheres = new List<Sphere>();

            string queryStr = "SELECT * FROM Sphere WHERE UserID = @UserID";
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand(queryStr, conn);
                cmd.Parameters.AddWithValue("@UserID", userID);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Sphere sphere = new Sphere
                    {
                        SphereID = int.Parse(dr["SphereID"].ToString()),
                        UserID = int.Parse(dr["UserID"].ToString()),
                        SphereImage = dr["SphereImage"].ToString(),
                        SphereDesc = dr["SphereDesc"].ToString(),
                        SpherePrice = decimal.Parse(dr["SpherePrice"].ToString()),
                        SphereMat = dr["SphereMat"].ToString(),
                        SphereColour = dr["SphereColour"].ToString(),
                        SphereSize = dr["SphereSize"].ToString()
                    };
                    spheres.Add(sphere);
                }
            }

            return spheres;
        }
        public int DeleteSphereByUserID(int userID)
        {
            string queryStr = "DELETE FROM Sphere WHERE UserID = @userID";

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
        public static List<UserSphereStatistics> GetUserSphereStatistics()
        {
            List<UserSphereStatistics> statsList = new List<UserSphereStatistics>();
            string connectionString = ConfigurationManager.ConnectionStrings["EcoArtisanDBContext"].ConnectionString;
            string query = @"
        SELECT 
            UserID,
            COUNT(SphereID) AS TotalSpheres
        FROM 
            [Sphere]
        GROUP BY 
            UserID";

            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserSphereStatistics stats = new UserSphereStatistics
                            {
                                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                TotalSpheres = reader.GetInt32(reader.GetOrdinal("TotalSpheres"))
                            };
                            statsList.Add(stats);
                        }
                    }
                }
            }
            return statsList;
        }

        public class UserSphereStatistics
        {
            public int UserID { get; set; }
            public int TotalSpheres { get; set; }
        }

        public static List<BubbleChartData> GetBubbleChartData()
        {
            List<BubbleChartData> bubbleChartData = new List<BubbleChartData>();

            // Example logic to fetch bubble chart data from the database
            // Replace this with your actual data retrieval logic
            string connectionString = ConfigurationManager.ConnectionStrings["EcoArtisanDBContext"].ConnectionString;
            string query = @"
                SELECT SphereSize, COUNT(*) AS SpheresOrdered
                FROM Sphere
                GROUP BY SphereSize";

            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string sphereSize = reader.GetString(reader.GetOrdinal("SphereSize"));
                            int spheresOrdered = reader.GetInt32(reader.GetOrdinal("SpheresOrdered"));
                            bubbleChartData.Add(new BubbleChartData(sphereSize, spheresOrdered));
                        }
                    }
                }
            }

            return bubbleChartData;
        }

        public class BubbleChartData
        {
            public string SphereSize { get; set; }
            public int SpheresOrdered { get; set; }

            public BubbleChartData(string sphereSize, int spheresOrdered)
            {
                SphereSize = sphereSize;
                SpheresOrdered = spheresOrdered;
            }
        }




    }
}