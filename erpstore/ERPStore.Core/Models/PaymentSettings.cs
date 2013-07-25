using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Configuration des modes de reglement
	/// </summary>
	[Serializable]
	public class PaymentSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PaymentSettings"/> class.
		/// </summary>
		public PaymentSettings()
		{
			ShowPriceWithTax = false;
			//UseAccount = false;
			//UseCheck = false;
			//UseOgone = false;
			//UsePaypal = false;
			//UseWireTransfer = false;
			AllowedByAccountPaymentModeIdList = new List<int>();
			DisableRecycleOnExport = false;
		}

		/// <summary>
		/// Relevé d'identité bancaire pour les reglements par virement bancaire
		/// </summary>
		/// <value>The bank account.</value>
		public BankAccount BankAccount { get; set; }
		/// <summary>
		/// Tous les prix sur le site sont affichés TTC
		/// </summary>
		/// <value><c>true</c> if [show price with tax]; otherwise, <c>false</c>.</value>
		public bool ShowPriceWithTax { get; set; }
		/// <summary>
		/// Adresse de remise des cheques
		/// </summary>
		/// <value>The check delivery address.</value>
		public Address CheckDeliveryAddress { get; set; }
		///// <summary>
		///// Utilise le mode de reglement par cheque
		///// </summary>
		///// <value><c>true</c> if [use check]; otherwise, <c>false</c>.</value>
		//public bool UseCheck { get; set; }
		///// <summary>
		///// Utilise le mode de reglement par virement bancaire
		///// </summary>
		///// <value><c>true</c> if [use wire transfer]; otherwise, <c>false</c>.</value>
		//public bool UseWireTransfer { get; set; }
		///// <summary>
		///// Utilise le mode de reglement Paypal
		///// </summary>
		///// <value><c>true</c> if [use paypal]; otherwise, <c>false</c>.</value>
		//public bool UsePaypal { get; set; }
		///// <summary>
		///// Utilise le mode de reglement Ogone
		///// </summary>
		///// <value><c>true</c> if [use ogone]; otherwise, <c>false</c>.</value>
		//public bool UseOgone { get; set; }
		///// <summary>
		///// Utilise le mode de reglement sogenactif
		///// </summary>
		///// <value><c>true</c> if [use sogenactif]; otherwise, <c>false</c>.</value>
		//public bool UseSogenactif { get; set; }
		///// <summary>
		///// Utilise le mode de reglement par compte interne
		///// </summary>
		///// <value><c>true</c> if [use account]; otherwise, <c>false</c>.</value>
		//public bool UseAccount { get; set; }

		/// <summary>
		/// Montant minimum de commande
		/// </summary>
		/// <value>The minimal order amount.</value>
		public decimal? MinimalOrderAmount { get; set; }

		/// <summary>
		/// Retourne la liste des payments autorisés
		/// pour le mode de reglement "en compte".
		/// </summary>
		/// <value>The allowed by account payment mode id list.</value>
		public IList<int> AllowedByAccountPaymentModeIdList { get; private set; }

		/// <summary>
		/// Desactivation de la DEEE dans le cas d'un export
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [disable recycle on export]; otherwise, <c>false</c>.
		/// </value>
		public bool DisableRecycleOnExport { get; set; }

		/// <summary>
		/// Taux de tva appliqué par defaut
		/// </summary>
		/// <value>The default tax rate.</value>
		public double DefaultTaxRate { get; set; }
	}
}
