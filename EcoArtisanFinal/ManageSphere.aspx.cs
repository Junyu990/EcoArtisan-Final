using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class ManageSphere : System.Web.UI.Page
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
                Sphere sphere = new Sphere();
                List<Sphere> spheres = sphere.GetAllSpheres(); // Assuming you have this method in Sphere class
                RepeaterSpheres.DataSource = spheres;
                RepeaterSpheres.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error retrieving spheres: {ex.Message}');</script>");
            }

        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // Inline variable declaration in the if-statement
                if (int.TryParse(tbSearchID.Text.Trim(), out int sphereID))
                {
                    Sphere sphere = new Sphere();
                    List<Sphere> spheres = new List<Sphere>();
                    Sphere foundSphere = sphere.SphereRead(sphereID);

                    if (foundSphere != null)
                    {
                        spheres.Add(foundSphere);
                    }

                    RepeaterSpheres.DataSource = spheres;
                    RepeaterSpheres.DataBind();
                }
                else
                {
                    // Handle invalid input
                    Response.Write("<script>alert('Please enter a valid Sphere ID.');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error searching for sphere: {ex.Message}');</script>");
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
                Response.Redirect($"AdminSphere.aspx?SphereID={sphereID}");
            }
        }
    }
}