using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginwithSqlServer
{
    public partial class FrmMenuOptions : Form
    {
        public FrmMenuOptions()
        {
            InitializeComponent();
            this.MaximizeBox = false;
        }

        private void btnview_Click(object sender, EventArgs e)
        {
            new ViewPeople().Show();
            this.Dispose();
            new FrmMenuOptions().Dispose();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            new FrmLottery().Show();
        }

        private void btnshopping_Click(object sender, EventArgs e)
        {
            this.Dispose();
            new FrmShopping().Show();
        }

        private void btnproduct_Click(object sender, EventArgs e)
        {
            new FormProduct().Show();
            this.Dispose ();
        }

       

        private void btnjointable_Click(object sender, EventArgs e)
        {
            new FormOrderDetail().Show();
        }
    }
}
