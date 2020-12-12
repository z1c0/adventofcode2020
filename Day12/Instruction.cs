using System;

namespace Day12
{
	internal class Instruction
	{
		private char _command;
		private int _value;

		public Instruction(char command, int value)
		{
			_command = command;
			_value = value;
		}

		public override string ToString()
		{
			return $"{_command}: {_value}";
		}
		internal static void Move(ref char direction, ref int x, ref int y, int by)
		{
			switch(direction)
			{
				case 'N':
					y -= by;
					break;
				case 'S':
					y += by;
					break;
				case 'W':
					x -= by;
					break;
				case 'E':
					x += by;
					break;
			}
		}

		internal void RunEx(ref char direction, ref int x, ref int y, ref int wpE, ref int wpN)
		{
			if (_command == 'F')
			{
				var diffX = wpE * _value;
				var diffY = wpN * _value;
				x += diffX;
				y += diffY;
			}
			else if (_command == 'N')
			{
				wpN += _value;
			}
			else if (_command == 'E')
			{
				wpE += _value;
			}
			else if (_command == 'S')
			{
				wpN -= _value;
			}
			else if (_command == 'W')
			{
				wpE -= _value;
			}
			else if (_command == 'R')
			{
					for (var i = 0; i < _value / 90; i++)
					{
						var wpS = wpN * -1;
						var wpW = wpE * -1;
						var tmp = wpE;
						wpE = wpN;
						wpN = wpW;
						wpW = wpS;
						wpS = tmp;
					}
			}
			else if (_command == 'L')
			{
					for (var i = 0; i < _value / 90; i++)
					{
						var wpS = wpN * -1;
						var wpW = wpE * -1;
						var tmp = wpW;
						wpW = wpN;
						wpN = wpE;
						wpE = wpS;
						wpS = tmp;
					}
			}
		}

		internal void Run(ref char direction, ref int x, ref int y)
		{
			switch(_command)
			{
				case 'N':
					Move(ref _command, ref x, ref y, _value);
					break;
				case 'S':
					Move(ref _command, ref x, ref y, _value);
					break;
				case 'W':
					Move(ref _command, ref x, ref y, _value);
					break;
				case 'E':
					Move(ref _command, ref x, ref y, _value);
					break;
				case 'L':
					for (var i = 0; i < _value / 90; i++)
					{						
						if (direction == 'N')
						{
							direction = 'W';
						}
						else if (direction == 'W')
						{
							direction = 'S';
						}
						else if (direction == 'S')
						{
							direction = 'E';
						}
						else if (direction == 'E')
						{
							direction = 'N';
						}
					}
					break;
				case 'R':
					for (var i = 0; i < _value / 90; i++)
					{						
						if (direction == 'N')
						{
							direction = 'E';
						}
						else if (direction == 'E')
						{
							direction = 'S';
						}
						else if (direction == 'S')
						{
							direction = 'W';
						}
						else if (direction == 'W')
						{
							direction = 'N';
						}
					}
					break;
				case 'F':
					Move(ref direction, ref x, ref y, _value);
					break;
			}
		}
	}
}