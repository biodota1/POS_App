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
    public partial class PaymentMethod : Form
    {
        public PaymentMethod()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }


        private void cashButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bankButton_Click(object sender, EventArgs e)
        {
            paymentTabControl.SelectTab(1);
        }

        private void eWalletButton_Click(object sender, EventArgs e)
        {
            paymentTabControl.SelectTab(2);
        }

        private void debitButton_Click(object sender, EventArgs e)
        {
            paymentTabControl.SelectTab(3);
        }

        private void chequeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
