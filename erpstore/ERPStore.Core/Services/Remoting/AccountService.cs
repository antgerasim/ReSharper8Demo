using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

using Microsoft.Practices.Unity;
using System.Web.Mvc;

namespace ERPStore.Services.Remoting
{
	public class AccountService : IAccountService
	{
		public AccountService()
		{
            this.ConcreteAccountService = DependencyResolver.Current.GetService<IAccountService>();
		}

		protected IAccountService ConcreteAccountService { get; private set; }

		private void AuthenticateRequest()
		{
			string apiKey = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("apiKey", "ns");
			bool authenticated = apiKey == ERPStoreApplication.WebSiteSettings.ApiToken;
			if (!authenticated)
			{
				throw new System.Security.SecurityException("this request is not authenticated");
			}
		}

		#region IAccountService Members

		public ERPStore.Models.User GetUserById(int userId)
		{
			throw new NotImplementedException();
		}

		public int Authenticate(string login, string password)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.Corporate GetCorporateById(int corporateId)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.User RegisterUser(ERPStore.Models.RegistrationUser user)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.User SetConfirmationByUser(int userId)
		{
			throw new NotImplementedException();
		}

		public void SetPassword(ERPStore.Models.User user, string newpassword)
		{
			throw new NotImplementedException();
		}

		public List<ERPStore.Models.BrokenRule> ValidateRegistrationCorporate(ERPStore.Models.RegistrationUser corporate, System.Web.HttpContextBase context)
		{
			throw new NotImplementedException();
		}

		public List<ERPStore.Models.BrokenRule> ValidateUser(ERPStore.Models.User user, System.Web.HttpContextBase context)
		{
			throw new NotImplementedException();
		}

		public List<ERPStore.Models.BrokenRule> ValidateCorporate(ERPStore.Models.Corporate corporate, System.Web.HttpContextBase context)
		{
			throw new NotImplementedException();
		}

		public void SaveUser(ERPStore.Models.User user)
		{
			throw new NotImplementedException();
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

		public ERPStore.Models.User GetUserByEmailOrLogin(string emailOrLogin)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.User CompleteRegistration(ERPStore.Models.RegistrationUser Registration, System.Web.HttpContextBase context)
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

		public void ProcessRegistrationCorporate(ERPStore.Models.RegistrationUser Registration, System.Web.HttpContextBase context)
		{
			throw new NotImplementedException();
		}

		public void ProcessRegistrationBillingAddress(ERPStore.Models.RegistrationUser Registration, System.Web.HttpContextBase context)
		{
			throw new NotImplementedException();
		}

		public bool UpdateAccountPostRegistration(ERPStore.Models.User account, ERPStore.Models.RegistrationUser registration, System.Web.HttpContextBase context)
		{
			throw new NotImplementedException();
		}

		public List<ERPStore.Models.BrokenRule> ValidateUserAddress(ERPStore.Models.Address address, System.Web.HttpContextBase context)
		{
			throw new NotImplementedException();
		}

		public List<ERPStore.Models.BrokenRule> ValidateBillingAdressRegistrationUser(ERPStore.Models.RegistrationUser user, System.Web.HttpContextBase context)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.Corporate GetCorporateByApiKey(string apiKey)
		{
			throw new NotImplementedException();
		}

		public void ProcessRegistrationUser(ERPStore.Models.RegistrationUser Registration, System.Web.HttpContextBase context)
		{
			throw new NotImplementedException();
		}

		public List<ERPStore.Models.BrokenRule> ValidateRegistrationUser(ERPStore.Models.RegistrationUser user, System.Web.HttpContextBase context)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.RegistrationUser CreateRegistrationUser()
		{
			AuthenticateRequest();
			return ConcreteAccountService.CreateRegistrationUser();
		}

		public void SaveRegistrationUser(string visitorId, ERPStore.Models.RegistrationUser registration)
		{
			AuthenticateRequest();
			ConcreteAccountService.SaveRegistrationUser(visitorId, registration);
		}

		public ERPStore.Models.RegistrationUser GetRegistrationUser(string visitorId)
		{
			AuthenticateRequest();
            var result = ConcreteAccountService.GetRegistrationUser(visitorId);
            return result;
		}

		public ERPStore.Models.User CreateUserFromRegistration(ERPStore.Models.RegistrationUser registration)
		{
			throw new NotImplementedException();
		}

		public void CloseRegistrationUser(string visitorId, int userId)
		{
			throw new NotImplementedException();
		}

		public void SaveContactMessage(ERPStore.Models.ContactInfo contactInfo)
		{
			throw new NotImplementedException();
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
