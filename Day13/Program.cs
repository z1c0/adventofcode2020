using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Day13
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

		private static void Part2()
		{
            var input = ReadInput();
            var boffs = new List<Tuple<int, int>>();
            for (var i = 0; i < input.Item2.Count; i ++)
            {
                var l = input.Item2[i];
                if (l >= 0)
                {
                    boffs.Add(new Tuple<int, int>(i, input.Item2[i]));
                }
            }
            long minutes = boffs.First().Item2;
            long stride = minutes;
            foreach (var t in boffs.Skip(1))
            {
                var offs = t.Item1;
                var val = t.Item2;
                while ((minutes + offs) % val != 0)
                {
                    minutes += stride;
                }
                stride *= val;
            }
            System.Console.WriteLine($"timestamp: {minutes}");

            /*
            var input = ReadInput();
            long timestamp = 0;
            var max = input.Item2.Max();
            var indexMax = input.Item2.IndexOf(max);
            timestamp -= indexMax;
            while (timestamp < 100000000000000L)
            {
                timestamp += max;
                var allValid = true;
                for (var i = 0; i < input.Item2.Count; i ++)
                {
                    var l = input.Item2[i];
                    if (l >= 0)
                    {
                        if ((timestamp + i) % l != 0)
                        {
                            allValid = false;
                            break;
                        }
                    }
                }
                if (allValid)
                {
                    System.Console.WriteLine($"timestamp: {timestamp}");
                    break;
                }
            }
            */
		}

		private static void Part1()
        {
            var input = ReadInput();
            var timestamp = input.Item1;
            var minDiff = Int32.MaxValue;
            var busId = -1;
            foreach (var i in input.Item2)
            {
                if (i >= 0)
                {
                    var times = timestamp / i;
                    var next = i * (times + 1);
                    var diff = next - timestamp;
                    if (diff < minDiff)
                    {
                        minDiff = diff;
                        busId = i;
                    }
                }
            }
            System.Console.WriteLine($"minDiff: {minDiff}, busId: {busId} -> {minDiff * busId}");
        }

        private static Tuple<int, List<int>> ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            var timestamp = Int32.Parse(lines[0]);
            var list = new List<int>();
            var tokens = lines[1].Split(",");
            foreach (var t in tokens)
            {
                list.Add(t != "x" ? Int32.Parse(t) : -1);
            }
            return new Tuple<int, List<int>>(timestamp, list);
        }
    }
}