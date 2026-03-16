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
    public partial class frmDriverInterNationalLicenseInfo : Form
    {
        public frmDriverInterNationalLicenseInfo(clsInternationalLicense internationalLicense)
        {
            InitializeComponent();
            ctrlInternationalLicenseInfo1.InternationalLicense = internationalLicense;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}
