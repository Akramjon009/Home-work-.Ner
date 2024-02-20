namespace LINQsecond
{
    internal class Program
    {
        static void Main(string[] args)
        {

            IEnumerable<string> stringList = ["a fkgfjkgfgmk a", "a", "adddddd", "ddddddda"];
            char c = 'a';
            var n = Task1(c, stringList);
            foreach (var i in n)
            {
                Console.WriteLine(i);
            }
        }
        public static IEnumerable<string> Task1(char c, IEnumerable<string> n)
        {

            return n.Select(i => i.ToString()).Where(i => i.StartsWith(c) && i.EndsWith(c) && i.Length > 1);
        }

    }
}
