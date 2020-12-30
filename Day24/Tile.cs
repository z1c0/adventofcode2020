using System;
using System.Collections.Generic;

namespace Day24
{
	internal static class TileManager
	{
		public static Dictionary<ValueTuple<int, int>, Tile> Tiles { get; } = new Dictionary<(int, int), Tile>();
		
		internal static bool IsBlackTile(int x, int y)
		{
			var key = new ValueTuple<int, int>(x, y);
			if (Tiles.ContainsKey(key))
			{
				return Tiles[key].IsBlack;
			}
			return false;
		}

		internal static Tile GetTile(int x, int y)
		{
			var key = new ValueTuple<int, int>(x, y);
			if (!Tiles.ContainsKey(key))
			{
				Tiles.Add(key, new Tile());
			}
			return Tiles[key];
		}
	}
	internal class Tile
	{
		internal void Flip()
		{
			IsBlack = !IsBlack;
		}

		public override string ToString()
		{
			return IsBlack ? "Black" : "White";
		}

		internal bool IsBlack { get; set; }
		public bool NewIsBlack { get; internal set; }
	}
}