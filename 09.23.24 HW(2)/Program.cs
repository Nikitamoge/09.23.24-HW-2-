using System;
using System.Collections.Generic;
using System.Linq;


//Task 7
public static class StringExtensions
{
    public static bool IsValidBrackets(this string input)
    {
        var stack = new Stack<char>();
        var brackets = new Dictionary<char, char> { { '(', ')' }, { '{', '}' }, { '[', ']' } };

        foreach (var ch in input)
        {
            if (brackets.ContainsKey(ch))
            {
                stack.Push(ch);
            }
            else if (brackets.ContainsValue(ch))
            {
                if (stack.Count == 0 || brackets[stack.Pop()] != ch)
                {
                    return false;
                }
            }
        }

        return stack.Count == 0;
    }
}


//Task 8
public static class ArrayExtensions
{
    public static int[] Filter(this int[] array, Predicate<int> condition)
    {
        return array.Where(n => condition(n)).ToArray();
    }


    class Program
    {
        //Task 1
        public delegate IEnumerable<int> NumberOperation(int[] numbers);


        //Task 2
        public delegate void DisplayAction();
        public delegate double CalculateArea(double a, double b);

        static void Main(string[] args)
        {
            //Task 1
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            NumberOperation evenNumbers = GetEvenNumbers;
            NumberOperation oddNumbers = GetOddNumbers;
            NumberOperation primeNumbers = GetPrimeNumbers;
            NumberOperation fibonacciNumbers = GetFibonacciNumbers;

            Console.WriteLine("Even Numbers: " + string.Join(", ", evenNumbers(numbers)));
            Console.WriteLine("Odd Numbers: " + string.Join(", ", oddNumbers(numbers)));
            Console.WriteLine("Prime Numbers: " + string.Join(", ", primeNumbers(numbers)));
            Console.WriteLine("Fibonacci Numbers: " + string.Join(", ", fibonacciNumbers(numbers)));


            //Task 2
            DisplayAction showCurrentTime = ShowCurrentTime;
            DisplayAction showCurrentDate = ShowCurrentDate;
            DisplayAction showCurrentDayOfWeek = ShowCurrentDayOfWeek;
            CalculateArea calculateTriangleArea = CalculateTriangleArea;
            CalculateArea calculateRectangleArea = CalculateRectangleArea;

            showCurrentTime();
            showCurrentDate();
            showCurrentDayOfWeek();
            Console.WriteLine("Triangle Area: " + calculateTriangleArea(3, 4));
            Console.WriteLine("Rectangle Area: " + calculateRectangleArea(5, 6));


            //Task 3
            Func<string, string, bool> containsWord = (text, word) => text.Contains(word);

            string sampleText = "The quick brown fox jumps over the lazy dog.";
            string wordToCheck = "fox";

            Console.WriteLine($"Does the text contain the word '{wordToCheck}'? " + containsWord(sampleText, wordToCheck));


            //Task 4
            int[] num = { 7, 14, 21, 22, 30, 35, 49, 50 };
            Func<int[], int> countMultiplesOfSeven = (arr) => arr.Count(n => n % 7 == 0);

            Console.WriteLine("Count of numbers multiple of seven: " + countMultiplesOfSeven(num));


            //Task 5
            int[] nums = { -1, 2, -3, 4, 5, -6, 7, 8, -9, 10 };
            Func<int[], int> countPositiveNumbers = (arr) => arr.Count(n => n > 0);

            Console.WriteLine("Count of positive numbers: " + countPositiveNumbers(nums));


            //Task 6
            int[] numbs = { -1, 2, -3, 4, -3, -6, 7, -8, -9, 10 };
            Func<int[], IEnumerable<int>> uniqueNegativeNumbers = (arr) => arr.Where(n => n < 0).Distinct();

            Console.WriteLine("Unique negative numbers: " + string.Join(", ", uniqueNegativeNumbers(numbs)));


            //Task 7
            string[] testStrings = { "{}[]", "(())", "[{}]", "[}", "[[{]}]" };

            foreach (var str in testStrings)
            {
                Console.WriteLine($"Is the string '{str}' valid? " + str.IsValidBrackets());
            }


            //Task 8
            int[] numbes = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            int[] evenNumbers2 = numbes.Filter(n => n % 2 == 0);
            int[] oddNumbers2 = numbes.Filter(n => n % 2 != 0);

            Console.WriteLine("Even numbers: " + string.Join(", ", evenNumbers2));
            Console.WriteLine("Odd numbers: " + string.Join(", ", oddNumbers2));
        }


        //Task 1
        public static IEnumerable<int> GetEvenNumbers(int[] numbers)
        {
            return numbers.Where(n => n % 2 == 0);
        }

        public static IEnumerable<int> GetOddNumbers(int[] numbers)
        {
            return numbers.Where(n => n % 2 != 0);
        }

        public static IEnumerable<int> GetPrimeNumbers(int[] numbers)
        {
            return numbers.Where(IsPrime);
        }

        public static IEnumerable<int> GetFibonacciNumbers(int[] numbers)
        {
            HashSet<int> fibonacciSet = new HashSet<int>();
            int a = 0, b = 1, c = 0;
            while (c <= numbers.Max())
            {
                fibonacciSet.Add(c);
                c = a + b;
                a = b;
                b = c;
            }
            return numbers.Where(n => fibonacciSet.Contains(n));
        }

        public static bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }


        //Task 2
        public static void ShowCurrentTime()
        {
            Console.WriteLine("Current Time: " + DateTime.Now.ToString("hh:mm:ss tt"));
        }

        public static void ShowCurrentDate()
        {
            Console.WriteLine("Current Date: " + DateTime.Now.ToString("yyyy-MM-dd"));
        }

        public static void ShowCurrentDayOfWeek()
        {
            Console.WriteLine("Current Day of the Week: " + DateTime.Now.DayOfWeek);
        }

        public static double CalculateTriangleArea(double baseLength, double height)
        {
            return 0.5 * baseLength * height;
        }

        public static double CalculateRectangleArea(double width, double height)
        {
            return width * height;

        }
    }
}


