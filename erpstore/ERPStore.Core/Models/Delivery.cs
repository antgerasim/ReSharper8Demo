using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ERPStore.Models
{
	/// <summary>
	/// Bon de livraison pour une commande
	/// </summary>
	[Serializable]
	[DataContract]
	public class Delivery
	{
		/// <summary>
		/// Identifiant interne du bon de livraison
		/// </summary>
		/// <value>The id.</value>
		public int Id { get; set; }

		/// <summary>
		/// Date de création du bon de livraison
		/// </summary>
		/// <value>The creation date.</value>
		[DataMember]
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Message à afficher sur le bon de livraison
		/// </summary>
		/// <value>The message.</value>
		[DataMember]
		public string Message { get; set; }

		/// <summary>
		/// Affichage des prix dans le report
		/// </summary>
		/// <value><c>true</c> if [show prices]; otherwise, <c>false</c>.</value>
		[DataMember]
		public bool ShowPrices { get; set; }

		/// <summary>
		/// Adresse de destination de la marchandise
		/// </summary>
		/// <value>The address.</value>
		[DataMember]
		public Models.Address Address { get; set; }

		/// <summary>
		/// Transporteur sélectionné pour la livraison
		/// </summary>
		/// <value>The conveyor.</value>
		[DataMember]
		public Models.Conveyor Conveyor { get; set; }

		/// <summary>
		/// Code du bon de livraison
		/// </summary>
		/// <value>The code.</value>
		[DataMember]
		public string Code { get; set; }

		/// <summary>
		/// Poids total des produits livrés
		/// </summary>
		/// <value>The weight.</value>
		[DataMember]
		public int Weight { get; set; }

		/// <summary>
		/// Nombre de colis
		/// </summary>
		/// <value>The package count.</value>
		[DataMember]
		public int PackageCount { get; set; }

		/// <summary>
		/// Adresse du point de relai
		/// </summary>
		/// <value>The pickup address.</value>
		[DataMember]
		public Models.Address PickupAddress { get; set; }

		/// <summary>
		/// Détail du bon de livraison
		/// </summary>
		/// <value>The items.</value>
		[DataMember]
		public List<DeliveryItem> Items { get; set; }

		/// <summary>
		/// Informations de livraison auprès du transporteur
		/// </summary>
		/// <value>The delivery package.</value>
		public DeliveryPackage DeliveryPackage { get; set; }
	}
}
