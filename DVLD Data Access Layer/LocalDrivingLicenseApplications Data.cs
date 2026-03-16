using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVLD_Data_Access_Layer
{
    public class clsLocalDrivingLicenseApplicationsData
    {
        public static int AddLocalDrivingLicenseApplication(int ApplicationID,int LicenseClassID)
        {
            int LocalDrivingLicenseApplicationID = -1;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "INSERT INTO LocalDrivingLicenseApplications (ApplicationID, LicenseClassID) VALUES (@ApplicationID, @LicenseClassID);SELECT SCOPE_IDENTITY();";
            SqlCommand command= new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                Object result= command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    LocalDrivingLicenseApplicationID= InsertedID;
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
            return LocalDrivingLicenseApplicationID;

        }
        public static bool UpdateLocalDrivingLicenseApplications(int LocalDrivingLicenseApplicationID,int ApplicationID,int LicenseClassID)
        {
            int rowsAffected= 0;
            SqlConnection connection=new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "UPDATE LocalDrivingLicenseApplications SET ApplicationID = @ApplicationID, LicenseClassID = @LicenseClassID WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";
            SqlCommand command= new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
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
        public static DataTable GetAllLocalDrivingLicenseApplictions()
        {
            DataTable LocalDrivingLicenseApplicationTable= new DataTable();
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT * FROM LocalDrivingLicenseApplications_View";
            SqlCommand command= new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader= command.ExecuteReader();  
                if(reader.HasRows)
                {
                    LocalDrivingLicenseApplicationTable.Load(reader);
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
            return LocalDrivingLicenseApplicationTable;
        }
        public static int isPersonHasActiveApplictionWithThisClass(string NationalNo, string LicenseClass)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT LocalDrivingLicenseApplicationID FROM LocalDrivingLicenseApplications_View WHERE NationalNo =@NationalNo AND ClassName LIKE  '%'+@LicenseClass+'%' AND Status like '%New%' ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                    ApplicationID = Convert.ToInt32(result);
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return ApplicationID;

        }
        public static int isPersonHasCompletedApplictionWithThisClass(string NationalNo, string LicenseClass)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT LocalDrivingLicenseApplicationID FROM LocalDrivingLicenseApplications_View WHERE NationalNo =@NationalNo AND ClassName LIKE  '%'+@LicenseClass+'%' AND Status like '%Completed%' ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                    ApplicationID = Convert.ToInt32(result);
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return ApplicationID;

        }
        public static int GetApplicationIDbyLDLAppID(int LDLAppID)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT ApplicationID FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID=@LDLAppID";
            SqlCommand command= new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result !=null)
                    ApplicationID=Convert.ToInt32(result);
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return ApplicationID;
        }
        public static bool DeleteLocalDrivingLicenseApplication(int LDLAppID)
        {
            int rowsAffected= 0;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "DELETE FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID=@LDLAppID";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID",LDLAppID);
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
        public static bool GetLDLApp(int LDLAppID,ref int LicenseClassID,ref int ApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID=@LDLAppID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        isFound = true;
                        LicenseClassID = (int)reader["LicenseClassID"];
                        // ⚠️ تأكد الاسم هنا مطابق قاعدة البيانات
                        ApplicationID = (int)reader["ApplicationID"];
                    }
                }
            }
            catch { throw; }
            finally { connection.Close(); }

            return isFound;
        }
        public static int PassedTestsCount(int LDLAppID)
        {
            int Count = 0;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "select count(*) from TestAppointments join LocalDrivingLicenseApplications on TestAppointments.LocalDrivingLicenseApplicationID=LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID join Tests on TestAppointments.TestAppointmentID=Tests.TestAppointmentID Where Tests.TestResult=1 And LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=@LDLAppID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            try
            {
                connection.Open();
                Object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int PassedCount))
                {
                    Count = PassedCount;
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
            return Count;
        }
    }
}
