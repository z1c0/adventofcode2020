using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("START");
            Solve(2020);
            Solve(30000000);
            System.Console.WriteLine("DONE");
        }

		private static void Solve(int maxRound)
        {
            var numbers = ReadInput();
            var history = new Dictionary<int, Tuple<int, int>>();
            var round = 1;
            var lastSpoken = 0;
            foreach (var n in numbers)
            {
                history[n] = new Tuple<int, int>(round++, 0);
                lastSpoken = n;
            }
            while (round <= maxRound)
            {
                if (history.TryGetValue(lastSpoken, out var value))
                {
                    if (value.Item2 == 0) // first time?
                    {
                        lastSpoken = 0;
                        Remember(history, lastSpoken, round);
                    }
                    else
                    {
                        lastSpoken = value.Item1 - value.Item2;
                        Remember(history, lastSpoken, round);
                    }
                }
                round++;
            }
            System.Console.WriteLine($"Last number spoken after {maxRound} rounds is {lastSpoken}" );
        }

        private static void Remember(Dictionary<int, Tuple<int, int>> dictionary, int number, int round)
        {
            if (dictionary.TryGetValue(number, out var value))
            {
                dictionary[number] = new Tuple<int, int>(round, value.Item1);
            }
            else
            {
                dictionary.Add(number, new Tuple<int, int>(round, 0));
            }
        }

        private static List<int> ReadInput()
        {
            var line = File.ReadAllText("input.txt");
            var tokens = line.Split(",");
            var numbers = new List<int>();
            foreach (var t in tokens)
            {
                numbers.Add(int.Parse(t));
            }
            return numbers;
        }
    }
}