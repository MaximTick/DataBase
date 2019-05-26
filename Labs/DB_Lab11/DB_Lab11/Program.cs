using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace DB_Lab11
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1 select");
            Console.WriteLine("2 delete");
            Console.WriteLine("3 update");
            Console.WriteLine("4 insert");

            int number = Convert.ToInt32(Console.ReadLine());
                switch (number)
                {
                    case 1:
                        Select();
                        break;
                    case 2:
                        Delete();
                        break;
                    case 3:
                        Update();
                        break;
                    case 4:
                        Insert();
                        break;
                    default:
                        Console.WriteLine("default");
                        break;
                }
            
            //if (!File.Exists(@"Lab11.db"))
            //{
            //    SQLiteConnection.CreateFile(@"Lab11.db");
            //}

            void Insert()
            {
                Console.Write("Добавим город в БД: ");
                string cityName = Console.ReadLine();

                using (SQLiteConnection Connect = new SQLiteConnection(@"Data Source=Lab11.db; Version=3;"))
                {
                    // в запросе есть переменные, они начинаются на @, обратите на это внимание
                    string commandText = "INSERT INTO [City] ([city]) VALUES(@city)";
                    SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                    Command.Parameters.AddWithValue("@city", cityName); // присваиваем переменной значение
                    Connect.Open();
                    Command.ExecuteNonQuery();
                    Connect.Close();
                }
            }


            void Select()
            {
                List<string> cityList = new List<string>();
                using (SQLiteConnection Connect = new SQLiteConnection(@"Data Source=Lab11.db; Version=3;"))
                {
                    try
                    {
                        Connect.Open();
                        SQLiteCommand Command = new SQLiteCommand
                        {
                            Connection = Connect,
                            CommandText = @"SELECT * FROM [City]"
                        };
                        SQLiteDataReader sqlReader = Command.ExecuteReader();
                        string _dbCity = null;
                        string _dbId = null;
                        while (sqlReader.Read()) // считываем и вносим в лист все параметры
                        {
                            _dbCity = sqlReader["city"].ToString();
                            _dbId = sqlReader["id"].ToString();
                            cityList.Add(_dbId + " " + _dbCity); // добавляем в List
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        Connect.Close();
                    }
                }

                foreach (var list in cityList)
                    Console.WriteLine(list);
            }
            


            void Update()
            {
                using (SQLiteConnection Connect = new SQLiteConnection(@"Data Source=Lab11.db; Version=3;"))
                {
                    string commandText = "UPDATE [City] SET [city] = @value WHERE [id] = @id";
                    SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                    Console.WriteLine("Введите id замены");
                    var id = Console.ReadLine();
                    Console.WriteLine("Введите новое имя города");
                    var city = Console.ReadLine();
                    Command.Parameters.AddWithValue("@value", city);
                    Command.Parameters.AddWithValue("@id", id); // присваиваем переменной номер (id) записи, которую будем обновлять
                    Connect.Open();
                    Int32 _rowsUpdate = Command.ExecuteNonQuery(); // sql возвращает сколько строк обработано
                    Console.WriteLine("Обновлено строк: " + _rowsUpdate);
                    Connect.Close();
                }
            }

            void Delete()
            {
                using (SQLiteConnection Connect = new SQLiteConnection(@"Data Source=Lab11.db; Version=3;"))
                {
                    string commandText = "DELETE FROM [City] WHERE [id] = @id LIMIT 1";
                    SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                    Console.WriteLine("Введите id для удаления");
                    var id = Console.ReadLine();
                    Command.Parameters.AddWithValue("@id", id);
                }
            }
        }
    }
}
