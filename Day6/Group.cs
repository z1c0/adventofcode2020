using System.Linq;
using System.Collections.Generic;

namespace Day6
{
	internal class Group
	{
		private Dictionary<char, int> _answers = new Dictionary<char, int>();

		internal void AddAnswer(char c)
		{
			if (!_answers.ContainsKey(c))
			{
				_answers.Add(c, 1);
			}
			else
			{
				_answers[c]++;
			}
		}

		public override string ToString()
		{
			return $"Group of {Size}, any 'Yes' {AnyCount}, all 'Yes' {AllCount}";
		}

		internal int Size { get; set; }

		internal int AnyCount => _answers.Count;

		internal int AllCount => _answers.Values.Where(c => c == Size).Count();
	}
}