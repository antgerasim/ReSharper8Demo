using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Filtre pour l'affichage de la liste des factures
	/// </summary>
	[Serializable]
	public class InvoiceListFilter
	{
		/// <summary>
		/// Terme de recherche
		/// </summary>
		/// <value>The search.</value>
		public string Search { get; set; }
		/// <summary>
		/// Période de recherche
		/// </summary>
		/// <value>The period id.</value>
		public int PeriodId { get; set; }
		/// <summary>
		/// Status des factures
		/// </summary>
		/// <value>The status id.</value>
		public int StatusId { get; set; }
	}
}
