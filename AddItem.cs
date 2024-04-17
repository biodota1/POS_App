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

        private void addItemToDbButton_Click(object sender, EventArgs e)
        {
            string name = addItemName.Text;
            string price = addItemPrice.Text;
            string quantity = addItemPrice.Text;
            string category = addItemCategory.Text;


            string dbSource = @"BIODOTA\SQLEXPRESS";
            string db = "POS";
            string connString = @"Data Source=" + dbSource + ";Initial Catalog=" + db + ";Integrated Security=True;";

            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                string sqlQuery = $"Insert Into POS_Items (Description, Price, Quantity, Category) Values(N'{name}','{price}','{quantity}','{category}')";

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
        }
    }
}
