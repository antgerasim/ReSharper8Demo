using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Recuperation de la liste des commentaires pour un document de vente
	/// </summary>
	public delegate List<Comment> GetCommentList(ERPStore.Models.User user, SaleDocumentType document, int internalEntityId);

	/// <summary>
	/// Recuperation de la liste des items pour les documents de vente
	/// </summary>
	public delegate List<ISaleItem> GetItemList(int entityId);

	/// <summary>
	/// Recuperation de la liste des items pour une facture
	/// </summary>
	public delegate List<InvoiceItem> GetInvoiceItemList(int invoiceId);

	/// <summary>
	/// Récupération de la liste des frais pour une facture
	/// </summary>
	public delegate IEnumerable<Fee> GetInvoiceFeeList(int invoiceId);

	/// <summary>
	/// Retourne un coupon de reduction via son code
	/// </summary>
	public delegate Models.Coupon GetCouponByCode(string couponCode);

}
