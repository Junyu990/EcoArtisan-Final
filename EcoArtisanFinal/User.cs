using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace EcoArtisanFinal
{
    public class User
    {
        string _connStr = ConfigurationManager.ConnectionStrings["EcoArtisanDBContext"].ConnectionString;
        private int _UserID = 0;
        private string _UserName = string.Empty;
        private string _UserEmail = string.Empty;
        private string _UserPassword = string.Empty;
        private int _Points = 0;
        private string _SignUpDate = string.Empty;

        public User(int userID, string userName, string userEmail, string userPassword, int points, string signUpDate)
        {
            _UserID = userID;
            _UserName = userName;
            _UserEmail = userEmail;
            _UserPassword = userPassword;
            _Points = points;
            _SignUpDate = signUpDate;
        }

        public User()
        {
        }

        public User(string userName, string userEmail, string userPassword, int userPoints, string signUpDate)
        {
            UserName = userName;
            UserEmail = userEmail;
            UserPassword = userPassword;
            Points = userPoints;
            SignUpDate = signUpDate;
        }

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public string UserEmail
        {
            get { return _UserEmail; }
            set { _UserEmail = value; }
        }

        public string UserPassword
        {
            get { return _UserPassword; }
            set { _UserPassword = value; }
        }

        public int Points
        {
            get { return _Points; }
            set { _Points = value; }
        }

        public string SignUpDate
        {
            get { return _SignUpDate; }
            set { _SignUpDate = value; }
        }

        //Below as the Class methods for some DB operations. 
        public User getUserByUserID(int userID)
        {
            User userDetail = null;
            string userName, userEmail, userPassword, signUpDate;
            int points;

            string queryStr = "SELECT * FROM [User] WHERE UserID = @UserID";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);

                        conn.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                userName = dr["UserName"].ToString();
                                userEmail = dr["UserEmail"].ToString();
                                userPassword = dr["UserPassword"].ToString();
                                points = int.Parse(dr["Points"].ToString());
                                signUpDate = dr["SignUpDate"].ToString();

                                userDetail = new User(userID, userName, userEmail, userPassword, points, signUpDate);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception if needed
                Console.WriteLine(ex.Message);
            }

            return userDetail;
        }

        public enum LoginResult
        {
            Success,
            InvalidEmail,
            InvalidPassword,
            Error
        }

        public int ValidateUser(string userEmail, string password)
        {
            int result = 0; // 0 represents an error

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    string query = "SELECT * FROM [User] WHERE UserEmail = @UserEmail";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserEmail", userEmail);

                        conn.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                // User found, check password
                                if (dr["UserPassword"].ToString() == password)
                                {
                                    // Password is correct
                                    UserID = Convert.ToInt32(dr["UserID"]);
                                    UserName = dr["UserName"].ToString();
                                    UserEmail = userEmail;
                                    Points = Convert.ToInt32(dr["Points"]);
                                    result = 1; // 1 represents successful login
                                }
                                else
                                {
                                    // Invalid password
                                    result = 2; // 2 represents invalid password
                                }
                            }
                            else
                            {
                                // User not found
                                result = 3; // 3 represents invalid email
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception if needed
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public int UserInsert()
        {
            int result = 0;

            string queryStr = "INSERT INTO [User] (UserName, UserEmail, UserPassword, Points, SignUpDate)" +
                  " values (@UserName, @UserEmail, @UserPassword, @Points, @SignUpDate)";

            try
            {
                SqlConnection conn = new SqlConnection(_connStr);
                SqlCommand cmd = new SqlCommand(queryStr, conn);
                cmd.Parameters.AddWithValue("@UserName", this.UserName);
                cmd.Parameters.AddWithValue("@UserEmail", this.UserEmail);
                cmd.Parameters.AddWithValue("@UserPassword", this.UserPassword);
                cmd.Parameters.AddWithValue("@Points", this.Points);
                cmd.Parameters.AddWithValue("@SignUpDate", this.SignUpDate);

                conn.Open();
                result += cmd.ExecuteNonQuery();
                conn.Close();

                return result;
            }

            catch (SqlException ex)
            {
                // Handle the exception if needed
                Console.WriteLine("SQL Exception Message: " + ex.Message);
                Console.WriteLine("SQL Exception Number: " + ex.Number);
                Console.WriteLine("SQL Exception State: " + ex.State);
                Console.WriteLine("SQL Exception Class: " + ex.Class);
                // Add more details based on your needs

                return 0;
            }

            catch (Exception ex)
            {
                // Handle the exception if needed
                Console.WriteLine(ex.Message);
                return 0;
            }
        }//end Insert

        public int getUserIdByEmail(string userEmail)
        {
            int userId = 0;

            string queryStr = "SELECT UserID FROM [User] WHERE UserEmail = @UserEmail";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserEmail", userEmail);

                        conn.Open();

                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            userId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception if needed
                Console.WriteLine(ex.Message);
            }

            return userId;
        }
        public int UpdateUserInformation(int userID, string userName, string userEmail)
        {
            int result = 0;

            string queryStr = "UPDATE [User] SET UserName = @UserName, UserEmail = @UserEmail WHERE UserID = @UserID";

            try
            {
                SqlConnection conn = new SqlConnection(_connStr);
                SqlCommand cmd = new SqlCommand(queryStr, conn);
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@UserEmail", userEmail);
                cmd.Parameters.AddWithValue("@UserID", userID);

                conn.Open();
                result += cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                // Handle the exception if needed
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public int UserDelete(int userID)
        {
            int result = 0;

            Review rev = new Review();
            result += rev.DeleteReviewsByUserID(userID);

            Wishlist wish = new Wishlist();
            result += wish.DeleteWishlistItemsByUserID(userID);


            Redemption redeem = new Redemption();
            result += redeem.DeleteRedemptionByUserID(userID);

            Sphere sphere = new Sphere();
            result += sphere.DeleteSphereByUserID(userID);

            Order order = new Order();
            result += order.DeleteOrderByUserID(userID);

            Cart cart = new Cart();
            result += cart.DeleteCartItemsByUserID(userID);


            // Delete user details first
            result += DeleteUserDetails(userID);

            // Delete user
            string queryStr = "DELETE FROM [User] WHERE UserID = @UserID";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);

                        conn.Open();
                        result += cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception if needed
                Console.WriteLine(ex.Message);
            }

            return result;
        }
        private int DeleteUserDetails(int userID)
        {
            int result = 0;

            string queryStr = "DELETE FROM [UserDetails] WHERE UserID = @UserID";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);

                        conn.Open();
                        result += cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception if needed
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public int UpdatePasswordByEmail(string userEmail, string newPassword)
        {
            int result = 0;

            string queryStr = "UPDATE [User] SET UserPassword = @NewPassword WHERE UserEmail = @UserEmail";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                    {
                        cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                        cmd.Parameters.AddWithValue("@UserEmail", userEmail);

                        conn.Open();
                        result += cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception if needed
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public string GetRoleByEmail(string userEmail)
        {
            string role = null;

            string queryStr = "SELECT Role FROM [User] WHERE UserEmail = @UserEmail";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserEmail", userEmail);

                        conn.Open();

                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            role = Convert.ToString(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception if needed
                Console.WriteLine(ex.Message);
            }

            return role;
        }

        public bool CheckIfEmailExists(string userEmail)
        {
            bool emailExists = false;

            string queryStr = "SELECT COUNT(*) FROM [User] WHERE UserEmail = @UserEmail";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserEmail", userEmail);

                        conn.Open();

                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            emailExists = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception if needed
                Console.WriteLine(ex.Message);
            }

            return emailExists;
        }

        public int AddPoints(int userId, int pointsToAdd)
        {
            // Retrieve current points from the database
            int currentPoints = GetPoints(userId);

            // Update points
            int newPoints = currentPoints + pointsToAdd;

            // Update points in the database
            UpdatePoints(userId, newPoints);

            return newPoints;
        }

        public int DeductPoints(int userId, int pointsToDeduct)
        {
            // Retrieve current points from the database
            int currentPoints = GetPoints(userId);

            // Check if user has enough points
            if (currentPoints < pointsToDeduct)
            {
                // Not enough points
                return -1;
            }

            // Update points
            int newPoints = currentPoints - pointsToDeduct;

            // Update points in the database
            UpdatePoints(userId, newPoints);

            return newPoints;
        }

        public int GetPoints(int userId)
        {
            int points = 0;

            string query = "SELECT Points FROM [User] WHERE UserID = @UserID";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            points = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine(ex.Message);
            }

            return points;
        }


        public void UpdatePoints(int userId, int newPoints)
        {
            string query = "UPDATE [User] SET Points = @NewPoints WHERE UserID = @UserID";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NewPoints", newPoints);
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine(ex.Message);
            }
        }

        public List<User> GetAllCustomers()
        {
            List<User> customers = new List<User>();

            string query = "SELECT * FROM [User] WHERE Role = 'Customer'";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User customer = new User
                                {
                                    UserID = Convert.ToInt32(reader["UserID"]),
                                    UserName = reader["UserName"].ToString(),
                                    UserEmail = reader["UserEmail"].ToString(),
                                    UserPassword = reader["UserPassword"].ToString(),
                                    Points = Convert.ToInt32(reader["Points"]),
                                    SignUpDate = reader["SignUpDate"].ToString()
                                };

                                customers.Add(customer);
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

            return customers;
        }

        public List<User> GetAllAdmins()
        {
            List<User> admins = new List<User>();

            string query = "SELECT * FROM [User] WHERE Role = 'Admin'";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User admin = new User
                                {
                                    UserID = Convert.ToInt32(reader["UserID"]),
                                    UserName = reader["UserName"].ToString(),
                                    UserEmail = reader["UserEmail"].ToString(),
                                    UserPassword = reader["UserPassword"].ToString(),
                                    Points = Convert.ToInt32(reader["Points"]),
                                    SignUpDate = reader["SignUpDate"].ToString()
                                };

                                admins.Add(admin);
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

            return admins;
        }

        public int GiveAdminRole(int userId)
        {
            int rowsAffected = 0;

            string query = "UPDATE [User] SET Role = 'Admin' WHERE UserID = @UserID";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);

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

        public int RemoveAdminRole(int userId)
        {
            int rowsAffected = 0;

            string query = "UPDATE [User] SET Role = 'Customer' WHERE UserID = @UserID";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);

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
    }
}