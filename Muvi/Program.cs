using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Movies
{
    public class Program
    {
        public static async Task Main()
        {
            string film = Console.ReadLine();
            HttpClient httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://www.omdbapi.com/?apikey=ed7b702d&s={film}");
            var response = await httpClient.SendAsync(request);
            string Body = await response.Content.ReadAsStringAsync();

            JObject search = JsonConvert.DeserializeObject<JObject>(Body)!;
            JArray muvies = (JArray)search["Search"];
            foreach (var Item in muvies) 
            {
                Console.WriteLine(Item);
            }

        }
    }
}