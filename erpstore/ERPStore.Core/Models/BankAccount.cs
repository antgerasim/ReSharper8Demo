using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	[Serializable]
	public class BankAccount
	{
		public BankAccount ()
		{

		}
		public string Designation { get; set; }
		public string BankCode { get; set; }
		public string Counter { get; set; }
		public string Account { get; set; }
		public string Key { get; set; }
		public string IBAN { get; set; }
		public string BIC { get; set; }
	}
}
