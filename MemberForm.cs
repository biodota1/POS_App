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

namespace OOP2_POS
{
    public partial class MemberForm : Form
    {
        List<string> itemName = new List<string>();
        List<string> itemPrice = new List<string>();
        List<string> itemQuantity = new List<string>();

        int total = 0;

        public MemberForm()
        {
            InitializeComponent();

            GetProductsInCategory("sdf");
            
            int location = 0;

            int labelLocation = 0;

            int index = 0;
            foreach (string productPrice in itemPrice)
            {
                string productName = itemName[index];
                string productQuantity = itemQuantity[index];
                int price = int.Parse(productPrice);
                CreateButton(productName, productPrice, productQuantity, 10+location, 10, 100, bakeryandbreadTab, GetButtonClickHandler(productName, price, labelLocation));
                index++;
                location += 120;
                labelLocation += 30;
            }


        }
 

        private void CreateButton(string text, string price, string quantity, int x, int y, int size, TabPage tabPage, EventHandler onClick)
        {
            Button button = new Button();
            button.Text = text+"\n"+price + "\n" + quantity;
            button.Location = new System.Drawing.Point(x, y);
            button.Size = new System.Drawing.Size(size, size);
            button.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            button.Click += onClick;
            tabPage.Controls.Add(button);
        }

        private void CreateLabel(string itemName, string itemPrice, int location)
        {
            Label label = new Label();
            label.Text = itemName+ "\t₱ " + itemPrice+"\t";
            label.Location = new System.Drawing.Point(10, 10+location);
            label.AutoSize = true;
            label.Font = new System.Drawing.Font("Arial", 15, System.Drawing.FontStyle.Bold);
            CalculationPanel.Controls.Add(label);
        }

        private EventHandler GetButtonClickHandler(string itemName, int price, int location)
        {
            return (sender, e) =>
            {
                
                CreateLabel(itemName, price.ToString(), location);
                total += price;
                string strTotal = total.ToString();
                totalCostLabel.Text = strTotal;
            };
        }


        private void GetProductsInCategory(string category)
        {
            List<string> productList = new List<string>();

            string dbSource = @"BIODOTA\SQLEXPRESS";
            string db = "POS";
            string connString = @"Data Source=" + dbSource + ";Initial Catalog=" + db + ";Integrated Security=True;";

            string query = "SELECT Description, Price, Quantity FROM POS_Items WHERE Category = @Category";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@Category", category);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();


                        if (reader.HasRows)
                        {
 
                            while (reader.Read())
                            {
                                string productName = reader.GetString(0);
                                string productPrice = reader.GetString(1);
                                string productQuantity = reader.GetString(2);
                                itemName.Add(productName);
                                itemPrice.Add(productPrice);
                                itemQuantity.Add(productQuantity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }


        }

        

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
