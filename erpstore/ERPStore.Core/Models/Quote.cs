using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	[Serializable]
	public class Quote : ISaleDocument
	{
        public Quote()
		{
			ShippingFee = new Fee()
			{
				Amount = 0,
				Quantity = 1,
				TaxRate = 0,
			};
		}

		public SaleDocumentType Document
		{
			get
			{
				return SaleDocumentType.Quote;
			}
		}

		/// <summary>
		/// Identitifiant interne de la commande
		/// </summary>
		/// <value>The id.</value>
		public int Id { get; set; }
		/// <summary>
		/// Code de la commande
		/// </summary>
		/// <value>The code.</value>
		public string Code { get; set; }
		/// <summary>
		/// Le client
		/// </summary>
		/// <value>The user.</value>
		public User User { get; set; }
		/// <summary>
		/// Message supplémentaire à indiquer sur la commande
		/// </summary>
		/// <value>The message for customer.</value>
		public string MessageForCustomer { get; set; }
		/// <summary>
		/// Date de création
		/// </summary>
		/// <value>The creation date.</value>
		public DateTime CreationDate { get; set; }
		/// <summary>
		/// Date d'expiration de l'offre
		/// </summary>
		/// <value>The expiration date.</value>
		public DateTime ExpirationDate { get; set; }
		/// <summary>
		/// Accepte les livraisons partielles de la commande
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [allow partial delivery]; otherwise, <c>false</c>.
		/// </value>
		public bool AllowPartialDelivery { get; set; }
		/// <summary>
		/// Référence de la commande pour le client
		/// </summary>
		/// <value>The customer document reference.</value>
		public string CustomerDocumentReference { get; set; }
		/// <summary>
		/// Adresse de livraison
		/// </summary>
		/// <value>The shipping address.</value>
		public Address ShippingAddress { get; set; }
		/// <summary>
		/// Adresse de facturation
		/// </summary>
		/// <value>The billing address.</value>
		public Address BillingAddress { get; set; }
		/// <summary>
		/// Frais de port
		/// </summary>
		/// <value>The shipping fee.</value>
		public Fee ShippingFee { get; private set; }
		/// <summary>
		/// Est un cadeau
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is present; otherwise, <c>false</c>.
		/// </value>
		public bool IsPresent { get; set; }
		///// <summary>
		///// Mode de reglement
		///// </summary>
		///// <value>The payment mode.</value>
		//// public PaymentMode PaymentMode { get; set; }

		/// <summary>
		/// Nom du mode de reglement
		/// </summary>
		/// <value>The name of the payment mode.</value>
		public string PaymentModeName { get; set; }

		/// <summary>
		/// Description du mode de reglement
		/// </summary>
		/// <value>The payment description.</value>
		public string PaymentModeDescription { get; set; }

        /// <summary>
		/// Status du devis
		/// </summary>
		/// <value>The order status.</value>
		public QuoteStatus Status { get; set; }

		/// <summary>
		/// Liste du détail de la commande
		/// </summary>
		/// <value>The items.</value>
        public LazyList<ISaleItem> Items { get; set; }

		/// <summary>
		/// Nombre d'items dans le devis
		/// </summary>
		/// <value>The item count.</value>
		public int ItemCount { get ; set; }

		/// <summary>
		/// Liste des commentaires associés, laissé par l'utilisateur en cours
		/// </summary>
		/// <value>The comments.</value>
		public LazyList<Comment> Comments { get; set; }

		/// <summary>
		/// Chargé de compte
		/// </summary>
		/// <value>The vendor.</value>
		public Vendor Vendor { get; set; }

		#region Totals

		/// <summary>
		/// Montant total HT du panier sans l'eco participation
		/// </summary>
		/// <value>The total.</value>
		public decimal Total { get ; set; }

		/// <summary>
		/// Montant de la tva du panier sans l'eco participation
		/// </summary>
		/// <value>The total tax.</value>
		public decimal TotalTax { get ; set; }

		/// <summary>
		/// Montant total TTC du panier sans l'eco participation
		/// </summary>
		/// <value>The total with tax.</value>
		public decimal TotalWithTax { get ; set; }

		/// <summary>
		/// Montant total ht de l'eco participation
		/// </summary>
		/// <value>The recycle total.</value>
		public decimal RecycleTotal { get ; set; }

		/// <summary>
		/// Montant total de la tva de l'eco participation
		/// </summary>
		/// <value>The recycle tax total.</value>
		public decimal RecycleTaxTotal { get ; set; }

		/// <summary>
		/// Montant total ttc de l'eco participation
		/// </summary>
		/// <value>The recycle total with tax.</value>
		public decimal RecycleTotalWithTax { get ; set; }

		/// <summary>
		/// Montant total ht du panier y compris l'eco participation
		/// </summary>
		/// <value>The grand total.</value>
		public decimal GrandTotal { get ; set; }

		/// <summary>
		/// Montant total de la tva y compris avec l'eco participation
		/// </summary>
		/// <value>The grand tax total.</value>
		public decimal GrandTaxTotal { get ; set; }

		/// <summary>
		/// Montant total ttc du panier y compris avec l'eco participation
		/// </summary>
		/// <value>The grand total with tax.</value>
		public decimal GrandTotalWithTax { get ; set; }

		/// <summary>
		/// Montant total du port de livraison TTC
		/// </summary>
		/// <value>The shipping fee total with tax.</value>
		public decimal ShippingFeeTotalWithTax { get ; set; }

		#endregion

	}
}
