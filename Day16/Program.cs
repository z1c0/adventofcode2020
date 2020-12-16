using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Day16
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("START");
            var inputData = ReadInput();
            Part1(inputData);
            Part2(inputData);
            System.Console.WriteLine("DONE");
        }

		private static void Part2(InputData inputData)
		{
            var validTickets = inputData.NearbyTickets.Where(t => t.IsValid).ToList();
            validTickets.Add(inputData.MyTicket);

            while (true)
            {
                var unmatchedFields = inputData.Fields.Count(f => f.Index < 0);
                if (unmatchedFields == 0)
                {
                    break;
                }
                foreach (var f in inputData.Fields)
                {
                    if (f.Index >= 0)
                    {
                        continue;
                    }
                    //System.Console.WriteLine($"resolving {f.Name} ...");
                    var matchCount = 0;
                    var lastMatchIndex = 0;
                    for (var i = 0; i < inputData.Fields.Count(); i++)
                    {
                        if (IsIndexAvailable(inputData.Fields, i))
                        {
                            if (FieldMatchesForAllTickets(f, i, validTickets))
                            {
                                lastMatchIndex = i;
                                matchCount++;
                            }
                        }
                    }
                    if (matchCount == 1)
                    {
                        System.Console.WriteLine($"     column {lastMatchIndex} is {f.Name}");
                        f.Index = lastMatchIndex;
                        break;
                    }
                }
            }
            System.Console.WriteLine("Multiplying values on my ticket starting with 'departure':");
            var product = 1L;
            foreach (var f in inputData.Fields)
            {
                if (f.Name.StartsWith("departure"))
                {
                    var n = inputData.MyTicket.Numbers[f.Index];
                    product *= n;
                }
            }
            System.Console.WriteLine($"Result: {product}");
		}

		private static bool IsIndexAvailable(List<Field> fields, int index)
		{
            foreach (var f in fields)
            {
                if (f.Index == index)
                {
                    return false;
                }
            }
            return true;
		}

		private static bool FieldMatchesForAllTickets(Field field, int index, List<Ticket> validTickets)
		{
            foreach (var t in validTickets)
            {
                if (!field.CheckRange(t.Numbers[index]))
                {
                    return false;
                }
            }
            return true;
		}

		private static void Part1(InputData inputData)
        {
            var invalidSum = 0;
            foreach (var t in inputData.NearbyTickets)
            {
                foreach (var n in t.Numbers)
                {
                    var isValid = false;
                    foreach (var f in inputData.Fields)
                    {
                        if (f.CheckRange(n))
                        {
                            isValid = true;
                            break;
                        }
                    }
                    if (!isValid)
                    {
                        invalidSum += n;
                        t.IsValid = false;
                    }
                }
            }
            System.Console.WriteLine($"Ticket scanning error rate: {invalidSum}");
        }

        private static InputData ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            var inputData = new InputData();
            var pos = 0;
            while (true)
            {
                var line = lines[pos++];
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }
                var tokens1 = line.Split(":");
                var tokens2 = tokens1[1].Trim().Split(" or");
                var tokens3 = tokens2[0].Split("-");
                var tokens4 = tokens2[1].Split("-");
                inputData.Fields.Add(new Field(
                    tokens1[0],
                    Int32.Parse(tokens3[0]),
                    Int32.Parse(tokens3[1]),
                    Int32.Parse(tokens4[0]),
                    Int32.Parse(tokens4[1])));

            }
            if (lines[pos++] != "your ticket:")
            {
                throw new InvalidDataException();
            }
            var tokens = lines[pos++].Split(",");
            foreach (var t in tokens)
            {
                inputData.MyTicket.Numbers.Add(Int32.Parse(t));
            }
            pos++;

            if (lines[pos++] != "nearby tickets:")
            {
                throw new InvalidDataException();
            }
            while (pos < lines.Count())
            {
                tokens = lines[pos++].Split(",");
                var ticket = new Ticket();
                foreach (var t in tokens)
                {
                    ticket.Numbers.Add(Int32.Parse(t));
                }
                inputData.NearbyTickets.Add(ticket);
            }
            return inputData;
        }
    }
}