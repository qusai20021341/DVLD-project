using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVLD_Data_Access_Layer
{
    public class clsTestTypesData
    {
        public static DataTable GetAllTestTypes()
        {
            DataTable TestTypesTable = new DataTable();
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT * FROM TestTypes";
            SqlCommand command= new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader= command.ExecuteReader();
                if(reader.HasRows)
                {
                    TestTypesTable.Load(reader);
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
            return TestTypesTable;
        }
        public static bool GetTestTypeByID(int TestTypeID, ref string TestTypeTitle, ref string TestTypeDescription,ref  decimal TestTypeFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT * FROM TestTypes WHERE TestTypeID=@TestTypeID ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader= command.ExecuteReader();
                if(reader.Read())
                {
                    isFound = true;
                    TestTypeTitle = (string)reader["TestTypeTitle"];
                    TestTypeDescription = (string)reader["TestTypeDescription"];
                    TestTypeFees = (decimal)reader["TestTypeFees"];
                }
                reader.Close();

            }catch
            {
                isFound= false;
                throw;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool GetTestTypeByTestTypeTitle(string TestTypeTitle, ref int TestTypeID , ref string TestTypeDescription, ref decimal TestTypeFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT * FROM TestTypes WHERE TestTypeTitle LIKE @TestTypeTitle ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeTitle", "%"+TestTypeTitle+"%");
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestTypeID = (int)reader["TestTypeID"];
                    TestTypeDescription = (string)reader["TestTypeDescription"];
                    TestTypeFees = (decimal)reader["TestTypeFees"];
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
        public static bool UpdateTestType(int TestTypeID,string TestTypeTitle,string TestTypeDescription,decimal TestTypeFees)
        {
            int rowsAffected = 0;
            SqlConnection connection= new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "UPDATE TestTypes SET TestTypeTitle = @TestTypeTitle, TestTypeDescription = @TestTypeDescription, TestTypeFees = @TestTypeFees WHERE TestTypeID = @TestTypeID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
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
        public static int GetTestTypeIDByTestTypeTitle(string TestTypeTitle)
        {
            int TestTypeID = -1;
            SqlConnection connection =new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT TestTypeID FROM TestTypes WHERE TestTypeTitle LIKE @TestTypeTitle";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeTitle", "%" + TestTypeTitle + "%");
            try
            {
                connection.Open();
                object result=command.ExecuteScalar();
                if(result !=null)
                {
                    TestTypeID= (int)result;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }


            return TestTypeID;
        }
    }
}
