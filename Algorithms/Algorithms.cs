using System;
using System.Linq

namespace DeveloperSample.Algorithms
{
    public static class Algorithms
    {
        public static int GetFactorial(int n) 
        {
            if(n>0)
            {
                int fact = n;
                for (int i = n - 1; i >= 1; i--)
                {
                    fact = fact * i;
                }
            }
            else{
                throw new ArgumentException("fcatorial must be greater than 0");
            }
        }

        public static string FormatSeparators(params string[] items)
        {
            if(String.IsNullOrEmpty(items))
                 return string.Empty;
                 
             if(Items.Length == 1) return items[0];
             if(Items.Length == 2) return $"{items[0]} and {items[1]}";
             return string.Join(", ", items.Take(items.Length-1))+" and " + items.Last();            
        }
    }
}
