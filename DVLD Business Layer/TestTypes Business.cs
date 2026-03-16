using DVLD_Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsTestType
    {
        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }

        private clsTestType(int TestTypeID,string TestTypeTitle,string TestTypeDescription,decimal TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAllTestTypes();
        }
        public static bool UpdateTestType(int TestTypeID,string TestTypeTitle,string TestTypeDescription,decimal TestypeFees)
        {
            return clsTestTypesData.UpdateTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestypeFees);
        }

        public static clsTestType GetTestTypeByID(int TestTypeID)
        {
            string TestTypeTitle = "", TestTypeDescription = "";
            decimal TestTypeFees = 0;
            if(clsTestTypesData.GetTestTypeByID(TestTypeID,ref TestTypeTitle,ref TestTypeDescription,ref TestTypeFees))
            {
                return new clsTestType(TestTypeID,TestTypeTitle,TestTypeDescription,TestTypeFees);
            }
            else
            {
                return null;
            }
        }
        public static clsTestType GetTestTypeByTestTypeTitle(string TestTypeTitle)
        {
            int TestTypeID = -1;
            string TestTypeDescription = "";
            decimal TestTypeFees = 0;
            if (clsTestTypesData.GetTestTypeByTestTypeTitle(TestTypeTitle, ref TestTypeID, ref TestTypeDescription, ref TestTypeFees))
            {
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            }
            else
            {
                return null;
            }
        }


        public static int GetTestTypeIDByTestTypeTitle(string TestTypeTitle)
        {
            return clsTestTypesData.GetTestTypeIDByTestTypeTitle(TestTypeTitle);
        }



    }
}
