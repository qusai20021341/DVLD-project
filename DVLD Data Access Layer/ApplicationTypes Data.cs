using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.Design;

namespace DVLD_Data_Access_Layer
{
    public class clsApplicationTypeData
    {
        public static DataTable GetAllApplicationTypes()
        {
            DataTable AllAplicationTypes = new DataTable();
            SqlConnection connection= new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT * FROM ApplicationTypes";
            SqlCommand command= new SqlCommand(query,connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    AllAplicationTypes.Load(reader);
                }
                reader.Close();

            }catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return AllAplicationTypes;
        }
        public static bool UpdateUplicationType(int ApplicationTypeID,string ApplicationTypeTitle,decimal ApplicationFees )
        {
            int rowsAffected = 0;
            SqlConnection connection= new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "UPDATE ApplicationTypes SET ApplicationTypeTitle = @ApplicationTypeTitle, ApplicationFees = @ApplicationFees WHERE ApplicationTypeID =@ApplicationTypeID ;";
            SqlCommand command=new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            try
            {
                connection.Open();
                rowsAffected= command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }




            return rowsAffected > 0;
        }
        public static bool GetApplicationTypeByID(int ApplicationTypeID,ref string ApplicationTypeTitle, ref decimal ApplicationFees)
        {
            bool isFound= false;
            SqlConnection connection= new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID;";
            SqlCommand command= new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@ApplicationTypeID",ApplicationTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader= command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    ApplicationFees = (decimal)reader["ApplicationFees"];
                }
                reader.Close();
            }catch
            {
                isFound = false;
                throw;
            }
            finally
            {
                connection.Close();
            }
            return isFound;

        }
        
        public static bool GetApplicationTypeByApplicationTypeTitle(string ApplicationTypeTitle,ref int ApplicationTypeID,ref decimal ApplicationFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeTitle = @ApplicationTypeTitle;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationFees = (decimal)reader["ApplicationFees"];
                }
                reader.Close();
            }
            catch
            {
                isFound = false;
                throw;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
    }
}
