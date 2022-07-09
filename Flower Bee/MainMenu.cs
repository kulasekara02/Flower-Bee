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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnFlower_Click(object sender, EventArgs e)
        {
            Product frmobj = new Product();
            frmobj.Show();
            this.Hide();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            Customer frmobj = new Customer();
            frmobj.Show();
            this.Hide();
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            Invoice frmobj = new Invoice();
            frmobj.Show();
            this.Hide();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            Employee frmobj = new Employee();
            frmobj.Show();
            this.Hide();

        }

        private void btnSupplyer_Click(object sender, EventArgs e)
        {
            Supplyer frmobj = new Supplyer();
            frmobj.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
