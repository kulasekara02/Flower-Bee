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
    public partial class Employee : Form
    {
        private SqlConnection sqlCon;

        public Employee()
        {
            try
            {
                DBConnection obj = new DBConnection();
                sqlCon = obj.getConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Error connecting " + ex, " Employee Form ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                SqlCommand cmd = new SqlCommand(" insert into Employee_DB values  ('" + txtEmployeeID.Text + "', '" + txtEmployeeName.Text + "',  '" + txtNIC.Text + "','" + txtContactNo.Text + "','" + txtAddress.Text + "');");

                cmd.Connection = sqlCon;
                int temp = cmd.ExecuteNonQuery();

                if (temp > 0)
                {
                    MessageBox.Show("Record Successfuly Added", " Employee Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Record Fail To Added", "Employee Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Inserting Data" + ex, "Employee Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Employee_Load(object sender, EventArgs e)
        {

            try
            {
                SqlCommand cmd = new SqlCommand("Select EmployeeId from Employee_DB ", sqlCon);
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
                        txtEmployeeID.Text = "E00" + CTR;
                    }
                    else if (CTR >= 9 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        txtEmployeeID.Text = "E0" + CTR;
                    }
                    else if (CTR >= 99)
                    {
                        CTR = CTR + 1;
                        txtEmployeeID.Text = "E" + CTR;
                    }

                }
                else
                {
                    txtEmployeeID.Text = "E001";
                }
                dr.Close();

            }
            catch (Exception e1)
            {
                MessageBox.Show("Error" + e1, "Employee Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEmployeeName.Text = "";
            txtNIC.Text = "";
            txtContactNo.Text = "";
            txtAddress.Text = "";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Employee frmobj = new Employee();
            frmobj.Show();
            this.Hide();

        }
    }
}
