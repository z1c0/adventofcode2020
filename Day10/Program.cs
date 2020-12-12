using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("START");
            Part1();
            Part2();
            System.Console.WriteLine("DONE");
        }

        private static void Part1()
        {
            var numbers = ReadInput();
            var diff1 = 0;
            var diff3 = 0;
            for (var i = 1; i < numbers.Count; i++)
            {
                var diff = numbers[i] - numbers[i - 1];
                if (diff == 1)
                {
                    diff1++;
                }
                if (diff == 3)
                {
                    diff3++;
                }
            }
            System.Console.WriteLine($"diff1: {diff1}, diff3: {diff3} - diff1 * diff3 = {diff1 * diff3}");
        }

        private static void Part2()
        {
            var numbers = ReadInput();
            var cache = new Dictionary<int, long>();
            var combinations = TryList(numbers, 0, cache);
            System.Console.WriteLine($"combinations: {combinations}");
        }

        private static long TryList(List<int> numbers, int startPos, Dictionary<int, long> cache)
        {
            if (cache.ContainsKey(startPos))
            {
                return cache[startPos];
            }
            if (startPos == numbers.Count - 1)
            {
                return 1;
            }
            long combinations = 0;
            for (var i = startPos + 1; i < numbers.Count; i++)
            {
                if (numbers[i] - numbers[startPos] <= 3)
                {
                    combinations += TryList(numbers, i, cache);
                }
            }
            cache[startPos] = combinations;
            return combinations;
        }

        private static List<int> ReadInput()
        {
            var numbers = new List<int>();
            var lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                numbers.Add(Int32.Parse(line));
            }
            numbers.Add(0);
            numbers.Sort();
            numbers.Add(numbers.Last() + 3);
            return numbers;
        }
    }
}