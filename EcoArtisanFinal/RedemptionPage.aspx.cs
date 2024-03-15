using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class RedemptionPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the information from session variables
                if (Session["SelectedRewardId"] != null)
                {
                    int selectedRewardId = (int)Session["SelectedRewardId"];
                    string selectedRewardName = Session["SelectedRewardName"].ToString();
                    string selectedDescription = Session["SelectedRewardDescription"].ToString();
                    int selectedRewardPoints = (int)Session["SelectedRewardPoints"];
                    string selectedRewardImageUrl = Session["SelectedRewardImageUrl"].ToString();

                    // Use the retrieved information as needed
                    // For example, you can set labels or images on the page
                    if (Session["UserID"] != null && !string.IsNullOrEmpty(Session["UserID"].ToString()))
                    {
                        int userId = int.Parse(Session["UserID"].ToString());

                        // Create an instance of the User class
                        User user = new User();

                        // Get the points for the user
                        int points = user.GetPoints(userId);

                        // Display the points in the ASPX page
                        pointsLabel.Text = $"You have {points.ToString()} EcoCoins!" + "<img class='threeleaf' src='../Content/Images/ecocoin.png'>";
                    }

                    lblRewardName.Text = selectedRewardName;
                    lblRewardDescription.Text = selectedDescription;
                    lblRewardPoints.Text = selectedRewardPoints.ToString();
                }
            }
            }

        protected void btnRedeem_Click(object sender, EventArgs e)
        {
            // Check if the user is logged in
            if (Session["UserID"] != null && !string.IsNullOrEmpty(Session["UserID"].ToString()))
            {
                int userId = int.Parse(Session["UserID"].ToString());

                // Get the points required to redeem the selected item
                int pointsRequired = int.Parse(lblRewardPoints.Text);

                // Create an instance of the User class
                User user = new User();

                // Get the user's current points
                int currentPoints = user.GetPoints(userId);

                // Check if the user has enough points to redeem the item
                if (currentPoints >= pointsRequired)
                {
                    // Deduct points from the user's account
                    int newPoints = user.DeductPoints(userId, pointsRequired);

                    // Check if points were deducted successfully
                    if (newPoints >= 0)
                    {
                        // Points deducted successfully
                        // Insert redemption record into the database
                        Redemption redemption = new Redemption();
                        redemption.UserID = userId;
                        redemption.ItemID = int.Parse(Session["SelectedRewardId"].ToString());
                        redemption.RedemptionDate = DateTime.Now;

                        // Insert redemption record
                        int redemptionID = redemption.InsertRedemption();

                        if (redemptionID > 0)
                        {
                            // Successful redemption
                            Redemption redeemedItem = redemption.GetDetailsByRedemptionID(redemptionID);

                            if (redeemedItem != null)
                            {
                                // Redirect the user to the redemption confirmation page and pass redemption details
                                Session["RedeemedItem"] = redeemedItem;
                                Response.Redirect("/RedemptionConfirmation");
                            }
                            else
                            {
                                // Redemption details not found
                                // Handle error or display a message
                                string errorMessage = "Redemption details not found. Please contact support for assistance.";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorAlert", "alert('" + errorMessage + "');", true);
                            }
                        }
                        else
                        {
                            // Error inserting redemption record
                            // Show an error message to the user
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error redeeming reward. Please try again later.');", true);
                        }
                    }
                    else
                    {
                        // Not enough points to redeem the item
                        // Show a message to the user indicating insufficient points
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You do not have enough points to redeem this reward.');", true);
                    }
                }
                else
                {
                    // Not enough points to redeem the item
                    // Show a message to the user indicating insufficient points
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You do not have enough points to redeem this reward.');", true);
                }
            }
            else
            {
                // User is not logged in
                // Redirect the user to the login page or display a message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please log in to redeem rewards.');", true);
            }
        }
    }
}