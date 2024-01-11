using System;

namespace DeveloperSample.Algorithms
{
    public static class Algorithms
    {
        public static int GetFactorial(int n)
        {
            if (n < 0)
                throw new ArgumentException("Factorial is not defined for negative numbers.");

            // Base case: factorial of 0 or 1 is 1
            if (n == 0 || n == 1)
            {
                return 1;
            }

            // Recursive case: n! = n * (n-1)!
            return n * GetFactorial(n - 1);
        }

        public static string FormatSeparators(params string[] items)
        {
            try
            {
                //If items are null or none, return empty string
                if (items == null || items.Length == 0)
                    return string.Empty;

                if (items?.Length == 1)
                    return items[0];

                var part1 = string.Join(", ", items, 0, items.Length - 1);
                var part2 = items[items.Length - 1];
                return $"{part1} and {part2}";
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error occured. Details:{e.Message}");
                throw;
            }
        }
    }
}