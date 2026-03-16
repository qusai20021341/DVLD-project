using DVLD_Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsDetaineLicense
    {
        enum enMode { AddNew=0,Update=1};
        enMode _Mode;
        public int DetianID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? ReleasedByUserID { get; set; }
        public int? ReleaseApplicationID { get; set; }
        public clsDetaineLicense()
        {
            _Mode = enMode.AddNew;
            DetianID = -1;
            LicenseID=-1;
            DetainDate = DateTime.Now;
            FineFees = 0;
            CreatedByUserID = -1;
            IsReleased = false;
            this.ReleaseDate = null;
            this.ReleasedByUserID = null;
            this.ReleaseApplicationID = null;

        }
        public clsDetaineLicense(int DetainID, int LicenseID,DateTime DetinDate,decimal FineFees,int CreatedByUserID,bool IsReleased)
        {
            _Mode = enMode.Update;
            this.DetianID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetinDate;
            this.FineFees=FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased= IsReleased;
            this.ReleaseDate = null;
            this.ReleasedByUserID = null;
            this.ReleaseApplicationID = null;
        }
        private bool AddDetainLicense()
        {
            this.DetianID = clsDetaineLicenseData.AddDetainedLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
            return this.DetianID != -1;
        }
        private bool UpdateDetainLicense()
        {
            return clsDetaineLicenseData.UpdateDetainedLicense(this.DetianID,this.LicenseID,this.DetainDate,this.FineFees,this.CreatedByUserID,this.IsReleased,this.ReleaseDate,this.ReleasedByUserID,this.ReleaseApplicationID);
        }
        public bool Save()
        {
            switch(this._Mode)
            {
                case enMode.AddNew:
                    {
                        if(AddDetainLicense())
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }  break;
                case enMode.Update:
                    {
                        if(UpdateDetainLicense())
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }break;
                default:
                    return false;
                    break;
            }
        }
        public static bool isDetained(int LicenseID)
        {
            return clsDetaineLicenseData.IsDetianed(LicenseID);
        }
        public static clsDetaineLicense GetDetainedLicense(int LicenceID)
        {
            int DetainID = -1,CreatedByUserID=-1;
            DateTime DetainDate = DateTime.Now;
            decimal FineFees= 0;
            bool IsReleased = false;
            DateTime? ReleaseDate = null;
            int? ReleasedByUserID = null;
            int? ReleaseApplicationID = null;
            if(clsDetaineLicenseData.GetDetainLicense(LicenceID,ref DetainID,ref DetainDate,ref FineFees,ref CreatedByUserID,ref IsReleased,ref ReleaseDate,ref ReleasedByUserID,ref ReleaseApplicationID))
            {
                return new  clsDetaineLicense(DetainID, LicenceID, DetainDate, FineFees, CreatedByUserID, IsReleased);
            }
            else
            {
                return null;    
            }
        }
        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetaineLicenseData.GetAllDetainedLicenses();
        }





    }
}
