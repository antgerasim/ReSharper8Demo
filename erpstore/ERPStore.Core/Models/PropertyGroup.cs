using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Groupe de propriété etendu, permet d'ajouter des propriété sur une entité
	/// </summary>
	[Serializable]
	public class PropertyGroup 
	{
		/// <summary>
		/// Nom du groupe
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }
		/// <summary>
		/// Description du groupe
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; }
		/// <summary>
		/// Liste des noms de propriété du groupe
		/// </summary>
		/// <value>The property name list.</value>
		public List<string> PropertyNameList { get; set; }
		/// <summary>
		/// Liste des valeurs distinctes enregistrées par propriété etendue
		/// </summary>
		/// <value>The distinct property values.</value>
		public Dictionary<string, List<ExtendedDistinctValues>> DistinctPropertyValues { get; set; }
	}
}
