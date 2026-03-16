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
    public partial class frmIssueDrivingLicense : Form
    {
        public delegate void OnIssued(bool isIssued);
        public event OnIssued OnIssue;
        private clsLocalDrivingLicenseApplication _LDLApp;
        public frmIssueDrivingLicense(clsLocalDrivingLicenseApplication LDLApp)
        {
            InitializeComponent();
            _LDLApp = LDLApp;
            LoadData(); 
        }
        private void LoadData()
        {
           ctrlApplicationDetails1.LDLApplication = _LDLApp;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            

            clsDriver Driver = clsDriver.GetDriverByPersonID(_LDLApp.ApplicantPersonID);

            if (Driver == null)
            {
                if (!clsDriver.IsPersonADriver(_LDLApp.ApplicantPersonID))
                {
                    Driver = new clsDriver();
                    Driver.PersonID = _LDLApp.ApplicantPersonID;
                    Driver.CreatedDate = DateTime.Now;
                    Driver.CreatedByUserID = _LDLApp.CreatedByUserID;
                    Driver.Save();
                }
                else
                {
                    Driver = clsDriver.GetDriverByPersonID(_LDLApp.ApplicantPersonID);
                }
            }

            {
                _LDLApp.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                _LDLApp.Save();
                clsLicense License = new clsLicense();
                License.ApplicationID = _LDLApp.ApplicationID;
                License.IssueDate = DateTime.Now;
                License.ExpirationDate = DateTime.Now.AddYears(clsLicenseClass.GetLicenseClassByID(_LDLApp.LicenseClassID).DefaultValidityLength);
                License.DriverID = Driver.DriverID;
                License.LicenseClass = _LDLApp.LicenseClassID;
                License.Notes = txtNotes.Text;
                License.PaidFees = _LDLApp.PaidFees;
                License.IsActive = true;
                License.IssueReasone = 1;
                License.CreatedByUserID = _LDLApp.CreatedByUserID;
                if (License.Save() )
                {
                   
                    MessageBox.Show($"License issued successfully with license id= {License.LicenseID}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnIssue?.Invoke(true);

                }
                else
                {
                    MessageBox.Show("Failed to issue license.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
