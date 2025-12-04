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
    public static class UserController
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["OrderSystemDB"].ConnectionString;

        public static bool IsLoggedIn;
        public static UserModel CurrentUser;

        /// <summary>
        /// Checks the login credentials of a user.
        /// </summary>
        /// <param name="username">The username of the user attempting to log in.</param>
        /// <param name="password">The password of the user attempting to log in.</param>
        /// <returns>Returns true if the credentials are valid and the user is logged in; otherwise, returns false.</returns>
        public static bool CheckLogin(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Username, Password, Role FROM [User] WHERE Username = @Username;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    if (reader.HasRows)
                    {
                        if (BCrypt.Net.BCrypt.Verify(password, (string)reader["Password"]))
                        {
                            IsLoggedIn = true;
                            CurrentUser = new UserModel
                            {
                                Username = (string)reader["Username"],
                                Role = (string)reader["Role"]
                            };
                            return true;
                        }
                    }
                    IsLoggedIn = false;
                    CurrentUser = null;
                    return false;
                }
            }
        }
    }
}
