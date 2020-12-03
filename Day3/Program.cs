using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("START");
            long total = 1;
            total *= Slope(1, 1);
            total *= Slope(3, 1);
            total *= Slope(5, 1);
            total *= Slope(7, 1);
            total *= Slope(1, 2);
            System.Console.WriteLine($"Total: {total}");
            System.Console.WriteLine("DONE");
        }

        private static int Slope(int right, int down)
        {
            var forest = ReadInput();
            var height = forest.Count;
            var width = forest.First().Count;
            var x = 0;
            var y = 0;
            var treesFound = 0;
            while (y < height)
            {
                if (forest[y][x])
                {
                    treesFound++;
                }
                x = (x + right) % width;
                y += down;
            }
            Console.WriteLine($"Trees found for slope {right}/{down}: {treesFound}");
            return treesFound;
        }

        private static List<List<bool>> ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            var forest = new List<List<bool>>();
            foreach (var line in lines)
            {
                var treeLine = new List<bool>();
                foreach (char c in line)
                {
                    treeLine.Add(c == '#');
                }
                forest.Add(treeLine);
            }
            return forest;
        }
    }
}
