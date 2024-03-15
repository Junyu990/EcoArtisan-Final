using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;


namespace EcoArtisanFinal
{
    public partial class AdminHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                // The session variable exists, proceed with your logic
                // For example, you can display the user's name or perform other operations
                string userName = Session["UserName"].ToString();
                // Other operations...
            }

            if (!IsPostBack)
            {
                PopulateDashboard();
                InjectChartDataIntoPage();

                InjectUserSphereStatistics(); // Make sure this is being called here
                InjectRadarChartIntoPage();
            }
        }
        private void PopulateDashboard()
        {
            DashboardMetrics metrics = Order.GetDashboardMetrics();
            string dashboardScript = $@"
                <script type='text/javascript'>
                    document.getElementById('total-sales').textContent = '{metrics.TotalSales.ToString("C")}';
                    document.getElementById('total-units-sold').textContent = '{metrics.TotalUnitsSold}';
                    document.getElementById('impact-made').textContent = '{metrics.ImpactMade} 🍚';
                    document.getElementById('total-customers').textContent = '{metrics.TotalCustomers}';
                </script>";

            ClientScript.RegisterStartupScript(GetType(), "DashboardScript", dashboardScript, false);
        }

        private void InjectChartDataIntoPage()
        {
            var monthlyMetrics = Order.GetSecondlyDashboardMetrics(); // Make sure this is fetching the correct data
            var jsonMonthlyMetrics = JsonConvert.SerializeObject(monthlyMetrics);
            var script = $@"
            <script type='text/javascript'>
                document.addEventListener('DOMContentLoaded', function() {{
                    var monthlyMetrics = {jsonMonthlyMetrics};
                    console.log(monthlyMetrics);
                    initializeCharts(monthlyMetrics);
                }});
            </script>";

            ClientScript.RegisterStartupScript(GetType(), "ChartDataScript", script, false);
        }

        private void InjectUserSphereStatistics()
        {
            var statsList = Sphere.GetUserSphereStatistics();
            string jsonStats = JsonConvert.SerializeObject(statsList);

            string statsScript = $@"
            <script type='text/javascript'>
                var stats = {jsonStats};
            </script>";

            ClientScript.RegisterStartupScript(this.GetType(), "UserSphereStatsScript", statsScript, false);
        }
        private void InjectRadarChartIntoPage() // Updated method name
        {
            var radarChartData = Sphere.GetBubbleChartData(); // You may need to adjust this method to suit radar chart data requirements
            var jsonRadarChartData = JsonConvert.SerializeObject(radarChartData);

            var script = $@"
            <script type='text/javascript'>
                document.addEventListener('DOMContentLoaded', function() {{
                    var radarChartData = {jsonRadarChartData};
                    initializeRadarChart(radarChartData); // Call the new initializeRadarChart function
                }});
            </script>";

            ClientScript.RegisterStartupScript(GetType(), "RadarChartScript", script, false); // Updated key name
        }
    }
}