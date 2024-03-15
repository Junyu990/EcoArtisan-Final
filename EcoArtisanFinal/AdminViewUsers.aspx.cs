using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcoArtisanFinal
{
    public partial class AdminViewUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Bind the GridView controls with data
                BindGridViewCustomers();
                BindGridViewAdmins();
            }
        }
        protected void BindGridViewCustomers()
        {
            // Create an instance of the User class
            User user = new User();

            // Call the GetAllCustomers method to retrieve all customers
            List<User> customers = user.GetAllCustomers();

            // Bind the customers list to the GridViewCustomers control
            GridViewCustomers.DataSource = customers;
            GridViewCustomers.DataBind();
        }

        protected void BindGridViewAdmins()
        {
            // Create an instance of the User class
            User user = new User();

            // Call the GetAllAdmins method to retrieve all admins
            List<User> admins = user.GetAllAdmins();

            // Bind the admins list to the GridViewAdmins control
            GridViewAdmins.DataSource = admins;
            GridViewAdmins.DataBind();
        }

        protected void GridViewCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "GiveAdmin")
            {
                // Retrieve the index of the row clicked
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Retrieve the UserID from the GridView data keys
                int userId = Convert.ToInt32(GridViewCustomers.DataKeys[rowIndex].Values["UserID"]);

                // Call a method to give admin role to the user
                User user = new User();
                user.GiveAdminRole(userId);

                // Rebind the grids after updating roles
                BindGridViewCustomers();
                BindGridViewAdmins();
            }
            else if (e.CommandName == "DeleteUser")
            {
                // Handle DeleteUser command
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int userId = Convert.ToInt32(GridViewCustomers.DataKeys[rowIndex].Values["UserID"]);
                User user = new User();
                user.UserDelete(userId);
                BindGridViewCustomers();
            }
        }

        protected void GridViewAdmins_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RemoveAdmin")
            {
                // Retrieve the index of the row clicked
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Retrieve the UserID from the GridView data keys
                int userId = Convert.ToInt32(GridViewAdmins.DataKeys[rowIndex].Values["UserID"]);

                // Call a method to remove admin role from the user
                User user = new User();
                user.RemoveAdminRole(userId);

                // Rebind the grids after updating roles
                BindGridViewCustomers();
                BindGridViewAdmins();
            }
            else if (e.CommandName == "DeleteAdmin")
            {
                // Handle DeleteAdmin command
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int userId = Convert.ToInt32(GridViewAdmins.DataKeys[rowIndex].Values["UserID"]);
                User user = new User();
                user.UserDelete(userId);
                BindGridViewAdmins();
            }
        
    }
    }
}