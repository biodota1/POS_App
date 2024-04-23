using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
/*using Xceed.Document.NET;
using Xceed.Words.NET;*/
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace OOP2_POS
{
    public class Cashier 
    {

        public static void PrintReceipt(List<ProductController.Product> purchasedItem, int total)
        {
            Document doc = new Document();

            try
            {
                // Specify the path for the PDF file
                string filePath = "SamplePdf.pdf";

                // Create a PdfWriter instance to write the document to a file
                PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));

                // Open the document
                doc.Open();

                // Add content to the document
                doc.Add(new Paragraph("DEVJACKIE POS"));
                doc.Add(new Paragraph("_______________________________________"));
                doc.Add(new Paragraph("N.Bacalso, Lipata, Cebu"));
                doc.Add(new Paragraph("Philippine"));
                doc.Add(new Paragraph("Tel : 0912345678"));
                doc.Add(new Paragraph("_______________________________________"));
                doc.Add(new Paragraph("PURCHASED RECEIPT"));
                doc.Add(new Paragraph("_______________________________________"));
                doc.Add(new Paragraph("Staff: Janine"));
                doc.Add(new Paragraph("Data: 04-22-2024"));
                doc.Add(new Paragraph("_______________________________________"));
                doc.Add(new Paragraph("Description\t\t\tAmount"));
                doc.Add(new Paragraph("_______________________________________"));
                for(int i = 0; i < purchasedItem.Count; i++)
                {
                    doc.Add(new Paragraph($"{purchasedItem[i].ItemName}\t\t\t{purchasedItem[i].ItemPrice}"));
                }
                doc.Add(new Paragraph("_______________________________________"));
                doc.Add(new Paragraph($"Total Amount\t\t\t{total}"));
                doc.Add(new Paragraph($"Paid Amount\t\t\t{total}"));
                doc.Add(new Paragraph($"Change Amount\t\t\t{total}"));
                doc.Add(new Paragraph("_______________________________________"));
                doc.Add(new Paragraph($"Number of Items:\t\t\t{purchasedItem.Count}"));
                doc.Add(new Paragraph("_______________________________________"));
                doc.Add(new Paragraph("Keep bill for exchange within 7 days"));
                doc.Add(new Paragraph("Thank you for Shopping"));
                doc.Add(new Paragraph("Shop online at www.DEVJAKIEPOS.com"));


            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                // Close the document
                doc.Close();
            }

            OpenPdfFile("SamplePdf.pdf");
        }

        static void OpenPdfFile(string filePath)
        {
            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Open the file using the default associated application
                Process.Start(filePath);
            }
            else
            {
                Console.WriteLine("File does not exist: " + filePath);
            }
        }

 /*       public static void ProductQuantity(Panel panelRef, string quantity)
        {
            MemberForm form = new MemberForm();

            Panel panel = new Panel();
            panel.Location = new System.Drawing.Point(50, 200);
            panel.Size = new System.Drawing.Size(300, 300);
            panel.BorderStyle = BorderStyle.FixedSingle; // Optional: Set border style

            // Create a label
            Label label = new Label();
            label.Text = "Quantity";
            label.Location = new System.Drawing.Point(10, 10); // Position within the panel

            // Create a TextBox
            TextBox textBox = new TextBox();
            textBox.Location = new System.Drawing.Point(10, 10); // Position within the panel
            textBox.Size = new System.Drawing.Size(50, 50);

            // Create a button
            Button button = new Button();
            button.Text = "OK";
            button.Location = new System.Drawing.Point(10, 40); // Position within the panel
            button.Click += (sender, e) =>
            {
                MessageBox.Show(textBox.Text);
                form.ChangeQuantity = "15";
            };
 
            // Add the label and button to the panel
            panel.Controls.Add(textBox);
            panel.Controls.Add(label);
            panel.Controls.Add(button);


            // Add the panel to the form
            panelRef.Controls.Add(panel);
        }*/


        public class Product
        {
            public string ItemBarcode {  get; set; }
            public string ItemName { get; set; }
            public string ItemPrice { get; set; }
            public string ItemQuantity { get; set; }


            public Product(string barcode,string name, string price, string quantity)
            {
                ItemBarcode = barcode;
                ItemName = name;
                ItemPrice = price;
                ItemQuantity = quantity;
            }

            public string GetPrice()
            {
                return ItemPrice;
            }
        }
    }
}
