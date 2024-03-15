using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class Profile : System.Web.UI.Page
    {
        public bool InsertDetails;
        UserDetails userDetail = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set the username and email values from the session
                usernameTextBox.Text = Session["UserName"]?.ToString();
                emailTextBox.Text = Session["UserEmail"]?.ToString();
                int userID = Convert.ToInt32(Session["UserID"]);

                // Call your getUserByUserID method to retrieve user details
                UserDetails aUser = new UserDetails();
                userDetail = aUser.getUserByUserID(userID);

                // Check if user details are retrieved successfully
                if (userDetail != null)
                {
                    // Set the values in your ASP.NET controls
                    Session["UserDetail"] = userDetail;
                    img_User.ImageUrl = $"~/Content/Images/{userDetail.ProfilePicture}";
                    usernameTextBox.Text = userDetail.UserName;
                    firstNameTextBox.Text = userDetail.FirstName;
                    lastNameTextBox.Text = userDetail.LastName;
                    mobileNumberTextBox.Text = userDetail.MobileNumber;
                    birthdateTextBox.Text = userDetail.Birthdate;
                    countryDDL.Text = userDetail.Country;
                    addressLine1TextBox.Text = userDetail.AddressLine1;
                    addressLine2TextBox.Text = userDetail.AddressLine2;
                    postcodeTextBox.Text = userDetail.Postcode;
                    stateTextBox.Text = userDetail.State;
                    areaTextBox.Text = userDetail.Area;
                    emailTextBox.Text = userDetail.Email;
                    genderRBL.Text = userDetail.Gender;

                }
                else
                {
                    // Handle the case where user details are not found
                    // You may want to redirect to an error page or display a message
                    img_User.ImageUrl = "~/Content/Images/defaultprofilepic.jpg";

                }
            }
            else
            {
                // If no user ID in the session, display default inputs or handle as needed
                InsertDetails = true;
                userDetail = (UserDetails)Session["UserDetail"];
                img_User.ImageUrl = $"~/Content/Images/defaultprofilepic.jpg";
                SetDefaultInputs();
            }

            if (!IsPostBack)
            {
                BindRedemption();
            }
        }
        private void SetDefaultInputs()
        {
            // Set default values or clear the inputs based on your requirements
            SetTextBoxValue(usernameTextBox, Session["UserName"].ToString());
            /*SetTextBoxValue(firstNameTextBox, "");
            SetTextBoxValue(lastNameTextBox, "");
            SetTextBoxValue(mobileNumberTextBox, "");
            SetTextBoxValue(birthdateTextBox, "");
            SetTextBoxValue(countryDDL, "");
            SetTextBoxValue(addressLine1TextBox, "");
            SetTextBoxValue(addressLine2TextBox, "");
            SetTextBoxValue(postcodeTextBox, "");
            SetTextBoxValue(stateTextBox, "");
            SetTextBoxValue(areaTextBox, "");*/
            SetTextBoxValue(emailTextBox, Session["UserEmail"].ToString());
        }

        private void SetTextBoxValue(TextBox textBox, string value)
        {
            // Check for null value before setting the TextBox value
            textBox.Text = value ?? string.Empty;
        }

        protected void editButton_Click(object sender, EventArgs e)
        {
            int userID = (int)Session["UserID"];
            userDetail = (UserDetails)Session["UserDetail"];

            if (userDetail == null)
            {
                img_User.ImageUrl = "~/Content/Images/defaultprofilepic.jpg";
                usernameTextBox.Text = Session["UserName"].ToString();
                firstNameTextBox.Text = "";
                lastNameTextBox.Text = "";
                mobileNumberTextBox.Text = "";
                birthdateTextBox.Text = "";
                countryDDL.Text = null;
                addressLine1TextBox.Text = "";
                addressLine2TextBox.Text = "";
                postcodeTextBox.Text = "";
                stateTextBox.Text = "";
                areaTextBox.Text = "";
                genderRBL.Text = null;
                emailTextBox.Text = Session["UserEmail"].ToString();
            }
            else
            {
                if (userID != userDetail.UserID)
                {
                    img_User.ImageUrl = "~/Content/Images/defaultprofilepic.jpg";
                    usernameTextBox.Text = Session["UserName"].ToString();
                    firstNameTextBox.Text = "";
                    lastNameTextBox.Text = "";
                    mobileNumberTextBox.Text = "";
                    birthdateTextBox.Text = "";
                    countryDDL.Text = null;
                    addressLine1TextBox.Text = "";
                    addressLine2TextBox.Text = "";
                    postcodeTextBox.Text = "";
                    stateTextBox.Text = "";
                    areaTextBox.Text = "";
                    genderRBL.Text = null;
                    emailTextBox.Text = Session["UserEmail"].ToString();
                }
                else
                {
                    img_User.ImageUrl = $"~/Content/Images/{userDetail.ProfilePicture}";
                    usernameTextBox.Text = userDetail.UserName;
                    firstNameTextBox.Text = userDetail.FirstName;
                    lastNameTextBox.Text = userDetail.LastName;
                    mobileNumberTextBox.Text = userDetail.MobileNumber;
                    birthdateTextBox.Text = userDetail.Birthdate;
                    countryDDL.Text = userDetail.Country;
                    addressLine1TextBox.Text = userDetail.AddressLine1;
                    addressLine2TextBox.Text = userDetail.AddressLine2;
                    postcodeTextBox.Text = userDetail.Postcode;
                    stateTextBox.Text = userDetail.State;
                    areaTextBox.Text = userDetail.Area;
                    emailTextBox.Text = userDetail.Email;
                    genderRBL.Text = userDetail.Gender;
                }
            }
            enableEditing(true);

            // Show the Save button and hide the Edit button
            editButton.Style.Add("display", "none");
            cancelButton.Style.Add("display", "block");

            if (userDetail != null)
            {
                // If user details are present, show the Update button
                update.Style.Add("display", "block");
                // Hide the Save button
                saveButton.Style.Add("display", "none");
            }
            else
            {
                // If user details are not present, show the Save button
                saveButton.Style.Add("display", "block");
                // Hide the Update button
                update.Style.Add("display", "none");
            }
        }
        private void enableEditing(bool isEditing)
        {
            // Get all textboxes
            var textboxes = new List<TextBox>
            {
                usernameTextBox, firstNameTextBox, lastNameTextBox, mobileNumberTextBox, birthdateTextBox,
                addressLine1TextBox, addressLine2TextBox, postcodeTextBox,
                stateTextBox, areaTextBox, emailTextBox
            };

            var ddllist = new List<DropDownList>
            {
                countryDDL
            };

            var fileupload = new List<FileUpload>
            {
                profilePicture
            };

            var radiobutton = new List<RadioButtonList>
            {
                genderRBL
            };

            // Loop through textboxes and set the ReadOnly attribute
            foreach (var textbox in textboxes)
            {
                textbox.ReadOnly = !isEditing;
            }

            foreach (var dropdown in ddllist)
            {
                dropdown.Enabled = isEditing;
            }

            foreach (var upload in fileupload)
            {
                upload.Enabled = isEditing;
            }
            foreach (var radio in radiobutton)
            {
                radio.Enabled = isEditing;
            }
        }

        private void disableEditing(bool isEditing)
        {
            // Get all textboxes
            var textboxes = new List<TextBox>
            {
                usernameTextBox, firstNameTextBox, lastNameTextBox, mobileNumberTextBox, birthdateTextBox,
                addressLine1TextBox, addressLine2TextBox, postcodeTextBox,
                stateTextBox, areaTextBox, emailTextBox
            };

            var ddllist = new List<DropDownList>
            {
                countryDDL
            };

            var fileupload = new List<FileUpload>
            {
                profilePicture
            };

            var radiobutton = new List<RadioButtonList>
            {
                genderRBL
            };

            // Loop through textboxes and set the ReadOnly attribute
            foreach (var textbox in textboxes)
            {
                textbox.ReadOnly = isEditing;
            }
            foreach (var dropdown in ddllist)
            {
                dropdown.Enabled = !isEditing;
            }
            foreach (var upload in fileupload)
            {
                upload.Enabled = !isEditing;
            }
            foreach (var radio in radiobutton)
            {
                radio.Enabled = !isEditing;
            }
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            string userEmail = Session["UserEmail"].ToString();
            User user = new User();
            int userid = user.getUserIdByEmail(userEmail);
            string image = "";


            if (profilePicture.HasFile == true)
            {
                image = "Images/" + profilePicture.FileName;
            }

            int UserID = userid;
            string UserName = usernameTextBox.Text.ToString();
            string FirstName = firstNameTextBox.Text.ToString();
            string LastName = lastNameTextBox.Text.ToString();
            string MobileNumber = mobileNumberTextBox.Text.ToString();
            string Birthdate = birthdateTextBox.Text.ToString();

            string Country = countryDDL.Text.ToString();
            string AddressLine1 = addressLine1TextBox.Text.ToString();
            string AddressLine2 = addressLine2TextBox.Text.ToString();
            string Postcode = postcodeTextBox.Text.ToString();
            string State = stateTextBox.Text.ToString();
            string Area = areaTextBox.Text.ToString();
            string Email = userEmail.ToString();
            string ProfilePicture = profilePicture.FileName.ToString();
            string Gender = genderRBL.Text.ToString();

            UserDetails userDetail = new UserDetails(UserID, UserName, FirstName, LastName, MobileNumber, Birthdate, Country, AddressLine1, AddressLine2, Postcode, State, Area, Email, ProfilePicture, Gender);

            int insertResult = userDetail.InsertUserDetails();

            // Check the insert result and handle accordingly
            if (insertResult > 0)
            {
                string saveimg = Server.MapPath("~/Content/") + image;
                profilePicture.SaveAs(saveimg);
                // Successful insert
                // You may want to display a success message or redirect the user\
                Response.Write($"<script>alert('Successfully inserted Username:{userDetail.UserName}')</script>");
                //Response.Write("<script>alert('User details saved successfully.')</script>");

                Session["UserName"] = UserName;
                Session["UserEmail"] = Email;

                img_User.ImageUrl = $"~/Content/Images/{userDetail.ProfilePicture}";
                usernameTextBox.Text = userDetail.UserName;
                firstNameTextBox.Text = userDetail.FirstName;
                lastNameTextBox.Text = userDetail.LastName;
                mobileNumberTextBox.Text = userDetail.MobileNumber;
                birthdateTextBox.Text = userDetail.Birthdate;
                countryDDL.Text = userDetail.Country;
                addressLine1TextBox.Text = userDetail.AddressLine1;
                addressLine2TextBox.Text = userDetail.AddressLine2;
                postcodeTextBox.Text = userDetail.Postcode;
                stateTextBox.Text = userDetail.State;
                areaTextBox.Text = userDetail.Area;
                emailTextBox.Text = userDetail.Email;
                genderRBL.Text = userDetail.Gender;
            }
            else
            {
                // Handle the case where insert failed
                // You may want to display an error message or log the error
                Response.Write($"<script>alert('Failed to save user details. UserDetails:{userDetail.UserID}, {userDetail.ProfilePicture}, {userDetail.UserName}, {userDetail.FirstName}, {userDetail.LastName}, {userDetail.AddressLine1}, {userDetail.Birthdate}, {userDetail.Country}, {userDetail.AddressLine2}, {userDetail.Email}')</script>");
            }

            // Disable editing for all textboxes
            enableEditing(false);


            // Show the Edit button and hide the Save button
            editButton.Style.Add("display", "block");
            saveButton.Style.Add("display", "none");
            cancelButton.Style.Add("display", "none");
            update.Style.Add("display", "none");
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            int userID = (int)Session["UserID"];
            userDetail = (UserDetails)Session["UserDetail"];

            if (userDetail == null)
            {
                img_User.ImageUrl = "~/Content/Images/defaultprofilepic.jpg";
                usernameTextBox.Text = Session["UserName"].ToString();
                firstNameTextBox.Text = "";
                lastNameTextBox.Text = "";
                mobileNumberTextBox.Text = "";
                birthdateTextBox.Text = "";
                countryDDL.Text = null;
                addressLine1TextBox.Text = "";
                addressLine2TextBox.Text = "";
                postcodeTextBox.Text = "";
                stateTextBox.Text = "";
                areaTextBox.Text = "";
                genderRBL.Text = null;
                emailTextBox.Text = Session["UserEmail"].ToString();
            }
            else
            {
                if (userID != userDetail.UserID)
                {
                    img_User.ImageUrl = "~/Content/Images/defaultprofilepic.jpg";
                    usernameTextBox.Text = Session["UserName"].ToString();
                    firstNameTextBox.Text = "";
                    lastNameTextBox.Text = "";
                    mobileNumberTextBox.Text = "";
                    birthdateTextBox.Text = "";
                    countryDDL.Text = null;
                    addressLine1TextBox.Text = "";
                    addressLine2TextBox.Text = "";
                    postcodeTextBox.Text = "";
                    stateTextBox.Text = "";
                    areaTextBox.Text = "";
                    emailTextBox.Text = Session["UserEmail"].ToString();
                    genderRBL.Text = null;
                }
                else
                {
                    Session["UserDetail"] = userDetail;
                    img_User.ImageUrl = $"~/Content/Images/{userDetail.ProfilePicture}";
                    usernameTextBox.Text = userDetail.UserName;
                    firstNameTextBox.Text = userDetail.FirstName;
                    lastNameTextBox.Text = userDetail.LastName;
                    mobileNumberTextBox.Text = userDetail.MobileNumber;
                    birthdateTextBox.Text = userDetail.Birthdate;
                    countryDDL.Text = userDetail.Country;
                    addressLine1TextBox.Text = userDetail.AddressLine1;
                    addressLine2TextBox.Text = userDetail.AddressLine2;
                    postcodeTextBox.Text = userDetail.Postcode;
                    stateTextBox.Text = userDetail.State;
                    areaTextBox.Text = userDetail.Area;
                    emailTextBox.Text = userDetail.Email;
                    genderRBL.Text = userDetail.Gender;
                }
            }

            enableEditing(false);
            // Show the Save button and hide the Edit button
            saveButton.Style.Add("display", "none");
            update.Style.Add("display", "none");
            editButton.Style.Add("display", "block");
            cancelButton.Style.Add("display", "none");
        }

        protected void update_Click(object sender, EventArgs e)
        {
            // Get the user ID from the session
            int userID = (int)Session["UserID"];

            // Get the existing user details from the session
            userDetail = (UserDetails)Session["UserDetail"];

            // Update the user details based on the modified values
            userDetail.UserName = usernameTextBox.Text;
            userDetail.FirstName = firstNameTextBox.Text;
            userDetail.LastName = lastNameTextBox.Text;
            userDetail.MobileNumber = mobileNumberTextBox.Text;
            userDetail.Birthdate = birthdateTextBox.Text;
            userDetail.Country = countryDDL.Text;
            userDetail.AddressLine1 = addressLine1TextBox.Text;
            userDetail.AddressLine2 = addressLine2TextBox.Text;
            userDetail.Postcode = postcodeTextBox.Text;
            userDetail.State = stateTextBox.Text;
            userDetail.Area = areaTextBox.Text;
            userDetail.Email = emailTextBox.Text;
            userDetail.Gender = genderRBL.Text;

            // Check if a new profile picture is uploaded
            if (profilePicture.HasFile)
            {
                try
                {
                    // Save the new profile picture
                    string fileName = Path.GetFileName(profilePicture.FileName);
                    string savePath = Server.MapPath("~/Content/Images/") + fileName;
                    profilePicture.SaveAs(savePath);

                    // Update the profile picture in the userDetail object
                    userDetail.ProfilePicture = fileName;
                }
                catch (Exception ex)
                {
                    // Handle the exception if needed
                    Response.Write($"<script>alert('Error uploading profile picture: {ex.Message}')</script>");
                    return;
                }
            }

            // Call the UpdateUserDetails method to update the record in the database
            int updateResult = userDetail.UpdateUserDetails();

            // Check the update result and handle accordingly
            if (updateResult > 0)
            {
                // Update the user detail session with the modified data
                Session["UserDetail"] = userDetail;
                Session["UserName"] = userDetail.UserName;
                Session["UserEmail"] = userDetail.Email;

                // Update the user information in the User class
                User userInstance = new User();
                userInstance.UpdateUserInformation(userID, usernameTextBox.Text, emailTextBox.Text);

                // Successful update
                Response.Write($"<script>alert('User details updated successfully. {userID}, {userDetail.UserName}')</script>");
            }
            else
            {
                // Handle the case where update failed
                Response.Write("<script>alert('Failed to update user details.')</script>");
            }

            // Disable editing for all textboxes
            enableEditing(false);
            userDetail = (UserDetails)Session["UserDetail"];
            img_User.ImageUrl = $"~/Content/Images/{userDetail.ProfilePicture}";
            // Show the Edit button and hide the Update button
            editButton.Style.Add("display", "block");
            update.Style.Add("display", "none");
            cancelButton.Style.Add("display", "none");
        }
        protected void Redirect_rewards(object sender, EventArgs e)
        {
            Response.Redirect("~/Rewards");
        }


        protected void Redirect_order_history(object sender, EventArgs e)
        {
            Response.Redirect("/OrderHistory");
        }

        protected void Redirect_reviews(object sender, EventArgs e)
        {
            Response.Redirect("/ReadReview");
        }

        private void BindRedemption()
        {
            try
            {
                int userID = int.Parse(Session["UserID"].ToString()); // Assuming you have a method to get the logged-in user ID

                // Create an instance of Redemption class
                Redemption redemption = new Redemption();

                // Call the GetRedemptionsByUser method to retrieve redemptions for the logged-in user
                List<Reward> redemptions = redemption.GetRedemptionsByUser(userID);

                // Bind the list of redemptions to the Repeater control
                rptRewards.DataSource = redemptions;
                rptRewards.DataBind();
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine(ex.Message);
            }
        }

        protected void delete_btn_Click(object sender, EventArgs e)
        {
            // This method will be called only if the user confirms the deletion on the client-side
            User user = new User();
            int ID = Convert.ToInt32(Session["UserID"]);

            if (user.getUserByUserID(ID) == null)
            {
                Response.Write("<script>alert('User deletion NOT successful');</script>");
            }
            else
            {

                user.UserDelete(ID);
                Response.Write("<script>alert('User deletion successful');</script>");

                // Clear user-related sessions
                Session["UserID"] = null;
                Session["UserName"] = null;
                Session["UserEmail"] = null;
                Session["Points"] = null;
                Session["UserDetail"] = null;

                // Redirect to the login page or any other desired page
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}