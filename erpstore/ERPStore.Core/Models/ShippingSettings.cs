using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	[Serializable]
	public class ShippingSettings
	{
		public ShippingSettings()
		{
			DeliveryCountryList = new List<DeliveryCountry>();
			ConveyorList = new List<Conveyor>();
			AllowPartialDelivery = false;
		}
		/// <summary>
		/// Taux de taxe par defaut pour le transport
		/// </summary>
		/// <value>The default shipping fee tax rate.</value>
		public double DefaultShippingFeeTaxRate { get; set; }

		/// <summary>
		/// Liste des pays livrés
		/// </summary>
		/// <value>The country list.</value>
		public IList<DeliveryCountry> DeliveryCountryList { get; private set; }

		/// <summary>
		/// Liste des transporteurs
		/// </summary>
		/// <value>The conveyor list.</value>
		public IList<Conveyor> ConveyorList { get; private set; }

		/// <summary>
		/// Transporteur selectionné par defaut
		/// </summary>
		/// <value>The default conveyor.</value>
		public Conveyor DefaultConveyor { get; set; }

		/// <summary>
		/// Configure les commandes comme acceptant les livraisons partielles par defaut.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [allow partial delivery]; otherwise, <c>false</c>.
		/// </value>
		public bool AllowPartialDelivery { get; set; }

	}
}
