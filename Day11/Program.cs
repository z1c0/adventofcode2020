using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Day9
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
            var occupied = Simulate(seats, 4, true);
            System.Console.WriteLine($"occupied {occupied}");
        }

        private static void Part2()
        {
            var seats = ReadInput();
            var occupied = Simulate(seats, 5, false);
            System.Console.WriteLine($"occupied {occupied}");
        }

        private static int Simulate(char[,] seats, int maxOccupied, bool onlyNext)
        {
            var h = seats.GetLength(0);
            var w = seats.GetLength(1);
            var copy = new char[h, w];

            int diffs;
            do
            {
                //Print(seats);
                for (var y = 0; y < h; y++)
                {
                    for (var x = 0; x < w; x++)
                    {
                        if (seats[y, x] == 'L')
                        {
                            copy[y, x] = CountAdjacent('#', y, x, seats, onlyNext) == 0 ? '#' : 'L';
                        }
                        else if (seats[y, x] == '#')
                        {
                            copy[y, x] = CountAdjacent('#', y, x, seats, onlyNext) >= maxOccupied ? 'L' : '#';
                        }
                        else
                        {
                            copy[y, x] = '.';
                        }
                    }
                }

                diffs = 0;
                for (var y = 0; y < h; y++)
                {
                    for (var x = 0; x < w; x++)
                    {
                        if (seats[y, x] != copy[y, x])
                        {
                            diffs++;
                        }
                    }
                }

                var temp = seats;
                seats = copy;
                copy = temp;
            }
            while (diffs > 0);

            var occupied = 0;
            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    if (seats[y, x] == '#')
                    {
                        occupied++;
                    }
                }
            }
            return occupied;
        }

        private static void Print(char[,] seats)
        {
            var h = seats.GetLength(0);
            var w = seats.GetLength(1);
            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    System.Console.Write(seats[y, x]);
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine();
        }

        private static void Scan(char[,] seats, int x, int y, int incX, int incY, bool onlyNext, char target, ref int count)
        {
            while (true)
            {
                x += incX;
                y += incY;

                if (x < 0 || x >= seats.GetLength(1))
                {
                    break;
                }
                if (y < 0 || y >= seats.GetLength(0))
                {
                    break;
                }
                var seat = seats[y, x];
                if (seat == '#')
                {
                    count++;
                    break;
                }
                if (seat == 'L')
                {
                    break;
                }

                if (onlyNext)
                {
                    break;
                }
            }
        }

        private static int CountAdjacent(char target, int y, int x, char[, ] seats, bool onlyNext)
        {
            var count = 0;
            var h = seats.GetLength(0);
            var w = seats.GetLength(1);

            // E
            Scan(seats, x, y, 1, 0, onlyNext, target, ref count);
            // W
            Scan(seats, x, y, -1, 0, onlyNext, target, ref count);
            // S
            Scan(seats, x, y, 0, 1, onlyNext, target, ref count);
            // N
            Scan(seats, x, y, 0, -1, onlyNext, target, ref count);
            // NE
            Scan(seats, x, y, 1, -1, onlyNext, target, ref count);
            // SE
            Scan(seats, x, y, 1, 1, onlyNext, target, ref count);
            // SW
            Scan(seats, x, y, -1, 1, onlyNext, target, ref count);
            // NW
            Scan(seats, x, y, -1, -1, onlyNext, target, ref count);

            return count;
        }

        private static char[,] ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            var seats = new char[lines.Count(), lines.First().Length];
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                for (var j = 0; j < line.Length; j++)
                {
                    seats[i, j] = line[j];
                }
            }
            return seats;
        }
    }
}