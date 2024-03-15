using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class PasswordResetPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserEmail_TB.Text = Session["UserEmail"].ToString();
        }

        protected void verify_btn_Click(object sender, EventArgs e)
        {
            string userEmail = Session["UserEmail"].ToString();
            string newPassword = passTB.Text;
            string confirmPassword = confirmpassTB.Text;


            // Validate inputs (you may add more validation)
            if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                // Handle invalid input
                Response.Write("<script>alert('Please enter all fields.')</script>");
                // You may display an error message to the user
                passTB.Text = "";
                confirmpassTB.Text = "";
                return;
            }


            // Instantiate the User class
            User user = new User();

            // Update the password
            int rowsAffected = user.UpdatePasswordByEmail(userEmail, newPassword);
            if (rowsAffected > 0)
            {
                Response.Write("<script>alert('Password reset successful.')</script>");
            }

            if (rowsAffected > 0)
            {
                // Password update successful
                Response.Redirect("~/Login");
            }
            else
            {
                Response.Write("<script>alert('Password reset unsuccessful.')</script>");
                // You may display an error message to the user
                passTB.Text = "";
                confirmpassTB.Text = "";
            }
        }
    }
}