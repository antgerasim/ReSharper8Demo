using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	[Serializable]
	public class Invoice
	{
		public Invoice()
		{
		}

		public SaleDocumentType Document
		{
			get
			{
				return SaleDocumentType.Invoice;
			}
		}

		/// <summary>
		/// Identitifiant interne de la facture
		/// </summary>
		/// <value>The id.</value>
		public int Id { get; set; }
		/// <summary>
		/// Code de la facture
		/// </summary>
		/// <value>The code.</value>
		public string Code { get; set; }
		/// <summary>
		/// Le client
		/// </summary>
		/// <value>The user.</value>
		public Models.User User { get; set; }
		/// <summary>
		/// Société associée à la facture
		/// </summary>
		/// <value>The customer.</value>
		public Corporate Customer { get; set; }
		/// <summary>
		/// Date de création
		/// </summary>
		/// <value>The creation date.</value>
		public DateTime CreationDate { get; set; }
		/// <summary>
		/// Date d'expiration du reglement
		/// </summary>
		/// <value>The expiration date.</value>
		public DateTime ExpirationDate { get; set; }
		/// <summary>
		/// Adresse de facturation
		/// </summary>
		/// <value>The billing address.</value>
		public Address BillingAddress { get; set; }
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
		/// Description du mode de règlement
		/// </summary>
		/// <value>The payment mode description.</value>
		public string PaymentModeDescription { get; set; }
		/// <summary>
		/// Status de la commande
		/// </summary>
		/// <value>The order status.</value>
		public InvoiceStatus Status { get; set; }

		/// <summary>
		/// Nombre d'item dans la commande
		/// </summary>
		/// <value>The item count.</value>
		public int ItemCount { get; set; }

		/// <summary>
		/// Numéro de tva du client
		/// </summary>
		/// <value>The customer vat number.</value>
		public string CustomerVatNumber { get; set; }

		/// <summary>
		/// Type de facture
		/// </summary>
		/// <value>The type.</value>
		public InvoiceType Type { get; set; }

		/// <summary>
		/// Nom du type de facture
		/// </summary>
		/// <value>The name of the invoice type.</value>
		public string TypeName { get; set; }

		/// <summary>
		/// Liste du détail de la commande
		/// </summary>
		/// <value>The items.</value>
		public LazyList<InvoiceItem> Items { get; set; }

		/// <summary>
		/// Liste des commentaires associés, laissé par l'utilisateur en cours
		/// </summary>
		/// <value>The comments.</value>
		public LazyList<Comment> Comments { get; set; }

		/// <summary>
		/// Liste des taxes groupées par Taux.
		/// </summary>
		/// <value>The tax list.</value>
		public IEnumerable<Price> TaxList 
		{
			get
			{
				var list = from item in Items
						   group item by item.SalePrice.TaxRate into g
						   select new Price(
							   g.Sum(i => i.GrandTotal)
							   , g.Key
						);

				return list;
			}
		}

		/// <summary>
		/// Liste des frais associé à la facture
		/// </summary>
		/// <value>The fee list.</value>
		public LazyList<Fee> FeeList { get; set; }

		#region Totaux

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

		#endregion

	}
}
