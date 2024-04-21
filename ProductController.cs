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
        private const string database = @"BIODOTA\SQLEXPRESS";

        private const string table = "POS_Products";

        private const string connString = "Data Source= "+ database + ";Initial Catalog= POS; Integrated Security=True;";

        static Random random = new Random();

        public class Product
        {
            public string ItemBarcode { get; set; }
            public string ItemName { get; set; }
            public string ItemPrice { get; set; }
            public string ItemQuantity { get; set; }
            public string ItemCategory {  get; set; }

            public Product(string barcode, string name, string price, string quantity, string category)
            {
                ItemBarcode = barcode;
                ItemName = name;
                ItemPrice = price;
                ItemQuantity = quantity;
                ItemCategory = category;
            }
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            string query = $"SELECT Barcode, Name, Price, Quantity, Category FROM {table}";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string productBarcode = reader.GetString(0);
                            string productName = reader.GetString(1);
                            string productPrice = reader.GetString(2);
                            string productQuantity = reader.GetString(3);
                            string productCategory = reader.GetString(4);

                            Product product = new Product(productBarcode, productName, productPrice, productQuantity, productCategory);
                            products.Add(product);
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

            return products;
        }

        public void SelectProductsByCategory(string category, Dictionary<int, Cashier.Product> productList)
        {

            string query = $"SELECT Barcode, Name, Price, Quantity FROM {table} WHERE Category = @Category";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@Category", category);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        int i = 0;

                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                string productBarcode = reader.GetString(0);
                                string productName = reader.GetString(1);
                                string productPrice = reader.GetString(2);
                                string productQuantity = reader.GetString(3);

                                Cashier.Product product = new Cashier.Product(productBarcode, productName, productPrice, productQuantity);
                                productList.Add(i, product);
                                i++;
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

        public void AddProduct( string name, string price, string quantity, string category)
        {
            string barcode = GenerateBarcode(16);

            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                string query = $"Insert Into {table} (Barcode, Name, Price, Quantity, Category) Values(N'{barcode}','{name}','{price}','{quantity}','{category}')";

                using (SqlCommand cmd = new SqlCommand(query, conn))
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

        public void UpdateProduct(string barcode, string updatedName, string updatedPrice, string udpatedQuantity, string updatedCategory)
        {
            SqlConnection conn = new SqlConnection(connString);
            {
                string query = $"UPDATE {table} SET Name = @UpdatedName, Price = @UpdatedPrice, Quantity = @UpdatedQuantity, Category = @UpdatedCategory WHERE Barcode = @Barcode";

                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Barcode", barcode);
                    command.Parameters.AddWithValue("@UpdatedName", updatedName);
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

        public void DeleteProduct(string barcode)
        {
            string query = $"DELETE FROM {table} WHERE Barcode = @Barcode";

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Barcode", barcode);
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
            }catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
