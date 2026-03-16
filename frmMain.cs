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
    public partial class frmMain : Form
    {
        public delegate void SignOutHandler(object sender,bool isSignedOut);
        public event SignOutHandler SignOutEvent;
        public frmMain()
        {
            InitializeComponent();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmManagePeople = new frmManagePeople();
            frmManagePeople.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SignOutEvent?.Invoke(this,true);
            this.Close();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmManageUsers= new frmManageUsers();
            frmManageUsers.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmChangePassword = new  frmChangePassword(clsGlobal.CurrentUser);
            frmChangePassword.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmUserDetail=new frmUserDetails(clsGlobal.CurrentUser);
            frmUserDetail.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmManageApplicationTypes = new frmManageApplicationsTypes();
            frmManageApplicationTypes.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmManageTestTypes = new frmManageTestTypes();
            frmManageTestTypes.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication NewLDLApp= new clsLocalDrivingLicenseApplication();
            Form frmNewLocalLicenseApplication = new frmAddUpdateLocalLicenseApplication(NewLDLApp);
            frmNewLocalLicenseApplication.ShowDialog();
        }

        private void localDrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmLocalLicenseApplications = new frmManageLoacalLicenseApplications();
            frmLocalLicenseApplications.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmDrivers=new frmDrivers();
            frmDrivers.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmAddInternationalLicense = new frmAddInternationalLicenseApplication();
            frmAddInternationalLicense.ShowDialog();
        }

        private void internationalDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmManageInternationalLicense = new frmManageInternationalLicense();
            frmManageInternationalLicense.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmRenewDrivingLicese = new frmRenewLicenseApplication();
            frmRenewDrivingLicese.ShowDialog();
        }

        private void replacementOfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmReplaceDamagedOrLostLicense = new frmReplacementForDamagedOrLostLicense();
            frmReplaceDamagedOrLostLicense.ShowDialog();
        }

        private void replacementOfToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form frmReplaceDamagedOrLostLicense = new frmReplacementForDamagedOrLostLicense();
            frmReplaceDamagedOrLostLicense.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmDetainLicense = new frmDetainLicense();
            frmDetainLicense.ShowDialog();
        }

        private void releaseDetaindLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmReleaseDetainedLicense = new frmReleaseDetainedLicense();
            frmReleaseDetainedLicense.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmManageDetianLicenses = new frmManageDetainedLicenses();
            frmManageDetianLicenses.ShowDialog();
        }
    }
}
