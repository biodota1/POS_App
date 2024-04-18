using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace OOP2_POS
{
    public partial class AuthForm : Form
    {

        public string UserRole {  get; set; }

        public AuthForm()
        {
            InitializeComponent();

            userInvalidInput.Hide();
            userInvalidInputIcon.Hide();
            passErrorIcon.Hide();
            passWrongPassword.Hide();
            
            regEmailError.Hide();
            regEmailErrorIcon.Hide();
            regUserError.Hide();
            regUserErrorIcon.Hide();
            regPassError.Hide();
            regPassErrorIcon.Hide();

        }

        private void linkToRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            userInvalidInput.Hide();
            userInvalidInputIcon.Hide();
            passErrorIcon.Hide();
            passWrongPassword.Hide();

            authTabControl.SelectTab(1);
        }

        private void linkToLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            regEmailError.Hide();
            regEmailErrorIcon.Hide();
            regUserError.Hide();
            regUserErrorIcon.Hide();
            regPassError.Hide();
            regPassErrorIcon.Hide();

            authTabControl.SelectTab(0);
        }


        private void loginButton_Click(object sender, EventArgs e)
        {
            if (usernameTextbox.Text == "")
            {
                userInvalidInput.Show();
                userInvalidInputIcon.Show();
            }
            if (passwordTextBox.Text == "")
            {
                passWrongPassword.Show();
                passErrorIcon.Show();
            }
            if (usernameTextbox.Text != "" && passwordTextBox.Text != "")
            {
                userInvalidInput.Hide();
                userInvalidInputIcon.Hide();
                passErrorIcon.Hide();
                passWrongPassword.Hide();


                string username = usernameTextbox.Text;
                string password = passwordTextBox.Text;

                UserController controller = new UserController();

                if (controller.ValidateUser(username, password))
                {
                    MessageBox.Show("Login Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    UserRole = controller.UserRoles(username);
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if(regEmailInput.Text != "" && regUsernameInput.Text != "" && regPasswordInput.Text != "")
            {
                regEmailError.Hide();
                regEmailErrorIcon.Hide();
                regUserError.Hide();
                regUserErrorIcon.Hide();
                regPassError.Hide();
                regPassErrorIcon.Hide();

                string email = regEmailInput.Text;
                string username = regUsernameInput.Text;
                string password = regPasswordInput.Text;

                UserController controller = new UserController();

                controller.CreateUser(email, username, password);
            }
            if (regEmailInput.Text == "")
            {
                regEmailError.Show();
                regEmailErrorIcon.Show();
            }
            if (regUsernameInput.Text == "")
            {
                regUserError.Show();
                regUserErrorIcon.Show();
            }
            if (regPasswordInput.Text == "")
            {
                regPassError.Show();
                regPassErrorIcon.Show();
            }
        }

   
    }
}
