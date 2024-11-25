using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LoginwithSqlServer
{
    public partial class ViewPeople : Form
    {
        private string photo;
        private int selectedIndex;

        public ViewPeople()
        {
            InitializeComponent();
            this.MaximizeBox = false;
        }
        // Generate random product id by name
        private void generateId(string name)
        {
            Random rand = new Random();
            string randomIdproduct = name +"##"+ + rand.Next(100, 300);
            txtid.Text = randomIdproduct;
        }
        // Generate random phone number
        private void generateNumber()
        {
            Random rand = new Random();
            string phoneNumber = "0" + rand.Next(100000000, 1000000000).ToString();
            txtprice.Text = phoneNumber;
        }

        // Generate email based on name
        public void generateEmail()
        {
            Random random = new Random();
            string nameWithoutSpaces = txtname.Text.Trim().Replace(" ", ""); // Remove spaces from the name
            string email = $"{nameWithoutSpaces.ToLower()}{random.Next(100, 200)}@gmail.com";
            txtqty.Text = email;
        }

        // Load data into DataGridView
        private void ViewPeople_Load(object sender, EventArgs e)
        {
            try
            {
                combocategory.SelectedIndex = 0;
                generateNumber();
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Refresh DataGridView with updated data
        private void RefreshDataGridView()
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

        // Load data from database
        private void LoadData()
        {
            dataview.Rows.Clear();
            string sql = "select * from tblproduct";
            using (SqlCommand sqlCommand = new SqlCommand(sql, DataConnection.DataCon))
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    string id = reader[0].ToString();
                    string name = reader[1].ToString();
                    string category = reader[2].ToString();
                    string price = reader[3].ToString();
                    string qty = reader[4].ToString();
                    double subtotal = double.Parse(reader[5].ToString());
                    string im = reader[6].ToString();
                    total += subtotal;
                    dataview.Rows.Add(id, name, category, price, qty, subtotal, im);
                }
                reader.Close();
                labelTotal.Text = total.ToString("$0.00");
            }
        }

        // Browse for an image
        private void btnbrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult result = ofd.ShowDialog();

            if (result == DialogResult.OK)
            {
                photo = ofd.FileName;
                Image img = Image.FromFile(ofd.FileName);
                picturebox.Image = img;
            }
        }

        // Handle cell click event to populate fields
        private void dataview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedIndex = e.RowIndex;
            DataGridViewRow row = dataview.Rows[selectedIndex];
            txtname.Text = row.Cells[1].Value.ToString();
            combocategory.Text = row.Cells[2].Value.ToString();
            txtprice.Text = row.Cells[3].Value.ToString();
            txtqty.Text = row.Cells[4].Value.ToString();
            string pathfile = row.Cells[5].Value.ToString();
            picturebox.Image = Image.FromFile(pathfile);
        }

        // Update a record
        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtname.Text.Trim();
                string gender = combocategory.Text.Trim();
                string phone = txtprice.Text.Trim();
                string email = txtqty.Text.Trim();

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(photo))
                {
                    MessageBox.Show("Please enter values in all fields.");
                }
                else
                {
                    DataGridViewRow row = dataview.Rows[selectedIndex];
                    int id = int.Parse(row.Cells[0].Value.ToString());
                    string sql = "update tblproduct set name='" + name + "', gender='" + gender + "', phone='" + phone + "', email='" + email + "', image='" + photo + "' where id='" + id + "'";
                    using (SqlCommand s = new SqlCommand(sql, DataConnection.DataCon))
                    {
                        s.ExecuteNonQuery();
                    }
                    MessageBox.Show("Data updated successfully!");
                    RefreshDataGridView();
                    generateNumber();
                    generateEmail();
                    txtname.Clear();
                    txtqty.Clear();
                    picturebox.Image = Properties.Resources.noimage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Delete a record
        private void btndelete_Click(object sender, EventArgs e)
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
                labelTotal.Text = total.ToString("$0.00");

                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Generate random phone number
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            generateNumber();
        }

        // Generate random email
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            generateEmail();
        }

        // Search record by ID
        double total = 0;

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchText = txtsearch.Text.Trim();
                string sql = "select * from tblproduct where name LIKE @searchText OR id LIKE @searchText";

                using (SqlCommand sqlCommand = new SqlCommand(sql, DataConnection.DataCon))
                {
                    sqlCommand.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    dataview.Rows.Clear(); // Clear existing rows
                    total = 0; // Reset total before recalculating

                    while (reader.Read())
                    {
                        string id = reader[0].ToString();
                        string name = reader[1].ToString();
                        string category = reader[2].ToString();
                        string price = reader[3].ToString();
                        string qty = reader[4].ToString();
                        double subtotal = double.Parse(reader[5].ToString());
                        string im = reader[6].ToString();

                        total += subtotal;
                        dataview.Rows.Add(id, name, category, price, qty, subtotal, im);
                    }
                    reader.Close();

                    // Update total outside the loop
                    labelTotal.Text = total.ToString("$0.00");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void btninsert_Click(object sender, EventArgs e)
        {
          
                try
                {
                    string name = txtname.Text.Trim();
                    string category = combocategory.Text.Trim();
                    double price = double.Parse(txtprice.Text.Trim());
                    int qty = int.Parse(txtqty.Text.Trim());
                    double subtotal = price * qty;

                    if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(category) || string.IsNullOrEmpty(price.ToString()) || string.IsNullOrEmpty(qty+"") || picturebox.Image == null)
                    {
                        MessageBox.Show("Please enter values in all fields.");
                    }

                    else
                    {
                        string sql = "insert into tblproduct (name, category, price, qty,subtotal,image) values ('" + name + "', '" + category + "', '" + price + "', '" + qty + "','" + subtotal + "','"+photo+"')";

                        SqlCommand s = new SqlCommand(sql, DataConnection.DataCon);
                        s.ExecuteNonQuery();
                        s.Dispose();
                        MessageBox.Show("Data inserted successfully!");
                        RefreshDataGridView();
                        generateNumber();
                        generateEmail();
                        txtname.Clear();
                        txtqty.Clear();
                        picturebox.Image = Properties.Resources.noimage;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }

        private void btngernateId_Click(object sender, EventArgs e)
        {

        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {
            generateId(txtname.Text);
            if(txtname.Text == "")
            {
                txtid.Clear();
                
            }
        }

        private void dataview_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataview.CurrentRow;
            try
            {
                txtid.Text = row.Cells[0].Value.ToString();
                txtname.Text = row.Cells[1].Value.ToString();
                combocategory.Text = row.Cells[2].Value.ToString();
                txtprice.Text = row.Cells[3].Value.ToString();
                txtqty.Text = row.Cells[4].Value.ToString();
                string pathfile = row.Cells[6].Value.ToString();
                picturebox.Image = Image.FromFile(pathfile);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }

        private void dataview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

