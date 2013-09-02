using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Terme de recherche existant
	/// </summary>
	[Serializable]
	public class SearchTerm
	{
		/// <summary>
		/// Terme de recherche
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }
		/// <summary>
		/// Niveau 
		/// </summary>
		/// <value>The level.</value>
		public int Level { get; set; }
		/// <summary>
		/// Nombre d'utilisation sur un an glissant
		/// </summary>
		/// <value>The search count.</value>
		public int SearchCount { get; set; }
		/// <summary>
		/// Nombre de résultat
		/// </summary>
		/// <value>The result count.</value>
		public int ResultCount { get; set; }
	}
}
