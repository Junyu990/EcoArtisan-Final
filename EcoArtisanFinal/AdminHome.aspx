<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminHome.aspx.cs" Inherits="EcoArtisanFinal.AdminHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <style>
        /* Custom CSS to reduce left margin of the container */
        .custom-container {
            margin-left: -15px; /* Adjust the value as needed */
            margin-right: -15px;
        }
    </style>
    <link rel="stylesheet" type="text/css" href='<%= ResolveUrl("~/Content/Dashboard.css") %>?v=<%= DateTime.Now.Ticks %>' />
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/apexcharts"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="dashboard">
        <div class="header">Sales Report</div>
        <div class="row">
            <div class="card">
                <div class="card-body">
                    <div class="card-inner-box">
                        <h5 class="card-title">Total Sales</h5>
                        <p class="card-text" id="total-sales">$0</p>
                    </div>
                </div>
            </div>
    
            <div class="card">
                <div class="card-body">
                    <div class="card-inner-box">
                        <h5 class="card-title">Total Units Sold</h5>
                        <p class="card-text" id="total-units-sold">0</p>
                    </div>
                </div>
            </div>
    
            <div class="card">
                <div class="card-body">
                    <div class="card-inner-box">
                        <h5 class="card-title">Impact Made</h5>
                        <p class="card-text" id="impact-made">0</p>
                    </div>
                </div>
            </div>
    
            <div class="card">
                <div class="card-body">
                    <div class="card-inner-box">
                        <h5 class="card-title">Total Customers</h5>
                        <p class="card-text" id="total-customers">0</p>
                    </div>
                </div>
            </div>
       </div>
    
        <div class="row">
            <!-- Chart container for Total Sales -->
            <div class="chart-container">
                <div id="total-sales-chart"></div>
            </div>
            <!-- Chart container for Product Contribution -->
            <div class="chart-container">
                <div id="product-contribution-chart"></div>
            </div>
            <div class="chart-container">
                <div id="sphere-distribution-chart"></div>
            </div>
            <!-- Radar Chart container -->
            <div class="chart-container">
                <div id="radar-chart"></div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function aggregateProductData(monthlyMetrics) {
            // Group and sum the product contribution by name
            const productContribution = monthlyMetrics.reduce((acc, item) => {
                if (acc[item.ProdName]) {
                    acc[item.ProdName] += item.TotalQuantityBought;
                } else {
                    acc[item.ProdName] = item.TotalQuantityBought;
                }
                return acc;
            }, {});

            // Convert the object into an array of { x, y } objects for the chart
            const productContributionData = Object.entries(productContribution).map(([name, quantity]) => {
                return { x: name, y: quantity };
            });

            return productContributionData;
        }

        function initializeCharts(monthlyMetrics) {
            // Parsing the metrics to ensure the timestamp and values are correct
            var totalSalesData = monthlyMetrics.map(item => {
                return { x: new Date(item.Timestamp), y: item.TotalSales };
            });

            var unitsSoldData = monthlyMetrics.map(item => {
                return { x: new Date(item.Timestamp), y: item.TotalUnitsSold };
            });

            var impactMadeData = monthlyMetrics.map(item => {
                return { x: new Date(item.Timestamp), y: item.ImpactMade };
            });

            // Total Sales Chart configuration
            var optionsTotalSales = {
                chart: { type: 'line', height: 350 },
                series: [{ name: 'Total Sales', data: totalSalesData }],
                xaxis: {
                    type: 'datetime',
                    labels: {
                        formatter: function (value, timestamp) {
                            let date = new Date(timestamp);
                            return date.toLocaleDateString('en-US', {
                                month: 'short',
                                day: 'numeric'
                            }) + ' ' + date.toLocaleTimeString('en-US', {
                                hour: 'numeric',
                                minute: '2-digit',
                                hour12: true
                            });
                        },
                        rotate: -70,
                        rotateAlways: true,
                    }
                },
                yaxis: { title: { text: 'Sales in SGD' } },
                tooltip: {
                    x: {
                        format: 'MMM dd HH:mm:ss'
                    }
                },

                stroke: { curve: 'smooth' },
                colors: ['#008FFB'],
                title: {
                    text: 'Sales'
                }
            };
            new ApexCharts(document.querySelector("#total-sales-chart"), optionsTotalSales).render();

            // Units Sold Chart configuration
            var optionsUnitsSold = {
                chart: { type: 'bar', height: 350 },
                series: [{ name: 'Units Sold', data: unitsSoldData }],
                xaxis: {
                    type: 'datetime',
                    labels: {
                        formatter: function (value, timestamp) {
                            let date = new Date(timestamp);
                            return date.toLocaleDateString('en-US', {
                                month: 'short',
                                day: 'numeric'
                            }) + '\n' + date.toLocaleTimeString('en-US', {
                                hour: 'numeric',
                                minute: '2-digit',
                                hour12: true
                            });
                        },
                        rotate: -70,
                        rotateAlways: true,
                    }
                },
                yaxis: { title: { text: 'Number of Units Sold' } },
                plotOptions: {
                    bar: {
                        columnWidth: '8%', 
                    }
                },
                title: {
                    text: 'Number of Products Sold'
                }
            };
            new ApexCharts(document.querySelector("#units-sold-chart"), optionsUnitsSold).render();

            // Impact Made Chart configuration
            var optionsImpactMade = {
                chart: { type: 'line', height: 350 },
                series: [{ name: 'Impact Made', data: impactMadeData }],
                xaxis: {
                    type: 'datetime',
                    labels: {
                        formatter: function (value, timestamp) {
                            let date = new Date(timestamp);
                            return date.toLocaleDateString('en-US', {
                                month: 'short',
                                day: 'numeric'
                            }) + '\n' + date.toLocaleTimeString('en-US', {
                                hour: 'numeric',
                                minute: '2-digit',
                                hour12: true
                            });
                        },
                        rotate: -70,
                        rotateAlways: true,
                    }
                },
                yaxis: { title: { text: 'Impact Points' } },
                stroke: { curve: 'straight' },
                colors: ['#FF4560'],
                title: {
                    text: '🍚 Saved'
                }
            };
            new ApexCharts(document.querySelector("#impact-made-chart"), optionsImpactMade).render();

            // Product Contribution Chart configuration (Pie Chart)
            var productContributionData = monthlyMetrics.map(item => {
                return { x: item.ProdName, y: item.TotalQuantityBought };
            });

            var productContributionData = aggregateProductData(monthlyMetrics);

            var optionsProductContribution = {
                chart: { type: 'pie', height: 350 },
                series: productContributionData.map(item => item.y),
                labels: productContributionData.map(item => item.x),
                colors: ['#008FFB', '#00E396', '#FEB019', '#FF4560', '#775DD0'],
                legend: { show: true, position: 'bottom' },
                title: {
                    text: 'Units Sold per Product'
                }
            };
            new ApexCharts(document.querySelector("#product-contribution-chart"), optionsProductContribution).render();
        }

        // Call this function with the parsed JSON data from the backend
        document.addEventListener('DOMContentLoaded', function () {
            initializeCharts(monthlyMetrics);
        });

        function plotNormalDistribution(data) {
            var options = {
                chart: {
                    type: 'bar',
                    height: 350
                },
                series: [{
                    name: 'Sphere Count',
                    data: data.map(item => {
                        return {
                            x: item.UserID,
                            y: item.TotalSpheres
                        };
                    })
                }],
                xaxis: {
                    type: 'category',
                    title: {
                        text: 'User ID'
                    }
                },
                yaxis: {
                    title: {
                        text: 'Number of Spheres Ordered'
                    }
                },
                plotOptions: {
                    bar: {
                        horizontal: false
                    }
                },
                dataLabels: {
                    enabled: false
                },
                title: {
                    text: 'Distribution of Spheres Ordered per User'
                }
            };

            var chart = new ApexCharts(document.querySelector("#sphere-distribution-chart"), options);
            chart.render();
        }

        document.addEventListener('DOMContentLoaded', function () {
            // The 'stats' variable is already defined in the injected script from the ASP.NET code-behind
            plotNormalDistribution(stats);
        });
        function initializeRadarChart(radarChartData) {
            var options = {
                chart: {
                    type: 'radar',
                    height: 350
                },
                series: [{
                    name: 'Sphere Orders',
                    data: radarChartData.map(item => item.SpheresOrdered)
                }],
                labels: radarChartData.map(item => item.SphereSize),
                // ... additional options as needed ...
            };

            var chart = new ApexCharts(document.querySelector("#radar-chart"), options);
            chart.render();
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
