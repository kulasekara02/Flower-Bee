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
    public partial class Supplyer : Form
    {
        private SqlConnection sqlCon;
        private object cmdProductId;

        public int Price { get; private set; }

        public Supplyer()
        {
            try
            {
                DBConnection obj = new DBConnection();
                sqlCon = obj.getConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Error connecting " + ex, " Supplyer Form ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand
                    ("Update Product_DB set ProductName='" + cmdProductName.Text +
                    "',Qty='" + txtQty.Text + "',Price='" + txtPrice.Text +
                    "'where ProductId='" + cmdProductID.Text + "'", sqlCon);
                //cmd.Connection = sqlCon;
                int numberOfRecords = cmd.ExecuteNonQuery();
                if (numberOfRecords > 0)
                {
                    MessageBox.Show("Record Update", "Customer Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Updating Update", "Customer Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                SqlCommand cmd = new SqlCommand(" insert into Supplyer_DB values  ('" + txtSupplyerID.Text + "', '" + cmdProductID.Text + "',  '" + txtSupplyerName.Text + "','" + txtContactNo.Text + "','" + cmdProductName.Text + "','" + txtQty.Text + "','" + txtPrice.Text + "');");

                cmd.Connection = sqlCon;
                int temp = cmd.ExecuteNonQuery();

                if (temp > 0)
                {
                    MessageBox.Show("Record Successfuly Added", " Supplyer Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Record Fail To Added", "Supplyer Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Inserting Data" + ex, "Supplyer Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSupplyerName.Text = "";
            txtContactNo.Text = "";
            txtPrice.Text = "";
            txtQty.Text = "";
        }

        private void Supplyer_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select SupplyerID from Supplyer_DB ", sqlCon);
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
                        txtSupplyerID.Text = "S00" + CTR;
                    }
                    else if (CTR >= 9 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        txtSupplyerID.Text = "S0" + CTR;
                    }
                    else if (CTR >= 99)
                    {
                        CTR = CTR + 1;
                        txtSupplyerID.Text = "S" + CTR;
                    }

                }
                else
                {
                    txtSupplyerID.Text = "S001";
                }
                dr.Close();

            }
            catch (Exception e1)
            {
                MessageBox.Show("Error" + e1, "Supplyer Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                SqlCommand SelectProductNameCommand = new SqlCommand("Select ProductName from Product_DB", sqlCon);
                SqlDataAdapter da = new SqlDataAdapter(SelectProductNameCommand);
                DataSet ds = new DataSet();

                da.Fill(ds, "Product_DB");

                cmdProductName.DataSource = ds;
                cmdProductName.DisplayMember = "Product_DB.ProductName";
            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex, "Supplyer Form", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            try
            {
                SqlCommand SelectProductIDCommand = new SqlCommand("Select ProductID from Product_DB", sqlCon);
                SqlDataAdapter da = new SqlDataAdapter(SelectProductIDCommand);
                DataSet ds = new DataSet();

                da.Fill(ds, "Product_DB");

                cmdProductID.DataSource = ds;
                cmdProductID.DisplayMember = "Product_DB.ProductID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex, "Supplyer Form", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
             public int getQty()
        {
            int Qty = 0;
            try
            {
                SqlCommand selectCostCommand = new SqlCommand("Select Qty from Product_DB   where ProductID= '" + getProductName() + "'", sqlCon);

                SqlDataReader readerProduct = selectCostCommand.ExecuteReader();
                bool rowFoundpro = readerProduct.HasRows;
                String SQty = null;
                if (rowFoundpro)
                {
                    while (readerProduct.Read())
                    {
                        SQty = readerProduct[0].ToString();

                    }
                    Qty = Int32.Parse(SQty);
                }
                readerProduct.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting Quantity" + ex);
            }
            return Qty;
        }

        private string getProductName()
        {
            throw new NotImplementedException();
        }

        public String getProductId()
        {
            String ProductId = null;
            try
            {

                SqlCommand selectCostCommand = new SqlCommand
                ("Select ProductId from Product_DB where ProductName=@ProductID", sqlCon);

                selectCostCommand.Parameters.Add(new SqlParameter("ProductID", cmdProductId.ToString()));

                SqlDataReader readerProduct = selectCostCommand.ExecuteReader();
                bool rowFoundpro = readerProduct.HasRows;

                if (rowFoundpro)
                {
                    while (readerProduct.Read())
                    {
                        ProductId = readerProduct[0].ToString();

                    }

                }
                readerProduct.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting Product id" + ex);

            }
            return ProductId;
        }
       
       
        public int getprodctName()
        {

            try
            {
              
                txtQty.Text = "";
                SqlDataReader readerProduct = null;
                SqlCommand selectCostCommand = new SqlCommand("Select ProductName from Product_DB where ProductId= @ProductId", sqlCon);
                //  MessageBox.Show(cmbProductId.Text);
                if (string.IsNullOrEmpty(getProductId()))
                {
                    selectCostCommand.Parameters.Add(new SqlParameter("ProductId", "P001"));
                }
                else
                {
                    selectCostCommand.Parameters.Add(new SqlParameter("ProductId", getProductId()));
                    readerProduct = selectCostCommand.ExecuteReader();
                    bool rowFoundpro = readerProduct.HasRows;
                    if (rowFoundpro)
                    {
                        while (readerProduct.Read())
                        {
                            Price = Int32.Parse(readerProduct[0].ToString());

                        }

                    }

                    readerProduct.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
            return Price;
        }







        private void btnHome_Click(object sender, EventArgs e)
        {
            MainMenu frmobj = new MainMenu();
            frmobj.Show();
            this.Hide();
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtQty.Text))
                {
                    int QtyValue = Int32.Parse(txtQty.Text.ToString());

                    if (getQty() > QtyValue)
                    {
                        int final = Price  + QtyValue;
                        txtQty.Text = final.ToString();
                    }
                    else
                    {
                        MessageBox.Show("not enough stock available", "Invoice form", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        
                        txtQty.Text = "";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calcualting total" + ex);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Supplyer frmobj = new Supplyer();
            frmobj.Show();
            this.Hide();

        }
    }
}
