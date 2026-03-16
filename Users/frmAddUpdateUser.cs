using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_project
{
    public partial class frmAddUpdateUser : Form
    {
        public delegate void UserSavedHandler(clsUser User);
        public event UserSavedHandler UserSaved;
        public frmAddUpdateUser(clsUser User)
        {
            InitializeComponent();
            _User = User;
            if (_User.UserID==-1)
            {
                AddUserMode();
            }
            else
            {
                UpdateUserMode();
            }

        }
        public clsUser _User { get; set; }

        private void AddUserMode()
        {
            lblFormMode.Text = "Add New User";
            lblUserID.Text = "N/A";
        }
        private void UpdateUserMode()
        {
            lblFormMode.Text = "Update User";
            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.isActive;

            ctrlPersonInfoWithFilter1.Person = clsPerson.GetPersonByID(_User.PersonID);
        }

        

        private void btnNext_Click(object sender, EventArgs e)
        {  
            if (ctrlPersonInfoWithFilter1.Person == null)
            {
                MessageBox.Show("Please find or add a person before proceeding.");
            }
            else
            {
                if(_User.UserID==-1)
                {
                    if(clsUser.isUserExistByPersonID(ctrlPersonInfoWithFilter1.Person.PersonID))
                    {
                        MessageBox.Show("Thes Person is Already an User");
                    }
                    else
                    {
                        tcAddNewUser.SelectedIndex = 1;

                    }
                }
                else
                {
                    tcAddNewUser.SelectedIndex = 1;
                }
                
            }
        }       
        

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Username is required.");
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password is required.");
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Please confirm the password.");
            }
            else if (txtConfirmPassword.Text != txtPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Passwords do not match.");
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtUserName.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text) && !string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {

                _User.PersonID = ctrlPersonInfoWithFilter1.Person.PersonID;
                _User.UserName = txtUserName.Text;
                _User.Password = txtPassword.Text;
                _User.isActive = chkIsActive.Checked;
                if (_User.UserID == -1)
                {
                    if (_User.Save())
                    {
                        MessageBox.Show("User saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateUserMode();
                        UserSaved?.Invoke(_User);
                    }
                    else
                    {
                        MessageBox.Show("An error occurred while saving the user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (_User.Save())
                    {
                        MessageBox.Show("User Updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateUserMode();
                        UserSaved?.Invoke(_User);
                    }
                    else
                    {
                        MessageBox.Show("An error occurred while saving the user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }



            }
            else
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
