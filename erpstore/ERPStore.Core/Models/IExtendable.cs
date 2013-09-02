using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Permet d'indiquer qu'une entité peut avoir des propriétés etendues
	/// </summary>
	public interface IExtendable
	{
		/// <summary>
		/// Liste des propriétés etendues
		/// </summary>
		/// <value>The extended properties.</value>
		ExtendedPropertyList ExtendedProperties { get; set; }
	}
}
