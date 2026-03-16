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
    public partial class frmAddUpdateLocalLicenseApplication : Form
    {
        public delegate void OnSaveEventHndler(bool isSaved);
        public event OnSaveEventHndler OnSave;
        private clsLocalDrivingLicenseApplication _LDLApp;
        public frmAddUpdateLocalLicenseApplication(clsLocalDrivingLicenseApplication LDLApp)
        {
            InitializeComponent();
            _LDLApp = LDLApp;
            FillApplicationData();
            FillLicenseClasses();
        }
        private void FillLicenseClasses()
        {
            DataTable LicenseClasses = clsLicenseClass.GetAllLicenseClasses();
            cbLicenseClass.DataSource =LicenseClasses ;
            cbLicenseClass.DisplayMember = "ClassName";
            cbLicenseClass.ValueMember = "LicenseClassID";
           
            
        }
        private void FillApplicationData()
        {
            if (_LDLApp.LocalDrivingLicenseApplicationID == -1)
            {
                _AddNewMode();
            }
            else
            {
                _UpdateMode();
            }

        }
        private void _AddNewMode()
        {
            lblFormMode.Text = "New Local License Application";
            lblApplicationDate.Text = DateTime.Now.ToString();
            lblApplicationFees.Text = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("New Local Driving License Service").ApplicationFees.ToString();
            lblCreatedByUserName.Text = clsGlobal.CurrentUser.UserName;
        }
        private void _UpdateMode()
        {
            lblFormMode.Text = "Update Local License Application";
            ctrlPersonInfoWithFilter1.Person=clsPerson.GetPersonByID(_LDLApp.ApplicantPersonID);
            lblApplicationID.Text = _LDLApp.ApplicationID.ToString();
            lblApplicationDate.Text = _LDLApp.ApplicationDate.ToString();
            cbLicenseClass.SelectedValue = _LDLApp.LicenseClassID;
            lblApplicationFees.Text = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("New Local Driving License Service").ApplicationFees.ToString();
            lblCreatedByUserName.Text = clsUser.GetUserByUserID(_LDLApp.CreatedByUserID).UserName;

        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if(ctrlPersonInfoWithFilter1.Person==null)
            {
                MessageBox.Show("Select a Person to continue L.D.L Applicatoin Steps.","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                tcNewLocalLicenseApplication.SelectedIndex = 1;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsApplicationType ApplicationType = clsApplicationType.GetApplicationTypeByApplicationTypeTitle("New Local Driving License Service");

            if (clsLocalDrivingLicenseApplication.isPersonHasCompletedApplictionWithThisClass(ctrlPersonInfoWithFilter1.Person.NationalNo, cbLicenseClass.SelectedValue.ToString()) ==-1)
            {


                if (_LDLApp.LocalDrivingLicenseApplicationID == -1)
                {
                    _LDLApp.LicenseClassID = (int)cbLicenseClass.SelectedValue;
                    _LDLApp.ApplicantPersonID = ctrlPersonInfoWithFilter1.Person.PersonID;
                    _LDLApp.ApplicationDate = DateTime.Now;
                    _LDLApp.ApplicationTypeID = ApplicationType.ApplicationTypeID;
                    _LDLApp.ApplicationStatus = clsApplication.enApplicationStatus.New;
                    _LDLApp.LastStatusDate = DateTime.Now;
                    _LDLApp.PaidFees = ApplicationType.ApplicationFees;
                    _LDLApp.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                    int ActiveApplicationIDWithThisClassForThisPerson = clsLocalDrivingLicenseApplication.isPersonHasActiveApplictionWithThisClass(ctrlPersonInfoWithFilter1.Person.NationalNo, cbLicenseClass.SelectedValue.ToString());
                    if (ActiveApplicationIDWithThisClassForThisPerson == -1)
                    {
                        if (_LDLApp.Save())
                        {
                            MessageBox.Show("Local Driving License Appliction Added Successfully");
                            OnSave?.Invoke(true);
                            _UpdateMode();

                        }
                        else
                        {
                            MessageBox.Show("Fiald to add Local Driving License Application!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Choose another License Class,the selected Person Already have active application for the selected class with id ={ActiveApplicationIDWithThisClassForThisPerson}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    _LDLApp.LicenseClassID = (int)cbLicenseClass.SelectedValue;
                    _LDLApp.ApplicantPersonID = ctrlPersonInfoWithFilter1.Person.PersonID;
                    _LDLApp.LastStatusDate = DateTime.Now;
                    int ActiveApplicationIDWithThisClassForThisPerson = clsLocalDrivingLicenseApplication.isPersonHasActiveApplictionWithThisClass(ctrlPersonInfoWithFilter1.Person.NationalNo, cbLicenseClass.SelectedValue.ToString());
                    if (ActiveApplicationIDWithThisClassForThisPerson == -1)
                    {


                        if (_LDLApp.Save())
                        {
                            MessageBox.Show("Local Driving License Appliction Updated  Successfully");
                            OnSave?.Invoke(true);


                        }
                        else
                        {
                            MessageBox.Show("Fiald to Update Local Driving License Application!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show($"Choose another License Class,the selected Person Already have active application for the selected class with id ={ActiveApplicationIDWithThisClassForThisPerson}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Person alrady has license with the same applied driving class, Choose differente driving class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
