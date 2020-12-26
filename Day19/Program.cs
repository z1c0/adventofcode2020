using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Day19
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("START");
            Part1("input.txt");
            Part1("input2.txt");
            System.Console.WriteLine("DONE");
        }

		private static void Part1(string fileName)
        {
            var input = ReadInput(fileName);
            var rules = input.Item1;
            var messages = input.Item2;
            var valid = 0;
            foreach (var m in messages)
            {
                var rule = rules.Single(r => r.Id == 0);
                var pos = 0;
                if (rule.Match(m, ref pos, rules) && pos == m.Length)
                {
                    //System.Console.WriteLine(m);
                    valid++;
                }
            }
            System.Console.WriteLine($"Valid messages: {valid}");
        }

		private static Tuple<List<Rule>, List<string>> ReadInput(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            var rules = new List<Rule>();
            var messages = new List<string>();
            var pos = 0;
            do
            {                
                rules.Add(Rule.ParseFrom(lines[pos++]));
            }
            while (!string.IsNullOrEmpty(lines[pos]));
            pos++;
            while (pos < lines.Length)
            {
                messages.Add(lines[pos++]);
            }
            return new Tuple<List<Rule>, List<string>>(rules, messages);
        }
	}
}