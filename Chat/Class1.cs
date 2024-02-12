
using Npgsql;
using System.Security.Cryptography;
using System.Text;


namespace Chat
{
    public  class Class1
    {
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

                password = HashPasword(password);
                while (result.Read())
                {
                    if (password.ToLower().Trim() == result[0].ToString().Trim().ToLower())
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
        public static void Create(string connectionString, string name, string password)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            password = HashPasword(password);
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
        
            
        public static string HashPasword(string password)
        {
            StringBuilder builder = new StringBuilder();
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // Convert byte to hexadecimal string
                }
            }
            return builder.ToString();
        } 

        
    }
}
