using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models.Events
{
	public class AddCartItemEvent
	{
		public Product Product { get; set; }
		public int Quantity { get; set; }
		public Models.Price SalePrice { get; set; }
	}
}
