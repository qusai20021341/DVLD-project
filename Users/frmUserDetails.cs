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
    public partial class frmUserDetails : Form
    {
        public delegate void PersonUpdatedHandler(clsUser User);
        public event PersonUpdatedHandler PersonUpdated;
        private clsUser _User;
        public frmUserDetails(clsUser User)
        {
            InitializeComponent();
          
            _User = User;
            ctrlUserDetails1.OnPersonUpdated += personUpdated;
            LoadUserData();
        }
        private void LoadUserData()
        {
            ctrlUserDetails1.User= _User;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void personUpdated(bool isUpdated)
        {
            PersonUpdated?.Invoke(_User);
        }
    }
}
