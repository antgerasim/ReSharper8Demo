using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore
{
	public static class SEOHelper
	{
		public static string SEOUrlDecode(string url)
		{
			StringBuilder sb = new StringBuilder(url);
			sb.Replace("?", "");
			sb.Replace("%", "");
			sb.Replace("@", "%40");
			sb.Replace("+", "%2b");
			sb.Replace("'", "%27");
			sb.Replace("/", "$");
			sb.Replace("\"", "$");
			sb.Replace("#", "%23");
			sb.Replace("&", "%26");
			sb.Replace(".", "");
			sb.Replace(":", "@");
			sb.Replace(";", "%3B");
			sb.Replace("=", "~");
			sb.Replace("&lt", "");
			sb.Replace("&gt", "");
			sb.Replace("<", "");
			sb.Replace(">", "");
			sb.Replace("*", "~");
			sb.Replace(" ", "+");
			sb.Replace("é", "%c3%a9");
			sb.Replace("è", "%c3%a8");
			sb.Replace("'", "27");
			sb.Replace(",", "%2c");
			sb.Replace("à", "%c3%a0");
			return System.Web.HttpUtility.UrlEncode(sb.ToString());
		}

		public static string SEOUrlEncode(string url)
		{
			url = url.ToLower();
			url = url.Trim();
			url = AccentLess(url);
			// Suppression des doubles espaces
			while (true)
			{
				if (url.IndexOf("  ") == -1)
				{
					break;
				}
				url = url.Replace("  ", " ");
			}
			var sb = new StringBuilder(url);
			sb.Replace(" / ", "/");
			sb.Replace(" /", "/");
			sb.Replace("/ ", "/");
			sb.Replace(" ", "_");
			sb.Replace("?", "");
			sb.Replace("%", "");
			//sb.Replace("@", "");
			// sb.Replace("+", "%2B");
			sb.Replace("\"", "");
			sb.Replace("#", "");
			sb.Replace("&", "");
			sb.Replace(".", "");
			sb.Replace(":", "");
			sb.Replace(";", "");
			// sb.Replace("=", "%3D");
			sb.Replace("&lt", "");
			sb.Replace("&gt", "");
			sb.Replace("<", "");
			sb.Replace(">", "");
			sb.Replace("*", "");
			sb.Replace("+", "");
			//sb.Replace("'", "%27");
			//sb.Replace(",", "%2C");
			var result = sb.ToString();
			result = result.Trim();
			result = result.TrimEnd('+');

			return result;
		}

		public static string AccentLess(string input)
		{
			string accent = "ŠŒŽšœžŸ¢µÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝàáâãäåæçèéêëìíîïðñòóôõöùúûüý";
			string accentless = "SOZsozYcuAAAAAAACEEEEIIIIDNOOOOOxOUUUUYaaaaaaaceeeeiiiionooooouuuuy";

			for (int i = 0; i < accent.Length; i++)
			{
				if (input.IndexOf(accent[i]) > -1)
				{
					input = input.Replace(accent[i], accentless[i]);
				}
			}

			return input;
		}

		public static string RemoveHTML(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return null;
			}
			string result = System.Text.RegularExpressions.Regex.Replace(input, "<[^>]*>", "");
			result = System.Text.RegularExpressions.Regex.Replace(result, @"&[\w#\d]+;", "");
			return result.Trim();
		}

	}
}
