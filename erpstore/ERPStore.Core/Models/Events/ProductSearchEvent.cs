using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models.Events
{
	/// <summary>
	/// Valeur d'un resultat de recherche de produits
	/// </summary>
	[Serializable]
	public class ProductSearchEvent
	{
		public ProductSearchEvent(Models.UserPrincipal principal, string query, int resultCount, string searchEngineName)
		{
			this.Query = query;
			this.ResultCount = resultCount;
			this.SearchEngineName = searchEngineName;
			this.Principal = principal;
		}
		/// <summary>
		/// La requete
		/// </summary>
		/// <value>The query.</value>
		public string Query { get; set; }
		/// <summary>
		/// Nombre de produits trouvés
		/// </summary>
		/// <value>The result count.</value>
		public int ResultCount { get; set; }
		/// <summary>
		/// Nom du moteur de recherche utilisé
		/// </summary>
		/// <value>The search engine.</value>
		public string SearchEngineName { get; set; }
		/// <summary>
		/// Utilisateur en cours
		/// </summary>
		/// <value>The principal.</value>
		public Models.UserPrincipal Principal { get; set; }
	}
}
