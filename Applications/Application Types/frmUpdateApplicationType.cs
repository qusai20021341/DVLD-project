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
    public partial class frmUpdateApplicationType : Form
    {
        public delegate void ApplicationTypeUpdated(bool isUpdated);
        public event ApplicationTypeUpdated OnApplicationTypeUpdated;
        private clsApplicationType _ApplicationType;
        public frmUpdateApplicationType(clsApplicationType ApplicationType)
        {
            InitializeComponent();
            _ApplicationType = ApplicationType;
            FillFields();
        }

        private void FillFields()
        {
            txtApplicationTypeTitle.Text=_ApplicationType.ApplicationTypeTitle;
            txtApplicationTypeFees.Text = _ApplicationType.ApplicationFees.ToString();
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtApplicationTypeTitle.Text) || !string.IsNullOrEmpty(txtApplicationTypeFees.Text))
            {
                if (!decimal.TryParse(txtApplicationTypeFees.Text, out decimal ApplicationTypeFees))
                {
                    MessageBox.Show("Please enter a valid decimal number.");
                    return;
                }

                if(clsApplicationType.UpdateApplicationType(_ApplicationType.ApplicationTypeID,txtApplicationTypeTitle.Text,ApplicationTypeFees))
                {
                    MessageBox.Show("Application Type Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnApplicationTypeUpdated?.Invoke(true);

                }

            }
            else
            {
                MessageBox.Show("Please fill all fields to perform update", "Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
