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
    public partial class frmChangePassword : Form
    {
        public delegate void PasswordChanded(clsUser User);
        public event PasswordChanded OnPasswordChanged;
        public clsUser _User { get; set; }
        public frmChangePassword(clsUser User)
        {
            InitializeComponent();
            _User = User;
            LoadUserData();
        }

        private void LoadUserData()
        {
            ctrlPersonDetails1.Person = clsPerson.GetPersonByID(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;
            if (_User.isActive)
            {
                lblIsActive.Text = "Yes";
            }
            else
            {
                lblIsActive.Text = "No";
            }
        }

        private bool CheckPassword()
        {
            if (_User.Password != txtCurrentPassword.Text)
            {
                MessageBox.Show("Current password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                if (txtNewPassword.Text != txtConfirmPassWord.Text)
                {
                    MessageBox.Show("New password and confirm password do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        private bool isAllFielsEmpty()
        {
            return string.IsNullOrEmpty(txtCurrentPassword.Text) ||
                   string.IsNullOrEmpty(txtNewPassword.Text) ||
                   string.IsNullOrEmpty(txtConfirmPassWord.Text);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!isAllFielsEmpty())
            {


                if (CheckPassword())
                {
                    _User.Password = txtNewPassword.Text;
                    if (_User.Save())
                    {
                        OnPasswordChanged?.Invoke(_User);
                        MessageBox.Show("Password changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to change password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
