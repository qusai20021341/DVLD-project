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
    public partial class ctrlDriverLicenses : UserControl
    {
        private clsLicense _License;
        public clsLicense License { 
            get
            {
                return _License;
            }
            set
            {
                _License = value;
                if (_License != null)
                    LoadLocalLicenses();
                    LoadInternationalLicenses();
            }
        }
       
        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }
        public ctrlDriverLicenses(clsLicense License)
        {
            InitializeComponent();
            _License = License;
            LoadLocalLicenses();
            LoadInternationalLicenses();

        }
        private void LoadLocalLicenses()
        {
            DataTable LocalLicense = clsLicense.GetPersonLicenses(clsDriver.GetDriver(_License.DriverID).PersonID);
            dgvLocalLicenses.DataSource = LocalLicense;

            dgvLocalLicenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvLocalLicenses.Columns["LicenseID"].HeaderText = "Lic. ID";
            dgvLocalLicenses.Columns["ApplicationID"].HeaderText = "App. ID";
            dgvLocalLicenses.Columns["LicenseID"].FillWeight = 10;
            dgvLocalLicenses.Columns["ApplicationID"].FillWeight = 10;
            dgvLocalLicenses.Columns["ClassName"].FillWeight = 30;
            dgvLocalLicenses.Columns["IssueDate"].FillWeight = 20;
            dgvLocalLicenses.Columns["ExpirationDate"].FillWeight = 20;
            dgvLocalLicenses.Columns["IsActive"].FillWeight = 10;

            lblNumberOfLocalLicense.Text = LocalLicense.Rows.Count.ToString();
        }
        private void LoadInternationalLicenses()
        {
            DataTable InternationalLicense = clsInternationalLicense.GetInternationalLicenses(_License.DriverID);
            dgvInternationalLicenses.DataSource = InternationalLicense;

            dgvInternationalLicenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvInternationalLicenses.Columns["InternationalLicenseID"].HeaderText = "Int. Lic. ID";
            dgvInternationalLicenses.Columns["ApplicationID"].HeaderText = "App. ID";
            dgvInternationalLicenses.Columns["DriverID"].HeaderText = "Driver ID";
            dgvInternationalLicenses.Columns["IssuedUsingLocalLicenseID"].HeaderText = "Local Lic. ID";
            dgvInternationalLicenses.Columns["IssueDate"].HeaderText = "Issue Date";
            dgvInternationalLicenses.Columns["ExpirationDate"].HeaderText = "Expiry Date";
            dgvInternationalLicenses.Columns["IsActive"].HeaderText = "Active";
            dgvInternationalLicenses.Columns["CreatedByUserID"].HeaderText = "Created By";

            dgvInternationalLicenses.Columns["InternationalLicenseID"].FillWeight = 10;
            dgvInternationalLicenses.Columns["ApplicationID"].FillWeight = 10;
            dgvInternationalLicenses.Columns["DriverID"].FillWeight = 10;
            dgvInternationalLicenses.Columns["IssuedUsingLocalLicenseID"].FillWeight = 15;
            dgvInternationalLicenses.Columns["IssueDate"].FillWeight = 15;
            dgvInternationalLicenses.Columns["ExpirationDate"].FillWeight = 15;
            dgvInternationalLicenses.Columns["IsActive"].FillWeight = 10;
            dgvInternationalLicenses.Columns["CreatedByUserID"].FillWeight = 10;

            lblNumberOfInternationalLicense.Text = InternationalLicense.Rows.Count.ToString();
        }

        private void dgvLocalLicenses_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvLocalLicenses.ClearSelection();
                dgvLocalLicenses.Rows[e.RowIndex].Selected = true;
                dgvLocalLicenses.CurrentCell = dgvLocalLicenses.Rows[e.RowIndex].Cells[0];
            }
        }

        private void showLIcenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvLocalLicenses.SelectedRows[0].Cells["LicenseID"].Value;
            Form frmShowDetails = new frmLicense(clsLicense.GetLicense(LicenseID));
            frmShowDetails.ShowDialog();


        }
    }
}
