using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LINQLesson
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Info info = new Info();
            foreach (var item in info.GetCenterByNameWithExperience())
            {
                Console.WriteLine(item.FirstName + " " + item.studyType);
            }
        }
    }
}
