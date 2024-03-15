using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace EcoArtisanFinal
{
    public partial class ManageRedemption : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRedemptions();
            }
        }
        private void BindRedemptions()
        {
            StringBuilder html = new StringBuilder();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EcoArtisanDBContext"].ConnectionString))
                {
                    string query = @"
                        SELECT r.RedemptionID, u.UserName, r.RedemptionDate, rd.ItemName, rd.ItemDescription, rd.FilterClass, rd.ImageURL, rd.Points, rd.Discount
                        FROM Redemption r
                        JOIN Reward rd ON r.ItemID = rd.ID
                        JOIN [User] u ON r.UserID = u.UserID
                    ";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        html.Append("<tr>");
                        html.Append("<td>" + reader["RedemptionID"] + "</td>");
                        html.Append("<td>" + reader["UserName"] + "</td>");
                        html.Append("<td>" + reader["RedemptionDate"] + "</td>");
                        html.Append("<td>" + reader["ItemName"] + "</td>");
                        html.Append("<td>" + reader["ItemDescription"] + "</td>");
                        html.Append("<td>" + reader["FilterClass"] + "</td>");
                        html.Append("<td><image style='width:50px;height:auto;border-radius:8px;' src='../Content/Rewards/" + reader["ImageURL"] + "'></image></td>");
                        html.Append("<td>" + reader["Points"] + "</td>");
                        html.Append("<td>" + reader["Discount"] + "</td>");
                        html.Append("</tr>");
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine(ex.Message);
            }

            litRedemptions.Text = html.ToString();
        }
    }
}