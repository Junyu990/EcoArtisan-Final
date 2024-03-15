using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || string.IsNullOrEmpty(Session["UserID"].ToString()))
            {
                string script = "alert('Please log in first.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                // Redirect the user to the login page if UserID is not set in the session
                Response.Redirect("/Login");
            }
        }

        protected void btnSwitchCustomer_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Default");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            string script = "if(confirm('Are you sure you want to log out?')) { window.location = 'Login'; }";
            ScriptManager.RegisterStartupScript(this, GetType(), "LogoutConfirmation", script, true);
        }
    }
}