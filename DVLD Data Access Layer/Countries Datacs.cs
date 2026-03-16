using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Dynamic;

namespace DVLD_Data_Access_Layer
{
    public class Countries_Datacs
    {
        public static DataTable GetAllCountries()
        {
            DataTable CountriesTable = new DataTable();
            SqlConnection connection= new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT * FROM Countries";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader=command.ExecuteReader();
                if(reader.HasRows)
                {
                    CountriesTable.Load(reader);
                }
                reader.Close();
            }
            catch
            {
                throw;
            }finally
            {
                connection.Close();
            }
            return CountriesTable;
        }
        public static bool GetCountryByID(int CountryID, ref string CountryName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT CountryName FROM Countries WHERE CountryID=@CountryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    CountryName = (string)reader["CountryName"];
                }
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
