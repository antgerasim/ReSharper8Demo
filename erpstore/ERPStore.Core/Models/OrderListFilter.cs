using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Filtre pour l'affichage des commandes
	/// </summary>
	[Serializable]
	public class OrderListFilter
	{
		/// <summary>
		/// Critère de recherche
		/// </summary>
		/// <value>The search.</value>
		public string Search { get; set; }
		/// <summary>
		/// Status de la commande
		/// </summary>
		/// <value>The status.</value>
		public int StatusId { get; set; }
		/// <summary>
		/// Période de recherche
		/// </summary>
		/// <value>The period id.</value>
		public int PeriodId { get; set; }
	}
}
