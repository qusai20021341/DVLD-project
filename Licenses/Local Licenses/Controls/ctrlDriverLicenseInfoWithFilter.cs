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
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        public delegate void LicenseFoundEventHandler(object sender, clsLicense license);
        public LicenseFoundEventHandler OnLicenseFound;
        private clsLicense _License;
        public clsLicense License
        {
            get { return _License; }
            set
            {
                _License = value;
                if(_License !=null)
                {
                    ctrlDriverLicenseInfo1.License = _License;
                    OnLicenseFound?.Invoke(this, _License);
                }
            }
        }

        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private void btnFindButton_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtLicenseID.Text) && int.TryParse(txtLicenseID.Text,out int LicenseID))
            {
                License = clsLicense.GetLicense(LicenseID);
                if(License != null)
                {
                    ctrlDriverLicenseInfo1.License = License;
                    OnLicenseFound?.Invoke(this, License);
                }
                else
                {
                    MessageBox.Show("License not found.");
                }

            }
            else
            {
                MessageBox.Show("Please enter a valid License ID.");
            }
        }
    }
}
