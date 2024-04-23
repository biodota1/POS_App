using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2_POS
{
    public partial class EditItem : Form
    {
        private string category = "";

        private string barcode = "";

        public EditItem(AdminForm.Product product)
        {
            InitializeComponent();

            InitializeComboBox(product.ItemCategory);

            editItemName.Text = product.ItemName;
            editItemPrice.Text = product.ItemPrice;
            editItemQuantity.Text = product.ItemPrice;
            barcode = product.ItemBarcode;
        }

        private void doneEditButton_Click(object sender, EventArgs e)
        {
            AdminForm adminForm = new AdminForm();

            string name = editItemName.Text;
            string price = editItemPrice.Text;
            string quantity = editItemQuantity.Text;

            ProductController controller = new ProductController();

            controller.UpdateProduct(barcode, name, price, quantity, category);

            adminForm.RefreshForm();

            this.Dispose();
        }

        private void InitializeComboBox(string category)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Location = new System.Drawing.Point(23, 250);
            comboBox.Size = new System.Drawing.Size(227, 30);
            comboBox.Font = new Font("Arial", 12, FontStyle.Regular);
            comboBox.Text = category;

            // Add items to the ComboBox
            comboBox.Items.Add("FruitsAndVeggie");
            comboBox.Items.Add("DairyAndEggs");
            comboBox.Items.Add("MeatAndSeafood");
            comboBox.Items.Add("BakeryAndBread");
            comboBox.Items.Add("CannedGoods");
            comboBox.Items.Add("DryGoodsAndGrains");
            comboBox.Items.Add("Beverage");
            comboBox.Items.Add("SnacksAndSweets");
            comboBox.Items.Add("FrozenFoods");
            comboBox.Items.Add("NonFood");

            // Add event handler for selection change
            comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;

            // Add ComboBox to the form's Controls collection
            this.Controls.Add(comboBox);
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle selection change event
            ComboBox comboBox = (ComboBox)sender;
            string selectedOption = comboBox.SelectedItem.ToString();
            category = selectedOption;
        }

        private void editItemName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
