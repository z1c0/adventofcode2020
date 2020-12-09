using System;
using System.Collections.Generic;
using System.IO;

namespace Day9
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
            var result = Scan(numbers, 25);
            System.Console.WriteLine(result);
        }

        private static void Part2()
        {
            var numbers = ReadInput();
            var invalid = Scan(numbers, 25);
            var result = FindMinMax(numbers, invalid);
            System.Console.WriteLine($"{result.Item1} + {result.Item2} = {result.Item1 + result.Item2}");
        }

        private static Tuple<long, long> FindMinMax(List<long> numbers, long target)
        {
            long sum;
            long min = 0;
            long max = 0;
            for (var i = 0; i < numbers.Count; i++)
            {
                min = max = sum = numbers[i];
                for (var j = i + 1; j < numbers.Count; j++)
                {
                    var n = numbers[j];
                    min = Math.Min(n, min);
                    max = Math.Max(n, max);
                    sum += n;
                    if (sum == target)
                    {
                        return new Tuple<long, long>(min, max);
                    }
                    else if (sum > target)
                    {
                        break;
                    }
                }
            }
            return new Tuple<long, long>(-1, -1);
        }

        private static long Scan(List<long> numbers, int preambleLength)
        {
            var start = 0;
            for (var i = preambleLength; i < numbers.Count; i++)
            {
                var n = numbers[i];
                if (!FindSum(numbers, start, start + preambleLength, n))
                {
                    return n;
                }
                start++;
            }
            return -1;
        }

        private static bool FindSum(List<long> numbers, int start, int end, long number)
        {
            for (var i = start; i < end; i++)
            {
                for (var j = i + 1; j < end; j++)
                {
                    if (numbers[i] + numbers[j] == number)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static List<long> ReadInput()
        {
            var numbers = new List<long>();
            var lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                numbers.Add(Int64.Parse(line));
            }
            return numbers;
        }
    }
}