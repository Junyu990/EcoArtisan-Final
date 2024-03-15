using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class EmailVerification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_ConfirmCode_Click(object sender, EventArgs e)
        {
            string enteredCode = tb_ConfirmationCode.Text;
            string confirmationCode = Session["ConfirmationCode"] as string;
            if (!string.IsNullOrEmpty(enteredCode) && confirmationCode.Equals(enteredCode, StringComparison.OrdinalIgnoreCase))
            {
                // The entered code matches the generated one
                // Continue with user registration and database storage
                string UserName = Session["UserName"].ToString();
                string UserEmail = Session["UserEmail"].ToString();
                string UserPassword = Session["UserPassword"].ToString(); // Retrieve password from session
                int Points = 0;
                string SignUpDate = DateTime.Now.ToString();

                User newUser = new User(UserName, UserEmail, UserPassword, Points, SignUpDate);

                // Logging: Print User details
                Response.Write($"<script>alert('User details: {newUser.UserName}, {newUser.UserEmail}, {newUser.UserPassword}, {newUser.Points}, {newUser.SignUpDate}');</script>");

                int result = newUser.UserInsert();
                Response.Write($"<script>alert('rows affected: {result}');</script>");

                if (result > 0)
                {
                    // Insertion successful
                    lbl_Message.Text = "Sign Up Successful!";
                    lbl_Message.CssClass = "text-success";
                    Response.Redirect("~/Login");
                }
                else
                {
                    // Insertion failed
                    lbl_Message.Text = "Failed to store user in the database.";
                    lbl_Message.CssClass = "text-danger";
                }
            }
            else
            {
                tb_ConfirmationCode.Text = "";
                Response.Write("<script>alert('Wrong Code Entered!');</script>");
            }
        }
    }
}