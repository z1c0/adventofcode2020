using System.Collections.Generic;

namespace Day8
{
	internal class Runtime
	{
		public int Accumulator { get; private set; }

		public bool Run(List<Instruction> instructions)
		{
			Accumulator = 0;
			var pcHistory = new HashSet<int>();
			var pc = 0;
			while (pc < instructions.Count)
			{
				if (pcHistory.Contains(pc))
				{
					System.Console.WriteLine($"Loop detected!");
					return false;
				}
				pcHistory.Add(pc);
				var i = instructions[pc];
				switch (i.OpCode)
				{
					case "acc":
						Accumulator += i.Value;
						pc++;
						break;

					case "jmp":
						pc += i.Value;
						break;

					case "nop":
						pc++;
						break;
				}
			}
			return true;
		}
	}
}