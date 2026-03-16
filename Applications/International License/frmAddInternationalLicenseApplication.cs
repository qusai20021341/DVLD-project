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
    public partial class frmAddInternationalLicenseApplication : Form
    {
        public delegate void OnIssueEventHndler(bool isIssued);
        public event OnIssueEventHndler OnIssue;
        public frmAddInternationalLicenseApplication()
        {
            InitializeComponent();
            LoadInternationalLicenseApplicationData();
            ctrlDriverLicenseInfoWithFilter1.OnLicenseFound += CtrlDriverLicenseInfoWithFilter1_OnLicenseFound;
        }
        private void LoadInternationalLicenseApplicationData()
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblFees.Text=clsApplicationType.GetApplicationTypeByApplicationTypeTitle("New International License").ApplicationFees.ToString();
            lblCreatedBy.Text=clsGlobal.CurrentUser.UserName;
            lnkShowLicenseInfo.Enabled = false;
            lnkShowLicenseHistory.Enabled = false;
            btnIssue.Enabled = false;
            
        }
        private void CtrlDriverLicenseInfoWithFilter1_OnLicenseFound(object sender, clsLicense license)
        {
            btnIssue.Enabled = true;
            lblLocalLicenseID.Text = license.LicenseID.ToString();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            clsLicense License = ctrlDriverLicenseInfoWithFilter1.License;
            if (License!=null  && License.LicenseClass ==clsLicenseClass.GetLicenseClassByID(3).LicenseClassID)
            {
                if (clsInternationalLicense.HasInternationalLicense(License.LicenseID))
                {
                    MessageBox.Show("The driver have an international license.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if(License.IsActive != true)
                {
                    MessageBox.Show("The local license is not active.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if(License.ExpirationDate < DateTime.Now.Date)
                {
                    MessageBox.Show("The local license is expired.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                clsInternationalLicense newInternationalLicense = new clsInternationalLicense();
                newInternationalLicense.DriverID = License.DriverID;
                newInternationalLicense.IssuedUsingLocalLicenseID = License.LicenseID;
                newInternationalLicense.IssueDate = DateTime.Now.Date;
                newInternationalLicense.ExpirationDate = DateTime.Now.Date.AddYears(1);
                newInternationalLicense.IsActive = true;
                newInternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                newInternationalLicense.ApplicationTypeID = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("New International License").ApplicationTypeID;
                newInternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.New;
                newInternationalLicense.ApplicationDate = DateTime.Now.Date;
                newInternationalLicense.ApplicantPersonID=clsDriver.GetDriver(License.DriverID).PersonID;
                newInternationalLicense.LastStatusDate = DateTime.Now.Date;
                newInternationalLicense.PaidFees = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("New International License").ApplicationFees;
                if(newInternationalLicense.Save())
                {
                    OnIssue?.Invoke(true);
                    MessageBox.Show("International license application has been submitted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lnkShowLicenseHistory.Enabled = true;
                    lnkShowLicenseInfo.Enabled = true;
                    lblILApplicationID.Text = newInternationalLicense.ApplicationID.ToString();
                    lblILLicenseID.Text = newInternationalLicense.InternationalLicenseID.ToString();
                }




            }
            else
            {
                MessageBox.Show("The local license must be Ordinary Driving License .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
          
        }

        private void lnkShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsInternationalLicense internationalLicense = clsInternationalLicense.GetInternationalLicenseByID(int.Parse(lblILLicenseID.Text));
            if (internationalLicense != null)
            {
                frmDriverInterNationalLicenseInfo frm = new frmDriverInterNationalLicenseInfo(internationalLicense);
                frm.ShowDialog();
            }

        }

        private void lnkShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsLicense License=clsLicense.GetLicense(int.Parse(lblLocalLicenseID.Text));
            if(License != null)
            {
                Form frmLicenseHistory = new frmLicenseHistory(License);
                frmLicenseHistory.ShowDialog();
            }
            

        }
    }
}
