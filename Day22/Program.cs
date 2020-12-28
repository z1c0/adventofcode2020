using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Day22
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
            var input = ReadInput();
            var winner = Play(input.Item1, input.Item2, false);
            Score(input, winner);
        }

		private static void Part2()
		{
            var input = ReadInput();
            var winner = Play(input.Item1, input.Item2, true);
            Score(input, winner);
		}

        private static int Play(List<int> deck1, List<int> deck2, bool withRecurse)
        {
            var history = new Dictionary<int, Tuple<List<int>, List<int>>>();
            var round = 0;
            while (deck1.Any() && deck2.Any())
            {
                foreach (var t in history.Values)
                {
                    if (t.Item1.SequenceEqual(deck1) && t.Item2.SequenceEqual(deck2))
                    {
                        return 1;
                    }
                }
                history[round++] = new Tuple<List<int>, List<int>>(deck1.ToList(), deck2.ToList());
                var card1 = deck1[0];
                deck1.RemoveAt(0);
                var card2 = deck2[0];
                deck2.RemoveAt(0);

                // Recurse?
                var recursiveWin = 0;
                if (withRecurse)
                {
                    if (deck1.Count >= card1 && deck2.Count >= card2)
                    {
                        var copy1 = deck1.Take(card1).ToList();
                        var copy2 = deck2.Take(card2).ToList();
                        recursiveWin = Play(copy1, copy2, true);
                    }
                }
                if (recursiveWin == 1 || (recursiveWin == 0 && card1 > card2))
                {
                    deck1.Add(card1);
                    deck1.Add(card2);
                }
                else if (recursiveWin == 2 || (recursiveWin == 0 && card2 > card1))
                {
                    deck2.Add(card2);
                    deck2.Add(card1);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            var winner = deck1.Any() ? 1 : 2;
            return winner;
        }
        private static void Score(Tuple<List<int>, List<int>> input, int winner)
		{
            var score = 0;
            var winningDeck = (winner == 1) ? input.Item1 : input.Item2;
            var factor = winningDeck.Count;
            foreach (var c in winningDeck)
            {
                score += (factor-- * c);
            }
            System.Console.WriteLine($"Score of the winning deck: {score}");
		}

		private static Tuple<List<int>, List<int>> ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            var input = new List<Tuple<List<string>, List<string>>>();
            
            var player1 = new List<int>();
            var player2 = new List<int>();
            List<int> current = null;
            foreach (var l in lines)
            {
                if (l == "Player 1:")
                {
                    current = player1;
                }
                else if (l == "Player 2:")
                {
                    current = player2;
                }
                else if (!string.IsNullOrEmpty(l))
                {
                    current.Add(Int32.Parse(l));
                }
            }
            return new Tuple<List<int>, List<int>>(player1, player2);
        }
	}
}