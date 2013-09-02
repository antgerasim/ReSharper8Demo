using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ERPStore.Models
{
	/// <summary>
	/// Détail d'une livraison
	/// </summary>
	[Serializable]
	[DataContract]
	public class DeliveryItem
	{
		/// <summary>
		/// Quantité livré
		/// </summary>
		/// <value>The quantity.</value>
		[DataMember]
		public int Quantity { get; set; }

		/// <summary>
		/// Position de la ligne
		/// </summary>
		/// <value>The position.</value>
		[DataMember]
		public int? Position { get; set; }

		/// <summary>
		/// Produit livré
		/// </summary>
		/// <value>The product.</value>
		[DataMember]
		public Product Product { get; set; }

		/// <summary>
		/// Gets or sets the ordered quantity.
		/// </summary>
		/// <value>The ordered quantity.</value>
		[DataMember]
		public int OrderedQuantity { get; set; }

		/// <summary>
		/// Produit d'emballage
		/// </summary>
		/// <value>The product package.</value>
		[DataMember]
		public Product ProductPackage { get; set; }

		/// <summary>
		/// Nombre d'emballage
		/// </summary>
		/// <value>The product package quantity.</value>
		[DataMember]
		public int? ProductPackageQuantity { get; set; }
	}
}
