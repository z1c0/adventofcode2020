using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day7
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
            var rules = ReadInput();            
            var number = CountBags(rules, "shiny gold") - 1;
            System.Console.WriteLine($"Total bags {number}");
		}

		private static void Part1()
        {
            var rules = ReadInput();
            var fits = Find(rules.Keys, rules, "shiny gold");
            System.Console.WriteLine($"Total fits {fits}");
        }

        private static int CountBags(Dictionary<string, Dictionary<string, int>> rules, string color)
        {
            var count = 0;
            if (rules.ContainsKey(color))
            {
                count++;
                foreach (var e in rules[color])
                {
                    var multi = e.Value;
                    count += multi * CountBags(rules, e.Key);
                }
            }
            return count;
        }

		private static int Find(IEnumerable<string> keys, Dictionary<string, Dictionary<string, int>> rules, string bag)
		{
            int fits = 0;
            foreach (var k in keys)
            {                
                var v = rules[k];
                if (v.ContainsKey(bag))
                {
                    fits++;
                }
                else
                {
                    if (Find(v.Keys, rules, bag) > 0)
                    {
                        fits++;
                    }
                }                
            }
            return fits;
		}

		private static Dictionary<string, Dictionary<string, int>> ReadInput()
        {
            var rules = new Dictionary<string, Dictionary<string, int>>();
            var lines = File.ReadAllLines("input.txt");
            foreach (var l in lines)
            {
                var position = 0;
                var tokens = l.Split(" ");
                var color = ParseColor(tokens, ref position);
                Expect(tokens, ref position, "contain");
                var contents = new Dictionary<string, int>();
                do
                {
                    var amount = ParseNumber(tokens, ref position);
                    if (amount > 0)
                    {
                        var col = ParseColor(tokens, ref position);
                        //System.Console.WriteLine($"Add {amount} {col} to {color}");
                        contents.Add(col, amount);
                    }
                } 
                while (position < tokens.Length);
                rules.Add(color, contents);
            }
            return rules;
        }

		private static int ParseNumber(string[] tokens, ref int position)
		{
            var token = tokens[position++];
            if (token == "no")
            {
                Expect(tokens, ref position, "other");
                Expect(tokens, ref position, "bags.");
                return 0;
            }
			return Int32.Parse(token);
		}

		private static string ParseColor(string[] tokens, ref int position)
		{
			var color = $"{tokens[position]} {tokens[position + 1]}";
            position += 2;
            Expect(tokens, ref position, "bag");
            return color;
		}

		private static void Expect(string[] tokens, ref int position, string expected)
		{
            var actual = tokens[position++];
			if (!actual.StartsWith(expected))
            {
                throw new Exception($"Invalid token ${tokens[position]} encountered, expected {expected}.");
            }
		}
	}
}