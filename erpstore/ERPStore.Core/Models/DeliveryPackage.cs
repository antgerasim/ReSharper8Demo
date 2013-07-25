using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Informations liée a la livraison d'un colis concernant une commande
	/// </summary>
	[Serializable]
	public class DeliveryPackage
	{
		/// <summary>
		/// Identifiant interne de la livraison
		/// </summary>
		/// <value>The id.</value>
		public int Id { get; set; }
		/// <summary>
		/// Poids en gramme de la livraison
		/// </summary>
		/// <value>The weight.</value>
		public int Weight { get; set; }
		/// <summary>
		/// Identifiant du colis (Transporteur)
		/// </summary>
		/// <value>The track id.</value>
		public string TrackId { get; set; }
		/// <summary>
		/// Adresse internet du transporteur pour l'affichage du détail du colis
		/// </summary>
		/// <value>The track URL.</value>
		public string TrackUrl { get; set; }
		/// <summary>
		/// Description du colis
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; }
		/// <summary>
		/// Date de création
		/// </summary>
		/// <value>The creation date.</value>
		public DateTime CreationDate { get; set; }
		/// <summary>
		/// Date du départ du colis de l'entrepot
		/// </summary>
		/// <value>The shipping date.</value>
		public DateTime ShippingDate { get; set; }
	}
}
