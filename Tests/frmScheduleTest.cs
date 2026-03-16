using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_project
{
    public partial class frmScheduleTest : Form
    {
        public delegate void OnSaveHandler(bool isSaved);
        public event OnSaveHandler OnSave;
        private clsAppointment _Appointment;
     
        public frmScheduleTest(clsAppointment Appointment)
        {
            _Appointment = Appointment;
            InitializeComponent();
            LoadAppointmentData();
        }
     
        private void LoadAppointmentData()
        {
            clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication.GetLDLApp(_Appointment.LDLAppID);
            lblDLAppID.Text = LDLApp.ApplicationID.ToString();
            lblDClass.Text = clsLicenseClass.GetLicenseClassByID(LDLApp.LicenseClassID).ClassName;
            lblName.Text = clsPerson.GetPersonByID(LDLApp.ApplicantPersonID).FullName;
            int TestTypeID = clsTestType.GetTestTypeIDByTestTypeTitle("Vision Test");
            lblTrail.Text = clsAppointment.NumberOfTrails(_Appointment.LDLAppID, TestTypeID).ToString();
            lblFees.Text = clsTestType.GetTestTypeByTestTypeTitle("Vision Test").TestTypeFees.ToString();
            
            lblTotalFees.Text = clsTestType.GetTestTypeByTestTypeTitle("Vision Test").TestTypeFees.ToString();
            lblRTestAppFees.Text = "N/A";
            if (_Appointment.RetakeTestApplicationID==-1)
            {
                gbRetakeTestInfo.Enabled = false;
                lblRAppFees.Text = 0.ToString();

            }
            else
            {
                gbRetakeTestInfo.Enabled = true;
                lblSchedule.Text = "Schedule Retake Test ";
                lblRAppFees.Text = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Retake Test").ApplicationFees.ToString();
                lblTotalFees.Text= (clsTestType.GetTestTypeByTestTypeTitle("Vision Test").TestTypeFees + clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Retake Test").ApplicationFees).ToString();
            }
            if (_Appointment.TestAppointmentID == -1)
            {
                dtpAppointmentDate.Value = DateTime.Now;

            }
            else
            {
                dtpAppointmentDate.Value = _Appointment.AppointmentDate;
            }

            if (!clsTest.isTestTaken(_Appointment.TestAppointmentID))
            {
                lblTestTakenMessage.Visible= false;
            }
            else
            {
                lblTestTakenMessage.Visible= true;
                dtpAppointmentDate.Enabled = false;
                btnSave.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication.GetLDLApp(_Appointment.LDLAppID);
            if (_Appointment.TestAppointmentID == -1)
            {
                _Appointment.TestTypeID = clsTestType.GetTestTypeIDByTestTypeTitle("Vision Test");
                _Appointment.LDLAppID = LDLApp.LocalDrivingLicenseApplicationID;
                _Appointment.AppointmentDate = dtpAppointmentDate.Value;
                _Appointment.PaidFees = clsTestType.GetTestTypeByTestTypeTitle("Vision Test").TestTypeFees;
                _Appointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                _Appointment.IsLocked = false;
                clsApplication RetakeApplication=new clsApplication();
                if (_Appointment.RetakeTestApplicationID != -1)
                {
                    RetakeApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
                    RetakeApplication.ApplicationTypeID = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Retake Test").ApplicationTypeID;
                    RetakeApplication.ApplicationDate = DateTime.Now;
                    RetakeApplication.ApplicantPersonID = LDLApp.ApplicantPersonID;
                    RetakeApplication.LastStatusDate = DateTime.Now;
                    RetakeApplication.PaidFees=clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Retake Test").ApplicationFees;
                    RetakeApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                    if (RetakeApplication.Save())
                    {
                        //MessageBox.Show("Retake application created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _Appointment.RetakeTestApplicationID = RetakeApplication.ApplicationID;
                    }
                    else
                    {
                        MessageBox.Show("Failed to create retake application. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }
                if (_Appointment.Save())
                {
                    MessageBox.Show("Test appointment scheduled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnSave?.Invoke(true);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to schedule test appointment. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                _Appointment.AppointmentDate = dtpAppointmentDate.Value;
                if (_Appointment.Save())
                {
                    MessageBox.Show("Test appointment Updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnSave?.Invoke(true);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to Update test appointment. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
