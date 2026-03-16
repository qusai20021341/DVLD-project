using DVLD_Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DVLD_Business_Layer
{
    public class clsLocalDrivingLicenseApplication:clsApplication
    {
        enum enMode { AddNew=0,Update=1}
        enMode _Mode;
        public int LocalDrivingLicenseApplicationID {  get; set; }
        //Application ID inhereted From Base Class.(clsApplication)
        public int LicenseClassID {  get; set; }
        public clsLocalDrivingLicenseApplication()
        {
            _Mode= enMode.AddNew;
            LocalDrivingLicenseApplicationID = -1;
            LicenseClassID = -1;
        }
        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int LicenseClassID, int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID): base( ApplicationID,  ApplicantPersonID,  ApplicationDate,  ApplicationTypeID,  ApplicationStatus,  LastStatusDate,  PaidFees,  CreatedByUserID)
        {
            _Mode = enMode.Update;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {

            this.LocalDrivingLicenseApplicationID= clsLocalDrivingLicenseApplicationsData.AddLocalDrivingLicenseApplication(this.ApplicationID,this.LicenseClassID);
            return this.LocalDrivingLicenseApplicationID != -1;
        }
        private bool _UpdateLocalDrivingLicenseApplicationID()
        {
            return clsLocalDrivingLicenseApplicationsData.UpdateLocalDrivingLicenseApplications(this.LocalDrivingLicenseApplicationID, this.ApplicationID, LicenseClassID);
        }

        public bool Save()
        {
            bool BaseSaved = base.Save();
            if(!BaseSaved)
            {
                return false;
            }
            switch(this._Mode)
            {
                case enMode.AddNew:
                {
                        if(_AddNewLocalDrivingLicenseApplication())
                        {
                            this._Mode= enMode.Update;
                            return true;
                        }else
                        {
                            return false;
                        }
                }break;
                case enMode.Update:
                {
                        return _UpdateLocalDrivingLicenseApplicationID();
                }break;
                default: return false;
            }
          
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationsData.GetAllLocalDrivingLicenseApplictions();
        }

        public static int isPersonHasActiveApplictionWithThisClass(string NationalNo,string LicenseClass)
        {
            return clsLocalDrivingLicenseApplicationsData.isPersonHasActiveApplictionWithThisClass(NationalNo, LicenseClass);
        }
        public static int GetApplicationIDByLDLAppID(int LDLAppID)
        {
            return clsLocalDrivingLicenseApplicationsData.GetApplicationIDbyLDLAppID(LDLAppID);
        }
        public static bool DeleteLCLApp(int LDLAppID)
        {
            int ApplicationID = GetApplicationIDByLDLAppID(LDLAppID);
            if (clsLocalDrivingLicenseApplicationsData.DeleteLocalDrivingLicenseApplication(LDLAppID) )
            {
                return DeleteApplication(ApplicationID);//base class Delete function.
            }
            else
            {
                return false;
            }
        }

        public static clsLocalDrivingLicenseApplication GetLDLApp(int LDLAppID)
        {
            int LicenseClassID = -1;
            int ApplicationID = -1;
            int ApplicantPersonID = -1;
            DateTime ApplicationDate=DateTime.Now;
            int ApplicationTypeID = -1;
            byte ApplicationStatus = (byte)enApplicationStatus.New;
            DateTime LastStatusDate = DateTime.Now;
            decimal PaidFees = -1;
            int CreatedByUserID = -1;
            if(clsLocalDrivingLicenseApplicationsData.GetLDLApp(LDLAppID,ref LicenseClassID,ref ApplicationID))
            {
                if(clsApplicationsData.GetApplication(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
                {
                    return new clsLocalDrivingLicenseApplication(LDLAppID, LicenseClassID, ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, (enApplicationStatus)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
                }
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            
        }

        public static int PassedTestsCount(int LDLAppID)
        {
            return clsLocalDrivingLicenseApplicationsData.PassedTestsCount(LDLAppID);
        }
        public static int isPersonHasCompletedApplictionWithThisClass(string NationalNo, string LicenseClass)
        {
            return clsLocalDrivingLicenseApplicationsData.isPersonHasCompletedApplictionWithThisClass(NationalNo, LicenseClass);
        }

    }
}
