using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVLD_Data_Access_Layer
{
    public  class clsDriverData
    {
        public static int AddDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int DriverID = -1;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "INSERT INTO Drivers (PersonID, CreatedByUserID, CreatedDate) VALUES (@PersonID, @CreatedByUserID, @CreatedDate); SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int newDriverID))
                {
                    DriverID = newDriverID;
                }
            }catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return DriverID;
        }
        public static DataTable GetAllDrivers()
        {
            DataTable Drivers = new DataTable();
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "Select DISTINCT *\r\nfrom(\r\nSelect  Drivers.DriverID,Drivers.PersonID,People.NationalNo,People.FirstName+People.SecondName+People.LastName As FullName,Drivers.CreatedDate,Licenses.IsActive\r\nFrom Drivers join People on Drivers.PersonID= People.PersonID join Licenses on Drivers.DriverID=Licenses.DriverID\r\n)as d";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    Drivers.Load(reader);
                }
                reader.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return Drivers;
        }
        public static bool GetDriver(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT * FROM Drivers WHERE DriverID = @DriverID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    isFound = true;
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
        public static bool GetDriverByPersonID(
    int PersonID,
    ref int DriverID,
    ref int CreatedByUserID,
    ref DateTime CreatedDate)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);

            string query = "SELECT * FROM Drivers WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    isFound = true;
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
        public static bool isPersonADriver(int PersonID)
        {
            bool isDriver = false;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT COUNT(*) FROM Drivers WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                int count = (int)command.ExecuteScalar();
                isDriver = count > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return isDriver;
        }

    }
}
