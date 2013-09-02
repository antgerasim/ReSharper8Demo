using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Parametre de configuration globaux du catalog
	/// </summary>
	[Serializable]
	public class CatalogSettings
	{
		public CatalogSettings()
		{
			PageSize = 10;
			PageParameterName = "page";
			AllowShowDestockWithoutStock = false;
		}

		/// <summary>
		/// Nombre d'items affiché par page
		/// </summary>
		/// <value>The size of the page.</value>
		public int PageSize { get; set; }

		/// <summary>
		/// Nom du paramètre de paging
		/// </summary>
		/// <value>The name of the page parameter.</value>
		public string PageParameterName { get; set; }

		/// <summary>
		/// Autorise l'affichage des produits destockés sans stock physique
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [allow destock without stock]; otherwise, <c>false</c>.
		/// </value>
		public bool AllowShowDestockWithoutStock { get; set; }

		/// <summary>
		/// Filtre du catalogue
		/// </summary>
		/// <value>The filter.</value>
		public CatalogSettingsFilter Filter { get; set; }
	}
}
