using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Configuration des informations de contact
	/// </summary>
	[Serializable]
	public class ContactSettings
	{
		/// <summary>
		/// Numéro de téléphone pour contacter le service commercial du site
		/// </summary>
		/// <value>The contact phone number.</value>
		public string ContactPhoneNumber { get; set; }
		/// <summary>
		/// Email pour contacter le service commercial du site
		/// </summary>
		/// <value>The contact email.</value>
		public string ContactEmail { get; set; }
		/// <summary>
		/// Nom d'Email pour contacter le service commercial du site
		/// </summary>
		/// <value>The contact email.</value>
		public string ContactEmailName { get; set; }
		/// <summary>
		/// Numéro de fax pour contacter le service commercial du site
		/// </summary>
		/// <value>The contact fax number.</value>
		public string ContactFaxNumber { get; set; }
		/// <summary>
		/// Heures pendant lesquelles il est possible de contacter le service commercial
		/// </summary>
		/// <value>The office hours.</value>
		public string OfficeHours { get; set; }
		/// <summary>
		/// Gets or sets the default address.
		/// </summary>
		/// <value>The default address.</value>
		public Address DefaultAddress { get; set; }
		/// <summary>
		/// Adresse Email pour l'envoi des Emails
		/// </summary>
		/// <value>The email sender.</value>
		public string EmailSender { get; set; }
		/// <summary>
		/// Nom de l'envoyeur des Emails
		/// </summary>
		/// <value>The name of the email sender.</value>
		public string EmailSenderName { get; set; }
		/// <summary>
		/// Adresse de retour des colis
		/// </summary>
		/// <value>The return address.</value>
		public Address ReturnAddress { get; set; }
		/// <summary>
		/// Nom de l'entreprise
		/// </summary>
		/// <value>The name of the corporate.</value>
		public string CorporateName { get; set; }
		/// <summary>
		/// Code NAF
		/// </summary>
		/// <value>The naf code.</value>
		public string NafCode { get; set; }
		/// <summary>
		/// Numéro SIRET
		/// </summary>
		/// <value>The siret number.</value>
		public string SiretNumber { get; set; }
		/// <summary>
		/// Numéro au registre du commerce
		/// </summary>
		/// <value>The RCS number.</value>
		public string RcsNumber { get; set; }
		/// <summary>
		/// Statut social
		/// </summary>
		/// <value>The corporate status.</value>
		public string SocialStatus { get; set; }
		/// <summary>
		/// Numéro de TVA
		/// </summary>
		/// <value>The VAT number.</value>
		public string VATNumber { get; set; }
		/// <summary>
		/// Adresse email en destinataire caché pour les mails
		/// </summary>
		/// <value>The BCC email.</value>
		public string BCCEmail { get; set; }
	}
}
