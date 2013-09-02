using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Détail du panier
	/// </summary>
	[Serializable]
	public class CartItem
	{
		public CartItem()
		{
		}

		/// <summary>
		/// Identifiant interne de la ligne
		/// </summary>
		/// <value>The id.</value>
		public int Id { get; set; }
		/// <summary>
		/// Le produit selectionné
		/// </summary>
		/// <value>The product.</value>
		public Product Product { get; set; }
		/// <summary>
		/// La quantité
		/// </summary>
		/// <value>The quantity.</value>
		public int Quantity { get; set; }
		/// <summary>
		/// Prix de vente public
		/// </summary>
		/// <value>The catalog price.</value>
		public Price CatalogPrice { get; set; }


		/// <summary>
		/// Le prix de vente 
		/// </summary>
		/// <value>The sale price.</value>
		public Price SalePrice { get; set; }
		/// <summary>
		/// Unité de vente
		/// </summary>
		/// <value>The sale unit value.</value>
		public int SaleUnitValue { get; set; }
		/// <summary>
		/// Packaging
		/// </summary>
		/// <value>The packaging.</value>
		public int Packaging { get; set; }
		///// <summary>
		///// TVA par defaut
		///// </summary>
		///// <value>The default tax rate.</value>
		//public double DefaultTaxRate { get; set; }
		/// <summary>
		/// Montant de l'eco taxe
		/// </summary>
		/// <remarks>
		/// s'il n'y a pas d'eco taxe sur le produit cette valeur est nulle
		/// </remarks>
		/// <value>The recycle price.</value>
		public Price RecyclePrice { get; set; }

		/// <summary>
		/// Le prix de vente est-il deja négocié par le client
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is customer price applied; otherwise, <c>false</c>.
		/// </value>
		public bool IsCustomerPriceApplied { get; set; }

		/// <summary>
		/// Raison de la remise via un coupon
		/// </summary>
		/// <value>The coupon reason.</value>
		public string CouponReason { get; set; }

		/// <summary>
		/// Informations sur le stock
		/// </summary>
		/// <value>The product stock info.</value>
		public Models.ProductStockInfo ProductStockInfo { get; set; }

		/// <summary>
		/// Type de livraison
		/// </summary>
		/// <value>The shipping type id.</value>
		public ShippingType ShippingType { get; set; }

		/// <summary>
		/// Remise appliquée au prix catalogue
		/// </summary>
		/// <value>The discount.</value>
		public double Discount { get; set; }

		/// <summary>
		/// Type de remise appliquée au prix catalogue
		/// </summary>
		/// <value>The discount type id.</value>
		public PriceType PriceType { get; set; }

		/// <summary>
		/// Date de création de la ligne
		/// </summary>
		/// <value>The creation date.</value>
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Date de la dernière mise à jour de la ligne
		/// </summary>
		/// <value>The last update.</value>
		public DateTime LastUpdate { get; set; }

		/// <summary>
		/// Accèpte les livraisons partielles de la ligne si la quantité est un multiple supérieur au packaging
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [allow partial delivery]; otherwise, <c>false</c>.
		/// </value>
		public bool AllowPartialDelivery { get; set; }

		/// <summary>
		/// Ligne verrouillée par le vendeur
		/// </summary>
		/// <value><c>true</c> if this instance is locked; otherwise, <c>false</c>.</value>
		public bool IsLocked { get; set; }

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
				return (Quantity * SalePrice.Value) / (SaleUnitValue * 1.0m);
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
				return (Quantity * SalePrice.TaxValue) / (SaleUnitValue * 1.0m);
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
				return (Quantity * SalePrice.ValueWithTax) / (SaleUnitValue * 1.0m); ;
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
