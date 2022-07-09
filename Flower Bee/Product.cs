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
    public partial class Product : Form
    {
        private SqlConnection sqlCon;

        public Product()
        {
            try
            {
                DBConnection obj = new DBConnection();
                sqlCon = obj.getConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Error connecting " + ex, " Product Form ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                SqlCommand cmd = new SqlCommand(" insert into Product_DB values  ('" + txtProductId.Text + "', '" + txtProductName.Text + "',  '" + txtQty.Text + "','" + txtPrice.Text + "');");

                cmd.Connection = sqlCon;
                int temp = cmd.ExecuteNonQuery();

                if (temp > 0)
                {
                    MessageBox.Show("Record Successfuly Added", " Product Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Record Fail To Added", "Product Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Inserting Data" + ex, "Product Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Product_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select ProductId from Product_DB ", sqlCon);
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
                        txtProductId.Text = "P00" + CTR;
                    }
                    else if (CTR >= 9 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        txtProductName.Text = "P0" + CTR;
                    }
                    else if (CTR >= 99)
                    {
                        CTR = CTR + 1;
                        txtProductId.Text = "P" + CTR;
                    }

                }
                else
                {
                    txtProductId.Text = "P001";
                }
                dr.Close();

            }
            catch (Exception e1)
            {
                MessageBox.Show("Error" + e1, "Product Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                SqlCommand SelectCatogeryCommand = new SqlCommand("Select distinct ProductName from Product_DB", sqlCon);
                SqlDataAdapter da = new SqlDataAdapter(SelectCatogeryCommand);
                DataSet ds = new DataSet();

                da.Fill(ds, "Product_DB");

                cmdProductName.DataSource = ds;
                cmdProductName.DisplayMember = "Product_DB.ProductName";

            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex, "Product Form", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            
            txtProductName.Text = "";
            txtPrice.Text = "";
            txtQty.Text = "";
        }

        private void cmdProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlCommand SearchCategoryCommand = new SqlCommand("Select * from Product_DB where ProductName='" + cmdProductName.Text.ToString() + "'", sqlCon);
                SqlDataAdapter da = new SqlDataAdapter(SearchCategoryCommand);
                DataSet ds = new DataSet();

                da.Fill(ds, "Product_DB");

                dgProductDetails.DataSource = ds;
                dgProductDetails.DataMember = "Product_DB";

            }
            catch (Exception ex)
            {
                MessageBox.Show("error geting Product names!" + ex);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Product frmobj = new Product();
            frmobj.Show();
            this.Hide();

        }
    }
}
