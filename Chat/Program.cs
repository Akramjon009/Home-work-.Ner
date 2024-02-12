using Npgsql;
using System.Transactions;

internal class Chat
{
    public static void Main(string[] args)
    {
       
        string pgConnector = "Host=localhost;Port=5432;Database=TestDB;username=postgres;Password=Akramjon_09;";
        while (true)
        {
            Console.WriteLine("1)Login in 2)Create 3)End");
            string check = Console.ReadLine();
            if ("1" == check)
            {
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("Enter your name");
                string name = Console.ReadLine();
                if (CheckName(pgConnector, name))
                {
                    Console.WriteLine("Enter your password");
                    string password = Console.ReadLine();

                    if (Checkpassword(pgConnector, password))
                    {
                        GetAll(pgConnector);
                        while (true)
                        {
                            Console.WriteLine("1)Enter message 2)Back");
                            string userscheack = Console.ReadLine();
                            if ("1" == userscheack)
                            {
                                Console.WriteLine("enter message: ");
                                string message = Console.ReadLine();
                                Thread.Sleep(2000);
                                Console.Clear();
                                CreateMessage(pgConnector, name, message);
                            }
                            else if ("2" == userscheack)
                            {
                                break;
                            }
                            else { Console.WriteLine("Enter right "); }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong password ");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong name ");
                }
            }
            else if (check == "2")
            {
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("Enter your name ");
                string name = Console.ReadLine();
                if (!CheckName(pgConnector, name))
                {
                    Console.WriteLine("Enter password");
                    string password = Console.ReadLine();
                    Thread.Sleep(2000);
                    Console.Clear();
                    Create(pgConnector, name, password);
                    GetAll(pgConnector);
                    while (true)
                    {
                        Console.WriteLine("1)Enter message 2)Back");
                        string userscheack = Console.ReadLine();
                        if ("1" == userscheack)
                        {
                            Console.WriteLine("enter message: ");
                            string message = Console.ReadLine();
                            Thread.Sleep(2000);
                            Console.Clear();
                            CreateMessage(pgConnector, name, message);
                        }
                        else if ("2" == userscheack)
                        {
                            break;
                        }
                        else { Console.WriteLine("Enter right "); }
                    }

                }

            }
            else if (check == "3") 
            {
                break;
            }
            else
            {
                Console.WriteLine("Enter right number ");
            }
            Thread.Sleep (2000);
            Console.Clear();

        }

    }
    public static bool CheckName(string connectionString, string name)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();


            string query = $"select  name from users;";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

            var result = cmd.ExecuteReader();


            while (result.Read())
            {
                Console.WriteLine(result[0].ToString());
                if (name == result[0].ToString().Trim()) 
                {
                    return true;
                }
            }
            return false;

        }
    }
    public static bool Checkpassword(string connectionString, string password)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {

            connection.Open();

            string query = $"select password from users;";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

            var result = cmd.ExecuteReader();


            while (result.Read())
            {
                if (password == result[0].ToString().Trim())
                {
                    return true;
                }
            }
            return false;

        }
    }
    public static void GetAll(string connectionString)
    {

        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        { 
            connection.Open();
            string query = $"select * from Message;";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

            var result = cmd.ExecuteReader();

           
            while (result.Read())
            {
                Console.WriteLine(result[0] + "\n\t" + result[1]);
            }


        }
    }
    public static void Create(string connectionString,string name,string password) 
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();
        string query = $"insert into users(name,password) values('{name}','{password}')";
        using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

        var result = cmd.ExecuteReader();
        connection.Close();
    }
    public static void CreateMessage(string connectionString, string name, string message)
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();
        string query = $"insert into message(name,message) values('{name}','{message}')";
        using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

        var result = cmd.ExecuteReader();
        GetAll(connectionString);
    }
}