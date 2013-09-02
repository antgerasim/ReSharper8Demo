using System;
namespace ERPStore.Models
{
	public interface ISaleItem
	{
		string CustomerProductCode { get; set; }
		DateTime? DeliveryDelay { get; set; }
		string Designation { get; set; }
		double Discount { get; set; }
		string ExtendedDescription { get; set; }
		decimal GrandTaxTotal { get; }
		decimal GrandTotal { get; }
		decimal GrandTotalWithTax { get; }
		int PackagingValue { get; set; }
		Product Product { get; set; }
		string ProductCode { get; set; }
		int Quantity { get; set; }
		Price RecyclePrice { get; set; }
		decimal RecycleTaxTotal { get; }
		decimal RecycleTotal { get; }
		decimal RecycleTotalWithTax { get; }
		Price SalePrice { get; set; }
		int SaleUnitValue { get; set; }
		ShippingType ShippingType { get; set; }
		double TaxRate { get; set; }
		decimal Total { get; }
		decimal TotalTax { get; }
		decimal TotalWithTax { get; }
		int Balance { get; set; }
		bool IsBalanced { get; set; }
	}
}
