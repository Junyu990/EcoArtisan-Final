using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Configuration;

namespace EcoArtisanFinal
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Default.aspx");
            }
            Session["UserID"] = null;
            Session["UserName"] = null;
            Session["UserEmail"] = null;
            Session["Points"] = null;
            Session["UserDetail"] = null;
            Session["Role"] = null;
        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {
            string userEmail = tb_LoginEmail.Text.Trim();
            string password = tb_LoginPassword.Text;

            User user = new User();
            int loginResult = user.ValidateUser(userEmail, password);

            if (loginResult == 1)
            {
                // Authentication successful, store user information in session
                Session["UserID"] = user.UserID;
                Session["UserName"] = user.UserName;
                Session["UserEmail"] = user.UserEmail;
                Session["Points"] = user.Points;

                // Authentication successful, redirect to the homepage or another secure page
                string role = user.GetRoleByEmail(userEmail);
                if (role == "Admin")
                {
                    // If user is admin, redirect to AdminHome
                    Response.Redirect("~/AdminHome.aspx");
                }
                else if (role == "Customer")
                {
                    // If user is customer, redirect to the homepage or another secure page
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    // If user role is not recognized, handle it accordingly
                    Response.Write($"<script>alert('Unknown user role.');</script>");
                }
            }
            else if (loginResult == 2)
            {
                // Invalid password
                Response.Write($"<script>alert('Invalid password.');</script>");
            }
            else if (loginResult == 3)
            {
                // Invalid email
                Response.Write($"<script>alert('Invalid email.');</script>");
            }
            else
            {
                // An error occurred
                Response.Write($"<script>alert('An error occurred.');</script>");
            }
        }
        public static User GetUserByEmailAndPassword(string userEmail, string password)
        {
            User user = null;

            string queryStr = "SELECT * FROM [User] WHERE UserEmail = @UserEmail AND UserPassword = @UserPassword";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EcoArtisanDBContext"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@UserEmail", userEmail);
                    cmd.Parameters.AddWithValue("@UserPassword", password);

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        int userID = Convert.ToInt32(dr["UserID"]);
                        string userName = dr["UserName"].ToString();
                        string userEmailDb = dr["UserEmail"].ToString();
                        string userPasswordDb = dr["UserPassword"].ToString();
                        int points = Convert.ToInt32(dr["Points"]);
                        string signUpDate = dr["SignUpDate"].ToString();

                        user = new User(userID, userName, userEmailDb, userPasswordDb, points, signUpDate);
                    }

                    dr.Close();
                }
            }

            return user;
        }
    }
}