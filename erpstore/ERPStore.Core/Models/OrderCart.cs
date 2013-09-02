using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Panier de commande
	/// </summary>
	[Serializable]
	public class OrderCart : CartBase
	{
		public OrderCart()
		{
			AcceptCondition = false;
			ShippingFee = new Fee();
			AcceptConversion = false;
		}

		/// <summary>
		/// Adresse de livraison du panier
		/// </summary>
		/// <value>The delivery address.</value>
		public Address DeliveryAddress { get; set; }
		/// <summary>
		/// Adresse de facturation du panier
		/// </summary>
		/// <value>The billing address.</value>
		public Address BillingAddress { get; set; }
		/// <summary>
		/// Mode de reglement selectionné pour la commande
		/// </summary>
		/// <value>The payment mode.</value>
		public string PaymentModeName { get; set; } 
		/// <summary>
		/// L'utilisateur accepte les conditions générales de vente
		/// </summary>
		/// <value><c>true</c> if [accept condition]; otherwise, <c>false</c>.</value>
		public bool AcceptCondition { get; set; }
		/// <summary>
		/// Accepte les livraisons partielles sur la commande
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [allow partial delivery]; otherwise, <c>false</c>.
		/// </value>
		public bool AllowPartialDelivery { get; set; }
		/// <summary>
		/// Ce panier est un cadeau
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is present; otherwise, <c>false</c>.
		/// </value>
		public bool IsPresent { get; set; }

		#region Totals

		/// <summary>
		/// Montant total HT du panier sans l'eco participation
		/// </summary>
		/// <value>The total.</value>
		public decimal Total
		{
			get
			{
				return Items.Sum(i => i.Total);
			}
		}

		/// <summary>
		/// Montant de la tva du panier sans l'eco participation
		/// </summary>
		/// <value>The total tax.</value>
		public decimal TotalTax
		{
			get
			{
				return Items.Sum(i => i.TotalTax);
			}
		}

		/// <summary>
		/// Montant total TTC du panier sans l'eco participation
		/// </summary>
		/// <value>The total with tax.</value>
		public decimal TotalWithTax
		{
			get
			{
				return Items.Sum(i => i.TotalWithTax);
			}
		}

		/// <summary>
		/// Montant total ht de l'eco participation
		/// </summary>
		/// <value>The recycle total.</value>
		public decimal RecycleTotal
		{
			get
			{
				return Items.Sum(i => i.RecycleTotal);
			}
		}
		/// <summary>
		/// Montant total de la tva de l'eco participation
		/// </summary>
		/// <value>The recycle tax total.</value>
		public decimal RecycleTaxTotal
		{
			get
			{
				return Items.Sum(i => i.RecycleTaxTotal);
			}
		}
		/// <summary>
		/// Montant total ttc de l'eco participation
		/// </summary>
		/// <value>The recycle total with tax.</value>
		public decimal RecycleTotalWithTax
		{
			get
			{
				return Items.Sum(i => i.RecycleTotalWithTax);
			}
		}
		/// <summary>
		/// Montant total ht du panier y compris l'eco participation
		/// </summary>
		/// <value>The grand total.</value>
		public decimal GrandTotal
		{
			get
			{
				return Total + RecycleTotal + ShippingFee.Total;
			}
		}
		/// <summary>
		/// Montant total de la tva y compris avec l'eco participation
		/// </summary>
		/// <value>The grand tax total.</value>
		public decimal GrandTaxTotal
		{
			get
			{
				return TotalTax + RecycleTaxTotal + ShippingFee.TotalTax;
			}
		}
		/// <summary>
		/// Montant total ttc du panier y compris avec l'eco participation
		/// </summary>
		/// <value>The grand total with tax.</value>
		public decimal GrandTotalWithTax
		{
			get
			{
				return TotalWithTax + RecycleTotalWithTax + ShippingFeeTotalWithTax;
			}
		}

		/// <summary>
		/// Montant total du port de livraison TTC
		/// </summary>
		/// <value>The shipping fee total with tax.</value>
		public decimal ShippingFeeTotalWithTax
		{
			get
			{
				return ShippingFee.TotalWithTax;
			}
		}

		/// <summary>
		/// Montant de la remise globale en pourcentage
		/// </summary>
		/// <value>The discount.</value>
		public double Discount
		{
			get
			{
				if (Total == 0)
				{
					return 0;
				}
				return Convert.ToDouble(DiscountTotal / Total);
			}
		}

		#endregion

		/// <summary>
		/// Frais de livraison
		/// </summary>
		/// <value>The shipping fee.</value>
		public Fee ShippingFee { get; set; }

		/// <summary>
		/// Frais de port verouillé par le créateur du panier
		/// </summary>
		/// <value><c>true</c> if [shipping fee locked]; otherwise, <c>false</c>.</value>
		public bool ShippingFeeLocked { get; set; }

		/// <summary>
		/// Montant global de la remise
		/// </summary>
		/// <value>The global discount.</value>
		public decimal DiscountTotal { get; set; }

		/// <summary>
		/// Montant du taux de taxe sur la remise globale
		/// </summary>
		/// <value>The discount total tax rate.</value>
		public double DiscountTotalTaxRate { get; set; }

		/// <summary>
		/// Montant à atteindre pour obtenir un franco de port
		/// </summary>
		/// <value>The free of shipping amount.</value>
		public decimal FreeOfShippingAmount { get; set; }

		/// <summary>
		/// Montant minimal de commande
		/// </summary>
		/// <value>The minimal order amount.</value>
		public decimal MinimalOrderAmount { get; set; }

		/// <summary>
		/// Transporteur selectionné
		/// </summary>
		/// <value>The conveyor.</value>
		public Conveyor Conveyor { get; set; }

		/// <summary>
		/// Accepte la conversion d'un devis en commande
		/// </summary>
		/// <value><c>true</c> if [accept conversion]; otherwise, <c>false</c>.</value>
		public bool? AcceptConversion { get; set; }

		private Coupon m_Coupon;
		/// <summary>
		/// Coupon associé au panier
		/// </summary>
		/// <value>The coupon.</value>
		public Coupon Coupon 
		{
			get
			{
				if (m_Coupon == null)
				{
					if (GetCouponByCode != null)
					{
						m_Coupon = GetCouponByCode.Invoke(this.CouponCode);
					}
				}
				return m_Coupon;
			}
			set
			{
				m_Coupon = value;
			}
		}

		/// <summary>
		/// Code du coupon de reduction
		/// </summary>
		/// <value>The coupon code.</value>
		public string CouponCode { get; set; }

		/// <summary>
		/// Raison de l'application du coupon
		/// </summary>
		/// <value>The coupon reason.</value>
		public string CouponReason { get; set; }

		/// <summary>
		/// Methode de recupération du coupon de reduction
		/// en lazy loading
		/// </summary>
		/// <value>The get coupon by code.</value>
		public GetCouponByCode GetCouponByCode { get; set; }

		/// <summary>
		/// Identifiant de la source de création d'un panier
		/// </summary>
		/// <value>From entity id.</value>
		public int? FromEntityId { get; set; }
		/// <summary>
		/// Numéro du type de source de création d'un panier
		/// </summary>
		/// <value>From meta entity id.</value>
		public int? FromMetaEntityId { get; set; }
	}
}