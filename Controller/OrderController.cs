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
    public class OrderController
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["OrderSystemDB"].ConnectionString;

        /// <summary>
        /// Creates a new order in the database.
        /// </summary>
        /// <param name="order">The OrderModel object containing the details of the order to be created.</param>
        /// <returns>Returns the number of rows affected in the database. A value greater than 0 indicates successful creation of the order.</returns>
        public int CreateOrder(OrderModel order)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO [Order] (ProductID, TotalAmount) VALUES (@ProductID, @TotalAmount);";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", order.ProductID);
                    command.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}
