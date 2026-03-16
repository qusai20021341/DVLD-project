using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business_Layer;

namespace DVLD_project
{
    public partial class frmManagePeople : Form
    {
        private DataView _PeopleDataView;
        public frmManagePeople()
        {
            InitializeComponent();
        }

        //Load People Data from Database and show it in the DataGridView
        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            LoadPeopleList();
        }

        private void LoadPeopleList()
        {
            DataTable PeopleTable = clsPerson.GetAllPeople();
            _PeopleDataView = new DataView(PeopleTable);
            dgvPeopleList.DataSource = _PeopleDataView;
            dgvPeopleList.Columns["ImagePath"].Visible = false;
            lblNumberOfPeople.Text = _PeopleDataView.Count.ToString();
            cbFilterBy.SelectedIndex = 1;
        }

        //Filter People Data in the DataGridView based on the selected filter in the ComboBox
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cbFilterBy.SelectedIndex)
            {
             
                case 1:
                    _PeopleDataView.Sort = "PersonID ASC";
                    break;
                    case 2:
                    _PeopleDataView.Sort = "NationalNo ASC";
                    break;
                    case 3:
                    _PeopleDataView.Sort = "FirstName ASC";
                    break;
                case 4:
                    _PeopleDataView.Sort = "SecondName ASC";
                    break;
                    case 5:
                    _PeopleDataView.Sort = "ThirdName ASC";
                    break;
                case 6:
                    _PeopleDataView.Sort = "LastName ASC";
                    break;
                case 7:
                    _PeopleDataView.Sort = "NationalityCountryID ASC";
                    break;
                    case 8:
                    _PeopleDataView.Sort = "Gendor ASC";
                    break;
                    case 9:
                    _PeopleDataView.Sort = "Phone ASC";
                    break;
                    case 10:
                    _PeopleDataView.Sort = "Email ASC";
                    break;



            }
        }
        //Close the form when the Close button is clicked
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Open the frmAddUpdatePerson form to add a new person when the Add Person button is clicked
        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            clsPerson NewPerosn = new clsPerson();
            Form frmAddPerson = new frmAddUpdatePerson(NewPerosn);
            ((frmAddUpdatePerson)frmAddPerson).OnPersonSaved+= RefreshPeopleList;   
            frmAddPerson.ShowDialog();

        }
        //Refresh the People list after a person is added or updated
        private void RefreshPeopleList(int PersonID)
        {
            if (PersonID>-1)
                LoadPeopleList();
        }

        //Show context menu on right-click in the DataGridView
        private void dgvPeopleList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvPeopleList.ClearSelection();
                dgvPeopleList.Rows[e.RowIndex].Selected = true;
                dgvPeopleList.CurrentCell = dgvPeopleList.Rows[e.RowIndex].Cells[0];
            }

        }
        
        private void ShowDetailsMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeopleList.SelectedRows[0].Cells["PersonID"].Value;
            clsPerson Person=clsPerson.GetPersonByID(PersonID);
            Form frmPersonDetails = new frmPersonDetails(Person);
            ((frmPersonDetails)frmPersonDetails).PersonUpdated += RefreshPeopleList;
            frmPersonDetails.ShowDialog();
        }

        private void RefreshPeopleList(bool isSaved)
        {
            if (isSaved)
                LoadPeopleList();
        }
        //Open the frmAddUpdatePerson form to add a new person when the Add New Person context menu item is clicked
        private void AddNewPersonMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson NewPerosn = new clsPerson();
            Form frmAddPerson = new frmAddUpdatePerson(NewPerosn);
            ((frmAddUpdatePerson)frmAddPerson).OnPersonSaved += RefreshPeopleList;
            frmAddPerson.ShowDialog();
        }

        //Open the frmAddUpdatePerson form to edit the selected person when the Edit Person context menu item is clicked
        private void EditPersonMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeopleList.SelectedRows[0].Cells["PersonID"].Value;
             clsPerson Person=clsPerson.GetPersonByID(PersonID);
            Form frmEditPerson = new frmAddUpdatePerson(Person);
            ((frmAddUpdatePerson)frmEditPerson).OnPersonSaved += RefreshPeopleList;
            frmEditPerson.ShowDialog();

        }

        private void DeletePersonMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeopleList.SelectedRows[0].Cells["PersonID"].Value;
            if(clsPerson.DeletePerson(PersonID))
            {
                MessageBox.Show("Person deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPeopleList();
            }
            else
            {
                MessageBox.Show("Failed to delete person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SendEmailMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Implement email sending functionality
            MessageBox.Show("Send Email functionality is not implemented yet.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PhoneCallMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Implement phone call functionality
            MessageBox.Show("Phone Call functionality is not implemented yet.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

      
    }
}
