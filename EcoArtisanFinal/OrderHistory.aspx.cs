using System;
using System.IO; // For working with files
using System.Configuration; // For reading configuration from web.config
using Stripe;
using Stripe.Checkout;
using System.Net;
using System.Net.Mail; // For sending email
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Html2pdf;

namespace EcoArtisanFinal
{
    public partial class OrderHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || string.IsNullOrEmpty(Session["UserID"].ToString()))
            {
                string script = "alert('Please log in first.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
                // Redirect the user to the login page if UserID is not set in the session
                Response.Redirect("/Login");
            };

            int userID = Convert.ToInt32(Session["UserID"]);

            Order orderInstance = new Order();
            CustomCart customCart = new CustomCart();
            List<CustomCartItem> customCartItems = customCart.GetCustomCartItems(userID);

            // Check if the 'status' query parameter is 'success' and 'session_id' parameter exists
            if (Request.QueryString["status"] == "success" && Request.QueryString["session_id"] != null)
            {
                string sessionId = Request.QueryString["session_id"];

                // Use the sessionId to retrieve the associated Stripe Checkout Session
                var sessionService = new SessionService();
                var stripeSession = sessionService.Get(sessionId);

                // Extract customer email from customer_details
                string customerEmail = stripeSession.CustomerDetails?.Email;

                long totalAmount = (long)(stripeSession.AmountTotal ?? 0);

                string shippingName = stripeSession.ShippingDetails?.Name;
                string shippingAddress = ExtractShippingAddress(stripeSession);
                string cardLast4 = ExtractCardLast4(stripeSession);
                string cardBrand = CardBrand(stripeSession);

                var service = new SessionService();
                StripeList<LineItem> lineItems = service.ListLineItems(sessionId);

                MemoryStream pdfReceiptStream = GeneratePdfReceipt(shippingName, totalAmount, cardLast4, cardBrand, shippingAddress, stripeSession.Currency, lineItems);
                byte[] pdfReceiptBytes = pdfReceiptStream.ToArray();

                // Send email receipt
                SendEmailReceipt(customerEmail, pdfReceiptBytes);

                User userInstance = new User();
                userInstance.AddPoints(userID, CalculatePoints(totalAmount));

                TransferCartItemsToOrder(userID, lineItems, customCartItems, shippingAddress, pdfReceiptBytes);

                Response.Redirect("OrderHistory.aspx?processed=true");
            }

            List<Order> orders = orderInstance.FetchOrdersAndItemsByUserId(userID);

            if (orders != null && orders.Count > 0)
            {
                orders.Reverse();
                OrdersRepeater.DataSource = orders;
                OrdersRepeater.DataBind();
            }
        }

        // Method to calculate points based on the purchase amount
        private int CalculatePoints(long totalAmount)
        {
            // Calculate the points based on the purchase amount
            int points = (int)(totalAmount / 10); // 10 points for every $1

            // Ensure a minimum of 1 point
            return Math.Max(1, points);
        }

        private string ExtractShippingAddress(Session stripeSession)
        {
            // Assuming shipping information is requested and available in the session
            var shipping = stripeSession.ShippingDetails;

            if (shipping != null && shipping.Address != null)
            {
                // Format and return the shipping address as a string
                var address = shipping.Address;
                return $"{address.Line1}, {address.PostalCode}";
            }

            return string.Empty;
        }

        private string ExtractCardLast4(Session stripeSession)
        {
            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = paymentIntentService.Get(stripeSession.PaymentIntentId);

            // Fetch the PaymentMethod separately
            var paymentMethodService = new PaymentMethodService();
            var paymentMethod = paymentMethodService.Get(paymentIntent.PaymentMethodId);

            return paymentMethod.Card.Last4;
        }

        private string CardBrand(Session stripeSession)
        {
            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = paymentIntentService.Get(stripeSession.PaymentIntentId);

            // Fetch the PaymentMethod separately
            var paymentMethodService = new PaymentMethodService();
            var paymentMethod = paymentMethodService.Get(paymentIntent.PaymentMethodId);

            return paymentMethod.Card.Brand;
        }

        private void TransferCartItemsToOrder(int userID, StripeList<LineItem> lineItems, List<CustomCartItem> customCartItems, string shippingAddress, byte[] pdfReceipt)
        {
            Cart cart = new Cart();
            CustomCart customCart = new CustomCart();
            Order order = new Order();
            Product productInstance = new Product();

            decimal totalAmount = lineItems.Data.Sum(item => (decimal)item.AmountTotal) / 100;

            // Set order properties
            order.UserID = userID;
            order.TotalAmount = totalAmount;
            order.ShippingAddress = shippingAddress;
            order.OrderDate = DateTime.Now;
            order.ETADate = DateTime.Now.AddDays(7);
            order.PDFReceipt = pdfReceipt;

            foreach (var lineItem in lineItems)
            {
                var matchingCartItem = cart.getCart(userID).CartItems.FirstOrDefault(ci => ci.ProdName == lineItem.Description);
                if (matchingCartItem != null)
                {
                    var orderItem = new Order.OrderItem
                    {
                        ProdID = matchingCartItem.ProdID,
                        OrderItemQty = matchingCartItem.CartItemQty,
                    };
                    order.OrderItems.Add(orderItem);
                    productInstance.UpdateProductQuantity(matchingCartItem.ProdID, matchingCartItem.CartItemQty);
                }
            }

            foreach (var customItem in customCartItems)
            {
                var customOrderItem = new Order.CustomOrderItem
                {
                    SphereID = customItem.SphereID,
                    OrderItemQty = customItem.CustomCartItemQty,
                };
                order.CustomOrderItems.Add(customOrderItem);
            }

            // Insert the order and its items
            int orderID = order.OrderInsert();

            if (orderID > 0)
            {
                cart.ClearCart(userID);
                customCart.ClearCustomCart(userID);
            }
            else
            {
                throw new InvalidOperationException("Failed to create order.");
            }
        }

        private void SendEmailReceipt(string recipientEmail, byte[] pdfReceiptBytes)
        {
            try
            {
                // Set up SMTP client
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("EcoArtisanOfficial@gmail.com", "aciz ipbk llia wlfn"),
                };

                // Create a new MailMessage
                var mail = new MailMessage
                {
                    From = new MailAddress("your-email@gmail.com"),
                    Subject = "EcoArtisan Purchase Receipt",
                    IsBodyHtml = true,
                    Body = "Thank you for your purchase. Attached is the details of your purchase:<br><br>"
                };

                // Set recipient email address
                mail.To.Add(recipientEmail);

                // Directly using MemoryStream created from pdfReceiptBytes
                var pdfStream = new MemoryStream(pdfReceiptBytes);
                // Ensure the stream position is set to the beginning
                pdfStream.Position = 0;
                var attachment = new Attachment(pdfStream, "Receipt.pdf", "application/pdf");
                mail.Attachments.Add(attachment);

                // Send the email
                smtpClient.Send(mail);

                // Close pdfStream after the mail has been sent
                pdfStream.Close();
            }
            catch (Exception ex)
            {
                // Log the error using the LogError method or handle it as appropriate
                LogError(ex);
                Console.WriteLine("Error occurred while sending email: " + ex.Message);
            }
            // No finally block needed for disposing MemoryStream, as it's encapsulated in the try-catch
        }

        private MemoryStream GeneratePdfReceipt(string shippingName, long totalAmount, string cardLast4, string cardBrand, string shippingAddress, string currency, StripeList<LineItem> lineItems)
        {
            MemoryStream memoryStream = new MemoryStream();

            try
            {
                string receiptHtmlContent = $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>EcoArtisan Purchase Receipt</title>
                    <style>
                        body {{
                            font-family: 'Helvetica Neue', 'Helvetica', Arial, sans-serif;
                            font-size: 12px;
                            color: #333;
                            margin: 0;
                            padding: 0;
                        }}
                        .header-table {{
                            width: 100%;
                            border-collapse: collapse;
                            margin-bottom: 1.5rem;
                        }}
                        .receipt-title {{
                            color: #333;
                            text-align: left;
                            font-size: 1.5rem;
                            margin: 0;
                        }}
                        .logo {{
                            text-align: right;
                            height: 2rem;
                        }}
                        .bill-table {{
                            width: 80%;
                            margin-bottom: 2rem;
                        }}
                        .shop-title, .shop-ship {{
                            font-weight: bold;
                        }}
                        .receipt-content {{
                            margin-top: 1rem;
                        }}
                        .receipt-title-table {{
                            width: 100%;
                            border-collapse: collapse;
                            margin-top: 1.5rem;
                            margin-bottom: 2rem;
                        }}
                        .receipt-title-table thead th {{
                            border-bottom: 1px solid #aaa;
                            padding: 10px 0;
                            text-align: left;
                        }}
                        .receipt-title-table tbody td {{
                            padding: 10px 0;
                            text-align: right;
                        }}
                        .amount-row {{
                            font-weight: bold;
                        }}
                        .contact-table {{
                            color: #aaa;
                            margin-top: 2rem;
                        }}
                    </style>
                </head>
                <body>
                    <table class='header-table'>
                        <tbody>
                            <tr>
                                <td><h1 class='receipt-title'>Receipt</h1></td>
                                <td><img class='logo' src='../Content/Images/Chatbot.png' alt='Logo'></td>
                            </tr>
                        </tbody>
                    </table>
                    <h3>Date Paid: <span class='receipt-title-date'>" + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + @"</span></h3>
                    <table class='bill-table'>
                        <thead>
                            <tr>
                                <th style='text-align: left;'><h3 class='shop-title'>EcoArtisan</h3></th>
                                <th style='text-align: left;'><h3 class='shop-ship'>Ship to</h3></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td></td>
                                <td>Receipient: " + shippingName + @"</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>Address: " + shippingAddress + @"</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>Payment Method: " + cardBrand.ToUpper() + " - " + cardLast4 + @"</td>
                            </tr>
                        </tbody>
                    </table>
                    <div class='receipt-content'>
                        <p>We received payment for your order.</p>
                        <p>Thanks for your business!</p>
                    </div>
                    
                    <table class='receipt-title-table'>
                        <thead>
                            <tr>
                                <th style='width: 50%'>Description</th>
                                <th style='text-align: left;'>Quantity</th>
                                <th style='text-align: left;'>Unit Price</th>
                                <th style='text-align: right;'>Amount</th>
                            </tr>
                        </thead>
                        <tbody>
                        ";
                foreach (var item in lineItems)
                {
                    receiptHtmlContent += $@"
                            <tr>
                                <td style='text-align: left;'>{item.Description}</td>
                                <td style='text-align: left;'>{item.Quantity}</td>
                                <td style='text-align: left;'>{((decimal)item.Price.UnitAmount / 100).ToString("C")} {currency.ToUpper()}</td>
                                <td>{((decimal)(item.Price.UnitAmount * item.Quantity) / 100).ToString("C")} {currency.ToUpper()}</td>
                            </tr>
                            ";
                }
                receiptHtmlContent += $@"
                            <tr class='shipping-row'>
                                <td></td>
                                <td style='text-align: left;'>Shipping Fee:</td>
                                <td></td>
                                <td>$2.00 SGD</td>
                            </tr>
                            <tr class='total-row'>
                                <td></td>
                                <td style='text-align: left;'>Total:</td>
                                <td></td>
                                <td>{(totalAmount / 100).ToString("C")} {currency.ToUpper()}</td>
                            </tr>
                            <tr class='amount-row'>
                                <td></td>
                                <td style='text-align: left;'>Amount Paid:</td>
                                <td></td>
                                <td>{(totalAmount / 100).ToString("C")} {currency.ToUpper()}</td>
                            </tr>
                        </tbody>
                    </table>
                    ";

                receiptHtmlContent += $@"
                    <p style='color:#aaa; font-size: 2.8rem; font-weight: bold; margin-bottom: 1rem;'>Thank You.</p>
                    <p style='font-weight: bolder;'>EcoArtisan</p>
                    <table class='contact-table'>
                        <tbody>
                            <tr>
                                <td style='text-align: left; width: 42%'>EcoArtisan Inc.</td>
                                <td>https://support.ecoartisan.com/contact</td>
                            </tr>
                            <tr class='shipping-row'>
                                <td style='text-align: left;'>180 Ang Mo Kio Ave 8</td>
                                <td></td>
                            </tr>
                            <tr class='shipping-row'>
                                <td style='text-align: left;'>Singapore 569830</td>
                                <td></td>
                            </tr>
                            <tr class='shipping-row'>
                                <td style='text-align: left;'>Singapore</td>
                                <td></td>
                            </tr>
                        </tbody>
                    </table>
                </body>
                </html>
                ";

                // Convert the combined HTML content to PDF
                var writer = new PdfWriter(memoryStream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                HtmlConverter.ConvertToPdf(receiptHtmlContent, pdf, new ConverterProperties());

                // Close the document
                document.Close();
            }
            catch (Exception ex)
            {
                // Log the error using the LogError method or handle it as appropriate
                LogError(ex);
                Console.WriteLine("Error occurred while generating PDF receipt: " + ex.Message);
            }

            return memoryStream;
        }

        protected void lnkDownloadReceipt_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                Order orderInstance = new Order();
                byte[] pdfBytes = orderInstance.FetchPdfBytesByOrderId(orderId);

                if (pdfBytes != null)
                {
                    string fileName = $"Receipt-{orderId}.pdf";
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", $"attachment; filename={fileName}");
                    Response.BinaryWrite(pdfBytes);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    // Handle the case when PDF bytes are not found
                }
            }
        }

        // Function to log errors to a text file
        private static void LogError(Exception ex)
        {
            string logFilePath = HttpContext.Current.Server.MapPath("~/App_Data/ErrorLog.txt");

            using (StreamWriter sw = System.IO.File.AppendText(logFilePath))
            {
                sw.WriteLine($"Error at {DateTime.Now}:");
                sw.WriteLine($"Exception Type: {ex.GetType().FullName}");
                sw.WriteLine($"Message: {ex.Message}");
                sw.WriteLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    sw.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    sw.WriteLine($"Inner Exception Stack Trace: {ex.InnerException.StackTrace}");
                }
                sw.WriteLine(new string('-', 40));
            }
        }
    }
}