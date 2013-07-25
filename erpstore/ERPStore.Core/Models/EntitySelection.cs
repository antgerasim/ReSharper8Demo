using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Représente un selection d'entités
	/// </summary>
	public class EntitySelection
	{
		/// <summary>
		/// Identifiant interne de la selection
		/// </summary>
		/// <value>The id.</value>
		public int Id { get; set; }
		/// <summary>
		/// Nom de la selection
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }
		/// <summary>
		/// Description de la selection
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; }
	}
}
