using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace OOP2_POS
{
    public partial class AuthForm : Form
    {

        public string UserRole {  get; set; }
        public AuthForm()
        {
            InitializeComponent();

            userInvalidInput.Hide();
            userInvalidInputIcon.Hide();
            passErrorIcon.Hide();
            passWrongPassword.Hide();
            
            regEmailError.Hide();
            regEmailErrorIcon.Hide();
            regUserError.Hide();
            regUserErrorIcon.Hide();
            regPassError.Hide();
            regPassErrorIcon.Hide();

         
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool ValidateUser(string username, string password)
        {
            string hashedPassword = HashPassword(password);

            string dbSource = @"BIODOTA\SQLEXPRESS";
            string db = "POS";
            string connString = @"Data Source=" + dbSource + ";Initial Catalog=" + db + ";Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM POSUsers WHERE Username = @Username AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", hashedPassword);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        private string UserRoles(string username)
        {
            string userRole ="";
            string dbSource = @"BIODOTA\SQLEXPRESS";
            string db = "POS";
            string connString = @"Data Source=" + dbSource + ";Initial Catalog=" + db + ";Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                string query = "SELECT Role FROM POSUsers WHERE Username = @Username";
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



        private void linkToRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            userInvalidInput.Hide();
            userInvalidInputIcon.Hide();
            passErrorIcon.Hide();
            passWrongPassword.Hide();

            authTabControl.SelectTab(1);
        }

        private void linkToLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            regEmailError.Hide();
            regEmailErrorIcon.Hide();
            regUserError.Hide();
            regUserErrorIcon.Hide();
            regPassError.Hide();
            regPassErrorIcon.Hide();

            authTabControl.SelectTab(0);
        }


        private void loginButton_Click(object sender, EventArgs e)
        {
            if (usernameTextbox.Text == "")
            {
                userInvalidInput.Show();
                userInvalidInputIcon.Show();
            }
            if (passwordTextBox.Text == "")
            {
                passWrongPassword.Show();
                passErrorIcon.Show();
            }
            if (usernameTextbox.Text != "" && passwordTextBox.Text != "")
            {
                userInvalidInput.Hide();
                userInvalidInputIcon.Hide();
                passErrorIcon.Hide();
                passWrongPassword.Hide();


                string username = usernameTextbox.Text;
                string password = passwordTextBox.Text;

                if (ValidateUser(username, password))
                {
                    MessageBox.Show("Login Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    UserRole = UserRoles(username);
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if(regEmailInput.Text != "" && regUsernameInput.Text != "" && regPasswordInput.Text != "")
            {
                regEmailError.Hide();
                regEmailErrorIcon.Hide();
                regUserError.Hide();
                regUserErrorIcon.Hide();
                regPassError.Hide();
                regPassErrorIcon.Hide();

                string email = regEmailInput.Text;
                string username = regUsernameInput.Text;
                string password = regPasswordInput.Text;

                string hashedPassword = HashPassword(password);

                string dbSource = @"BIODOTA\SQLEXPRESS";
                string db = "POS";
                string connString = @"Data Source=" + dbSource + ";Initial Catalog=" + db + ";Integrated Security=True;";

                SqlConnection conn = new SqlConnection(connString);
                try
                {
                    conn.Open();
                    string sqlQuery = $"Insert Into POSUsers (Email, Username, Password) Values(N'{email}','{username}','{hashedPassword}')";

                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Registered Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    authTabControl.SelectTab(0);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            if (regEmailInput.Text == "")
            {
                regEmailError.Show();
                regEmailErrorIcon.Show();
            }
            if (regUsernameInput.Text == "")
            {
                regUserError.Show();
                regUserErrorIcon.Show();
            }
            if (regPasswordInput.Text == "")
            {
                regPassError.Show();
                regPassErrorIcon.Show();
            }
        }
    }
}
