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
    public partial class frmLicenseHistory : Form
    {
        private clsLicense _License; 
        public frmLicenseHistory(clsLicense License)
        {
            InitializeComponent();
            _License = License;
            LoadLicenseHistory();
        }
        private void LoadLicenseHistory()
        {
            ctrlDriverLicenses1.License = _License;
            clsPerson person=clsPerson.GetPersonByID(clsDriver.GetDriver( _License.DriverID).PersonID);
            if(person != null)
            {
                ctrlPersonDetails1.Person =person;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
