using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Access_Layer
{
    public class clsAppointmentData
    {
        public static int AddAppointment(int TestTypeID, int LDLAppID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool isLocked,int RetakeTestApplicationID)
        {
            int TestAppointmentID = -1;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "INSERT INTO TestAppointments (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked,RetakeTestApplicationID) VALUES (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees, @CreatedByUserID, @IsLocked,@RetakeTestApplicationID);SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("LocalDrivingLicenseApplicationID", LDLAppID);
            command.Parameters.AddWithValue("AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("PaidFees", PaidFees);
            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("IsLocked", isLocked);
            if(RetakeTestApplicationID==-1)
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

            }
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    TestAppointmentID = Convert.ToInt32(result);
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
            return TestAppointmentID;
        }
        public static bool UpateAppointment(int TestAppintmentID, int TestTypeID, int LDLAppID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool isLocked,int RetakeTestApplicationID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "UPDATE TestAppointments SET TestTypeID=@TestTypeID, LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID, AppointmentDate=@AppointmentDate, PaidFees=@PaidFees, CreatedByUserID=@CreatedByUserID, IsLocked=@IsLocked, RetakeTestApplicationID=@RetakeTestApplicationID WHERE TestAppointmentID=@TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("LocalDrivingLicenseApplicationID", LDLAppID);
            command.Parameters.AddWithValue("AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("PaidFees", PaidFees);
            command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("IsLocked", isLocked);
            command.Parameters.AddWithValue("TestAppointmentID", TestAppintmentID);
            if (RetakeTestApplicationID == -1)
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

            }
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
        public static DataTable GetLDLAppTestAppointments(int LDLAppID, int TestTypeID)
        {
            DataTable AppointmentsTable = new DataTable();
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT * FROM TestAppointments WHERE LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID AND TestTypeID=@TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("LocalDrivingLicenseApplicationID", LDLAppID);
            command.Parameters.AddWithValue("TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    AppointmentsTable.Load(reader);
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
            return AppointmentsTable;
        }
        public static bool HasActiveAppointment(int LDLAppID, int TestTypeID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT COUNT(*) FROM TestAppointments WHERE LocalDrivingLicenseApplicationID=@LDLAppID AND TestTypeID=@TestTypeID AND IsLocked=0";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && Convert.ToInt32(result) > 0)
                {
                    isFound = true;
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
            return isFound;
        }
        public static bool GetAppointmentByID(int AppointmentID, ref int TestTypeID, ref int LDLAppID, ref DateTime AppointmentDate, ref decimal PaidFees, ref int CreatedByUserID, ref bool isLocked)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID=@TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", AppointmentID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    TestTypeID = Convert.ToInt32(reader["TestTypeID"]);
                    LDLAppID = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]);
                    AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                    PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    isLocked = Convert.ToBoolean(reader["IsLocked"]);
                    isFound = true;
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
        public static bool GetAppintmentByLDLAppIDAndTestType(int LDLAppID, int TestTypeID, ref int TestAppointmentID, ref DateTime AppointmentDate, ref decimal PaidFees, ref int CreatedByUserID, ref bool isLocked)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = @"
                            SELECT TOP 1 * 
                            FROM TestAppointments 
                            WHERE LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID 
                            AND TestTypeID=@TestTypeID
                            ORDER BY TestAppointmentID DESC"; 
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    TestAppointmentID = Convert.ToInt32(reader["TestAppointmentID"]);
                    AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                    PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    isLocked = Convert.ToBoolean(reader["IsLocked"]);
                    isFound = true;
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
        public static bool HasAppoinments(int LDLAppID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT COUNT(*) FROM TestAppointments WHERE LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && Convert.ToInt32(result) > 0)
                {
                    isFound = true;
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
            return isFound;
        }
        public static int NumberOfTrails(int LDLAppID,int TestTypeID)
        {
            int NumberOfTrails = 0;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT COUNT(*) FROM TestAppointments WHERE LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID AND TestTypeID=@TestTypeID AND IsLocked=1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    NumberOfTrails = Convert.ToInt32(result);
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
            return NumberOfTrails;
        }
    }
}
