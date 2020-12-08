namespace Day8
{
	internal class Instruction
	{
		public Instruction(string opCode, int value)
		{
			OpCode = opCode;
			Value = value;
		}

		public string OpCode { get; set; }
		public int Value { get; }

		public override string ToString()
		{
			return $"{OpCode} {Value}";
		}
	}
}