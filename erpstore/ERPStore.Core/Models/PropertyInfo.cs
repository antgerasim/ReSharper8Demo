using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Informations sur une propriété etendue
	/// </summary>
	[Serializable]
	public class PropertyInfo 
	{
		/// <summary>
		/// Id de la propriété
		/// </summary>
		/// <value>The id.</value>
		public string Id { get; set; }
		/// <summary>
		/// Nom d'affichage de la propriété.
		/// </summary>
		/// <value>The display name.</value>
		public string DisplayName { get; set; }
		/// <summary>
		/// Nom de regroupement.
		/// </summary>
		/// <value>The name of the group.</value>
		public string DisplayGroupName { get; set; }
		/// <summary>
		/// Identifiant du groupe de propriétés.
		/// </summary>
		/// <value>The property group id.</value>
		public int PropertyGroupId { get; set; }

	}
}
