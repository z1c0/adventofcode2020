using System;
using System.Collections.Generic;

namespace Day16
{
	public class Ticket
	{
		public bool IsValid { get; internal set; } = true;
		internal List<int> Numbers { get; } = new List<int>();
	}
}