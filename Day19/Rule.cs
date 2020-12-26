using System;
using System.Linq;
using System.Collections.Generic;

namespace Day19
{
	internal class Rule
	{
		private List<int> _subRulesLeft;
		private List<int> _subRulesRight;

		public int Id { get; }

		public override string ToString()
		{
			return $"{Id}";
		}

		private char _ch;

		public Rule(int id, char ch)
		{
			Id = id;
			_ch = ch;
		}
		public Rule(int id, List<int> subRulesLeft, List<int> subRulesRight)
		{
			Id = id;
			_subRulesLeft = subRulesLeft;
			_subRulesRight = subRulesRight;
		}

		internal static Rule ParseFrom(string text)
		{
			var tokens = text.Split(":");
			var id = Int32.Parse(tokens[0]);
			tokens = tokens[1].Trim().Split(" ");
			if (tokens[0][0] == '"')
			{
				return new Rule(id, tokens[0][1]);
			}
			var subRulesLeft = new List<int>();
			var subRulesRight = new List<int>();
			var subRules = subRulesLeft;
			foreach (var t in tokens)
			{
				if (t == "|")
				{
					subRules = subRulesRight;
				}
				else
				{
					// Negative numbers mark a "loop rule"
					subRules.Add(Int32.Parse(t));
				}
			}
			return new Rule(id, subRulesLeft, subRulesRight);
		}

		internal bool Match(string message, ref int pos, List<Rule> rules)
		{
			if (pos >= message.Length)
			{
				return false;
			}
			if (_ch != 0)
			{
				return message[pos++] == _ch;
			}

			if (MatchSubRules(_subRulesLeft, message, ref pos, rules))
			{
				return true;
			}

			return MatchSubRules(_subRulesRight, message, ref pos, rules);
		}

		private static bool MatchSubRules(List<int> subRules, string message, ref int pos, List<Rule> rules)
		{
			if (!subRules.Any())
			{
				return false;
			}
			if (subRules.Any(r => r < 0))
			{
				return MatchLoop(subRules, message, ref pos, rules);
			}
			var matched = true;
			var savePos = pos;
			foreach (var i in subRules)
			{
				var rule = rules.Single(r => r.Id == i);
				if (!rule.Match(message, ref pos, rules))
				{
					pos = savePos;
					matched = false;
					break;
				}
			}
			return matched;
		}

		private static bool MatchLoop(List<int> subRules, string message, ref int pos, List<Rule> rules)
		{
			// Find maximum match for first rule
			var matchCount1 = 0;
			var firstRule = rules.Single(r => subRules.First() == r.Id * -1);
			do
			{
				var savePos = pos;
				if (!firstRule.Match(message, ref pos, rules))
				{
					pos = savePos;
					break;
				}
				matchCount1++;
			}
			while (true);

			var matchCount2 = 0;
			var secondRule = rules.Single(r => subRules.Last() == r.Id * -1);
			do
			{
				var savePos = pos;
				if (!secondRule.Match(message, ref pos, rules))
				{
					pos = savePos;
					break;
				}
				matchCount2++;
			}
			while (true);

			return matchCount1 > matchCount2 && matchCount2 > 0;
		}
	}
}