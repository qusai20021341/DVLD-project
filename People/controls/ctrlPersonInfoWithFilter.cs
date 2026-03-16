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
    public partial class ctrlPersonInfoWithFilter : UserControl
    {

        private clsPerson _Person;
        public clsPerson Person
        {
            get
            {
                return _Person;
            }

            set
            {
                _Person=value;
                if(_Person !=null)
                {
                    ctrlPersonDetails1.Person=_Person;
                }
            }
        }
        public ctrlPersonInfoWithFilter()
        {
            InitializeComponent();
            cbFindBy.SelectedIndex=0;    
        }

        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtFindBy.Text))
            {

                if (cbFindBy.SelectedIndex == 0)
                {
                    int PersonID = Convert.ToInt32(txtFindBy.Text);
                    Person = clsPerson.GetPersonByID(PersonID);
                    if (Person != null)
                    {
                        ctrlPersonDetails1.Person = Person;
                    }
                    else
                    {
                        MessageBox.Show("No person found with the given ID.");
                    }
                }
                else if (cbFindBy.SelectedIndex == 1)
                {
                     Person = clsPerson.GetPerosnByNationalNo(txtFindBy.Text);
                    if (Person != null)
                    {
                        ctrlPersonDetails1.Person = Person;
                    }
                    else
                    {
                        MessageBox.Show("No person found with the given National Number.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a value to search.");
            }
        }

        private void btnAddperson_Click(object sender, EventArgs e)
        {
            clsPerson NewPerson = new clsPerson();
            Form frmAddPerson = new frmAddUpdatePerson(NewPerson);
            frmAddPerson.ShowDialog();
            if (NewPerson.PersonID != 0)
            {
                Person = NewPerson;
                ctrlPersonDetails1.Person = NewPerson;
                txtFindBy.Text = NewPerson.PersonID.ToString();
                cbFindBy.SelectedIndex = 0;
            }
        }
    }
}
