using BloodBankManagementSystem.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BloodBankManagementSystem.DataAccessLayer
{
    class userDAL
    {
        //create a static string method to connect Database
      static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region SELECT data from database
        public DataTable select()
        {
            //create an object to connect to database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //create a datatable to hold the data from database
            DataTable dt = new DataTable();
            try
            {
                //write sql query to get data from database
                string sql = "SELECT * FROM tbl_users";

                //create sql command to execute query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //create sql data adapter to hold the data from database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //open dtabase connection 
                conn.Open();

                //transfer data from sqldata adapter to datatable
                adapter.Fill(dt);
            }
            catch(Exception ex)
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

        #region Insert Data into Database for User Module
        public bool Insert(UserBLL u)
        {
            //create a boolean variable and set its default value to false
            bool IsSuccess = false;
            //create an object of sql connection to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //create a string variable to store the INSERT QUERY
                string sql = "INSERT INTO tbl_users(Username, Email, Password, Fullname, Contact, Address, Added_date, Image_name) VALUES (@Username, @Email, @Password, @Fullname, @Contact, @address, @Added_date, @Image_name)";

                //create a sql  command to pass the value in our query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //create the parameter to pass get the value from UI and pass it on sql query above
                cmd.Parameters.AddWithValue("@Username", u.Username);
                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.Parameters.AddWithValue("@Password", u.Password);
                cmd.Parameters.AddWithValue("@Fullname", u.Fullname);
                cmd.Parameters.AddWithValue("@Contact", u.Contact);
                cmd.Parameters.AddWithValue("@Address", u.Address);
                cmd.Parameters.AddWithValue("@Added_date", u.Added_date);
                cmd.Parameters.AddWithValue("@Image_name", u.Image_name);

                //open database connection
                conn.Open();

                //create an integer variable to hold the variable after the query is executed
                int rows = cmd.ExecuteNonQuery();

                //the value of rows will be greater than 0 if the query is executed  successfully
                //else it will be 0

                if(rows > 0)
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
            catch(Exception ex)
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
        #region UPDATE data in database (user module)
        public bool Update(UserBLL u)
        {
            //create  a boolean variable and set its default value to false
            bool IsSuccess = false;

            //create an object for database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //create a string variable to hold the sql query

                string sql = "UPDATE tbl_users SET Username=@Username, Email=@Email, @Password=Password, Fullname=@Fullname, Contact=@Contact, Address=@Address, Added_date=@Added_date, Image_name=@Image_name WHERE UserId=@UserId";


                //create sql command to execute query and also pass the value to sql query
                SqlCommand cmd = new SqlCommand(sql,conn);

                //now pass the values to sql query

                cmd.Parameters.AddWithValue("@Username", u.Username);
                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.Parameters.AddWithValue("@Password", u.Password);
                cmd.Parameters.AddWithValue("@Fullname", u.Fullname);
                cmd.Parameters.AddWithValue("@Contact", u.Contact);
                cmd.Parameters.AddWithValue("@Address", u.Address);
                cmd.Parameters.AddWithValue("@Added_date", u.Added_date);
                cmd.Parameters.AddWithValue("@Image_name", u.Image_name);
                cmd.Parameters.AddWithValue("@UserId", u.UserId);

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
        #region Delete Data from Database (User Module)
        public bool Delete(UserBLL u)
        {
            //create a boolean variable and set its default value to false
            bool IsSuccess = false;
            //create an object  for sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //create a string variable to hold the sql query to delete data
                string sql = "DELETE FROM tbl_users WHERE UserID=@UserID";
                //create sql command to execute the query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //pass the value through parameters
                cmd.Parameters.AddWithValue("@UserId", u.UserId);

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
            catch(Exception ex)
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
        #region SEARCH
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
                string sql = "SELECT * FROM tbl_users WHERE UserId LIKE '%" + keywords + "%' OR Fullname LIKE '%" + keywords + "%' OR Address LIKE '%" + keywords + "%'";

                //create sql command to execute query
                SqlCommand cmd = new SqlCommand(sql,conn);

                //create sql data adapter to hold the data from database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                // open database connection
                conn.Open();

                //transfer data from sqldata adapter to datatable
                adapter.Fill(dt);
            }
            catch(Exception ex)
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
        #region
        public UserBLL GetIDFromUsername(string Username)
        {
            UserBLL u = new UserBLL();

            //create an object of sql connection to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);

            //create datatable to hold data from database temporarily
            DataTable dt = new DataTable();
            try
            {
                //write the sql query to get the ID from USERNAME
                string sql = "SELECT  UserId FROM tbl_users WHERE Username '" + Username + "' ";

                //create sql data adapter to hold the data from database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);

                // open database connection
                conn.Open();

                //transfer data from sqldata adapter to datatable
                adapter.Fill(dt);
                //if theres user based on the useename get userId
                if (dt.Rows.Count > 0)
                {
                    u.UserId = int.Parse(dt.Rows[0]["UserId"].ToString());
                }
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
            return u;
        }
        #endregion
    }

}
