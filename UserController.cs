using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace OOP2_POS
{
    public class UserController
    {
        private const string database = @"BIODOTA\SQLEXPRESS";

        private const string table = "POSUsers";

        private const string connString = "Data Source= " + database + ";Initial Catalog= POS; Integrated Security=True;";

        public class User
        {
            public string Email { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }

            public User(string email, string username, string password, string role)
            {
                Email = email;
                Username = username;
                Password = password;
                Role = role;
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            string query = $"SELECT Email, Username, Password, Role FROM {table}";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string email = reader.GetString(0);
                            string username = reader.GetString(1);
                            string password = reader.GetString(2);
                            string role = reader.GetString(3);

                            User user = new User(email, username, password, role);
                            users.Add(user);

                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            return users;
        }

        public void CreateUser(string email, string username, string password)
        {


            string hashedPassword = HashPassword(password);

            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                string sqlQuery = $"Insert Into {table} (Email, Username, Password) Values(N'{email}','{username}','{hashedPassword}')";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Registered Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateUser(string username, string upatedUsername,string updatedPassword, string updatedEmail, string updatedRole)
        {
            SqlConnection conn = new SqlConnection(connString);
            {
                string query = $"UPDATE {table} SET UserName = @UpdatedUsername, Password = @UpdatedPassword, Email = @UpdatedEmail, Role = @UpdatedRole WHERE Username = @Username";

                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@UpdatedUsername", upatedUsername);
                    command.Parameters.AddWithValue("@UpdatedPassword", updatedPassword);
                    command.Parameters.AddWithValue("@UpdatedEmail", updatedEmail);
                    command.Parameters.AddWithValue("@UpdatedRole", updatedRole);
                    command.Parameters.AddWithValue("@Username", username);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User information updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("User information update failed.");
                    }
                }
            }
        }

        public void DeleteUser(string username)
        {
            string query = $"DELETE FROM {table} WHERE Username = @Username";

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User deleted successfully.");
                            AdminForm form = new AdminForm();
                            form.RefreshForm();
                        }
                        else
                        {
                            MessageBox.Show("User not found or deletion failed.");
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

      
        }

        public bool ValidateUser(string username, string password)
        {
            string hashedPassword = HashPassword(password);
            string query = $"SELECT COUNT(*) FROM {table} WHERE Username = @Username AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", hashedPassword);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        public string FindOne(string usernameRef)
        {
            string username = "";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                string query = $"SELECT Username FROM {table} WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter for the username to find
                    command.Parameters.AddWithValue("@Username", usernameRef);
                    
                    try
                    {

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            username = reader.GetString(0);
                        }
                        else
                        {
                            MessageBox.Show("Username not found.");
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }

            }
            return username;
        }

        public string UserRoles(string username)
        {
            string userRole = "";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                string query = $"SELECT Role FROM {table} WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter for the username to find
                    command.Parameters.AddWithValue("@Username", username);

                    try
                    {

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            string role = reader.GetString(0);
                            userRole = role;
                        }
                        else
                        {
                            MessageBox.Show("Username not found.");
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }

            }
            return userRole;
        }

        public string HashPassword(string toHashPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(toHashPassword));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
