using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.IO.Compression;
using System.Collections.Specialized;

namespace ERPStore
{
	/// <summary>
	/// Quelques methodes d'extension du type String
	/// </summary>
	public static class StringExtension
	{
		/// <summary>
		/// Determines whether [is null or trimmed empty] [the specified value].
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		/// 	<c>true</c> if [is null or trimmed empty] [the specified value]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsNullOrTrimmedEmpty(this string value)
		{
			return (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value.Trim()));
		}

		/// <summary>
		/// Lefts the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="length">The length.</param>
		/// <returns></returns>
		public static string Left(this string value, int length)
		{
			if (value.IsNullOrTrimmedEmpty())
			{
				return value;
			}
			return value.Substring(0, Math.Min(length, value.Length));
		}

		/// <summary>
		/// Rights the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="length">The length.</param>
		/// <returns></returns>
		public static string Right(this string value, int length)
		{
			if (value.IsNullOrTrimmedEmpty())
			{
				return value;
			}
			return value.Substring(Math.Min(Math.Max(value.Length - length,0), value.Length));
		}

		/// <summary>
		/// Simplify the String.Format usage, example: "Test {0}".With(DateTime.Now)
		/// </summary>
		/// <param name="format"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static string With(this string format, params object[] args)
		{
			return string.Format(format, args);
		}

		public static bool IsMatch(this string value, string pattern)
		{
			return  !string.IsNullOrEmpty(value) && Regex.IsMatch(value, pattern);
		}

		public static bool IsInteger(this string value)
		{
			return IsMatch(value, @"^[\d]{1,}$");
		}

		public static string Replace(this string value, string pattern, string replacement)
		{
			return Regex.Replace(value, pattern, replacement);
		}

		public static string Reverse(this string input)
		{
			char[] chars = input.ToCharArray();
			Array.Reverse(chars);
			return new String(chars);
		}



		//public static string GetFixedLengthString(string input, int length)
		//{
		//    string result = string.Empty;

		//    if (string.IsNullOrEmpty(input))
		//    {
		//        result = new string(' ', length);
		//    }
		//    else if (input.Length > length)
		//    {
		//        result = input.Substring(0, length);
		//    }
		//    else
		//    {
		//        //result = input;
		//        result = input.PadRight(length);
		//    }

		//    return result;
		//}

		//public static string GetMaxLengthString(string input, int maxLength)
		//{
		//    string result = string.Empty;

		//    if (string.IsNullOrEmpty(input))
		//    {
		//        result = string.Empty;
		//    }
		//    else if (input.Length > maxLength)
		//    {
		//        result = input.Substring(0, maxLength);
		//    }
		//    else
		//    {
		//        result = input;
		//    }

		//    return result;
		//}

		public static int LineCount(this string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return 0;
			}
			Regex RE = new Regex("\n", RegexOptions.Multiline);
			MatchCollection theMatches = RE.Matches(text);
			return (theMatches.Count + 1);
		}

		public static string ToFixedLength(this string input, int length)
		{
			//string result = string.Empty;

			//if (string.IsNullOrEmpty(input))
			//{
			//    result = new string(' ', length);
			//}
			//else if (input.Length > length)
			//{
			//    result = input.Substring(0, length);
			//}
			//else
			//{
			//    //result = input;
			//    result = input.PadRight(length);
			//}

			//return result;
			return ToFixedLength(input, length, ' ');
		}


		public static string ToFixedLength(this string input, int length, char paddingChar)
		{
			string result = string.Empty;

			if (string.IsNullOrEmpty(input))
			{
				result = new string(paddingChar, length);
			}
			else if (input.Length > length)
			{
				result = input.Substring(0, length);
			}
			else
			{
				//result = input;
				result = input.PadRight(length, paddingChar);
			}

			return result;
		}

		

		public static string ToMaxLength(this string input, int maxLength)
		{
			string result = string.Empty;

			if (string.IsNullOrEmpty(input))
			{
				result = string.Empty;
			}
			else if (input.Length > maxLength)
			{
				result = input.Substring(0, maxLength);
			}
			else
			{
				result = input;
			}

			return result;
		}

		/// <summary>
		/// zip the base64 input string to string.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public static string GZipToBase64String(this string input)
		{
			string result = null;
			using (var ms = new System.IO.MemoryStream())
			{
				var buffer = System.Text.Encoding.UTF8.GetBytes(input);
				using (var zip = new System.IO.Compression.GZipStream(ms, CompressionMode.Compress, true))
				{
					zip.Write(buffer, 0, buffer.Length);
					zip.Close();
				}
				result = Convert.ToBase64String(ms.ToArray());
				ms.Close();
			}
			return result;
		}

		/// <summary>
		/// UnZip zippedBuffer
		/// </summary>
		/// <param name="zippedbuffer">The zippedbuffer.</param>
		/// <returns></returns>
		public static string UnGZip(this byte[] zippedbuffer)
		{
			string result = null;
			int blockSize = 512;
			using (var compressedStream = new System.IO.MemoryStream(zippedbuffer, false))
			{
				if (compressedStream.CanSeek)
				{
					compressedStream.Seek(0, SeekOrigin.Begin);
				}
				using (var uncompressedStream = new System.IO.MemoryStream())
				{
					using (var unzip = new System.IO.Compression.GZipStream(compressedStream, CompressionMode.Decompress))
					{
						var bf = new byte[blockSize];
						while (true)
						{
							// Bug ! if zippedbuffer smaller than 4096 bytes, read byte one by one
							if (zippedbuffer.Length <= 4096)
							{
								var pos = unzip.ReadByte();
								if (pos == -1)
								{
									break;
								}
								uncompressedStream.WriteByte((byte)pos);
							}
							else
							{
								var count = unzip.Read(bf, 0, blockSize);
								if (count == 0)
								{
									break;
								}
								uncompressedStream.Write(bf, 0, count);
							}
						}
						result = System.Text.Encoding.UTF8.GetString(uncompressedStream.ToArray());
						unzip.Close();
					}
					uncompressedStream.Close();
				}
				compressedStream.Close();
			}
			return result;
		}

		/// <summary>
		/// unzip base64 zipped string.
		/// </summary>
		/// <param name="zippedinput">The zippedinput.</param>
		/// <returns></returns>
		public static string UnGZipFromBase64String(this string zippedinput)
		{
			var buffer = Convert.FromBase64String(zippedinput);
			return buffer.UnGZip();
		}

		/// <summary>
		/// Converti les paramètres d'une url en HashTable key/value
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public static NameValueCollection ToNameValueDictionary(this string query)
		{
			if (query.IsNullOrTrimmedEmpty() || query.IndexOf("?") == -1)
			{
				return new System.Collections.Specialized.NameValueCollection();
			}
			var parameters = query.Split('?')[1].Split('&');
			var result = new NameValueCollection();
			foreach (var item in parameters)
			{
				var tokens = item.Split('=');
				if (tokens.Count() == 2)
				{
					result.Add(tokens[0], tokens[1]);
				}
			}
			return result;
		}

		public static NameValueCollection ToHtmlDecodedNameValueDictionary(this string query)
		{
			if (query.IsNullOrTrimmedEmpty() || query.IndexOf("?") == -1)
			{
				return new System.Collections.Specialized.NameValueCollection();
			}
			var parameters = query.Split('?')[1].Split('&');
			var result = new NameValueCollection();
			foreach (var item in parameters)
			{
				var tokens = item.Split('=');
				if (tokens.Count() == 2)
				{
					var key = System.Web.HttpUtility.UrlDecode(tokens[0]);
					var value = System.Web.HttpUtility.UrlDecode(tokens[1]);
					result.Add(key, value);
				}
			}
			return result;
		}


		public static string EllipsisAt(this string input, int length)
		{
			if (input == null)
			{
				return input;
			}
			if (length > input.Length)
			{
				length = input.Length;
			}
			if (length == input.Length)
			{
				return input;
			}
			return string.Format("{0}...", input.Substring(0, length));
		}

		public static string[] Words(this string input)
		{
			// Suppression des doubles espaces
			var text = input.Trim();

			var result = input.Split(' ');
			result.RemoveAll(i => i == " ");
			return result;
		}

		public static string CapitalizeWords(this string phrase)
		{
			var words = phrase.ToLower().ToWordList();
			string result = null;
			foreach (var word in words)
			{
				var f = word[0];
				var capitalizedWord = word;
				if (char.IsLetter(f))
				{
					capitalizedWord = char.ToUpper(f) + word.Substring(1);
				}
				result += capitalizedWord + " ";
			}

			return result.Trim();
		}
	}
}
