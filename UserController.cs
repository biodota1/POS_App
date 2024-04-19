using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2_POS
{
    public class UserController
    {
        private const string connString = @"Data Source= EARTH137\SQLEXPRESS;Initial Catalog= POS;Integrated Security=True;";

        public void GetAllUsers(ListView listview)
        {
            string query = "SELECT Email, Username, Role FROM Users";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        listview.Columns.Clear();
                        listview.Items.Clear();


                        listview.Columns.Add("Username");
                        listview.Columns.Add("Email");
                        listview.Columns.Add("Role");
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["Username"].ToString());
                            item.SubItems.Add(reader["Email"].ToString());
                            item.SubItems.Add(reader["Role"].ToString());
                            listview.Items.Add(item);

                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        public void CreateUser(string email, string username, string password)
        {


            string hashedPassword = HashPassword(password);

            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                string sqlQuery = $"Insert Into Users (Email, Username, Password) Values(N'{email}','{username}','{hashedPassword}')";

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

        public void UpdateUser(string username, string upatedUsername, string updatedEmail)
        {
            SqlConnection conn = new SqlConnection(connString);
            {
                string query = "UPDATE Users SET UserName = @UpdatedUserName, Email = @Email WHERE Username = @Username";

                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@UpdatedUserName", upatedUsername);
                    command.Parameters.AddWithValue("@Email", updatedEmail);
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
            string query = "DELETE FROM Users WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", username);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("User not found or deletion failed.");
                    }
                }
            }
        }

        public bool ValidateUser(string username, string password)
        {
            string hashedPassword = HashPassword(password);
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";

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

        public string UserRoles(string username)
        {
            string userRole = "";
            string dbSource = @"EARTH137\SQLEXPRESS";
            string db = "POS";
            string connString = @"Data Source=" + dbSource + ";Initial Catalog=" + db + ";Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                string query = "SELECT Role FROM Users WHERE Username = @Username";
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

        private string HashPassword(string toHashPassword)
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
