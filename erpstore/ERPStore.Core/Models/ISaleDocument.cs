using System;
using System.Collections.Generic;

namespace ERPStore.Models
{
	/// <summary>
	/// Entité de type vente (Commande ou Devis)
	/// </summary>
	public interface ISaleDocument
	{
		int Id { get; set; }
		SaleDocumentType Document { get; }
		bool AllowPartialDelivery { get; set; }
		Address BillingAddress { get; set; }
		string Code { get; set; }
		LazyList<Comment> Comments { get; }
		DateTime CreationDate { get; set; }
		string CustomerDocumentReference { get; set; }
		decimal GrandTaxTotal { get; set; }
		decimal GrandTotal { get; set; }
		decimal GrandTotalWithTax { get; set; }
		bool IsPresent { get; set; }
		int ItemCount { get; set; }
		string MessageForCustomer { get; set; }
		//PaymentMode PaymentMode { get; set; }
		string PaymentModeName { get; set; }
		string PaymentModeDescription { get; set; }
		decimal RecycleTaxTotal { get; set; }
		decimal RecycleTotal { get; set; }
		decimal RecycleTotalWithTax { get; set; }
		Address ShippingAddress { get; set; }
		Fee ShippingFee { get; }
		decimal ShippingFeeTotalWithTax { get; set; }
		decimal Total { get; set; }
		decimal TotalTax { get; set; }
		decimal TotalWithTax { get; set; }
		User User { get; set; }
		Vendor Vendor { get; set; }
		LazyList<ISaleItem> Items { get; }
	}
}
