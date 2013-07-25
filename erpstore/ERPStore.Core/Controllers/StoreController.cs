using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

using Microsoft.Practices.Unity;

namespace ERPStore.Controllers
{
	public abstract class StoreController : Controller
	{
		public StoreController()
		{


            // comments 
		}

		[Dependency]
		public Services.ISearchOptimizationService SearchOptimizationService { get; set; }

		[Dependency]
		public Logging.ILogger Logger { get; set; }

		[Dependency]
		public Services.ILocalizationService LocalizationService { get; set; }

		[Dependency]
		public Services.IEventPublisher EventPublisherService { get; set; }

		protected Models.WebSiteSettings SiteSettings
		{
			get
			{
				return ERPStoreApplication.WebSiteSettings;
			}
		}

		protected override void Initialize(System.Web.Routing.RequestContext requestContext)
		{
			base.Initialize(requestContext);
			LocalizationService.Initialize(this.HttpContext);
		}

		protected override ViewResult View(IView view, object model)
		{
			var result = base.View(view, model);
			try
			{
				SearchOptimizationService.GenerateMetasInformations(this.HttpContext, ViewData.Model);
			}
			catch(Exception ex)
			{
				LogError(Logger, ex);
			}
			return result;
		}

		protected override ViewResult View(string viewName, string masterName, object model)
		{
			var result = base.View(viewName, masterName, model);
			try
			{
				SearchOptimizationService.GenerateMetasInformations(this.HttpContext, ViewData.Model);
			}
			catch (Exception ex)
			{
				LogError(Logger, ex);
			}
			return result;
		}

        protected RedirectToRouteResult RedirectToERPStoreRoute(string routeName)
        {
			return RedirectToERPStoreRoute(routeName, null);
        }

        protected RedirectToRouteResult RedirectToERPStoreRoute(string routeName, object routeValues)
        {
			routeName = this.HttpContext.ResolveRouteName(routeName);
			var result = RedirectToRoute(routeName, routeValues);

			if (HttpContext.Request["trace"] == "off")
			{
				return result;
			}
			var @event = new Models.Events.TrackerEvent()
			{
				HttpContextBase = HttpContext,
				ActionResult = result,
				ControllerName = this.GetType().Name,
			};

			EventPublisherService.Publish(@event);

            return result;
        }

		protected override RedirectResult Redirect(string url)
		{
			var result = base.Redirect(url);

			if (HttpContext.Request["trace"] == "off")
			{
				return result;
			}
			var @event = new Models.Events.TrackerEvent()
			{
				HttpContextBase = HttpContext,
				ActionResult = result,
				ControllerName = this.GetType().Name,
			};

			EventPublisherService.Publish(@event);

			return result;
		}

		protected int GetPageId(int? page)
		{
			var result = Math.Max(0,page.GetValueOrDefault(0) - 1);
			return result;
		}

		protected void LogError(Logging.ILogger logger, Exception ex)
		{
			ex.Data.Add("Message", ex.Message);
			ex.Data.Add("machineName", Environment.MachineName);
			ex.Data.Add("host", Request.Url.Host);
			// ex.Data.Add("visitorId", visitorId);
			ex.Data.Add("userHostAddress", Request.UserHostAddress);
			ex.Data.Add("userHostName", Request.UserHostName);
			ex.Data.Add("url", Request.RawUrl);
			ex.Data.Add("referer", Request.UrlReferrer);
			ex.Data.Add("applicationPath", Request.ApplicationPath);
			ex.Data.Add("user-agent", Request.Headers["User-Agent"]);
			ex.Data.Add("cookie", Request.Headers["Cookie"]);
			if (Request.Form.Count > 0)
			{
				ex.Data.Add("begin-form", "-----------------------");
				foreach (var item in Request.Form.AllKeys)
				{
					if (ex.Data.Contains(item))
					{
						continue;
					}
					ex.Data.Add(item, Request.Form[item]);
				}
				ex.Data.Add("end-form", "-----------------------");
			}
			if (User.Identity.IsAuthenticated
				&& User.Identity is Models.UserPrincipal)
			{
				ex.Data.Add("user", User.GetUserPrincipal().CurrentUser.Login);
			}
			else
			{
				ex.Data.Add("user", "anonymous");
			}
			logger.Error(ex);
		}

		protected void MovedPermanently(string location)
		{
			Response.Clear();
			Response.StatusCode = 301;
			Response.StatusDescription = "Moved Permanently";
			Response.AppendHeader("Location", location);
			Response.End();
		}
	}
}
