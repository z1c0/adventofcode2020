using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Day17
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
            var inputData = ReadInput4();
            inputData = Explode4(inputData);
            
            for (var cycle = 0; cycle < 6; cycle++)
            {
                var zL = inputData.GetLength(3);
                var yL = inputData.GetLength(2);
                var xL = inputData.GetLength(1);
                var wL = inputData.GetLength(0);

                var copy = Copy4(inputData);

                for (var z = 1; z < zL - 1; z++)
                {
                    for (var y = 1; y < yL - 1; y++)
                    {
                        for (var x = 1; x < xL - 1; x++)
                        {
                            for (var w = 1; w < wL - 1; w++)
                            {
                                if (inputData[w, x, y, z])
                                {
                                    var i = CountActiveNeighbors4(inputData, w, x, y, z);
                                    if (i != 2 && i != 3)
                                    {
                                        copy[w, x, y, z] = false;
                                    }
                                }
                                else
                                {
                                    var i = CountActiveNeighbors4(inputData, w, x, y, z);
                                    if (i == 3)
                                    {
                                        copy[w, x, y, z] = true;
                                    }
                                }
                            }
                        }
                    }
                }
                Print4(copy);

                inputData = Explode4(copy);
                
            }
        }

		private static void Part1()
        {
            var inputData = ReadInput();
            inputData = Explode(inputData);
            
            for (var cycle = 0; cycle < 6; cycle++)
            {
                var zL = inputData.GetLength(2);
                var yL = inputData.GetLength(1);
                var xL = inputData.GetLength(0);

                var copy = Copy(inputData);

                for (var z = 1; z < zL - 1; z++)
                {
                    for (var y = 1; y < yL - 1; y++)
                    {
                        for (var x = 1; x < xL - 1; x++)
                        {
                            if (inputData[x, y, z])
                            {
                                var i = CountActiveNeighbors(inputData, x, y, z);
                                if (i != 2 && i != 3)
                                {
                                    copy[x, y, z] = false;
                                }
                            }
                            else
                            {
                                var i = CountActiveNeighbors(inputData, x, y, z);
                                if (i == 3)
                                {
                                    copy[x, y, z] = true;
                                }
                            }
                        }
                    }
                }
                Print(copy);

                inputData = Explode(copy);
                
            }
        }
		private static bool[,,] Copy(bool[,,] inputData)
		{
            var zL = inputData.GetLength(2);
            var yL = inputData.GetLength(1);
            var xL = inputData.GetLength(0);
            var copy = new bool[xL, yL, zL];

            for (var z = 0; z < zL; z++)
            {
                for (var y = 0; y < yL; y++)
                {
                    for (var x = 0; x < xL; x++)
                    {
                        copy[x, y, z] = inputData[x, y, z];
                    }
                }
            }
            return copy;
		}

		private static bool[,,,] Copy4(bool[,,,] inputData)
		{
            var zL = inputData.GetLength(3);
            var yL = inputData.GetLength(2);
            var xL = inputData.GetLength(1);
            var wL = inputData.GetLength(0);
            var copy = new bool[wL, xL, yL, zL];

            for (var z = 0; z < zL; z++)
            {
                for (var y = 0; y < yL; y++)
                {
                    for (var x = 0; x < xL; x++)
                    {
                        for (var w = 0; w < wL; w++)
                        {
                            copy[w, x, y, z] = inputData[w, x, y, z];
                        }
                    }
                }
            }
            return copy;

		}
		private static bool[,,] Explode(bool[,,] inputData)
		{
            var zL = inputData.GetLength(2);
            var yL = inputData.GetLength(1);
            var xL = inputData.GetLength(0);
            var copy = new bool[xL + 2, yL + 2, zL + 2];

            for (var z = 0; z < zL; z++)
            {
                for (var y = 0; y < yL; y++)
                {
                    for (var x = 0; x < xL; x++)
                    {
                        copy[x + 1, y + 1, z + 1] = inputData[x, y, z];
                    }
                }
            }
            return copy;
		}

		private static bool[,,,] Explode4(bool[,,,] inputData)
		{
            var zL = inputData.GetLength(3);
            var yL = inputData.GetLength(2);
            var xL = inputData.GetLength(1);
            var wL = inputData.GetLength(0);
            var copy = new bool[wL + 2, xL + 2, yL + 2, zL + 2];

            for (var z = 0; z < zL; z++)
            {
                for (var y = 0; y < yL; y++)
                {
                    for (var x = 0; x < xL; x++)
                    {
                        for (var w = 0; w < wL; w++)
                        {
                            copy[w + 1, x + 1, y + 1, z + 1] = inputData[w, x, y, z];
                        }
                    }
                }
            }
            return copy;
		}

		private static void Print(bool[,,] inputData)
        {
            var count = 0;
            var zL = inputData.GetLength(2);
            var yL = inputData.GetLength(1);
            var xL = inputData.GetLength(0);

            for (var z = 1; z < zL - 1; z++)
            {
                for (var y = 1; y < yL - 1; y++)
                {
                    for (var x = 1; x < xL - 1; x++)
                    {
                        if (inputData[x, y, z])
                        {
                            count++;
                            System.Console.Write('#');
                        }
                        else
                        {
                            System.Console.Write('.');
                        }
                    }
                    System.Console.WriteLine();
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine($"Active cubes: {count}");
        }

		private static void Print4(bool[,,,] inputData)
        {
            var count = 0;
            var zL = inputData.GetLength(3);
            var yL = inputData.GetLength(2);
            var xL = inputData.GetLength(1);
            var wL = inputData.GetLength(0);

            for (var z = 1; z < zL - 1; z++)
            {
                for (var y = 1; y < yL - 1; y++)
                {
                    for (var x = 1; x < xL - 1; x++)
                    {
                        for (var w = 1; w < wL - 1; w++)
                        {
                            if (inputData[w, x, y, z])
                            {
                                count++;
                            }
                        }
                    }
                }
            }
            System.Console.WriteLine($"Active cubes: {count}");
        }

		private static int CountActiveNeighbors(bool[,,] inputData, int x, int y, int z)
		{
            var count = 0;

            for (var z1 = z  - 1; z1 <= z + 1; z1++)
            {
                for (var y1 = y  - 1; y1 <= y + 1; y1++)
                {
                    for (var x1 = x  - 1; x1 <= x + 1; x1++)
                    {                
                        if (x1 != x || y1 != y || z1 != z)
                        {
                            if (inputData[x1, y1, z1])
                            {
                                count++;
                            }
                        }
                    }                
                }
            }
            return count;
		}

		private static int CountActiveNeighbors4(bool[,,,] inputData, int w, int x, int y, int z)
		{
            var count = 0;

            for (var z1 = z  - 1; z1 <= z + 1; z1++)
            {
                for (var y1 = y  - 1; y1 <= y + 1; y1++)
                {
                    for (var x1 = x  - 1; x1 <= x + 1; x1++)
                    {                
                        for (var w1 = w  - 1; w1 <= w + 1; w1++)
                        {
                            if (x1 != x || y1 != y || z1 != z || w1 != w)
                            {
                                if (inputData[w1, x1, y1, z1])
                                {
                                    count++;
                                }
                            }
                        }                
                    }
                }
            }
            return count;
		}

		private static bool[,,] ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            var z = 5;
            var y = lines.Count() + 2;
            var x = lines.First().Length + 2;
            var input = new bool[x, y, z];
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                for (var j = 0; j < line.Length; j++)
                {
                    input[j + 1, i + 1, 2] = line[j] == '#';
                }
            }
            return input;
        }

		private static bool[,,,] ReadInput4()
        {
            var lines = File.ReadAllLines("input.txt");
            var w = 5;
            var z = 5;
            var y = lines.Count() + 2;
            var x = lines.First().Length + 2;
            var input = new bool[w, x, y, z];
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                for (var j = 0; j < line.Length; j++)
                {
                    input[2, j + 1, i + 1, 2] = line[j] == '#';
                }
            }
            return input;
        }
    }
}