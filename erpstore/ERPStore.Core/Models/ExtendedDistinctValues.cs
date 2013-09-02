using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Valeur distinct d'une propriété etendue d'une entité
	/// </summary>
	[Serializable]
	public class ExtendedDistinctValues
	{
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public string Value { get; set; }
		/// <summary>
		/// Nombre de fois ou la valeur est présente par entité
		/// </summary>
		/// <value>The count.</value>
		public int Count { get; set; }
	}
}
