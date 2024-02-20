namespace Home_work
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4,5, 6, 7, 8, 9, 10 };
            int i = 0;

            var res = numbers.Where(x => { i ++; return i >= 4 && i <= 6; }).ToList();

            foreach (int x in res)
            {
                Console.WriteLine(x);
            }

        }
    }           
}
