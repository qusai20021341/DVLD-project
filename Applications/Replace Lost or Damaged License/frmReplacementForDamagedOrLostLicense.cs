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
    public partial class frmReplacementForDamagedOrLostLicense : Form
    {
        private clsLicense _License;
        public frmReplacementForDamagedOrLostLicense()
        {
            InitializeComponent();
            ctrlDriverLicenseInfoWithFilter1.OnLicenseFound += CtrlDriverLicenseInfoWithFilter1_OnLicenseFound;
            LoadData();

        }
        private void LoadData()
        {
            rbDamagedLicnse.Checked = true;
            btnIssueReplacement.Enabled = false;
            lnkShowLicensesHistory.Enabled = false;
            lnkShowNewLicenseInfo.Enabled = false;
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
            if(rbDamagedLicnse.Checked)
            {
                lblApplicationFees.Text = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Replacement for a Damaged Driving License").ApplicationFees.ToString();
            }
            else if (rbLostLicense.Checked)
            {
                lblApplicationFees.Text = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Replacement for a Lost Driving License").ApplicationFees.ToString();
            }
        }
        private void CtrlDriverLicenseInfoWithFilter1_OnLicenseFound(object sender,  clsLicense license)
        {
            _License = license;
            lblOldLicenseID.Text = _License.LicenseID.ToString();
            if (!license.IsActive)
            {
                MessageBox.Show("Selected license is not active. Please check the license information.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }else if(license.ExpirationDate<DateTime.Now)
            {
                MessageBox.Show("Selected license is expired. Please check the license information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                lnkShowLicensesHistory.Enabled = true;
                btnIssueReplacement.Enabled = true;
            }
           
        }

        private void rbDamagedLicnse_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDamagedLicnse.Checked)
            {
                lblApplicationFees.Text = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Replacement for a Damaged Driving License").ApplicationFees.ToString();
            }else if (rbLostLicense.Checked)
            {
                lblApplicationFees.Text = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Replacement for a Lost Driving License").ApplicationFees.ToString();
            }
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            clsApplication ReplacementApplication = new clsApplication();
            if (rbDamagedLicnse.Checked)
            {
                ReplacementApplication.ApplicationTypeID = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Replacement for a Damaged Driving License").ApplicationTypeID;
            }
            else if (rbLostLicense.Checked)
            {
                ReplacementApplication.ApplicationTypeID = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Replacement for a Lost Driving License").ApplicationTypeID;
            }
            ReplacementApplication.ApplicantPersonID=clsDriver.GetDriver(_License.DriverID).PersonID;
            ReplacementApplication.ApplicationDate = DateTime.Now;
            ReplacementApplication.ApplicationStatus=clsApplication.enApplicationStatus.Completed;
            ReplacementApplication.LastStatusDate = DateTime.Now;
            if (rbDamagedLicnse.Checked)
            {
                ReplacementApplication.PaidFees = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Replacement for a Damaged Driving License").ApplicationFees;
            }
            else if (rbLostLicense.Checked)
            {
                ReplacementApplication.PaidFees = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Replacement for a Lost Driving License").ApplicationFees;
            }
            ReplacementApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            if(ReplacementApplication.Save())
            {
                clsLicense ReplacedLicense = new clsLicense();
                ReplacedLicense.ApplicationID = ReplacementApplication.ApplicationID;
                ReplacedLicense.DriverID = _License.DriverID;
                ReplacedLicense.LicenseClass= _License.LicenseClass;
                ReplacedLicense.IssueDate = DateTime.Now;
                ReplacedLicense.ExpirationDate = DateTime.Now.AddYears(clsLicenseClass.GetLicenseClassByID(_License.LicenseClass).DefaultValidityLength);
                ReplacedLicense.Notes = _License.Notes;
                ReplacedLicense.PaidFees=_License.PaidFees;
                ReplacedLicense.IsActive = true;
                if(rbDamagedLicnse.Checked)
                {
                    ReplacedLicense.IssueReasone = 3;
                }else if(rbLostLicense.Checked)
                {
                    ReplacedLicense.IssueReasone = 4;
                }
                ReplacedLicense.CreatedByUserID= clsGlobal.CurrentUser.UserID;
                if(ReplacedLicense.Save())
                {
                    clsLicense AciveLicense= clsLicense.GetDriverLicense(_License.DriverID);
                    if(AciveLicense != null)
                    {
                        AciveLicense.IsActive = false;
                        if (AciveLicense.Save())
                        {
                            lblLRApplicationID.Text = ReplacementApplication.ApplicationID.ToString();
                            lblReplacedLIcenseID.Text = ReplacedLicense.LicenseID.ToString();
                            lnkShowNewLicenseInfo.Enabled = true;
                            if (ReplacedLicense.IssueReasone == 3)
                            {
                                MessageBox.Show("New License Issued Successfully, Issue Reason for Damage","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            }
                            else if (ReplacedLicense.IssueReasone == 4)
                            {
                                MessageBox.Show("New License Issued Successfully, Issue Reason for Lost ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    
                }
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnkShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsLicense NewLicense = clsLicense.GetLicense(int.Parse(lblReplacedLIcenseID.Text));
            Form frmShowLicenseInfo = new frmLicense(NewLicense);
            frmShowLicenseInfo.ShowDialog();
        }

        private void lnkShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsLicense NewLicense = clsLicense.GetLicense(_License.LicenseID);
            Form frmShowLicenseHistory = new frmLicenseHistory(NewLicense);
            frmShowLicenseHistory.ShowDialog();
        }
    }
}
