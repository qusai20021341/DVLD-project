using DVLD_Business_Layer;
using DVLD_project.Properties;
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
    public partial class frmAddUpdatePerson : Form
    {
       public delegate void SavePersonHandler(int PersonID);
       public event SavePersonHandler OnPersonSaved;
        //Person object to hold the data
        private clsPerson _Person;
       
        //reseve the object with constructor and Fill _Person and set the form mode
        public frmAddUpdatePerson(clsPerson Person)
        {
            InitializeComponent();
            _Person = Person;
            _LoadCountries();
            

                if (_Person.PersonID == -1)
                {
                    _AddNewPersonMode();
                }
                else
                {
                    _UpdatePersonMode();
                }
                SetValidDateOfBirth();
            
        }
        //Load Countries into the Countries combobox
        private void _LoadCountries()
        { 
            DataTable CountriesDataTable=clsCountry.GetAllCountires();
            cbCountry.DataSource = CountriesDataTable;
            cbCountry.DisplayMember = "CountryName";
            cbCountry.ValueMember = "CountryID";
            cbCountry.SelectedIndex = cbCountry.FindStringExact("Jordan");

        }
        // Set the form to Add New Person mode
        private void _AddNewPersonMode()
        {
            lblControlMode.Text = "Add New Person";
            lblPerosnID.Text = "N/A";
            this.Text = "Add New Person";
            rbMale.Checked = true;
            LoadImage();
        }
        // Set the form to Update Person mode and fill the fields with the person's data
        private void _UpdatePersonMode()
        {
            lblControlMode.Text = "Update Person";
            lblPerosnID.Text = _Person.PersonID.ToString();
            this.Text = "Update Person";
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            if (_Person.Gendor == 0)
            {
                rbMale.Checked = true;
            }
            else if(_Person.Gendor == 1)
            {
                rbFemale.Checked = true;
            }
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            cbCountry.SelectedValue = _Person.CountryID;
            txtAddress.Text = _Person.Address;
            LoadImage();

        }
        //Load the person's image if available
        private void LoadImage()
        {
            if(!string.IsNullOrWhiteSpace(_Person.ImagePath))
            {
                pbImage.Image = Image.FromFile(_Person.ImagePath);
                lnkRemoveImage.Visible = true;
            }
            else
            {
                lnkRemoveImage.Visible = false;
            }
        }
        //Event handler for radio button checked change to update image
        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(_Person.ImagePath))
                pbImage.Image = Resources.Male;

        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_Person.ImagePath))
                pbImage.Image = Resources.administrator_female;


        }
        //Event handler for Close label click to close the form
        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Event handler for Save button click to save the person's data
        private void btnSave_Click(object sender, EventArgs e)
        {
            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.NationalNo = txtNationalNo.Text;
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            if (rbMale.Checked)
            {
                _Person.Gendor = 0;
            }
            else if (rbFemale.Checked)
            {
                _Person.Gendor = 1;
            }
            _Person.Phone = txtPhone.Text;
            _Person.Email = txtEmail.Text;
            _Person.CountryID = (int)cbCountry.SelectedValue;
            _Person.Address = txtAddress.Text;
            if (isAllRequiredFeildsFilled())
            {


                if (_Person.PersonID == -1)
                {
                    //Add New Person
                    if (_Person.Save())
                    {
                        MessageBox.Show("Person added successfully.");
                        _UpdatePersonMode();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add person.");
                    }
                }
                else
                {
                    //Update Person
                    if (_Person.Save())
                    {
                        MessageBox.Show("Person updated successfully.");
                        _UpdatePersonMode();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update person.");
                    }
                }
                OnPersonSaved?.Invoke(_Person.PersonID);
            }
            else
            {
                errorProvider1.SetError(btnSave, "Please fill all required fields.");
            }


        }
        //Event handler for Set Image link click to set the person's image
        private void lnkSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(ofdPersonImage.ShowDialog() == DialogResult.OK)
            {
                string imagePath = ofdPersonImage.FileName;
                pbImage.Image= Image.FromFile(imagePath);
                _Person.ImagePath = imagePath;
                lnkRemoveImage.Visible = true;

            }
        }
        //Event handler for Remove Image link click to remove the person's image
        private void lnkRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbImage.Image= null;
            _Person.ImagePath = "";
            lnkRemoveImage.Visible = false;
            if(rbFemale.Checked)
            {
                pbImage.Image = Resources.administrator_female;
            }
            else
            {
                pbImage.Image = Resources.Male;
            }
        }

        //Set Valid Date of Birth (at least 18 years old)
        private void SetValidDateOfBirth()
        {
            dtpDateOfBirth.MaxDate = DateTime.Today.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;
        }
        //Check if all required fields are filled
        private bool isAllRequiredFeildsFilled()
        {
            return !string.IsNullOrWhiteSpace(txtFirstName.Text) &&
                   !string.IsNullOrWhiteSpace(txtSecondName.Text) &&
                   !string.IsNullOrWhiteSpace(txtLastName.Text) &&
                   !string.IsNullOrWhiteSpace(txtNationalNo.Text) &&
                   (rbMale.Checked || rbFemale.Checked) &&
                   !string.IsNullOrWhiteSpace(txtPhone.Text) &&
                   !string.IsNullOrWhiteSpace(txtAddress.Text);
        }

        //Validating Feilds

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                e.Cancel = true;
                txtFirstName.Focus();
                errorProvider1.SetError(txtFirstName, "First Name is required.");
            }
            else
            {
                e.Cancel= false;
                errorProvider1.SetError(txtFirstName, "");
            }
        }

        private void txtSecondName_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtSecondName.Text))
            {
                e.Cancel = true;
                txtSecondName.Focus();
                errorProvider1.SetError(txtSecondName, "Second Name is required.");
            }
            else
            {
                e.Cancel= false;
                errorProvider1.SetError(txtSecondName, "");
            }
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                e.Cancel = true;
                txtLastName.Focus();
                errorProvider1.SetError(txtLastName, "Last Name is required.");
            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtNationalNo.Text))
            {
                e.Cancel = true;
                txtNationalNo.Focus();
                errorProvider1.SetError(txtNationalNo, "National No is required.");
                
            }
            else
            {
                if (clsPerson.isPersonExistByNationalNo(txtNationalNo.Text))
                {
                    e.Cancel = true;
                    txtNationalNo.Focus();
                    errorProvider1.SetError(txtNationalNo, "National No already exists.");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtNationalNo, "");
                }
            }
        }

        private void dtpDateOfBirth_Validating(object sender, CancelEventArgs e)
        {
            if (dtpDateOfBirth.Value > DateTime.Today.AddYears(-18))
            {
                e.Cancel = true;
                dtpDateOfBirth.Focus();
                errorProvider1.SetError(dtpDateOfBirth, "Person must be at least 18 years old.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(dtpDateOfBirth, "");
            }
        }

        private void gbGendor_Validating(object sender, CancelEventArgs e)
        {
            if(!rbFemale.Checked && !rbMale.Checked)
            {
                errorProvider1.SetError(gbGendor, "You must select a gendor.");
            }
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                e.Cancel = true;
                txtPhone.Focus();
                errorProvider1.SetError(txtPhone, "Phone is required.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPhone, "");
            }
        }

        private void cbCountry_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                e.Cancel = true;
                txtAddress.Focus();
                errorProvider1.SetError(txtAddress, "Address is required.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtAddress, "");
            }
        }

        private bool isValidEmail(string EmailText)
        {
            if(EmailText.Contains("@") && EmailText.Contains("."))
                return true;
            else return false;
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEmail.Text)  )
            {
                if (!isValidEmail(txtEmail.Text))
                {
                    e.Cancel = true;
                    txtEmail.Focus();
                    errorProvider1.SetError(txtEmail, "Invalid email format.");
                }
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, "");
            }
        }

        
    }
}
