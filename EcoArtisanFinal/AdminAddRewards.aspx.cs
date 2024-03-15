using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace EcoArtisanFinal
{
    public partial class AdminAddRewards : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) // Check if all validation controls pass
            {
                string image = "";
                // Save the uploaded image
                if (fuImage.HasFile)
                {
                    string fileName = Path.GetFileName(fuImage.PostedFile.FileName);
                    string filePath = Server.MapPath("~/Content/Rewards/") + fileName;
                    fuImage.SaveAs(filePath);

                    image = "Rewards/" + fuImage.FileName;
                }

                // Retrieve form inputs
                string itemName = txtItemName.Text.ToString();
                string description = txtDescription.Text.ToString();
                string filterClass = ddlFilterClass.SelectedValue.ToString();
                string imageurl = fuImage.FileName.ToString();
                int points = Convert.ToInt32(txtPoints.Text);
                double discount = Convert.ToDouble(txtDiscount.Text);

                //You can now use these values to insert a new reward into the database
                // For example:
                Reward reward = new Reward();
                int rowsAffected = reward.InsertReward(itemName, description, filterClass, imageurl, points, discount);

                //Handle the result accordingly
                if (rowsAffected > 0)
                {
                    string saveimg = Server.MapPath("~/Content/") + image;
                    fuImage.SaveAs(saveimg);
                    // Success
                    Response.Redirect("/AdminRewards");
                }
                else
                {
                    // Error handling
                    Response.Write("<script>alert('Failed to add reward.')</script>");
                }

            }
        }
    }
}