using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Security.Cryptography;
using System.Reflection;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;

using ERPStore.Html;

namespace ERPStore
{
	public class ERPStoreApplication
	{
		Logging.ILogger m_Logger;
		static bool m_IsFirstRequest = true;
		static object m_Lock = new object();
		static bool m_FailedOnInitialize = false;
		static string m_Host;
		static List<string> PageNotFoundList = new List<string>();
		private static Models.WebSiteSettings m_WebSiteSettings;
		private static bool m_IsWebSiteSettingsReady;

		public ERPStoreApplication()
		{

		}

		// For tests
		public ERPStoreApplication(IUnityContainer container)
		{
			// Container = container;
			m_WebSiteSettings = null;
			m_IsWebSiteSettingsReady = true;
		}

		#region properties

		public static Models.WebSiteSettings WebSiteSettings
		{
			get
			{
				if (m_WebSiteSettings == null && m_IsWebSiteSettingsReady)
				{
					var storeService = System.Web.Mvc.DependencyResolver.Current.GetService<Services.ISettingsService>();
					m_WebSiteSettings = storeService.GetWebSiteSettings(m_Host);
				}
				return m_WebSiteSettings;
			}
		}

		private static IUnityContainer m_DIContainer;
        [Obsolete("Use DependencyResolver.Current Instead", true)]
		public static IUnityContainer Container
		{
			get
			{
				return m_DIContainer;
			}
			private set
			{
				m_DIContainer = value;
			}
		}

		#endregion

		#region Web Application Events

		public void Start(System.Web.HttpContext ctx)
		{
			AreaRegistration.RegisterAllAreas();

			RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			RouteTable.Routes.IgnoreRoute("{handler}.ashx");
			RouteTable.Routes.IgnoreRoute("robots.txt");
			RouteTable.Routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
			RouteTable.Routes.IgnoreRoute("services/{*service}");
			RouteTable.Routes.IgnoreRoute("content/{*content}");
			RouteTable.Routes.IgnoreRoute("scripts/{*scripts}");
			RouteTable.Routes.IgnoreRoute("sitemaps/{*sitemaps}");

			// ViewEngines.Engines.Clear();
			// ViewEngines.Engines.Add(new ERPStoreViewEngine());
		}

		public void Stop()
		{
            if (System.Web.Mvc.DependencyResolver.Current != null)
			{
				var taskService = System.Web.Mvc.DependencyResolver.Current.GetService<Services.IScheduledTaskService>();
				taskService.Stop();
			}
		}

		public void Error(System.Web.HttpContext ctx)
		{
			// Code that runs when an unhandled error occurs
			if (ctx.Request.Url.Host.IndexOf("localhost") != -1)
			{
				return;
			}
			var ex = ctx.Server.GetLastError();
			var isNewVisitor = false;
			var visitorId = ctx.GetOrCreateVisitorId(out isNewVisitor);
			ex.Data.Add("Message", ex.Message);
			ex.Data.Add("machineName", Environment.MachineName);
			ex.Data.Add("host", ctx.Request.Url.Host);
			ex.Data.Add("visitorId", visitorId);
			ex.Data.Add("userHostAddress", ctx.Request.UserHostAddress);
			ex.Data.Add("userHostName", ctx.Request.UserHostName);
			ex.Data.Add("url", ctx.Request.RawUrl);
			ex.Data.Add("referer", ctx.Request.UrlReferrer);
			ex.Data.Add("applicationPath", ctx.Request.ApplicationPath);
			ex.Data.Add("user-agent", ctx.Request.Headers["User-Agent"]);
			ex.Data.Add("cookie", ctx.Request.Headers["Cookie"]);
			if (ctx.Request.Form.Count > 0)
			{
				ex.Data.Add("begin-form", "-----------------------");
				foreach (var item in ctx.Request.Form.AllKeys)
				{
					ex.Data.Add(item, ctx.Request.Form[item]);
				}
				ex.Data.Add("end-form", "-----------------------");
			}
			if (ctx.User.Identity.IsAuthenticated)
			{
				ex.Data.Add("user", ctx.User.GetUserPrincipal().CurrentUser.Login);
			}
			else
			{
				ex.Data.Add("user", "anonymous");
			}

			var logger = System.Web.Mvc.DependencyResolver.Current.GetService<Logging.ILogger>();
			logger.Error(ex);
		}

		public void AuthenticateRequest(System.Web.HttpContext ctx)
		{
			if (ctx.Request.RawUrl.ToLower().StartsWith("/images")
				|| ctx.Request.RawUrl.ToLower().StartsWith("/content")
				|| ctx.Request.RawUrl.ToLower().StartsWith("/scripts")
				|| ctx.Request.RawUrl.ToLower().StartsWith("/services")
				|| ctx.Request.RawUrl.ToLower().StartsWith("/favicon.ico"))
			{
				return;
			}

			var isNewVisitor = false;
			var visitorId = ctx.GetOrCreateVisitorId(out isNewVisitor);
			var principal = new Models.UserPrincipal(visitorId);
			ctx.User = principal;

			var authCookie = ctx.Request.Cookies[FormsAuthentication.FormsCookieName];
			if (authCookie == null)
			{
				return;
			}

            var container = System.Web.Mvc.DependencyResolver.Current;
			var logger = container.GetService<Logging.ILogger>();

			logger.Debug("RequestAuthenticated {0}", ctx.Request.Url);
			// Extraction et decryptage du ticket d'autentification
			FormsAuthenticationTicket authTicket = null;
			try
			{
				authTicket = FormsAuthentication.Decrypt(authCookie.Value);
			}
			catch (Exception ex)
			{
				logger.Error(ex);
			}

			if (authTicket == null)
			{
				return;
			}

			// Recuperation du userId
			int userId = 0;

			int.TryParse(authTicket.Name, out userId);

			if (userId > 0)
			{
				// Creation d'une nouvelle identité
				var id = new FormsIdentity(authTicket);

				// On passe l'identité et les roles a l'objet Principal
				principal = new Models.UserPrincipal(id, visitorId);
                var authenticationService = container.GetService<Services.IAccountService>();
				var user = authenticationService.GetUserById(userId);
				if (user != null)
				{
                    if (!user.LastLoginDate.HasValue)
                    {
                        user.LastLoginDate = DateTime.UtcNow;
                    }
					principal.CurrentUser = user;
					logger.Debug("User : {0} Logged", principal.CurrentUser.FullName);
				}
				else
				{
					principal = new Models.UserPrincipal(visitorId);
					FormsAuthentication.SignOut();
				}
				ctx.User = principal;
			}
		}

		public void BeginRequest(System.Web.HttpContext ctx)
		{
			if (m_IsFirstRequest)
			{
				lock (m_Lock)
				{
					if (m_IsFirstRequest)
					{
						try
						{
							Initialize(ctx);
							m_IsWebSiteSettingsReady = true;
						}
						catch (Exception ex)
						{
							m_FailedOnInitialize = true;
							if (ctx.Request.IsLocal
								|| ctx.Request.Url.Host.IndexOf("localhost") != -1)
							{
								throw new System.Web.HttpException(500, ex.Message);
							}
							else
							{
								System.Diagnostics.EventLog.WriteEntry("Application", ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
							}
						}
						finally
						{
							m_IsFirstRequest = false;
						}
					}
				}
			}

			if (m_FailedOnInitialize)
			{
				ctx.Response.Write("Failed on initialization, show event log for more informations");
				ctx.Response.End();
			}
		}

		#endregion

		public void ReloadSettings()
		{
			m_WebSiteSettings = null;
		}

		private void Initialize(System.Web.HttpContext ctx)
		{
			m_Host = ctx.Request.Url.Host;
			var contextBase = new System.Web.HttpContextWrapper(ctx);

			var container = new UnityContainer();

			container.AddNewExtension<Interception>();

			// Enregistrement des repositories
			container.RegisterType<Repositories.ICouponRepository, Repositories.NullCouponRepository>(new ContainerControlledLifetimeManager());
			container.RegisterType<Repositories.ICartRepository, Repositories.HttpContextCartRepository>(new ContainerControlledLifetimeManager());
			container.RegisterType<Repositories.IRegistrationRepository, Repositories.SessionRegistrationRepository>(new ContainerControlledLifetimeManager());
			container.RegisterType<Repositories.ILocalizationRepository, Repositories.VoidLocalizationRepository>(new ContainerControlledLifetimeManager());
			container.RegisterType<Repositories.ICommentRepository, Repositories.VoidCommentRepository>(new ContainerControlledLifetimeManager());

			// Enregistrement des services
			container.RegisterType<Services.ICacheService, Services.SimpleCacheService>(new ContainerControlledLifetimeManager());
			container.RegisterType<Services.ICartService, Services.CartService>(new ContainerControlledLifetimeManager());
			container.RegisterType<Services.IScheduledTaskService, Services.ScheduledTaskService>(new ContainerControlledLifetimeManager());
			container.RegisterType<Services.IEmailerService, Services.EmailerService>(new ContainerControlledLifetimeManager());
			container.RegisterType<Services.IIncentiveService, Services.IncentiveService>(new ContainerControlledLifetimeManager());
			container.RegisterType<Services.ILocalizationService, Services.VoidLocalizationService>(new ContainerControlledLifetimeManager());
			container.RegisterType<Services.ISearchOptimizationService, Services.VoidSearchOptimizationService>(new ContainerControlledLifetimeManager());
			var pluginEnumeratorInjectionMembers = new InjectionMember[] 
			{
				new InjectionConstructor(
						ctx.Server.MapPath("/plugins.config")
						, ctx.Server.MapPath("/bin")
				)
			};
			container.RegisterType<Extensibility.IPluginEnumerator, Extensibility.XmlConfigPluginEnumerator>(new ContainerControlledLifetimeManager(), pluginEnumeratorInjectionMembers);
			container.RegisterType<Extensibility.IPluginLoaderService, Extensibility.PluginLoaderService>(new ContainerControlledLifetimeManager());
			container.RegisterType<Services.IEventSubscriptionService, Services.EventSubscriptionService>(new ContainerControlledLifetimeManager());
			container.RegisterType<Services.IEventPublisher, Services.EventPublisher>(new ContainerControlledLifetimeManager());
			container.RegisterType<Services.ISettingsService, Services.SettingsService>(new ContainerControlledLifetimeManager());
			container.RegisterType<Services.ISalesService, Services.SalesService>(new ContainerControlledLifetimeManager());
			// Configuration des routes
			container.RegisterType<Services.IRoutesRegistrationService, ERPStoreRoutes>(new ContainerControlledLifetimeManager());

			// Enregistrement des modes de reglement par defaut
			container.RegisterType<Services.IPaymentService, Services.PaymentByCheckService>("check",new ContainerControlledLifetimeManager());
			container.RegisterType<Services.IPaymentService, Services.PaymentByWireTransferService>("wiretransfer",new ContainerControlledLifetimeManager());

			// Global Event Consumer
			container.RegisterType<Services.UserLoggedEventMessage>("userLoggedEventMessage", new ContainerControlledLifetimeManager());

			// Logger par defaut
			container.RegisterType<Logging.ILogger, Logging.ConsoleLogger>(new PerThreadLifetimeManager());

			// Chargement des services et overriding a partir du fichier
			// de configuration unity.config
			var map = new ExeConfigurationFileMap();
			var unityConfigFileName = ERPStore.Configuration.ConfigurationSettings.AppSettings["unityConfigFileName"] ?? "unity.config";
			map.ExeConfigFilename = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"\"), unityConfigFileName);
			var config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
			var section = (UnityConfigurationSection)config.GetSection("unity");
			if (section == null)
			{
				throw new Exception(string.Format("unity section in {0} does not exists", unityConfigFileName));
			}
			section.Configure(container);

			RegisterGlobalFilters(GlobalFilters.Filters);

			container.RegisterInstance<IControllerFactory>(System.Web.Mvc.ControllerBuilder.Current.GetControllerFactory());
			container.RegisterType<IControllerActivator, UnityControllerActivator>();
			container.RegisterType<IViewPageActivator, UnityViewPageActivator>();
			var filterProvider = new UnityFilterProvider(container);
			container.RegisterInstance<IFilterProvider>("attributes", filterProvider);
			container.RegisterType<ModelMetadataProvider, DataAnnotationsModelMetadataProvider>();
			var resolver = new UnityDependencyResolver(container);
			System.Web.Mvc.DependencyResolver.SetResolver(resolver);

			m_Logger = container.Resolve<Logging.ILogger>();

			// Resolution et chargement des paramètres de configuration du site
			m_Logger.Info("Loading site configuration");
			var settingsService = container.Resolve<Services.ISettingsService>();
			m_WebSiteSettings = settingsService.GetWebSiteSettings(m_Host);
			m_WebSiteSettings.PhysicalPath = ctx.Server.MapPath("/");

			var injectionMembers = new InjectionMember[] 
			{ 
				new InjectionConstructor(m_WebSiteSettings.CryptoKey, m_WebSiteSettings.CryptoIV) ,
			};
			container.RegisterType<Services.CryptoService>(new ContainerControlledLifetimeManager(), injectionMembers);

			m_Logger.Info("Loading plugins");
			// Chargement des plugins
			var pluginLoaderService = container.Resolve<Extensibility.IPluginLoaderService>();
			var pluginEnumeratorService = container.Resolve<Extensibility.IPluginEnumerator>();
			var plugins = pluginEnumeratorService.EnumeratePlugins();
			pluginLoaderService.Load(plugins, (System.Web.HttpContextBase)contextBase, m_WebSiteSettings);
			m_Logger.Info("Plugins loaded");

			// Configuration des routes
			var erpStoreRoutes = container.Resolve<Services.IRoutesRegistrationService>();
			erpStoreRoutes.Register();
 
			// Enregistrement des routes par defaut

			RouteTable.Routes.MapERPStoreRoute(
				"Admin"
				, "admin/{action}/{id}"
				, new { controller = "Admin", action = "Index", id = string.Empty }
			);

			RouteTable.Routes.MapERPStoreRoute(
				"CatchAll"
				, "{*catchall}"
				, new { controller = "Home", action = "CatchAll" }
			);

			// Ne pas supprimer cette route
			RouteTable.Routes.MapERPStoreRoute(
				"Default",
				"{controller}/{action}/{id}",
				new { controller = "Home", action = "Index", id = string.Empty }
			);

			var eventPublisherService = container.Resolve<Services.IEventPublisher>();
			eventPublisherService.Publish(new Models.Events.RegisteredRoutesEvent());

			m_Logger.Info("Routes configured");

			// Demarrage du planificateur de taches 
			var taskService = container.Resolve<Services.IScheduledTaskService>();
			taskService.Start();
			m_Logger.Info("Scheduler started");
		}

		private void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			var handleError = new HandleErrorAttribute();
			handleError.View = "500";
			filters.Add(handleError);

			var sessionLessFilter = new SessionStateAttribute(System.Web.SessionState.SessionStateBehavior.Disabled);
			filters.Add(sessionLessFilter);
		}
	}
}
