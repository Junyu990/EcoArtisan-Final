using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace EcoArtisanFinal
{
    public partial class ManageUserSphere : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRepeater();
            }
        }
        private void BindRepeater()
        {
            try
            {
                // Assuming you have set Session["LoggedInUserID"] = 2; elsewhere in your code
                int userID = Convert.ToInt32(Session["UserID"]); // Use the logged-in user's ID
                Sphere sphere = new Sphere();
                List<Sphere> spheres = sphere.GetSpheresByUserID(userID); // Fetch spheres for logged-in user
                RepeaterSpheres.DataSource = spheres;
                RepeaterSpheres.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error retrieving spheres: {ex.Message}');</script>");
            }
        }

        protected void RepeaterSpheres_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    int sphereID = Convert.ToInt32(e.CommandArgument);
                    Sphere sphere = new Sphere();
                    sphere.SphereDelete(sphereID.ToString());
                    BindRepeater(); // Refresh the list
                    Response.Write("<script>alert('Delete successful');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('Error deleting sphere: {ex.Message}');</script>");
                }
            }
            else if (e.CommandName == "Update")
            {
                // Example of redirecting to an update page with SphereID as query string
                string sphereID = e.CommandArgument.ToString();
                Response.Redirect($"UpdateSphere.aspx?SphereID={sphereID}");
            }
        }
    }
    }
    
