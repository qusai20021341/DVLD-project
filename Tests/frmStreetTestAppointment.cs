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

    public partial class frmStreetTestAppointment : Form
    {
        public delegate void OnTakeTest(bool isTaken);
        public event OnTakeTest OnStreetTestTaken;
        private clsLocalDrivingLicenseApplication _LDLApp;

        public frmStreetTestAppointment(clsLocalDrivingLicenseApplication LDLApp)
        {
            InitializeComponent();
            _LDLApp = LDLApp;
        }
        private void LoadApplictionDetails()
        {
            ctrlApplicationDetails1.LDLApplication = _LDLApp;

            DataTable TestAppointments = clsAppointment.GetLDLTestAppointments(_LDLApp.LocalDrivingLicenseApplicationID, clsTestType.GetTestTypeIDByTestTypeTitle("Practical (Street) Test"));
            DataView TestAppointmentsView = new DataView(TestAppointments);
            dgvTestAppointments.DataSource = TestAppointmentsView;
            if (TestAppointmentsView.Count > 0)
            {

                dgvTestAppointments.Columns["TestAppointmentID"].HeaderText = "Appointment ID";
                dgvTestAppointments.Columns["AppointmentDate"].HeaderText = "Appointment Date";
                dgvTestAppointments.Columns["PaidFees"].HeaderText = "Paid Fees";
                dgvTestAppointments.Columns["IsLocked"].HeaderText = "Locked";
                dgvTestAppointments.Columns["TestTypeID"].Visible = false;
                dgvTestAppointments.Columns["LocalDrivingLicenseApplicationID"].Visible = false;
                dgvTestAppointments.Columns["CreatedByUserID"].Visible = false;
                dgvTestAppointments.Columns["RetakeTestApplicationID"].Visible = false;


            }
            lblNumberOfAppointments.Text = TestAppointmentsView.Count.ToString();

        }

        private void frmStreetTestAppointment_Load(object sender, EventArgs e)
        {
            LoadApplictionDetails();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            int writtenTestTypeID =
             clsTestType.GetTestTypeIDByTestTypeTitle("Practical (Street) Test");

            clsAppointment lastAppointment =
                clsAppointment.GetAppointmentByLDLAppIDAndTestTypeID(
                    _LDLApp.LocalDrivingLicenseApplicationID,
                    writtenTestTypeID);

            if (lastAppointment == null)
            {
                clsAppointment newAppointment = new clsAppointment();
                newAppointment.LDLAppID = _LDLApp.LocalDrivingLicenseApplicationID;

                Form frmScheduleStreetTest = new frmScheduleStreetTest(newAppointment);
                ((frmScheduleStreetTest)frmScheduleStreetTest).OnSave += RefreshAppointments;

                frmScheduleStreetTest.ShowDialog();
                return;
            }

            if (clsAppointment.HasActiveAppointment(
                    _LDLApp.LocalDrivingLicenseApplicationID,
                    writtenTestTypeID))
            {
                MessageBox.Show("There is already an active appointment.",
                    "Active Appointment Exists",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (clsTest.isFaildTest(lastAppointment.TestAppointmentID))
            {
                clsAppointment newAppointment = new clsAppointment();
                newAppointment.LDLAppID = _LDLApp.LocalDrivingLicenseApplicationID;
                newAppointment.RetakeTestApplicationID = 0;

                Form frmScheduleStreetTest = new frmScheduleStreetTest(newAppointment);
                ((frmScheduleStreetTest)frmScheduleStreetTest).OnSave += RefreshAppointments;

                frmScheduleStreetTest.ShowDialog();
            }
            else
            {

                MessageBox.Show("Person already passed this test.",
                    "Test Already Passed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void RefreshAppointments(bool isSaved)
        {
            if (isSaved)
            {
                OnStreetTestTaken?.Invoke(true);
                LoadApplictionDetails();
            }
        }

        private void dgvTestAppointments_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvTestAppointments.ClearSelection();
                dgvTestAppointments.Rows[e.RowIndex].Selected = true;
                dgvTestAppointments.CurrentCell = dgvTestAppointments.Rows[e.RowIndex].Cells[0];
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvTestAppointments.SelectedRows[0].Cells["TestAppointmentID"].Value;
            clsAppointment appointment = clsAppointment.GetAppointmentByID(TestAppointmentID);
            if (appointment != null)
            {
                Form frmScheduleStreetTest = new frmScheduleStreetTest(appointment);
                ((frmScheduleStreetTest)frmScheduleStreetTest).OnSave += RefreshAppointments;
                frmScheduleStreetTest.ShowDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvTestAppointments.SelectedRows[0].Cells["TestAppointmentID"].Value;

            clsAppointment appointment = clsAppointment.GetAppointmentByID(TestAppointmentID);

            if (appointment.IsLocked == true)
            {
                MessageBox.Show("This appointment is locked, You can not take the test. ", "Appointment Locked", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Form frmTakeStreetTest = new frmTakeStreetTest(appointment);
                ((frmTakeStreetTest)frmTakeStreetTest).OnTestTaken += RefreshAppointments;
                frmTakeStreetTest.ShowDialog();
            }
        }
    }
}
