using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP2_POS
{
    public class ProductController
    {
        private const string connString = @"Data Source= BIODOTA\SQLEXPRESS;Initial Catalog= POS;Integrated Security=True;";

        static Random random = new Random();

        public void GetAllProducts(ListView listview)
        {
            string query = "SELECT Description, Username, Role FROM POSUsers";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        listview.Columns.Clear();
                        listview.Items.Clear();


                        listview.Columns.Add("Description");
                        listview.Columns.Add("Price");
                        listview.Columns.Add("Quantity");
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["Description"].ToString());
                            item.SubItems.Add(reader["Price"].ToString());
                            item.SubItems.Add(reader["Quantity"].ToString());
                            listview.Items.Add(item);

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


        public void AddProduct( string description, string price, string quantity, string category)
        {
            string barcode = GenerateBarcode(16);

            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                string sqlQuery = $"Insert Into POS_Items (Barcode, Description, Price, Quantity, Category) Values(N'{barcode}','{description}','{price}','{quantity}','{category}')";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("New Item has been added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateProduct(string updatedDescription, string updatedPrice, string udpatedQuantity, string updatedCategory)
        {
            SqlConnection conn = new SqlConnection(connString);
            {
                string query = "UPDATE POS_Items SET Description = @UpdatedDescription, Price = @UpdatedPrice, Quantity = @UpdatedQuantity, Category = @UpdatedCategory WHERE Description = @Description";

                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@UpdatedDescription", updatedDescription);
                    command.Parameters.AddWithValue("@UpdatedPrice", updatedPrice);
                    command.Parameters.AddWithValue("@UpdatedQuantity", udpatedQuantity);
                    command.Parameters.AddWithValue("@UpdatedCategory", updatedCategory);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User information updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("User information update failed.");
                    }
                }
            }
        }

        public void DeleteUser(string description)
        {
            string query = "DELETE FROM POS_Items WHERE Description = @Description";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Description", description);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("User not found or deletion failed.");
                    }
                }
            }
        }

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
    }
}
