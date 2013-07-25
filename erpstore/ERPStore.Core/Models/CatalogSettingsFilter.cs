using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	[Serializable]
	public class CatalogSettingsFilter
	{
		public CatalogSettingsFilter()
		{
			// ProductCategoryIdList = new List<int>();
			// BrandIdList = new List<int>();
			// SelectionNameList = new List<string>();
		}

		// public List<int> ProductCategoryIdList { get; private set; }
		public int? ProductCategoryId { get; set; }

		// public List<int> BrandIdList { get; private set; }
		public int? BrandId { get; set; }

		// public List<string> SelectionNameList { get; set; }
		public string ProductSelectionName { get; set; }

		//public CatalogSettingsFilter AddProductCategoryIdRange(List<int> idList)
		//{
		//    this.ProductCategoryIdList.AddRange(idList);
		//    return this;
		//}

		//public CatalogSettingsFilter AddBrandIdRange(List<int> idList)
		//{
		//    this.BrandIdList.AddRange(idList);
		//    return this;
		//}

		//public CatalogSettingsFilter AddSelectionNameRange(List<string> selectionNameList)
		//{
		//    this.SelectionNameList.AddRange(selectionNameList);
		//    return this;
		//}

		//public CatalogSettingsFilter AddSelectionName(string selectionName)
		//{
		//    this.SelectionNameList.Add(selectionName);
		//    return this;
		//}
	}
}
