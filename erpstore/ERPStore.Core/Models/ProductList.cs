using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace ERPStore.Models
{
	/// <summary>
	/// Liste des produits trouvés lors d'une recherche
	/// </summary>
	[Serializable]
	public class ProductList : List<Product>, IPaginable
	{
		public ProductList(IEnumerable<Product> list)
			: base(list)
		{

		}

		/// <summary>
		/// Terme de recherche
		/// </summary>
		/// <value>The search.</value>
		public string Query { get; set; }

		/// <summary>
		/// Liste des propriétés etendues passée en parametre de l'url
		/// </summary>
		/// <value>The extended parameters.</value>
		public NameValueCollection ExtendedParameters { get; set; }

		/// <summary>
		/// categorie passée en paramètre de l'url
		/// </summary>
		/// <value>The category code.</value>
		public Models.ProductCategory Category { get; set; }

		/// <summary>
		/// Marque passée en paramètre de l'url
		/// </summary>
		/// <value>The brand.</value>
		public Models.Brand Brand { get; set; }

		/// <summary>
		/// Nom de la selection en cours
		/// </summary>
		/// <value>The name of the selection.</value>
		public string SelectionName { get; set; }

		/// <summary>
		/// Type de liste
		/// </summary>
		/// <value>The type of the list.</value>
		public ProductListType ListType { get; set; }

		/// <summary>
		/// Type de relation entre les produits
		/// </summary>
		/// <value>The type of the relation.</value>
		public ProductRelationType RelationType { get; set; }

		#region IPaginable Members

		public int ItemCount { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }

		#endregion
	}
}
