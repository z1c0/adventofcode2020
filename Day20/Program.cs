using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Day20
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
            var tiles = ReadInput();
            // Get corners and multiply
            long total = 1;
            foreach (var t in tiles)
            {
                var edges = t.MatchEdges(tiles);
                if (edges.Count(e => e == -1) == 2)
                {
                    System.Console.WriteLine($"[{t.Id}]");
                    total *= t.Id;
                }
            }
            System.Console.WriteLine($"Ids multiplied: {total}");

            // Arrange images
            var sideLength = (int)Math.Sqrt(tiles.Count);
            var grid = new Tile[sideLength, sideLength];
            for (var y = 0; y < sideLength; y++)
            {
                // start with upper left corner
                if (y == 0)
                {
                    grid[0, 0] = tiles.First(t => t.MatchEdges(tiles)[0] == -1 && t.MatchEdges(tiles)[3] == -1);
                }
                else
                {
                    var id = grid[0, y - 1].MatchEdges(tiles)[2];
                    grid[0, y] = tiles.Single(t => t.Id == id);
                    while (grid[0, y - 1].GetEdges()[2] != grid[0, y].GetEdges()[0])
                    {
                        grid[0, y].RotateAndFlipLines();
                    }
                }

                for (var x = 1; x < sideLength; x++)
                {
                    var id = grid[x - 1, y].MatchEdges(tiles)[1];
                    grid[x, y] = tiles.Single(t => t.Id == id);
                    while (grid[x - 1, y].GetEdges()[1] != grid[x, y].GetEdges()[3])
                    {
                        grid[x, y].RotateAndFlipLines();
                    }
                }
            }

            // Print and merge to image
            var lineCount = grid[0, 0].Lines.Count;
            var image = new Tile(0);
            for (var y = 0; y < sideLength; y++)
            {
                for (var l  = 0; l < lineCount; l++)
                {
                    var imageLine = string.Empty;
                    for (var x = 0; x < sideLength; x++)
                    {
                        var line = grid[x, y].Lines[l];
                        System.Console.Write($" {line} ");
                        imageLine += grid[x, y].Lines[l].Substring(1, line.Length - 2);
                    }
                    if (l > 0 && l < lineCount - 1)
                    {
                        image.Lines.Add(imageLine);
                    }
                    System.Console.WriteLine();
                }
                System.Console.WriteLine();
            }

            // Find the sea monster in the image.
            var monsterLine1 = "                  # ";
            var monsterLine2 = "#    ##    ##    ###";
            var monsterLine3 = " #  #  #  #  #  #   ";
            var monsterCount = 0;
            while (monsterCount == 0)
            {
                for (var y = image.Lines.Count - 1; y >= 0; y--)
                {
                    for (var x = 0; x < image.Lines.Count - monsterLine1.Length; x++)
                    {
                        if (FindPattern(image.Lines[y], x, monsterLine3) && FindPattern(image.Lines[y - 1], x, monsterLine2) && FindPattern(image.Lines[y - 2], x, monsterLine1))
                        {
                            image.Lines[y] = ApplyPattern(image.Lines[y], x, monsterLine3);
                            image.Lines[y - 1] = ApplyPattern(image.Lines[y - 1], x, monsterLine2);
                            image.Lines[y - 2] = ApplyPattern(image.Lines[y - 2], x, monsterLine1);
                            monsterCount++;
                        }
                    }
                }
                image.RotateAndFlipLines();
            }
            System.Console.WriteLine($"Sea monsters found: {monsterCount}");

            // Print
            foreach (var l in image.Lines)
            {
                System.Console.WriteLine(l);
            }
            System.Console.WriteLine();

            // Calculate roughness
            var roughness = 0;
            foreach (var l in image.Lines)
            {
                roughness += l.Count(c => c == '#');
            }
            System.Console.WriteLine($"Sea roughness: {roughness}");
        }

		private static string ApplyPattern(string line, int pos, string pattern)
		{
            var chars = line.ToArray();
			for (var x = 0; x < pattern.Length; x++)
            {
                if (pattern[x] == '#')
                {
                    chars[pos + x] = 'O';
                }
            }
            return new string(chars);
		}

		private static bool FindPattern(string line, int pos, string pattern)
		{
			for (var x = 0; x < pattern.Length; x++)
            {
                if (pattern[x] == '#' && line[pos + x] != '#')
                {
                    return false;
                }
            }
            return true;
		}

		private static List<Tile> ReadInput()
        {
            var tiles = new List<Tile>();
            var lines = File.ReadAllLines("input.txt");
            var i = 0;
            while (i < lines.Length)
            {                
                var id = Int32.Parse(lines[i++].Substring(5, 4));
                var tile = new Tile(id);
                while (i < lines.Length)
                {
                    var line = lines[i++];
                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }
                    tile.Lines.Add(line);
                }
                tiles.Add(tile);
            }

            return tiles;
        }
	}
}