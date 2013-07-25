using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Liste des groupes et valeurs des propriétés etendues d'une entité
	/// </summary>
	[Serializable]
	public class ExtendedPropertyList : Dictionary<PropertyGroup, Dictionary<string, string>>
	{
		public ExtendedPropertyList()
		{

		}

		/// <summary>
		/// Gets the ExtendedPropertyList
		/// </summary>
		/// <value></value>
		public Dictionary<string, string> this[string name]
		{
			get
			{
				var group = this.SingleOrDefault(i => i.Key.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
				if (group.Key != null)
				{
					return group.Value;
				}
				return null;
			}
		}
	}
}
