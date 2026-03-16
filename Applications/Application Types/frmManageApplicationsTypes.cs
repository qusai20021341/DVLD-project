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
    public partial class frmManageApplicationsTypes : Form
    {
        public frmManageApplicationsTypes()
        {
            InitializeComponent();
            LoadApplicationTypesData();
        }
        private void LoadApplicationTypesData()
        {
            DataTable AllApplicationTypes=clsApplicationType.GetAllApplicationTypes();
            DataView AllApplicationTypesDataView=new DataView(AllApplicationTypes);
            dgvAllApplicationTypes.DataSource = AllApplicationTypesDataView;
            dgvAllApplicationTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAllApplicationTypes.Columns[0].FillWeight = 20;
            dgvAllApplicationTypes.Columns[1].FillWeight = 60;
            dgvAllApplicationTypes.Columns[2].FillWeight = 20;
            lblNumberOfRecords.Text=AllApplicationTypesDataView.Count.ToString();  
            
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationTypeID = (int)dgvAllApplicationTypes.SelectedRows[0].Cells["ApplicationTypeID"].Value;
            Form frmUpdateApplicationType = new frmUpdateApplicationType(clsApplicationType.GetApplicationTypeByID(ApplicationTypeID));
            ((frmUpdateApplicationType)frmUpdateApplicationType).OnApplicationTypeUpdated += RefreshApplicationTypesDataOnApplicationUpdate;
            frmUpdateApplicationType.ShowDialog();
        }
        private void RefreshApplicationTypesDataOnApplicationUpdate(bool isUpdated)
        {
            if (isUpdated)
            {
                LoadApplicationTypesData();
            }
        }
        private void dgvAllApplicationTypes_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvAllApplicationTypes.ClearSelection();
                dgvAllApplicationTypes.Rows[e.RowIndex].Selected = true;
                dgvAllApplicationTypes.CurrentCell = dgvAllApplicationTypes.Rows[e.RowIndex].Cells[0];
            }
        }
    }
}
