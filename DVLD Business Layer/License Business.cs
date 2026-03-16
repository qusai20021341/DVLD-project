using DVLD_Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsLicense
    {
        enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode;

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClass { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public byte IssueReasone { get; set; }
        public int CreatedByUserID { get; set; }

        public clsLicense()
        {
            _Mode = enMode.AddNew;
            LicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            LicenseClass = -1;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            Notes = "";
            PaidFees = 0;
            IsActive = false;
            IssueReasone = 0;
            CreatedByUserID = -1;
        }

        private clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate,
            DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReasone, int CreatedByUserID)
        {
            _Mode = enMode.Update;
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReasone = IssueReasone;
            this.CreatedByUserID = CreatedByUserID;
        }

        private bool _AddNewLicense()
        {

            this.LicenseID = clsLicenseData.AddLicense(ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReasone, CreatedByUserID);
            return this.LicenseID != -1;
        }
        private bool _UpdateLicense()
        {
            return clsLicenseData.UpdateLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReasone);
        }
        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:
                    {
                        this._Mode = enMode.Update;
                        return _AddNewLicense();


                    }
                    break;
                case enMode.Update:
                    {
                        return _UpdateLicense();
                    }
                    break;
                default:
                    return false;
            }
        }
        public static bool DeleteLicese(int LicenseID)
        {
            return clsLicenseData.DeleteLicense(LicenseID);
        }
        public static clsLicense GetLicenseByApplicationID(int ApplicationID)
        {
            int LicenseID = -1, DriverID = -1, LicenseClass = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0;
            bool IsActive = false;
            byte IssueReasone = 0;
            int CreatedByUserID = -1;

            bool found = clsLicenseData.GetLicenseByApplicationID(ApplicationID, ref LicenseID, ref DriverID,
                ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees,
                ref IsActive, ref IssueReasone, ref CreatedByUserID);

            if (found)
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate,
                    ExpirationDate, Notes, PaidFees, IsActive, IssueReasone, CreatedByUserID);
            }

            return null;
        }
        public static DataTable GetPersonLicenses(int PersonID)
        {
            return clsLicenseData.GetAllPersonLicenses(PersonID);
        }
        public static clsLicense GetLicense(int LicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0;
            bool IsActive = false;
            byte IssueReasone = 0;
            int CreatedByUserID = -1;
            if (clsLicenseData.GetLicenseByID(LicenseID,ref ApplicationID,ref DriverID,ref LicenseClass,ref IssueDate,ref ExpirationDate,ref Notes,ref PaidFees,ref IsActive,ref IssueReasone))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReasone, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }
        public static clsLicense GetDriverLicense(int DriverID)
        {
            int LicenseID = -1;
            int ApplicationID = -1;
            int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0;
            bool IsActive = false;
            byte IssueReasone = 0;
            int CreatedByUserID = -1;
            if(clsLicenseData.GetDriverLicense(DriverID,ref ApplicationID,ref LicenseID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReasone))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReasone, CreatedByUserID);
            }
            else
            {
                return null;
            }

        }
    }
}
