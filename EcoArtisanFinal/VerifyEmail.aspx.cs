using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class VerifyEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void verify_btn_click(object sender, EventArgs e)
        {
            string enteredCode = code_TB.Text;

            // Get the saved verification code and user email from the session
            string savedCode = Session["ConfirmationCode"].ToString();
            string userEmail = Session["UserEmail"].ToString();

            if (enteredCode == savedCode && !string.IsNullOrEmpty(userEmail))
            {
                // Verification successful
                Response.Write($"<script>alert('Verification Successful!')</script>");
                // Redirect to the password reset page
                Response.Redirect("/PasswordResetPage");
            }
            else
            {
                Response.Write($"<script>alert('Incorrect code, please check again')</script>");
                Response.Write($"<script>alert('savedCode = {savedCode}, {userEmail}')</script>");
                // Verification failed
                // You may display an error message or redirect back to the verification page
                code_TB.Text = "";
            }

        }
    }
}