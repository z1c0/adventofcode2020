using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Day21
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("START");
            Part1();
            System.Console.WriteLine("DONE");
        }

		private static void Part1()
        {
            var input = ReadInput();
            var allergens = new List<string>();
            var ingredients = new Dictionary<string, Dictionary<string, int>>();
            foreach (var l in input)
            {
                foreach (var i in l.Item1)
                {
                    if (!ingredients.ContainsKey(i))
                    {
                        ingredients.Add(i, new Dictionary<string, int>());
                    }
                    foreach (var a in l.Item2)
                    {
                        if (!allergens.Contains(a))
                        {
                            allergens.Add(a);
                        }
                        if (!ingredients[i].ContainsKey(a))
                        {
                            ingredients[i].Add(a, 0);
                        }
                        ingredients[i][a]++;
                    }
                }
            }

            for (var x = 0; x < allergens.Count; x++)
            {
                var a = allergens[x];
                var max = ingredients.Values.Max(i => { i.TryGetValue(a, out var v); return v; } );
                foreach (var i in ingredients.Values)
                {
                    if (i.ContainsKey(a) && i[a] < max)
                    {
                        i[a] = 0;
                    }
                }
            }

            // Counting the number of times any of these ingredients appear in any ingredients list.
            var count = 0;
            foreach (var i in ingredients.Keys)
            {
                if (ingredients[i].Values.All(v => v == 0))
                {
                    System.Console.WriteLine(i);
                    foreach (var l in input)
                    {
                        if (l.Item1.Contains(i))
                        {
                            count++;
                        }
                    }
                }
            }
            System.Console.WriteLine($"Count is {count}");

            // Remove ingredients with no allergens
            foreach (var i in ingredients.Keys)
            {
                if (ingredients[i].Values.All(i => i == 0))
                {
                    ingredients.Remove(i);
                }
            }
            // Find ingredient - allergen relationship
            var result = new Dictionary<string, string>();
            var pos = 0;
            while (pos < ingredients.Keys.Count)
            {
                var i = ingredients.Keys.ElementAt(pos++);
                var max = ingredients[i].Values.Max(i => i);
                var maxCount = ingredients[i].Values.Count(i => i == max);
                if (maxCount == 1)
                {
                    var key = ingredients[i].Single(t => t.Value == max).Key;
                    result.Add(i, key);
                    // Reset all with this allergen
                    foreach (var j in ingredients.Values)
                    {
                        if (j.ContainsKey(key))
                        {
                            j[key] = 0;
                        }
                    }
                    ingredients.Remove(i);
                    pos = 0;
                }
            }
            var ordered = result.OrderBy(t => t.Value);
            var list = string.Join(",", ordered.Select(t => t.Key).ToArray());
            System.Console.WriteLine($"Dangerous ingredient list: " + list);
        }

		private static List<Tuple<List<string>, List<string>>> ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            var input = new List<Tuple<List<string>, List<string>>>();
            foreach (var l in lines)
            {
                var ingredients = new List<string>();
                var allergens = new List<string>();
                var i = l.IndexOf("(contains");
                foreach (var t in l.Substring(0, i - 1).Split(" "))
                {
                    ingredients.Add(t);
                }
                foreach (var t in l.Substring(i + 9, l.Length - i - 10).Split(","))
                {
                    allergens.Add(t.Trim());
                }
                input.Add(new Tuple<List<string>, List<string>>(ingredients, allergens));
            }
            return input;
        }
	}
}