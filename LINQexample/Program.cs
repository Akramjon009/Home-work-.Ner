namespace LINQexample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Пример данных для использования в LINQ запросах
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            List<int> numbers2 = new List<int> { 3, 4, 5, 6, 7 };
            List<string> names = new List<string> { "John", "Alice", "Bob", "David", "Eva" };
            List<int> grades = new List<int> { 85, 70, 90, 75 };

            // Select
            var squares = numbers.Select(num => num * num);
            Console.WriteLine("Select:");
            foreach (var square in squares)
            {
                Console.WriteLine(square);
            }
            Console.WriteLine();

            // Where
            var evenNumbers = numbers.Where(num => num % 2 == 0);
            Console.WriteLine("Where:");
            foreach (var num in evenNumbers)
            {
                Console.WriteLine(num);
            }
            Console.WriteLine();

            // OrderBy
            var sortedNames = names.OrderBy(name => name);
            Console.WriteLine("OrderBy:");
            foreach (var name in sortedNames)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            // All
            bool allGreaterThanZero = numbers.All(num => num > 0);
            Console.WriteLine("All:");
            Console.WriteLine(allGreaterThanZero);
            Console.WriteLine();

            // Any
            bool anyGreaterThanTen = numbers.Any(num => num > 10);
            Console.WriteLine("Any:");
            Console.WriteLine(anyGreaterThanTen);
            Console.WriteLine();

            // Count
            int countOfNumbers = numbers.Count();
            Console.WriteLine("Count:");
            Console.WriteLine(countOfNumbers);
            Console.WriteLine();

            // Distinct
            var distinctNumbers = numbers.Concat(numbers2).Distinct();
            Console.WriteLine("Distinct:");
            foreach (var num in distinctNumbers)
            {
                Console.WriteLine(num);
            }
            Console.WriteLine();

            // Sum
            int sumOfGrades = grades.Sum();
            Console.WriteLine("Sum:");
            Console.WriteLine(sumOfGrades);
            Console.WriteLine();

            // Max
            int maxGrade = grades.Max();
            Console.WriteLine("Max:");
            Console.WriteLine(maxGrade);
            Console.WriteLine();

            // Min
            int minGrade = grades.Min();
            Console.WriteLine("Min:");
            Console.WriteLine(minGrade);
            Console.WriteLine();

            // Take
            var firstThreeNumbers = numbers.Take(3);
            Console.WriteLine("Take:");
            foreach (var num in firstThreeNumbers)
            {
                Console.WriteLine(num);
            }
            Console.WriteLine();

            // Skip
            var skipFirstTwoNumbers = numbers.Skip(2);
            Console.WriteLine("Skip:");
            foreach (var num in skipFirstTwoNumbers)
            {
                Console.WriteLine(num);
            }
            Console.WriteLine();

            // Concat
            var concatenatedLists = numbers.Concat(numbers2);
            Console.WriteLine("Concat:");
            foreach (var num in concatenatedLists)
            {
                Console.WriteLine(num);
            }
            Console.WriteLine();
        }
    }
}
