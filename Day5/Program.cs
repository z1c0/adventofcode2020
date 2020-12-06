using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day5
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
            var seats = ReadInput();
            var highest = seats.Max(s => s.SeatId);
            System.Console.WriteLine($"Highest seat: {highest}");
        }

        private static void Part2()
        {
            var seats = ReadInput().OrderBy(s => s.SeatId);
            var expectedSeatId = seats.First().SeatId;
            foreach (var s in seats.Skip(1))
            {
                expectedSeatId++;
                if (s.SeatId != expectedSeatId)
                {
                    System.Console.WriteLine($"My seat id: {expectedSeatId}");
                    break;
                }
            }
        }
        private static List<Seat> ReadInput()
        {
            var seats = new List<Seat>();
            var lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                seats.Add(new Seat(line));
            }
            return seats;
        }
    }
}