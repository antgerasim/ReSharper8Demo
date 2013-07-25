using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Represente un prix de vente en fonction d'un interval de quantité
	/// </summary>
	[Serializable]
	public class PriceByQuantity
	{
		/// <summary>
		/// Quantité de départ 1 minimum
		/// </summary>
		/// <value>From.</value>
		public int From { get; set; }
		/// <summary>
		/// Seuil de quantité superieure, si null alors infini
		/// </summary>
		/// <value>To.</value>
		public int? To { get; set; }
		/// <summary>
		/// Prix de vente
		/// </summary>
		/// <value>The sale price.</value>
		public Price SalePrice { get; set; }
	}
}
