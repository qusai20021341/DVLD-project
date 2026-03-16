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
    public partial class frmManageUsers : Form
    {
        private DataView _UsersDataView;
        public frmManageUsers()
        {
            InitializeComponent();
            LoadUsers();
        }
        private void LoadUsers()
        {
            DataTable UsersTable=clsUser.GetAllUsers();
           if (!UsersTable.Columns.Contains("FullName"))
            {
                UsersTable.Columns.Add("FullName", typeof(string));
            }

            foreach (DataRow row in UsersTable.Rows)
            {
                string FullName = clsPerson.GetPersonByID((int)row["PersonID"]).FullName;
                row["FullName"] = FullName;
            }

            _UsersDataView = new DataView(UsersTable);
            dgvUsersList.DataSource = _UsersDataView;
            dgvUsersList.Columns["UserID"].DisplayIndex = 0;
            dgvUsersList.Columns["UserName"].DisplayIndex = 1;
            dgvUsersList.Columns["FullName"].DisplayIndex = 2;
            dgvUsersList.Columns["PersonID"].DisplayIndex = 3;
            dgvUsersList.Columns["isActive"].DisplayIndex = 4;

            if (dgvUsersList.Columns.Contains("Password"))
                dgvUsersList.Columns["Password"].Visible = false;
            lblNumberOfUsers.Text = _UsersDataView.Count.ToString();
            cbFilter.SelectedIndex = 0;
            cbIsActiveType.SelectedIndex = 0;
            cbIsActiveType.Visible = false;
            txtSearch.Visible = true;
            
        }
        private void RefreshUsersList(clsUser User)
        {
            if (User.UserID != -1)
            {
                LoadUsers();
            }
        }
        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            clsUser NewUser = new clsUser();
            Form frmAddNewUser=new frmAddUpdateUser(NewUser);
            ((frmAddUpdateUser)frmAddNewUser).UserSaved +=RefreshUsersList;
            frmAddNewUser.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _UsersDataView.RowFilter = "";
            switch (cbFilter.SelectedIndex)
            {
                case 0:
                    txtSearch.Visible = true;
                    cbIsActiveType.Visible = false;
                    _UsersDataView.Sort="UserID";
                   
                    break;
                case 1:
                    txtSearch.Visible = true;
                    cbIsActiveType.Visible = false; 
                    _UsersDataView.Sort = "Username";
                    break;
                case 2:
                    txtSearch.Visible = true;
                    cbIsActiveType.Visible = false;
                    _UsersDataView.Sort = "PersonID";
                    break;
                case 3:
                    txtSearch.Visible = true;
                    cbIsActiveType.Visible = false;
                    _UsersDataView.Sort = "FullName";
                    break;
                case 4:
                    txtSearch.Visible = false;
                    cbIsActiveType.Visible = true;
                    _UsersDataView.Sort = "isActive";
                    break;

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtSearch.Text))
            {
                LoadUsers();
                return;
            }
            string text = txtSearch.Text.Replace("'", "''");

            switch (cbFilter.SelectedIndex)
            {

                case 0:
                   
                    if (int.TryParse(text, out int ID))
                    {
                        _UsersDataView.RowFilter = $"UserID = {ID}";
                    }
                   

                    break;
                case 1:
                   
                    _UsersDataView.RowFilter = $"Username like '%{text}%'";

                    break;
                case 2:
                   
                    if (int.TryParse(txtSearch.Text, out int personID))
                    {
                        _UsersDataView.RowFilter = $"PersonID = {personID}";
                    }
                   
                    break;
                case 3:
               
                    _UsersDataView.RowFilter = $"FullName like '%{text}%'";

                    break;
              
                default:
                    LoadUsers();
                    break;


            }
        }

        private void cbIsActiveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cbIsActiveType.SelectedIndex)
            {
                case 0:
                    _UsersDataView.RowFilter = "";
                    break;
                case 1:
                    _UsersDataView.RowFilter = "isActive = true";
                    break;
                case 2:
                    _UsersDataView.RowFilter = "isActive = false";
                    break;
                default:
                    LoadUsers();
                    break;
            }

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string UserName = (string)dgvUsersList.SelectedRows[0].Cells["UserName"].Value;

            Form frmUpdateUser = new frmAddUpdateUser(clsUser.GetUserByUserName(UserName));
            ((frmAddUpdateUser)frmUpdateUser).UserSaved += RefreshUsersList;
            frmUpdateUser.ShowDialog();
        }

        private void dgvUsersList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvUsersList.ClearSelection();
                dgvUsersList.Rows[e.RowIndex].Selected = true;
                dgvUsersList.CurrentCell = dgvUsersList.Rows[e.RowIndex].Cells[0];
            }

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsersList.SelectedRows[0].Cells["UserID"].Value;
           if( MessageBox.Show("Are you sure you want to delete this user?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)== DialogResult.Yes)
            {
                if(clsUser.DeleteUserByUserID(UserID))
                {
                    LoadUsers();
                    MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to delete user. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {

            clsUser User = clsUser.GetUserByUserName((string)dgvUsersList.SelectedRows[0].Cells["UserName"].Value);
            Form frmChangePassword = new frmChangePassword(User);
            ((frmChangePassword)frmChangePassword).OnPasswordChanged += RefreshUsersList;
            frmChangePassword.ShowDialog(); 
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsUser User = clsUser.GetUserByUserName((string)dgvUsersList.SelectedRows[0].Cells["UserName"].Value);
            Form frmShowUserDetails = new frmUserDetails(User);
            ((frmUserDetails)frmShowUserDetails).PersonUpdated += RefreshUsersList;
            frmShowUserDetails.ShowDialog();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsUser NewUser = new clsUser();
            Form frmAddNewUser = new frmAddUpdateUser(NewUser);
            ((frmAddUpdateUser)frmAddNewUser).UserSaved += RefreshUsersList;
            frmAddNewUser.ShowDialog();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature NOT implemented yet");
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature NOT implemented yet");

        }
    }
}
