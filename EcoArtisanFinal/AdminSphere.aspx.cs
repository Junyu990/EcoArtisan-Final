using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace EcoArtisanFinal
{
    public partial class AdminSphere : System.Web.UI.Page
    {
        Sphere stud = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the SphereID is passed as a query string
                string sphereID = Request.QueryString["SphereID"];
                if (!string.IsNullOrEmpty(sphereID))
                {
                    LoadSphereDetails(sphereID);
                }
            }
        }
        private void LoadSphereDetails(string sphereID)
        {
            try
            {
                Sphere astud = new Sphere();
                // Assuming SphereRead is a method that fetches sphere details
                // Make sure to correctly parse your sphereID to the expected type
                var stud = astud.SphereRead(int.Parse(sphereID));

                if (stud != null)
                {
                    tb_SphereID.Text = stud.SphereID.ToString();
                    tb_UserID.Text = stud.UserID.ToString();
                    tb_SphereImage.Text = stud.SphereImage;
                    tb_SphereDesc.Text = stud.SphereDesc;
                    tb_SpherePrice.Text = stud.SpherePrice.ToString();

                    ListItem selectedMatItem = rbl_SphereMat.Items.FindByText(stud.SphereMat);
                    if (selectedMatItem != null)
                    {
                        selectedMatItem.Selected = true;
                    }

                    foreach (ListItem item in cbl_SphereColour.Items)
                    {
                        item.Selected = stud.SphereColour.Contains(item.Text);
                    }

                    ListItem selectedSizeItem = ddl_SphereSize.Items.FindByText(stud.SphereSize);
                    if (selectedSizeItem != null)
                    {
                        ddl_SphereSize.ClearSelection(); // Important to clear previous selections
                        selectedSizeItem.Selected = true;
                    }
                }
                else
                {
                    // No record found logic
                    Response.Write("<script>alert('Record not found');</script>");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Response.Write($"<script>alert('Error loading sphere details: {ex.Message}');</script>");
            }
        }

        protected void Btn_read_Click(object sender, EventArgs e)
        {
            try
            {
                Sphere astud = new Sphere();

                string SphereID = tb_SphereID.Text.Trim();

                if (!string.IsNullOrEmpty(SphereID))
                {
                    stud = astud.SphereRead(int.Parse(SphereID));

                    if (stud != null)
                    {
                        tb_UserID.Text = stud.UserID.ToString();
                        tb_SphereImage.Text = stud.SphereImage;
                        tb_SphereDesc.Text = stud.SphereDesc;
                        tb_SpherePrice.Text = stud.SpherePrice.ToString();

                        // Set the selected value for RadioButtonList
                        ListItem selectedMatItem = rbl_SphereMat.Items.FindByText(stud.SphereMat);
                        if (selectedMatItem != null)
                        {
                            selectedMatItem.Selected = true;
                        }

                        // Set the selected values for CheckBoxList
                        foreach (ListItem item in cbl_SphereColour.Items)
                        {
                            item.Selected = stud.SphereColour.Contains(item.Text);
                        }

                        // Set the selected value for DropDownList
                        ListItem selectedSizeItem = ddl_SphereSize.Items.FindByText(stud.SphereSize);
                        ddl_SphereSize.ClearSelection(); // Clear previous selection
                        if (selectedSizeItem != null)
                        {
                            selectedSizeItem.Selected = true;
                        }
                        else
                        {
                            // Handle the scenario where the selected size doesn't exist in the DropDownList
                            // You can display a message to the user or set a default value
                            Response.Write("<script>alert('Selected size not found in the list');</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Record not found');</script>");
                        ClearControls();
                    }
                }
                else
                {
                    Response.Write("<script>alert('Enter a valid student ID!');</script>");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during sphere read operation
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }
        }
        private void ClearControls()
        {
            tb_UserID.Text = "";
            tb_SphereImage.Text = "";
            tb_SphereDesc.Text = "";
            tb_SpherePrice.Text = "";
            rbl_SphereMat.ClearSelection();
            foreach (ListItem item in cbl_SphereColour.Items)
            {
                item.Selected = false;
            }

            ddl_SphereSize.ClearSelection();

        }

        protected void Btn_update_Click(object sender, EventArgs e)
        {
            Func<string, bool> hasSpecialChars = input => !Regex.IsMatch(input, @"^[a-zA-Z0-9\s]+$");

            try
            {

                // Validate User ID int
                int SphereID = int.Parse(tb_SphereID.Text.Trim());
                if (!int.TryParse(tb_SphereID.Text, out SphereID) || hasSpecialChars(tb_SphereID.Text))
                {
                    Response.Write("<script>alert('Invalid Student ID. Please enter a valid integer without special characters.');</script>");
                    return;
                }

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
                    Response.Write("<script>alert('Please select an option from the right size');</script>");
                    return;
                }

                Sphere stud = new Sphere(SphereID, UserID, sphereimage, spheredesc, sphereprice, selectedMat, combinedColours, selectedSize);

                // Update the database with the new values
                int result = stud.SphereUpdate(SphereID, UserID, sphereimage, spheredesc, sphereprice, selectedMat, combinedColours, selectedSize);

                if (result > 0)
                {
                    Response.Write("<script>alert('Update successful');</script>");
                    Response.Write("<script>setTimeout(function(){ window.location.href = 'ManageSphere.aspx'; }, 1000);</script>");
                }
                else
                {
                    Response.Write("<script>alert('Update NOT successful');</script>");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during sphere update operation
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }
        }
    }
}