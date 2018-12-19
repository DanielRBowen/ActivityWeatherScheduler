using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ActivityWeatherSchedulerLibraryNETStandard
{
	public class Validation
	{
		public static bool IsFiveDigits(string word)
		{
			return Regex.IsMatch(word, @"^\d{5}$");
		}

		public static bool IsEmail(string email)
		{
			try
			{
				email = new MailAddress(email).Address;
				return true;
			}
			catch (FormatException)
			{
				return false;
			}
		}
	}
}
