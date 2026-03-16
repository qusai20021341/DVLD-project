using DVLD_Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsUser
    {
        enum enMode { AddNew=0,Update=1}
        enMode _Mode { get; set; }
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool isActive { get; set; }
        public clsUser()
        {
            _Mode=enMode.AddNew;
            UserID = -1;
            PersonID=-1;
            UserName = "";
            Password = "";
            isActive = false;
        }
        private clsUser(int UserID,int PersonID,string UserName,string Password,bool isActive)
        {
            _Mode = enMode.Update;
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.isActive = isActive;

        }

        private bool _AddNewUser()
        {
            this.UserID=clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.isActive);
            return UserID > -1;
        }
        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.isActive);
        }
        public static clsUser GetUserByUserID(int UserID)
        {
            string UserName = "";
            int PersonID = -1;
            string Password = "";
            bool isActive = false;
            if(clsUserData.GetUserByUserID(UserID,ref PersonID,ref UserName,ref Password,ref isActive))
            {
                return new clsUser(UserID,PersonID,UserName,Password,isActive);
            }
            else
            {
                return null;
            }
        }
        public static clsUser GetUserByUserName(string UserName)
        {   
            int UserID = -1;
            int PersonID = -1;
            string Password = "";
            bool isActive = false;
            if (clsUserData.GetUserByUserName(UserName, ref PersonID, ref UserID, ref Password, ref isActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, isActive);
            }
            else
            {
                return null;
            }

        }
        public static bool isUserExistByUserName(string UserName)
        {
            return clsUserData.isUserExistByUserName(UserName);
        }
        public static bool isUserExistByPersonID(int PersonID)
        {
            return clsUserData.isUserExistByPersonID(PersonID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewUser())
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    break;
                case enMode.Update:
                    {
                        return _UpdateUser();
                    }
                    break;
                default: return false;
            }
        }
        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }
        public static  bool DeleteUserByUserID(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }
    }
}
