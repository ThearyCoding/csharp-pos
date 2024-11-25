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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ActiveControl = txtusername;
            txtusername.Focus();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                string dbname = "MyFormLogin";
                string servsername = "DESKTOP-8G86QJP";
                DataConnection.ConnectionDB(servsername, dbname);
                string sqlc = "select * from tbllogin";
                SqlCommand sql = new SqlCommand(sqlc, DataConnection.DataCon);
                SqlDataReader reader = sql.ExecuteReader();
                string user_name = txtusername.Text;
                string password = txtpassword.Text;
                bool loginSuccess = false;

                while (reader.Read())
                {
                    if (user_name.Equals(reader[1].ToString()) && password.Equals(reader[2].ToString()))
                    {
                        loginSuccess = true;
                        break;
                    }
                }

                if (loginSuccess)
                {
                    MessageBox.Show("Login Successfully", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    new LoginAuthentication().ShowDialog();
                }
                else
                {
                    MessageBox.Show("Login Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtusername.Focus();
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
            }
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Show();
            new FrmRegistration().ShowDialog();
        }

        private void txtusername_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Shift)
            {
                txtpassword.Focus();
            }
        }

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnlogin.PerformClick();
            }
        }
    }
}
