using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DB_Lab02
{
    public class CarriageDB
    {
        private string connectionString;

        public CarriageDB()
        {
            connectionString = WebConfigurationManager.ConnectionStrings["Trucking"].ConnectionString;
        }

        public CarriageDB(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddCarriage(CarriageDetails carriage)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("AddCarriage", con);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@idClient", SqlDbType.Int, 4));
            command.Parameters["idClient"].Value = carriage.IDClient;

            command.Parameters.Add(new SqlParameter("@idGoods", SqlDbType.Int, 4));
            command.Parameters["idGoods"].Value = carriage.IDGoods;

            command.Parameters.Add(new SqlParameter("@idTransport", SqlDbType.Int, 4));
            command.Parameters["idTransport"].Value = carriage.IDTransport;

            command.Parameters.Add(new SqlParameter("@idCity", SqlDbType.Int, 4));
            command.Parameters["idCity"].Value = carriage.IDCity;

            command.Parameters.Add(new SqlParameter("@dateOfDelivery",SqlDbType.DateTime));
            command.Parameters["dateOfDelivery"].Value = carriage.DeteOfDelivery;

            command.Parameters.Add(new SqlParameter("@typeOfService", SqlDbType.NVarChar, 60));
            command.Parameters["typeOfService"].Value = carriage.TypeOfService;

            try
            {
                con.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw new ApplicationException("ошибка данных");
            }

            finally
            {
                con.Close();
            }
        }

        public List<CarriageDetails> GetAllCarriage()
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetAllCarriage", con);
            cmd.CommandType = CommandType.StoredProcedure;

            List<CarriageDetails> carriages = new List<CarriageDetails>();

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    CarriageDetails carriage = new CarriageDetails((int)reader["idClient"], (int)reader["idGoods"],
                        (int)reader["idTransport"], (int)reader["idCity"], (DateTime)reader["dateOfDelivery"], (string)reader["typeOfService"]);
                    carriages.Add(carriage);
                }
                reader.Close();
                return carriages;
            }

            catch(SqlException)
            {
                throw new ApplicationException("Ошибка данных");
            }
            finally
            {
                con.Close();
            }
        }


        
    }
}