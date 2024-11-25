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
    public partial class FormCreateNew : Form
    {
        FormProduct product;
        public FormCreateNew(FormProduct p)
        {
            InitializeComponent();
            product = p;
            MaximizeBox = false;
            foreach(var temp in cbocategory.Items)
            {
                categoryName.Add(temp.ToString());
                
            }
            for (int i = 0; i < cbocategory.Items.Count; i++)
            {
                categoryId.Add((i + 1).ToString());
            }
            txtcategoryido.TextChanged += txtcategoryido_TextChanged;
            StartPosition = FormStartPosition.CenterScreen;
            

        }
        List<string> categoryId = new List<string>();
        List<string> categoryName = new List<string>();
        public void loadCategory()
        {
            try
            {
                string sql = "select * from tblcategory;";

                SqlCommand s = new SqlCommand(sql, DataConnection.DataCon);

                SqlDataReader r = s.ExecuteReader();

                while (r.Read())
                {
                    string id = r[0].ToString();
                    string name = r[1].ToString();
                    cbocategory.Items.Add(name);
                    categoryId.Add(id);
                    categoryName.Add(name);
                }
                cbocategory.SelectedIndex = 0;
                r.Close();
                s.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void FormCreateNew_Load(object sender, EventArgs e)
        {
            loadimagePath();

        }
        private string originalPhoto;
        private void picturebox_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult result = ofd.ShowDialog();

            if (result == DialogResult.OK)
            {
                originalPhoto = ofd.FileName;
                Image img = Image.FromFile(ofd.FileName);
                pictureBox.Image = img;
            }
        }

        private void btninsert_Click(object sender, EventArgs e)
        {
            try
            {
                
                string name = txtname.Text.Trim();
                string cid = txtcategoryido.Text;
                double price = double.Parse(txtprice.Text.Trim());
                int qty = int.Parse(txtqty.Text.Trim());
                double subtotal = price * qty;

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(cid) || string.IsNullOrEmpty(price.ToString()) || string.IsNullOrEmpty(qty + "") || pictureBox.Image == null)
                {
                    MessageBox.Show("Please enter values in all fields.");
                }

                else
                {
                    string sql = "insert into tblproduct (name, price, qty,subtotal,image,cid) values ('" + name + "', '" + price + "', '" + qty + "','" + subtotal + "','" + originalPhoto + "','"+cid+"')";

                    SqlCommand s = new SqlCommand(sql, DataConnection.DataCon);
                    s.ExecuteNonQuery();
                    s.Dispose();
                    MessageBox.Show("Data inserted successfully!");
                    product.RefreshDataGridView();
                    product.cbofilter.SelectedIndex = -1;
                    txtname.Clear();
                    txtqty.Clear();
                    cbocategory.SelectedIndex = 0;
                    pictureBox.Image = Properties.Resources.noimage;
                    product.RefreshDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void cbocategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = cbocategory.SelectedIndex;
                txtcategoryido.Text = categoryId[index];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtcategoryido_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string categoryIdValue = txtcategoryido.Text;
                int index = categoryId.IndexOf(categoryIdValue);
                if (index != -1 && index < cbocategory.Items.Count)
                {
                    cbocategory.SelectedIndex = index;
                }
                else
                {

                    MessageBox.Show("Category ID not found.");
                    cbocategory.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void loadimagePath()
        {
            try
            {
                string sql = "SELECT image FROM tblproduct WHERE id = '"+txtpid.Text+"'";

                SqlCommand s = new SqlCommand(sql, DataConnection.DataCon);

                SqlDataReader r = s.ExecuteReader();

                while (r.Read())
                {
                    originalPhoto = r[0].ToString();
                    
                }
                r.Close();
                s.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                string pid = txtpid.Text.Trim();
                string name = txtname.Text.Trim();
                string cid = txtcategoryido.Text;
                double price = double.Parse(txtprice.Text.Trim());
                int qty = int.Parse(txtqty.Text.Trim());
                double subtotal = price * qty;

                string photo = originalPhoto;


                string sql = "update tblproduct set name='" + name + "', price='" + price + "', qty='" + qty + "', subtotal='" + subtotal + "', image='" + photo + "', cid = '"+cid+"' where id='" + pid + "'";
                SqlCommand s = new SqlCommand(sql, DataConnection.DataCon);
               
                    s.ExecuteNonQuery();
              
                MessageBox.Show("Data updated successfully!");
                product.RefreshDataGridView();
                product.cbofilter.SelectedIndex = -1;
                this.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult result = ofd.ShowDialog();
            ofd.Filter = "Image Files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";
            if (result == DialogResult.OK)
            {
                originalPhoto = ofd.FileName;
                Image img = Image.FromFile(ofd.FileName);
                pictureBox.Image = img;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
