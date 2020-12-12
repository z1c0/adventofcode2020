using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Day12
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
            var direction = 'E';
            var x = 0;
            var y = 0;
            var wpN = 1;
            var wpE = 10;
            foreach (var i in instructions)
            {
                i.RunEx(ref direction, ref x, ref y, ref wpE, ref wpN);
            }
            System.Console.WriteLine($"pos: {x}/{y} -> {Math.Abs(x) + Math.Abs(y)}");
        }

        private static void Part1()
        {
            var instructions = ReadInput();
            var direction = 'E';
            var x = 0;
            var y = 0;
            foreach (var i in instructions)
            {
                i.Run(ref direction, ref x, ref y);
            }
            System.Console.WriteLine($"pos: {x}/{y} -> {Math.Abs(x) + Math.Abs(y)}");
        }

        private static List<Instruction> ReadInput()
        {
            var instructions = new List<Instruction>();
            var lines = File.ReadAllLines("input.txt");
            foreach (var l in lines)
            {
                instructions.Add(new Instruction(l[0], Int32.Parse(l.Substring(1))));
            }
            return instructions;
        }
    }
}