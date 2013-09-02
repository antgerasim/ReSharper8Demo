using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Pays de livraison
	/// </summary>
	[Serializable]
	public class DeliveryCountry
	{
		/// <summary>
		/// Pays
		/// </summary>
		/// <value>The country.</value>
		public Country Country { get; set; }

		/// <summary>
		/// Frais de port par defaut
		/// </summary>
		/// <value>The default fee amount.</value>
		public decimal DefaultShippingFeeAmount { get; set; }

		/// <summary>
		/// Montant minimal de commande
		/// </summary>
		/// <value>The minimal order amount.</value>
		public decimal MinimalOrderAmount { get; set; }

		/// <summary>
		/// Montant minimal pour atteindre le franco de port
		/// </summary>
		/// <value>The minimal free of carriage amount.</value>
		public decimal MinimalFreeOfCarriageAmount { get; set; }
	}
}
