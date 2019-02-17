using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DB_Lab02
{
    public partial class _Default : Page
    {
        CarriageDB db = new CarriageDB();
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = "<h1>Данные из таблицы Carriage</h1>";
            WriteCarriage();
        }


        public void WriteCarriage()
        {
            string result = "";
            db.GetAllCarriage().Select(p =>
            {
                result += String.Format("<li> {0} {1} {2} {3} {4} {5}<br>", p.IDClient, p.IDGoods, p.IDTransport,
                   p.IDCity, p.DeteOfDelivery, p.TypeOfService);
                return p;
            }).ToList(); //Сделать запрос неотложенным

            Label1.Text += result;  
        }

    }
}