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
    public partial class frmVisionTestAppointment : Form
    {
        public delegate void OnTestTaken(bool isTaken);
        public event OnTestTaken OnTakeTest;
        private clsLocalDrivingLicenseApplication _LDLApp;
        public frmVisionTestAppointment(clsLocalDrivingLicenseApplication LDLApp)
        {
            InitializeComponent();
            _LDLApp = LDLApp;
        }
        private void LoadApplictionDetails()
        {
            ctrlApplicationDetails1.LDLApplication = _LDLApp;
            
            DataTable TestAppointments= clsAppointment.GetLDLTestAppointments(_LDLApp.LocalDrivingLicenseApplicationID, clsTestType.GetTestTypeIDByTestTypeTitle("Vision Test"));
            DataView TestAppointmentsView = new DataView(TestAppointments);
            dgvTestAppointments.DataSource = TestAppointmentsView;
            if(TestAppointmentsView.Count > 0 )
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
            lblNumberOfAppointments.Text= TestAppointmentsView.Count.ToString();

        }

        private void frmVisionTestAppointment_Load(object sender, EventArgs e)
        {
            LoadApplictionDetails();

        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            if (!clsAppointment.HasAppointments(_LDLApp.LocalDrivingLicenseApplicationID))
            {
                clsAppointment NewAppointment = new clsAppointment();
                NewAppointment.LDLAppID = _LDLApp.LocalDrivingLicenseApplicationID;
                Form frmShceduleTest = new frmScheduleTest(NewAppointment);
                ((frmScheduleTest)frmShceduleTest).OnSave += RefreshAppointments;
                frmShceduleTest.ShowDialog();
            }
            else
            {
                if (clsAppointment.HasActiveAppointment(_LDLApp.LocalDrivingLicenseApplicationID, clsTestType.GetTestTypeByTestTypeTitle("Vision Test").TestTypeID))
                {
                    MessageBox.Show("Person Already have an active appointment for this test, You can not Add new Appointment. ", "Active Appointment Exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (clsTest.isFaildTest(clsAppointment.GetAppointmentByLDLAppIDAndTestTypeID(_LDLApp.LocalDrivingLicenseApplicationID, clsTestType.GetTestTypeIDByTestTypeTitle("Vision Test")).TestAppointmentID))
                    {
                        clsAppointment NewAppointment = new clsAppointment();
                        NewAppointment.LDLAppID = _LDLApp.LocalDrivingLicenseApplicationID;
                        NewAppointment.RetakeTestApplicationID = 0;
                        Form frmShceduleTest = new frmScheduleTest(NewAppointment);
                        ((frmScheduleTest)frmShceduleTest).OnSave += RefreshAppointments;
                        frmShceduleTest.ShowDialog();

                    }
                    else
                    {
                        MessageBox.Show("Person already passed this test, You can not Add new Appointment. ", "Test Already Passed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
               
            }

        }
        private void RefreshAppointments(bool isSaved)
        {
            if(isSaved)
            {
                OnTakeTest?.Invoke(true);
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
                Form frmShceduleTest = new frmScheduleTest(appointment);
                ((frmScheduleTest)frmShceduleTest).OnSave += RefreshAppointments;
                frmShceduleTest.ShowDialog();
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

            if(appointment.IsLocked==true)
            {
                MessageBox.Show("This appointment is locked, You can not take the test. ", "Appointment Locked", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                 Form frmTakeTest = new frmTakeTest(appointment);
                ((frmTakeTest)frmTakeTest).OnTestTaken += RefreshAppointments;
                frmTakeTest.ShowDialog();
            }
           
        }
    }
}
