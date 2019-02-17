using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB_Lab02
{
    public class CarriageDetails
    {
        private int id;
        private int idClient;
        private int idGoods;
        private int idTransport;
        private int idCity;
        private DateTime deteOfDelivery;
        private string typeOfService;

        public CarriageDetails(int idClient, int idGoods, int idTransport, int idCity, DateTime deteOfDelivery, string typeOfService)
        {
            this.idClient = idClient;
            this.idGoods = idGoods;
            this.idTransport = idTransport;
            this.idCity = idCity;
            this.deteOfDelivery = deteOfDelivery;
            this.typeOfService = typeOfService;
        }

        public CarriageDetails() { }

        public CarriageDetails(int id, int idClient, int idGoods, int idTransport, int idCity, DateTime deteOfDelivery, string typeOfService)
        {
            this.id = id;
            this.idClient = idClient;
            this.idGoods = idGoods;
            this.idTransport = idTransport;
            this.idCity = idCity;
            this.deteOfDelivery = deteOfDelivery;
            this.typeOfService = typeOfService;
        }

        public int IDClient
        {
            get { return idClient; }
            set { idClient = value; }
        }

        public int IDGoods
        {
            get { return idGoods; }
            set { idGoods = value; }
        }

        public int IDTransport
        {
            get { return idTransport; }
            set { idTransport = value; }
        }

        public int IDCity
        {
            get { return idCity; }
            set { idCity = value; }
        }

        public DateTime DeteOfDelivery { get => deteOfDelivery; set => deteOfDelivery = value; }
        public string TypeOfService { get => typeOfService; set => typeOfService = value; }
    }
}