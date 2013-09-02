using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Indique l'emballage d'un produit
	/// </summary>
	[Serializable]
	public class Packaging
	{
		/// <summary>
		/// Nombre de produits dans l'emballage
		/// </summary>
		/// <value>The value.</value>
		public int Value { get; set; }
		/// <summary>
		/// L'emballage est-il splittable
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is splittable; otherwise, <c>false</c>.
		/// </value>
		public bool IsSplittable { get; set; }
		/// <summary>
		/// Tarif du produit dans le cas ou le produit est vendu unitairement ou
		/// si la quantité vendue ne correspond pas au nombres de produits 
		/// dans l'emballage
		/// </summary>
		/// <value>The splitted unit price.</value>
		public Price SplittedUnitPrice { get; set; }
		/// <summary>
		/// Le produit correspondant à l'emballage
		/// </summary>
		/// <value>The package product.</value>
		public Product PackageProduct { get; set; }
	}
}
