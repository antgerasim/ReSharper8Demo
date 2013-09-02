using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	[Serializable]
	public class QuoteListFilter
	{
		public string Search { get; set; }
		public int StatusId { get; set; }
		public int PeriodId { get; set; }
	}
}
