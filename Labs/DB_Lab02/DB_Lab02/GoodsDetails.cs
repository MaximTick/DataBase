using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB_Lab02
{
    public class GoodsDetails
    {
        private int id;
        private string shippingName;
        private int weightGoods;

        public GoodsDetails(string shippingName, int weightGoods)
        {
            this.ShippingName = shippingName;
            this.WeightGoods = weightGoods;
        }

        public GoodsDetails(int id, string shippingName, int weightGoods)
        {
            this.Id = id;
            this.ShippingName = shippingName;
            this.WeightGoods = weightGoods;
        }

        public int Id { get => id; set => id = value; }
        public string ShippingName { get => shippingName; set => shippingName = value; }
        public int WeightGoods { get => weightGoods; set => weightGoods = value; }
    }
}