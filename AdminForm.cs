using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2_POS
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();

            GetAllProducts();

            UserController controller = new UserController();
            controller.GetAllUsers(userListView);

            GetAllLogs();
        }

        private void dashboardButton_Click(object sender, EventArgs e)
        {
            adminTabControl.SelectTab(0);
        }

        private void usersButton_Click(object sender, EventArgs e)
        {
            adminTabControl.SelectTab(1);
        }

        private void logsButton_Click(object sender, EventArgs e)
        {
            adminTabControl.SelectTab(2);
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            adminTabControl.SelectTab(3);
        }

        private void addItemButton_Click(object sender, EventArgs e)
        {
           AddItem addItem = new AddItem();
            addItem.Show();
            addItem.Location = new Point(this.Location.X+this.Width, this.Location.Y);
        }

        public void GetAllProducts()
        {
            string dbSource = @"BIODOTA\SQLEXPRESS";
            string db = "POS";
            string connString = @"Data Source=" + dbSource + ";Initial Catalog=" + db + ";Integrated Security=True;";

            string query = "SELECT Description, Price, Quantity FROM POS_Items";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        listView1.Columns.Clear();
                        listView1.Items.Clear();


                        listView1.Columns.Add("Description");
                        listView1.Columns.Add("Price");
                        listView1.Columns.Add("Quantity");
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["Description"].ToString());
                            item.SubItems.Add(reader["Price"].ToString());
                            item.SubItems.Add(reader["Quantity"].ToString());
                            listView1.Items.Add(item);

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

        public void CreateLog(string info, string details)
        {
            DateTime currentTime = DateTime.Now;

            string dbSource = @"BIODOTA\SQLEXPRESS";
            string db = "POS";
            string connString = @"Data Source=" + dbSource + ";Initial Catalog=" + db + ";Integrated Security=True;";

            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                string sqlQuery = $"Insert Into POS_Logs (Date, Information, Details) Values(N'{currentTime}','{info}','{details}')";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("New Item has been added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void GetAllLogs()
        {
            string dbSource = @"BIODOTA\SQLEXPRESS";
            string db = "POS";
            string connString = @"Data Source=" + dbSource + ";Initial Catalog=" + db + ";Integrated Security=True;";

            string query = "SELECT Date, Information, Details FROM POS_Logs";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        historyListView.Columns.Add("Date");
                        historyListView.Columns.Add("Information");
                        historyListView.Columns.Add("Details");
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["Date"].ToString());
                            item.SubItems.Add(reader["Information"].ToString());
                            item.SubItems.Add(reader["Details"].ToString());
                            historyListView.Items.Add(item);

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

       /* public void GetAllUsers()
        {
            string dbSource = @"BIODOTA\SQLEXPRESS";
            string db = "POS";
            string connString = @"Data Source=" + dbSource + ";Initial Catalog=" + db + ";Integrated Security=True;";

            string query = "SELECT Email, Username, Role FROM POSUsers";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        userListView.Columns.Clear();
                        userListView.Items.Clear();


                        userListView.Columns.Add("Username");
                        userListView.Columns.Add("Email");
                        userListView.Columns.Add("Role");
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["Username"].ToString());
                            item.SubItems.Add(reader["Email"].ToString());
                            item.SubItems.Add(reader["Role"].ToString());
                            userListView.Items.Add(item);

                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }*/


    }
}
