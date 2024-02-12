
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

                password = HashPasword(password, out var salt);
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
        public static void Create(string connectionString, string name, string password)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            password = HashPasword(password, out var salt);
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
        
            
        public static string HashPasword(string password, out byte[] salt)
        {
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
            const int keySize = 64;
            const int iterations = 350000;
                salt = RandomNumberGenerator.GetBytes(keySize);
                var hash = Rfc2898DeriveBytes.Pbkdf2(
                    Encoding.UTF8.GetBytes(password),
                    salt,
                    iterations,
                    hashAlgorithm,
                    keySize);
                return Convert.ToHexString(hash);
        } 

        
    }
}
