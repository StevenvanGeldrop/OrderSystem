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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Open the login & register form in Login mode
            FrmLoginRegister loginForm = new FrmLoginRegister(AuthMode.Login);
            loginForm.ShowDialog();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Open the login & register form in Register mode
            FrmLoginRegister registerForm = new FrmLoginRegister(AuthMode.Register);
            registerForm.ShowDialog();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (UserController.IsLoggedIn == true)
            {
                // Open the order form
                FrmOrder frmOrder = new FrmOrder();
                frmOrder.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please log in to place an order.", "Not Logged In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
