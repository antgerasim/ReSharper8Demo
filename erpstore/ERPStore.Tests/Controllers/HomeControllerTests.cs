using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ERPStore.Controllers;
using NUnit.Framework;

using Microsoft.Practices.Unity;

namespace ERPStore.Tests.Controllers
{
	[TestFixture]
	public class HomeControllerTests : TestBase
	{
		[SetUp]
		public override void Initialize()
		{
			base.Initialize();
		}

		[Test]
		public void Index()
		{
			var m_Controller = CreateController<ERPStore.Controllers.HomeController>();
			var result = (System.Web.Mvc.ViewResult) m_Controller.Index();
			var settings = result.ViewData.Model as ERPStore.Models.WebSiteSettings;
			Assert.AreEqual(result.ViewName, "Index");
			Assert.IsNotNull(settings);
		}

	    [Test]
	    public void Submit_Contact()
	    {
	        var m_Controller = CreateController<ERPStore.Controllers.HomeController>();
	        var contactInfo = new ERPStore.Models.ContactInfo();
	        contactInfo.Email = "test@email.com";
	        contactInfo.FullName = "test";
	        contactInfo.Message = "test";
	        contactInfo.CorporateName = "test";
	        contactInfo.PhoneNumber = "0102030405";

	        var result = (System.Web.Mvc.ViewResult)m_Controller.Contact(contactInfo);

	        Assert.AreEqual(result.ViewData.ModelState.IsValid, true);

	        Assert.IsNotNull(result.ViewData["IsSent"]);
	    }

	    [Test]
		public void CatchAll()
		{
			var m_Controller = CreateController<ERPStore.Controllers.HomeController>();
			var result = (System.Web.Mvc.ViewResult)m_Controller.CatchAll();
			Assert.AreEqual(result.ViewName, "Index");
		}

		[Test]
		public void CatchAll_With_RawUrl()
		{
			var httpContext = CreateMockHttpContext();
			m_Container.RegisterInstance(typeof(System.Web.HttpContextBase), httpContext.Object);
			httpContext.Setup(ctx => ctx.Request.RawUrl).Returns("/badpage");
			var controller = m_Container.Resolve<ERPStore.Controllers.HomeController>();
			controller.SetupControllerContext(httpContext.Object);
			var result = (System.Web.Mvc.ViewResult)controller.CatchAll();
			Assert.AreEqual(result.ViewName, "404");
		}

		[Test]
		public void CatchAll_With_Language_Parameter()
		{
			var httpContext = CreateMockHttpContext();
			m_Container.RegisterInstance(typeof(System.Web.HttpContextBase), httpContext.Object);
			var controller = m_Container.Resolve<ERPStore.Controllers.HomeController>();
			httpContext.Setup(ctx => ctx.Request.RawUrl).Returns("/?language=fr");
			controller.SetupControllerContext(httpContext.Object);
			var result = (System.Web.Mvc.ViewResult)controller.CatchAll();
			Assert.AreEqual(result.ViewName, "Index");
		}

		[Test]
		public void TermsAndConditions()
		{
			var m_Controller = CreateController<ERPStore.Controllers.HomeController>();
			var result = (System.Web.Mvc.ViewResult)m_Controller.TermsAndConditions();
			var settings = result.ViewData.Model as ERPStore.Models.WebSiteSettings;
			Assert.AreEqual(result.ViewName, string.Empty);
			Assert.IsNotNull(settings);
		}

		[Test]
		public void LegalInformation()
		{
			var m_Controller = CreateController<ERPStore.Controllers.HomeController>();
			var result = (System.Web.Mvc.ViewResult)m_Controller.LegalInformation();
			var settings = result.ViewData.Model as ERPStore.Models.WebSiteSettings;
			Assert.AreEqual(result.ViewName, string.Empty);
			Assert.IsNotNull(settings);
		}

		[Test]
		public void Help()
		{
			var m_Controller = CreateController<ERPStore.Controllers.HomeController>();
			var result = (System.Web.Mvc.ViewResult)m_Controller.Help();
			var settings = result.ViewData.Model as ERPStore.Models.WebSiteSettings;
			Assert.AreEqual(result.ViewName, string.Empty);
			Assert.IsNotNull(settings);
		}

		[Test]
		public void About()
		{
			var m_Controller = CreateController<ERPStore.Controllers.HomeController>();
			var result = (System.Web.Mvc.ViewResult)m_Controller.About();
			var settings = result.ViewData.Model as ERPStore.Models.WebSiteSettings;
			Assert.AreEqual(result.ViewName, string.Empty);
			Assert.IsNotNull(settings);
		}

		[Test]
		public void Error()
		{
			var m_Controller = CreateController<ERPStore.Controllers.HomeController>();
			var result = (System.Web.Mvc.ViewResult)m_Controller.Error();
			Assert.AreEqual(result.ViewName, "500");
			Assert.AreEqual(m_Controller.Response.StatusCode, 500);
		}

		[Test]
		public void Anonymous_Contact()
		{
			var m_Controller = CreateController<ERPStore.Controllers.HomeController>();
			var result = (System.Web.Mvc.ViewResult)m_Controller.Contact();
			var contact = result.ViewData.Model as ERPStore.Models.ContactInfo;
			Assert.AreEqual(result.ViewName, string.Empty);
			Assert.IsNotNull(contact);
		}

		[Test]
		public void Authenticated_Contact()
		{
			var controller = CreateAuthenticatedController<ERPStore.Controllers.HomeController>();

			var result = (System.Web.Mvc.ViewResult)controller.Contact();
			var contact = result.ViewData.Model as ERPStore.Models.ContactInfo;

			Assert.AreEqual(result.ViewName, string.Empty);
			Assert.IsNotNull(contact);

			Assert.AreEqual(contact.Email, "test@erpstore.net");
		}

		[Test]
		public void Submit_Empty_Contact()
		{
			var m_Controller = CreateController<ERPStore.Controllers.HomeController>();
			var result = (System.Web.Mvc.RedirectToRouteResult)m_Controller.Contact(null);
			Assert.AreEqual(result.RouteName, ERPStoreRoutes.HOME);
		}

		[Test]
		public void Submit_Contact_With_Bad_Data()
		{
			var m_Controller = CreateController<ERPStore.Controllers.HomeController>();
			var contactInfo = new ERPStore.Models.ContactInfo();
			contactInfo.Email = "@bademail";
			contactInfo.FullName = null;
			var result = (System.Web.Mvc.ViewResult)m_Controller.Contact(contactInfo);

			Assert.AreEqual(result.ViewData.ModelState.IsValid, false);
		}

	    public class blabla
	    {
	        Because of = () => {  };

	        It should_something = () => { };
	    }


	    [Test]
		public void Show_Header()
		{
			var m_Controller = CreateController<ERPStore.Controllers.HomeController>();
			var result = (System.Web.Mvc.ViewResult)m_Controller.ShowHeader("test");
			var settings = result.ViewData.Model as ERPStore.Models.WebSiteSettings;

			Assert.AreEqual("~/views/shared/test", result.ViewName);
			Assert.IsNotNull(settings);
		}

		[Test]
		public void Show_Footer()
		{
			var m_Controller = CreateController<ERPStore.Controllers.HomeController>();
			var result = (System.Web.Mvc.PartialViewResult)m_Controller.ShowFooter("test");
			var settings = result.ViewData.Model as ERPStore.Models.WebSiteSettings;

			Assert.AreEqual("~/views/shared/test", result.ViewName);
			Assert.IsNotNull(settings);
		}

		[Test]
		public void Show_Menu()
		{
			var m_Controller = CreateController<ERPStore.Controllers.HomeController>();
			var result = (System.Web.Mvc.ViewResult)m_Controller.ShowMenu("test", new System.Web.Routing.RouteData());
			var menus = result.ViewData.Model as List<ERPStore.Models.MenuItem>;

			Assert.AreEqual("~/views/shared/test", result.ViewName);
			Assert.IsNotNull(menus);
		}

	}
}
