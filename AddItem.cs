﻿using System;
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
    public partial class AddItem : Form
    {
        private string category = "";

        public AddItem()
        {
            InitializeComponent();

            InitializeComboBox();
        }

     

        private void addItemToDbButton_Click(object sender, EventArgs e)
        {
            AdminForm adminForm = new AdminForm();

            string name = addItemName.Text;
            string price = addItemPrice.Text;
            string quantity = addItemPrice.Text;

            ProductController controller = new ProductController();

            controller.AddProduct(name, price, quantity, category);

            
            adminForm.RefreshForm();

            this.Dispose();

            
        }

        private void InitializeComboBox()
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Location = new System.Drawing.Point(23, 250);
            comboBox.Size = new System.Drawing.Size(227, 30);
            comboBox.Font = new Font("Arial", 12, FontStyle.Regular);

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
    }
}
