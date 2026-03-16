
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;
namespace DVLD_Data_Access_Layer
{
    public class clsDetaineLicenseData
    {
        public static int AddDetainedLicense(int LicenseID,DateTime DetainDate,decimal FineFees,int CreatedByUserID,bool IsReleased,DateTime? ReleaseDate,int? ReleasedByUserID,int? ReleaseApplicationID)
        {
            int DetainID = -1;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "INSERT INTO DetainedLicenses (LicenseID,DetainDate,FineFees,CreatedByUserID,IsReleased,ReleaseDate,ReleasedByUserID,ReleaseApplicationID) VALUES (@LicenseID,@DetainDate,@FineFees,@CreatedByUserID,@IsReleased,@ReleaseDate,@ReleasedByUserID,@ReleaseApplicationID);SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID ?? (object)DBNull.Value);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(),out int ID))
                {
                    DetainID = ID;
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
            return DetainID;
        }
        public static bool UpdateDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, DateTime? ReleaseDate, int? ReleasedByUserID, int? ReleaseApplicationID)
        {
            int rowsAfficted = 0;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "UPDATE DetainedLicenses SET LicenseID=@LicenseID, DetainDate=@DetainDate, FineFees=@FineFees, CreatedByUserID=@CreatedByUserID, IsReleased=@IsReleased, ReleaseDate=@ReleaseDate, ReleasedByUserID=@ReleasedByUserID, ReleaseApplicationID=@ReleaseApplicationID WHERE DetainID=@DetainID";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            command.Parameters.AddWithValue("@ReleaseDate",ReleaseDate == DateTime.MinValue ? DBNull.Value : (object)ReleaseDate); command.Parameters.AddWithValue("@ReleasedByUserID", (object)ReleasedByUserID ?? DBNull.Value);
            command.Parameters.AddWithValue("@ReleaseApplicationID", (object)ReleaseApplicationID ?? DBNull.Value);
            try
            {
                connection.Open();
                rowsAfficted = command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return rowsAfficted > 0;
        }
        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "select DetainedLicenses.DetainID,DetainedLicenses.LicenseID,DetainedLicenses.IsReleased,DetainedLicenses.FineFees,DetainedLicenses.ReleaseDate,People.NationalNo,CONCAT(People.FirstName,' ',People.SecondName,' ',People.ThirdName,' ',People.LastName)as FullName,DetainedLicenses.ReleaseApplicationID\r\nfrom DetainedLicenses join Licenses on DetainedLicenses.LicenseID=Licenses.LicenseID join Drivers on Licenses.DriverID=Drivers.DriverID join People on Drivers.PersonID=People.PersonID";
            SqlCommand command=new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    dt.Load(reader);
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
            return dt;
        }
        public static bool IsDetianed(int LicenseID)
        {
            bool isDetained = false;
            SqlConnection connection =new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT COUNT(1) FROM DetainedLicenses WHERE LicenseID=@LicenseID AND IsReleased=0";
            SqlCommand command= new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                int count = (int)command.ExecuteScalar();

                isDetained = count > 0;

            }
            catch
            {
                isDetained = false;
                throw;
            }
            finally
            {
                connection.Close();
            }
            return isDetained;
        }
        public static bool GetDetainLicense(int LicenseID, ref  int  DetainID, ref  DateTime DetainDate,ref  decimal FineFees,ref  int CreatedByUserID,ref  bool IsReleased,ref  DateTime? ReleaseDate, ref int? ReleasedByUserID,ref  int? ReleaseApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DB_Connection_Settings.connectionString);
            string query = "SELECT TOP 1 * FROM DetainedLicenses WHERE LicenseID=@LicenseID";
            SqlCommand command= new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    isFound = true;
                    DetainID = (int)reader["DetainID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (decimal)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];
                    if (reader["ReleaseDate"]==DBNull.Value)
                    {
                        ReleaseDate = (DateTime?)null;
                    }
                    else
                    {
                        ReleaseDate = (DateTime?)reader["ReleaseDate"];
                    }
                    if (reader["ReleasedByUserID"] == DBNull.Value)
                    {
                        ReleasedByUserID = (int?)null;
                    }
                    else
                    {
                        ReleasedByUserID = (int?)reader["ReleasedByUserID"];
                    }
                    if (reader["ReleaseApplicationID"] == DBNull.Value)
                    {
                        ReleaseApplicationID = (int?)null;
                    }
                    else
                    {
                        ReleaseApplicationID = (int?)reader["ReleaseApplicationID"];
                    }
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
            return isFound;

        }





    }
}
