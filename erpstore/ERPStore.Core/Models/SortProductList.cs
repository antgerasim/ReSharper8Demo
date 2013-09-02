using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Ordre de tri des liste de produit
	/// </summary>
	public class SortProductList
	{
		public SortProductList()
		{
			Type = typeof(string);
		}
		public string PropertyName { get; set; }
		public Type Type { get; set; }
		public System.ComponentModel.ListSortDirection Direction { get; set; }
		public string ColumnName { get; set; }
	}
}
