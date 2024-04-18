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
    public partial class AddItem : Form
    {
        public AddItem()
        {
            InitializeComponent();
        }

        static Random random = new Random();

        static string GenerateBarcode(int length)
        {
            const string chars = "0123456789";
            char[] barcode = new char[length];
            for (int i = 0; i < length; i++)
            {
                barcode[i] = chars[random.Next(chars.Length)];
            }
            return new string(barcode);
        }

        private void addItemToDbButton_Click(object sender, EventArgs e)
        {

            AdminForm adminForm = new AdminForm();

            string name = addItemName.Text;
            string price = addItemPrice.Text;
            string quantity = addItemPrice.Text;
            string category = addItemCategory.Text;
            string barcode = GenerateBarcode(16);


            string dbSource = @"BIODOTA\SQLEXPRESS";
            string db = "POS";
            string connString = @"Data Source=" + dbSource + ";Initial Catalog=" + db + ";Integrated Security=True;";

            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                string sqlQuery = $"Insert Into POS_Items (Barcode, Description, Price, Quantity, Category) Values(N'{barcode}','{name}','{price}','{quantity}','{category}')";

                using(SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("New Item has been added.", "Success",MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            adminForm.GetAllProducts();
            adminForm.CreateLog("Add Item", name);
        }
    }
}
