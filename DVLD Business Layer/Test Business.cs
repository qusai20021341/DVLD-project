using DVLD_Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsTest
    {
        enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode;
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }


        public clsTest()
        {
            _Mode = enMode.AddNew;
            TestID = -1;
            TestAppointmentID = -1;
            TestResult = false;
            Notes = "";
            CreatedByUserID = -1;
        }
        private clsTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            _Mode = enMode.Update;
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
        }
        private bool _AddTest()
        {
            this.TestID = clsTestsData.AddTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
            return this.TestID != -1;
        }
        private bool _UpdateTest()
        {
            return clsTestsData.UpdateTest(this.TestID, this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
        }
        public bool Save()
        {
            if (_Mode == enMode.AddNew)
            {
                if (_AddTest())
                {
                    _Mode = enMode.Update;
                    return true;
                }
                return false;
            }
            else
            {
                return _UpdateTest();
            }
        }
        public static bool isPassedTest(int TestAppointmentID)
        {
            return clsTestsData.isPassTest(TestAppointmentID);
        }
        public static bool isTestTaken(int TestAppointmentID)
        {
            return clsTestsData.isTestTaken(TestAppointmentID);
        }
        public static bool isFaildTest(int TestAppointmentsID)
        {
            return clsTestsData.isFaildTest(TestAppointmentsID);
        }
       

    }
}
