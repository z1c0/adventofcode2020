using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Day14
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
            var instructions = ReadInput();
            string mask = null;
            var memory = new Dictionary<ulong, ulong>();
            foreach (var i in instructions)
            {
                if (i.IsMask)
                {
                    mask = i.Mask;
                }
                else
                {
                    var addrs = ApplyMaskToEx(mask, i.MemoryIndex);
                    foreach (var a in addrs)
                    {
                        if (!memory.ContainsKey(a))
                        {
                            memory.Add(a, i.Value);
                        }
                        else
                        {
                            memory[a] = i.Value;
                        }
                    }
                }

            }
            ulong sum = 0;
            foreach (var v in memory.Values)
            {
                sum += v;
            }
            System.Console.WriteLine($"sum: {sum}");
		}

		internal static ulong ApplyMaskTo(string mask, ulong value)
		{
			var pos = 0;
			ulong masked = value;
			foreach (var c in mask.Reverse())
			{
				var bits = 1UL << pos;
				if (c == '0')
				{
					masked &= ~bits;
					ulong maskTop = 0b0000000000000000000000000001111_11111111111111111111111111111111;
					masked &= maskTop;
				}
				else if (c == '1')
				{
					masked |= bits;
				}
				pos++;
			}
			return masked;
		}

		private static List<ulong> ApplyMaskToEx(string mask, ulong value)
		{
			var masked = new List<ulong>();
            var pos = 0;
            masked.Add(value);
			foreach (var c in mask.Reverse())
			{
				var bits = 1UL << pos;
				if (c == '0')
				{
				}
				else if (c == '1')
				{
                    var count = masked.Count;
                    for (var j = 0; j < count; j++)
                    {
                        masked[j] |= bits;
                    }
				}
                else
                {
                    var count = masked.Count;
                    for (var j = 0; j < count; j++)
                    {
                        var tmp = masked[j];
					    var v = masked[j] &= ~bits;
					    ulong maskTop = 0b0000000000000000000000000001111_11111111111111111111111111111111;
					    v &= maskTop;
                        masked[j] &= v;

                        tmp |= (1UL << pos);
                        masked.Add(tmp);
                    }
                }
				pos++;
			}
			return masked;
        }

		private static void Part1()
        {
            var instructions = ReadInput();
            string mask = null;
            var memory = new Dictionary<ulong, ulong>();
            foreach (var i in instructions)
            {
                if (i.IsMask)
                {
                    mask = i.Mask;
                }
                else
                {
                    var maskedValue = ApplyMaskTo(mask, i.Value);
                    if (!memory.ContainsKey(i.MemoryIndex))
                    {
                        memory.Add(i.MemoryIndex, maskedValue);
                    }
                    else
                    {
                        memory[i.MemoryIndex] = maskedValue;
                    }
                }
            }
            ulong sum = 0;
            foreach (var v in memory.Values)
            {
                sum += v;
            }
            System.Console.WriteLine($"sum: {sum}");
        }

        private static List<Instruction> ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            var instructions = new List<Instruction>();
            foreach (var l in lines)
            {
                if (l.StartsWith("mask"))
                {
                    var tokens = l.Split("=");
                    var mask = tokens[1].Trim();
                    instructions.Add(new Instruction(mask));
                }
                else
                {
                    var tokens = l.Split("=");
                    var t = tokens[0].Trim().Substring(4);
                    t = t.Remove(t.Length - 1);
                    var mem = UInt64.Parse(t);
                    var value = UInt64.Parse(tokens[1].Trim());
                    instructions.Add(new Instruction(mem, value));
                }
            }
            return instructions;
        }
    }
}