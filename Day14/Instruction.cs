using System;
using System.Collections.Generic;
using System.Linq;

namespace Day14
{
	internal class Instruction
	{
		public ulong MemoryIndex { get; }
		public ulong Value { get; }
		public string Mask { get; }
		public bool IsMask { get => !string.IsNullOrEmpty(Mask); }

		public Instruction(string mask)
		{
			Mask = mask;
		}

		public Instruction(ulong mem, ulong value)
		{
			MemoryIndex = mem;
			Value = value;
		}
	}
}