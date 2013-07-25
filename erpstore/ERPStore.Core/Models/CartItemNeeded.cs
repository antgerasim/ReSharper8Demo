using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Information avant ajout d'un item dans le panier
	/// </summary>
	[Serializable]
	public class CartItemNeeded
	{
		public Product Product { get; set; }
		public int Quantity { get; set; }
		public string CartUrl { get; set; }
		public Price Price { get; set; }
		public OrderCart Cart { get; set; }
	}
}
