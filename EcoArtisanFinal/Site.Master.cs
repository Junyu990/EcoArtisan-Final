using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.SessionState;
using System.Threading.Tasks;
using System.Web.Script.Services;
using Newtonsoft.Json;
namespace EcoArtisanFinal
{
    public partial class SiteMaster : MasterPage
    {
        User user = new User();
        protected void Page_Load(object sender, EventArgs e)
        {
            string userEmail = "";
            string role = ""; // Declare the role variable outside of the if block
            if (Session["UserEmail"] != null && !string.IsNullOrEmpty(Session["UserEmail"].ToString()))
            {
                userEmail = Session["UserEmail"].ToString();
                role = user.GetRoleByEmail(userEmail);
                Session["Role"] = role;
            }
            else
            {
                return;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Show confirmation dialog using JavaScript
            string script = "if(confirm('Are you sure you want to log out?')) { window.location = 'Login'; }";
            ScriptManager.RegisterStartupScript(this, GetType(), "LogoutConfirmation", script, true);
        }

        protected void btnSwitchInterface_Click(object sender, EventArgs e)
        {
            Response.Redirect("/AdminHome");
        }
    }
}