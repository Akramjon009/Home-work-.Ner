using System;
using static Check;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var a = new Person("John");
            var b = new Person("John");

            Console.WriteLine(a.Equals(b)); 
        }
    }
}