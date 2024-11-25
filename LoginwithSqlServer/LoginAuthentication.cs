using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LoginwithSqlServer
{
    public partial class LoginAuthentication : Form
    {
        public LoginAuthentication()
        {
            
            InitializeComponent();
            this.MaximizeBox = false;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string ip = txtip.Text;
                string dbname = "su44";
                string username = txtuser.Text.Trim();
                string password = txtpassword.Text;
                int index = Comboauth.SelectedIndex;
                if(index == 0)
                {
                    // window authentication
                    DataConnection.ConnectionDB(ip, dbname);
                    
                }
                else
                {
                    // sql server authentication
                    

                    DataConnection.ConnectionDB(ip, dbname, username, password);
                    
                    
                }

                new FrmMenuOptions().Show();
                new LoginAuthentication().Dispose();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Comboauth.SelectedIndex = 0;
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

       

        private void Comboauth_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            int index = Comboauth.SelectedIndex;
            if (index == 0)
            {
                txtuser.Enabled = false;
                txtpassword.Enabled = false;


            }
            else
            {


                txtuser.Enabled = true;
                txtpassword.Enabled = true;
            }
        }
    }
}
