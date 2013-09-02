using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Liste des commandes paginée
	/// </summary>
	[Serializable]
	public class OrderList : List<Order>, IPaginable
	{
		public OrderList(IEnumerable<Order> list)
			: base(list)
		{

		}

		/// <summary>
		/// Nom du filtre selectionné pour l'affichage des commandes
		/// </summary>
		/// <value>The name of the filter.</value>
		public string Name { get; set; }

		#region IPaginable Members

		public int ItemCount { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }

		#endregion
	}
}
