using System.Linq;

namespace Day2
{
	internal class Record
	{
		internal int Min { get; set; }
		internal int Max { get; set; }
		internal char Character { get; set; }
		internal string Password { get; set; }

		internal bool IsValid()
		{
			var count = Password.Count(c => c == Character);
			return count >= Min && count <= Max;
		}

		internal bool IsValidEx()
		{
			return Password[Min - 1] == Character ^ Password[Max - 1] == Character;
		}
	}
}