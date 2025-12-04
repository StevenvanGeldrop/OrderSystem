using OrderSystem.Controller;
using OrderSystem.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderSystem.View
{
    public partial class FrmLoginRegister : Form
    {
        private AuthMode currentMode;

        public FrmLoginRegister(AuthMode mode)
        {
            InitializeComponent();
            currentMode = mode;
            UpdateForm();
        }

        private void UpdateForm()
        {
            if (currentMode == AuthMode.Login)
            {
                btnSubmit.Text = "Login";
                this.Text = "User Login";
            }
            else
            {
                btnSubmit.Text = "Register";
                this.Text = "User Registration";
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(tbUsername.Text))
            {
                MessageBox.Show("Please enter a username.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                MessageBox.Show("Please enter a password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Perform authentication
            try
            {
                // Attempt login
                if (currentMode == AuthMode.Login)
                {
                    bool success = UserController.CheckLogin(tbUsername.Text, tbPassword.Text);

                    if (success)
                    {
                        MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Attempt registration
                    int affectedRows = UserController.Register(tbUsername.Text, tbPassword.Text);

                    if (affectedRows == 1)
                    {
                        MessageBox.Show("Registration successful! You can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Registration failed. The username may already be taken.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while processing your request. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
