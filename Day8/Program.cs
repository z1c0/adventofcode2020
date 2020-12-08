using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
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

        private static void Part1()
        {
            var instructions = ReadInput();
            var runtime = new Runtime();
            runtime.Run(instructions);
            System.Console.WriteLine($"Accumulator: {runtime.Accumulator}");
        }

        private static void Part2()
        {
            var instructions = ReadInput();
            var runtime = new Runtime();
            var patcher = new Patcher(instructions);
            while (!runtime.Run(instructions))
            {
                patcher.Patch();
            }
            System.Console.WriteLine($"Accumulator: {runtime.Accumulator}");
        }

        private static List<Instruction> ReadInput()
        {
            var instructions = new List<Instruction>();
            var lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                var tokens = line.Split(" ");
                instructions.Add(new Instruction(tokens[0], Int32.Parse(tokens[1])));
            }
            return instructions;
        }
    }
}