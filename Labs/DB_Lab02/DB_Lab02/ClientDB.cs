using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace DB_Lab02
{
    public class ClientDB
    {
        private string connectionString;

        public ClientDB()
        {
            connectionString = WebConfigurationManager.ConnectionStrings["Trucking"].ConnectionString;
        }

        public ClientDB(string connectionString)
        {
            this.connectionString = connectionString;
        }
        
        public void DeleteClient(ClientDetails client)
        {
            string sql = String.Format("Delete from Client where clientName = '{0}'", client.ClientName);
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, connection);

            command.CommandType = CommandType.Text;
            command.Parameters.Add((new SqlParameter("@clientName", SqlDbType.NVarChar, 70)));
            command.Parameters["@clientName"].Value = client.ClientName;
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Exception error = new Exception("Этот клиент имеет заказ. Удаление невозможно", ex);
                throw error;
            }
            finally { connection.Close(); }
        }

        public void InsertIntoClient(ClientDetails client)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("InsertIntoClient", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@clientName", SqlDbType.NVarChar, 70));
            command.Parameters["@clientName"].Value = client.ClientName;

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                //return (int)command.Parameters["@id"].Value;
            }
            catch(SqlException)
            {
                throw new ApplicationException("Error data");
            }

            finally
            {
                connection.Close();
            }
        }



    }
}