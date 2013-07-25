using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
	public static class FormatExtensions
	{
		public static string ToCurrency(this decimal value)
		{
			return value.ToCurrency(false);
		}

		public static string ToCurrency(this decimal value, bool hideSymbol)
		{
			string symbol = " €";
			if (hideSymbol)
			{
				symbol = string.Empty;
			}
			var pattern = "{0:#,##0.00}{1}";
			if (value < 1)
			{
				pattern = "{0:#,####0.0000}{1}";
			}
			return System.Web.HttpUtility.HtmlEncode(string.Format(pattern, value, symbol));
		}

        public static string ToRawCurrency(this decimal value)
        {
            return value.ToRawCurrency(false);
        }

        public static string ToRawCurrency(this decimal value, bool hideSymbol)
        {
            string symbol = " €";
            if (hideSymbol)
            {
                symbol = string.Empty;
            }
            var pattern = "{0:#,##0.00}{1}";
            if (value < 1)
            {
                pattern = "{0:#,####0.0000}{1}";
            }
            return string.Format(pattern, value, symbol);
        }
	}
}
