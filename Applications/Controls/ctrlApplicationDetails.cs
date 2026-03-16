using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_project
{
    public partial class ctrlApplicationDetails : UserControl
    {
        private int PassedTestsCount=0;
        private clsLocalDrivingLicenseApplication _LDLApplication;
        public clsLocalDrivingLicenseApplication LDLApplication
        {
            get { return _LDLApplication; }
            set
            {
                _LDLApplication = value;

                if (!this.DesignMode && _LDLApplication != null)
                {
                    LoadApplicationDetails();
                }
            }
        }

       
        public ctrlApplicationDetails()
        {
            InitializeComponent();
        }
        private int CountPassedTest()
        {
            
            clsAppointment Appointment =clsAppointment.GetAppointmentByLDLAppIDAndTestTypeID(LDLApplication.LocalDrivingLicenseApplicationID, clsTestType.GetTestTypeIDByTestTypeTitle("Vision Test") );
            if (Appointment != null && clsTest.isPassedTest(Appointment.TestAppointmentID))
            {
                PassedTestsCount++;
            }
           return PassedTestsCount;
        }
        private void LoadApplicationDetails()
        {
            lblLDLAppID.Text = LDLApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedForLicense.Text = clsLicenseClass.GetLicenseClassByID(LDLApplication.LicenseClassID).ClassName;
            //Passed tests to be implemented.
            lblPassedTests.Text = clsLocalDrivingLicenseApplication.PassedTestsCount(_LDLApplication.LocalDrivingLicenseApplicationID).ToString();
            lblDate.Text=LDLApplication.ApplicationDate.ToShortDateString();
            lblApplicationID.Text = LDLApplication.ApplicationID.ToString();
            lblStatus.Text= LDLApplication.ApplicationStatus.ToString();
            lblFees.Text=clsLicenseClass.GetLicenseClassByID(LDLApplication.LicenseClassID).ClassFees.ToString();
            lblApplicationType.Text=clsApplicationType.GetApplicationTypeByID(LDLApplication.ApplicationTypeID).ApplicationTypeTitle;
            lblApplicant.Text = clsPerson.GetPersonByID(LDLApplication.ApplicantPersonID).FullName;
            lblStatusDate.Text= LDLApplication.LastStatusDate.ToShortDateString();
            lblCreatedBy.Text=clsUser.GetUserByUserID(LDLApplication.CreatedByUserID).UserName;

        }

        private void lnkViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsPerson Person=clsPerson.GetPersonByID(LDLApplication.ApplicantPersonID);
            Form frmPersonDetails = new frmPersonDetails(Person);
            frmPersonDetails.ShowDialog();
        }
    }
}
