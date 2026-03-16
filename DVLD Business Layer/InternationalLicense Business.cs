using DVLD_Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsInternationalLicense : clsApplication
    {
        enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode;
        public int InternationalLicenseID { get; set; }
        //Application ID inhereted From Base Class.(clsApplication)
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        //CreatedByUserID inhereted From Base Class.(clsApplication)
        public clsInternationalLicense()
        {
            _Mode = enMode.AddNew;
            InternationalLicenseID = -1;
            DriverID = -1;
            IssuedUsingLocalLicenseID = -1;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            IsActive = true;
        }
        private clsInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, decimal PaidFees) : base(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
        {
            _Mode = enMode.Update;
            this.InternationalLicenseID = InternationalLicenseID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
        }

        private bool AddInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicenseData.AddInternationalLicense(ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            return this.InternationalLicenseID != -1;
        }
        private bool UpdateInternationalLicense()
        {
            return clsInternationalLicenseData.UpdateInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive);
        }
        public bool Save()
        {
            bool BaseSaved = base.Save();
            if (!BaseSaved)
            {
                return false;
            }
            switch (this._Mode)
            {
                case enMode.AddNew:
                    {
                        if (AddInternationalLicense())
                        {
                            this._Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    break;
                case enMode.Update:
                    {
                        return UpdateInternationalLicense();
                    }
                    break;
                default: return false;
            }
        }
        public static clsInternationalLicense GetInternationalLicenseByID(int InternationalLicenseID)
        {
            int ApplicationID = -1, DriverID = -1, IssuedUsingLocalLicenseID = -1, CreatedByUserID = -1, ApplicantPersonID = -1, ApplicationTypeID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now, ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            enApplicationStatus ApplicationStatus = enApplicationStatus.New;
            decimal PaidFees = 0;
            bool IsActive = true;

            if (clsInternationalLicenseData.GetInternationalLicense(InternationalLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                clsApplication Application = clsApplication.GetApplication(ApplicationID);
                if (Application != null)
                {
                    return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, Application.CreatedByUserID, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate, Application.PaidFees);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;

            }
        }

        public static bool HasInternationalLicense(int LicenseID)
        {
            return clsInternationalLicenseData.HasInternationalLicense(LicenseID);
        }
        public static DataTable GetInternationalLicenses(int DriverID)
        {
            return clsInternationalLicenseData.GetInternationalLicenses(DriverID);
        }
        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicenseData.GetAllInternationalLicenses();
        }
    }
}

