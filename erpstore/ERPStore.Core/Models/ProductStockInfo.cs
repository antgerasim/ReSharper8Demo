using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Disponibilité d'un produit
	/// </summary>
	[Serializable]
	public class ProductStockInfo
	{
		/// <summary>
		/// Code du produit
		/// </summary>
		/// <value>The code.</value>
		public string ProductCode { get; set; }
		/// <summary>
		/// Stock physique du produit
		/// </summary>
		/// <value>The physical stock.</value>
		public int PhysicalStock{ get; set; }
		/// <summary>
		/// Total de la quantité réservée par les clients
		/// </summary>
		/// <value>The reserved quantity.</value>
		public int ReservedQuantity { get; set; }
		/// <summary>
		/// Total de la quantité réservée chez les fournisseurs
		/// </summary>
		/// <value>The provisionned quantity.</value>
		public int ProvisionnedQuantity { get; set; }
		/// <summary>
		/// Nombre de jours de livraison
		/// </summary>
		/// <remarks>
		/// Si le produit est en stock, ajouter ce nombre de jour pour connaitre
		/// la date de livraison prevue chez le client depart de l'entrepot
		/// </remarks>
		/// <value>The delivery days count.</value>
		public int DeliveryDaysCount { get; set; }
		/// <summary>
		/// Nombre de jours avant reception dans l'entrepot
		/// </summary>
		/// <value>The provisionning days count.</value>
		public int ProvisionningDaysCount { get; set; }
		/// <summary>
		/// Prochaine date de reception prevue pour réappro
		/// </summary>
		/// <remarks>
		/// Cette valeur peut etre nulle s'il n'y a pas de réappro engagée
		/// </remarks>
		/// <value>The most provisionning date.</value>
		public DateTime? MostProvisionningDate { get; set; }
		/// <summary>
		/// Stock disponible
		/// </summary>
		/// <value>The available stock.</value>
		public int AvailableStock
		{
			get
			{
				return Math.Max(0,PhysicalStock - ReservedQuantity);
			}
		}
		/// <summary>
		/// Indique s'il y a du stock pour le produit
		/// </summary>
		/// <value><c>true</c> if this instance has stock; otherwise, <c>false</c>.</value>
		public bool HasStock 
		{
			get
			{
				return AvailableStock > 0;
			}
		}
		/// <summary>
		/// Indique le temps total qu'il faut pour faire parvenir 
		/// le produit au client
		/// </summary>
		/// <remarks>
		/// Si la valeur est -1 alors ne pas vendre le produit, car pas configuré 
		/// pour la vente.
		/// </remarks>
		/// <value>The total provisionning day count.</value>
		public int TotalProvisionningDayCount
		{
			get
			{
				if (ProvisionningDaysCount == -1)
				{
					return -1;
				}
				return DeliveryDaysCount + ProvisionningDaysCount;
			}
		}

		/// <summary>
		/// Indique textuellement la disponibilité du stock
		/// </summary>
		/// <value>The disponibility.</value>
		public string Disponibility
		{
			get
			{
				if (HasStock)
				{
					return string.Format("{0} en stock", AvailableStock);
				}
				if (TotalProvisionningDayCount < 0)
				{
					return "Nous contacter";
				}
				var plurial = (TotalProvisionningDayCount > 1) ? "s" : string.Empty;
				return string.Format("disponible en {0} jour{1}", TotalProvisionningDayCount, plurial);
			}
		}

		/// <summary>
		/// Indique la quantité minimale en stock
		/// </summary>
		/// <value>The minimal quantity.</value>
		public int MinimalQuantity { get; set; }
	}
}