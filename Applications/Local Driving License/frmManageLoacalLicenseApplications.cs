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
    public partial class frmManageLoacalLicenseApplications : Form
    {
        private DataView _LDLAppsView;
        public frmManageLoacalLicenseApplications()
        {
            InitializeComponent();
            LoadLocalDrivingLicenseApplications();
            cbFilterBy.SelectedIndex = 0;
            txtSearch.Visible = false; 


        }
        
        
        private void LoadLocalDrivingLicenseApplications()
        {
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
            DataTable LocalDrivingLicenseApplications= clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
             _LDLAppsView = new DataView( LocalDrivingLicenseApplications);
            dgvLDLApps.DataSource= _LDLAppsView;
            lblNumberOfRecords.Text= _LDLAppsView.Count.ToString();
            dgvLDLApps.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvLDLApps.Columns[0].HeaderText = "L.D.LAppID";
            dgvLDLApps.Columns[0].FillWeight = 8;
            dgvLDLApps.Columns[1].HeaderText = "Driving Class";
            dgvLDLApps.Columns[1].FillWeight = 22;
            dgvLDLApps.Columns[2].HeaderText = "National No.";
            dgvLDLApps.Columns[2].FillWeight = 10;

            dgvLDLApps.Columns[3].HeaderText = "Full Name";
            dgvLDLApps.Columns[3].FillWeight = 28;

            dgvLDLApps.Columns[4].HeaderText = "Application Date";
            dgvLDLApps.Columns[4].FillWeight = 12;
            dgvLDLApps.Columns[4].DefaultCellStyle.Format = "yyyy-MM-dd";

            dgvLDLApps.Columns[5].HeaderText = "Passed Tests";
            dgvLDLApps.Columns[5].FillWeight = 8;

            dgvLDLApps.Columns[6].HeaderText = "Status";
            dgvLDLApps.Columns[6].FillWeight = 12;






        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddLDLApp_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication NewLDLApp=new clsLocalDrivingLicenseApplication();
            Form frmAddUpdateLocalLicenseApplication= new frmAddUpdateLocalLicenseApplication (NewLDLApp);
            ((frmAddUpdateLocalLicenseApplication)frmAddUpdateLocalLicenseApplication).OnSave += RefreshLDLAppsData;
            frmAddUpdateLocalLicenseApplication.ShowDialog ();
        }
        private void RefreshLDLAppsData(bool isSaved)
        {
            if(isSaved)
            {
                LoadLocalDrivingLicenseApplications();
            }

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cbFilterBy.SelectedIndex)
            {
                case 0:
                    txtSearch.Visible=false;
                    break;
                case 1:
                    txtSearch.Visible = true;
                    _LDLAppsView.Sort = "LocalDrivingLicenseApplicationID";
                    break;
                case 2:
                    txtSearch.Visible = true;
                    _LDLAppsView.Sort = "NationalNo";
                    break;
                case 3:
                    txtSearch.Visible = true;
                    _LDLAppsView.Sort = "FullName";
                    break;
                case 4:
                    txtSearch.Visible = true;
                    _LDLAppsView.Sort = "Status";
                    break;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtSearch.Text))
            {
                LoadLocalDrivingLicenseApplications();
                return;
            }
            string text = txtSearch.Text.Replace("'", "''");

            switch (cbFilterBy.SelectedIndex)
            {
                case 1:
                    if(int.TryParse(text,out int ID))
                    {
                        _LDLAppsView.RowFilter = $"LocalDrivingLicenseApplicationID = {ID} ";
                    }
                    break;
                case 2:
                    _LDLAppsView.RowFilter = $"NationalNo like '%{text}%' ";
                    break;
                case 3:
                    _LDLAppsView.RowFilter = $"FullName like '%{text}%' ";
                    break;
                case 4:
                    _LDLAppsView.RowFilter = $"Status like '%{text}%' ";
                    break;
            }
        }

        private void dgvLDLApps_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvLDLApps.ClearSelection();
                dgvLDLApps.Rows[e.RowIndex].Selected = true;
                dgvLDLApps.CurrentCell = dgvLDLApps.Rows[e.RowIndex].Cells[0];
            }
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure do you want to cancel this applicaton?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
            {
                int LDLAppID = (int)dgvLDLApps.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value;
                int ApplicationID=clsLocalDrivingLicenseApplication.GetApplicationIDByLDLAppID(LDLAppID);
                if(clsApplication.CnacelApplicaiton(ApplicationID))
                {
                    MessageBox.Show("Application Cancelled Successfully", "Cnacelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadLocalDrivingLicenseApplications();
                }

            }
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = (int)dgvLDLApps.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value;
            if(MessageBox.Show("Are you sure you want to delete this Local Driving License Application ?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(clsLocalDrivingLicenseApplication.DeleteLCLApp(LDLAppID))
                {
                    MessageBox.Show("Local Driving License Deleted Successfully","Success",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadLocalDrivingLicenseApplications();
                }
                else
                {
                    MessageBox.Show("Fiald to Delete Local Driving License Application","Faild",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void schedleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = (int)dgvLDLApps.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value;
            clsLocalDrivingLicenseApplication LDLApp= clsLocalDrivingLicenseApplication.GetLDLApp(LDLAppID);
            Form frmVisionTestAppointments = new  frmVisionTestAppointment(LDLApp);
            ((frmVisionTestAppointment)frmVisionTestAppointments).OnTakeTest += RefreshLDLAppsData;
            frmVisionTestAppointments.ShowDialog();
        }
        
       

       

        private void cmsLDLApp_Opening(object sender, CancelEventArgs e)
        {
            int LDLAppID = (int)dgvLDLApps.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value;
            clsLocalDrivingLicenseApplication LDLApp=clsLocalDrivingLicenseApplication.GetLDLApp(LDLAppID);
            int PassedTestsCount = clsLocalDrivingLicenseApplication.PassedTestsCount(LDLAppID);
            showLicenseToolStripMenuItem.Enabled = false;
            switch (PassedTestsCount)
            {
                case 0:
                    scheduleTestsToolStripMenuItem.Enabled = true;
                    schedleVisionTestToolStripMenuItem.Enabled = true;
                    scheduleWrittenTestToolStripMenuItem.Enabled = false;
                    scheduleStreetTestToolStripMenuItem.Enabled = false;
                    break;
                case 1:
                    scheduleTestsToolStripMenuItem.Enabled = true;
                    schedleVisionTestToolStripMenuItem.Enabled = false;
                    scheduleWrittenTestToolStripMenuItem.Enabled = true;
                    scheduleStreetTestToolStripMenuItem.Enabled = false;
                    break;
                case 2:
                    scheduleTestsToolStripMenuItem.Enabled = true;
                    schedleVisionTestToolStripMenuItem.Enabled = false;
                    scheduleWrittenTestToolStripMenuItem.Enabled = false;
                    scheduleStreetTestToolStripMenuItem.Enabled = true;
                    break;
                case 3:
                    scheduleTestsToolStripMenuItem.Enabled = false;
                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
                    break;
            }
            switch (LDLApp.ApplicationStatus)
            {
                case clsLocalDrivingLicenseApplication.enApplicationStatus.Canceled:
                    {
                        cmsLDLApp.Enabled = false;
                    }
                    break;
                case clsLocalDrivingLicenseApplication.enApplicationStatus.Completed:
                    {
                        cmsLDLApp.Enabled = true;
                        showApplicationDetailsToolStripMenuItem.Enabled = true;
                        editApplicationToolStripMenuItem.Enabled = false;
                        cancelApplicationToolStripMenuItem.Enabled = false;
                        deleteApplicationToolStripMenuItem.Enabled = false;
                        scheduleTestsToolStripMenuItem.Enabled = false;
                        issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                        showLicenseToolStripMenuItem.Enabled = true;
                    }
                    break;
                case clsLocalDrivingLicenseApplication.enApplicationStatus.New:
                    {
                        cmsLDLApp.Enabled = true;
                        showApplicationDetailsToolStripMenuItem.Enabled = true;
                        editApplicationToolStripMenuItem.Enabled = true;
                        cancelApplicationToolStripMenuItem.Enabled = true;
                        deleteApplicationToolStripMenuItem.Enabled = true;
                    }
                    break;
                    
            }
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = (int)dgvLDLApps.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value;
            clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication.GetLDLApp(LDLAppID);
            Form frmWrittenTestAppointments = new frmWrittenTestAppointment(LDLApp);
            ((frmWrittenTestAppointment)frmWrittenTestAppointments).OnWrittenTestTaken += RefreshLDLAppsData;
            frmWrittenTestAppointments.ShowDialog();

        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = (int)dgvLDLApps.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value;
            clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication.GetLDLApp(LDLAppID);
            Form frmStreetTestAppointments = new frmStreetTestAppointment(LDLApp);
            ((frmStreetTestAppointment)frmStreetTestAppointments).OnStreetTestTaken += RefreshLDLAppsData;
            frmStreetTestAppointments.ShowDialog();
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = (int)dgvLDLApps.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value;
            clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication.GetLDLApp(LDLAppID);
            Form frmIssueDrivingLicense=new frmIssueDrivingLicense(LDLApp);
            ((frmIssueDrivingLicense)frmIssueDrivingLicense).OnIssue += RefreshLDLAppsData;
            frmIssueDrivingLicense.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = (int)dgvLDLApps.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value;
            clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication.GetLDLApp(LDLAppID);
           

            clsLicense License = clsLicense.GetLicenseByApplicationID(LDLApp.ApplicationID);
            if (License == null)
            {
                MessageBox.Show($"No license found for this application.{LDLApp.ApplicationID.ToString()}");
                return;
            }
            clsLicense ActiveLicense=clsLicense.GetDriverLicense(License.DriverID);
            Form frmShowDrivingLicense = new frmLicense(ActiveLicense);
            frmShowDrivingLicense.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            int LDLAppID = (int)dgvLDLApps.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value;
            clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication.GetLDLApp(LDLAppID);

            if (LDLApp != null)
            {
                clsDriver Driver =clsDriver.GetDriverByPersonID(LDLApp.ApplicantPersonID);
                if(Driver  !=null)
                {
                    clsLicense License = clsLicense.GetDriverLicense(Driver.DriverID);
                    Form frmLicenseHistory = new frmLicenseHistory(License);
                    frmLicenseHistory.ShowDialog();
                }
               
            }
            
        }
    }
}
