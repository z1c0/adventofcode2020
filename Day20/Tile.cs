using System.Linq;
using System.Collections.Generic;

namespace Day20
{
	internal class Tile
	{
		private bool _verticalFlip;
		private int _rotateCount;
		private bool _horizontalFlip;

		public Tile(int id)
		{
			Id = id;
		}

		public override string ToString()
		{
			return $"{Id}";
		}

		public List<string> Lines { get; private set; } = new List<string>();
		public int Id { get; }
		internal int[] MatchEdges(List<Tile> tiles)
		{
			var thisEdges = GetEdges();
			var edges = new int[thisEdges.Length];
			var i = 0;
			foreach (var e in thisEdges)
			{
				foreach (var t in tiles)
				{
					if (t.Id != Id)
					{
						edges[i] = MatchEdge(e, t);
						if (edges[i] > 0)
						{
							break;
						}
					}
				}
				i++;
			}
			return edges;
		}

		private int MatchEdge(string edge, Tile tile)
		{
			foreach (var e in tile.GetEdges())
			{
				if (edge == e)
				{
					return tile.Id;
				}
				var flipped = new string(e.Reverse().ToArray());
				if (edge == flipped)
				{
					return tile.Id;
				}
			}
			return -1;
		}

		internal string[] GetEdges()
		{
			var edges = new string[4];
			edges[0] = Lines.First();
			edges[1] = new string(Lines.Select(l => l.Last()).ToArray());
			edges[2] = Lines.Last();
			edges[3] = new string(Lines.Select(l => l.First()).ToArray());
			return edges;
		}

		internal void RotateAndFlipLines()
		{
			if (_rotateCount == 4)
			{
				_rotateCount = 0;
				// Flip
				if (!_verticalFlip)
				{
					Lines.Reverse();
					_verticalFlip = true;
				}
				else if (!_horizontalFlip)
				{
					Lines.Reverse(); // vertical unflip
					// horizontal flip
					for (var i = 0; i < Lines.Count; i++)
					{
						Lines[i] = new string(Lines[i].Reverse().ToArray());
					}
					_horizontalFlip = true;
				}
				else
				{
					ToString();
				}
			}
			else
			{
				// Rotate
				_rotateCount++;
				var lines = new List<string>();
				for (var x = 0; x < Lines.Count; x++)
				{
					var flipLine = new List<char>();
					for (var y = Lines.Count - 1; y >= 0; y--)
					{
						flipLine.Add(Lines[y][x]);
					}
					lines.Add(new string(flipLine.ToArray()));
				}
				Lines = lines;
			}
		}
	}
}