using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB_Lab02
{
    public class CityDetails
    {
        private int id;
        private string city;

        public CityDetails(string city)
        {
            this.City = city;
        }

        public CityDetails(int id, string city)
        {
            this.Id = id;
            this.City = city;
        }

        public int Id { get => id; set => id = value; }
        public string City { get => city; set => city = value; }
    }
}