using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace LoginwithSqlServer
{
    public partial class FormProduct : Form
    {
        public FormProduct()
        {
            InitializeComponent();
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
        }
       
        public void LoadData()
        {
            dataview.Rows.Clear();
            int i = 1;
            string sql = "select * from tblproduct";
            using (SqlCommand sqlCommand = new SqlCommand(sql, DataConnection.DataCon))
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    string id = reader[0].ToString();
                    string name = reader[1].ToString();
                    string price = reader[2].ToString();
                    string qty = reader[3].ToString();
                    double subtotal = double.Parse(reader[4].ToString());
                    string im = reader[5].ToString();
                    string cid = reader[6].ToString();
                    total += subtotal;
                    totalQty += int.Parse(qty);
                    listid.Add(int.Parse(id));
                    dataview.Rows.Add(i,id, name, price, qty, subtotal, im,cid);
                    i++;
                }
                reader.Close();
                txttotalqty.Text = totalQty.ToString();
                txttotal.Text = total.ToString("$0.00");
            }
        }
        public void RefreshDataGridView()
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        List<int> listid = new List<int>();
        

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (dataview.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to remove.");
                return;
            }


            DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                try
                {
                    DataGridViewRow row = dataview.SelectedRows[0];
                    int id = int.Parse(row.Cells[0].Value.ToString());
                    double subtotal = double.Parse(row.Cells[5].Value.ToString());
                    string sqldelete = "delete from tblproduct where id = '" + id + "'";
                    using (SqlCommand sqlCommand = new SqlCommand(sqldelete, DataConnection.DataCon))
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                    total -= subtotal;
                    txttotal.Text = total.ToString("$0.00");
                    
                    RefreshDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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
        private void FormProduct_Load(object sender, EventArgs e)
        {
            try
            {
                loadCategory();
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        double total = 0;
        int totalQty = 0;
      
        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchText = txtsearch.Text.Trim();
                string sql = "select * from tblproduct where name LIKE @searchText OR id LIKE @searchText OR cid LIKE @searchText";
                int i = 1;

                using (SqlCommand sqlCommand = new SqlCommand(sql, DataConnection.DataCon))
                {
                    sqlCommand.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    dataview.Rows.Clear(); // Clear existing rows
                    total = 0; // Reset total before recalculating
                     totalQty = 0;
                    while (reader.Read())
                    {
                        string id = reader[0].ToString();
                        string name = reader[1].ToString();
                        
                        string price = reader[2].ToString();
                        string qty = reader[3].ToString();
                        double subtotal = double.Parse(reader[4].ToString());
                        string im = reader[5].ToString();
                        string cid = reader[6].ToString();
                        totalQty += int.Parse(qty);
                        total += subtotal;
                        dataview.Rows.Add(i,id, name, price, qty, subtotal, im,cid);
                        i++;
                    }
                    reader.Close();
                    txttotalqty.Text = totalQty.ToString();
                    // Update total outside the loop
                    txttotal.Text = total.ToString("$0.00");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        

        private void btncreatenew_Click(object sender, EventArgs e)
        {
            FormCreateNew createnew = new FormCreateNew(this);
            createnew.Text = "Form Add New Product";
            createnew.btnupdate.Enabled = false;
            createnew.txtpid.Enabled = false;
            createnew.ShowDialog();
        }
        string _pid, _name, _price, _qty, _urlphoto, _cname, cid;

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Dispose();
            new FrmMenuOptions().Show();
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
                int i = 1;
                using (SqlCommand sqlCommand = new SqlCommand(sql, DataConnection.DataCon))
                {
                    if (cbofilter.SelectedIndex != 0)  // Add parameter only if not "All Categories"
                    {
                        sqlCommand.Parameters.AddWithValue("@filter", cbofilter.SelectedIndex);
                    }

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    dataview.Rows.Clear(); 
                    total = 0;
                    totalQty = 0;
                    while (reader.Read())
                    {
                        string id = reader[0].ToString();
                        string name = reader[1].ToString();

                        string price = reader[2].ToString();
                        string qty = reader[3].ToString();
                        double subtotal = double.Parse(reader[4].ToString());
                        string im = reader[5].ToString();
                        string cid = reader[6].ToString();
                        totalQty += int.Parse(qty);
                        total += subtotal;
                        dataview.Rows.Add(i,id, name, price, qty, subtotal, im, cid);
                        i++;
                    }
                    reader.Close();
                    txttotalqty.Text = totalQty.ToString();
                    txttotal.Text = total.ToString("$0.00");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = dataview.Columns[e.ColumnIndex].Name;
         
            if(colname == "coedit")
            {
                FormCreateNew formCreateNew = new FormCreateNew(this);
                formCreateNew.Text = "Form Edit Product";
                formCreateNew.btninsert.Enabled = false;
                formCreateNew.btnupdate.Enabled = true;
                formCreateNew.txtpid.Enabled = false;
                formCreateNew.txtpid.Text = _pid;
                formCreateNew.txtname.Text = _name;
                formCreateNew.txtprice.Text = _price;
                formCreateNew.txtqty.Text = _qty;
                
                if (!string.IsNullOrEmpty(_urlphoto))
                {
                    try
                    {
                        formCreateNew.pictureBox.Image = Image.FromFile(_urlphoto);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading image: " + ex.Message);
                    }
                }
                formCreateNew.cbocategory.Text = _cname;
                formCreateNew.txtcategoryido.Text = cid;
               
                formCreateNew.ShowDialog();
                
            }else if(colname == "coremove")
            {
                
                DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                    try
                    {
                        DataGridViewRow row = dataview.CurrentRow;
                        string id = row.Cells[0].Value.ToString();
                        string sqldelete = "delete from tblproduct where id = '"+id+"'";
                        SqlCommand sqlCommand = new SqlCommand(sqldelete, DataConnection.DataCon);
                        sqlCommand.ExecuteNonQuery();
                        RefreshDataGridView();
                        MessageBox.Show($"this product {id} have been remove from database","Successfully",MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void dataview_SelectionChanged(object sender, EventArgs e)
        {
            int i = dataview.CurrentRow.Index;

            _pid = dataview[0, i].Value.ToString();
            _name = dataview[1,i].Value.ToString();
            _price = dataview[2,i].Value.ToString();
            _qty = dataview[3,i].Value.ToString();
            
            _urlphoto = dataview[5,i].Value.ToString();
            cid = dataview[6, i].Value.ToString();
        }
    }
}
