using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void DriverPivot (Int32 StartYear, Int32 EndYear )
    {
        SqlCommand command = new SqlCommand();
        command.Connection = new SqlConnection("Context connection = true");
        command.Connection.Open();
        string sql_string = @"select distinct  lastName, [2015], [2016], [2017] from (select lastName, [year], salary 
                            from Driver where [year] between @StartYear and @EndYear) as salary PIVOT (sum(salary) 
                            for [year] in([2015], [2016], [2017])) as my_pivot group by lastName, [2015], [2016], [2017]
                            order by lastName";

        //Получить объект StringBuilder
        command.CommandText = sql_string.ToString();
        SqlParameter parameter = command.Parameters.Add("@StartYear", SqlDbType.Int);
        parameter.Value = StartYear;
        parameter = command.Parameters.Add("@EndYear", SqlDbType.Int);
        parameter.Value = EndYear;
        SqlContext.Pipe.ExecuteAndSend(command);
        command.Connection.Close();
    }
}
