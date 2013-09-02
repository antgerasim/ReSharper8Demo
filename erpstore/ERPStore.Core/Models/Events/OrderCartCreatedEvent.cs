using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models.Events
{
	/// <summary>
	/// Evenement lors de la création d'un panier de type commande
	/// </summary>
	[Serializable]
	public class OrderCartCreatedEvent
	{
		/// <summary>
		/// Le panier
		/// </summary>
		/// <value>The cart.</value>
		public OrderCart Cart { get; set; }
		/// <summary>
		/// Le visiteur ayant crée le panier
		/// </summary>
		/// <value>The user principal.</value>
		public UserPrincipal UserPrincipal { get; set; }
	}
}
