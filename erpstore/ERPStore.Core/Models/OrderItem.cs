using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Détail d'une commande
	/// </summary>
	[Serializable]
	public class OrderItem : ISaleItem
	{
		public OrderItem()
		{

		}
		/// <summary>
		/// Quantité commandée
		/// </summary>
		/// <value>The quantity.</value>
		public int Quantity { get; set; }
		/// <summary>
		/// Produit vendu
		/// </summary>
		/// <value>The product.</value>
		public Product Product { get; set; }
		/// <summary>
		/// Prix de vente
		/// </summary>
		/// <value>The sale price.</value>
		public Price SalePrice { get; set; }
		/// <summary>
		/// Unité de vente
		/// </summary>
		/// <value>The sale unit value.</value>
		public int SaleUnitValue { get; set; }
		/// <summary>
		/// Remise
		/// </summary>
		/// <value>The discount.</value>
		public double Discount { get; set; }
		/// <summary>
		/// Délai de livraison
		/// </summary>
		/// <value>The delivery delay.</value>
		public DateTime? DeliveryDelay { get; set; }
		/// <summary>
		/// Tarif de l'eco taxe
		/// </summary>
		/// <value>The recycle price.</value>
		public Price RecyclePrice { get; set; }
		/// <summary>
		/// Solde à livrer
		/// </summary>
		/// <value>The balance.</value>
		public int Balance { get; set; }
		/// <summary>
		/// Le produit est livré
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is balanced; otherwise, <c>false</c>.
		/// </value>
		public bool IsBalanced { get; set; }
		/// <summary>
		/// Code du produit
		/// </summary>
		/// <value>The product code.</value>
		public string ProductCode { get; set; }
		/// <summary>
		/// Code du produit du client
		/// </summary>
		/// <value>The customer product code.</value>
		public string CustomerProductCode { get; set; }
		/// <summary>
		/// Designation courte du produit
		/// </summary>
		/// <value>The designation.</value>
		public string Designation { get; set; }
		/// <summary>
		/// Description longue du produit
		/// </summary>
		/// <value>The extended description.</value>
        public string ExtendedDescription { get; set; }
		/// <summary>
		/// Type de livraison
		/// </summary>
		/// <value>The type of the shipping.</value>
		public ShippingType ShippingType { get; set; }
		/// <summary>
		/// Taux de tva appliquée
		/// </summary>
		/// <value>The tax rate.</value>
		public double TaxRate { get; set; }
		/// <summary>
		/// Conditionnement
		/// </summary>
		/// <value>The packaging value.</value>
		public int PackagingValue { get; set; }

		#region Totals

		/// <summary>
		/// Montant total HT de l'eco taxe
		/// </summary>
		/// <value>The recycle total.</value>
		public decimal RecycleTotal
		{
			get
			{
				if (RecyclePrice == null)
				{
					return 0;
				}
				return Quantity * RecyclePrice.Value;
			}
		}
		/// <summary>
		/// Montant de la tva de l'eco taxe
		/// </summary>
		/// <value>The recycle tax total.</value>
		public decimal RecycleTaxTotal
		{
			get
			{
				if (RecyclePrice == null)
				{
					return 0;
				}
				return Quantity * RecyclePrice.TaxValue;
			}
		}
		/// <summary>
		/// Montant total TTC de l'eco taxe
		/// </summary>
		/// <value>The recycle total with tax.</value>
		public decimal RecycleTotalWithTax
		{
			get
			{
				if (RecyclePrice == null)
				{
					return 0;
				}
				return Quantity * RecyclePrice.ValueWithTax;
			}
		}
		/// <summary>
		/// Montant total de la ligne HT hors eco taxe
		/// </summary>
		/// <value>The total.</value>
		public decimal Total
		{
			get
			{
				return (Quantity * SalePrice.Value) / SaleUnitValue;
			}
		}
		/// <summary>
		/// Montant total de la tva de la ligne hors eco taxe
		/// </summary>
		/// <value>The total tax.</value>
		public decimal TotalTax
		{
			get
			{
				return new Price(Total, TaxRate).TaxValue;
			}
		}

		/// <summary>
		/// Montant total ttc de la ligne hors eco taxe
		/// </summary>
		/// <value>The total with tax.</value>
		public decimal TotalWithTax
		{
			get
			{
				return new Price(Total, TaxRate).ValueWithTax;
			}
		}
		/// <summary>
		/// Montant total ht de la ligne, y compris l'eco taxe
		/// </summary>
		/// <value>The grand total.</value>
		public decimal GrandTotal
		{
			get
			{
				return Total + RecycleTotal;
			}
		}
		/// <summary>
		/// Montant total de la tva , y compris l'eco taxe
		/// </summary>
		/// <value>The grand tax total.</value>
		public decimal GrandTaxTotal
		{
			get
			{
				return TotalTax + RecycleTaxTotal;
			}
		}
		/// <summary>
		/// Montant total ttc de la ligne, y compris l'eco taxe
		/// </summary>
		/// <value>The grand total with tax.</value>
		public decimal GrandTotalWithTax
		{
			get
			{
				return TotalWithTax + RecycleTotalWithTax;
			}
		}

		#endregion

	}
}
