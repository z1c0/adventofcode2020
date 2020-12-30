using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Day25
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
            var input = ReadInput();
            var publicKeyCard = input.Item1;
            var publicKeyDoor = input.Item2;
            var loopSizeCard = GetLoopSize(publicKeyCard);
            var loopSizeDoor = GetLoopSize(publicKeyDoor);
            var encryptionKeyDoor = Encrypt(publicKeyCard, loopSizeDoor);
            var encryptionKeyCard = Encrypt(publicKeyDoor, loopSizeCard);
            System.Console.WriteLine($"Encryption keys: {encryptionKeyDoor}, {encryptionKeyCard}");
        }

		private static long Encrypt(long subject, int loopSize)
		{
            long value = 1;
            for (var i = 0; i < loopSize; i++)
            {
                value = value * subject;
                value = value % 20201227;
            }
            return value;
		}

		private static int GetLoopSize(long publicKey)
		{
            long value = 1;
            var subjectNumber = 7;
            for (var i = 1; i < Int32.MaxValue; i++)
            {
                value = value * subjectNumber;
                value = value % 20201227;
                if (value == publicKey)
                {
                    return i;
                }
            }
            return -1;
		}

		private static Tuple<long, long> ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            return new Tuple<long, long>(long.Parse(lines[0]), long.Parse(lines[1]));
        }
	}
}