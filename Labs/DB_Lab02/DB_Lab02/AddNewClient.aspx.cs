using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DB_Lab02
{
    public partial class AddNewClient : System.Web.UI.Page
    {
        ClientDB clientDB = new ClientDB();

        public AddNewClient(string textBox1)
        {
           TextBox1.Text = textBox1;
        }

        public AddNewClient(){}

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            clientDB.InsertIntoClient(new ClientDetails(TextBox1.Text));
            Response.Write("<script>alert('Data inserted successfully')</script>");
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["Trucking"].ConnectionString;
            SqlConnection connect = new SqlConnection(connectionString);
            string command = "Select * from Client";

            SqlDataAdapter adapter = new SqlDataAdapter(command, connect);

            DataSet dataset = new DataSet();
            adapter.Fill(dataset, "Client");

            foreach(DataRow row in dataset.Tables["Client"].Rows)
            {
                Label1.Text += $"<li> <em>{row["clientName"]:d}</em> </br>";

            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {                     
            clientDB.DeleteClient(new ClientDetails(TextBox1.Text));
            Response.Write("<script>alert('Data deleted successfully')</script>");
            GridView1.DataBind();

        }
    }
}