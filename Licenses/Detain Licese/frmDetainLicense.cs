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
    public partial class frmDetainLicense : Form
    {
        public delegate void DetainLicenseEventHandler(bool isDetained);
        public DetainLicenseEventHandler OnDetian;
        clsLicense _License;
        public frmDetainLicense()
        {
            InitializeComponent();
            ctrlDriverLicenseInfoWithFilter1.OnLicenseFound += LoadLicense;
            LoadData();
        }
        private void LoadData()
        {
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByID.Text = clsGlobal.CurrentUser.UserName;
            btnDetain.Enabled = false;
            lnkShowLicenseHistory.Enabled = false;
            lnkShowLicenseInfo.Enabled = false;
            txtFineFees.Enabled= false;
        }
        private void LoadLicense( object sender,clsLicense license)
        {
            _License = license;

            if(!clsDetaineLicense.isDetained(license.LicenseID))
            {
                btnDetain.Enabled = true;
                txtFineFees.Enabled = true;

            }
            else
            {
                MessageBox.Show("Selected License is already detained","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            lnkShowLicenseHistory.Enabled=true;
            lblLicenseID.Text=_License.LicenseID.ToString();
            



        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (!clsDetaineLicense.isDetained(_License.LicenseID))
            {


                clsDetaineLicense detaineLicense = new clsDetaineLicense();
                detaineLicense.LicenseID = _License.LicenseID;
                detaineLicense.DetainDate = DateTime.Now;
                detaineLicense.FineFees = decimal.Parse(txtFineFees.Text);
                detaineLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                detaineLicense.IsReleased = false;
                if (detaineLicense.Save())
                {
                    OnDetian?.Invoke(true);
                    MessageBox.Show("License Detained Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblDetainID.Text = detaineLicense.DetianID.ToString();
                    lnkShowLicenseInfo.Enabled = true;


                }
                else
                {
                    MessageBox.Show("Faild to Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Selected License is already detained", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnkShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frmLicensesHistory = new frmLicenseHistory(_License);
            frmLicensesHistory.ShowDialog();
        }

        private void lnkShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frmLicense=new frmLicense(_License);
            frmLicense.ShowDialog();
        }
    }
}
