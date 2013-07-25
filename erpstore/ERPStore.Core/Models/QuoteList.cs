using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	[Serializable]
	public class QuoteList : List<Quote>, IPaginable
	{
		public QuoteList(IEnumerable<Quote> list) 
			: base(list)
		{

		}

		#region IPaginable Members

		public int ItemCount { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }

		#endregion

	}
}
