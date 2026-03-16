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
    public partial class frmPersonDetails : Form
    {
        public delegate void PersonUpdatedEventHandler(bool isSaved);
        public event PersonUpdatedEventHandler PersonUpdated;

        // Constructor that accepts a clsPerson object
        public frmPersonDetails(clsPerson Person)
        {
            InitializeComponent();
            ctrlPersonDetails1.Person = Person;
            ctrlPersonDetails1.OnPersonUpdated += CtrlPersonDetails1_OnPersonUpdated;
        }

        // Event handler for button1 click event to close the form
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CtrlPersonDetails1_OnPersonUpdated(bool isSaved)
        {
            if (isSaved)
            {
                PersonUpdated?.Invoke(true);
            }
        }

    }
}
