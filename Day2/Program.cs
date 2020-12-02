using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day2
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
            var records = ReadNumbers().ToArray();
            System.Console.WriteLine($"Correct # of passwords: {records.Where(r => r.IsValid()).Count()} (method 1)");
        }
        private static void Part2()
        {
            var records = ReadNumbers().ToArray();
            System.Console.WriteLine($"Correct # of passwords: {records.Where(r => r.IsValidEx()).Count()} (method 2)");
        }

        private static IEnumerable<Record> ReadNumbers()
        {
            var lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                var tokens = line.Split(" ");
                var tokens2 = tokens[0].Split("-");
                yield return new Record {
                    Min = Int32.Parse(tokens2[0]),
                    Max = Int32.Parse(tokens2[1]),
                    Character = tokens[1][0],
                    Password = tokens[2]
                };
            }
        }
    }
}
