using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
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
            var groups = ReadInput();
            long totalYesCount = 0;
            foreach (var g in groups)
            {
                System.Console.WriteLine(g);
                totalYesCount += g.AnyCount;
            }
            System.Console.WriteLine($"Total 'Yes' count: {totalYesCount}");
        }

        private static void Part2()
        {
            var groups = ReadInput();
            long totalYesCount = groups.Sum(g => g.AllCount);
            System.Console.WriteLine($"Total 'Yes' count: {totalYesCount}");
        }

        private static List<Group> ReadInput()
        {
            var groups = new List<Group>();
            var lines = File.ReadAllLines("input.txt");
            Group group = null;
            foreach (var line in lines)
            {
                if (group == null)
                {
                    group = new Group();
                    groups.Add(group);
                }
                if (string.IsNullOrEmpty(line))
                {
                    group = null;
                }
                else
                {
                    foreach (var c in line)
                    {
                        group.AddAnswer(c);
                    }
                    group.Size++;
                }
            }
            return groups;
        }
    }
}