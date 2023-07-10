using BloodBankManagementSystem.BusinessLogicLayer;
using BloodBankManagementSystem.DataAccessLayer;
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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        //create the object of BLL and DAL
        LoginBLL l = new LoginBLL();
        LoginDAL dal = new LoginDAL();

        //create a static string method to save username
       public static string LoggedInUser;
        private void lblUsername_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //write the code to close application
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //write the code to login our Application
            //step 1: Get the Username and Password from  Login Form
            l.Username = txtUsername.Text;
            l.Password = txtPassword.Text;
           

            //check the login credentials
            bool success = dal.LoginCheck(l);

            //step 3: check whether the login is successful or not
            if (success == true)
            {
                //login successful
                MessageBox.Show("Login successful");

                //save the username in loggedinuser static method
                LoggedInUser = l.Username;
                //display home form
                FrmHome home = new FrmHome();
                home.Show();
                this.Hide();//to close login form

                
            }
            else
            {
                //log in failed
                MessageBox.Show("Login unsuccessful.Try Again");
            }
        }
    }
    
}
