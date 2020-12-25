using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Day18
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("START");
            //Part1();
            Part2();
            System.Console.WriteLine("DONE");
        }

		private static void Part2()
		{
            var input = ReadInput();
            long total = 0;
            foreach (var t in input)
            {
                var pos = 0;
                var e = ParseExpressionEx(t, ref pos);
                System.Console.WriteLine(e);
                var sum = e.Evaluate();
                System.Console.WriteLine($"-> {sum}");
                total += sum;
            }
            System.Console.WriteLine($"Sum of expressions: {total}");
		}

		private static void Part1()
        {
            var input = ReadInput();
            long total = 0;
            foreach (var t in input)
            {
                var pos = t.Length - 1;
                var e = ParseExpression(t, ref pos);
                System.Console.WriteLine(e);
                var sum = e.Evaluate();
                System.Console.WriteLine($"-> {sum}");
                total += sum;
            }
            System.Console.WriteLine($"Sum of expressions: {total}");
        }
		private static List<string[]> ReadInput()
        {
            var tokens = new List<string[]>();
            var lines = File.ReadAllLines("input.txt");
            foreach (var l in lines)
            {                
                tokens.Add(Tokenize(l));
            }
            return tokens;
        }

		private static string[] Tokenize(string line)
		{
			var tokens = new List<string>();
            for (var i = 0; i < line.Length; i++)
            {
                var ch = line[i];
                if (Char.IsDigit(ch))
                {
                    var n = Int32.Parse(ch.ToString());
                    tokens.Add(n.ToString());
                }
                else if (ch == '(' || ch == ')' || ch == '+' || ch == '*')
                {
                    tokens.Add(ch.ToString());
                }
            }
            return tokens.ToArray();
		}

		private static Expression ParseExpressionEx(string[] tokens, ref int pos)
		{
            var expression = ParseFactorEx(tokens, ref pos);

            if (pos < tokens.Length && tokens[pos] == "*")
            {
                pos++;
                var tmp = expression;
                expression = new Expression();
                expression.Left = tmp;
                expression.Operation = Operation.Multiply;
                expression.Right = ParseExpressionEx(tokens, ref pos);
            }

            return expression;
		}

		private static Expression ParseExpression(string[] tokens, ref int pos)
		{
            var expression = new Expression();

            expression.Right = ParseFactor(tokens, ref pos);
            if (pos > 0 && tokens[pos] != "(")
            {
                var t = tokens[pos--];
                if (t == "+")
                {
                    expression.Operation = Operation.Add;
                }
                else if (t == "*")
                {
                    expression.Operation = Operation.Multiply;
                }
                else
                {
                    throw new InvalidOperationException();
                }

                expression.Left = (pos == 0 || tokens[pos - 1] == "(") ? ParseFactor(tokens, ref pos) : ParseExpression(tokens, ref pos);
            }

            return expression;
		}

		private static Expression ParseFactorEx(string[] tokens, ref int pos)
		{
            Expression expression = null;
            var t = tokens[pos++];
			if (Int32.TryParse(t, out var n))
            {
                expression = new Number(n);
            }
            else if (t == "(")
            {
                expression = ParseExpressionEx(tokens, ref pos);
                if (tokens[pos++] != ")")
                {
                    throw new InvalidProgramException();
                }
            }

            if (pos < tokens.Length && tokens[pos] == "+")
            {
                pos++;
                var tmp = expression;
                expression = new Expression();
                expression.Left = tmp;
                expression.Operation = Operation.Add;
                expression.Right = ParseFactorEx(tokens, ref pos);
            }

            return expression;
		}

		private static string NextToken(string[] tokens, int pos)
		{
			return (pos < tokens.Length - 1) ? tokens[pos + 1] : null;
		}

		private static Expression ParseFactor(string[] tokens, ref int pos)
		{
            var expression = new Expression();
            var t = tokens[pos++];
			if (Int32.TryParse(t, out var n))
            {
                expression.Left = new Number(n);
            }
            else if (t == ")")
            {
                expression.Left = ParseExpression(tokens, ref pos);
                if (tokens[pos--] != "(")
                {
                    throw new InvalidProgramException();
                }
            }
            return expression;
		}
	}
}