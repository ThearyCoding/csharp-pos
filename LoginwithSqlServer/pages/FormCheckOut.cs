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

namespace LoginwithSqlServer
{
    public partial class FormCheckOut : Form
    {
        public FormCheckOut()
        {
            InitializeComponent();
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormCheckOut_Load(object sender, EventArgs e)
        {

        }

        public void AddProduct(int i,string id, string name, string price, int quantity, double subtotal)
        {
            dgvCheckOut.Rows.Add(i,id, name, price, quantity, subtotal);
        }
        public string insertOrder()
        {
            
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                string time = DateTime.Now.ToString("HH:mm:ss");
                string orderSql = "insert into tblOrder (ordDate,ordTime) values ('" + date + "','" + time + "');";

                SqlCommand s = new SqlCommand(orderSql, DataConnection.DataCon);
                s.ExecuteNonQuery();
                orderSql = "SELECT SCOPE_IDENTITY()";
                s = new SqlCommand(orderSql, DataConnection.DataCon);
                SqlDataReader r = s.ExecuteReader();
                string ordID = "";
                if (r.Read())
                {
                    ordID = r[0].ToString();
                }
                r.Close();
                s.Dispose();
                return ordID;
        }
        public void updateStock(string qty,string pid)
        {
            string sql = "update tblproduct set Qty-=" + qty + "where id = " + pid ;
            SqlCommand s = new SqlCommand (sql, DataConnection.DataCon);
            s.ExecuteNonQuery();
            s.Dispose();
        }
        public void insertOrderDetail(string ordId)
        {
            try
            {
                foreach (DataGridViewRow row in dgvCheckOut.Rows)
                {
                    if (row.Cells[1].Value != null && row.Cells[2].Value != null && row.Cells[3].Value != null && row.Cells[4].Value != null)
                    {
                        string productId = row.Cells[1].Value.ToString();
                        string productName = row.Cells[2].Value.ToString();
                        string productPrice = row.Cells[3].Value.ToString();
                        string quantity = row.Cells[4].Value.ToString();
                        updateStock(quantity, productId);
                        string orderDetailSql = "insert into tblOrderDetail values ('" + ordId + "', '" + productId + "', " + quantity + ", " + productPrice + ");";

                        SqlCommand s = new SqlCommand(orderDetailSql, DataConnection.DataCon);
                        s.ExecuteNonQuery();
                        s.Dispose();
                    }
                }

                

                MessageBox.Show("Order placed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnOrderNow_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (dgvCheckOut.Rows.Count == 0)
                {
                    MessageBox.Show("Please add products to the checkout before placing an order.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string ordID = insertOrder();
                insertOrderDetail(ordID);
                FrmShopping p = new FrmShopping();

                FrmShopping shoppingForm = Application.OpenForms.OfType<FrmShopping>().FirstOrDefault();
                if (shoppingForm != null)
                {
                    // Reset state of each ProductControl
                    foreach (ProductControl productControl in shoppingForm.tableLayoutPanel.Controls.OfType<ProductControl>())
                    {
                        productControl.ResetState();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
