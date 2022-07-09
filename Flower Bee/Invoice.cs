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
    public partial class Invoice : Form
    {
        private SqlConnection sqlCon;
        int Price = 0;
       

        public Invoice()
        {
            try
            {
                DBConnection obj = new DBConnection();
                sqlCon = obj.getConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Error connecting " + ex, " Invoice Form ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmdCustomerID.Text = "";
            cmdProductId.Text = "";
            txtTotal.Text = "";
            txtQty.Text = "";
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
                SqlCommand insertCommand = new SqlCommand("INSERT INTO Invoice_DB VALUES (@InvoiceId, @ProductID, @CustomerId, @Qty,@Total)", sqlCon);
                insertCommand.Parameters.Add(new SqlParameter("InvoiceId", txtInvoiceId.Text.ToString()));
                insertCommand.Parameters.Add(new SqlParameter("ProductID", getProductId()));
                insertCommand.Parameters.Add(new SqlParameter("CustomerId", getCustomerId()));
                insertCommand.Parameters.Add(new SqlParameter("Qty", txtQty.Text.ToString()));
                insertCommand.Parameters.Add(new SqlParameter("Total", txtTotal.Text.ToString()));
                insertCommand.ExecuteNonQuery();
                MessageBox.Show("Data inserted", "Customer Form", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SqlCommand ReduceStockCommand = new SqlCommand("update Product_DB set Qty=Qty-@qty where ProductID=@ProductID", sqlCon);
                ReduceStockCommand.Parameters.Add(new SqlParameter("Qty", txtQty.Text.ToString()));
                ReduceStockCommand.Parameters.Add(new SqlParameter("ProductID", getProductId()));
                ReduceStockCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting" + ex);
            }

        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select InvoiceId from Invoice_DB ", sqlCon);
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
                        txtInvoiceId.Text = "I00" + CTR;
                    }
                    else if (CTR >= 9 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        txtInvoiceId.Text = "I0" + CTR;
                    }
                    else if (CTR >= 99)
                    {
                        CTR = CTR + 1;
                        txtInvoiceId.Text = "I" + CTR;
                    }

                }
                else
                {
                    txtInvoiceId.Text = "I001";
                }
                dr.Close();

            }
            catch (Exception e1)
            {
                MessageBox.Show("Error" + e1, "Invoice Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                SqlCommand SelectCustomerNameCommand = new SqlCommand("Select CustomerName from Customer_DB", sqlCon);
                SqlDataAdapter da = new SqlDataAdapter(SelectCustomerNameCommand);
                DataSet ds = new DataSet();

                da.Fill(ds, "Customer_DB");

                cmdCustomerID.DataSource = ds;
                cmdCustomerID.DisplayMember = "Customer_DB.CustomerName";

            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex, "Invoice Form", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            try
            {
                SqlCommand SelectProductNameCommand = new SqlCommand("Select ProductName from Product_DB", sqlCon);
                SqlDataAdapter da = new SqlDataAdapter(SelectProductNameCommand);
                DataSet ds = new DataSet();

                da.Fill(ds, "Product_DB");

                cmdProductId.DataSource = ds;
                cmdProductId.DisplayMember = "Product_DB.ProductName";

            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex, "Invoice Form", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
            public String getProductId()
        {
            String ProductId = null;
            try
            {

                SqlCommand selectCostCommand = new SqlCommand
                ("Select ProductId from Product_DB where ProductName=@ProductName", sqlCon);

                selectCostCommand.Parameters.Add(new SqlParameter("ProductName", cmdProductId.Text.ToString()));

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
                MessageBox.Show("Error getting customer id" + ex);

            }
            return ProductId;
        }
        public String getCustomerId()
        {
            String CustId = null;
            try
            {
                SqlCommand selectCostCommand = new SqlCommand("Select CustomerId from Customer_DB   where CustomerName= @CustomerName", sqlCon);
                selectCostCommand.Parameters.Add(new SqlParameter("CustomerName", cmdCustomerID.Text.ToString()));
                SqlDataReader readerProduct = selectCostCommand.ExecuteReader();
                bool rowFoundpro = readerProduct.HasRows;

                if (rowFoundpro)
                {
                    while (readerProduct.Read())
                    {
                        CustId = readerProduct[0].ToString();

                    }

                }
                readerProduct.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting customer id" + ex);
            }
            return CustId;
        }
        public int getQty()
        {
            int Qty = 0;
            try
            {
                SqlCommand selectCostCommand = new SqlCommand("Select Qty from Product_DB   where ProductID= '" + getProductId() + "'", sqlCon);

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
        public int getprodctPrice()
        {

            try
            {
                txtTotal.Text = "";
                txtQty.Text = "";
                SqlDataReader readerProduct = null;
                SqlCommand selectCostCommand = new SqlCommand("Select Price from Product_DB where ProductId= @ProductId", sqlCon);
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

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtQty.Text))
                {
                    int QtyValue = Int32.Parse(txtQty.Text.ToString());

                    if (getQty() > QtyValue)
                    {
                        int final = Price * QtyValue;
                        txtTotal.Text = final.ToString();
                    }
                    else
                    {
                        MessageBox.Show("not enough stock available", "Invoice form", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        txtTotal.Text = "";
                        txtQty.Text = "";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calcualting total" + ex);
            }

        }

    

        private void cmdProductId_SelectedValueChanged(object sender, EventArgs e)
        {String pID = getProductId();
            MessageBox.Show(pID);
            int pCost = getprodctPrice();
            MessageBox.Show("cost" + pCost);
            lblStock.Text = "Available Stock:" + getQty().ToString();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Invoice frmobj = new Invoice();
            frmobj.Show();
            this.Hide();

        }
    }
    }

