using System;
using System.Collections.Generic;

namespace Day8
{
	internal class Patcher
	{
		private List<Instruction> _instructions;
		private int _lastPatchPos = -1;

		public Patcher(List<Instruction> instructions)
		{
			_instructions = instructions;
		}

		internal void Patch()
		{
			// unpatch last patch first;
			if (_lastPatchPos >= 0)
			{
				PatchAt(_lastPatchPos);
			}
			while (!PatchAt(++_lastPatchPos))
			{
			}
		}

		private bool PatchAt(int pos)
		{
			if (_instructions[pos].OpCode == "nop")
			{
				_instructions[pos].OpCode = "jmp";
				return true;
			}
			else if (_instructions[pos].OpCode == "jmp")
			{
				_instructions[pos].OpCode = "nop";
				return true;
			}
				return false;
		}
	}
}