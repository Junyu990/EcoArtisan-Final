using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
namespace EcoArtisanFinal
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["UserEmail"] == null || string.IsNullOrEmpty(Session["UserEmail"].ToString()))
            {
                string script = "alert('Please log in first.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                // Redirect the user to the login page if UserID is not set in the session
                Response.Redirect("/Login");
            }
            else
            {
                try
                {
                    // Configure your Google email SMTP settings

                    string smtpServer = "smtp.gmail.com";
                    int smtpPort = 587;
                    string smtpUsername = "EcoArtisanOfficial@gmail.com";
                    string smtpPassword = "aciz ipbk llia wlfn";

                    /// Get the user ID from the session
                    string userEmail = Session["UserEmail"].ToString();
                    string userID = Session["UserID"].ToString();
                    string userName = Session["UserName"].ToString();

                    // Create a new MailMessage
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("EcoArtisanOfficial@gmail.com"); // Sender's email address
                    mail.To.Add("EcoArtisanOfficial@gmail.com"); // Recipient's email address
                    mail.Subject = "Customer Inquiry: " + userName; // Subject of the email

                    // Construct the HTML-formatted body of the email
                    string htmlBody = "<html><body>";
                    htmlBody += "<h4>Customer Particulars: </h4>";
                    htmlBody += "<p>User ID: " + userID + "</p>";
                    htmlBody += "<p>User Name: " + userName + "</p>";
                    htmlBody += "<p>User Email: " + userEmail + "</p>";
                    htmlBody += "<p><strong>Subject:</strong> " + messageSubject.Text + "</p>";
                    htmlBody += "<p><strong>Message:</strong> " + txtMessage.Text + "</p>";
                    htmlBody += "</body></html>";


                    mail.Body = htmlBody;

                    // Specify that the body of the email is HTML-formatted
                    mail.IsBodyHtml = true;
                    // Create a SmtpClient
                    SmtpClient smtp = new SmtpClient(smtpServer, smtpPort);
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword);

                    // Send the email
                    smtp.Send(mail);

                    Response.Write("<script>alert('Email Sent! We will get back to you as soon as possible!')</script>");
                    messageSubject.Text = "";
                    txtMessage.Text = "";
                }
                catch (Exception ex)
                {
                    // Handle exceptions (log or display an error message)
                    Response.Write($"<script>alert('Error sending email: {ex.Message}');</script>");
                }
            }
        }
    }
}