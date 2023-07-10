using BloodBankManagementSystem.DataAccessLayer;
using BloodBankManagementSystem.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBankManagementSystem
{
    public partial class FrmHome : Form
    {
        public FrmHome()
        {
            InitializeComponent();
        }
        //create object of donor dal
        donorDAL dal = new donorDAL();
        private void Form1_Load(object sender, EventArgs e)
        {
            //load all the blood donors count when form is loaded
            //call alldonorcountmethods
            allDonorCount();

            //display all the donors
            DataTable dt = dal.select();
            dgvDonors.DataSource = dt;

            //display username of the logged in user
            lblUser.Text = frmLogin.LoggedInUser;
        }
        public void allDonorCount()
        {
            //get the donor count from database and set in respective labels
            lblOpositiveCount.Text = dal.countDonors("O+");
            lblOnegativeCount.Text = dal.countDonors("O-");
            lblApositiveCount.Text = dal.countDonors("A+");
            lblAnegativeCount.Text = dal.countDonors("A-");
            lblBpositiveCount.Text = dal.countDonors("B+");
            lblBnegativeCount.Text = dal.countDonors("B-");
            lblABpositiveCount.Text = dal.countDonors("AB+");
            lblABnegativeCount.Text = dal.countDonors("AB-");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //code to close the window
            this.Hide();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open users Form
            frmUsers users = new frmUsers();
            users.Show();
        }

        private void lblABpositiveCount_Click(object sender, EventArgs e)
        {

        }

        private void donorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // open manage donors form
            FrmDonors donors = new FrmDonors();
            donors.Show();
        }

        private void FrmHome_Activated(object sender, EventArgs e)
        {
            //call alldonorcountMethods
            allDonorCount();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //1. Get the keyboards typed on the search text box
            string keywords = txtSearch.Text;
            //check whether textbox is empty or not
            if (keywords != null)
            {
                //textbox  is not empty, display donors on data grid view based on keywords
                DataTable dt = dal.search(keywords);
                dgvDonors.DataSource = dt;
            }
            else
            {
                //textbox is empty and display donors on gridview
                DataTable dt = dal.select();
                dgvDonors.DataSource = dt;
            }
        }
    }
    
}
