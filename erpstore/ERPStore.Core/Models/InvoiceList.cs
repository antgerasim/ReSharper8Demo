using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Liste de facture
	/// </summary>
	[Serializable]
	public class InvoiceList : List<Invoice>, IPaginable
	{
		public InvoiceList(IEnumerable<Invoice> list)
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
