using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Parametre de transportabilité d'un produit
	/// </summary>
	[Serializable]
	public class TransportSettings
	{
		public TransportSettings()
		{
			Strategy = FeeTransportStrategy.ByLevel;
			Level = 1;
		}
		/// <summary>
		/// Mode de calul des frais de transport
		/// </summary>
		/// <value>The fee transport strategy.</value>
		public FeeTransportStrategy Strategy { get; set; }

		/// <summary>
		/// Niveau de transportabilité du produit
		/// </summary>
		/// <value>The fee transport level.</value>
		public int Level { get; set; }

		/// <summary>
		/// Montant des frais de transport fixe
		/// </summary>
		/// <value>The fee tranport price.</value>
		public decimal FixedPrice { get; set; }

		/// <summary>
		/// Retourne le total des frais de port
		/// </summary>
		/// <param name="quantity">The quantity.</param>
		/// <param name="unitPriceByLevel">The unit price by level.</param>
		/// <returns></returns>
		public decimal GetFeeTotal(int quantity, decimal unitPriceByLevel)
		{
			switch (Strategy)
			{
				case FeeTransportStrategy.FixedPrice:
					return FixedPrice * quantity;
				case FeeTransportStrategy.ByLevel:
					return Level * quantity * unitPriceByLevel;
			}
			return 0;
		}
	}
}
