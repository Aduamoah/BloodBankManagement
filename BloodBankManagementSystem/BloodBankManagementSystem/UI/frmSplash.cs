using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBankManagementSystem.UI
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }
        int move = 0;

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timerSplash_Tick(object sender, EventArgs e)
        {
            //write the code to show loading animation
            timerSplash.Interval = 20;

            panelMovable.Width += 5;

            move += 5;

            //if the loading is complete then display login form and close this form
            if(move == 700)
            {
                //stop the timer and close this form
                timerSplash.Stop();
                this.Hide();

                //display login form
                frmLogin login = new frmLogin();
                login.Show();
            }
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            //load the timer
            timerSplash.Start();
        }
    }
}
