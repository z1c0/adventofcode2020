using System;

namespace Day5
{
	internal class Seat
	{
		public Seat(string code)
		{
			Code = code;
			Row = Decode(Code.Substring(0, 7), 0, 128);
			Column = Decode(Code.Substring(7), 0, 8);
		}

		private int Decode(string s, int from, int to)
		{
			foreach (char c in s)
			{
				var step = (to - from) / 2;
				if (c == 'F' || c == 'L')
				{
					to -= step;
				}
				else if (c == 'B' || c == 'R')
				{
					from +=  step;
				}
			}
			return from;
		}

		public override string ToString()
		{
			return $"{Code}: row {Row}, column {Column}, seat ID {SeatId}";
		}

		public string Code { get; }
		public int Row { get; private set; }
		public int Column { get; private set; }
		public int SeatId { get { return Row * 8 + Column; } }
	}
}