using BloodBankManagementSystem.BusinessLogicLayer;
using BloodBankManagementSystem.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBankManagementSystem.UI
{
    public partial class FrmDonors : Form
    {
        public FrmDonors()
        {
            InitializeComponent();
        }
        //create object of donor BLL and donor DAL
        donorBLL d = new donorBLL();
        donorDAL dal = new donorDAL();
        userDAL udal = new userDAL();

        

        //global variable for image
        string imagename = "no-image.jpg";
        string sourcePath = "";
        string destinationPath = "";
        string imagePath = "";

        //global variable for the image to delete
        string rowHeaderImage;
        private void lblAddress_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //close this form
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //we will write the code to add new donor
            //1.get the data from manage donors form
            //step 1: Get the values from UI
            d.Firstname = txtFirstname.Text;
            d.Lastname = txtLastname.Text;
            d.Email = txtEmail.Text;
            d.Contact = txtContact.Text;
            d.Gender = cmbGender.Text;
            d.Address = txtAddress.Text;
            d.Bloodgrooup = cmbBloodGroup.Text;
            d.Added_date = DateTime.Now;
            //get the id of logged in user
            string LoggedInUser = frmLogin.LoggedInUser;
            UserBLL usr = udal.GetIDFromUsername(LoggedInUser);
            d.Added_by = usr.UserId;//todo: get the id of the logged in user
            d.Image_name = imagename;

            //upload image
            //check whether the user has uploaded image or not
            if (imagename != "n0-image.jpg")
            {
                //user has selected the image
                File.Copy(sourcePath, destinationPath);
            }




            //step 2 : inserting data into database
            //create a boolean variable to check whether the data is inserted successfully or not
            bool success = dal.Insert(d);

            //step 3: check whether the data is inserted successfully or not
            if (success == true)
            {
                //data or user added successfully
                MessageBox.Show("New Donor added successfully");

                //display the user in datagridview
                DataTable dt = dal.select();
                dgvDonors.DataSource = dt;

                //clear textboxes
                clear();
            }
            else
            {
                //failed to add user
                MessageBox.Show("failed to add new Donor");
            }

        }

        public void clear()
        {
            txtFirstname.Text = "";
            txtLastname.Text = "";
            txtEmail.Text = "";
            txtDonorId.Text = ""; 
            cmbGender.Text = "";
            cmbBloodGroup.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            imagename = "no-image.jpg";
            
          
            //get the image path
            string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);

            //path to destination folder
            string imagePath = paths + "\\images\\no-image.jpg";

            //display in picture box
            pictureBoxProfilePicture.Image = new Bitmap(imagePath);

        }

        private void FrmDonors_Load(object sender, EventArgs e)
        {
            //display donors in datagridview
            DataTable dt = dal.select();
            dgvDonors.DataSource = dt;

            //get the image path
            string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);

            //path to destination folder
            string imagePath = paths + "\\images\\no-image.jpg";

            //display in picture box
            pictureBoxProfilePicture.Image = new Bitmap(imagePath);
        }

        private void dgvDonors_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDonors_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //find out the row index of row clicked on users data grid view
            int RowIndex = e.RowIndex;
            txtDonorId.Text = dgvDonors.Rows[RowIndex].Cells[0].Value.ToString();
            txtFirstname.Text = dgvDonors.Rows[RowIndex].Cells[1].Value.ToString();
            txtLastname.Text = dgvDonors.Rows[RowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDonors.Rows[RowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDonors.Rows[RowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvDonors.Rows[RowIndex].Cells[5].Value.ToString();
            txtAddress.Text = dgvDonors.Rows[RowIndex].Cells[6].Value.ToString();
            cmbBloodGroup.Text = dgvDonors.Rows[RowIndex].Cells[7].Value.ToString();
            imagename = dgvDonors.Rows[RowIndex].Cells[9].Value.ToString();

            //update the value of rowHeader image
            rowHeaderImage = imagename;

            //display the image of selected donor
            //get the image path
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
           
                //path to destination folder
                string imagePath = paths + "\\images\\" + imagename;

            //display image of selected donor
            pictureBoxProfilePicture.Image = new Bitmap(imagePath);


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //step 1: Get the values from UI
            d.DonorId = int.Parse(txtDonorId.Text);
            d.Firstname = txtFirstname.Text;
            d.Lastname = txtLastname.Text;
            d.Email = txtEmail.Text;
            d.Gender = cmbGender.Text;
            d.Bloodgrooup = cmbBloodGroup.Text;
            d.Contact = txtContact.Text;
            d.Address = txtAddress.Text;
            //get the id of logged in user
            string LoggedInUser = frmLogin.LoggedInUser;
            UserBLL usr = udal.GetIDFromUsername(LoggedInUser);
            d.Added_by = usr.UserId;//todo: get the id of the logged in user
      
            d.Image_name = imagename;

            //upload new image
            //check whether the user has selected the image or not
            if (imagename != "n0-image.jpg")
            {
                //user has selected the image
                File.Copy(sourcePath, destinationPath);
            }

            //step 2 : create a boolean variable to check whether data is updated successfully or not
            bool success = dal.Update(d);

            //remove previous image
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
                //data updated successfully
                MessageBox.Show("Donor updated successfully");
                clear();

                //refresh data gridview
                DataTable dt = dal.select();
                dgvDonors.DataSource = dt;


            }
            else
            {
                //data failed to update
                MessageBox.Show("Failed to update donors");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //step 1 : get the value from form
            d.DonorId = int.Parse(txtDonorId.Text);

            //check whether the donor has profile picture or not
            if (rowHeaderImage != "no-image.jpg")
            {
                //path of the project folder
                string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                //give the path to image folder
                string imagePath = paths + "\\images\\" + rowHeaderImage;

                //call clear function to clear all the textboxes and picturebox
                clear();
                //call garbage collection function
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //delete the physical file of the user profile
                File.Delete(imagePath);

            }

            //create the boolean value to check whether the donor is deleted or not
            bool success = dal.Delete(d);

            //lets check whether donor is deleted or not
            if (success == true)
            {
                //data or donor deleted successfully
                MessageBox.Show(" donor deleted successfully");

                //refresh datagridview
                DataTable dt = dal.select();
                dgvDonors.DataSource = dt;

            }
            else
            {
                //failed to delete
                MessageBox.Show("failed to delete Donor");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //clear the textboxes
            clear();
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            //code to select image and upload
            // 1. open dialog box to select image
            OpenFileDialog open = new OpenFileDialog();

            //filter the file type .Only Allow image file types
            open.Filter = " image files( *.jpg; *.jpeg; *.png;   *.gif;)| *.jpg; *.jpeg; *.png;   *.gif;)";

            //check whether the image is selected or not
            if (open.ShowDialog() == DialogResult.OK)
            {
                //check if the file exist or not
                if (open.CheckFileExists)
                {
                    //display the selected image in picturebox
                    pictureBoxProfilePicture.Image = new Bitmap(open.FileName);

                    //remove the selected image
                    //step 1 : get the extension of  image
                    string ext = Path.GetExtension(open.FileName);
                    string name = Path.GetFileNameWithoutExtension(open.FileName);

                    //2. generate random but globally unique identifier
                    Guid g = new Guid();
                    g = Guid.NewGuid();


                    // 3.  Finally rename the image
                    imagename = "Blood_Bank_MS_" + name + g + ext;

                    //get the  source path (path of image) 
                     sourcePath = open.FileName;

                    //get the path of destination
                    string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);

                    //path to destination folder
                     destinationPath = paths + "\\images\\" + imagename;

                    //upload the image to the destination folder
                    //File.Copy(sourcePath, destinationPath);

                    //display message
                    //MessageBox.Show("Image successfully uploaded");

                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Lets Add the functionality to search Donors
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
