
using BloodBankManagementSystem.BusinessLogicLayer;
using BloodBankManagementSystem.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBankManagementSystem.UI
{
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }UserBLL u = new UserBLL();
        userDAL dal = new userDAL();
        //create objects of userBLL and userDAL
        

        string imagename = "no-image.jpg";

        string sourcePath = "";
        string destinationPath = "";
        string imagePath = "";
        //global variable for the image to delete
        string rowHeaderImage;

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //Add functionality to close this form
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //step 1: Get the values from UI
            u.Fullname = txtFullName.Text;
            u.Email = txtEmail.Text;
            u.Username = txtUsername.Text;
            u.Password = txtPassword.Text;
            u.Contact = txtContact.Text;
            u.Address = txtAddress.Text;
            u.Added_date = DateTime.Now;
            u.Image_name = imagename;
            //upload image when it is selected
            //check whether the user has selected the image or not
            if(imagename != "n0-image.jpg")
            {
                //user has selected the image
                File.Copy(sourcePath, destinationPath);
            }
            
            //step 2 : Adding the values from UI to the database
            //create a boolean variable to check whether the data is inserted successfully or not
            bool success = dal.Insert(u);

            //step 3: check whether the data is inserted successfully or not
            if(success==true)
            {
                //data or user added successfully
                MessageBox.Show("New user added successfully");

                //display the user in datagridview
                DataTable dt = dal.select();
                dgvUsers.DataSource = dt;

                //clear textboxes
                clear();
            }
            else
            {
                //failed to add user
                MessageBox.Show("failed to add new user");
            }
        }

        //method or function to clear textboxes
        public void clear()
        {
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtUserId.Text = "";

            //get the image path
            string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);

            //path to destination folder
            string imagePath = paths + "\\images\\no-image.jpg";

            //display in picture box
            pictureBoxProfilePicture.Image = new Bitmap(imagePath);

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //step 1: Get the values from UI
            u.UserId = int.Parse(txtUserId.Text);
            u.Fullname = txtFullName.Text;
            u.Email = txtEmail.Text;
            u.Username = txtUsername.Text;
            u.Password = txtPassword.Text;
            u.Contact = txtContact.Text;
            u.Address = txtAddress.Text;
            u.Added_date = DateTime.Now;
            u.Image_name = imagename;

            //upload new image
            //check whether the user has selected the image or not
            if (imagename != "n0-image.jpg")
            {
                //user has selected the image
                File.Copy(sourcePath, destinationPath);
            }

            //step 2 : create a boolean variable to check whether data is updated successfully or not
            bool success = dal.Update(u);

            //remove the previous image

            if (rowHeaderImage != "no-image.jpg")
            {
                //path of the project folder
                string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                //give the path to image folder
                imagePath = paths + "\\images\\" + rowHeaderImage;

                //call clear function to clear all the textboxes and picturebox
                clear();
                //call garbage collection function
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //delete the physical file of the user profile
                File.Delete(imagePath);

            }

            //lets check whether the data is updated or not
            if (success == true)
            {
                //dated updated successfully
                MessageBox.Show("User updated successfully");

                //refresh data gridview
                DataTable dt = dal.select();
                dgvUsers.DataSource = dt;

                //clear the textboxes
                clear();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //step 1 : get user from text box to delete the user
            u.UserId = int.Parse(txtUserId.Text);

            //remove the physical file of the user profile
            if(rowHeaderImage != "no-image.jpg")
            {
                //path of the project folder
                string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                //give the path to image folder
                 imagePath = paths + "\\images\\" + rowHeaderImage;

                //call clear function to clear all the textboxes and picturebox
                clear();
                //call garbage collection function
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //delete the physical file of the user profile
                File.Delete(imagePath);

            }

            //create the boolean value to check whether the user is deleted or not
            bool success = dal.Delete(u);

            //lets check whether user is deleted or not
            if (success == true)
            {
                //data or user deleted successfully
                MessageBox.Show(" user deleted successfully");

                //refresh datagridview
                DataTable dt = dal.select();
                dgvUsers.DataSource = dt;

                //clear textboxes
                clear();
            }
            

        }

        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //find out the row index of row clicked on users data grid view
            int RowIndex = e.RowIndex;
            txtUserId.Text = dgvUsers.Rows[RowIndex].Cells[0].Value.ToString();
            txtUsername.Text = dgvUsers.Rows[RowIndex].Cells[1].Value.ToString();
            txtEmail.Text = dgvUsers.Rows[RowIndex].Cells[2].Value.ToString();
            txtPassword.Text = dgvUsers.Rows[RowIndex].Cells[3].Value.ToString();
            txtFullName.Text = dgvUsers.Rows[RowIndex].Cells[4].Value.ToString();
            txtContact.Text = dgvUsers.Rows[RowIndex].Cells[5].Value.ToString();
            txtAddress.Text = dgvUsers.Rows[RowIndex].Cells[6].Value.ToString();
            imagename = dgvUsers.Rows[RowIndex].Cells[8].Value.ToString();

            //update the value of global variable rowHeader image
            rowHeaderImage = imagename;
            //display the image of selected user
            //get the image path
            string paths = Application.StartupPath.Substring(0,( Application.StartupPath.Length - 10));

            if(imagename !="no-image.jpg" )
            {
                //path to destination folder
                 imagePath = paths + "\\images\\" + imagename;

                //display in picture box
                pictureBoxProfilePicture.Image = new Bitmap(imagePath);

            }
            else
            {
                //path to destination folder
                imagePath = paths + "\\images\\no-image.jpg";

                //display in picture box
                pictureBoxProfilePicture.Image = new Bitmap(imagePath);
            }
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            //display the users in data grid view when the form is loaded
            DataTable dt = dal.select();
            dgvUsers.DataSource = dt;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //call the user function
            clear();
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            //Write the code to upload the image of the user
            //open dialog box to select image
            OpenFileDialog open = new OpenFileDialog();

            //filter the file type .Only Allow image file types
            open.Filter = " image files( *.jpg; *.jpeg; *.png; *.PNG;  *.gif;)| *.jpg; *.jpeg; *.png; *.PNG;  *.gif;)";

            //check if the file is selected or not
            if(open.ShowDialog() == DialogResult.OK)
            {
                //check if the file exist or not
                if(open.CheckFileExists)
                {
                    //display the selected file on picturebox
                    pictureBoxProfilePicture.Image = new Bitmap(open.FileName);

                    //rename the image we selected
                    //step 1 : get the extension of  image
                    string ext = Path.GetExtension(open.FileName);

                    //2. generate random integer
                    Random random = new Random();
                    int RandInt = random.Next(0, 1000);

                   // 3. rename the image
                    imagename = "Blood_Bank_MS_" + RandInt + ext;

                    //get the path of selected image 
                     sourcePath = open.FileName;

                    //get the path of destination
                    string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);

                    //path to destination folder
                     destinationPath = paths + "\\images\\" + imagename;

                    //copy the image to the destination folder
                    //File.Copy(sourcePath, destinationPath);

                    //display message
                    //MessageBox.Show("Image successfully uploaded");

                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //write the code to get the users based on keywords

            //1. get the keywords from the textbox
            string keywords = txtSearch.Text;

            //check whether textbox is empty or not
            if(keywords != null)
            {
                //textbox  is not empty, display users on data grid view based on keywords
                DataTable dt = dal.search(keywords);
                dgvUsers.DataSource = dt;
            }
            else
            {
                //textbox is empty and display users on gridview
                DataTable dt  = dal.select();
                dgvUsers.DataSource = dt;
            }
        }
    }
}
