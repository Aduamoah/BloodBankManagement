using BloodBankManagementSystem.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBankManagementSystem.DataAccessLayer
{
    class donorDAL
    {
        //create a connection string to connect to database
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;  
        #region SELECT to display data in datagridview from database
        public DataTable select()
        {
            //create object to datatable to hold the data from database and return it
            DataTable dt = new DataTable();

            //create an object of sql connection to connect to database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //write sql query to get data from database
                string sql = "SELECT * FROM tbl_donors";

                //create sql command to execute query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //create sql data adapter to hold the data from database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //open dtabase connection 
                conn.Open();

                //transfer data from sqldata adapter to datatable
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                //Display error message if there's any exceptional errors
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //close Database Connection
                conn.Close();
            }
            return dt;
        }
        #endregion
        #region Insert Data into Database 
        public bool Insert(donorBLL d)
        {
            //create a boolean variable and set its default value to false
            bool IsSuccess = false;
            //create an object of sql connection to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //create a string variable to store the INSERT QUERY
                string sql = "INSERT INTO tbl_donors(Firstname, Lastname, Email, Contact, Gender, Address, Bloodgroup, Added_date, Image_name, Added_by) VALUES (@Firstname, @Lastname, @Email, @Contact, @Gender,  @Address, @Bloodgroup, @Added_date, @Image_name, @Added_by)";

                //create a sql  command to pass the value in our query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //create the parameter to pass get the value from UI and pass it on sql query above
                cmd.Parameters.AddWithValue("@Firstname", d.Firstname);
                cmd.Parameters.AddWithValue("@Lastname", d.Lastname);
                cmd.Parameters.AddWithValue("@Email", d.Email);
                cmd.Parameters.AddWithValue("@Contact", d.Contact);
                cmd.Parameters.AddWithValue("@Gender", d.Gender);
                cmd.Parameters.AddWithValue("@Address", d.Address);
                cmd.Parameters.AddWithValue("@Bloodgroup", d.Bloodgrooup);
                cmd.Parameters.AddWithValue("@Added_date", d.Added_date);
                cmd.Parameters.AddWithValue("@Image_name", d.Image_name);
                cmd.Parameters.AddWithValue("@Added_by", d.Added_by);
              

                //open database connection
                conn.Open();

                //create an integer variable to hold the variable after the query is executed
                int rows = cmd.ExecuteNonQuery();

                //the value of rows will be greater than 0 if the query is executed  successfully
                //else it will be 0

                if (rows > 0)
                {
                    //query executed successfully
                    IsSuccess = true;
                }
                else
                {
                    //failed to execute query
                    IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                //display Error Message if there's any exceptional errors
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //close database connection
                conn.Close();
            }
            return IsSuccess;
        }

        #endregion
        #region UPDATE donor in database 
        public bool Update(donorBLL d)
        {
            //create  a boolean variable and set its default value to false
            bool IsSuccess = false;

            //create an object for database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //create a string variable to hold the sql query

                string sql = "UPDATE tbl_donors SET Firstname=@Firstname,Lastname=@Lastname,Email=@Email,Contact=@Contact,Gender=@Gender,Image_name=@Image_name,Added_by=@Added_by WHERE DonorId=@DonorId";


                //create sql command to execute query and also pass the value to sql query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //now pass the values to sql query

                cmd.Parameters.AddWithValue("@Firstname", d.Firstname);
                cmd.Parameters.AddWithValue("@Lastname", d.Lastname);
                cmd.Parameters.AddWithValue("@Email", d.Email);
                cmd.Parameters.AddWithValue("@Contact", d.Contact);
                cmd.Parameters.AddWithValue("@Gender", d.Gender);
                cmd.Parameters.AddWithValue("@Address", d.Address);
                cmd.Parameters.AddWithValue("@Bloodgroup", d.Bloodgrooup);
                cmd.Parameters.AddWithValue("@Image_name", d.Image_name);
                cmd.Parameters.AddWithValue("@Added_by", d.Added_by);
                cmd.Parameters.AddWithValue("@DonorId", d.DonorId);

                //open database connection
                conn.Open();

                //create an integer variable to hold the value after the query is executed
                int rows = cmd.ExecuteNonQuery();

                //if the query is executed succesfully then the value of rows will be greater than 0
                //else it'll be 0

                if (rows > 0)
                {
                    //query executed successfully
                    IsSuccess = true;
                }
                else
                {
                    //failed to execute query
                    IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                //display error message if there,s any exceptional error
                MessageBox.Show(ex.Message);

            }
            finally
            {
                //close database connection
                conn.Close();
            }
            return IsSuccess;
        }
        #endregion
        #region Delete donor from Database (User Module)
        public bool Delete(donorBLL d)
        {
            //create a boolean variable and set its default value to false
            bool IsSuccess = false;
            //create an object  for sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //create a string variable to hold the sql query to delete data
                string sql = "DELETE FROM tbl_donors WHERE DonorId=@DonorId";
                //create sql command to execute the query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //pass the value through parameters
                cmd.Parameters.AddWithValue("@DonorId", d.DonorId );

                //open database connection
                conn.Open();

                //create an integer variable to hold the value after query is executed
                int rows = cmd.ExecuteNonQuery();
                //if the query is executed succesfully then the value of rows will be greater than 0
                //else it'll be 0

                if (rows > 0)
                {
                    //query executed successfully
                    IsSuccess = true;
                }
                else
                {
                    //failed to execute query
                    IsSuccess = false;
                }


            }
            catch (Exception ex)
            {
                //Display error message if there's any Exceptional errors
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return IsSuccess;
        }

        #endregion
        #region count donors for a specific blood group
        public string countDonors(string Bloodgroup)
        {
            //create  sql connection to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //create a string variable for donor count and set its default value to 0
            string donors = "0";
            try
            {
                //sql query to count donors for specific bloodgroup
                string sql = "SELECT * FROM tbl_donors WHERE Bloodgroup = '" + Bloodgroup + "' ";

                //create sql command to execute query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //create sql data adapter to hold the data from database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //datatable to hold data temporarily
                DataTable dt = new DataTable();

                //transfer data from sqldata adapter to datatable
                adapter.Fill(dt);

                //get the total number of donors based on bloodgroup
                donors = dt.Rows.Count.ToString();
            }
            catch(Exception ex)
            {
                //display message if theres any
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //close database connection
                conn.Close();
            }
            return donors;
        }
        #endregion
        #region Method to SEARCH donors
        public DataTable search(string keywords)
        {
            //create an sql connection to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);

            //create datatable to hold data from database temporarily
            DataTable dt = new DataTable();

            //write the code to search the users
            try
            {
                //write the sql query to search the user from database
                string sql = "SELECT * FROM tbl_donors WHERE DonorId LIKE '%" + keywords + "%' OR Firstname LIKE '%" + keywords + "%' OR Lastname LIKE '%" + keywords + "%'  OR Email LIKE '%" + keywords + "%'  OR Bloodgroup LIKE '%" + keywords + "%'";

                //create sql command to execute query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //create sql data adapter to hold the data from database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                // open database connection
                conn.Open();

                //transfer data from sqldata adapter to datatable
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                //display error message if there are any exceptional errors
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //close the database connection
                conn.Close();
            }

            return dt;

        }
        #endregion
    }
}
