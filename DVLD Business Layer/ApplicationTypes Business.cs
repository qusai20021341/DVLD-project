using DVLD_Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsApplicationType
    {
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationFees { get; set; }

        private clsApplicationType(int ApplicationID,string ApplicationTypeTitle,decimal ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
        }
        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }


        public static bool UpdateApplicationType(int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationFees)
        {
            return clsApplicationTypeData.UpdateUplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
        }
        public static clsApplicationType GetApplicationTypeByID(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = "";
            decimal ApplicationFees = -1;
            if(clsApplicationTypeData.GetApplicationTypeByID(ApplicationTypeID,ref ApplicationTypeTitle,ref ApplicationFees))
            {
                return new clsApplicationType(ApplicationTypeID,ApplicationTypeTitle,ApplicationFees);
            }
            else
            {
                return null;
            }
        }
        public static clsApplicationType GetApplicationTypeByApplicationTypeTitle(string ApplicationTypeTitle)
        {
            int ApplicationTypeID = -1;
            decimal ApplicationFees = -1;
            if(clsApplicationTypeData.GetApplicationTypeByApplicationTypeTitle(ApplicationTypeTitle,ref ApplicationTypeID,ref ApplicationFees))
            {
                return new clsApplicationType(ApplicationTypeID,ApplicationTypeTitle,ApplicationFees);
            }
            else
            {
                return null;
            }
        }
    }
}
