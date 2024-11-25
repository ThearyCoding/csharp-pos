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
    public partial class FrmRegistration : Form
    {
        public FrmRegistration()
        {
            InitializeComponent();
            this.ActiveControl = txtusername;
            txtusername.Focus();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                string registerDate = currentDate.ToString("yyyy-MM-dd");
                string registerTime = currentDate.ToString("HH:mm:ss");
                string username = txtusername.Text.Trim();
                string password = txtpassword.Text.Trim();
                string confirm = txtconfirm.Text.Trim();
                if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirm))
                {
                    MessageBox.Show("all in field can't emtpy");
                }
                else
                {
                    
                    if (confirm == password)
                    {
                        string sqlstatement = "INSERT INTO tbllogin (username, password, registerDate, registerTime) VALUES ('"+username+"','"+password+ "','"+registerDate+ "','"+registerTime+"')";
                        string dbname = "MyFormLogin";
                        string serverName = "DESKTOP-8G86QJP";

                        DataConnection.ConnectionDB(serverName, dbname);
                        SqlCommand sqlcommand = new SqlCommand(sqlstatement,DataConnection.DataCon);
                        sqlcommand.ExecuteNonQuery();
                        sqlcommand.Dispose();
                        txtusername.Clear();
                        txtpassword.Clear();
                        txtconfirm.Clear();
                        MessageBox.Show("Register Successful");
                        this.Dispose();
                        new FrmLogin().Show();
                    }
                    else
                    {
                        MessageBox.Show("Password and Confirm Password do not match.", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            
        }

        private void txtusername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpassword.Focus();
            }
        }

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtconfirm.Focus();
            }
        }

        private void txtconfirm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnregister.PerformClick();
            }
        }
        public string GenerateStrongPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-=_+";
            Random random = new Random();

            int passwordLength = 12;
            char[] password = new char[passwordLength];

            for (int i = 0; i < passwordLength; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }

            return new string(password);
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            string strongpw = GenerateStrongPassword();

            txtpassword.Text = strongpw;
            txtconfirm.Text = strongpw;
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            this.Dispose();
            new FrmLogin().Show();
        }

    }
}
