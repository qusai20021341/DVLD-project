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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text))
            {
                if (Login())
                {
                    Form frmMain = new frmMain();
                    (frmMain as frmMain).SignOutEvent += FrmLogin_SignOutEvent;
                    this.Hide();
                    frmMain.ShowDialog();
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtUserName.Text))
                {
                    errorProvider1.SetError(txtUserName, "UserName is required");
                }else
                {
                    errorProvider1.SetError(txtUserName, "");
                }
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    errorProvider1.SetError(txtPassword, "UserName is required");
                }
                else
                {
                    errorProvider1.SetError(txtPassword, "");
                }


            }
        }

        // Perform login validation
        private bool Login()
        {
            clsUser User = clsUser.GetUserByUserName(txtUserName.Text);

            if (User != null)
            {
                if (User.Password == txtPassword.Text)
                {
                    if (User.isActive)
                    {
                        clsGlobal.CurrentUser = User;
                        RememberMe();

                        return true;
                    }
                    else
                    {
                        MessageBox.Show("This User is NOT Active, Contact The Admin.");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Password is incorrect");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("UserName does not exist");
                return false;

            }
        }

        private void RememberMe()
        {
            if (chkRememberMe.Checked)
            {
                File.WriteAllText(
                    "RememberMe.txt",
                    txtUserName.Text + Environment.NewLine + txtPassword.Text);
            }
            else
            {
                txtPassword.Text = "";
                txtUserName.Text = "";
                if (File.Exists("RememberMe.txt"))
                {
                    File.Delete("RememberMe.txt");
                }
            }
        }

        private void LoadLoginInfo()
        {
            if (File.Exists("RememberMe.txt"))
            {
                string[] lines = File.ReadAllLines("RememberMe.txt");

                if (lines.Length >= 2)
                {
                    txtUserName.Text = lines[0];
                    txtPassword.Text = lines[1];
                    chkRememberMe.Checked = true;
                    return;
                }
                txtPassword.Text = "";
                txtUserName.Text = "";
            }

         
        }

        private void FrmLogin_SignOutEvent(object sender, bool isSignedOut)
        {
            if (isSignedOut)
            {
                this.Show();
                LoadLoginInfo();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            LoadLoginInfo();
        }
    }
}
