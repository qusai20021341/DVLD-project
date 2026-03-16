using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DVLD_Data_Access_Layer;

namespace DVLD_Business_Layer
{
    public class clsPerson
    {
        enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode;
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string NationalNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int CountryID { get; set; }
        public string ImagePath { get; set; }
        public string FullName
        {
            get
            {
                return $" {FirstName} {SecondName} {ThirdName} {LastName} ";
            }
        }
        public clsPerson()
        {
            PersonID = -1;
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            NationalNo = "";
            DateOfBirth = DateTime.Now;
            Gendor = 0;
            Address = "";
            Phone = "";
            Email = "";
            CountryID = -1;
            ImagePath = "";
            _Mode = enMode.AddNew;
        }
        private clsPerson(int PersonID, string FirstName, string SecondName, string ThirdName, string LastName, string NationalNo, DateTime DateOfBirth, byte Gendor, string Address, string Phone, string Email, int CountryID, string ImagePath)
        {

            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.NationalNo = NationalNo;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;
            this._Mode = enMode.Update;
        }

        public static clsPerson GetPersonByID(int personID)
        {
            string FirstName = "", LastName = "", SecondName = "", ThirdName = "", NationalNo = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gendor = 0;
            int CountryID = -1;
            if (clsPersonData.GetPersonByID(personID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref NationalNo, ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref CountryID, ref ImagePath))
            {
                return new clsPerson(personID, FirstName, SecondName, ThirdName, LastName, NationalNo, DateOfBirth, Gendor, Address, Phone, Email, CountryID, ImagePath);
            }
            else
            {
                return null;
            }

        }
        public static clsPerson GetPerosnByNationalNo(string NationalNo)
        {
            string FirstName = "", LastName = "", SecondName = "", ThirdName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gendor = 0;
            int CountryID = -1;
            int PersonID = -1;
            if (clsPersonData.GetPersonByNationalNo( ref PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, NationalNo, ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref CountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, FirstName, SecondName, ThirdName, LastName, NationalNo, DateOfBirth, Gendor, Address, Phone, Email, CountryID, ImagePath);
            }
            else
            {
                return null;
            }

        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.FirstName,this. SecondName,this.ThirdName,this.LastName, this.NationalNo, this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email, this.CountryID, this.ImagePath);
            return (PersonID != -1);
        }
        private  bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this.PersonID,this.FirstName,this.SecondName,this.ThirdName, this.LastName, this.NationalNo, this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email, this.CountryID, this.ImagePath);
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewPerson())
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    } break;
                case enMode.Update:
                    {
                        return _UpdatePerson();
                    } break;
                default: return false;  
            }
        }
        public static bool DeletePerson(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }
        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }
        public static bool isPersonExistByNationalNo(string NationalNo)
        {
            return clsPersonData.isPesonExistsByNationalNo(NationalNo);
        }

    }
}
