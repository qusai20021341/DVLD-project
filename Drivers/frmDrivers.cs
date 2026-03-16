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
    public partial class frmDrivers : Form
    {
        public frmDrivers()
        {
            InitializeComponent();
            LoadDrivers();
        }
        private void LoadDrivers()
        {
            DataTable Drivers = clsDriver.GetAllDrivers();
            dgvDrivers.DataSource = Drivers;
            dgvDrivers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDrivers.Columns["DriverID"].FillWeight = 10;
            dgvDrivers.Columns["PersonID"].FillWeight = 10;
            dgvDrivers.Columns["NationalNo"].FillWeight = 15;
            dgvDrivers.Columns["FullName"].FillWeight = 35; 
            dgvDrivers.Columns["CreatedDate"].FillWeight = 15;
            dgvDrivers.Columns["IsActive"].FillWeight = 15;
            lblNumberOfRecords.Text = Drivers.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = dgvDrivers.DataSource as DataTable;
            if (dt == null) return;

            string sortColumn = cbFilterBy.SelectedItem.ToString();

            txtSearchBy.Visible = sortColumn != "None";
            txtSearchBy.Text = "";

            DataView dv = dt.DefaultView;
            dv.Sort = sortColumn == "None" ? "" : sortColumn + " ASC";
            dgvDrivers.DataSource = dv.ToTable();

            dgvDrivers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDrivers.Columns["DriverID"].FillWeight = 10;
            dgvDrivers.Columns["PersonID"].FillWeight = 10;
            dgvDrivers.Columns["NationalNo"].FillWeight = 15;
            dgvDrivers.Columns["FullName"].FillWeight = 35;
            dgvDrivers.Columns["CreatedDate"].FillWeight = 15;
            dgvDrivers.Columns["IsActive"].FillWeight = 15;
        }

        private void txtSearchBy_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = clsDriver.GetAllDrivers();
            if (dt == null) return;

            string filterColumn = cbFilterBy.SelectedItem.ToString();
            if (filterColumn == "None") return;

            string filterText = txtSearchBy.Text.Trim().Replace("'", "''");
            DataView dv = dt.DefaultView;

            switch (filterColumn)
            {
                case "DriverID":
                    dv.RowFilter = $"Convert([DriverID], 'System.String') LIKE '%{filterText}%'";
                    break;
                case "PersonID":
                    dv.RowFilter = $"Convert([PersonID], 'System.String') LIKE '%{filterText}%'";
                    break;
                case "NationalNo":
                    dv.RowFilter = $"[NationalNo] LIKE '%{filterText}%'";
                    break;
                case "FullName":
                    dv.RowFilter = $"[FullName] LIKE '%{filterText}%'";
                    break;
                case "CreatedDate":
                    dv.RowFilter = $"Convert([CreatedDate], 'System.String') LIKE '%{filterText}%'";
                    break;
                case "IsActive":
                    dv.RowFilter = $"Convert([IsActive], 'System.String') LIKE '%{filterText}%'";
                    break;
                default:
                    dv.RowFilter = "";
                    break;
            }

            dgvDrivers.DataSource = dv.ToTable();
            lblNumberOfRecords.Text = dv.Count.ToString();
        }
    }
}
