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
        int itemsPerColumn = 0;
        int total = 0;
        int buttonLocationX = 0;
        int buttonLocationY = 0;
        int panelLocation = 0;

        private string quantityValue = "1";

        public string ChangeQuantity
        {
            get { return quantityValue; }
            set { quantityValue = value; }
        }

        Dictionary<int, Cashier.Product> productList = new Dictionary<int, Cashier.Product>();

       

        public MemberForm()
        {
            InitializeComponent();

            ProductController controller = new ProductController();

            controller.SelectProductsByCategory("Fruits", productList);

            for(int i = 0; i < productList.Count; i++)
            {
                CreateAButton(productList, 10 + buttonLocationX, 10 + buttonLocationY, 100, bakeryandbreadTab,i);
                itemsPerColumn++;
                if (itemsPerColumn <4)
                {
                    buttonLocationX += 120;
                }
                else
                {
                    buttonLocationY += 120;
                    buttonLocationX = 0;
                }
                
            }

        }

        private void handleQuantity_Click(object sender, EventArgs e)
        {
            Panel panel = new Panel();
            TextBox textBox = new TextBox();
            Button panelButton = new Button();
            Label label = new Label();

            // Set properties for the controls
            panel.Location = new System.Drawing.Point(50, 200);
            panel.Size = new System.Drawing.Size(300, 300);
            label.Location = new System.Drawing.Point(0, 10); // Position within the panel
            label.Size = new System.Drawing.Size(100, 20);
            label.Text = "Quantity";
            label.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            textBox.Location = new System.Drawing.Point(0, 30); // Position within the panel
            textBox.Size = new System.Drawing.Size(50, 50);
            panelButton.Location = new System.Drawing.Point(0, 70);
            panelButton.Size = new System.Drawing.Size(80, 30);
            panelButton.Text = "OK";

            // Add controls to the panel
            panel.Controls.Add(label);
            panel.Controls.Add(textBox);
            panel.Controls.Add(panelButton);

            // Add click event handler for the panel button
            panelButton.Click += (s, args) =>
            {
                quantityValue = textBox.Text;
                MessageBox.Show("Select an item");
                panel.Dispose(); // Dispose the panel after use
            };

            // Show the panel
            CalculationPanel.Controls.Add(panel);

        }

        private void CreateAButton(Dictionary<int, Cashier.Product> dictionary, int x, int y, int size, TabPage tabPage, int index)
        {
            Button button = new Button();
            Cashier.Product product = dictionary[index];

            button.Text = product.ItemName + "\n" + product.ItemPrice + "\n" + product.ItemQuantity;
            button.Location = new System.Drawing.Point(x, y);
            button.Size = new System.Drawing.Size(size, size);
            button.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            int productPrice = int.Parse(product.ItemPrice);
            int productQuantity = int.Parse(quantityValue);
            productPrice *= productQuantity;
            button.Click += (sender, e) =>
            {
                Label label = new Label();
                string text = $"{product.ItemName.PadRight(10)}P {product.ItemPrice.PadRight(10)}X{quantityValue}";
                label.Text = text;
                label.Location = new System.Drawing.Point(10, 10+ panelLocation);
                label.AutoSize = true;
                label.Font = new System.Drawing.Font("Arial", 15, System.Drawing.FontStyle.Bold);
                CalculationPanel.Controls.Add(label);

                buttonLocationX += 30;

                total += productPrice;
                string strTotal = total.ToString();
                totalCostLabel.Text = strTotal;

                quantityValue = "1";
                panelLocation += 30;
            };
            tabPage.Controls.Add(button);
            
        }

/*        private EventHandler GetButtonClickHandler(string itemName, int price, int location)
        {
            return (sender, e) =>
            {
                total += price;
                string strTotal = total.ToString();
                totalCostLabel.Text = strTotal;
            };
        }*/


        /*  private void CreateButton(string text, string price, string quantity, int x, int y, int size, TabPage tabPage, EventHandler onClick)
          {
              Button button = new Button();
              button.Text = text + "\n" + price + "\n" + quantity;
              button.Location = new System.Drawing.Point(x, y);
              button.Size = new System.Drawing.Size(size, size);
              button.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
              button.Click += onClick;
              tabPage.Controls.Add(button);
          }

          private void CreateLabel(string itemName, string itemPrice, int location)
          {
              Label label = new Label();
              label.Text = itemName + "\t₱ " + itemPrice + "\t";
              label.Location = new System.Drawing.Point(10, 10 + location);
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
  */




        

        private void handlePrintReceipt_Click(object sender, EventArgs e)
        {
            Cashier.PrintReceipt();
        }

    }
} 
