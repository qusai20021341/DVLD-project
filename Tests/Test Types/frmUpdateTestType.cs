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
    public partial class frmUpdateTestType : Form
    {
        public delegate void TestTypeUpdated(bool isUpdated);
        public event TestTypeUpdated OnTestTypeUpdated;
        private clsTestType _TestType;
        public frmUpdateTestType(clsTestType TestTyep)
        {
            InitializeComponent();
            _TestType = TestTyep;
            LoadTestData();
        }
        private void LoadTestData()
        {
            lblTestTypeID.Text = _TestType.TestTypeID.ToString();
            txtTestTypeTitle.Text=_TestType.TestTypeTitle;
            txtTestTypeDescription.Text= _TestType.TestTypeDescription;
            txtTestTypeFees.Text = _TestType.TestTypeFees.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtTestTypeTitle.Text) || !string.IsNullOrEmpty(txtTestTypeDescription.Text) || !string.IsNullOrEmpty(txtTestTypeFees.Text))
            {
                if (!decimal.TryParse(txtTestTypeFees.Text, out decimal TestTypeFees))
                {
                    MessageBox.Show("Please enter a valid decimal number.");
                    return;
                }
                if(clsTestType.UpdateTestType(_TestType.TestTypeID,txtTestTypeTitle.Text,txtTestTypeDescription.Text,TestTypeFees))
                {
                    OnTestTypeUpdated?.Invoke(true);
                    MessageBox.Show("Test Type Updated Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Faild To Update Test Type","Warning",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                MessageBox.Show("No Empty Fields Allowed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}
