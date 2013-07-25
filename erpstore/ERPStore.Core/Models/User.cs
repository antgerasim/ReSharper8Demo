using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Utilisateur d'ERPStore
	/// </summary>
	[Serializable]
	public class User : IEmailRecipient
	{
		public User()
		{
            State = UserState.Uncompleted;
            Presentation = UserPresentation.Mister;
            CreationDate = DateTime.UtcNow;
            Roles = new List<string>();
            DeliveryAddressList = new List<Address>();
			IsMaster = false;
		}

		/// <summary>
		/// Identifiant interne
		/// </summary>
		/// <value>The id.</value>
		public int Id { get; set; }
		/// <summary>
		/// Titre de la personne
		/// </summary>
		/// <value>The presentation.</value>
		public UserPresentation Presentation { get; set; }
		/// <summary>
		/// Prénom
		/// </summary>
		/// <value>The first name.</value>
		public string FirstName { get; set; }
		/// <summary>
		/// Nom
		/// </summary>
		/// <value>The last name.</value>
		public string LastName { get; set; }
		/// <summary>
		/// Nom complet
		/// </summary>
		/// <value>The full name.</value>
		public string FullName { get; set; }
		/// <summary>
		/// N° de téléphone
		/// </summary>
		/// <value>The phone number.</value>
		public string PhoneNumber { get; set; }
		/// <summary>
		/// N° de fax
		/// </summary>
		/// <value>The fax number.</value>
		public string FaxNumber { get; set; }
		/// <summary>
		/// N° de mobile
		/// </summary>
		/// <value>The mobile number.</value>
		public string MobileNumber { get; set; }
		/// <summary>
		/// Identifiant
		/// </summary>
		/// <value>The login.</value>
		public string Login { get; set; }
		/// <summary>
		/// Email
		/// </summary>
		/// <value>The email.</value>
		public string Email { get; set; }
		/// <summary>
		/// Date de création
		/// </summary>
		/// <value>The creation date.</value>
		public DateTime CreationDate { get; set; }
		/// <summary>
		/// Code unique de la personne
		/// </summary>
		/// <value>The code.</value>
		public string Code { get; set; }
		/// <summary>
		/// Identifiant OpenId
		/// </summary>
		/// <value>The open id key.</value>
		public string OpenIdKey { get; set; }
		/// <summary>
		/// Necessite un changement de mot de passe à la prochaine connexion
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is reset password requiered; otherwise, <c>false</c>.
		/// </value>
		public bool IsResetPasswordRequiered { get; set; }
		/// <summary>
		/// Statut de verification de l'Email
		/// </summary>
		/// <value>The email verification status.</value>
		public EmailVerificationStatus EmailVerificationStatus { get; set; }

		/// <summary>
		/// Roles de la personne (sécurité)
		/// </summary>
		/// <value>The roles.</value>
		public List<string> Roles { get; private set; }
		/// <summary>
		/// Entreprise eventuelle pour laquelle travaille la personne
		/// </summary>
		/// <value>The corporate.</value>
		public Corporate Corporate { get; set; }

		/// <summary>
		/// Nom de la société si la personne est un contact
		/// </summary>
		/// <value>The name of the corporate.</value>
		public string CorporateName 
		{
			get
			{
				if (Corporate != null)
				{
					return Corporate.Name;
				}
				return null;
			}
		}
		/// <summary>
		/// Adresse par defaut
		/// </summary>
		/// <value>The default address.</value>
		public Address DefaultAddress { get; set; }
		/// <summary>
		/// Dernière adresse de livraison utilisée
		/// </summary>
		/// <value>The last delivered address.</value>
		public Address LastDeliveredAddress { get; set; }

		/// <summary>
		/// Liste de toutes les adresses de livraison
		/// </summary>
		/// <value>The delivery address list.</value>
		public List<Address> DeliveryAddressList { get ; private set; }

        /// <summary>
        /// Dernière date de connexion au site web
        /// </summary>
        /// <value>The last login date.</value>
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// Etat du profil 
        /// </summary>
        /// <value>The state.</value>
        public UserState State { get; set; }

		/// <summary>
		/// Indique que l'utilisateur peut voir tous les documents
		/// </summary>
		/// <value><c>true</c> if this instance is admin; otherwise, <c>false</c>.</value>
		public bool IsMaster { get; set; }

		/// <summary>
		/// Identifiant de la catégorie de client
		/// </summary>
		/// <value>The category id.</value>
		public int? CategoryId { get; set; }

		/// <summary>
		/// Le mail d'inscription est envoyé
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [email registration sent]; otherwise, <c>false</c>.
		/// </value>
		public bool EmailRegistrationSent { get; set; }

		/// <summary>
		/// Vendeur associé 
		/// </summary>
		/// <value>The vendor.</value>
		public Vendor Vendor { get; set; }
	}
}
