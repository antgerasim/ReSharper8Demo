using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Traduction des propriétés d'une entité
	/// </summary>
	[Serializable]
	public class EntityLocalization
	{
		/// <summary>
		/// Le language pour la traduction
		/// </summary>
		/// <value>The language.</value>
		public Language Language { get; set; }
		/// <summary>
		/// Nom de la propriété à traduire
		/// </summary>
		/// <value>The name of the property.</value>
		public string PropertyName { get; set; }
		/// <summary>
		/// La valeur de la propriété traduite
		/// </summary>
		/// <value>The value.</value>
		public string Value { get; set; }
	}
}
