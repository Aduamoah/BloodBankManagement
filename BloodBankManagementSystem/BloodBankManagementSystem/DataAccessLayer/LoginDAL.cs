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
    class LoginDAL
    {
           
        //create a connection string to connect to database
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
  
        public bool LoginCheck(LoginBLL l)
        {
            //create a boolean variable and set its default value to false
            bool IsSuccess = false;
            //create an object of sql connection to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //sql query to check login based on username and password
                string sql = "SELECT * FROM tbl_users WHERE Username=@Username AND Password=@Password";
                //create a sql  command to pass the value in our query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //create the parameter to pass get the value from UI and pass it on sql query above
                cmd.Parameters.AddWithValue("@Username", l.Username);
                cmd.Parameters.AddWithValue("@Password", l.Password);

                //create sql data adapter to hold the data from database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //datatable to hold the data from database temporarily
                DataTable dt = new DataTable();

                //transfer data from sqldata adapter to datatable
                adapter.Fill(dt);
                //check whether user exists or not
                if (dt.Rows.Count > 0)
                {
                    //User exist and login is successful
                    IsSuccess = true;
                }
                else
                {
                    //login failed
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
    }
}
