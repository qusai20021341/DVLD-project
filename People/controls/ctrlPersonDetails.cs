using DVLD_Business_Layer;
using DVLD_project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_project
{
    public partial class ctrlPersonDetails : UserControl
    {   // Event to notify when person details are updated
        public event Action <bool> OnPersonUpdated;
        protected virtual void OnPersonUpdatedViaShowDetails(bool isSaved)
        {
            Action<bool> handler = OnPersonUpdated;
            if (handler != null)
            {
                handler(isSaved);
            }
        }
        private clsPerson _person;
        public clsPerson Person
        {
            get { return _person; }
            set
            {
                _person = value;
                LoadPersonDetails(); 
            }
        }
        public ctrlPersonDetails()
        {
            InitializeComponent();
            lnkEditPersonInfo.Visible = false; 
        }
        // Load Person Details
        private void LoadPersonDetails()
        {
            if (Person != null)
            {
                lblPersonID.Text = Person.PersonID.ToString();
                lblFullName.Text = Person.FullName;
                lblNationalNo.Text = Person.NationalNo;
                if (Person.Gendor == 0)
                {
                    lblGendor.Text = "Male";
                    pbGendorImage.Image = Resources.Male;
                }
                else
                {
                    lblGendor.Text = "Female";
                    pbGendorImage.Image = Resources.administrator_female;
                }
                lblDateOfBirth.Text = Person.DateOfBirth.ToShortDateString();
                lblAddress.Text = Person.Address;
                lblPhone.Text = Person.Phone;
                lblEmail.Text = Person.Email;
                lblCountry.Text = clsCountry.GetCountryByID(Person.CountryID).CountryName;
                if (!string.IsNullOrWhiteSpace(Person.ImagePath) && File.Exists(Person.ImagePath))
                {
                    pbImage.Image = Image.FromFile(Person.ImagePath);
                }
                SetImage(Person.ImagePath);
                lnkEditPersonInfo.Visible = true;
            }
        }
        private void SetImage(string imagePath)
        {
            if (!string.IsNullOrWhiteSpace(imagePath) && File.Exists(imagePath))
            {
                pbImage.Image = Image.FromFile(imagePath);
            }
            else
            {
                if(Person.Gendor==0)
                {
                    pbImage.Image= Resources.Male;
                }
                else
                {
                    pbImage.Image = Resources.administrator_female;
                }
            }
        }
        // On Load Event
        private void ctrlPersonDetails_Load(object sender, EventArgs e)
        {
            LoadPersonDetails();
        }

        private void lnkEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frmUpdatePersonInfo = new frmAddUpdatePerson(Person);
            ((frmAddUpdatePerson)frmUpdatePersonInfo).OnPersonSaved += PersonUpdated;
            frmUpdatePersonInfo.ShowDialog();
        }
        // Person Updated Event
        private void PersonUpdated(int PersonID)
        {
            if (PersonID>-1)
            {
                LoadPersonDetails();
                if(OnPersonUpdated != null)
                    OnPersonUpdatedViaShowDetails(true);

            }
        }
    }
}


