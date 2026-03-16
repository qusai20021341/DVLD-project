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
    public partial class frmManageDetainedLicenses : Form
    {
        private DataView _DetainedView;
        public frmManageDetainedLicenses()
        {
            InitializeComponent();
            LoadDetainLicense();
        }
        private void LoadDetainLicense()
        {
            DataTable Detains = clsDetaineLicense.GetAllDetainedLicenses();
            _DetainedView = new DataView(Detains);

            dgvDetainedLicenses.DataSource = _DetainedView;
            lblNumberOfRecords.Text = Detains.Rows.Count.ToString();

            dgvDetainedLicenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvDetainedLicenses.Columns["DetainID"].HeaderText = "Detain ID";
            dgvDetainedLicenses.Columns["LicenseID"].HeaderText = "License ID";
            dgvDetainedLicenses.Columns["IsReleased"].HeaderText = "Released";
            dgvDetainedLicenses.Columns["FineFees"].HeaderText = "Fine Fees";
            dgvDetainedLicenses.Columns["ReleaseDate"].HeaderText = "Release Date";
            dgvDetainedLicenses.Columns["NationalNo"].HeaderText = "National No";
            dgvDetainedLicenses.Columns["FullName"].HeaderText = "Full Name";
            dgvDetainedLicenses.Columns["ReleaseApplicationID"].HeaderText = "Release App ID";

            dgvDetainedLicenses.Columns["DetainID"].FillWeight = 8;
            dgvDetainedLicenses.Columns["LicenseID"].FillWeight = 10;
            dgvDetainedLicenses.Columns["IsReleased"].FillWeight = 8;
            dgvDetainedLicenses.Columns["FineFees"].FillWeight = 12;
            dgvDetainedLicenses.Columns["ReleaseDate"].FillWeight = 18;
            dgvDetainedLicenses.Columns["NationalNo"].FillWeight = 12;
            dgvDetainedLicenses.Columns["FullName"].FillWeight = 22;
            dgvDetainedLicenses.Columns["ReleaseApplicationID"].FillWeight = 10;

            cbFilterBy.SelectedIndex = 0;
        }
        private void RefreshDetains(bool isDetains)
        {
            LoadDetainLicense();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            Form frmDetainLicense = new frmDetainLicense();
            ((frmDetainLicense)frmDetainLicense).OnDetian += RefreshDetains;
            frmDetainLicense.ShowDialog();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            Form frmReleaseDetainedLicense = new frmReleaseDetainedLicense();
            ((frmReleaseDetainedLicense)frmReleaseDetainedLicense).OnRelease += RefreshDetains;
            frmReleaseDetainedLicense.ShowDialog();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbFilterBy.SelectedIndex)
            {
                case 0: 
                    txtSearch.Visible = false;
                    break;

                case 1: 
                    txtSearch.Visible = true;
                    _DetainedView.Sort = "DetainID";
                    break;

                case 2: 
                    txtSearch.Visible = true;
                    _DetainedView.Sort = "IsReleased";
                    break;

                case 3:
                    txtSearch.Visible = true;
                    _DetainedView.Sort = "NationalNo";
                    break;

                case 4: 
                    txtSearch.Visible = true;
                    _DetainedView.Sort = "FullName";
                    break;

                case 5: 
                    txtSearch.Visible = true;
                    _DetainedView.Sort = "ReleaseApplicationID";
                    break;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                LoadDetainLicense();
                return;
            }

            string text = txtSearch.Text.Replace("'", "''");

            switch (cbFilterBy.SelectedIndex)
            {
                case 1:
                    if (int.TryParse(text, out int DetainID))
                    {
                        _DetainedView.RowFilter = $"DetainID = {DetainID}";
                    }
                    break;

                case 2:
                    _DetainedView.RowFilter = $"Convert(IsReleased,'System.String') like '%{text}%'";
                    break;

                case 3:
                    _DetainedView.RowFilter = $"NationalNo like '%{text}%'";
                    break;

                case 4:
                    _DetainedView.RowFilter = $"FullName like '%{text}%'";
                    break;

                case 5:
                    if (int.TryParse(text, out int ReleaseAppID))
                    {
                        _DetainedView.RowFilter = $"ReleaseApplicationID = {ReleaseAppID}";
                    }
                    break;
            }
        }

        private void dgvDetainedLicenses_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvDetainedLicenses.ClearSelection();
                dgvDetainedLicenses.Rows[e.RowIndex].Selected = true;
                dgvDetainedLicenses.CurrentCell = dgvDetainedLicenses.Rows[e.RowIndex].Cells[0];

                bool isReleased = Convert.ToBoolean(
            dgvDetainedLicenses.Rows[e.RowIndex].Cells["IsReleased"].Value
        );

                releaseDetainedLicenseToolStripMenuItem.Enabled = !isReleased;
            }
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.SelectedRows[0].Cells["LicenseID"].Value;
            clsLicense License=clsLicense.GetLicense(LicenseID);
            clsDriver Driver= clsDriver.GetDriver(License.DriverID);
            if(Driver != null)
            {
                clsPerson Person = clsPerson.GetPersonByID(Driver.PersonID);
                if(Person != null)
                {
                    Form frmPerosnDetails = new frmPersonDetails(Person);
                    frmPerosnDetails.ShowDialog();
                }
               
            }
           

            

        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.SelectedRows[0].Cells["LicenseID"].Value;
            clsLicense License=clsLicense.GetLicense(LicenseID);
            if (License != null)
            {
                Form frmLicense = new frmLicense(License);
                frmLicense.ShowDialog();
            }
        }

        private void showPersonLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.SelectedRows[0].Cells["LicenseID"].Value;
            clsLicense License = clsLicense.GetLicense(LicenseID);
            if (License != null)
            {
                Form frmShowLicenseHistory = new frmLicenseHistory(License);
                frmShowLicenseHistory.ShowDialog();
            }
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.SelectedRows[0].Cells["LicenseID"].Value;
            clsLicense License = clsLicense.GetLicense(LicenseID);
            if(License != null)
            {
                Form frmReleaseDetainedLicense = new frmReleaseDetainedLicense(License);
                ((frmReleaseDetainedLicense)frmReleaseDetainedLicense).OnRelease += RefreshDetains;
                frmReleaseDetainedLicense.ShowDialog();
            }
        }
    }
}
