using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Data_Access_Layer
{
    public class clsTestAppointmentData
    {
        public static int AddTestAppointment(int TestTypeID, int LDLAppID,DateTime ApppointmentDate,decimal PiadFees,int CreatedByUserID,bool isLocked)
        {
            int TestAppointmentID = -1;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string qurey = "INSERT INTO TestAppointments (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked) VALUES (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees, @CreatedByUserID, @IsLocked);SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(qurey, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);
            command.Parameters.AddWithValue("@AppointmentDate", ApppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PiadFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@isLocked", isLocked);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null)
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
        public static bool UpdateTestAppointment(int TestAppintmentID, int TestTypeID, int LDLAppID, DateTime ApppointmentDate, decimal PiadFees, int CreatedByUserID, bool isLocked)
        {
            int rowsAffected=0;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "UPDATE TestAppointments SET TestTypeID = @TestTypeID, LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID, AppointmentDate = @AppointmentDate, PaidFees = @PaidFees, CreatedByUserID = @CreatedByUserID, IsLocked = @IsLocked WHERE TestAppointmentID = @TestAppointmentID;";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppintmentID", TestAppintmentID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            command.Parameters.AddWithValue("@ApppointmentDate", ApppointmentDate);
            command.Parameters.AddWithValue("@PiadFees", PiadFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@isLocked", isLocked);
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
    }
}
