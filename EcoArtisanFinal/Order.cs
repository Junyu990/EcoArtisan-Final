using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace EcoArtisanFinal
{
    public class Order
    {
        private string _connStr = ConfigurationManager.ConnectionStrings["EcoArtisanDBContext"].ConnectionString;
        private int _OrderID;
        private int _UserID;
        private decimal _TotalAmount;
        private string _ShippingAddress;
        private DateTime _OrderDate;
        private DateTime _ETADate;
        private byte[] _PDFReceipt;

        public List<OrderItem> OrderItems { get; set; }
        public List<CustomOrderItem> CustomOrderItems { get; set; } = new List<CustomOrderItem>();

        public Order(int UserID, decimal TotalAmount, string ShippingAddress, DateTime OrderDate, DateTime ETADate, byte[] PDFReceipt = null)
        {
            _UserID = UserID;
            _TotalAmount = TotalAmount;
            _ShippingAddress = ShippingAddress;
            _OrderDate = OrderDate;
            _ETADate = ETADate;
            _PDFReceipt = PDFReceipt;

            OrderItems = new List<OrderItem>();
        }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        // Properties
        public int OrderID { get => _OrderID; set => _OrderID = value; }
        public int UserID { get => _UserID; set => _UserID = value; }
        public decimal TotalAmount { get => _TotalAmount; set => _TotalAmount = value; }
        public string ShippingAddress { get => _ShippingAddress; set => _ShippingAddress = value; }
        public DateTime OrderDate { get => _OrderDate; set => _OrderDate = value; }
        public DateTime ETADate { get => _ETADate; set => _ETADate = value; }
        public byte[] PDFReceipt { get => _PDFReceipt; set => _PDFReceipt = value; }

        //JINGRONG ADD THIS FOR USER DELETE
        public int DeleteOrderByUserID(int userID)
        {
            int result = 0;

            string queryStr = @"DELETE FROM [Order] WHERE UserID = @UserID";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    result = cmd.ExecuteNonQuery(); // Execute the command and get the number of rows affected
                }
            }

            return result;
        }

        public List<Order> FetchOrdersAndItemsByUserId(int userID)
        {
            List<Order> userOrders = new List<Order>();

            string queryStr = @"
            SELECT 
                o.OrderID, o.UserID, o.TotalAmount, o.ShippingAddress, o.OrderDate, o.ETADate, o.PDFReceipt,
                oi.ProdID AS RegularProdID, oi.ProdName AS RegularProdName, oi.ProdDesc AS RegularProdDesc, oi.ProdPrice AS RegularProdPrice, oi.ProdImage AS RegularProdImage, oi.ProdImpact AS RegularProdImpact, oi.OrderItemQty AS RegularOrderItemQty,
                coi.SphereID AS CustomSphereID, coi.SphereDesc AS CustomSphereDesc, coi.SpherePrice AS CustomSpherePrice, coi.SphereImage AS CustomSphereImage, coi.SphereMat AS CustomSphereMat, coi.SphereColour AS CustomSphereColour, coi.SphereSize AS CustomSphereSize, coi.OrderItemQty AS CustomOrderItemQty
            FROM [Order] o
            LEFT JOIN (
                SELECT 
                    oi.OrderID, p.ProdID, p.ProdName, p.ProdDesc, p.ProdPrice, p.ProdImage, p.ProdImpact, oi.OrderItemQty
                FROM OrderItems oi
                INNER JOIN Product p ON oi.ProdID = p.ProdID
            ) oi ON o.OrderID = oi.OrderID
            LEFT JOIN (
                SELECT 
                    coi.OrderID, s.SphereID, s.SphereDesc, s.SpherePrice, s.SphereImage, s.SphereMat, s.SphereColour, s.SphereSize, coi.OrderItemQty
                FROM CustomOrderItems coi
                INNER JOIN Sphere s ON coi.SphereID = s.SphereID
            ) coi ON o.OrderID = coi.OrderID
            WHERE o.UserID = @UserID
            ORDER BY o.OrderID";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int orderId = dr.GetInt32(dr.GetOrdinal("OrderID"));
                            var existingOrder = userOrders.FirstOrDefault(o => o.OrderID == orderId);
                            if (existingOrder == null)
                            {
                                existingOrder = new Order
                                {
                                    // Set properties from the reader
                                    OrderID = orderId,
                                    UserID = dr.GetInt32(dr.GetOrdinal("UserID")),
                                    TotalAmount = dr.GetDecimal(dr.GetOrdinal("TotalAmount")),
                                    ShippingAddress = dr.GetString(dr.GetOrdinal("ShippingAddress")),
                                    OrderDate = dr.GetDateTime(dr.GetOrdinal("OrderDate")),
                                    ETADate = dr.GetDateTime(dr.GetOrdinal("ETADate")),
                                    PDFReceipt = dr["PDFReceipt"] as byte[]
                                };
                                userOrders.Add(existingOrder);
                            }

                            // Add regular item if available
                            if (!dr.IsDBNull(dr.GetOrdinal("RegularProdID")))
                            {
                                int regularProdID = dr.GetInt32(dr.GetOrdinal("RegularProdID"));
                                var existingRegularItem = existingOrder.OrderItems.FirstOrDefault(item => item.ProdID == regularProdID);
                                if (existingRegularItem == null)
                                {
                                    // If the item doesn't exist, create a new one
                                    var regularItem = new Order.OrderItem
                                    {
                                        // Set properties from the reader
                                        OrderItemID = regularProdID,
                                        ProdID = regularProdID,
                                        OrderItemQty = dr.GetInt32(dr.GetOrdinal("RegularOrderItemQty")),
                                        ProdName = dr.GetString(dr.GetOrdinal("RegularProdName")),
                                        ProdDesc = dr.GetString(dr.GetOrdinal("RegularProdDesc")),
                                        ProdPrice = dr.GetDecimal(dr.GetOrdinal("RegularProdPrice")),
                                        ProdImage = dr.GetString(dr.GetOrdinal("RegularProdImage")),
                                        ProdImpact = dr.GetInt32(dr.GetOrdinal("RegularProdImpact")).ToString()
                                    };
                                    existingOrder.OrderItems.Add(regularItem);
                                }
                            }

                            // Add custom item if available
                            if (!dr.IsDBNull(dr.GetOrdinal("CustomSphereID")))
                            {
                                int customSphereID = dr.GetInt32(dr.GetOrdinal("CustomSphereID"));
                                if (!existingOrder.CustomOrderItems.Any(item => item.SphereID == customSphereID))
                                {
                                    var customItem = new Order.CustomOrderItem
                                    {
                                        CustomOrderItemID = customSphereID,
                                        SphereID = customSphereID,
                                        OrderItemQty = dr.GetInt32(dr.GetOrdinal("CustomOrderItemQty")),
                                        SphereDesc = dr.GetString(dr.GetOrdinal("CustomSphereDesc")),
                                        SpherePrice = dr.GetDecimal(dr.GetOrdinal("CustomSpherePrice")),
                                        SphereImage = dr.GetString(dr.GetOrdinal("CustomSphereImage")),
                                        SphereMat = dr.GetString(dr.GetOrdinal("CustomSphereMat")),
                                        SphereColour = dr.GetString(dr.GetOrdinal("CustomSphereColour")),
                                        SphereSize = dr.GetString(dr.GetOrdinal("CustomSphereSize"))
                                    };
                                    existingOrder.CustomOrderItems.Add(customItem);
                                }
                            }
                        }
                    }
                }
            }

            return userOrders;
        }

        public int OrderInsert()
        {
            int insertedOrderId = 0;

            string queryStr = @"INSERT INTO [Order] (UserID, TotalAmount, ShippingAddress, OrderDate, ETADate, PDFReceipt) 
                        VALUES (@UserID, @TotalAmount, @ShippingAddress, @OrderDate, @ETADate, @PDFReceipt);
                        SELECT CAST(SCOPE_IDENTITY() as int);"; // This retrieves the last identity value inserted into an identity column in the same scope

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    // Add parameters to the command to prevent SQL injection
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                    cmd.Parameters.AddWithValue("@TotalAmount", this.TotalAmount);
                    cmd.Parameters.AddWithValue("@ShippingAddress", this.ShippingAddress);
                    cmd.Parameters.AddWithValue("@OrderDate", this.OrderDate);
                    cmd.Parameters.AddWithValue("@ETADate", this.ETADate);
                    cmd.Parameters.AddWithValue("@PDFReceipt", (this.PDFReceipt != null) ? (object)this.PDFReceipt : DBNull.Value);

                    object resultObj = cmd.ExecuteScalar();
                    if (resultObj != null)
                    {
                        // Successfully retrieved the new OrderID
                        insertedOrderId = Convert.ToInt32(resultObj);
                        this.OrderID = insertedOrderId;

                        // After successfully creating the order, insert its items
                        foreach (var orderItem in this.OrderItems)
                        {
                            orderItem.OrderID = this.OrderID;
                            this.InsertOrderItem(orderItem);
                        }

                        foreach (var customOrderItem in this.CustomOrderItems)
                        {
                            customOrderItem.OrderID = this.OrderID;
                            this.InsertCustomOrderItem(customOrderItem);
                        }
                    }
                }
            }

            return insertedOrderId;
        }

        public void InsertOrderItem(OrderItem orderItem)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string query = @"
            INSERT INTO OrderItems (OrderID, ProdID, OrderItemQty) 
            VALUES (@OrderID, @ProdID, @OrderItemQty);";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderItem.OrderID);
                    cmd.Parameters.AddWithValue("@ProdID", orderItem.ProdID);
                    cmd.Parameters.AddWithValue("@OrderItemQty", orderItem.OrderItemQty);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertCustomOrderItem(CustomOrderItem customOrderItem)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string query = @"
                INSERT INTO CustomOrderItems (OrderID, SphereID, OrderItemQty) 
                VALUES (@OrderID, @SphereID, @OrderItemQty);";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderID", customOrderItem.OrderID);
                    cmd.Parameters.AddWithValue("@SphereID", customOrderItem.SphereID);
                    cmd.Parameters.AddWithValue("@OrderItemQty", customOrderItem.OrderItemQty);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public byte[] FetchPdfBytesByOrderId(int orderId)
        {
            byte[] pdfBytes = null;

            string queryStr = "SELECT PDFReceipt FROM [Order] WHERE OrderID = @OrderID";

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderId);

                    conn.Open();

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        pdfBytes = (byte[])result;
                    }
                }
            }

            return pdfBytes;
        }

        public class OrderItem
        {
            public int OrderItemID { get; set; }
            public int OrderID { get; set; }
            public int ProdID { get; set; }
            public int OrderItemQty { get; set; }
            public string ProdName { get; set; }
            public string ProdDesc { get; set; }
            public decimal ProdPrice { get; set; }
            public string ProdImage { get; set; }
            public string ProdImpact { get; set; }
        }

        public class CustomOrderItem
        {
            public int CustomOrderItemID { get; set; }
            public int OrderID { get; set; }
            public int SphereID { get; set; }
            public string SphereDesc { get; set; }
            public decimal SpherePrice { get; set; }
            public string SphereImage { get; set; }
            public string SphereMat { get; set; }
            public string SphereColour { get; set; }
            public string SphereSize { get; set; }
            public int OrderItemQty { get; set; }

        }


        public static List<SecondlyDashboardMetric> GetSecondlyDashboardMetrics()
        {
            List<SecondlyDashboardMetric> secondlyMetrics = new List<SecondlyDashboardMetric>();
            string queryStr = @"
                SELECT 
                    p.ProdName,
                    o.OrderDate AS FullOrderDate,
                    COUNT(DISTINCT o.UserID) AS TotalCustomers, 
                    ISNULL(SUM(o.TotalAmount), 0) AS TotalSales,
                    ISNULL(SUM(oi.OrderItemQty), 0) AS TotalUnitsSold,
                    SUM(oi.OrderItemQty) AS TotalQuantityBought,
                    ISNULL(SUM(CAST(p.ProdImpact AS INT) * oi.OrderItemQty), 0) AS ImpactMade
                FROM 
                    [Order] o
                    INNER JOIN OrderItems oi ON o.OrderID = oi.OrderID
                    INNER JOIN Product p ON oi.ProdID = p.ProdID
                GROUP BY 
                    p.ProdName, o.OrderDate
                ORDER BY 
                    o.OrderDate;
            ";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EcoArtisanDBContext"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SecondlyDashboardMetric metric = new SecondlyDashboardMetric
                            {
                                ProdName = reader.GetString(reader.GetOrdinal("ProdName")),
                                Timestamp = new DateTimeOffset(reader.GetDateTime(reader.GetOrdinal("FullOrderDate"))).ToUnixTimeMilliseconds(),
                                TotalCustomers = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                                TotalSales = reader.IsDBNull(3) ? 0m : reader.GetDecimal(3),
                                TotalUnitsSold = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                                TotalQuantityBought = reader.IsDBNull(5) ? 0 : reader.GetInt32(5), // New property to store total quantity bought
                                ImpactMade = reader.IsDBNull(6) ? 0 : reader.GetInt32(6)
                            };
                            secondlyMetrics.Add(metric);
                        }
                    }
                }
            }

            return secondlyMetrics;
        }

        public static DashboardMetrics GetDashboardMetrics()
        {
            DashboardMetrics metrics = new DashboardMetrics();

            string queryStr = @"
                SELECT 
                    COUNT(DISTINCT o.UserID) AS TotalCustomers, 
                    ISNULL(SUM(o.TotalAmount), 0) AS TotalSales,
                    ISNULL(SUM(oi.OrderItemQty), 0) AS TotalUnitsSold,
                    ISNULL(SUM(CAST(p.ProdImpact AS INT) * oi.OrderItemQty), 0) AS ImpactMade
                FROM 
                    [Order] o
                    INNER JOIN OrderItems oi ON o.OrderID = oi.OrderID
                    INNER JOIN Product p ON oi.ProdID = p.ProdID;
            ";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EcoArtisanDBContext"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            metrics.TotalCustomers = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            metrics.TotalSales = reader.IsDBNull(1) ? 0m : reader.GetDecimal(1);
                            metrics.TotalUnitsSold = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                            metrics.ImpactMade = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                        }
                    }
                }
            }

            return metrics;
        }
    }

    public class DashboardMetrics
    {
        public int TotalCustomers { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalUnitsSold { get; set; }
        public int ImpactMade { get; set; }
    }

    public class SecondlyDashboardMetric
    {
        public string ProdName { get; set; }
        public long Timestamp { get; set; }
        public int TotalCustomers { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalUnitsSold { get; set; }
        public int TotalQuantityBought { get; set; }
        public int ImpactMade { get; set; }
    }
}