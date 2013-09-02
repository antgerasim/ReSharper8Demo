using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

using ERPStore.Html;
using System.Web.Routing;
using System.Security.Cryptography;

namespace ERPStore.Controllers
{
	/// <summary>
	/// Controlleur des pages "statiques"
	/// </summary>
	// [HandleError(View = "500")]
	public class HomeController : StoreController
	{
		public HomeController(Services.IEmailerService emailerService
			, Services.IAccountService accountService
			, Services.IEventPublisher eventPublisher)
		{
			this.EmailerService = emailerService;
			this.AccountService = accountService;
			this.EventPublisher = eventPublisher;
		}

		protected Services.IEmailerService EmailerService { get; set; }

		protected Services.IAccountService AccountService { get; private set; }

		protected Services.IEventPublisher EventPublisher { get; private set; }

		/// <summary>
		/// Page d'accueil par defaut
		/// </summary>
		/// <returns></returns>
		[ActionFilters.TrackerActionFilter]
		public ActionResult Index()
		{
			ViewData.Model = ERPStoreApplication.WebSiteSettings;
			return View("Index");
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult CatchAll()
		{
			ViewData.Model = ERPStoreApplication.WebSiteSettings;
			if (Request.RawUrl == null 
				|| Request.RawUrl.ToLower().IndexOf("default.aspx") != -1
				|| Request.RawUrl == "/"
				|| Request.RawUrl.IndexOf("/?") != -1
				)
			{
				return View("Index");	
			}

			EventPublisher.Publish(new Models.Events.Error404Event()
			{
				Date = DateTime.Now,
				IP = Request.UserHostAddress,
				Url = Request.RawUrl,
				UserAgent = Request.UserAgent,
				Cookie = (Request.Headers != null) ? Request.Headers["Cookie"] : string.Empty,
				Referer = Request.UrlReferrer,
				ApplicationPath = Request.ApplicationPath,
				Method = Request.HttpMethod,
			});

			return View("404");
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult TermsAndConditions()
		{
			ViewData.Model = ERPStoreApplication.WebSiteSettings;
			return View();
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult LegalInformation()
		{
			ViewData.Model = ERPStoreApplication.WebSiteSettings;
			return View();
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult Help()
		{
			ViewData.Model = ERPStoreApplication.WebSiteSettings;
			return View();
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult About()
		{
			ViewData.Model = ERPStoreApplication.WebSiteSettings;
			return View();
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult Error()
		{
			Response.StatusCode = 500;
			return View("500");
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult Contact()
		{
			var contactInfo = new Models.ContactInfo();
			ViewData.Model = contactInfo;
			if (User.Identity.IsAuthenticated)
			{
				var user = User.GetUserPrincipal().CurrentUser;
				contactInfo.Email = user.Email;
				contactInfo.FullName = user.FullName;
				contactInfo.PhoneNumber = user.PhoneNumber;
				if (user.Corporate != null)
				{
					contactInfo.CorporateName = user.Corporate.Name;
				}
			}
			return View();
		}

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AjaxContact(Models.ContactInfo contactInfo)
        {
            if (contactInfo == null)
            {
                return PartialView("_ContactForm");
            }
            if (ModelState.IsValid)
            {
                // Envoyer le mail de contact
                try
                {
                    AccountService.SaveContactMessage(contactInfo);
                    EmailerService.SendContactNeeded(contactInfo);
                }
                catch (Exception ex)
                {
                    LogError(Logger, ex);
                }
            }
            else
            {
                return PartialView("_ContactForm");
            }
            ViewData.Model = contactInfo;
            return PartialView("_ContactMessageSent");
        }


		[AcceptVerbs(HttpVerbs.Post)]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Contact(Models.ContactInfo contactInfo)
		{
			if (contactInfo == null)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}
			if (ModelState.IsValid)
			{
				// Envoyer le mail de contact
				try
				{
					AccountService.SaveContactMessage(contactInfo);
					EmailerService.SendContactNeeded(contactInfo);
					ViewData["IsSent"] = true;
				}
				catch (Exception ex)
				{
					LogError(Logger, ex);
				}
			}
			ViewData.Model = contactInfo;
			return View();
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult StaticPage(string viewName)
		{
			var fileName = Server.MapPath(string.Format("~/views/static/{0}.aspx", viewName));
			if (!System.IO.File.Exists(fileName))
			{
				viewName = viewName + ".cshtml";
			}
			else
			{
				viewName = viewName + ".aspx";
			}

			return View(string.Format("~/views/static/{0}", viewName));
		}

		public ActionResult HideReferer(string url)
		{
			if (url.IsNullOrTrimmedEmpty())
			{
				url = Request["url"];
				if (url.IsNullOrTrimmedEmpty())
				{
					return new EmptyResult();
				}
			}

            var bts = System.Convert.FromBase64String(url);
			var decodedUrl = System.Text.Encoding.UTF8.GetString(bts);
			var sb = new System.Text.StringBuilder();
			sb.Append("<html>");
			sb.Append("<head>");
			sb.AppendFormat("<meta http-equiv=\"refresh\" content=\"0;URL={0}\" />", decodedUrl);
			sb.Append("</head>");
			sb.Append("<body>");
			sb.Append("</body>");
			sb.Append("</html>");
			return Content(sb.ToString(), "text/html");
		}

		#region Partial Rendering

		/// <summary>
		/// Shows the header.
		/// </summary>
		/// <param name="viewName">Name of the view.</param>
		/// <returns></returns>
		public ActionResult ShowHeader(string viewName)
		{
			return PartialView( viewName, ERPStoreApplication.WebSiteSettings);
		}

		/// <summary>
		/// Shows the footer.
		/// </summary>
		/// <param name="viewName">Name of the view.</param>
		/// <returns></returns>
		public ActionResult ShowFooter(string viewName)
		{
			return PartialView(viewName, ERPStoreApplication.WebSiteSettings);
		}

		/// <summary>
		/// Shows the menu.
		/// </summary>
		/// <param name="viewName">Name of the view.</param>
		/// <param name="routeData">The route data.</param>
		/// <returns></returns>
		public ActionResult ShowMenu(string viewName, System.Web.Routing.RouteData routeData)
		{
			var enabledList = ERPStoreApplication.WebSiteSettings.Menus.Where(i => i.Enabled);
			return PartialView(viewName, enabledList.ToList());
		}

		#endregion

	}
}
