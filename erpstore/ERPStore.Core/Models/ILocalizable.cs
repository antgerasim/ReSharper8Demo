using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Permet d'indiquer qu'une entité est traductible
	/// </summary>
	public interface ILocalizable
	{
		int Id { get; set; }
		/// <summary>
		/// Gets or sets the localization list.
		/// </summary>
		/// <value>The localization list.</value>
		IEnumerable<EntityLocalization> LocalizationList { get; set; }
	}
}
