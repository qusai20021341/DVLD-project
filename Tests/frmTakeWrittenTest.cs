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
    public partial class frmTakeWrittenTest : Form
    {
        public delegate void OnTestTakenHandler(bool isTaken);
        public event OnTestTakenHandler OnTestTaken;
        private clsAppointment _Appointment;
        public frmTakeWrittenTest(clsAppointment Appointment)
        {
            InitializeComponent();
            _Appointment = Appointment;
            LoadAppointmentData();
        }
        private void LoadAppointmentData()
        {
            lblDLAppID.Text = _Appointment.LDLAppID.ToString();
            lblDClass.Text = clsLicenseClass.GetLicenseClassByID(clsLocalDrivingLicenseApplication.GetLDLApp(_Appointment.LDLAppID).LicenseClassID).ClassName;
            lblName.Text = clsPerson.GetPersonByID(clsLocalDrivingLicenseApplication.GetLDLApp(_Appointment.LDLAppID).ApplicantPersonID).FullName;
            int TestTypeID = clsTestType.GetTestTypeIDByTestTypeTitle("Written (Theory) Test");
            lblTrail.Text = clsAppointment.NumberOfTrails(_Appointment.LDLAppID, TestTypeID).ToString();
            lblDate.Text = _Appointment.AppointmentDate.ToString("yyyy-MM-dd");
            lblFees.Text = _Appointment.PaidFees.ToString();
            lblTestID.Text = "Not Taken Yet";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsTest Test = new clsTest();
            Test.TestAppointmentID = _Appointment.TestAppointmentID;
            Test.TestResult = rbPass.Checked;

            Test.Notes = txtNotes.Text.Trim();

            Test.CreatedByUserID = _Appointment.CreatedByUserID;
            if (Test.Save())
            {
                _Appointment.IsLocked = true;
                if (_Appointment.Save())
                {
                    MessageBox.Show("Test Result Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnTestTaken?.Invoke(true);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Failed to Save Test Result", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
