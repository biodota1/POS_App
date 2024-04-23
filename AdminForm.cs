using iTextSharp.text.pdf;
using iTextSharp.text.pdf.qrcode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using ComboBox = System.Windows.Forms.ComboBox;
using Label = System.Windows.Forms.Label;
using TextBox = System.Windows.Forms.TextBox;

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

        private Panel UserInfo(UserController.User user, int yAxis)
        {
            UserController userController = new UserController();

            Panel panel = new Panel();
            Label lineLabel = new Label();
            Label usernameLabel = new Label();
            Label passwordLabel = new Label();
            Label emailLabel = new Label();
            Label roleLabel = new Label();
            Button modifyButton = new Button();
            Button deleteButton = new Button();


            panel.Size = new System.Drawing.Size(900, 70);
            panel.Location = new System.Drawing.Point(0, yAxis);

            lineLabel.Size = new System.Drawing.Size(900, 15);
            lineLabel.Location = new System.Drawing.Point(0, 50);
            lineLabel.Text = "___________________________________________________________________________________________________________________________________________________________________________";

            usernameLabel.Location = new System.Drawing.Point(0, 0);
            usernameLabel.Size = new System.Drawing.Size(150, 50);
            usernameLabel.Text = user.Username;
            usernameLabel.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            usernameLabel.TextAlign = ContentAlignment.MiddleCenter;

            passwordLabel.Location = new System.Drawing.Point(150, 0);
            passwordLabel.Size = new System.Drawing.Size(200, 50);
            passwordLabel.Text = user.Password;
            passwordLabel.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            passwordLabel.TextAlign = ContentAlignment.MiddleCenter;

            emailLabel.Location = new System.Drawing.Point(400, 0);
            emailLabel.Size = new System.Drawing.Size(200, 50);
            emailLabel.Text = user.Email;
            emailLabel.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            emailLabel.TextAlign = ContentAlignment.MiddleCenter;


            roleLabel.Location = new System.Drawing.Point(600, 0);
            roleLabel.Size = new System.Drawing.Size(100, 50);
            roleLabel.Text = user.Role;
            roleLabel.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            roleLabel.TextAlign = ContentAlignment.MiddleCenter;

            modifyButton.Location = new System.Drawing.Point(700, 0);
            modifyButton.Size = new System.Drawing.Size(80, 40);
            modifyButton.BackColor = Color.FromArgb(0, 75, 200);
            modifyButton.ForeColor = Color.White;
            modifyButton.Text = "Modify";
            modifyButton.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            modifyButton.FlatStyle = FlatStyle.Flat;

            deleteButton.Location = new System.Drawing.Point(800, 0);
            deleteButton.Size = new System.Drawing.Size(80, 40);
            deleteButton.BackColor = Color.FromArgb(250, 25, 50);
            deleteButton.ForeColor = Color.White;
            deleteButton.Text = "Delete";
            deleteButton.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            deleteButton.FlatStyle = FlatStyle.Flat;

            modifyButton.Click += (s, args) =>
            {
                Panel modPanel = new Panel();
                Button submitButton = new Button();
                Button cancelButton = new Button();
                Label modTitle = new Label();
                Label modUsername = new Label();
                Label modPassword = new Label();
                Label modEmail = new Label();
                Label modRole = new Label();
                TextBox modUsernameText = new TextBox();
                TextBox modPasswordText = new TextBox();
                TextBox modEmailText = new TextBox();
                TextBox modRoleText = new TextBox();

                modPanel.Size = new System.Drawing.Size(400, 350);
                modPanel.BackColor = Color.White;
                modPanel.BorderStyle = BorderStyle.FixedSingle;

                modTitle.Location = new System.Drawing.Point(10, 10);
                modTitle.Size = new System.Drawing.Size(modPanel.Size.Width, 50);
                modTitle.Text = "EDIT USER";
                modTitle.Font = new System.Drawing.Font("Arial", 15, System.Drawing.FontStyle.Bold);
                modTitle.TextAlign = ContentAlignment.MiddleCenter;

                submitButton.Location = new System.Drawing.Point(110, 225);
                submitButton.Size = new System.Drawing.Size(202, 40);
                submitButton.BackColor = Color.FromArgb(0, 75, 200);
                submitButton.ForeColor = Color.White;
                submitButton.Text = "Confirm";
                submitButton.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
                submitButton.FlatStyle = FlatStyle.Flat;

                cancelButton.Location = new System.Drawing.Point(110, 280);
                cancelButton.Size = new System.Drawing.Size(202, 40);
                cancelButton.BackColor = Color.FromArgb(250, 25, 50);
                cancelButton.ForeColor = Color.White;
                cancelButton.Text = "Cancel";
                cancelButton.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
                cancelButton.FlatStyle = FlatStyle.Flat;


                modUsername.Location = new System.Drawing.Point(10, 65);
                modUsername.Size = new System.Drawing.Size(100, 15);
                modUsername.Text = "Username";
                modUsername.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

                modUsernameText.Location = new System.Drawing.Point(110, 60);
                modUsernameText.Size = new System.Drawing.Size(200, 20);
                modUsernameText.Text = user.Username;
                modUsernameText.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

                modPassword.Location = new System.Drawing.Point(10, 105);
                modPassword.Size = new System.Drawing.Size(100, 15);
                modPassword.Text = "Password";
                modPassword.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

                modPasswordText.Location = new System.Drawing.Point(110, 100);
                modPasswordText.Size = new System.Drawing.Size(200, 20);
                modPasswordText.Text = user.Password;
                modPasswordText.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

                modEmail.Location = new System.Drawing.Point(10, 145);
                modEmail.Size = new System.Drawing.Size(100, 15);
                modEmail.Text = "Email";
                modEmail.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

                modEmailText.Location = new System.Drawing.Point(110, 140);
                modEmailText.Size = new System.Drawing.Size(200, 20);
                modEmailText.Text = user.Email;
                modEmailText.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

                modRole.Location = new System.Drawing.Point(10, 185);
                modRole.Size = new System.Drawing.Size(100, 15);
                modRole.Text = "Role";
                modRole.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

                modRoleText.Location = new System.Drawing.Point(110, 180);
                modRoleText.Size = new System.Drawing.Size(200, 20);
                modRoleText.Text = user.Role;
                modRoleText.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);


                int centerX = (usersTabPage.ClientSize.Width - modPanel.Size.Width) / 2;
                int centerY = (usersTabPage.ClientSize.Height - modPanel.Size.Height) / 2;

                usersTabPage.Controls.Add(modPanel);
                modPanel.Controls.Add(submitButton);
                modPanel.Controls.Add(cancelButton);
                modPanel.Controls.Add(modTitle);
                modPanel.Controls.Add(modUsername);
                modPanel.Controls.Add(modUsernameText);
                modPanel.Controls.Add(modPassword);
                modPanel.Controls.Add(modPasswordText);
                modPanel.Controls.Add(modEmail);
                modPanel.Controls.Add(modEmailText);
                modPanel.Controls.Add(modRole);
                modPanel.Controls.Add(modRoleText);

                modPanel.Location = new System.Drawing.Point(centerX, centerY);
                modPanel.BringToFront();

                submitButton.Click += (r, argsw) =>
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to edit this item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {

                        string hashedPass = userController.HashPassword(modPasswordText.Text);

                        userController.UpdateUser(user.Username, modUsernameText.Text, hashedPass, modEmailText.Text, modRoleText.Text);
                        userListPanel.Controls.Clear();
                        RefreshForm();
                    }
                    else
                    {
                        // User clicked No or closed the message box, do nothing or handle accordingly
                    }
                    modPanel.Dispose();
                    modPanel.Controls.Clear();

                };
                cancelButton.Click += (r, argsw) =>
                {
                    modPanel.Dispose();
                    modPanel.Controls.Clear();

                };
            };

            deleteButton.Click += (s, args) =>
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    userController.DeleteUser(user.Username);
                    userListPanel.Controls.Clear();
                    RefreshForm();
                }
                else
                {
                    // User clicked No or closed the message box, do nothing or handle accordingly
                }

            };


            panel.Controls.Add(lineLabel);
            panel.Controls.Add(usernameLabel);
            panel.Controls.Add(emailLabel);
            panel.Controls.Add(passwordLabel);
            panel.Controls.Add(roleLabel);
            panel.Controls.Add(modifyButton);
            panel.Controls.Add(deleteButton);

            return panel;
        }

        public void RefreshForm()
        {
            ProductController productController = new ProductController();
            UserController userController = new UserController();

            userCount.Text = userController.GetAllUsers().Count.ToString();
            productCount.Text = productController.GetAllProducts().Count.ToString();

            List<UserController.User> users = new List<UserController.User>();

            foreach (UserController.User user in userController.GetAllUsers())
            {
                users.Add(user);
            }

            int userInfoLocationY = 0;

            for (int i = 0; i < users.Count; i++)
            {
               userListPanel.Controls.Add(UserInfo(users[i], userInfoLocationY));
               userInfoLocationY+=70;
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
