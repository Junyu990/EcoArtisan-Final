using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class RedemptionConfirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Redemption redeemedItem = (Redemption)Session["RedeemedItem"];
            // Display redemption details on the page
            lblRedemptionID.Text = redeemedItem.RedemptionID.ToString();
            lblRedemptionDate.Text = redeemedItem.RedemptionDate.ToString();
            // Clear the session for selected rewards
            ClearSelectedRewardSession();
        }
        private void ClearSelectedRewardSession()
        {
            Session.Remove("SelectedRewardId");
            Session.Remove("SelectedRewardName");
            Session.Remove("SelectedRewardDescription");
            Session.Remove("SelectedRewardPoints");
            Session.Remove("SelectedRewardImageUrl");
        }
    }
}