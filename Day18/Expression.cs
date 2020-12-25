using System;

namespace Day18
{
	internal enum Operation
	{
		None,
		Add,
		Multiply
	}
	internal class Expression
	{
		public Operation Operation { get; internal set; } = Operation.None;
		public Expression Left { get; internal set; }

		public Expression Right { get; internal set; }

		internal virtual long Evaluate()
		{
			if (Operation == Operation.Add)
			{
				return Right.Evaluate() + Left.Evaluate();
			}
			if (Operation == Operation.Multiply)
			{
				return Right.Evaluate() * Left.Evaluate();
			}
			if (Right != null)
			{
				return Right.Evaluate();
			}
			return Left.Evaluate();
		}

		public override string ToString()
		{
			return $"{Left} {Operation} {Right}";
		}
	}
}