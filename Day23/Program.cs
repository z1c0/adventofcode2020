using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Day23
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("START");
            LinkedList(100, false);
            LinkedList(10_000_000, true);
            System.Console.WriteLine("DONE");

        }
		private static void LinkedList(int maxRounds, bool millionize)
        {
            var input = ReadInput();
            var minValue = input.Min();
            var maxValue = input.Max();
            var cups = new LinkedList<int>();
            foreach (var i in input)
            {
                cups.AddLast(i);
            }
            if (millionize)
            {
                for (var i = maxValue + 1; i <= 1_000_000; i++)
                {
                    cups.AddLast(i);
                }
                maxValue = 1_000_000;
            }
            var lookup = new LinkedListNode<int>[cups.Count + 1];
            var c = cups.First;
            while (c != null)
            {
                lookup[c.Value] = c;
                c = c.Next;
            }

            var current = cups.First;
            for (var round = 0; round < maxRounds; round++)
            {
                if (round != 0 && round % 1_000_000 == 0)
                {
                    System.Console.WriteLine($"Round: {round}");
                }
                // pick up 3 cups
                var next = current.Next != null ? current.Next: cups.First;
                var remove1 = next; cups.Remove(next);
                next = current.Next != null ? current.Next: cups.First;
                var remove2 = next; cups.Remove(next);
                next = current.Next != null ? current.Next: cups.First;
                var remove3 = next; cups.Remove(next);

                // select destination cup
                var destinationValue = current.Value - 1;
                var destination = lookup[destinationValue];
                while (destination == null || destination == remove1 || destination == remove2 || destination == remove3)
                {
                    destinationValue--;
                    if (destinationValue < minValue)
                    {
                        destinationValue = maxValue;
                    }
                    destination = lookup[destinationValue];
                }
 
                // place back cups
                cups.AddAfter(destination, remove3);
                cups.AddAfter(destination, remove2);
                cups.AddAfter(destination, remove1);

                // next current
                current = current.Next != null ? current.Next : cups.First;
            }

            if (millionize)
            {
                var one = cups.Find(1);
                long next1 = one.Next.Value;
                long next2 = one.Next.Next.Value;
                long result = next1 * next2;
                System.Console.WriteLine($"{next1} * {next2} = {result}");
            }
            else
            {
                var result = string.Join(string.Empty, cups.Select(x => x.ToString()).ToArray());
                var tokens = result.Split("1");
                result = tokens[1] + tokens[0];
                System.Console.WriteLine(result);
            }
        }

		private static void List(int maxRounds, bool millionize)
        {
            var cups = ReadInput();
            var minValue = cups.Min();
            var maxValue = cups.Max();
            if (millionize)
            {
                for (var i = maxValue + 1; i <= 1_000_000; i++)
                {
                    cups.Add(i);
                }
            }

            var currentV = cups[0];
            var remove = new int[3];
            for (var round = 0; round < maxRounds; round++)
            {
                if (round % 100_000 == 0)
                {
                    System.Console.WriteLine($"Round: {round}");
                }
                // pick up 3 cups
                var i = (cups.IndexOf(currentV) + 1) % cups.Count;
                remove[0] = cups[i]; cups.RemoveAt(i);
                i = (cups.IndexOf(currentV) + 1) % cups.Count;
                remove[1] = cups[i]; cups.RemoveAt(i);
                i = (cups.IndexOf(currentV) + 1) % cups.Count;
                remove[2] = cups[i]; cups.RemoveAt(i);

                // select destination cup
                var destination = currentV - 1;
                i = cups.IndexOf(destination);
                while (i == -1)
                {
                    destination--;
                    if (destination < minValue)
                    {
                        destination = maxValue;
                    }
                    i = cups.IndexOf(destination);
                }
 
                // place back cups
                cups.InsertRange(i + 1, remove);

                // next current
                i = (cups.IndexOf(currentV) + 1) % cups.Count;
                currentV = cups[i];
            }

            if (millionize)
            {
                var i = cups.IndexOf(1);
                var next1 = cups[(i + 1) % cups.Count];
                var next2 = cups[(i + 2) % cups.Count];
                long result = next1 * next2;
                System.Console.WriteLine($"{next1} * {next2} = {result}");
            }
            else
            {
                var result = string.Join(string.Empty, cups.Select(x => x.ToString()).ToArray());
                var tokens = result.Split("1");
                result = tokens[1] + tokens[0];
                System.Console.WriteLine(result);
            }
        }

		private static List<int> ReadInput()
        {
            var cups = new List<int>();
            var text = File.ReadAllText("input.txt");
            foreach (var c in text)
            {
                cups.Add(Int32.Parse(c.ToString()));
            }
            return cups;
        }
	}
}