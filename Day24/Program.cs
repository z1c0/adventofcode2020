using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Day24
{
    public enum Direction
    {
        East,
        SouthEast,
        SouthWest,
        West,
        NorthWest,
        NorthEast,
    }

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
            // Ensure borders
            var minX = TileManager.Tiles.Keys.Min(k => k.Item1);
            var maxX = TileManager.Tiles.Keys.Max(k => k.Item1);
            var minY = TileManager.Tiles.Keys.Min(k => k.Item2);
            var maxY = TileManager.Tiles.Keys.Max(k => k.Item2);
            for (var y = minY - 10; y <= maxY + 10; y += 10)
            {
                for (var x = minX - 10; x <= maxX + 10; x += 5)
                {
                    TileManager.GetTile(x, y);
                }
            }


            for (var day = 1; day <= 100; day++)
            {
                foreach (var t in TileManager.Tiles.ToList())
                {
                    var count = CountAdjacentBlackTiles(t.Key.Item1, t.Key.Item2);
                    t.Value.NewIsBlack = t.Value.IsBlack;
                    if (t.Value.IsBlack)
                    {
                        if (count == 0 || count > 2)
                        {
                            t.Value.NewIsBlack = false;
                        }
                    }
                    else
                    {
                        if (count == 2)
                        {
                            t.Value.NewIsBlack = true;
                        }
                    }
                }

                // Apply new states
                foreach (var t in TileManager.Tiles.Values)
                {
                    t.IsBlack = t.NewIsBlack;
                }

                System.Console.WriteLine($"Day {day}: {TileManager.Tiles.Values.Count(t => t.IsBlack)}");
            }
		}

		private static int CountAdjacentBlackTiles(int x, int y)
		{
			var count = 0;
            if (TileManager.GetTile(x - 10, y).IsBlack) // W
            {
                count++;
            }
            if (TileManager.GetTile(x - 5, y - 10).IsBlack) // NW
            {
                count++;
            }
            if (TileManager.GetTile(x + 5, y - 10).IsBlack) // NE
            {
                count++;
            }
            if (TileManager.GetTile(x + 10, y).IsBlack)  // E
            {
                count++;
            }
            if (TileManager.GetTile(x + 5, y + 10).IsBlack) // SE
            {
                count++;
            }
            if (TileManager.GetTile(x - 5, y + 10).IsBlack) // SW
            {
                count++;
            }
            return count;
		}

		private static void Part1()
        {
            var instructionsList = ReadInput();
            int x = 0;
            int y = 0;
            var current = TileManager.GetTile(x, y);
            foreach (var instructions in instructionsList)
            {
                x = 0;
                y = 0;
                foreach (var dir in instructions)
                {
                    switch (dir)
                    {
                        case Direction.East:
                            x += 10;
                            break;
                        case Direction.West:
                            x -= 10;
                            break;
                        case Direction.NorthEast:
                            x += 5;
                            y -= 10;
                            break;
                        case Direction.NorthWest:
                            x -= 5;
                            y -= 10;
                            break;
                        case Direction.SouthEast:
                            x += 5;
                            y += 10;
                            break;
                        case Direction.SouthWest:
                            x -= 5;
                            y += 10;
                            break;
                    }
                    current = TileManager.GetTile(x, y);
                }
                current.Flip();
            }
            var count = TileManager.Tiles.Values.Count(t => t.IsBlack);
            System.Console.WriteLine($"Number of black tiles: {count}");
        }

		private static List<List<Direction>> ReadInput()
        {
            var instructionsList = new List<List<Direction>>();
            var lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                var instructions = new List<Direction>();
                for (var i = 0; i < line.Length; i++)
                {
                    var c = line[i];
                    if (c == 'e')
                    {
                        instructions.Add(Direction.East);
                    }
                    else if (c == 'w')
                    {
                        instructions.Add(Direction.West);
                    }
                    else if (c == 's')
                    {
                        c = line[++i];
                        if (c == 'e')
                        {
                            instructions.Add(Direction.SouthEast);
                        }
                        else if (c == 'w')
                        {
                            instructions.Add(Direction.SouthWest);
                        }
                        else
                        {
                            throw new InvalidProgramException();
                        }
                    }
                    else if (c == 'n')
                    {
                        c = line[++i];
                        if (c == 'e')
                        {
                            instructions.Add(Direction.NorthEast);
                        }
                        else if (c == 'w')
                        {
                            instructions.Add(Direction.NorthWest);
                        }
                        else
                        {
                            throw new InvalidProgramException();
                        }
                    }
                    else
                    {
                        throw new InvalidProgramException();
                    }

                }
                instructionsList.Add(instructions);
            }
            return instructionsList;
        }
	}
}