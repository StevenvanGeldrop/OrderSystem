using OrderSystem.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Controller
{
    public class ProductController
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["OrderSystemDB"].ConnectionString;

        /// <summary>
        /// Reads all products from the database.
        /// </summary>
        /// <returns>A list of ProductModel objects representing all products in the database.</returns>
        public List<ProductModel> Read()
        {
            List<ProductModel> products = new List<ProductModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductID, ProductName, Price, Quantity FROM [Product];";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductModel product = new ProductModel
                        {
                            ProductID = (int)reader["ProductID"],
                            ProductName = (string)reader["ProductName"],
                            Price = (decimal)reader["Price"],
                            Quantity = (int)reader["Quantity"]
                        };
                        products.Add(product);
                    }
                }
            }
            return products;
        }
    }
}
