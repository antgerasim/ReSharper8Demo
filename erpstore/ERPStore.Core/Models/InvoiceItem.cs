using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Ligne d'une facture
	/// </summary>
	[Serializable]
	public class InvoiceItem 
	{
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
		/// Date de livraison
		/// </summary>
		/// <value>The delivery delay.</value>
		public DateTime DeliveredDate { get; set; }
		/// <summary>
		/// Tarif de l'eco taxe
		/// </summary>
		/// <value>The recycle price.</value>
		public Price RecyclePrice { get; set; }
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
		/// Taux de tva appliquée
		/// </summary>
		/// <value>The tax rate.</value>
		public double TaxRate { get; set; }
		/// <summary>
		/// Conditionnement
		/// </summary>
		/// <value>The packaging value.</value>
		public int PackagingValue { get; set; }
		/// <summary>
		/// Date de création de la commande
		/// </summary>
		/// <value>The order creation date.</value>
		public DateTime OrderCreationDate { get; set; }
		/// <summary>
		/// Code de la commande associée
		/// </summary>
		/// <value>The order code.</value>
		public string OrderCode { get; set; }
		/// <summary>
		/// Reference du client concernant la commande
		/// </summary>
		/// <value>The customer document reference.</value>
		public string CustomerDocumentReference { get; set; }
		/// <summary>
		/// Informations concernant l'origine de la commande
		/// </summary>
		/// <value>The order detail.</value>
		public string OrderSourceInfo { get; set; }
		/// <summary>
		/// Code du bon de livraison associé
		/// </summary>
		/// <value>The delivery code.</value>
		public string DeliveryCode { get; set; }
		/// <summary>
		/// Date de création du bon de livraison
		/// </summary>
		/// <value>The delivery creation date.</value>
		public DateTime DeliveryCreationDate { get; set; }
		/// <summary>
		/// Informations concernant l'origine du bon de livraison
		/// </summary>
		/// <value>The delivery source info.</value>
		public string DeliverySourceInfo { get; set; }

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
