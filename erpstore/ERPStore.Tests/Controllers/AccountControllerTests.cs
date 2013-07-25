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
	public class AccountControllerTests : TestBase
	{
		[SetUp]
		public override void Initialize()
		{
			base.Initialize();
		}

		[Test]
		public void Anonymous_Index()
		{
			var m_Controller = CreateController<ERPStore.Controllers.AccountController>();
			var unauthorized = ControllerActionInvoker<HttpUnauthorizedResult>().InvokeAction(m_Controller.ControllerContext, "Index");
			Assert.AreEqual(true, unauthorized);
		}

		[Test]
		public void Authenticated_Index()
		{
			var authenticatedUser = new ERPStore.Models.User();
			authenticatedUser.Email = "authenticated@email.com";

			var m_Controller = CreateAuthenticatedController<ERPStore.Controllers.AccountController>(authenticatedUser);
			var authorized = ControllerActionInvoker<ViewResult>().InvokeAction(m_Controller.ControllerContext, "Index");
			Assert.AreEqual(true, authorized);

			var result = m_Controller.Index() as System.Web.Mvc.ViewResult;
			Assert.AreEqual(result.ViewName, string.Empty);
			var user = result.ViewData.Model as ERPStore.Models.User;

			Assert.AreEqual(user.Email, "authenticated@email.com");
		}

		[Test]
		public void Login()
		{
			var m_Controller = CreateController<ERPStore.Controllers.AccountController>();
			var result = m_Controller.Login() as System.Web.Mvc.ViewResult;
			Assert.AreEqual(result.ViewName, string.Empty);
		}

		[Test]
		public void Login_Form()
		{
			// Verification de l'attribut post
			Assert.AreEqual(true, IsOnlyPostAllowed<ERPStore.Controllers.AccountController>(m => m.Login("","",null,null)));

			var accountService = m_Container.Resolve<ERPStore.Services.IAccountService>();
			var user = new ERPStore.Models.User();
			user.Login = "userName1";
			user.Id = 12345;
			accountService.SaveUser(user);

			accountService.SetPassword(user, "password1");

			var controller = CreateController<ERPStore.Controllers.AccountController>();

			var result = controller.Login("userName1", "password1", false, null) as System.Web.Mvc.RedirectToRouteResult;

			Assert.AreEqual(result.RouteName, ERPStoreRoutes.ACCOUNT); 
		}

		[Test]
		public void Empty_Login_Form()
		{
			var controller = CreateController<ERPStore.Controllers.AccountController>();

			var result = controller.Login(null, null, false, null) as System.Web.Mvc.ViewResult;

			Assert.AreEqual(result.ViewName, string.Empty);
			Assert.IsFalse(result.ViewData.ModelState.IsValid);
		}

		[Test]
		public void Authenticated_Login()
		{
			var controller = CreateAuthenticatedController<ERPStore.Controllers.AccountController>();

			var result = controller.Login(null, null, false, null) as System.Web.Mvc.RedirectToRouteResult;

			Assert.AreEqual(result.RouteName, ERPStore.ERPStoreRoutes.ACCOUNT);
		}

		[Test]
		public void Login_With_Bad_Login_Password()
		{
			var controller = CreateController<ERPStore.Controllers.AccountController>();

			var result = controller.Login("bidon", "bidon", false, null) as System.Web.Mvc.ViewResult;

			Assert.AreEqual(result.ViewName, string.Empty);
			Assert.IsFalse(result.ViewData.ModelState.IsValid);
		}

		[Test]
		public void Logoff()
		{
			var controller = CreateAuthenticatedController<ERPStore.Controllers.AccountController>();

			var result = controller.Logoff(null) as System.Web.Mvc.RedirectToRouteResult;
			Assert.AreEqual(result.RouteName, ERPStore.ERPStoreRoutes.HOME);

		}

		public void Logoff_With_ReturnUrl()
		{
			var controller = CreateAuthenticatedController<ERPStore.Controllers.AccountController>();
			var result = controller.Logoff("http://www.google.fr") as System.Web.Mvc.RedirectResult;
			Assert.AreEqual(result.Url, "http://www.google.fr");
		}

		[Test]
		public void Recover_Password()
		{
			var controller = CreateController<ERPStore.Controllers.AccountController>();

			var result = controller.RecoverPassword() as System.Web.Mvc.ViewResult;
			Assert.AreEqual(result.ViewName, string.Empty);
		}

		[Test]
		public void Recover_Password_Form()
		{
			var accountService = m_Container.Resolve<ERPStore.Services.IAccountService>();
			var user = new ERPStore.Models.User();
			user.Login = "userName1";
			user.Email = "email@domain.com";
			user.Id = 12345;
			accountService.SaveUser(user);

			var controller = CreateController<ERPStore.Controllers.AccountController>();

			var result = controller.RecoverPassword("userName1") as System.Web.Mvc.ViewResult;
			Assert.AreEqual(result.ViewName, string.Empty);
			Assert.AreEqual((bool)result.ViewData["PasswordSent"], true);

			var key = (string)result.ViewData["key"];

			result = controller.ChangePassword(key) as System.Web.Mvc.ViewResult;

			Assert.AreEqual(result.ViewName, string.Empty);
			key = (string)result.ViewData["ChangePasswordKey"];

			var changePasswordResult = controller.ChangePassword(key, "newpassword", "newpassword") as System.Web.Mvc.ViewResult;

			Assert.AreEqual(result.ViewName, string.Empty);

			var loginResult = controller.Login("userName1", "newpassword", false, null) as System.Web.Mvc.RedirectToRouteResult;

			Assert.AreEqual(loginResult.RouteName, ERPStore.ERPStoreRoutes.ACCOUNT);
		}

		[Test]
		public void Recover_Password_Form_Bad_UserName()
		{
			var controller = CreateController<ERPStore.Controllers.AccountController>();

			var result = controller.RecoverPassword("unknown") as System.Web.Mvc.ViewResult;
			Assert.AreEqual(result.ViewName, string.Empty);
		}

	}
}
