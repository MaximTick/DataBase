using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB_Lab02
{
    public class ClientDetails
    {
        private int id;
        private string clientName;

        public ClientDetails(string clientName)
        {
            this.ClientName = clientName;
        }

        public ClientDetails(int id, string clientName)
        {
            this.Id = id;
            this.ClientName = clientName;
        }

        public int Id { get => id; set => id = value; }
        public string ClientName { get => clientName; set => clientName = value; }
    }
}