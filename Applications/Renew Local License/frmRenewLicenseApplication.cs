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
    public partial class frmRenewLicenseApplication : Form
    {
        private clsLicense _License;
        public frmRenewLicenseApplication()
        {
            InitializeComponent();
            ctrlDriverLicenseInfoWithFilter1.OnLicenseFound += CtrlDriverLicenseInfoWithFilter1_OnLicenseFound;
            LoadData();
        }
        private void LoadData()
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text=DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
            lblApplicationFees.Text = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Renew Driving License Service").ApplicationFees.ToString();
            btnRenew.Enabled = false;
            lnkShowLicenseHistory.Enabled = false;
            lnkShowNewLicenseInfo.Enabled = false;
        }
        private void CtrlDriverLicenseInfoWithFilter1_OnLicenseFound(object sender, clsLicense license)
        {
            _License = license;
            lnkShowLicenseHistory.Enabled = true;
            lblLicenseFees.Text = license.PaidFees.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears( clsLicenseClass.GetLicenseClassByID(license.LicenseClass).DefaultValidityLength).ToShortDateString();
            lblTotalFees.Text = (clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Renew Driving License Service").ApplicationFees + license.PaidFees).ToString();
            lblOldLicenseID.Text = license.LicenseID.ToString();
            if (_License.ExpirationDate > DateTime.Now)
            {
                MessageBox.Show("This license is not expired yet. You cannot proceed with the renewal application.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
               if(license.IsActive)
                {
                    btnRenew.Enabled = true;

                }
                else
                {
                    MessageBox.Show("This license is not active. You cannot proceed with the renewal application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            clsLicense license = clsLicense.GetLicense(_License.LicenseID);
            clsApplication RenewApplication = new clsApplication();
            RenewApplication.ApplicationTypeID = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Renew Driving License Service").ApplicationTypeID;
            RenewApplication.ApplicationDate = DateTime.Now;
            RenewApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            RenewApplication.ApplicantPersonID = clsDriver.GetDriver(license.DriverID).PersonID;
            RenewApplication.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            RenewApplication.PaidFees = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Renew Driving License Service").ApplicationFees;
            RenewApplication.LastStatusDate = DateTime.Now;
            if (RenewApplication.Save())
            {
                clsLicense NewLicense = new clsLicense();
                NewLicense.DriverID = license.DriverID;
                NewLicense.ApplicationID = RenewApplication.ApplicationID;
                NewLicense.LicenseClass = license.LicenseClass;
                NewLicense.IssueDate = DateTime.Now;
                NewLicense.ExpirationDate = DateTime.Now.AddYears(clsLicenseClass.GetLicenseClassByID(license.LicenseClass).DefaultValidityLength);
                NewLicense.Notes = txtNotes.Text;
                NewLicense.PaidFees = license.PaidFees + clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Renew Driving License Service").ApplicationFees;
                NewLicense.IsActive = true;
                NewLicense.IssueReasone = 2;//Renewal
                NewLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                if (NewLicense.Save())
                {
                    license.IsActive = false;
                    if (license.Save())
                    {
                        MessageBox.Show("License renewed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lnkShowNewLicenseInfo.Enabled = true;
                        lblRLApplication.Text = RenewApplication.ApplicationID.ToString();
                        lblRenewedLicenseID.Text = NewLicense.LicenseID.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Failed to create new license. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Failed to create renewal application. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnkShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frmShowDetails = new frmLicense(clsLicense.GetLicense(int.Parse(lblRenewedLicenseID.Text)));
            frmShowDetails.ShowDialog();
        }

        private void lnkShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frmShowLicenseHistory = new frmLicenseHistory(_License);
            frmShowLicenseHistory.ShowDialog();
        }
    }
}
