using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void MyStoredProcedure (SqlDateTime StartDate, SqlDateTime EndDate)
    {
        SqlCommand command = new SqlCommand();
        command.Connection = new SqlConnection("Context connection = true");
        command.Connection.Open();
        string sql_string = "select * from History where createAt between @StartDate and @EndDate";
        //Получить объект StringBuilder
        command.CommandText = sql_string.ToString();
        //присвоить параметры @StartDate and @EndDate
        SqlParameter parameter = command.Parameters.Add("@StartDate", SqlDbType.DateTime);
        parameter.Value = StartDate;
        parameter = command.Parameters.Add("@EndDate", SqlDbType.DateTime);
        parameter.Value = EndDate;
        SqlContext.Pipe.ExecuteAndSend(command);
        command.Connection.Close();
    }
}
