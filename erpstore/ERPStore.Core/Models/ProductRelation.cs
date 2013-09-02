using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	[Serializable]
	public class ProductRelation
	{
		public ProductRelation()
		{

		}

		public int ParentProductId { get; set; }
		public int ChildProductId { get; set; }
		public ProductRelationType ProductRelationType { get; set; }
		public int Position { get; set; }
	}
}
