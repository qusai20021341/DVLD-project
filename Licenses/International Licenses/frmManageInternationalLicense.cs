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
    public partial class frmManageInternationalLicense : Form
    {
        private DataView _ILLicenseView;
        public frmManageInternationalLicense()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            DataTable InternationalLicenses = clsInternationalLicense.GetAllInternationalLicenses();
            _ILLicenseView = new DataView(InternationalLicenses);

            dgvILLicense.DataSource = _ILLicenseView;
            NumberOfRecords.Text = InternationalLicenses.Rows.Count.ToString();

            dgvILLicense.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvILLicense.Columns["InternationalLicenseID"].HeaderText = "Int. Lic. ID";
            dgvILLicense.Columns["ApplicationID"].HeaderText = "App. ID";
            dgvILLicense.Columns["DriverID"].HeaderText = "Driver ID";
            dgvILLicense.Columns["IssuedUsingLocalLicenseID"].HeaderText = "Local Lic. ID";
            dgvILLicense.Columns["IssueDate"].HeaderText = "Issue Date";
            dgvILLicense.Columns["ExpirationDate"].HeaderText = "Expiry Date";
            dgvILLicense.Columns["IsActive"].HeaderText = "Active";

            dgvILLicense.Columns["InternationalLicenseID"].FillWeight = 10;
            dgvILLicense.Columns["ApplicationID"].FillWeight = 10;
            dgvILLicense.Columns["DriverID"].FillWeight = 10;
            dgvILLicense.Columns["IssuedUsingLocalLicenseID"].FillWeight = 15;
            dgvILLicense.Columns["IssueDate"].FillWeight = 15;
            dgvILLicense.Columns["ExpirationDate"].FillWeight = 15;
            dgvILLicense.Columns["IsActive"].FillWeight = 10;

            foreach (DataGridViewColumn col in dgvILLicense.Columns)
            {
                if (!new string[] { "InternationalLicenseID", "ApplicationID", "DriverID", "IssuedUsingLocalLicenseID", "IssueDate", "ExpirationDate", "IsActive" }.Contains(col.Name))
                {
                    col.Visible = false;
                }
            }
            cbFilterWith.SelectedIndex = 0;
        }

        private void dgvILLicenses_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvILLicenses.ClearSelection();
                dgvILLicenses.Rows[e.RowIndex].Selected = true;
                dgvILLicenses.CurrentCell = dgvILLicenses.Rows[e.RowIndex].Cells[0];
            }
        }

        private void cbFilterWith_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbFilterWith.SelectedIndex)
            {
                case 0:
                    txtSearch.Visible = false;
                    break;

                case 1:
                    txtSearch.Visible = true;
                    _ILLicenseView.Sort = "InternationalLicenseID";
                    break;

                case 2:
                    txtSearch.Visible = true;
                    _ILLicenseView.Sort = "ApplicationID";
                    break;

                case 3:
                    txtSearch.Visible = true;
                    _ILLicenseView.Sort = "DriverID";
                    break;

                case 4:
                    txtSearch.Visible = true;
                    _ILLicenseView.Sort = "IssuedUsingLocalLicenseID";
                    break;

                case 5:
                    txtSearch.Visible = true;
                    _ILLicenseView.Sort = "IssueDate";
                    break;

                case 6:
                    txtSearch.Visible = true;
                    _ILLicenseView.Sort = "ExpirationDate";
                    break;

                case 7:
                    txtSearch.Visible = true;
                    _ILLicenseView.Sort = "IsActive";
                    break;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                LoadData();
                return;
            }

            string text = txtSearch.Text.Replace("'", "''");

            switch (cbFilterWith.SelectedIndex)
            {
                case 1:
                    if (int.TryParse(text, out int InternationalLicenseID))
                    {
                        _ILLicenseView.RowFilter = $"InternationalLicenseID = {InternationalLicenseID}";
                    }
                    break;

                case 2:
                    if (int.TryParse(text, out int ApplicationID))
                    {
                        _ILLicenseView.RowFilter = $"ApplicationID = {ApplicationID}";
                    }
                    break;

                case 3:
                    if (int.TryParse(text, out int DriverID))
                    {
                        _ILLicenseView.RowFilter = $"DriverID = {DriverID}";
                    }
                    break;

                case 4:
                    if (int.TryParse(text, out int LocalID))
                    {
                        _ILLicenseView.RowFilter = $"IssuedUsingLocalLicenseID = {LocalID}";
                    }
                    break;

                case 5:
                    _ILLicenseView.RowFilter = $"Convert(IssueDate, 'System.String') like '%{text}%'";
                    break;

                case 6:
                    _ILLicenseView.RowFilter = $"Convert(ExpirationDate, 'System.String') like '%{text}%'";
                    break;

                case 7:
                    _ILLicenseView.RowFilter = $"Convert(IsActive, 'System.String') like '%{text}%'";
                    break;
            }
        }

        private void dgvILLicense_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvILLicense.ClearSelection();
                dgvILLicense.Rows[e.RowIndex].Selected = true;
                dgvILLicense.CurrentCell = dgvILLicense.Rows[e.RowIndex].Cells[0];
            }
        }

        private void btnAddApplication_Click(object sender, EventArgs e)
        {
            frmAddInternationalLicenseApplication frmAddApp = new frmAddInternationalLicenseApplication();
            ((frmAddInternationalLicenseApplication)frmAddApp).OnIssue += RefreshData;

            frmAddApp.ShowDialog();
        }
        private void RefreshData(bool isIssued)
        {
            if (isIssued)
            {
                LoadData();
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            int ILID = (int)dgvILLicense.SelectedRows[0].Cells["InternationalLicenseID"].Value;
            clsInternationalLicense internationalLicense=clsInternationalLicense.GetInternationalLicenseByID(ILID);
            if (internationalLicense != null)
            {
                clsPerson person = clsPerson.GetPersonByID(internationalLicense.ApplicantPersonID);
                if(person != null)
                {
                    Form frmShowPersonDetails=new frmPersonDetails(person);
                    frmShowPersonDetails.ShowDialog();
                }
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            int ILID = (int)dgvILLicense.SelectedRows[0].Cells["InternationalLicenseID"].Value;
            clsInternationalLicense internationalLicense = clsInternationalLicense.GetInternationalLicenseByID(ILID);
            if (internationalLicense != null)
            {
                Form frmShowLicenseDetails = new frmDriverInterNationalLicenseInfo(internationalLicense);
                frmShowLicenseDetails.ShowDialog();
            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ILID = (int)dgvILLicense.SelectedRows[0].Cells["InternationalLicenseID"].Value;
            clsInternationalLicense internationalLicense = clsInternationalLicense.GetInternationalLicenseByID(ILID);
            if (internationalLicense != null)
            {
                clsLicense license = clsLicense.GetLicense(internationalLicense.IssuedUsingLocalLicenseID);
                if(license != null)
                {
                    Form frmShowLicenseHistory = new frmLicenseHistory(license);
                    frmShowLicenseHistory.ShowDialog();
                }
                
            }
        }

        private void btnClosee_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
