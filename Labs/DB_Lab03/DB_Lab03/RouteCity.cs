using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.Server;


[Serializable]  
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.UserDefined, MaxByteSize = 8000)]
public struct RouteCity: INullable, IBinarySerialize
{
    String city1;
    String city2;

    public override string ToString()
    {
        return $"Route: {city1.ToString()}";   
    }
    
    public bool IsNull
    {
        get
        {
            return _null;
        }
    }
    
    public static RouteCity Null
    {
        get
        {
            RouteCity h = new RouteCity();
            h._null = true;
            return h;
        }
    }
    
    public static RouteCity Parse(SqlString s)
    {
        string[] sourec = s.Value.Split(',');
        if (s.IsNull)
            return Null;

        RouteCity u = new RouteCity();
        u.city1 = Convert.ToString(sourec[0]);
        u.city2 = Convert.ToString(sourec[1]);
        return u;
    }
    
    // Это метод-заполнитель
    public string Method1()
    {
        // Введите здесь код
        return string.Empty;
    }
    
    // Это статический метод заполнителя
    public static SqlString Method2()
    {
        // Введите здесь код
        return new SqlString("");
    }

    public void Read(BinaryReader r)
    {
          city1 = r.ReadString();
    }

    public void Write(BinaryWriter w)
    {
        w.Write( city1.ToString() + " - " + city2.ToString());
    }

    // Это поле элемента-заполнителя
    public int _var1;
 
    // Закрытый член
    private bool _null;
}