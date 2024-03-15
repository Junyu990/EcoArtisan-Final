using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;


namespace EcoArtisanFinal
{
    public partial class AdminUpdateRewards : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load reward details
                LoadRewardDetails();
            }
        }
        private void LoadRewardDetails()
        {
            if (Session["SelectedRewardId"] != null)
            {
                int selectedRewardId = (int)Session["SelectedRewardId"];
                Reward reward = new Reward();
                Reward rewardDetails = reward.GetRewardItemById(selectedRewardId);
                if (rewardDetails != null)
                {
                    txtItemName.Text = rewardDetails.ItemName;
                    txtDescription.Text = rewardDetails.Desc;
                    ddlFilterClass.SelectedValue = rewardDetails.FilterClass;
                    txtPoints.Text = rewardDetails.Points.ToString();
                    txtDiscount.Text = rewardDetails.Discount.ToString();
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateRewardDetails();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/AdminRewards");
        }
        private void UpdateRewardDetails()
        {
            if (Session["SelectedRewardId"] != null)
            {
                int selectedRewardId = (int)Session["SelectedRewardId"];
                string itemName = txtItemName.Text;
                string description = txtDescription.Text;
                string filterClass = ddlFilterClass.SelectedValue;
                int points = Convert.ToInt32(txtPoints.Text);
                double discount = Convert.ToDouble(txtDiscount.Text);

                // Save the uploaded image
                string image = "";
                if (fuImage.HasFile)
                {
                    string fileName = Path.GetFileName(fuImage.PostedFile.FileName);
                    string filePath = Server.MapPath("~/Content/Rewards/") + fileName;
                    fuImage.SaveAs(filePath);

                    image = "Rewards/" + fuImage.FileName;
                }

                // Create an instance of the Reward class
                Reward reward = new Reward();

                string imageurl = fuImage.FileName.ToString();

                // Update the reward details in the database
                int rowsAffected = reward.UpdateReward(selectedRewardId, itemName, description, filterClass, imageurl, points, discount);

                if (rowsAffected > 0)
                {
                    string saveimg = Server.MapPath("~/Content/") + image;
                    fuImage.SaveAs(saveimg);
                    // Reward details updated successfully
                    Response.Write("<script>alert('Reward details updated successfully.');</script>");
                    Response.Redirect("/AdminRewards");
                }
                else
                {
                    // Failed to update reward details
                    Response.Write("<script>alert('Failed to update reward details.');</script>");
                }
            }
        }
    }
}