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
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
            LoadTestTypesData();
        }
        private void LoadTestTypesData()
        {
            DataTable TestTypesDataTable = clsTestType.GetAllTestTypes();
            DataView TestTypesDataView=new DataView(TestTypesDataTable);
            dgvAllTestTypes.DataSource = TestTypesDataView;
            dgvAllTestTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAllTestTypes.Columns[0].FillWeight = 10;
            dgvAllTestTypes.Columns[0].HeaderText = "ID";
            dgvAllTestTypes.Columns[1].FillWeight = 30;
            dgvAllTestTypes.Columns[1].HeaderText = "Title";
            dgvAllTestTypes.Columns[2].FillWeight = 50;
            dgvAllTestTypes.Columns[2].HeaderText = "Description";
            dgvAllTestTypes.Columns[3].FillWeight = 10;
            dgvAllTestTypes.Columns[3].HeaderText = "Fees";
            lblNumberOfRecords.Text = TestTypesDataView.Count.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvAllTestTypes_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvAllTestTypes.ClearSelection();
                dgvAllTestTypes.Rows[e.RowIndex].Selected = true;
                dgvAllTestTypes.CurrentCell = dgvAllTestTypes.Rows[e.RowIndex].Cells[0];
            }
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestTypeID = (int)dgvAllTestTypes.SelectedRows[0].Cells["TestTypeID"].Value;
            Form frmUpdateTestType = new frmUpdateTestType(clsTestType.GetTestTypeByID(TestTypeID));
            ((frmUpdateTestType)frmUpdateTestType).OnTestTypeUpdated += RefreshTestTypesOnUpdate;
            frmUpdateTestType.ShowDialog();
        }
        private void RefreshTestTypesOnUpdate(bool isUpdaeted)
        {
            if(isUpdaeted)
            {
                LoadTestTypesData();
            }
        }
    }
}
