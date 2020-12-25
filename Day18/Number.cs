namespace Day18
{
	internal class Number : Expression
	{
		private int _value;

		public Number(int value)
		{
			_value = value;
		}

		internal override long Evaluate()
		{
			return _value;
		}

		public override string ToString()
		{
			return $"{_value}";
		}
	}
}