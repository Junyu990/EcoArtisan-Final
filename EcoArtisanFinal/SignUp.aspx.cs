using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Net.Mail;

namespace EcoArtisanFinal
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_SignUp_Click(object sender, EventArgs e)
        {
            string response = captchacode.Text;
            if (!VerifyCaptcha(response))
            {
                // CAPTCHA verification failed
                Response.Write("<script>alert('Please complete the CAPTCHA verification');</script>");
                return;
            }
            else
            {
                if (Page.IsValid)
                {
                    string userName = tb_Username.Text;
                    string userEmail = tb_Email.Text;

                    User user = new User();
                    bool checkemail = user.CheckIfEmailExists(userEmail);
                    if (!checkemail)
                    {
                        // Generate a random confirmation code
                        string confirmationCode = GenerateConfirmationCode();

                        // Save necessary data in Session for later use
                        Session["UserName"] = userName;
                        Session["UserEmail"] = userEmail;
                        Session["ConfirmationCode"] = confirmationCode;

                        // Save the entered password in the session
                        Session["UserPassword"] = tb_Password.Text;

                        // Redirect to the next step
                        // Send confirmation email
                        SendConfirmationEmail(userEmail, confirmationCode);
                        Response.Redirect("~/EmailVerification");
                    }
                    else
                    {
                        Response.Write("<script>alert('Email Already Exists! Login?');</script>");
                        tb_Username.Text = "";
                        tb_ConfirmEmail.Text = "";
                        tb_Email.Text = "";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Page not Valid');</script>");
                }
            }

        }
        private bool VerifyCaptcha(string response)
        {
            if (captchacode.Text.ToLower() == Session["sessionCaptcha"].ToString())
            {
                return true;
            }
            else
            {
                Response.Write("<script>alert('Captcha code is incorrect. Please enter correct captcha code.');</script>");
                return false;
            }
        }
        public static string GenerateConfirmationCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var confirmationCode = new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return confirmationCode;
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
                mail.Body = $"Your confirmation code is: {confirmationCode}";
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
    }
}