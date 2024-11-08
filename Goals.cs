using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnMe_App
{
    public partial class Goals : Form
    {

      

        public Goals()
        {
            InitializeComponent();

        }


        private void btn1_Click(object sender, EventArgs e)
        {
            Goal1 goal1Form = new Goal1();

            this.Hide();

            goal1Form.Show();
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            Home homeForm = new Home();

            this.Hide();

            homeForm.Show();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

        }

        private void btn2_Click(object sender, EventArgs e)
        {
            Goal2 goal2Form = new Goal2();

            this.Hide();

            goal2Form.Show();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            Goal3 goal3Form = new Goal3();

            this.Hide();

            goal3Form.Show();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            Goal4 goal4Form = new Goal4();

            this.Hide();

            goal4Form.Show();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            Goal5 goal5Form = new Goal5();

            this.Hide();

            goal5Form.Show();
        }

        private void btnGoBack_Click_1(object sender, EventArgs e)
        {
            Home homeForm = new Home();

            this.Hide();

            homeForm.Show();
        }
    }
}
