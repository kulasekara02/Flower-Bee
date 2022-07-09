using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flower_Bee
{
    public partial class Login : Form
    {
       

        public Login()
        {
            InitializeComponent();
            this.ActiveControl = cmdUserName;
            cmdUserName.Focus();
        }
        int ctr = 0;


        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user_id = null, password = null;
            bool valid = true;

            user_id = cmdUserName.Text.ToString();
            password = txtPassword.Text.ToString();

            if (string.IsNullOrEmpty(cmdUserName.Text) ||
                string.IsNullOrEmpty(txtPassword.Text))

            {
                MessageBox.Show("User ID or Password Can Not Be Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }
            if (valid)
            {
                if (user_id == "Vimukthi" && password == "1234")
                {

                    MainMenu frmobj = new MainMenu();
                    frmobj.Show();
                    this.Hide();
                }

                else if (user_id == "Anjana" && password == "5678")
                {

                    MainMenu frmobj = new MainMenu();
                    frmobj.Show();
                    this.Hide();
                }

                else if (ctr < 3)
                {
                    MessageBox.Show("Invalid Username or Password. Please Try Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctr = ctr + 1;
                    cmdUserName.Focus();
                }

                else
                {
                    MessageBox.Show("Unauthorized Access", "Login Form", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    Close();
                }





            }
        }
    

        

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                btnLogin.PerformClick();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                btnLogin.PerformClick();
            }

        }
    }
 }
