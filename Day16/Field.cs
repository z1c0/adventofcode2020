using System;

namespace Day16
{
	internal class Field
	{
		private readonly int _to1;

		public string Name { get; }
		public int Index { get; internal set; } = -1;

		private readonly int _from1;
		private readonly int _from2;
		private readonly int _to2;

		public Field(string name, int from1, int to1, int from2, int to2)
		{
            Name = name;
            _from1 = from1;
            _to1 = to1;
            _from2 = from2;
            _to2 = to2;
		}
		internal bool CheckRange(int n)
		{
			return
                (n >= _from1 && n <= _to1) ||
                (n >= _from2 && n <= _to2);
		}
	}
}