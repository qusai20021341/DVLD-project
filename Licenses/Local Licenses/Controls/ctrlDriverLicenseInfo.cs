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
using System.IO;

namespace DVLD_project
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        private clsLicense _License;
        public clsLicense License
        {
            get { return _License; }
            set {

                _License = value;
                LoadLicenseInfo();

            }
        }
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        public ctrlDriverLicenseInfo(clsLicense License)
        {
            InitializeComponent();
            _License = License;
            LoadLicenseInfo();
        }
        private void LoadLicenseInfo()
        {
            if (_License != null)
            {
                clsDriver Driver = clsDriver.GetDriver(_License.DriverID);
                if (Driver != null)
                {
                    clsPerson Person = clsPerson.GetPersonByID(Driver.PersonID);
                    if (Person != null)
                    {

                        lblClass.Text = clsLicenseClass.GetLicenseClassByID(_License.LicenseClass).ClassName;
                        lblName.Text = Person.FullName;
                        lblLicenseId.Text = _License.LicenseID.ToString();
                        lblNationlNo.Text = Person.NationalNo;
                        if (clsPerson.GetPersonByID(Driver.PersonID).Gendor == 0)
                        {
                            lblGendor.Text = "Male";
                        }
                        else
                        {
                            lblGendor.Text = "Female";
                        }
                        lblIssueDate.Text = _License.IssueDate.ToShortDateString();
                        if (_License.IssueReasone == 1)
                        {
                            lblIssueReason.Text = "First Time";
                        }
                        else if(_License.IssueReasone == 2)
                        {
                            lblIssueReason.Text = "Renew";
                        }else if(_License.IssueReasone==3)
                        {
                            lblIssueReason.Text = "Replacement for Damage";
                        }
                        else if(_License.IssueReasone == 4)
                        {
                            lblIssueReason.Text = "Replacement for Lost";
                        }
                        lblNotes.Text = _License.Notes;
                        lblIsAcitve.Text = _License.IsActive ? "Yes" : "No";
                        lblDateOfBirth.Text = Person.DateOfBirth.ToShortDateString();
                        lblDriverID.Text = _License.DriverID.ToString();
                        lblExpirationDate.Text = _License.ExpirationDate.ToShortDateString();
                        if(clsDetaineLicense.isDetained(_License.LicenseID))
                        {
                            lblIsDetained.Text = "Yes";
                        }
                        else
                        {
                            lblIsDetained.Text = "No";

                        }
                        if (string.IsNullOrEmpty(Person.ImagePath) || !File.Exists(Person.ImagePath))
                        {
                            if (Person.Gendor == 0)
                                pbImage.Image = Properties.Resources.Male;
                            else
                                pbImage.Image = Properties.Resources.administrator_female;
                        }
                        else
                        {
                            pbImage.Image = Image.FromFile(Person.ImagePath);
                        }
                    }
                }
            }
        }
        private void ctrlDriverLicenseInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
