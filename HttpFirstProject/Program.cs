using HttpFirstProject;
using Newtonsoft.Json;

try
{
    Console.WriteLine("Kaysi valyutada kaysinga masalan ->> dollordan yevroga");

    string kurs1 = Console.ReadLine();
    string kurs2 = Console.ReadLine();

    Console.WriteLine($"{kurs1} summani kiriting");
    float sum = float.Parse(Console.ReadLine());

    HttpClient httpclient = new HttpClient();
    var rquest = new HttpRequestMessage(HttpMethod.Get, "https://nbu.uz/exchange-rates/json/");
    var response = await httpclient.SendAsync(rquest);

    var body = await response.Content.ReadAsStringAsync();

    var courses = JsonConvert.DeserializeObject<List<Kurslar>>(body);


    float summ = 0F;
    float summ2 = 0F;

    foreach (var i in courses)
    {
        
        if (kurs1.ToUpper() == i.code)
        {

            summ = i.cb_price;

        }
        else if (kurs2.ToUpper() == i.code)
        {
            summ2 = i.cb_price;

        }
    }

    float all = summ * sum / summ2 + summ * sum % summ2;

    Console.WriteLine($"{all} {kurs2}");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}