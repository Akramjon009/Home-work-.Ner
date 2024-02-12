using Chat;
public class Program
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
                if (Class1.CheckName(pgConnector, name))
                {
                    Console.WriteLine("Enter your password");
                    string password = Console.ReadLine();

                    if (Class1.Checkpassword(pgConnector, password))
                    {
                        Class1.GetAll(pgConnector);
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
                                Class1.CreateMessage(pgConnector, name, message);
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
                if (!Class1.CheckName(pgConnector, name))
                {
                    Console.WriteLine("Enter password");
                    string password = Console.ReadLine();
                    Thread.Sleep(2000);
                    Console.Clear();
                    Class1.Create(pgConnector, name, password);
                    Class1.GetAll(pgConnector);
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
                            Class1.CreateMessage(pgConnector, name, message);
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
            Thread.Sleep(2000);
            Console.Clear();
    
        }

    }
}