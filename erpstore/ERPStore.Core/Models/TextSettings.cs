using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Configuration des textes legaux
	/// </summary>
	[Serializable]
	public class TextSettings
	{
		/// <summary>
		/// Texte des conditions générales de vente
		/// </summary>
		/// <value>The terms and conditions.</value>
		public string TermsAndConditions { get; set; }
		/// <summary>
		/// Texts legaux
		/// </summary>
		/// <value>The disclaimer.</value>
		public string Disclaimer { get; set; }
		/// <summary>
		/// Numéro de déclaration à la CNIL
		/// </summary>
		/// <value>The cnil number.</value>
		public string CnilNumber { get; set; }
		/// <summary>
		/// Texte sur les droits d'auteur du site
		/// </summary>
		/// <value>The copyright.</value>
		public string Copyright { get; set; }
		/// <summary>
		/// Texte officiel sur une facture
		/// </summary>
		/// <value>The invoice disclaimer.</value>
		public string InvoiceDisclaimer { get; set; }
	}
}
