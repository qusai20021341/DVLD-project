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
    public partial class frmReleaseDetainedLicense : Form
    {
        public delegate void ReleaseLicenseEventHandler(bool isReleased);
        public ReleaseLicenseEventHandler OnRelease;
        private clsLicense _License;
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
            ctrlDriverLicenseInfoWithFilter1.OnLicenseFound +=LoadLicenseData ;
            LoadData();
        }
        public frmReleaseDetainedLicense(clsLicense license)
        {
            InitializeComponent();
            _License = license;
            ctrlDriverLicenseInfoWithFilter1.OnLicenseFound += LoadLicenseData;
            if(license!=null)
                ctrlDriverLicenseInfoWithFilter1.License= license;
            LoadData();
           
        }
        private void LoadData()
        {
            if(ctrlDriverLicenseInfoWithFilter1.License==null)
            {
                lnkShowLicensesHistory.Enabled = false;
                lnkShowLicenseInfo.Enabled = false;
                btnRelease.Enabled = false;
            }
            
        }
        private void LoadLicenseData(object sender, clsLicense license)
        {
            _License = license;
            lnkShowLicensesHistory.Enabled = true;
            if (!clsDetaineLicense.isDetained(_License.LicenseID))
            {
                MessageBox.Show("The selected license is not Detianed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btnRelease.Enabled=true;
                clsDetaineLicense Detian= clsDetaineLicense.GetDetainedLicense(_License.LicenseID);
                lblDetainID.Text = Detian.DetianID.ToString();
                lblLicenseID.Text = _License.LicenseID.ToString();
                lblDetainDate.Text = Detian.DetainDate.ToShortDateString();
                lblApplicationFees.Text = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Release Detained Driving Licsense").ApplicationFees.ToString();
                lblFineFees.Text = Detian.FineFees.ToString();
                lblTotalFees.Text= (clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Release Detained Driving Licsense").ApplicationFees+Detian.FineFees).ToString();
                lblCreatedBy.Text=clsUser.GetUserByUserID( Detian.CreatedByUserID).UserName;    


            }


        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (clsDetaineLicense.isDetained(_License.LicenseID))
            {


                clsApplication ReleaseApplication = new clsApplication();
                ReleaseApplication.ApplicantPersonID = clsDriver.GetDriver(_License.DriverID).PersonID;
                ReleaseApplication.ApplicationDate = DateTime.Now;
                ReleaseApplication.ApplicationTypeID = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Release Detained Driving Licsense").ApplicationTypeID;
                ReleaseApplication.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                ReleaseApplication.LastStatusDate = DateTime.Now;
                ReleaseApplication.PaidFees = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("Release Detained Driving Licsense").ApplicationFees;
                ReleaseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                if (ReleaseApplication.Save())
                {
                    clsDetaineLicense Detain = clsDetaineLicense.GetDetainedLicense(_License.LicenseID);
                    Detain.IsReleased = true;
                    Detain.ReleaseDate = DateTime.Now;
                    Detain.ReleasedByUserID = clsGlobal.CurrentUser.UserID;
                    Detain.ReleaseApplicationID = ReleaseApplication.ApplicationID;
                    if (Detain.Save())
                    {
                        OnRelease?.Invoke(true);
                        MessageBox.Show("Detained License Released Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblApplicationID.Text = ReleaseApplication.ApplicationID.ToString();
                        lnkShowLicenseInfo.Enabled = true;
                        btnRelease.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Fiald to save Release Detained License application!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("The selected license is not Detianed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }





        }

        private void lnkShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frmShowLicenseHistory = new frmLicenseHistory(_License);
            frmShowLicenseHistory.ShowDialog();
        }

        private void lnkShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frmShowLicenseInfo=new frmLicense(_License);
            frmShowLicenseInfo.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}
