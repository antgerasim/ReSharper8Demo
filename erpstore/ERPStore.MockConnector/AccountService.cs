using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace ERPStore.MockConnector
{
	public class AccountService : Services.IAccountService
	{
		private List<ERPStore.Models.User> m_List;
		private Dictionary<int, string> m_PasswordList;

		public AccountService()
		{
			m_List = new List<ERPStore.Models.User>();
			m_PasswordList = new Dictionary<int, string>();

			var user = new Models.User()
			{
				Code = "Test",
				Corporate = null,
				CreationDate = DateTime.Now,
				DefaultAddress = AddressesDatas().First(),
				Email = "test@erpstore.net",
				EmailVerificationStatus = ERPStore.Models.EmailVerificationStatus.UserConfirmed,
				FaxNumber = "0102030405",
				FirstName = "Test",
				Id = 1,
				IsResetPasswordRequiered = false,
				LastDeliveredAddress = AddressesDatas().ElementAt(2),
				LastName = "Test",
				Login = "login",
				PhoneNumber = "0102030405",
				Presentation = ERPStore.Models.UserPresentation.Mister,
				Roles = { "customer" },
				MobileNumber = "0605040302",
			};

			user.DeliveryAddressList.AddRange(AddressesDatas());

			m_List.Add(user);

			m_PasswordList.Add(1, "pass");
		}

		[Dependency]
		public ERPStore.Repositories.IRegistrationRepository RegistrationRepository { get; set; }

		#region IAccountService Members

		public ERPStore.Models.User GetUserById(int userId)
		{
			return m_List.SingleOrDefault(i => i.Id == userId);
		}

		public int Authenticate(string login, string password)
		{
			var user = m_List.SingleOrDefault(i => i.Login.Equals(login, StringComparison.InvariantCultureIgnoreCase));
			if (user == null)
			{
				throw new System.Security.SecurityException("login does not exists"); 
			}
			var p = m_PasswordList.SingleOrDefault(i => i.Key == user.Id);
			if (p.Value != password)
			{
				throw new System.Security.SecurityException("bad password");
			}
			return user.Id;
		}

		public ERPStore.Models.Corporate GetCorporateById(int corporateId)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.User RegisterUser(ERPStore.Models.RegistrationUser user)
		{
			var result = new ERPStore.Models.User();
			var billingAddress = new ERPStore.Models.Address();
			billingAddress.City = user.BillingAddressCity;
			billingAddress.CorporateName = user.CorporateName;
			billingAddress.CountryId = user.BillingAddressCountryId;
			billingAddress.RecipientName = user.FullName;
			billingAddress.Region = user.BillingAddressRegion;
			billingAddress.Street = user.BillingAddressStreet;
			billingAddress.ZipCode = user.BillingAddressZipCode;

			result.Code = user.VisitorId;
			if (!user.CorporateName.IsNullOrTrimmedEmpty())
			{
				result.Corporate = new ERPStore.Models.Corporate();
				result.Corporate.Code = user.VisitorId;
				result.Corporate.CreationDate = DateTime.Now;
				result.Corporate.DefaultAddress = billingAddress;
				result.Corporate.Email = user.CorporateEmail;
				result.Corporate.FaxNumber = user.CorporateFaxNumber;
				result.Corporate.NafCode = user.NAFCode;
				result.Corporate.Name = user.CorporateName;
				result.Corporate.PhoneNumber = user.CorporatePhoneNumber;
				result.Corporate.RcsNumber = user.RcsNumber;
				result.Corporate.SiretNumber = user.SiretNumber;
				result.Corporate.SocialStatus = user.CorporateSocialStatus;
				result.Corporate.VatMandatory = user.VatMandatory;
				result.Corporate.VatNumber = user.TVANumber;
				result.Corporate.WebSite = user.CorporateWebSite;
			}

			result.CreationDate = DateTime.Now;
			result.DefaultAddress = billingAddress;
			result.Email = user.Email;
			result.FaxNumber = user.FaxNumber;
			result.FirstName = user.FirstName;
			result.LastName = user.LastName;
			result.Login = user.Email;
			result.MobileNumber = user.MobileNumber;
			result.PhoneNumber = user.PhoneNumber;
			result.Presentation = (ERPStore.Models.UserPresentation)Enum.Parse(typeof(ERPStore.Models.UserPresentation), user.PresentationId.ToString());
			result.State = ERPStore.Models.UserState.Completed;

			m_List.Add(result);
			result.Id = m_List.IndexOf(result) + 1;
			m_PasswordList.Add(result.Id, user.Password);

			return result;
		}

		public void SetPassword(ERPStore.Models.User user, string newpassword)
		{
			if (m_PasswordList.ContainsKey(user.Id))
			{
				m_PasswordList[user.Id] = newpassword;
			}
			else
			{
				m_PasswordList.Add(user.Id, newpassword);
			}
		}

		public void SaveAddress(ERPStore.Models.User user, int addressId)
		{
			throw new NotImplementedException();
		}

		public void SaveAddress(ERPStore.Models.User user, ERPStore.Models.Address address, bool isDeliveryAddress)
		{
			throw new NotImplementedException();
		}

		public void RecoverPassword(string email)
		{
			throw new NotImplementedException();
		}

		private IEnumerable<Models.Address> AddressesDatas()
		{
			var result = new List<Models.Address>();
			for (int i = 0; i < 4; i++)
			{
				var address = new ERPStore.Models.Address()
						{
							City = string.Format("City{0}", i),
							CountryId = Models.Country.Default.Id,
							Id = i,
							RecipientName = string.Format("M Test {0}", i),
							Region = string.Empty,
							Street = string.Format("Street{0}", i),
							ZipCode = string.Format("010{0:00}", i),
						};

				result.Add(address);
			}
			return result;
		}

		public List<ERPStore.Models.BrokenRule> ValidateRegistrationUser(ERPStore.Models.RegistrationUser user)
		{
			throw new NotImplementedException();
		}

		public List<ERPStore.Models.BrokenRule> ValidateRegistrationCorporate(ERPStore.Models.RegistrationUser corporate)
		{
			throw new NotImplementedException();
		}

		public List<ERPStore.Models.BrokenRule> ValidateUser(ERPStore.Models.User user)
		{
			throw new NotImplementedException();
		}

		public List<ERPStore.Models.BrokenRule> ValidateCorporate(ERPStore.Models.Corporate corporate)
		{
			throw new NotImplementedException();
		}

		public void SaveUser(ERPStore.Models.User user)
		{
			if (m_List.Any(i => i.Id == user.Id))
			{
				return;
			}

			m_List.Add(user);
		}

		public void SaveCorporate(ERPStore.Models.Corporate corporate)
		{
			throw new NotImplementedException();
		}

		public void CreateCorporateFromUser(ERPStore.Models.User user)
		{
			throw new NotImplementedException();
		}

		public bool CheckPassword(ERPStore.Models.User user, string password)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.User SetConfirmationByUser(int userId)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.User GetUserByEmailOrLogin(string emailOrLogin)
		{
			var result = m_List.Where(i => i.Login.Equals(emailOrLogin, StringComparison.InvariantCultureIgnoreCase));
			if (result.Count() == 1)
			{
				return result.First();
			}
			result = m_List.Where(i => i.Email.Equals(emailOrLogin, StringComparison.InvariantCultureIgnoreCase));
			if (result.Count() == 1)
			{
				return result.First();
			}
			return null;
		}

        public ERPStore.Models.User CompleteRegistration(ERPStore.Models.RegistrationUser webUser)
        {
            throw new NotImplementedException();
        }

		public ERPStore.Models.User GetRandomizedUserForTests()
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.Vendor GetVendorByCode(string vendorCode)
		{
			throw new NotImplementedException();
		}

		public void ProcessRegistrationUser(ERPStore.Models.RegistrationUser Registration)
		{
			throw new NotImplementedException();
		}

		public void ProcessRegistrationCorporate(ERPStore.Models.RegistrationUser Registration)
		{
			throw new NotImplementedException();
		}

		public void ProcessRegistrationBillingAddress(ERPStore.Models.RegistrationUser Registration)
		{
			throw new NotImplementedException();
		}

		public bool UpdateAccountPostRegistration(ERPStore.Models.User account, ERPStore.Models.RegistrationUser Registration)
		{
			throw new NotImplementedException();
		}

		public List<ERPStore.Models.BrokenRule> ValidateRegistrationUser(ERPStore.Models.RegistrationUser user, System.Web.HttpContextBase context)
		{
			var result = new List<ERPStore.Models.BrokenRule>();
			if (user.LastName.IsNullOrTrimmedEmpty())
			{
				result.AddBrokenRule("LastName", "Le nom doit etre indiqué");
			}

			if (user.Password.IsNullOrTrimmedEmpty())
			{
				result.AddBrokenRule("Password", "Vous devez indiquer un mot de passe");
			}

			if (!ERPStore.Services.EmailValidator.IsValidEmail(user.Email))
			{
				result.AddBrokenRule("Email", "L'Email est invalide");
			}

			if (!user.Password.IsNullOrTrimmedEmpty() && user.Password.Length < 6)
			{
				result.AddBrokenRule("Password", "Le mot de passe doit contenir au moins 6 caractères");
			}
			return result;
		}

		public List<ERPStore.Models.BrokenRule> ValidateRegistrationCorporate(ERPStore.Models.RegistrationUser corporate, System.Web.HttpContextBase context)
		{
			var result = new List<ERPStore.Models.BrokenRule>();
			if (corporate.CorporateName.IsNullOrTrimmedEmpty())
			{
				result.AddBrokenRule("CorporateName", "Le nom de la société doit etre indiqué");
			}

			if (!ERPStore.Services.PhoneNumberValidator.IsValidPhoneNumber(corporate.CorporatePhoneNumber))
			{
				result.AddBrokenRule("CorporatePhoneNumber", "Le numéro de téléphone est invalide ou n'est pas indiqué");
			}

			if (!ERPStore.Services.EmailValidator.IsValidEmail(corporate.CorporateEmail))
			{
				result.AddBrokenRule("CorporateEmail", "L'Email n'est pas valide");
			}

			if (corporate.CorporateSocialStatus.IsNullOrTrimmedEmpty())
			{
				result.AddBrokenRule("CorporateSocialStatus", "Vous devez indiquer le statut social de la société");
			}

			if (corporate.NAFCode.IsNullOrTrimmedEmpty())
			{
				result.AddBrokenRule("NAFCode", "Vous devez indiquer le code NAF de la société");
			}

			if (corporate.SiretNumber.IsNullOrTrimmedEmpty())
			{
				result.AddBrokenRule("SiretNumber", "Vous devez indiquer le numéro Siret de la société");
			}

			if (corporate.TVANumber.IsNullOrTrimmedEmpty())
			{
				result.AddBrokenRule("TVANumber", "Vous devez indiquer le numéro de TVA de la société");
			}
			return result;
		}

		public List<ERPStore.Models.BrokenRule> ValidateUser(ERPStore.Models.User user, System.Web.HttpContextBase context)
		{
			throw new NotImplementedException();
		}

		public List<ERPStore.Models.BrokenRule> ValidateCorporate(ERPStore.Models.Corporate corporate, System.Web.HttpContextBase context)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.User CompleteRegistration(ERPStore.Models.RegistrationUser Registration, System.Web.HttpContextBase context)
		{
			throw new NotImplementedException();
		}

		public void ProcessRegistrationUser(ERPStore.Models.RegistrationUser Registration, System.Web.HttpContextBase context)
		{
		}

		public void ProcessRegistrationCorporate(ERPStore.Models.RegistrationUser Registration, System.Web.HttpContextBase context)
		{
		}

		public void ProcessRegistrationBillingAddress(ERPStore.Models.RegistrationUser Registration, System.Web.HttpContextBase context)
		{
		}

		public bool UpdateAccountPostRegistration(ERPStore.Models.User account, ERPStore.Models.RegistrationUser registration, System.Web.HttpContextBase context)
		{
			// Ne rien faire
			return false;
		}

		public List<ERPStore.Models.BrokenRule> ValidateUserAddress(ERPStore.Models.Address address, System.Web.HttpContextBase context)
		{
			var result = new List<ERPStore.Models.BrokenRule>();
			if (address.RecipientName.IsNullOrTrimmedEmpty())
			{
				result.AddBrokenRule("RecipientName", "Le nom du destinataire doit etre indiqué");
			}

			if (address.ZipCode.IsNullOrTrimmedEmpty())
			{
				result.AddBrokenRule("ZipCode", "Le code postal doit etre indiqué");
			}

			if (address.Street.IsNullOrTrimmedEmpty())
			{
				result.AddBrokenRule("Street", "Le rue doit etre indiquée");
			}

			if (address.City.IsNullOrTrimmedEmpty())
			{
				result.AddBrokenRule("City", "La ville doit etre indiquée");
			}

			return result;
		}

		public List<ERPStore.Models.BrokenRule> ValidateBillingAdressRegistrationUser(ERPStore.Models.RegistrationUser user, System.Web.HttpContextBase context)
		{
			var result = new List<ERPStore.Models.BrokenRule>();
			if (user.BillingAddressRecipientName.IsNullOrTrimmedEmpty())
			{
				result.AddBrokenRule("BillingAddressRecipientName", "Le nom du destinataire doit etre indiqué");
			}

			if (user.BillingAddressZipCode.IsNullOrTrimmedEmpty())
			{
				result.AddBrokenRule("BillingAddressZipCode", "Le code postal doit etre indiqué");
			}

			if (user.BillingAddressStreet.IsNullOrTrimmedEmpty())
			{
				result.AddBrokenRule("BillingAddressStreet", "Le rue doit etre indiquée");
			}

			if (user.BillingAddressCity.IsNullOrTrimmedEmpty())
			{
				result.AddBrokenRule("BillingAddressCity", "La ville doit etre indiquée");
			}
			return result;
		}

		public ERPStore.Models.Corporate GetCorporateByApiKey(string apiKey)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.RegistrationUser CreateRegistrationUser()
		{
			return new ERPStore.Models.RegistrationUser();
		}

		public void SaveRegistrationUser(string visitorId, ERPStore.Models.RegistrationUser registration)
		{
			RegistrationRepository.SaveRegistrationUser(visitorId, registration);
		}

		public ERPStore.Models.RegistrationUser GetRegistrationUser(string visitorId)
		{
			return RegistrationRepository.GetRegistrationUser(visitorId);
		}

		public ERPStore.Models.User CreateUserFromRegistration(ERPStore.Models.RegistrationUser registration)
		{
			var user = new ERPStore.Models.User();

			user.CreationDate = DateTime.Now;

			user.Email = registration.Email;
			user.EmailRegistrationSent = false;
			user.FaxNumber = registration.FaxNumber;
			user.FirstName = registration.FirstName;
			user.LastName = registration.LastName;
			user.MobileNumber = registration.MobileNumber;
			user.PhoneNumber = registration.PhoneNumber;
			user.Presentation = (ERPStore.Models.UserPresentation)Enum.Parse(typeof(ERPStore.Models.UserPresentation), registration.PresentationId.ToString());
			user.State = ERPStore.Models.UserState.Uncompleted;

			// Dernière adresse de livraison
			user.LastDeliveredAddress = new ERPStore.Models.Address();
			user.LastDeliveredAddress.City = registration.ShippingAddressCity;
			user.LastDeliveredAddress.CorporateName = registration.CorporateName;
			user.LastDeliveredAddress.CountryId = registration.ShippingAddressCountryId;
			user.LastDeliveredAddress.RecipientName = registration.ShippingAddressRecipientName;
			user.LastDeliveredAddress.Region = registration.ShippingAddressRegion;
			user.LastDeliveredAddress.Street = registration.ShippingAddressStreet;
			user.LastDeliveredAddress.ZipCode = registration.ShippingAddressZipCode;

			user.DeliveryAddressList.Add(user.LastDeliveredAddress);

			// Adresse de facturation
			if (!registration.IsSameBillingAddress)
			{
				user.DefaultAddress = new ERPStore.Models.Address();
				user.DefaultAddress.City = registration.BillingAddressCity;
				user.DefaultAddress.CountryId = registration.BillingAddressCountryId;
				user.DefaultAddress.RecipientName = registration.BillingAddressRecipientName;
				user.DefaultAddress.Street = registration.BillingAddressStreet;
				user.DefaultAddress.ZipCode = registration.BillingAddressZipCode;
				user.DefaultAddress.Region = registration.BillingAddressRegion;
			}
			else
			{
				user.DefaultAddress = user.LastDeliveredAddress;
			}

			if (registration.CorporateName != null)
			{
				user.Corporate = new ERPStore.Models.Corporate();
				user.Corporate.Name = registration.CorporateName;
				user.Corporate.SiretNumber = registration.SiretNumber;
				user.Corporate.FaxNumber = registration.CorporateFaxNumber;
				user.Corporate.CreationDate = DateTime.Now;
				user.Corporate.Email = registration.Email;
				user.Corporate.NafCode = registration.NAFCode;
				user.Corporate.PhoneNumber = registration.CorporatePhoneNumber;
				user.Corporate.RcsNumber = registration.RcsNumber;
				user.Corporate.SiretNumber = registration.SiretNumber;
				user.Corporate.SocialStatus = registration.CorporateSocialStatus;
				user.Corporate.VatMandatory = registration.VatMandatory;
				user.Corporate.VatNumber = registration.TVANumber;
				user.Corporate.WebSite = registration.CorporateWebSite;
				user.Corporate.DefaultAddress = user.DefaultAddress;
			}

			return user;

		}

		public void CloseRegistrationUser(string visitorId, int userId)
		{
			RegistrationRepository.CloseRegistrationUser(visitorId, userId);
		}

		public void SaveContactMessage(ERPStore.Models.ContactInfo contactInfo)
		{
			// TODO : Stocker le message
		}

		public IList<ERPStore.Models.User> GetUserListBySelectionId(int selectionId)
		{
			throw new NotImplementedException();
		}

		public Dictionary<int, string> GetUserSelectionList()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
