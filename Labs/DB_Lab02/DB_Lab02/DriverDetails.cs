using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB_Lab02
{
    public class DriverDetails
    {
        private int id;
        private string lastname;
        private string firstname;
        private int driversLicenseNumber;
        private string category;

        public DriverDetails(string lastname, string firstname, int driversLicenseNumber, string category)
        {
            this.Lastname = lastname;
            this.Firstname = firstname;
            this.DriversLicenseNumber = driversLicenseNumber;
            this.Category = category;
        }

        public DriverDetails(int id, string lastname, string firstname, int driversLicenseNumber, string category)
        {
            this.Id = id;
            this.Lastname = lastname;
            this.Firstname = firstname;
            this.DriversLicenseNumber = driversLicenseNumber;
            this.Category = category;
        }

        public int Id { get => id; set => id = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public string Firstname { get => firstname; set => firstname = value; }
        public string Category { get => category; set => category = value; }
        public int DriversLicenseNumber { get => driversLicenseNumber; set => driversLicenseNumber = value; }
    }
}