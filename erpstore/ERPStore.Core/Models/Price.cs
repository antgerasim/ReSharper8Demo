using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	[Serializable]
	public class Price
	{
		System.Globalization.CultureInfo m_Ci;

		private Price()
		{
			m_Ci = new System.Globalization.CultureInfo("en-us");
		}

		public Price(decimal input, double taxRate)
			: this()
		{
			Value = input;
			TaxRate = taxRate;
		}

		public Price(double input, double taxRate)
			: this(Convert.ToDecimal(input), taxRate)
		{
		}


		public Price(float input, double taxRate)
			: this(Convert.ToDecimal(input), taxRate)
		{
		}

		#region Properties

		public decimal Value { get; set; }
		public double TaxRate { get; set; }
		public decimal TaxValue
		{
			get
			{
				return Value * Convert.ToDecimal(TaxRate);
			}
		}
		public decimal ValueWithTax
		{
			get
			{
				
				return Value * Convert.ToDecimal(1 + TaxRate);
			}
		}

		#endregion

		public string IntegerPart
		{
			get
			{
				var text = Value.ToString(CurrencyPattern, m_Ci);
				var parts = text.Split('.');
				return parts[0].Replace(","," ");
			}
		}

		public string IntegerPartWitTax
		{
			get
			{
				var text = ValueWithTax.ToString(CurrencyPattern, m_Ci);
				var parts = text.Split('.');
				return parts[0].Replace(",", " ");
			}
		}


		public string DecimalPart
		{
			get
			{
				var text = Value.ToString(CurrencyPattern, m_Ci);
				var parts = text.Split('.');
				return parts[1];
			}
		}

		public string DecimalPartWithTax
		{
			get
			{
				var text = ValueWithTax.ToString(CurrencyPattern, m_Ci);
				var parts = text.Split('.');
				return parts[1];
			}
		}

		public string CurrencyPattern 
		{
			get
			{
				if (Value < 1)
				{
					return "#,####0.0000";
				}
				return "#,##0.00";
			}
		}

		//public string ToCurrency()
		//{
		//    return System.Web.HttpUtility.HtmlEncode(string.Format("{0:#,##0.00} €", Value));
		//}

		//public string ToStrikedCurrency()
		//{
		//    return "<strike>" + System.Web.HttpUtility.HtmlEncode(string.Format("{0:#,##0.00} €", Value)) + "</strike>";
		//}

	}
}
