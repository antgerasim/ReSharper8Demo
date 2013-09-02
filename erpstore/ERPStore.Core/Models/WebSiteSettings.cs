using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Informations de configuration du site web
	/// </summary>
	[Serializable]
	public class WebSiteSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="WebSiteSettings"/> class.
		/// </summary>
		public WebSiteSettings()
		{
			Texts = new TextSettings();
			Contact = new ContactSettings();
			Payment = new PaymentSettings();
			Menus = new List<MenuItem>();
			Shipping = new ShippingSettings();
			Catalog = new CatalogSettings();
			UseActionCache = true;
			UseFullTextIndex = false;
			AllowGenerateSitemaps = true;
            ApiToken = Guid.NewGuid().ToString();
			Country = Models.Country.Default;
			EnableGZip = false;
			AllowMobileViews = false;
			ForceMobileViews = false;
			SendAccountRegistrationConfirmation = true;
			SignInWhenRegistered = true;
		}

		#region Global Settings

		/// <summary>
		/// Nom du site web
		/// </summary>
		/// <value>The name of the site.</value>
		public string SiteName { get; set; }
		/// <summary>
		/// Titre du site pour la home page
		/// </summary>
		/// <value>The site title.</value>
		public string SiteTitle { get; set; }
		/// <summary>
		/// Sloggan du site web
		/// </summary>
		/// <value>The sloggan.</value>
		public string Sloggan { get; set; }
		/// <summary>
		/// Balise meta de description sur la page d'accueil du site
		/// </summary>
		/// <value>The home meta description.</value>
		public string HomeMetaDescription { get; set; }
		/// <summary>
		/// Balise meta des mots clés sur la page d'accueil du site
		/// </summary>
		/// <value>The home meta keywords.</value>
		public string HomeMetaKeywords { get; set; }
		/// <summary>
		/// Les autres metas informations dans la page d'accueil
		/// </summary>
		/// <value>The home others metas.</value>
		public string HomeOthersMetas { get; set; }
		/// <summary>
		/// Indique l'url en cours
		/// </summary>
		/// <value>The URL.</value>
		public string CurrentUrl { get; set; }
		/// <summary>
		/// Image du logo
		/// </summary>
		/// <value>The name of the logo image file.</value>
		public string LogoImageFileName { get; set; }

		/// <summary>
		/// Pays du site.
		/// </summary>
		/// <value>The country.</value>
		public Models.Country Country { get; set; }

		/// <summary>
		/// Textes des conditions générales de vente et textes legaux
		/// </summary>
		/// <value>The texts.</value>
		public TextSettings Texts { get; private set; }

		/// <summary>
		/// Configuration des informations de contact
		/// </summary>
		/// <value>The contact.</value>
		public ContactSettings Contact { get; private set; }

		/// <summary>
		/// Configuration des modes de reglement
		/// </summary>
		/// <value>The payment.</value>
		public PaymentSettings Payment { get; private set; }

		/// <summary>
		/// Emplacement en bas de page pour le tracking web
		/// </summary>
		/// <value>Script js de tracking.</value>
		public string TrackingScripts { get; set; }

		/// <summary>
		/// Liste des items du menu
		/// </summary>
		/// <value>The menus.</value>
		public List<MenuItem> Menus { get; set; }

		/// <summary>
		/// Paramètres de livraison
		/// </summary>
		/// <value>The shipping.</value>
		public ShippingSettings Shipping { get; private set; }

		/// <summary>
		/// Paramètres du catalogue
		/// </summary>
		/// <value>The catalog.</value>
		public CatalogSettings Catalog { get; private set; }

        /// <summary>
        /// Clé de l'api d'administration du site a partir 
        /// d'ERP360
        /// </summary>
        /// <value>The API token.</value>
        public string ApiToken { get; set; }

		/// <summary>
		/// Active la compression GZip sur les pages
		/// </summary>
		/// <value><c>true</c> if [enable G zip]; otherwise, <c>false</c>.</value>
		public bool EnableGZip { get; set; }

		/// <summary>
		/// Permet un affichage dédié pour les mobiles
		/// pour activer ce mode il faut ajouter la clé suivante dans appSettings :
		/// <![CDATA[
		/// <add key="allowMobileViews" value="true"/>
		/// ]]>
		/// </summary>
		/// <value><c>true</c> if [allow mobile views]; otherwise, <c>false</c>.</value>
		public bool AllowMobileViews { get; set; }

		/// <summary>
		/// Force la vue mobile (utilisé uniquement pour le developpement)
		/// pour activer ce mode il faut ajouter la clé suivante dans appSettings :
		/// <![CDATA[
		/// <add key="forceMobileViews" value="true"/>
		/// ]]>
		/// </summary>
		/// <value><c>true</c> if [force mobile views]; otherwise, <c>false</c>.</value>
		public bool ForceMobileViews { get; set; }

		/// <summary>
		/// Force l'utilisation du catalogue fulltext
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [force full text catalog]; otherwise, <c>false</c>.
		/// </value>
		public bool ForceFullTextCatalog { get; set; }

		/// <summary>
		/// Indique si un utilisateur doit etre loggé suite à une inscription
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [sign in when registered]; otherwise, <c>false</c>.
		/// </value>
		public bool SignInWhenRegistered { get; set; }

		/// <summary>
		/// Envoie un mail de confirmation de création de compte lors 
		/// de l'inscription
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [send account registration confirmation]; otherwise, <c>false</c>.
		/// </value>
		public bool SendAccountRegistrationConfirmation { get; set; }

		/// <summary>
		/// Emplacement physique du site
		/// </summary>
		/// <value>The path.</value>
		public string PhysicalPath { get; set; }

		#endregion

		#region Internal

		/// <summary>
		/// Repertoire temporaire 
		/// </summary>
		/// <value>The temp path.</value>
		public string TempPath { get; set; }

		/// <summary>
		/// Repertoire des documents fichier
		/// </summary>
		/// <value>The document path.</value>
		public string DocumentPath { get; set; }

		public byte[] CryptoKey { get; set; }

		public byte[] CryptoIV { get; set; }

		public bool UseActionCache { get; set; }

		public bool UseFullTextIndex { get; set; }

		public string FullTextIndexPath { get; set; }

		public bool AllowGenerateSitemaps { get; set; }

		public string ExtranetUrl { get; set; }

		public string DefaultUrl { get; set; }

		#endregion
	}
}