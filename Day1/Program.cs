using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day1
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
            var numbers = ReadNumbers().ToArray();
            for (var i = 0; i < numbers.Length; i++)
            {
                var x = numbers[i];
                for (var j = i + 1; j < numbers.Length; j++)
                {
                    var y = numbers[j];
                    if (x + y == 2020)
                    {
                        System.Console.WriteLine($"{x} + {y} == 2020 => {x} * {y} == {x * y}");
                    }
                }
            }
        }

        private static void Part2()
        {
            var numbers = ReadNumbers().ToArray();
            for (var i = 0; i < numbers.Length; i++)
            {
                var x = numbers[i];
                for (var j = i + 1; j < numbers.Length; j++)
                {
                    var y = numbers[j];
                    for (var k = j + 1; k < numbers.Length; k++)
                    {
                        var z = numbers[k];
                        if (x + y + z == 2020)
                        {
                            System.Console.WriteLine($"{x} + {y} {z} == 2020 => {x} * {y} * {z} == {x * y * z}");
                        }
                    }
                }
            }
        }

        private static IEnumerable<int> ReadNumbers()
        {
            var lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                yield return Int32.Parse(line);
            }
        }
    }
}
