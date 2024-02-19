using Npgsql;
using System.Security.Cryptography;
using System.Text;


namespace Chat
{
    public  class Class1
    {
        public const int keySize = 64;
        public const int iterations = 350000;
        public static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
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
            int keySize = 64;
            int iterations = 350000;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {

                connection.Open();

                string query = $"select password,solt from users;";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

                var result = cmd.ExecuteReader();

                while (result.Read())
                {
                    if (DeHashPassword(password, result[0].ToString(), result[1].ToString(),keySize,iterations,hashAlgorithm))
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
            password = HashPasword(password, out byte[]? salt);
            string query = $"insert into users(name,password,solt) values('{name}','{password},'{Convert.ToHexString(salt)}')";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

            cmd.ExecuteNonQuery();
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

            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        } 

        private static bool DeHashPassword(
            string passwordFromUser,
            string hashFromPg,
            string saltAsStringFromPg,
            int keySizeFromProgram,
            int iterationsFromProgram,
            HashAlgorithmName hashAlgorithmFromProgram)
        {
            byte[] salt = Convert.FromHexString(saltAsStringFromPg);  

            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                password: passwordFromUser,
                salt,
                iterations: iterationsFromProgram,
                hashAlgorithm: hashAlgorithmFromProgram,
                outputLength: keySizeFromProgram);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hashFromPg));
        }
    }
}
