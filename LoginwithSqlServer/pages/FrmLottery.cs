using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace LoginwithSqlServer
{
    public partial class FrmLottery : Form
    {
        public FrmLottery()
        {
            InitializeComponent();
        }
        private List<People> people;
        private void Lottery_Load(object sender, EventArgs e)
        {
            people = new List<People>();

            try
            {
                string sqlcommand = "select * from tblpeople";
                SqlCommand sqlCommand = new SqlCommand(sqlcommand,DataConnection.DataCon);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    string id = reader[0] + "";
                    string name = reader[1] + "";
                    string gender = reader[2] + "";
                    string phone = reader[3] + "";
                    string email = reader[4] + "";
                    People p = new People(id, name, gender, phone, email);
                    people.Add(p);

                }
                reader.Close();
                sqlCommand.Dispose();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string start = btnstart.Text;

                if (start == "Start")
                {
                    timer1.Start();
                    btnstart.Text = "Stop";
                }
                else
                {
                    timer1.Stop();
                    btnstart.Text = "Start";

                    if (winner.Count < 3) 
                    {
                        winner.Add(people[index]);
                        listBox1.Items.Add(people[index].phone);
                        people.RemoveAt(index);
                    }
                    else
                    {
                        MessageBox.Show("You can only win three times.", "Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private int index;
        private List<People> winner = new List<People>();
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (people == null || people.Count == 0)
            {
                return;
            }
            Random rand = new Random();
            index = rand.Next(people.Count);
            string phone = people[index].phone;
            txtgenerate.Text = phone;
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
                string id = winner[i].id;
                string name = winner[i].name;
                string gender = winner[i].gender;
                string phone = winner[i].phone;
                string email = winner[i].email;

                dataview.Rows.Add(id, name, gender, phone, email);
                
            
        }

    }
}
