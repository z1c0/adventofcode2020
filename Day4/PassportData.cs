using System;
using System.Linq;

namespace Day4
{
	internal class PassportData
	{
		public string EyeColor { get; internal set; }
		public string PassportId { get; internal set; }
		public string ExpirationYear { get; internal set; }
		public string HairColor { get; internal set; }
		public string BirthYear { get; internal set; }
		public string IssueYear { get; internal set; }
		public string CountryId { get; internal set; }
		public string Height { get; internal set; }

		internal bool IsValid()
		{
		  return
				!string.IsNullOrEmpty(EyeColor) &&
				!string.IsNullOrEmpty(PassportId) &&
				!string.IsNullOrEmpty(ExpirationYear) &&
				!string.IsNullOrEmpty(HairColor) &&
				!string.IsNullOrEmpty(BirthYear) &&
				!string.IsNullOrEmpty(IssueYear) &&
				!string.IsNullOrEmpty(Height);
		}

		internal bool IsValidEx()
		{
		  return
				IsBirthYearValid() &&
				IsIssueYearValid() &&
				IsExpirationYearValid() &&
				IsHeightValid() &&
				IsHairColorValid() &&
				IsEyeColorValid() &&
				IsPassportIdValid() &&
				IsCountryIdValid();
		}

		private bool IsCountryIdValid()
		{
			return true;
		}

		private bool IsHeightValid()
		{
			if (string.IsNullOrEmpty(Height) || Height.Length < 4) 
			{
				return false;
			}
			uint value = 0;
			if (!UInt32.TryParse(Height.Substring(0, Height.Length - 2), out value))
			{
				return false;
			}
			var unit = Height.Substring(Height.Length - 2);
			return
				(unit == "cm" && (value >= 150 && value <= 193)) ||
				(unit == "in" && (value >= 59 && value <= 76));
		}

		private bool IsBirthYearValid()
		{
			return ValidateYear(BirthYear, 1920, 2002);
		}

		private bool IsIssueYearValid()
		{
			return ValidateYear(IssueYear, 2010, 2020);
		}

		private bool IsExpirationYearValid()
		{
			return ValidateYear(ExpirationYear, 2020, 2030);
		}

		private static bool ValidateYear(string year, int min, int max)
		{
			return
				year?.Length == 4 &&
				UInt32.TryParse(year, out var i) &&
				i >= min &&
				i <= max;
		}

		private bool IsHairColorValid()
		{
			return
				HairColor?.Length == 7 &&
				HairColor[0] == '#' &&
				HairColor.Substring(1).All(c => (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f'));
		}

		private bool IsPassportIdValid()
		{
			return PassportId?.Length == 9 && UInt32.TryParse(PassportId, out var i);
		}

		private bool IsEyeColorValid()
		{
			return 
				EyeColor == "amb" ||
				EyeColor == "blu" ||
				EyeColor == "brn" ||
				EyeColor == "gry" ||
				EyeColor == "grn" ||
				EyeColor == "hzl" ||
				EyeColor == "oth";
		}
	}
}