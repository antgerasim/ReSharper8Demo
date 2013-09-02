using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using NUnit.Framework;

using Microsoft.Practices.Unity;

namespace ERPStore.Tests.Controllers
{
	[TestFixture]
	public class RegistrationControllerTests : TestBase
	{
		[SetUp]
		public override void Initialize()
		{
			base.Initialize();
			m_FolderView = "~/views/account/{0}";
		}

		[Test]
		public void Register_User_With_Corporate()
		{
			// Verification du post
			Assert.AreEqual(true, IsOnlyPostAllowed<ERPStore.Controllers.RegistrationController>(m => m.Register(null, null, null)));

			var controller = CreateController<ERPStore.Controllers.RegistrationController>();

			var result = controller.Register() as System.Web.Mvc.ViewResult;
			Assert.AreEqual(result.ViewName, GetViewName("register.aspx"));
			var registrationUser = result.ViewData.Model as ERPStore.Models.RegistrationUser;

			registrationUser.PresentationId = (int)ERPStore.Models.UserPresentation.Mister;
			registrationUser.FirstName = "FirstName";
			registrationUser.LastName = "LastName";
			registrationUser.MobileNumber = "0605040203";
			registrationUser.Password = "123456";
			registrationUser.PhoneNumber = "0102030405";
			registrationUser.CorporateName = "CORPORATE";
			registrationUser.FaxNumber = "0102030405";
			registrationUser.Email = "test3@test.com";
			registrationUser.BillingAddressCountryId = ERPStore.Models.Country.Default.Id;

			var redirect = controller.Register(registrationUser, registrationUser.Email, registrationUser.Password) as System.Web.Mvc.RedirectToRouteResult;
			Assert.AreEqual(redirect.RouteName, ERPStore.ERPStoreRoutes.REGISTER_ACCOUNT_CORPORATE);

			var corporateResult = controller.RegisterCorporate() as System.Web.Mvc.ViewResult;
			registrationUser = corporateResult.ViewData.Model as ERPStore.Models.RegistrationUser;
			Assert.IsNotNull(registrationUser);
			Assert.AreEqual(corporateResult.ViewName, GetViewName("registercorporate.aspx"));

			registrationUser.CorporateEmail = "corporate@email.com";
			registrationUser.CorporateFaxNumber = "0102030405";
			registrationUser.CorporateName = "CORPORATE";
			registrationUser.CorporatePhoneNumber = "0101010101";
			registrationUser.CorporateSocialStatus = "Social Status";
			registrationUser.CorporateWebSite = "http://website.com";
			registrationUser.NAFCode = "nafcode";
			registrationUser.RcsNumber = "RCSNumber";
			registrationUser.VatMandatory = false;
			registrationUser.VATNumber = "FR1234";
			registrationUser.SiretNumber = "siretnumber";

			var corporatePostResult = controller.RegisterCorporate(registrationUser) as System.Web.Mvc.RedirectToRouteResult;

			Assert.AreEqual(corporatePostResult.RouteName, ERPStoreRoutes.REGISTER_BILLING_ADDRESS);

			var billingResult = controller.RegisterBillingAddress() as System.Web.Mvc.ViewResult;

			Assert.AreEqual(billingResult.ViewName, GetViewName("registerbillingaddress.aspx"));
			var billingAddressRegistrationUser = billingResult.ViewData.Model as ERPStore.Models.RegistrationUser;
			Assert.IsNotEmpty(billingAddressRegistrationUser.BillingAddressRecipientName);

			billingAddressRegistrationUser.BillingAddressCity = "billingCity";
			billingAddressRegistrationUser.BillingAddressCountryId = ERPStore.Models.Country.Default.Id;
			billingAddressRegistrationUser.BillingAddressRegion = "billingRegion";
			billingAddressRegistrationUser.BillingAddressStreet = "billingStreet";
			billingAddressRegistrationUser.BillingAddressZipCode = "12345";

			var billingPostResult = controller.RegisterBillingAddress(billingAddressRegistrationUser) as System.Web.Mvc.RedirectToRouteResult;
			Assert.AreEqual(billingPostResult.RouteName, ERPStoreRoutes.REGISTER_ACCOUNT_CONFIRMATION);

			var confirmationResult = controller.RegisterConfirmation() as System.Web.Mvc.ViewResult;
			registrationUser = confirmationResult.ViewData.Model as ERPStore.Models.RegistrationUser;

			Assert.AreEqual(registrationUser.BillingAddressCity, "billingCity");

			var confirmationPostResult = controller.RegisterConfirmation("on") as System.Web.Mvc.RedirectToRouteResult;

			Assert.AreEqual(confirmationPostResult.RouteName, ERPStoreRoutes.REGISTER_ACCOUNT_FINALIZED);
			var key = (string)confirmationPostResult.RouteValues["key"];

			var finalizedResult = controller.RegisterFinalized(null, key) as System.Web.Mvc.ViewResult;
			var user = finalizedResult.ViewData.Model as ERPStore.Models.User;

			Assert.IsNotNull(user);

			Assert.AreEqual(user.Presentation, ERPStore.Models.UserPresentation.Mister);
			Assert.AreEqual(user.FirstName, "FirstName");
			Assert.AreEqual(user.LastName, "LastName");
			Assert.AreEqual(user.MobileNumber, "0605040203");
			Assert.AreEqual(user.PhoneNumber, "0102030405");
			Assert.AreEqual(user.FaxNumber, "0102030405");
			Assert.AreEqual(user.Email, "test3@test.com");
			Assert.IsNotNull(user.DefaultAddress);
			Assert.AreEqual(user.DefaultAddress.CountryId, ERPStore.Models.Country.Default.Id);
			Assert.AreEqual(user.DefaultAddress.City, "billingCity");
			Assert.AreEqual(user.DefaultAddress.Region, "billingRegion");
			Assert.AreEqual(user.DefaultAddress.Street, "billingStreet");
			Assert.AreEqual(user.DefaultAddress.ZipCode, "12345");
			Assert.IsNotNull(user.DefaultAddress.RecipientName);

			Assert.IsNotNull(user.Corporate);
			Assert.AreEqual(user.Corporate.Email, "corporate@email.com");
			Assert.AreEqual(user.Corporate.FaxNumber, "0102030405");
			Assert.AreEqual(user.Corporate.Name, "CORPORATE");
			Assert.AreEqual(user.Corporate.PhoneNumber,  "0101010101");
			Assert.AreEqual(user.Corporate.SocialStatus, "Social Status");
			Assert.AreEqual(user.Corporate.WebSite, "http://website.com");
			Assert.AreEqual(user.Corporate.NafCode, "nafcode");
			Assert.AreEqual(user.Corporate.RcsNumber, "RCSNumber");
			Assert.AreEqual(user.Corporate.VatMandatory,  false);
			Assert.AreEqual(user.Corporate.VatNumber,  "FR1234");
			Assert.AreEqual(user.Corporate.SiretNumber, "siretnumber");

			Assert.AreEqual(finalizedResult.ViewName, GetViewName("registered.aspx"));

			// Verification du mot de passe
			var accountController = CreateController<ERPStore.Controllers.AccountController>();
			var loginResult = accountController.Login(user.Login, "123456", false, null) as System.Web.Mvc.RedirectToRouteResult;

			Assert.AreEqual(loginResult.RouteName, ERPStoreRoutes.ACCOUNT);


		}

		[Test]
		public void Register_User()
		{
			// Verification du post
			Assert.AreEqual(true, IsOnlyPostAllowed<ERPStore.Controllers.RegistrationController>(m => m.Register(null, null, null)));

			var controller = CreateController<ERPStore.Controllers.RegistrationController>();

			var result = controller.Register() as System.Web.Mvc.ViewResult;
			var registrationUser = result.ViewData.Model as ERPStore.Models.RegistrationUser;

			registrationUser.PresentationId = (int)ERPStore.Models.UserPresentation.Mister;
			registrationUser.FirstName = "FirstName";
			registrationUser.LastName = "LastName";
			registrationUser.MobileNumber = "0605040302";
			registrationUser.Password = "123456";
			registrationUser.PhoneNumber = "0102030405";
			registrationUser.CorporateName = null;
			registrationUser.FaxNumber = "0102030405";
			registrationUser.Email = "test2@test.com";
			registrationUser.BillingAddressCountryId = ERPStore.Models.Country.Default.Id;

			var registerResult = controller.Register(registrationUser, registrationUser.Email, registrationUser.Password) as System.Web.Mvc.RedirectToRouteResult;
			Assert.AreEqual(registerResult.RouteName, ERPStore.ERPStoreRoutes.REGISTER_BILLING_ADDRESS);

			var billingResult = controller.RegisterBillingAddress() as System.Web.Mvc.ViewResult;

			Assert.AreEqual(billingResult.ViewName, GetViewName("registerbillingaddress.aspx"));
			var billingAddressRegistrationUser = billingResult.ViewData.Model as ERPStore.Models.RegistrationUser;
			Assert.IsNotEmpty(billingAddressRegistrationUser.BillingAddressRecipientName);

			billingAddressRegistrationUser.BillingAddressCity = "billingCity";
			billingAddressRegistrationUser.BillingAddressCountryId = ERPStore.Models.Country.Default.Id;
			billingAddressRegistrationUser.BillingAddressRegion = "billingRegion";
			billingAddressRegistrationUser.BillingAddressStreet = "billingStreet";
			billingAddressRegistrationUser.BillingAddressZipCode = "12345";

			var billingPostResult = controller.RegisterBillingAddress(billingAddressRegistrationUser) as System.Web.Mvc.RedirectToRouteResult;
			Assert.AreEqual(billingPostResult.RouteName, ERPStoreRoutes.REGISTER_ACCOUNT_CONFIRMATION);

			var confirmationResult = controller.RegisterConfirmation() as System.Web.Mvc.ViewResult;
			registrationUser = confirmationResult.ViewData.Model as ERPStore.Models.RegistrationUser;

			Assert.AreEqual(registrationUser.BillingAddressCity, "billingCity");

			var confirmationPostResult = controller.RegisterConfirmation("on") as System.Web.Mvc.RedirectToRouteResult;

			Assert.AreEqual(confirmationPostResult.RouteName, ERPStoreRoutes.REGISTER_ACCOUNT_FINALIZED); 
			var key = (string)confirmationPostResult.RouteValues["key"];

			var finalizedResult = controller.RegisterFinalized(null, key) as System.Web.Mvc.ViewResult;
			var user = finalizedResult.ViewData.Model as ERPStore.Models.User;

			Assert.IsNotNull(user);

			Assert.AreEqual(user.Presentation, ERPStore.Models.UserPresentation.Mister);
			Assert.AreEqual(user.FirstName, "FirstName");
			Assert.AreEqual(user.LastName, "LastName");
			Assert.AreEqual(user.MobileNumber , "0605040302");
			Assert.AreEqual(user.PhoneNumber , "0102030405");
			Assert.IsNull(user.Corporate );
			Assert.AreEqual(user.FaxNumber , "0102030405");
			Assert.AreEqual(user.Email , "test2@test.com");
			Assert.IsNotNull(user.DefaultAddress);
			Assert.AreEqual(user.DefaultAddress.CountryId, ERPStore.Models.Country.Default.Id);
			Assert.AreEqual(user.DefaultAddress.City , "billingCity");
			Assert.AreEqual(user.DefaultAddress.Region , "billingRegion");
			Assert.AreEqual(user.DefaultAddress.Street , "billingStreet");
			Assert.AreEqual(user.DefaultAddress.ZipCode , "12345");
			Assert.IsNotNull(user.DefaultAddress.RecipientName);

			Assert.AreEqual(finalizedResult.ViewName, GetViewName("registered.aspx"));

			// Verification du mot de passe
			var accountController = CreateController<ERPStore.Controllers.AccountController>();
			var loginResult = accountController.Login(user.Login, "123456", false, null) as System.Web.Mvc.RedirectToRouteResult;

			Assert.AreEqual(loginResult.RouteName, ERPStoreRoutes.ACCOUNT);
		}
	}
}
