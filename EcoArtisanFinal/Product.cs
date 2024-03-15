using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace EcoArtisanFinal
{
    public class Product
    {
        string _connStr = ConfigurationManager.ConnectionStrings["EcoArtisanDBContext"].ConnectionString;
        private int _prodID = 0;
        private string _prodName = "";
        private string _prodDesc = "";
        private decimal _prodPrice = 0;
        private string _prodImg = "";
        private int _prodQty = 0;
        private int _prodImpact = 0;

        public Product(int prodID, string prodName, string prodDesc, decimal prodPrice, string prodImg, int prodQty, int prodImpact)
        {
            _prodID = prodID;
            _prodName = prodName;
            _prodDesc = prodDesc;
            _prodPrice = prodPrice;
            _prodImg = prodImg;
            _prodQty = prodQty;
            _prodImpact = prodImpact;
        }

        public Product(string prodName, string prodDesc, decimal prodPrice, string prodImg, int prodQty, int prodImpact)
        {
            _prodName = prodName;
            _prodDesc = prodDesc;
            _prodPrice = prodPrice;
            _prodImg = prodImg;
            _prodQty = prodQty;
            _prodImpact = prodImpact;
        }

        public Product()
        {
        }

        public int ProdID
        {
            get { return _prodID; }
            set { _prodID = value; }
        }

        public string ProdName
        {
            get { return _prodName; }
            set { _prodName = value; }
        }

        public string ProdDesc
        {
            get { return _prodDesc; }
            set { _prodDesc = value; }
        }
        public decimal ProdPrice
        {
            get { return _prodPrice; }
            set { _prodPrice = value; }
        }

        public string ProdImg
        {
            get { return _prodImg; }
            set { _prodImg = value; }
        }

        public int ProdQty
        {
            get { return _prodQty; }
            set { _prodQty = value; }
        }
        public int ProdImpact
        {
            get { return _prodImpact; }
            set { _prodImpact = value; }
        }

        public int AddProduct()
        {
            int result = 0;
            string queryStr = "INSERT INTO Product(ProdName, ProdDesc, ProdPrice, ProdImage, ProdQty, ProdImpact)" + "values(@prodName, @prodDesc, @prodPrice, @img, @qty, @impact)";

            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);

            cmd.Parameters.AddWithValue("@prodName", this.ProdName);
            cmd.Parameters.AddWithValue("@prodDesc", this.ProdDesc);
            cmd.Parameters.AddWithValue("@prodPrice", this.ProdPrice);
            cmd.Parameters.AddWithValue("@img", this.ProdImg);
            cmd.Parameters.AddWithValue("@qty", this.ProdQty);
            cmd.Parameters.AddWithValue("@impact", this.ProdImpact);

            conn.Open();
            result += cmd.ExecuteNonQuery();

            conn.Close();

            return result;

        }
        public Product GetProduct(int prodID)
        {
            Product prodDetail = null;

            int qty, prodImpact;
            decimal price;
            string prodName, prodDesc, prodImg;


            string queryStr = "SELECT * FROM Product WHERE ProdID = @prodID";

            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            cmd.Parameters.AddWithValue("@prodID", prodID);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                prodName = dr["ProdName"].ToString();
                prodDesc = dr["ProdDesc"].ToString();
                price = decimal.Parse(dr["ProdPrice"].ToString());
                prodImg = dr["ProdImage"].ToString();
                qty = int.Parse(dr["ProdQty"].ToString());
                prodImpact = int.Parse(dr["ProdImpact"].ToString());

                prodDetail = new Product(prodName, prodDesc, price, prodImg, qty, prodImpact);

            }
            else
            {
                prodDetail = null;
            }

            conn.Close();
            dr.Close();
            dr.Dispose();

            return prodDetail;
        }

        public int UpdateProduct(int prodID, string prodName, string prodDesc, decimal price, string img, int qty, int impact)
        {
            string queryStr = "UPDATE Product SET" + " ProdName = @prodName, " + " ProdDesc = @prodDesc," + " ProdPrice = @price," + " ProdImage = @img," + " ProdQty = @qty," + " ProdImpact = @impact" + " WHERE ProdID = @prodID";

            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);

            cmd.Parameters.AddWithValue("@prodID", prodID);
            cmd.Parameters.AddWithValue("@prodName", prodName);
            cmd.Parameters.AddWithValue("@prodDesc", prodDesc);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@img", img);
            cmd.Parameters.AddWithValue("@qty", qty);
            cmd.Parameters.AddWithValue("@impact", impact);

            conn.Open();
            int nofRow = 0;
            nofRow = cmd.ExecuteNonQuery();

            conn.Close();
            return nofRow;
        }

        public int DeleteProduct(int prodID)
        {
            string queryStr = "DELETE FROM Product WHERE ProdID = @prodID";

            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);

            cmd.Parameters.AddWithValue("@prodID", prodID);

            conn.Open();
            int nOfRow = 0;
            nOfRow = cmd.ExecuteNonQuery();

            conn.Close();

            return nOfRow;
        }

        public List<Product> getProductAll()
        {
            List<Product> ProductList = new List<Product>();
            int prodID, qty, prodImpact;
            decimal price;
            string prodName, prodDesc, prodImg;

            string queryStr = "SELECT * FROM Product"; // SQL
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                prodID = int.Parse(dr["ProdID"].ToString());
                prodName = dr["ProdName"].ToString();
                prodDesc = dr["ProdDesc"].ToString();
                price = decimal.Parse(dr["ProdPrice"].ToString());
                prodImg = dr["ProdImage"].ToString();
                qty = int.Parse(dr["ProdQty"].ToString());
                prodImpact = int.Parse(dr["ProdImpact"].ToString());

                Product prod = new Product(prodID, prodName, prodDesc, price, prodImg, qty, prodImpact);


                ProductList.Add(prod);
            }
            conn.Close();
            dr.Close();
            dr.Dispose();

            return ProductList;
        }

        public int UpdateProductQuantity(int prodID, int quantityToDeduct)
        {
            string queryStr = "UPDATE Product SET ProdQty = ProdQty - @quantity WHERE ProdID = @prodID AND ProdQty >= @quantity";
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand(queryStr, conn);

            cmd.Parameters.AddWithValue("@prodID", prodID);
            cmd.Parameters.AddWithValue("@quantity", quantityToDeduct);

            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();

            return result; // Returns the number of affected rows
        }
    }
}