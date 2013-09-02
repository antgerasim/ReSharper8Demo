using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Société cliente
	/// </summary>
	[Serializable]
	public class Corporate
	{
		/// <summary>
		/// Identifiant interne
		/// </summary>
		/// <value>The id.</value>
		public int Id { get; set; }
		/// <summary>
		/// Nom
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }
		/// <summary>
		/// Code
		/// </summary>
		/// <value>The code.</value>
		public string Code { get; set; }
		/// <summary>
		/// Numéro de fax
		/// </summary>
		/// <value>The fax number.</value>
		public string FaxNumber { get; set; }
		/// <summary>
		/// Numéro de téléphone
		/// </summary>
		/// <value>The phone number.</value>
		public string PhoneNumber { get; set; }
		/// <summary>
		/// Email
		/// </summary>
		/// <value>The email.</value>
		public string Email { get; set; }
		/// <summary>
		/// Site web
		/// </summary>
		/// <value>The web site.</value>
		public string WebSite { get; set; }
		/// <summary>
		/// Date de création
		/// </summary>
		/// <value>The creation date.</value>
		public DateTime CreationDate { get; set; }
		/// <summary>
		/// Status social
		/// </summary>
		/// <value>The social status.</value>
		public string SocialStatus { get; set; }
		/// <summary>
		/// N°Siret
		/// </summary>
		/// <value>The siret number.</value>
		public string SiretNumber { get; set; }
		/// <summary>
		/// N°RCS
		/// </summary>
		/// <value>The RCS number.</value>
		public string RcsNumber { get; set; }
		/// <summary>
		/// Code NAF
		/// </summary>
		/// <value>The naf code.</value>
		public string NafCode { get; set; }
		/// <summary>
		/// N°TVA
		/// </summary>
		/// <value>The tva number.</value>
		public string VatNumber { get; set; }
		/// <summary>
		/// Adresse par defaut
		/// </summary>
		/// <value>The default address.</value>
		public Address DefaultAddress { get; set; }
		/// <summary>
		/// Code language pour les reports
		/// </summary>
		/// <value>The language code.</value>
		public string LanguageCode { get; set; }
		/// <summary>
		/// Clé API
		/// </summary>
		/// <value>The API key.</value>
		public string ApiKey { get; set; }
		/// <summary>
		/// Vendeur par defaut associé 
		/// </summary>
		/// <value>The vendor.</value>
		public Vendor Vendor { get; set; }
		/// <summary>
		/// Identifiant de la Categorie de client
		/// </summary>
		/// <value>The category id.</value>
		public int? CategoryId { get; set; }
		/// <summary>
		/// Indique l'id du mode de reglement par defaut
		/// </summary>
		/// <value>The default payment mode id.</value>
		public int DefaultPaymentModeId { get; set; }
		/// <summary>
		/// Indique si l'on doit appliquer la TVA
		/// </summary>
		/// <value>The vat mandatory.</value>
		public bool VatMandatory { get; set; }
	}
}
