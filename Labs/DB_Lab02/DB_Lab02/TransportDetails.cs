using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB_Lab02
{
    public class TransportDetails
    {
        private int id;
        private string transportNumber;
        private int capasity;
        private int idDriver;

        public TransportDetails(string transportNumber, int capasity, int idDriver)
        {
            this.TransportNumber = transportNumber;
            this.Capasity = capasity;
            this.IdDriver = idDriver;
        }

        public TransportDetails(int id, string transportNumber, int capasity, int idDriver)
        {
            this.Id = id;
            this.TransportNumber = transportNumber;
            this.Capasity = capasity;
            this.IdDriver = idDriver;
        }

        public int Id { get => id; set => id = value; }
        public string TransportNumber { get => transportNumber; set => transportNumber = value; }
        public int Capasity { get => capasity; set => capasity = value; }
        public int IdDriver { get => idDriver; set => idDriver = value; }
    }
}