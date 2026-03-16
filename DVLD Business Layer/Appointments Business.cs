using DVLD_Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsAppointment
    {
        enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode;
        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public int LDLAppID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }

        public clsAppointment()
        {
            _Mode = enMode.AddNew;
            TestAppointmentID = -1;
            TestTypeID = -1;
            LDLAppID = -1;
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;
            IsLocked = true;
            RetakeTestApplicationID = -1;

        }
        private clsAppointment(int TestAppointmentID, int TestTypeID, int LDLAppID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            _Mode = enMode.Update;
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LDLAppID = LDLAppID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;

        }
        private bool _AddAppointment()
        {
            this.TestAppointmentID = clsAppointmentData.AddAppointment(this.TestTypeID, this.LDLAppID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked,this.RetakeTestApplicationID);
            return this.TestAppointmentID > -1;
        }
        private bool _UpdateAppointment()
        {
            return clsAppointmentData.UpateAppointment(this.TestAppointmentID, this.TestTypeID, this.LDLAppID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked,this.RetakeTestApplicationID);
        }
        public bool Save()
        {
            if (_Mode == enMode.AddNew)
            {
                if (_AddAppointment())
                {
                    _Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else if (_Mode == enMode.Update)
            {
                if (_UpdateAppointment())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static DataTable GetLDLTestAppointments(int LDLAppID, int TestTypeID)
        {
            return clsAppointmentData.GetLDLAppTestAppointments(LDLAppID, TestTypeID);
        }
        public static bool HasActiveAppointment(int LDLAppID, int TestTypeID)
        {
            return clsAppointmentData.HasActiveAppointment(LDLAppID, TestTypeID);
        }
        public static clsAppointment GetAppointmentByID(int AppointmentID)
        {
            int TestTypeID = -1;
            int LDLAppID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = 0;
            int CreatedByUserID = -1;
            bool isLocked = false;
            int RetakeTestApplicationID = -1;
            if (clsAppointmentData.GetAppointmentByID(AppointmentID, ref TestTypeID, ref LDLAppID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref isLocked))
            {
                return new clsAppointment(AppointmentID, TestTypeID, LDLAppID, AppointmentDate, PaidFees, CreatedByUserID, isLocked, RetakeTestApplicationID);
            }
            else
            {
                return null;
            }
        }
        public static clsAppointment GetAppointmentByLDLAppIDAndTestTypeID(int LDLAppID, int TestTypeID)
        {
            int TestAppointmentID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = 0;
            int CreatedByUserID = -1;
            bool isLocked = false;
            int RetakeTestApplicationID = -1;
            if (clsAppointmentData.GetAppintmentByLDLAppIDAndTestType(LDLAppID, TestTypeID, ref TestAppointmentID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref isLocked))
            {
                return new clsAppointment(TestAppointmentID, TestTypeID, LDLAppID, AppointmentDate, PaidFees, CreatedByUserID, isLocked,RetakeTestApplicationID);
            }
            else
            {
                return null;
            }


        }
        public static bool HasAppointments(int LDLAppID)
        {
            return clsAppointmentData.HasAppoinments(LDLAppID);
        }
        public static int NumberOfTrails(int LDLAppID, int TestTypeID)
        {
            return clsAppointmentData.NumberOfTrails(LDLAppID,TestTypeID);
        }


    }
}
