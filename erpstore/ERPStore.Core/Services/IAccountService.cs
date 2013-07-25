using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ERPStore.Services
{
	/// <summary>
	/// Service des utilisateurs
	/// </summary>
	[ServiceContract(Name = "AccountService"
	, Namespace = "http://www.erpstore.net/2010/12/21")]
	public interface IAccountService
	{
		/// <summary>
		/// Retroune un utilisateur de type client via son id
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		Models.User GetUserById(int userId);
		/// <summary>
		/// Authentifie un identifiant / mot de passe, s'ils sont ok , retourne l'id de l'utilsateur
		/// </summary>
		/// <param name="login">The login.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
		int Authenticate(string login, string password);

		/// <summary>
		/// Retourne une société de type client via son Id
		/// </summary>
		/// <param name="corporateId">The corporate id.</param>
		/// <returns></returns>
		Models.Corporate GetCorporateById(int corporateId);
		/// <summary>
		/// Création d'un utilisateur
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		Models.User RegisterUser(Models.RegistrationUser user);

		/// <summary>
		/// Sets the confirmation by user.
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <returns></returns>
		Models.User SetConfirmationByUser(int userId);

		/// <summary>
		/// Changer le mot de passe d'un utilisateur
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="newpassword">The newpassword.</param>
		void SetPassword(Models.User user, string newpassword);

		/// <summary>
		/// Verifie les regles d'integrités pour la création d'une société
		/// </summary>
		/// <param name="corporate">The corporate.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		List<Models.BrokenRule> ValidateRegistrationCorporate(Models.RegistrationUser corporate, System.Web.HttpContextBase context);

		/// <summary>
		/// Verifie les regles d'intégrités pour la modification d'un utilisateur
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		List<Models.BrokenRule> ValidateUser(Models.User user, System.Web.HttpContextBase context);

		/// <summary>
		/// Verifie les regles d'integrités pour la modification d'une société
		/// </summary>
		/// <param name="corporate">The corporate.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		List<Models.BrokenRule> ValidateCorporate(Models.Corporate corporate, System.Web.HttpContextBase context);

		/// <summary>
		/// Sauvegarde des modification d'un utilisateur
		/// </summary>
		/// <param name="user">The user.</param>
		void SaveUser(Models.User user);

		/// <summary>
		/// Sauvegarde des modifications d'une société
		/// </summary>
		/// <param name="corporate">The corporate.</param>
		void SaveCorporate(Models.Corporate corporate);

		/// <summary>
		/// Creation d'une société à partir d'un particulier
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		void CreateCorporateFromUser(Models.User user);

		/// <summary>
		/// Verifie le mot de passe courant
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
		bool CheckPassword(Models.User user, string password);

		/// <summary>
		/// Renvoie un utilisateur en fonction de son email ou mot de passe
		/// </summary>
		/// <param name="emailOrLogin">The email or login.</param>
		/// <returns></returns>
		Models.User GetUserByEmailOrLogin(string emailOrLogin);

		/// <summary>
		/// Complete l'enregistrement d'un client, venant d'un devis
		/// </summary>
		/// <param name="Registration">The registration.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		Models.User CompleteRegistration(Models.RegistrationUser Registration, System.Web.HttpContextBase context);

		/// <summary>
		/// Retourne un utilisateur aléatoire pour les tests
		/// </summary>
		/// <returns></returns>
		Models.User GetRandomizedUserForTests();

		/// <summary>
		/// Retourne un vendeur via son code
		/// </summary>
		/// <param name="vendorCode">The vendor code.</param>
		/// <returns></returns>
		Models.Vendor GetVendorByCode(string vendorCode);

		/// <summary>
		/// Permet d'appliquer des modifications au modele d'inscription, après le mapping
		/// du formulaire société
		/// </summary>
		/// <param name="Registration">The registration.</param>
		/// <param name="context">The context.</param>
		void ProcessRegistrationCorporate(ERPStore.Models.RegistrationUser Registration, System.Web.HttpContextBase context);

		/// <summary>
		/// Permet d'appliquer des modifications au modele d'inscription, après le mapping
		/// du formulaire d'adresse de facturation
		/// </summary>
		/// <param name="Registration">The registration.</param>
		/// <param name="context">The context.</param>
		void ProcessRegistrationBillingAddress(ERPStore.Models.RegistrationUser Registration, System.Web.HttpContextBase context);

		/// <summary>
		/// Applique les dernières modification avec l'enregistrement
		/// </summary>
		/// <param name="account">The account.</param>
		/// <param name="registration">The registration.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		bool UpdateAccountPostRegistration(ERPStore.Models.User account, ERPStore.Models.RegistrationUser registration, System.Web.HttpContextBase context);

		/// <summary>
		/// Validation d'une adresse
		/// </summary>
		/// <param name="address">The address.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		List<Models.BrokenRule> ValidateUserAddress(ERPStore.Models.Address address, System.Web.HttpContextBase context);

		/// <summary>
		/// Validation de l'adresse de facturation lors de l'inscription
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		List<Models.BrokenRule> ValidateBillingAdressRegistrationUser(ERPStore.Models.RegistrationUser user, System.Web.HttpContextBase context);

		/// <summary>
		/// Retourne une société via sa clé d'API
		/// </summary>
		/// <param name="apiKey">The API key.</param>
		/// <returns></returns>
		Models.Corporate GetCorporateByApiKey(string apiKey);

		#region Registration User

		/// <summary>
		/// Permet d'appliquer des modifications au modele d'inscription, après le mapping
		/// du formulaire personne
		/// </summary>
		/// <param name="Registration">The registration.</param>
		/// <param name="context">The context.</param>
		void ProcessRegistrationUser(ERPStore.Models.RegistrationUser Registration, System.Web.HttpContextBase context);
		/// <summary>
		/// Verifie les regles d'intégrités pour la création d'un utilisateur
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		List<Models.BrokenRule> ValidateRegistrationUser(Models.RegistrationUser user, System.Web.HttpContextBase context);
		/// <summary>
		/// Retourne un objet de type inscription
		/// </summary>
		/// <returns></returns>
		[OperationContract]
		Models.RegistrationUser CreateRegistrationUser();
		/// <summary>
		/// Sauvegarde une inscription
		/// </summary>
		/// <param name="visitorId">The visitor id.</param>
		/// <param name="registration">The registration.</param>
		[OperationContract]
		void SaveRegistrationUser(string visitorId, ERPStore.Models.RegistrationUser registration);
		/// <summary>
		/// Retourne une inscription en fonction du user en cours
		/// </summary>
		/// <param name="visitorId">The visitor id.</param>
		/// <returns></returns>
		[OperationContract]
		Models.RegistrationUser GetRegistrationUser(string visitorId);

		/// <summary>
		/// Creation d'un user à partir d'une inscription
		/// </summary>
		/// <param name="registration">The registration.</param>
		/// <returns></returns>
		ERPStore.Models.User CreateUserFromRegistration(ERPStore.Models.RegistrationUser registration);

		/// <summary>
		/// Fermeture de la procedure d'inscription
		/// </summary>
		/// <param name="visitorId">The visitor id.</param>
		/// <param name="userId">The user id.</param>
		[OperationContract]
		void CloseRegistrationUser(string visitorId, int userId);

		#endregion

		/// <summary>
		/// Enregistrement d'un message d'un contact
		/// </summary>
		/// <param name="contactInfo">The contact info.</param>
		void SaveContactMessage(ERPStore.Models.ContactInfo contactInfo);

		/// <summary>
		/// Retourne une liste de user via un identifiant de selection
		/// </summary>
		/// <param name="selectionId">The selection id.</param>
		/// <returns></returns>
		IList<Models.User> GetUserListBySelectionId(int selectionId);

		/// <summary>
		/// Retourne une liste de selection de user
		/// </summary>
		/// <returns></returns>
		Dictionary<int, string> GetUserSelectionList();
	}
}
