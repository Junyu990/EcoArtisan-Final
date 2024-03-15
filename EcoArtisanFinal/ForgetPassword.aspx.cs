using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;


namespace EcoArtisanFinal
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void resetpass_btn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string userEmail = email_TB.Text;

                // Check if the email exists in the database
                if (IsEmailRegistered(userEmail))
                {
                    // Generate a random confirmation code
                    string verificationCode = GenerateVerificationCode();

                    // Save necessary data in Session for later use
                    Session["UserEmail"] = userEmail;
                    Session["ConfirmationCode"] = verificationCode;

                    // Redirect to the next step
                    // Send confirmation email
                    SendConfirmationEmail(userEmail, verificationCode);
                    Response.Redirect("~/VerifyEmail");
                }
                else
                {
                    Response.Write("<script>alert('Email is not registered');</script>");
                    email_TB.Text = "";
                }
            }
            else
            {
                Response.Write("<script>alert('Page not Valid');</script>");
            }
        }
        public static string GenerateVerificationCode()
        {
            // Generate a random 6-digit verification code
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
        private void SendConfirmationEmail(string userEmail, string confirmationCode)
        {
            try
            {
                // Configure your Google email SMTP settings

                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
                string smtpUsername = "EcoArtisanOfficial@gmail.com";
                string smtpPassword = "aciz ipbk llia wlfn";

                // Create a MailMessage
                MailMessage mail = new MailMessage();
                mail.To.Add(userEmail);
                mail.From = new MailAddress("EcoArtisanOfficial@gmail.com");
                mail.Subject = "Confirmation Code for EcoArtisan";
                mail.Body = $"Your verification code is: {confirmationCode}";
                mail.IsBodyHtml = true;

                // Create a SmtpClient
                SmtpClient smtp = new SmtpClient(smtpServer, smtpPort);
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword);

                // Send the email
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                // Handle exceptions (log or display an error message)
                Response.Write($"<script>alert('Error sending email: {ex.Message}');</script>");
            }
        }

        private bool IsEmailRegistered(string userEmail)
        {
            bool isRegistered = false;

            string queryStr = "SELECT COUNT(*) FROM [User] WHERE UserEmail = @UserEmail";
            string _connStr = ConfigurationManager.ConnectionStrings["EcoArtisanDBContext"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserEmail", userEmail);

                        conn.Open();

                        int count = (int)cmd.ExecuteScalar();

                        // If count is greater than 0, the email is registered
                        if (count > 0)
                        {
                            isRegistered = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception if needed
                Console.WriteLine(ex.Message);
            }

            return isRegistered;
        }
    }
}