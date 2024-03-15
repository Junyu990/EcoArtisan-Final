using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace EcoArtisanFinal
{
    public class UserDetails
    {
        string _connStr = ConfigurationManager.ConnectionStrings["EcoArtisanDBContext"].ConnectionString;
        private int _UserID = 0;
        private string _UserName = null;
        private string _FirstName = null;
        private string _LastName = null;
        private string _MobileNumber = null;
        private string _Birthdate = null;
        private string _Country = null;
        private string _AddressLine1 = null;
        private string _AddressLine2 = null;
        private string _Postcode = null;
        private string _State = null;
        private string _Area = null;
        private string _Email = null;
        private string _ProfilePicture = null;
        private string _Gender = null;

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
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public string MobileNumber
        {
            get { return _MobileNumber; }
            set { _MobileNumber = value; }
        }

        public string Birthdate
        {
            get { return _Birthdate; }
            set { _Birthdate = value; }
        }

        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        public string AddressLine1
        {
            get { return _AddressLine1; }
            set { _AddressLine1 = value; }
        }

        public string AddressLine2
        {
            get { return _AddressLine2; }
            set { _AddressLine2 = value; }
        }

        public string Postcode
        {
            get { return _Postcode; }
            set { _Postcode = value; }
        }

        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        public string Area
        {
            get { return _Area; }
            set { _Area = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string ProfilePicture
        {
            get { return _ProfilePicture; }
            set { _ProfilePicture = value; }
        }

        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        public UserDetails()
        {
        }

        public UserDetails(int userid, string userName, string firstName, string lastName, string mobileNumber, string birthdate, string country, string addressLine1, string addressLine2, string postcode, string state, string area, string email, string profilePicture, string gender)
        {
            UserID = userid;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            MobileNumber = mobileNumber;
            Birthdate = birthdate;
            Country = country;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            Postcode = postcode;
            State = state;
            Area = area;
            Email = email;
            ProfilePicture = profilePicture;
            Gender = gender;
        }

        public UserDetails getUserByUserID(int userID)
        {
            UserDetails userDetail = null;
            string userName, firstName, lastName, mobileNumber, country, addressLine1, addressLine2, postcode, state, area, email, profilePicture, birthdate, gender;

            string queryStr = "SELECT * FROM [UserDetails] WHERE UserID = @UserID";

            try
            {
                SqlConnection conn = new SqlConnection(_connStr);
                SqlCommand cmd = new SqlCommand(queryStr, conn);
                cmd.Parameters.AddWithValue("@UserID", userID);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    userName = dr["UserName"].ToString();
                    firstName = dr["FirstName"].ToString();
                    lastName = dr["LastName"].ToString();
                    mobileNumber = dr["MobileNumber"].ToString();
                    birthdate = dr["Birthdate"].ToString();
                    country = dr["Country"].ToString();
                    addressLine1 = dr["AddressLine1"].ToString();
                    addressLine2 = dr["AddressLine2"].ToString();
                    postcode = dr["Postcode"].ToString();
                    state = dr["State"].ToString();
                    area = dr["Area"].ToString();
                    email = dr["Email"].ToString();
                    profilePicture = dr["ProfilePicture"].ToString();
                    gender = dr["Gender"].ToString();

                    userDetail = new UserDetails(userID, userName, firstName, lastName, mobileNumber, birthdate, country, addressLine1, addressLine2, postcode, state, area, email, profilePicture, gender);
                }
                else
                {
                    userDetail = null;
                }
                conn.Close();
                dr.Close();
                dr.Dispose();

                return userDetail;

            }
            catch (Exception ex)
            {
                // Handle the exception if needed
                Console.WriteLine(ex.Message);
            }

            return userDetail;
        }
        public int InsertUserDetails()
        {

            int result = 0;

            string queryStr = "INSERT INTO [UserDetails] (UserID, UserName, FirstName, LastName, MobileNumber, Birthdate, Country, AddressLine1, AddressLine2, Postcode, State, Area, Email, ProfilePicture, Gender)" +
                              " VALUES (@UserID, @UserName, @FirstName, @LastName, @MobileNumber, @Birthdate, @Country, @AddressLine1, @AddressLine2, @Postcode, @State, @Area, @Email, @ProfilePicture, @Gender)";

            try
            {
                SqlConnection conn = new SqlConnection(_connStr);
                SqlCommand cmd = new SqlCommand(queryStr, conn);
                cmd.Parameters.AddWithValue("@UserID", this.UserID);
                cmd.Parameters.AddWithValue("@UserName", this.UserName);
                cmd.Parameters.AddWithValue("@FirstName", this.FirstName);
                cmd.Parameters.AddWithValue("@LastName", this.LastName);
                cmd.Parameters.AddWithValue("@MobileNumber", this.MobileNumber);
                cmd.Parameters.AddWithValue("@Birthdate", this.Birthdate);
                cmd.Parameters.AddWithValue("@Country", this.Country);
                cmd.Parameters.AddWithValue("@AddressLine1", this.AddressLine1);
                cmd.Parameters.AddWithValue("@AddressLine2", this.AddressLine2);
                cmd.Parameters.AddWithValue("@Postcode", this.Postcode);
                cmd.Parameters.AddWithValue("@State", this.State);
                cmd.Parameters.AddWithValue("@Area", this.Area);
                cmd.Parameters.AddWithValue("@Email", this.Email);
                cmd.Parameters.AddWithValue("@ProfilePicture", this.ProfilePicture);
                cmd.Parameters.AddWithValue("@Gender", this.Gender);

                conn.Open();
                result += cmd.ExecuteNonQuery();
                conn.Close();

                // Update user information in the User class
                //User userInstance = new User();
                //userInstance.UpdateUserInformation(this.UserID, this.UserName, this.Email);


            }
            catch (Exception ex)
            {
                // Handle the exception if needed
                Console.WriteLine("Error inserting user details: " + ex.Message);
                Console.WriteLine("Query: " + queryStr);
            }

            return result;
        }

        public int UpdateUserDetails()
        {
            int result = 0;

            string queryStr = "UPDATE [UserDetails] SET UserName = @UserName, FirstName = @FirstName, LastName = @LastName, " +
                              "MobileNumber = @MobileNumber, Birthdate = @Birthdate, Country = @Country, AddressLine1 = @AddressLine1, " +
                              "AddressLine2 = @AddressLine2, Postcode = @Postcode, State = @State, Area = @Area, " +
                              "Email = @Email, ProfilePicture = @ProfilePicture, Gender = @Gender WHERE UserID = @UserID";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", this.UserID);
                        cmd.Parameters.AddWithValue("@UserName", this.UserName);
                        cmd.Parameters.AddWithValue("@FirstName", this.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", this.LastName);
                        cmd.Parameters.AddWithValue("@MobileNumber", this.MobileNumber);
                        cmd.Parameters.AddWithValue("@Birthdate", this.Birthdate);
                        cmd.Parameters.AddWithValue("@Country", this.Country);
                        cmd.Parameters.AddWithValue("@AddressLine1", this.AddressLine1);
                        cmd.Parameters.AddWithValue("@AddressLine2", this.AddressLine2);
                        cmd.Parameters.AddWithValue("@Postcode", this.Postcode);
                        cmd.Parameters.AddWithValue("@State", this.State);
                        cmd.Parameters.AddWithValue("@Area", this.Area);
                        cmd.Parameters.AddWithValue("@Email", this.Email);
                        cmd.Parameters.AddWithValue("@ProfilePicture", this.ProfilePicture);
                        cmd.Parameters.AddWithValue("@Gender", this.Gender);

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
    }
}