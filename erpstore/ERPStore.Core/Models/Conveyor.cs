using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Transporteur
	/// </summary>
	[Serializable]
	public class Conveyor
	{
		public Conveyor()
		{
			CountryList = new List<Country>();
		}
		/// <summary>
		/// Identifiant internet
		/// </summary>
		/// <value>The id.</value>
		public int Id { get; set; }
		/// <summary>
		/// Nom du transporteur
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }
		/// <summary>
		/// Code du transporteur
		/// </summary>
		/// <value>The code.</value>
		public string Code { get; set; }
		/// <summary>
		/// Montant unitaire par niveau de transportabilité d'un produit
		/// </summary>
		/// <value>The unit price by transport level.</value>
		public decimal UnitPriceByTransportLevel { get; set; }

		/// <summary>
		/// Liste des pays servis
		/// </summary>
		/// <value>The country list.</value>
		public IList<Country> CountryList
		{
			get;
			private set;
		}
	}
}
