using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Represente des frais associé à un document de vente
	/// </summary>
	[Serializable]
	public class Fee
	{
		public Fee()
		{
			Amount = 0;
			Quantity = 1;
			TaxRate = 0;
		}
		/// <summary>
		/// Montant unitaire des frais
		/// </summary>
		/// <value>The amount.</value>
		public decimal Amount { get; set; }
		/// <summary>
		/// Quantité
		/// </summary>
		/// <value>The quantity.</value>
		public int Quantity { get; set; }
		/// <summary>
		/// Montant de la TVA sur les frais
		/// </summary>
		/// <value>The tax rate.</value>
		public double TaxRate { get; set; }
		/// <summary>
		/// Nom du frais
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }
		/// <summary>
		/// Montant Total sans taxe
		/// </summary>
		/// <value>The total.</value>
		public decimal Total
		{
			get
			{
				return Amount * Quantity;
			}
		}
		/// <summary>
		/// Montant total de la taxe
		/// </summary>
		/// <value>The total tax.</value>
		public decimal TotalTax
		{
			get
			{
				return Total * Convert.ToDecimal(TaxRate / 100.0);
			}
		}
		/// <summary>
		/// Montant total Toutes Taxes Comprises
		/// </summary>
		/// <value>The total with tax.</value>
		public decimal TotalWithTax
		{
			get
			{
				return Total * (1 + Convert.ToDecimal(TaxRate / 100.0));
			}
		}

		/// <summary>
		/// Raison de la taxe supplémentaire
		/// </summary>
		/// <value>The extra reason.</value>
		public string ExtraReason { get; set; }
	}
}
