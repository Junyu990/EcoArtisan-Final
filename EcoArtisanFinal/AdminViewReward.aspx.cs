using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class AdminViewReward : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve and display reward details from session variables
                if (Session["SelectedRewardId"] != null)
                {
                    int selectedRewardId = (int)Session["SelectedRewardId"];
                    string selectedRewardName = Session["SelectedRewardName"].ToString();
                    string selectedDescription = Session["SelectedRewardDescription"].ToString();
                    string selectedFilterClass = Session["SelectedRewardFilterClass"].ToString();
                    string selectedImage = Session["SelectedRewardImageURL"].ToString();
                    int selectedPoints = (int)Session["SelectedRewardPoints"];
                    double selectedDiscount = Convert.ToDouble(Session["SelectedRewardDiscount"]);

                    txtItemName.Text = selectedRewardName;
                    txtDescription.Text = selectedDescription;
                    txtFilterClass.Text = selectedFilterClass;
                    txtPoints.Text = selectedPoints.ToString();
                    txtDiscount.Text = selectedDiscount.ToString();
                }
            }
        }

        protected void update_reward_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/AdminUpdateRewards");
        }

        protected void delete_reward_btn_Click(object sender, EventArgs e)
        {
            // Get the reward ID from the session
            if (Session["SelectedRewardId"] != null)
            {
                int rewardId = Convert.ToInt32(Session["SelectedRewardId"]);

                // Create an instance of the Reward class
                Reward reward = new Reward();

                // Delete the reward
                int rowsAffected = reward.DeleteReward(rewardId);

                // Check if the deletion was successful
                if (rowsAffected > 0)
                {
                    // Reward deleted successfully
                    Response.Write("<script>alert('Reward deleted successfully');</script>");
                    // Redirect to the rewards page or perform any other necessary action
                    Response.Redirect("/AdminRewards");
                }
                else
                {
                    // Failed to delete the reward
                    Response.Write("<script>alert('Failed to delete the reward');</script>");
                    // Handle the failure scenario
                }
            }
        }
    }
}