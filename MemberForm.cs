using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OOP2_POS.MemberForm;

namespace OOP2_POS
{
   
    public partial class MemberForm : Form
    {

        public class LocationValue
        {
            public int ItemsPerColumn { get; set; }
            public int ButtonLocationX { get; set; }
            public int ButtonLocationY { get; set; }

            public LocationValue(int itemCol, int x, int y)
            {
                ItemsPerColumn = itemCol;
                ButtonLocationX = x;
                ButtonLocationY = y;
            }
        }

        ProductController controller = new ProductController();

        List<ProductController.Product> productList = new List<ProductController.Product>();
        
        Dictionary<int, ProductController.Product> category1 = new Dictionary<int, ProductController.Product>();
        Dictionary<int, ProductController.Product> category2 = new Dictionary<int, ProductController.Product>();
        Dictionary<int, ProductController.Product> category3 = new Dictionary<int, ProductController.Product>();
        Dictionary<int, ProductController.Product> category4 = new Dictionary<int, ProductController.Product>();
        Dictionary<int, ProductController.Product> category5 = new Dictionary<int, ProductController.Product>();
        Dictionary<int, ProductController.Product> category6 = new Dictionary<int, ProductController.Product>();
        Dictionary<int, ProductController.Product> category7 = new Dictionary<int, ProductController.Product>();
        Dictionary<int, ProductController.Product> category8 = new Dictionary<int, ProductController.Product>();
        Dictionary<int, ProductController.Product> category9 = new Dictionary<int, ProductController.Product>();
        Dictionary<int, ProductController.Product> category10 = new Dictionary<int, ProductController.Product>();

        int total = 0;
        int discount = 0;
        int PanelLocation = 0;
        private string quantityValue = "1";
        private string discountText = "1";

        List<LocationValue> locations = new List<LocationValue>();
        List<ProductController.Product> purchasedItem = new List<ProductController.Product>();



        public MemberForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;

            for (int i = 0; i < 10; i++)
            {
                LocationValue value = new LocationValue(0,0,0);
                locations.Add(value);
            }

            for(int i = 0; i < controller.GetAllProducts().Count; i++)
            {
                productList.Add(controller.GetAllProducts()[i]);
            }

            for(int i = 0; i < productList.Count; i++)
            {
                if (productList[i].ItemCategory == "FruitsAndVeggie")
                {
                    category1.Add(category1.Count, productList[i]);
                }
                else if (productList[i].ItemCategory == "DairyAndEggs")
                {
                    category2.Add(category2.Count, productList[i]);
                }
                else if (productList[i].ItemCategory == "MeatAndSeafood")
                {
                    category3.Add(category3.Count, productList[i]);
                }
                else if (productList[i].ItemCategory == "BakeryAndBread")
                {
                    category4.Add(category4.Count, productList[i]);
                }
                else if (productList[i].ItemCategory == "CannedGoods")
                {
                    category5.Add(category5.Count, productList[i]);
                }
                else if (productList[i].ItemCategory == "DryGoodsAndGrains")
                {
                    category6.Add(category6.Count, productList[i]);
                }
                else if (productList[i].ItemCategory == "Beverage")
                {
                    category7.Add(category7.Count, productList[i]);
                }
                else if (productList[i].ItemCategory == "SnacksAndSweets")
                {
                    category8.Add(category8.Count, productList[i]);
                }
                else if (productList[i].ItemCategory == "FrozenFoods")
                {
                    category9.Add(category9.Count, productList[i]);
                }
                else if (productList[i].ItemCategory == "NonFood")
                {
                    category10.Add(category10.Count, productList[i]);
                }
            }

            CreateAButton(category1, item_1_tab,0);
            CreateAButton(category2, item_2_tab, 1);
            CreateAButton(category3, item_3_tab, 2);
            CreateAButton(category4, item_4_tab, 3);
            CreateAButton(category5, item_5_tab, 4);
            CreateAButton(category6, item_6_tab, 5);
            CreateAButton(category7, item_7_tab, 6);
            CreateAButton(category8, item_8_tab, 7);
            CreateAButton(category9, item_9_tab, 8);
            CreateAButton(category10, item_10_tab, 9);


        }

        private void handleQuantity_Click(object sender, EventArgs e)
        {
            Panel panel = new Panel();
            TextBox textBox = new TextBox();
            Button panelButton = new Button();
            Label label = new Label();

            panel.Location = new System.Drawing.Point(50, 200);
            panel.Size = new System.Drawing.Size(200, 200);
            label.Location = new System.Drawing.Point(20, 10); 
            label.Size = new System.Drawing.Size(100, 20);
            label.Text = "Quantity";
            label.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            textBox.Location = new System.Drawing.Point(20, 40); 
            textBox.Size = new System.Drawing.Size(100, 100);
            textBox.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            panelButton.Location = new System.Drawing.Point(20, 80);
            panelButton.Size = new System.Drawing.Size(100, 40);
            panelButton.BackColor = Color.FromArgb(0, 75, 200);
            panelButton.ForeColor = Color.White;
            panelButton.Text = "OK";
            panelButton.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);

            panel.Controls.Add(label);
            panel.Controls.Add(textBox);
            panel.Controls.Add(panelButton);


            panelButton.Click += (s, args) =>
            {
                quantityValue = textBox.Text;
                MessageBox.Show("Select an item");
                panel.Dispose(); 
            };

            CalculationPanel.Controls.Add(panel);
            panel.BringToFront();
            panel.BorderStyle = BorderStyle.FixedSingle;

        }

        private void CreateAButton(Dictionary<int, ProductController.Product> productList, TabPage tabPage, int location)
        {
            for (int i = 0; i < productList.Count; i++)
            {
                Button button = new Button();
                ProductController.Product product = productList[i];

                button.Text = $"{product.ItemName}\n₱{product.ItemPrice}";
                button.Location = new System.Drawing.Point(10 + locations[location].ButtonLocationX, 10 + locations[location].ButtonLocationY);
                button.Size = new System.Drawing.Size(100, 100);
                button.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
                button.ForeColor = Color.White;
                button.BackColor = Color.FromArgb(0, 100, 200);
                button.FlatAppearance.BorderSize = 0;
                int productPrice = int.Parse(product.ItemPrice);

                button.Click += (sender, e) =>
                {
                    purchasedItem.Add(product);
                    Label label = new Label();
                    string text = $"{product.ItemName.PadRight(10)}P {product.ItemPrice.PadRight(10)}X{quantityValue}";
                    label.Text = text;
                    label.Location = new System.Drawing.Point(10, 10 + PanelLocation);
                    label.AutoSize = true;
                    label.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
                    purchasedList.Controls.Add(label);

                    locations[location].ButtonLocationX += 30;

                    total += (productPrice* int.Parse(quantityValue));

                    string strTotal = total.ToString();
                    totalCostLabel.Text = strTotal+".00";
                    quantityValue = "1";
                    PanelLocation += 30;
                };
               
                tabPage.Controls.Add(button);

                locations[location].ItemsPerColumn++;
                if (locations[location].ItemsPerColumn < 4)
                {
                    locations[location].ButtonLocationX += 120;
                }
                else
                {
                    locations[location].ButtonLocationY += 120;
                    locations[location].ButtonLocationX = 0;
                    locations[location].ItemsPerColumn = 0;
                }
            }    
        }

        private void handlePrintReceipt_Click(object sender, EventArgs e)
        {
            Cashier.PrintReceipt(purchasedItem, total);
        }

        private void item_1_Click(object sender, EventArgs e)
        {
            categoryTabControl.SelectTab(0);
        }

        private void item_2_Click(object sender, EventArgs e)
        {
            categoryTabControl.SelectTab(1);
        }

        private void item_3_Click(object sender, EventArgs e)
        {
            categoryTabControl.SelectTab(2);
        }

        private void item_4_Click(object sender, EventArgs e)
        {
            categoryTabControl.SelectTab(3);
        }

        private void item_5_Click(object sender, EventArgs e)
        {
            categoryTabControl.SelectTab(4);
        }

        private void item_6_Click(object sender, EventArgs e)
        {
            categoryTabControl.SelectTab(5);
        }

        private void item_7_Click(object sender, EventArgs e)
        {
            categoryTabControl.SelectTab(6);
        }

        private void item_8_Click(object sender, EventArgs e)
        {
            categoryTabControl.SelectTab(7);
        }

        private void item_9_Click(object sender, EventArgs e)
        {
            categoryTabControl.SelectTab(8);
        }

        private void item_10_Click(object sender, EventArgs e)
        {
            categoryTabControl.SelectTab(9);
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            AuthForm authForm = new AuthForm();
            MessageBox.Show("Logging out...", "Log Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MemberForm memberForm = new MemberForm();
            this.Close();
            memberForm.Dispose();
            authForm.Show();
        }

        private void handlePayment_Click(object sender, EventArgs e)
        {
            PaymentMethod payment = new PaymentMethod();
            payment.Show();
        }

        private void handleVoid_Click(object sender, EventArgs e)
        {

        }

        private void handleDiscount_Click(object sender, EventArgs e)
        {
            Panel panel = new Panel();
            TextBox textBox = new TextBox();
            Button memberButton = new Button();
            Button seniorButton = new Button();
            Button pwdButton = new Button();
            Button cancelButton = new Button();
            Label label = new Label();

            panel.Size = new System.Drawing.Size(230, 280);

            int centerX = (cashierTab.ClientSize.Width - panel.Size.Width) / 2;
            int centerY = (cashierTab.ClientSize.Height - panel.Size.Height) / 2;

            panel.Location = new System.Drawing.Point(centerX, centerY);
           
            label.Location = new System.Drawing.Point(20, 10);
            label.Size = new System.Drawing.Size(200, 50);
            label.Text = "Discount";
            label.Font = new System.Drawing.Font("Arial", 20, System.Drawing.FontStyle.Bold);
            textBox.Location = new System.Drawing.Point(25, 60);
            textBox.Size = new System.Drawing.Size(150, 100);
            textBox.Font = new System.Drawing.Font("Arial", 15, System.Drawing.FontStyle.Bold);
            
            memberButton.Location = new System.Drawing.Point(25, 60);
            memberButton.Size = new System.Drawing.Size(150, 40);
            memberButton.BackColor = Color.FromArgb(0, 75, 200);
            memberButton.ForeColor = Color.White;
            memberButton.Text = "Member";
            memberButton.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            memberButton.FlatStyle = FlatStyle.Flat;

            seniorButton.Location = new System.Drawing.Point(25, 110);
            seniorButton.Size = new System.Drawing.Size(150, 40);
            seniorButton.BackColor = Color.FromArgb(0, 75, 200);
            seniorButton.ForeColor = Color.White;
            seniorButton.Text = "Senior";
            seniorButton.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            seniorButton.FlatStyle = FlatStyle.Flat;

            pwdButton.Location = new System.Drawing.Point(25, 160);
            pwdButton.Size = new System.Drawing.Size(150, 40);
            pwdButton.BackColor = Color.FromArgb(0, 75, 200);
            pwdButton.ForeColor = Color.White;
            pwdButton.Text = "PWD";
            pwdButton.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            pwdButton.FlatStyle = FlatStyle.Flat;

            cancelButton.Location = new System.Drawing.Point(25, 210);
            cancelButton.Size = new System.Drawing.Size(150, 40);
            cancelButton.BackColor = Color.Red;
            cancelButton.ForeColor = Color.White;
            cancelButton.Text = "Cancel";
            cancelButton.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            cancelButton.FlatStyle = FlatStyle.Flat;

            panel.Controls.Add(label);
            panel.Controls.Add(memberButton);
            panel.Controls.Add(seniorButton);
            panel.Controls.Add(pwdButton);
            panel.Controls.Add(cancelButton);


            memberButton.Click += (s, args) =>
            {
                discountText = "5";
                MessageBox.Show("Select an item");
                panel.Dispose();
            };

            seniorButton.Click += (s, args) =>
            {
                discountText = "20";
                MessageBox.Show("Select an item");
                panel.Dispose();
            };

            pwdButton.Click += (s, args) =>
            {
                discountText = "10";
                MessageBox.Show("Select an item");
                panel.Dispose();
            };

            cashierTab.Controls.Add(panel);
            panel.BringToFront();
            panel.BorderStyle = BorderStyle.FixedSingle;
        }

        private void handleCancel_Click(object sender, EventArgs e)
        {

        }

        private void memberDashButton_Click(object sender, EventArgs e)
        {

        }
    }
} 
