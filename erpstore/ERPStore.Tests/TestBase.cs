using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using ERPStore.Models;
using ERPStore.Services;
using ERPStore.Tests.Controllers;

using Microsoft.Practices.Unity;

using Moq;

using NUnit.Framework;

namespace ERPStore.Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestFixture]
	public abstract class TestBase
	{
		private bool m_IsRoutesRegistered = false;

		public TestBase()
		{
		}

		protected Microsoft.Practices.Unity.IUnityContainer m_Container;
		protected string m_FolderView;


		public virtual void Initialize()
		{
			m_Container = new Microsoft.Practices.Unity.UnityContainer();
			m_Container.RegisterType<ERPStore.Logging.ILogger, ERPStore.Logging.ConsoleLogger>(new ContainerControlledLifetimeManager());

			// Repositories
			m_Container.RegisterType<ERPStore.Repositories.ICartRepository, Repositories.MockCartRepository>(new ContainerControlledLifetimeManager());
			m_Container.RegisterType<MockConnector.Repositories.CatalogRepository>(new ContainerControlledLifetimeManager());
			m_Container.RegisterType<ERPStore.Repositories.ICouponRepository, Repositories.MockCouponRepository>(new ContainerControlledLifetimeManager());
			m_Container.RegisterType<ERPStore.Repositories.IRegistrationRepository, ERPStore.MockConnector.Repositories.RegistrationRepository>(new ContainerControlledLifetimeManager());

			// Services
			m_Container.RegisterType<ERPStore.Services.ICacheService, ERPStore.Services.MixedCacheService>(new ContainerControlledLifetimeManager());
			m_Container.RegisterType<ERPStore.Services.ICartService, ERPStore.Services.CartService>(new ContainerControlledLifetimeManager());
			m_Container.RegisterType<ERPStore.Services.ICatalogService, ERPStore.MockConnector.CatalogService>(new ContainerControlledLifetimeManager());
			m_Container.RegisterType<ERPStore.Services.IAccountService, ERPStore.MockConnector.AccountService>(new ContainerControlledLifetimeManager());
			m_Container.RegisterType<ERPStore.Services.IDocumentService, ERPStore.MockConnector.DocumentService>(new ContainerControlledLifetimeManager());
			m_Container.RegisterType<ERPStore.Services.ISalesService, ERPStore.MockConnector.SalesService>(new ContainerControlledLifetimeManager());
			m_Container.RegisterType<ERPStore.Services.IConnectorService, ERPStore.MockConnector.ConnectorService>(new ContainerControlledLifetimeManager());
			m_Container.RegisterType<ERPStore.Services.IEmailerService, ERPStore.MockConnector.EmailerService>(new ContainerControlledLifetimeManager());
			m_Container.RegisterType<ERPStore.Services.IAddressService, ERPStore.MockConnector.AddressService>(new ContainerControlledLifetimeManager());
			m_Container.RegisterType<ERPStore.Services.IIncentiveService, ERPStore.Services.IncentiveService>(new ContainerControlledLifetimeManager());
			m_Container.RegisterType<ERPStore.Services.IScheduledTaskService, ERPStore.Services.ScheduledTaskService>(new ContainerControlledLifetimeManager());
			m_Container.RegisterType<ERPStore.Services.IEventPublisher, ERPStore.Services.EventPublisher>(new ContainerControlledLifetimeManager());
			m_Container.RegisterType<ERPStore.Services.IEventSubscriptionService, ERPStore.Services.EventSubscriptionService>(new ContainerControlledLifetimeManager());
			m_Container.RegisterType<ERPStore.Services.ISearchOptimizationService, ERPStore.Services.VoidSearchOptimizationService>(new ContainerControlledLifetimeManager());
			m_Container.RegisterType<ERPStore.Services.ISettingsService, ERPStore.MockConnector.SettingsService>(new ContainerControlledLifetimeManager());
			m_Container.RegisterType<ERPStore.Services.ILocalizationService, ERPStore.Services.VoidLocalizationService>(new ContainerControlledLifetimeManager());
			// m_Container.RegisterType<ERPStore.Services.IRoutesRegistrationService, ERPStore.ERPStoreRoutes>(new ContainerControlledLifetimeManager());
			m_Container.RegisterType<ERPStore.Services.IScheduledTaskService, TaskService>(new ContainerControlledLifetimeManager());

			// Modes de reglement
			m_Container.RegisterType<ERPStore.Services.IPaymentService, ERPStore.Services.PaymentByCheckService>("check",new ContainerControlledLifetimeManager());
			m_Container.RegisterType<ERPStore.Services.IPaymentService, ERPStore.Services.PaymentByWireTransferService>("wirelesstransfer",new ContainerControlledLifetimeManager());

			// Cryptage
			var cryptoProvider = new System.Security.Cryptography.RC2CryptoServiceProvider();
			cryptoProvider.GenerateKey();
			cryptoProvider.GenerateIV();

			var injectionMembers = new InjectionMember[] 
					{ 
						new InjectionConstructor(cryptoProvider.Key, cryptoProvider.IV) ,
					};

			m_Container.RegisterType<ERPStore.Services.CryptoService>(new ContainerControlledLifetimeManager(), injectionMembers);

			// Enregistrement des routes
			System.Web.Routing.RouteTable.Routes.Clear();
			var routes = m_Container.Resolve<ERPStore.ERPStoreRoutes>();
			routes.Register();

			new ERPStore.ERPStoreApplication(m_Container);

			ERPStore.ERPStoreApplication.WebSiteSettings.Shipping.DeliveryCountryList.Add(new DeliveryCountry()
			{
				Country = ERPStore.Models.Country.FRA,
				DefaultShippingFeeAmount = 5.0m,
				MinimalFreeOfCarriageAmount = 10m,
				MinimalOrderAmount = 0,
			});
		}

		protected T CreateController<T>()
			where T : System.Web.Mvc.Controller
		{
			var httpContext = CreateMockHttpContext().Object;
			return CreateController<T>(httpContext);
		}

		protected T CreateController<T>(System.Web.HttpContextBase httpContext)
			where T : System.Web.Mvc.Controller
		{
			m_Container.RegisterInstance(typeof(System.Web.HttpContextBase), httpContext, new PerThreadLifetimeManager());
			var controller = m_Container.Resolve<T>();
			controller.SetupControllerContext(httpContext);

			return controller;
		}

		public T CreateAuthenticatedController<T>()
			where T : System.Web.Mvc.Controller
		{
			var accountService = m_Container.Resolve<IAccountService>();
			var user = accountService.GetUserById(1);
			return CreateAuthenticatedController<T>(user);
		}

		public T CreateAuthenticatedController<T>(ERPStore.Models.User user)
			where T : System.Web.Mvc.Controller
		{
			var httpContext = CreateMockHttpContext();
			m_Container.RegisterInstance(typeof(System.Web.HttpContextBase), httpContext.Object, new PerThreadLifetimeManager());

			var authenticatedUser = GetCustomerUserPrincipal(user);
			httpContext.Setup(ctx => ctx.User).Returns(authenticatedUser);

			var controller = m_Container.Resolve<T>();
			controller.SetupControllerContext(httpContext.Object);

			// Ajout du cookie authentifié
			httpContext.Object.Response.AddAuthenticatedCookie(user.Id, true);

			return controller;
		}

		public T CreateControllerWithForm<T>(FormCollection form)
			where T : System.Web.Mvc.Controller
		{
			var httpContext = CreateMockHttpContext();
			httpContext.Setup(i => i.Request.Form).Returns(form);
			m_Container.RegisterInstance(typeof(System.Web.HttpContextBase), httpContext.Object, new PerThreadLifetimeManager());
			var controller = m_Container.Resolve<T>();
			controller.SetupControllerContext(httpContext.Object);

			return controller;
			
		}

		protected ERPStore.Models.UserPrincipal GetCustomerUserPrincipal()
		{
			var accountService = m_Container.Resolve<IAccountService>();
			var user = accountService.GetUserById(1);
			return GetCustomerUserPrincipal(user);
		}

		protected ERPStore.Models.UserPrincipal GetCustomerUserPrincipal(ERPStore.Models.User user)
		{
			var ticket = new FormsAuthenticationTicket("mock", false, 100);
			var id = new FormsIdentity(ticket);
			var userPrincipal = new ERPStore.Models.UserPrincipal(id, Guid.NewGuid().ToString());
			userPrincipal.CurrentUser = user;
			user.Roles.Add("customer");
			return userPrincipal;
		}

		protected Mock<HttpContextBase> CreateMockHttpContext()
		{
			var principal = new ERPStore.Models.UserPrincipal(Guid.NewGuid().ToString());
			var context = new Mock<HttpContextBase>();
			var request = new Mock<HttpRequestBase>();
			var response = new Mock<HttpResponseBase>();
			var session = new Mock<HttpSessionStateBase>();
			var server = new Mock<HttpServerUtilityBase>();
			var cachePolicy = new Mock<HttpCachePolicyBase>();
			var cookies = new HttpCookieCollection();

			context.Setup(ctx => ctx.Request).Returns(request.Object);
			context.Setup(ctx => ctx.Request.AnonymousID).Returns(principal.VisitorId);

			context.Setup(ctx => ctx.Response).Returns(response.Object);
			context.SetupProperty(ctx => ctx.Response.StatusCode, 200);
			context.Setup(ctx => ctx.Response.Cache).Returns(cachePolicy.Object);

			context.Setup(ctx => ctx.Session).Returns(session.Object);
			context.Setup(ctx => ctx.Server).Returns(server.Object);

			context.Setup(ctx => ctx.User).Returns(principal);
			context.Setup(ctx => ctx.Response.Cookies).Returns(cookies);
			context.Setup(ctx => ctx.Request.Cookies).Returns(cookies);
			context.Setup(ctx => ctx.Request.Url).Returns(new Uri("http://www.google.com"));
			context.Setup(ctx => ctx.Request.Headers).Returns(new System.Collections.Specialized.NameValueCollection());

			context.Setup(ctx => ctx.Items).Returns(new Dictionary<string, object>());
			context.Object.Request.Cookies.Add(new HttpCookie("erpstorevid")
			{
				Value = principal.VisitorId,
			});

			return context;
		}

		protected TestControllerActionInvoker<Result> ControllerActionInvoker<Result>()
			where Result : ActionResult
		{
			return new TestControllerActionInvoker<Result>(m_Container);
		}

		protected bool IsOnlyPostAllowed<T>(Expression<Action<T>> action)
		{
			var body = action.Body as MethodCallExpression;

			var attribute = body.Method.GetCustomAttributes(typeof(AcceptVerbsAttribute), false)
									 .Cast<AcceptVerbsAttribute>()
									 .SingleOrDefault();

			return (attribute != null && attribute.Verbs.Contains(HttpVerbs.Post.ToString().ToUpper()));
		}

		protected string GetViewName(string viewName)
		{
			return string.Format(m_FolderView, viewName);
		}
	}
}
