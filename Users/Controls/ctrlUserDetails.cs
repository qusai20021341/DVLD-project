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
    public partial class ctrlUserDetails : UserControl
    {
        public event Action<bool> OnPersonUpdated;
        protected virtual void PersonUpdated(bool isUpdated)
        {
            Action <bool>handler= OnPersonUpdated;
            if (handler != null )
            {
                handler(isUpdated);
            }
        }
        private clsUser _User;
        public clsUser User {
            get { return _User; }
            set
            {
                _User = value;
                LoadUserInfo();
            }
        }
        public ctrlUserDetails()
        {
            InitializeComponent();
            ctrlPersonDetails1.OnPersonUpdated += personUpdated;
        }
        
        private void LoadUserInfo()
        {
            if (_User != null)
            {
                ctrlPersonDetails1.Person = clsPerson.GetPersonByID(_User.PersonID);
                lblUserID.Text = _User.UserID.ToString();
                lblUserName.Text = _User.UserName;
                if (_User.isActive)
                    lblIsActive.Text = "Yes";
                else
                    lblIsActive.Text = "No";
            }
        }
        private void personUpdated(bool isUpdated)
        {
            PersonUpdated(isUpdated);

        }

    }
}
