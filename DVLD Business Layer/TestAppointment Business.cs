using DVLD_Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsTestAppointment
    {
        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode;
        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool isLocked { get; set; }

        public clsTestAppointment()
        {
            _Mode = enMode.AddNew;
            TestAppointmentID = -1;
            TestTypeID = -1;
            LocalDrivingLicenseApplicationID = -1;
            AppointmentDate = DateTime.Now;
            PaidFees = -1;
            CreatedByUserID = -1;
            isLocked = false;
        }
        private clsTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool isLocked)
        {
            _Mode = enMode.Update;
            this.TestAppointmentID = this.TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.isLocked = isLocked;
        }
        private bool _AddTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddTestAppointment(this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.isLocked);
            return this.TestAppointmentID != -1;
        }
        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID, this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.isLocked);
        }
        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:
                    {
                        _Mode = enMode.Update;
                        return _AddTestAppointment();
                    }
                    break;
                case enMode.Update:
                    {
                        return _UpdateTestAppointment();
                    }
                    break;
                default:
                    {
                        return false;
                    }

            }
        }
    }
}
