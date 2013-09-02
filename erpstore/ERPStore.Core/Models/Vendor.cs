using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Chargé d'affaire pour un client de type société
	/// </summary>
	[Serializable]
	public class Vendor
	{
		/// <summary>
		/// Numéro interne du vendeur
		/// </summary>
		/// <value>The id.</value>
		public int Id { get; set; }
		/// <summary>
		/// Nom complet du vendeur
		/// </summary>
		/// <value>The full name.</value>
		public string FullName { get; set; }
		/// <summary>
		/// Email
		/// </summary>
		/// <value>The email.</value>
		public string Email { get; set; }
		/// <summary>
		/// Numéro de mobile
		/// </summary>
		/// <value>The mobile number.</value>
		public string MobileNumber { get; set; }
		/// <summary>
		/// Numéro de téléphone
		/// </summary>
		/// <value>The phone number.</value>
		public string PhoneNumber { get; set; }
		/// <summary>
		/// Photo par defaut du vendeur
		/// </summary>
		/// <value>The default image.</value>
		public Media DefaultImage { get; set; }
	}
}
