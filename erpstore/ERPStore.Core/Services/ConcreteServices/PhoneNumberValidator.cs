using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	public static class PhoneNumberValidator
	{
		public static bool IsValidPhoneNumber(string phoneNumber)
		{
			if (phoneNumber.IsNullOrTrimmedEmpty())
			{
				return false;
			} 
			var match = System.Text.RegularExpressions.Regex.Match(phoneNumber, @"^(\+[\s\d]{6,}|[\s\d]{6,})");
			return match.Success;
		}
	}
}
