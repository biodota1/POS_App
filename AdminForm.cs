using iTextSharp.text.pdf;
using iTextSharp.text.pdf.qrcode;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;

namespace OOP2_POS
{
    public partial class AdminForm : Form
    {
        public class Product
        {
            public string ItemBarcode { get; set; }
            public string ItemName { get; set; }
            public string ItemPrice { get; set; }
            public string ItemQuantity { get; set; }
            public string ItemCategory { get; set; }

            public Product(string barcode, string name, string price, string quantity, string category)
            {
                ItemBarcode = barcode;
                ItemName = name;
                ItemPrice = price;
                ItemQuantity = quantity;
                ItemCategory = category;
            }
        }



        public AdminForm()
        {
            InitializeComponent();

            RefreshForm();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
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

        private void editItemButton_Click(object sender, EventArgs e)
        {
            ProductController productController = new ProductController();

            if (itemsListView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = itemsListView.SelectedItems[0];
       
                string BarcodeToUpdate = selectedItem.Text;
                string NameToUpdate = selectedItem.SubItems[1].Text;
                string PriceToUpdate = selectedItem.SubItems[2].Text;
                string CategoryToUpdate = selectedItem.SubItems[3].Text;
                string QuantityToUpdate = selectedItem.SubItems[4].Text;

                Product product = new Product(BarcodeToUpdate, NameToUpdate, PriceToUpdate, QuantityToUpdate, CategoryToUpdate);

                EditItem editItem = new EditItem(product);
                editItem.Show();
                editItem.Location = new Point(this.Location.X + this.Width, this.Location.Y);

            }
            else
            {
                MessageBox.Show("Please select an item to updated.", "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        public void RefreshForm()
        {
            ProductController productController = new ProductController();
            UserController userController = new UserController();

            usersListView.Items.Clear();
            itemsListView.Items.Clear();

            userCount.Text = userController.GetAllUsers().Count.ToString();
            productCount.Text = productController.GetAllProducts().Count.ToString();

            foreach (UserController.User user in userController.GetAllUsers())
            {

                ListViewItem listItem = new ListViewItem(user.Email);
                listItem.SubItems.Add(user.Username);
                listItem.SubItems.Add(user.Password);
                listItem.SubItems.Add(user.Role);

                usersListView.Items.Add(listItem);
            }

            

            foreach (ProductController.Product product in productController.GetAllProducts())
            {
     
                ListViewItem listItem = new ListViewItem(product.ItemBarcode);
                listItem.SubItems.Add(product.ItemName);
                listItem.SubItems.Add(product.ItemPrice);
                listItem.SubItems.Add(product.ItemCategory);
                listItem.SubItems.Add(product.ItemQuantity);

                itemsListView.Items.Add(listItem);
            }
           
        }

    
        private void logoutButton_Click(object sender, EventArgs e)
        {
            AuthForm authForm = new AuthForm();
            MessageBox.Show("Logging out...", "Log Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Dispose();
            authForm.Show();
        }

        private void refreshItemButton_Click(object sender, EventArgs e)
        {
            RefreshForm();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {

            ProductController productController = new ProductController();

            if (itemsListView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = itemsListView.SelectedItems[0];

                DialogResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    productController.DeleteProduct(selectedItem.Text);

                    itemsListView.Items.Remove(selectedItem);
                }
                else
                {
                    // User clicked No or closed the message box, do nothing or handle accordingly
                }

                
            }
            else
            {
                MessageBox.Show("Please select an item to delete.", "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

       
    }
}
