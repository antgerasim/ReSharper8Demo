using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	public static class EmailValidator
	{
		public static bool IsValidEmail(string email)
		{
			if (email.IsNullOrTrimmedEmpty())
			{
				return false;
			}
			return System.Text.RegularExpressions.Regex.Match(email, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*").Success;
		}
	}
}
