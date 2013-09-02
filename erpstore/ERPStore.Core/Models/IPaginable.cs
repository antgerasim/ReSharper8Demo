using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Paramètres de pagination d'une liste
	/// </summary>
	public interface IPaginable
	{
		/// <summary>
		/// Nombre d'items totaux dans la liste
		/// </summary>
		/// <value>The item count.</value>
		int ItemCount { get; set; }
		/// <summary>
		/// Index de la page en cours
		/// </summary>
		/// <value>The index of the page.</value>
		int PageIndex { get; set; }
		/// <summary>
		/// Nombre d'items par page
		/// </summary>
		/// <value>The size of the page.</value>
		int PageSize { get; set; }
	}
}
