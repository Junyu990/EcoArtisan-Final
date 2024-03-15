using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class AdminRewards : System.Web.UI.Page
    {
        private List<Reward> rewardsData; // Declare rewardsData variable at the class level
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || string.IsNullOrEmpty(Session["UserID"].ToString()))
            {
                string script = "alert('Please log in first.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                // Redirect the user to the login page if UserID is not set in the session
                Response.Redirect("/Login");
            }
            else
            {
                // Get the user ID from the session
                int userId = int.Parse(Session["UserID"].ToString());

                // Create an instance of the User class
                User user = new User();

                // Get the points for the user
                int points = user.GetPoints(userId);
            }

            if (!IsPostBack)
            {
                BindRewards();
            }
        }
        private void BindRewards()
        {
            // Initialize rewardsData only if it's null

            if (rewardsData == null)
            {
                rewardsData = new List<Reward>();

                // Retrieve all reward items from the database
                Reward reward = new Reward();
                List<Reward> allRewards = reward.GetAllRewardItems(); // Assuming you have a method to fetch all reward items from the database

                // Add all fetched reward items to rewardsData list
                foreach (Reward item in allRewards)
                {
                    rewardsData.Add(item);
                }
            }

            // Bind the list of rewards to the repeater control
            rptRewards.DataSource = rewardsData;
            rptRewards.DataBind();
        }

        protected void rptRewards_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // filter-limited
            // filter-products
            if (e.CommandName == "Edit")
            {
                // Retrieve the ID of the clicked reward item
                int itemId = Convert.ToInt32(e.CommandArgument);

                // Retrieve the reward item's information based on the ID
                Reward reward = new Reward();
                Reward selectedItem = reward.GetRewardItemById(itemId);

                // Check if the selected item is not null
                if (selectedItem != null)
                {
                    // Store the relevant information in session variables
                    Session["SelectedRewardId"] = selectedItem.ItemID;
                    Session["SelectedRewardName"] = selectedItem.ItemName;
                    Session["SelectedRewardDescription"] = selectedItem.Desc;
                    Session["SelectedRewardFilterClass"] = selectedItem.FilterClass;
                    Session["SelectedRewardPoints"] = selectedItem.Points;
                    Session["SelectedRewardImageUrl"] = selectedItem.ImageURL;

                    // Redirect to the redemption page
                    Response.Redirect("~/AdminViewReward.aspx");
                }
                else
                {
                    // If the selected item is null, display an error message
                    Response.Write("<script>alert('Error: Reward not found.')</script>");
                }
            }
            else
            {
                // If the command name is not "Redeem", display an error message
                Response.Write("<script>alert('Error: Invalid command.')</script>");
            }
        }

        protected void add_rewards_Click(object sender, EventArgs e)
        {
            Response.Redirect("/AdminAddRewards");
        }
    }
}