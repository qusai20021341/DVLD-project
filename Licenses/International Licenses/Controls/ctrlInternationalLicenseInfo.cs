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
    public partial class ctrlInternationalLicenseInfo : UserControl
    {
        private clsInternationalLicense _InternationalLicense;
        public clsInternationalLicense InternationalLicense
        {
            get { return _InternationalLicense; }
            set
            {
                _InternationalLicense = value;
                if (_InternationalLicense != null)
                {
                    LoadInternationalLicenseInfo();
                }

            }
        }
        public ctrlInternationalLicenseInfo()
        {
            InitializeComponent();
        }
        public ctrlInternationalLicenseInfo(clsInternationalLicense InternationalLicense)
        {
            InitializeComponent();
            _InternationalLicense = InternationalLicense;
            LoadInternationalLicenseInfo();
        }
        private void LoadInternationalLicenseInfo()
        {
            clsLicense LocalLicense = clsLicense.GetLicense(_InternationalLicense.IssuedUsingLocalLicenseID);
            if(LocalLicense != null )
            {
                clsDriver Driver = clsDriver.GetDriver(LocalLicense.DriverID);
                if(Driver != null)
                {
                    clsPerson Person = clsPerson.GetPersonByID(Driver.PersonID);
                    if(Person != null)
                    {
                        lblName.Text = Person.FullName;
                        lblIntLicenseID.Text=_InternationalLicense.InternationalLicenseID.ToString();
                        lblLicenseID.Text = LocalLicense.LicenseID.ToString();
                        lblNationalNo.Text = Person.NationalNo;
                        if(Person.Gendor==0)
                        {
                            lblGendor.Text = "Male";
                        }
                        else
                        {
                            lblGendor.Text = "Female";
                        }
                        lblIssueDate.Text=_InternationalLicense.IssueDate.ToString();
                        lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
                        if(_InternationalLicense.IsActive==true)
                        {
                            lblIsActive.Text = "Yes";
                        }
                        else
                        {
                            lblIsActive.Text = "No";
                        }
                        lblDateOfBirth.Text = Person.DateOfBirth.ToShortDateString();
                        lblDriverID.Text = Driver.DriverID.ToString();
                        lblExpirationDate.Text= _InternationalLicense.ExpirationDate.ToShortDateString();
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

    }
}
