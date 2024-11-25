using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace LoginwithSqlServer
{
    public partial class FrmShopping : Form
    {
        private Dictionary<string, Order> orders = new Dictionary<string, Order>();

        public class Order
        {
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductPrice { get; set; }
            public int Quantity { get; set; }
            public double Subtotal { get; set; }
        }

        public FrmShopping()
        {
            InitializeComponent();
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
        }

        public void loadProduct()
        {
            string sql = "SELECT * FROM tblproduct";
            using (SqlCommand sqlCommand = new SqlCommand(sql, DataConnection.DataCon))
            {
                try
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    int x = 0, y = 1;
                    while (reader.Read())
                    {
                        string id = reader[0].ToString();
                        string name = reader[1].ToString();
                        double price = Convert.ToDouble(reader[2]);
                        string formattedPrice = price.ToString("c2");
                        string qty = reader[3].ToString();

                        if (int.Parse(qty) <= 0)
                        {
                            qty = "Out of Stock";
                        }
                        double subtotal = Convert.ToDouble(reader[4]);
                        string fileName = reader[5].ToString();

                        ProductControl product = new ProductControl(id, fileName, name, formattedPrice, qty, this);

                        if (qty == "Out of Stock")
                        {
                            product.Enabled = false;
                        }

                        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                        tableLayoutPanel.Controls.Add(product, y, x);
                        y++;
                        if (y >= 5)
                        {
                            y = 1;
                            x++;
                        }

                        // Check if there are existing orders for this product
                        if (orders.ContainsKey(id))
                        {
                            product.Quantity = orders[id].Quantity;
                            product.btnorder.Text = $"Order({orders[id].Quantity})";
                        }
                        // Ensure btnclear is always visible if there are existing orders
                        product.btnclear.Visible = orders.ContainsKey(id) && orders[id].Quantity > 0;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while loading products: " + ex.Message);
                }
            }
        }




        public void Shopping_Load(object sender, EventArgs e)
        {
            loadProduct();
            loadCategory();
        }
       
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            FormCheckOut checkoutForm = new FormCheckOut();
            int i = 1;
            foreach (ProductControl productControl in tableLayoutPanel.Controls.OfType<ProductControl>())
            {
                
                if (productControl.Quantity > 0)
                {
                    string productId = productControl.productId;
                    string productName = productControl.ProductName;
                    string productPrice = productControl.ProductPrice;
                    int quantity = productControl.Quantity;
                    double price = double.Parse(productPrice.Replace("$", "").Trim());
                    double subtotal = price * quantity;
                    checkoutForm.AddProduct(i, productId, productName, productPrice, quantity, subtotal);
                    i++;
                }
              
            }
            checkoutForm.ShowDialog();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
            new FrmMenuOptions().Show();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtsearch.Text.Trim();
            tableLayoutPanel.Controls.Clear();

            if (string.IsNullOrEmpty(searchTerm))
            {
                loadProduct();
                return;
            }
            tableLayoutPanel.AutoScroll = false;
            string sql = "SELECT * FROM tblproduct WHERE name LIKE @searchTerm";
            using (SqlCommand sqlCommand = new SqlCommand(sql, DataConnection.DataCon))
            {
                sqlCommand.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                try
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    int x = 0, y = 1;
                    while (reader.Read())
                    {
                        string id = reader[0].ToString();
                        string name = reader[1].ToString();
                        double price = Convert.ToDouble(reader[2]);
                        string formattedPrice = price.ToString("c2");
                        string qty = reader[3].ToString();

                        if (int.Parse(qty) <= 0)
                        {
                            qty = "Out of Stock";
                        }
                        double subtotal = Convert.ToDouble(reader[4]);
                        string fileName = reader[5].ToString();

                        ProductControl product = new ProductControl(id, fileName, name, formattedPrice, qty, this);

                        if (qty == "Out of Stock")
                        {
                            product.Enabled = false;
                        }

                        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                        tableLayoutPanel.Controls.Add(product, y, x);
                        y++;

                        if (y >= 5)
                        {
                            y = 1;
                            x++;
                            tableLayoutPanel.AutoScroll = true;
                        }

                        if (orders.ContainsKey(id))
                        {
                            Order order = orders[id];
                            product.Quantity = order.Quantity;
                            product.btnorder.Text = $"Order({order.Quantity})";
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while loading products: " + ex.Message);
                }
            }
        }

        private void cbofilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sql;
                if (cbofilter.SelectedIndex == 0)  // Assuming index 0 represents "All Categories"
                {
                    sql = "SELECT * FROM tblproduct";
                }
                else
                {
                    int filter = cbofilter.SelectedIndex;  // Adjust index as needed based on your ComboBox setup
                    sql = "SELECT * FROM tblproduct WHERE cid = @filter";
                }

                tableLayoutPanel.Controls.Clear();
                tableLayoutPanel.AutoScroll = false;

                using (SqlCommand sqlCommand = new SqlCommand(sql, DataConnection.DataCon))
                {
                    if (cbofilter.SelectedIndex != 0)  // Add parameter only if not "All Categories"
                    {
                        sqlCommand.Parameters.AddWithValue("@filter", cbofilter.SelectedIndex);
                    }

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    int x = 0, y = 1;
                    while (reader.Read())
                    {
                        string id = reader[0].ToString();
                        string name = reader[1].ToString();
                        double price = Convert.ToDouble(reader[2]);
                        string formattedPrice = price.ToString("c2");
                        string qty = reader[3].ToString();

                        if (int.Parse(qty) <= 0)
                        {
                            qty = "Out of Stock";
                        }
                        double subtotal = Convert.ToDouble(reader[4]);
                        string fileName = reader[5].ToString();

                        ProductControl product = new ProductControl(id, fileName, name, formattedPrice, qty, this);

                        if (qty == "Out of Stock")
                        {
                            product.Enabled = false;
                        }

                        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                        tableLayoutPanel.Controls.Add(product, y, x);
                        y++;

                        if (y >= 5)
                        {
                            y = 1;
                            x++;
                            tableLayoutPanel.AutoScroll = true;
                        }

                        if (orders.ContainsKey(id))
                        {
                            Order order = orders[id];
                            product.Quantity = order.Quantity;
                            product.btnorder.Text = $"Order({order.Quantity})";
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading products: " + ex.Message);
            }
        }


        public void loadCategory()
        {
            cbofilter.Items.Clear(); // Clear existing items first

            try
            {
                // Query to select all categories
                string sql = "SELECT * FROM tblcategory";

                using (SqlCommand sqlCommand = new SqlCommand(sql, DataConnection.DataCon))
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        string id = reader[0].ToString();
                        string name = reader[1].ToString();

                        // Add category name to ComboBox items
                        cbofilter.Items.Add(name);
                    }
                    reader.Close();
                }

                // Add "All Categories" option at the beginning
                cbofilter.Items.Insert(0, "All Categories");
                cbofilter.SelectedIndex = 0; // Select "All Categories" by default

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading categories: " + ex.Message);
            }
        }


        public void UpdateOrder(string productId, string productName, string productPrice, int quantity, double subtotal)
        {
            if (orders.ContainsKey(productId))
            {
                orders[productId].Quantity = quantity;
                orders[productId].Subtotal = subtotal;
            }
            else
            {
                orders.Add(productId, new Order
                {
                    ProductId = productId,
                    ProductName = productName,
                    ProductPrice = productPrice,
                    Quantity = quantity,
                    Subtotal = subtotal
                });
            }
        }
    }
}
