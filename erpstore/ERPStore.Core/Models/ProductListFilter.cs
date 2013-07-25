using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ERPStore.Models
{
	/// <summary>
	/// Filtre pour la recherche
	/// </summary>
	[Serializable]
	public class ProductListFilter
	{
		public ProductListFilter()
		{
			ListType = ProductListType.All;
			SortList = new List<SortProductList>();
		}

		/// <summary>
		/// Liste des propriétés etendues
		/// </summary>
		/// <value>The extended parameters.</value>
		public NameValueCollection ExtendedParameters { get; set; }
		/// <summary>
		/// Id de la catégorie
		/// </summary>
		/// <value>The product category id.</value>
		public int? ProductCategoryId { get; set; }
		/// <summary>
		/// Id de la marque
		/// </summary>
		/// <value>The brand id.</value>
		public int? BrandId { get; set; }
		/// <summary>
		/// Identifiant du contact
		/// </summary>
		/// <value>The user id.</value>
		public int? UserId { get; set; }
		/// <summary>
		/// Identifiant de la société
		/// </summary>
		/// <value>The corporate id.</value>
		public int? CorporateId { get; set; }
		/// <summary>
		/// Terme de recherche
		/// </summary>
		/// <value>The search.</value>
		public string Search { get; set; }
		/// <summary>
		/// Type de liste
		/// </summary>
		/// <value>The type of the list.</value>
		public ProductListType ListType { get; set; }
		/// <summary>
		/// Identifiant d'une selection prédéfinie.
		/// </summary>
		/// <value>The selection id.</value>
		public int? SelectionId { get; set; }
		/// <summary>
		/// Liste des ordres de tri
		/// </summary>
		/// <value>The sort list.</value>
		public List<SortProductList> SortList { get; private set; }

		public override string ToString()
		{
			var result = new StringBuilder();
			result.AppendLine();
			result.AppendFormat("ProductCategoryId : {0}{1}", ProductCategoryId, Environment.NewLine);
			result.AppendFormat("BrandId : {0}{1}", BrandId, Environment.NewLine);
			result.AppendFormat("CorporateId : {0}{1}", CorporateId, Environment.NewLine);
			result.AppendFormat("UserId : {0}{1}", UserId, Environment.NewLine);
			result.AppendFormat("Search : {0}{1}", Search, Environment.NewLine);
			result.AppendFormat("ListType : {0}{1}", ListType, Environment.NewLine);
			if (ExtendedParameters != null)
			{
				foreach (string item in ExtendedParameters)
				{
					result.AppendFormat("{0} : {1}{2}", item, ExtendedParameters[item], Environment.NewLine);
				}
			}
			return result.ToString();
		}
	}
}
