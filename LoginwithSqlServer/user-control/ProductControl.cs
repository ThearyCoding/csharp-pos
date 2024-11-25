using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LoginwithSqlServer
{
    public partial class ProductControl : UserControl
    {
        private FrmShopping parentForm;
        private int _quantity;

        public ProductControl(string productid, string image, string ProductName, string productprice, string qty, FrmShopping parentForm)
        {
            InitializeComponent();
            this.productId = productid;
            this.Pic = image;
            this.ProductName = ProductName;
            this.ProductPrice = productprice;
            this.productQty = qty;
            this.parentForm = parentForm;
            this.btnclear.Visible = false;
            this.btnqty.Enabled = false;
        }

        public string productQty
        {
            get => btnqty.Text;
            set => btnqty.Text = value;
        }

        public string productId
        {
            get => txtid.Text;
            set => txtid.Text = value;
        }

        public string Pic
        {
            get => picbox.ImageLocation;
            set => picbox.ImageLocation = value;
        }

        public string ProductName
        {
            get => txtproductname.Text;
            set => txtproductname.Text = value;
        }

        public string ProductPrice
        {
            get => txtprice.Text;
            set => txtprice.Text = value;
        }

        public void ResetState()
        {
            _quantity = 0;
            btnclear.Visible = false;
            btnorder.Text = "Order";
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                btnorder.Text = $"Order({_quantity})";
                btnclear.Visible = _quantity > 0;
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            if (productQty == "Out Of Stock")
            {
                productQty = "0";
            }

            if (_quantity <= 1)
            {
                _quantity = 0;
                btnorder.Text = "Order";
                btnclear.Visible = false;
            }
            else
            {
                _quantity--;
                int qty = int.Parse(productQty);
                qty++;
                productQty = qty.ToString();
                btnorder.Text = $"Order({_quantity})";

                // Update btnclear visibility based on Quantity
                btnclear.Visible = _quantity > 0;
            }
        }
        public int getProductQtyaviable(string pid)
        {
            int availableQuantity = 0;
            string query = $"SELECT Qty FROM tblproduct WHERE id = {pid};";
            SqlCommand cmd = new SqlCommand(query, DataConnection.DataCon);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                availableQuantity = int.Parse(r[0].ToString());
            }
            r.Close();
            cmd.Dispose();
            return availableQuantity;
        }

        private void btnorder_Click(object sender, EventArgs e)
        {
            try
            {
                int availableQuantity = getProductQtyaviable(productId);
                int orderQuantity = Quantity + 1;

                if (orderQuantity > availableQuantity)
                {
                    productQty = "Out Of Stock";
                    MessageBox.Show("Not enough quantity available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                availableQuantity -= orderQuantity;
                productQty = availableQuantity.ToString();

                btnclear.Visible = true;

                double price = double.Parse(ProductPrice.Replace("$", "").Trim());
                double subtotal = price * orderQuantity;

                Quantity = orderQuantity;
                btnorder.Text = $"Order({Quantity})";

                parentForm.UpdateOrder(productId, ProductName, ProductPrice, Quantity, subtotal);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
