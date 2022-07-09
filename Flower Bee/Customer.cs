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

namespace Flower_Bee
{
    public partial class Customer : Form
    {
        private SqlConnection sqlCon;

        public Customer()
        {
            try
            {
                DBConnection obj = new DBConnection();
                sqlCon = obj.getConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Error connecting " + ex, " Customer Form ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            InitializeComponent();
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            MainMenu frmobj = new MainMenu();
            frmobj.Show();
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(" insert into Customer_DB values  ('" + txtCustomerId.Text + "', '" + txtCustomerName.Text + "', '" + txtAddress.Text + "', '" + txtContactNo.Text + "');");

                cmd.Connection = sqlCon;
                int temp = cmd.ExecuteNonQuery();

                if (temp > 0)
                {
                    MessageBox.Show("Record Successfuly Added", " Customer Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Record Fail To Added", "Customer Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Inserting Data" + ex, "Customer Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select CustomerId from Customer_DB ", sqlCon);
                SqlDataReader dr = cmd.ExecuteReader();
                string id = "";
                bool NumberofRows = dr.HasRows;
                if (NumberofRows)
                {
                    while (dr.Read())
                    {
                        id = dr[0].ToString();
                    }
                    string idString = id.Substring(1);
                    int CTR = Int32.Parse(idString);
                    if (CTR >= 1 && CTR < 9)
                    {
                        CTR = CTR + 1;
                        txtCustomerId.Text = "C00" + CTR;
                    }
                    else if (CTR >= 9 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        txtCustomerName.Text = "C0" + CTR;
                    }
                    else if (CTR >= 99)
                    {
                        CTR = CTR + 1;
                        txtCustomerId.Text = "C" + CTR;
                    }

                }
                else
                {
                    txtCustomerId.Text = "C001";
                }
                dr.Close();

            }
        
            catch (Exception e1)
            {
                MessageBox.Show("Error" + e1, "Customer Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                SqlCommand SelectCustomerNameCommand = new SqlCommand("Select CustomerName from Customer_DB", sqlCon);
                SqlDataAdapter da = new SqlDataAdapter(SelectCustomerNameCommand);
                DataSet ds = new DataSet();

                da.Fill(ds, "Customer_DB");

                cmdCustomerName.DataSource = ds;
                cmdCustomerName.DisplayMember = "Customer_DB.CustomerName";

            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex, "Customer Form", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            
            txtCustomerName.Text = "";
            txtContactNo.Text = "";
            txtAddress.Text = "";
        }

        private void cmdCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlCommand SearchCustomerNameCommand = new SqlCommand("Select * from Customer_DB where CustomerName='" + cmdCustomerName.Text.ToString() + "'", sqlCon);
                SqlDataAdapter da = new SqlDataAdapter(SearchCustomerNameCommand);
                DataSet ds = new DataSet();

                da.Fill(ds, "Customer_DB");

                dgCustomerDetails.DataSource = ds;
                dgCustomerDetails.DataMember = "Customer_DB";

            }
            catch (Exception ex)
            {
                MessageBox.Show("error geting customer names!" + ex);

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customer frmobj = new Customer();
            frmobj.Show();
            this.Hide();

        }
    }
}
