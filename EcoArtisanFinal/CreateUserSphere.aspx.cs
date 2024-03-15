using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class CreateUserSphere : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // First, check if the user is logged in by checking if the UserID session variable has been set
                if (Session["UserID"] == null || string.IsNullOrEmpty(Session["UserID"].ToString()))
                {
                    // If the user is not logged in, show an alert and redirect them to the login page
                    string script = "alert('Please log in first.');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                    Response.Redirect("/Login");
                }
                else
                {
                    // If the user is logged in, retrieve the UserID from the session
                    string userID = Session["UserID"].ToString();
                    // Set the UserID to the TextBox for further use
                    tb_UserID.Text = userID;
                }

                // Then, check if the scene setup code is provided in the query string
                if (Request.QueryString["sceneSetupCode"] != null)
                {
                    // Retrieve the scene setup code from the query string
                    string sceneSetupCode = Request.QueryString["sceneSetupCode"];
                    // Set the scene setup code to the tb_SphereImage textbox, ensuring it's HTML encoded for security
                    tb_SphereImage.Text = Server.HtmlEncode(sceneSetupCode);
                }
            }
        }

        protected void Btn_create_Click(object sender, EventArgs e)
        {
            Func<string, bool> hasSpecialChars = input => !Regex.IsMatch(input, @"^[a-zA-Z0-9\s]+$");

            int result = 0;

            try
            {

                // Validate User ID int
                int UserID = int.Parse(tb_UserID.Text.Trim());
                if (!int.TryParse(tb_UserID.Text, out UserID) || hasSpecialChars(tb_UserID.Text))
                {
                    Response.Write("<script>alert('Invalid Student ID. Please enter a valid integer without special characters.');</script>");
                    return;
                }
                // 

                // Validate image string
                string sphereimage = tb_SphereImage.Text.Trim();
                if (string.IsNullOrEmpty(sphereimage))
                {
                    Response.Write("<script>alert('Invalid Image. Please enter a valid text without special characters or numbers.');</script>");
                    return;
                }

                //Validate Description
                string spheredesc = tb_SphereDesc.Text.Trim();
                if (string.IsNullOrEmpty(spheredesc))
                {
                    Response.Write("<script>alert('Invalid Description. Please enter a valid text without special characters or numbers.');</script>");
                    return;
                }

                // Validate Price
                decimal sphereprice;
                if (!decimal.TryParse(tb_SpherePrice.Text, out sphereprice))
                {
                    Response.Write("<script>alert('Invalid Price value. Please enter a valid decimal without special characters.');</script>");
                    return;
                }

                // Validate RadioButtonList (rbl_SphereMat)
                string selectedMat = rbl_SphereMat.SelectedItem?.Text;
                if (string.IsNullOrEmpty(selectedMat))
                {
                    Response.Write("<script>alert('Please select a Material.');</script>");
                    return;
                }

                // Validate CheckBoxList (cbl_SphereColour)
                List<string> selectedcolour = cbl_SphereColour.Items.Cast<ListItem>()
                                                  .Where(li => li.Selected)
                                                  .Select(li => li.Text)
                                                  .ToList();

                if (selectedcolour.Count == 0)
                {
                    Response.Write("<script>alert('Please select at least one option from colour.');</script>");
                    return;
                }

                // Combine the selected colours into a comma-separated string or any suitable format
                string combinedColours = string.Join(",", selectedcolour);

                // Validate DropDownList (ddl_SphereSize)
                string selectedSize = ddl_SphereSize.SelectedItem?.Text;
                if (string.IsNullOrEmpty(selectedSize))
                {
                    Response.Write("<script>alert('Please select an option from Who to Smash.');</script>");
                    return;
                }

                // Create the sphere without checking if it already exists
                Sphere stud = new Sphere(UserID, sphereimage, spheredesc, sphereprice, selectedMat, combinedColours, selectedSize);

                result = stud.SphereCreate();

                if (result > 0)
                {
                    Response.Write("<script>alert('Insert successful');</script>");
                    Response.Write("<script>setTimeout(function(){ window.location.href = 'ManageUserSphere.aspx'; }, 1000);</script>");
                }
                else
                {
                    Response.Write("<script>alert('Insert NOT successful');</script>");
                }
                if (result > 0)
                {
                    CustomCart customCart = new CustomCart();
                    customCart.InsertIntoCustomCart(UserID, result);

                    Response.Write("<script>alert('Insert successful');</script>");
                    Response.Write("<script>setTimeout(function(){ window.location.href = 'ManageUserSphere.aspx'; }, 1000);</script>");
                }
                else
                {
                    Response.Write("<script>alert('Insert NOT successful');</script>");
                }
            }
            catch
            {
                // Handle any exceptions that may occur during sphere creation
                Response.Write($"<script>alert('There is no such user ID!');</script>");
            }
        
    }
    }
}