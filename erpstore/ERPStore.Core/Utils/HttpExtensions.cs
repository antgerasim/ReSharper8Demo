using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;
using System.Collections.Specialized;

using Microsoft.Practices.Unity;
using System.Web.Security;
using System.Web.Mvc;

namespace ERPStore
{
	public static class HttpExtensions
	{
		/// <summary>
		/// Return the first the or default cached item.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="cache">The cache.</param>
		/// <param name="predicate">The predicate.</param>
		/// <returns></returns>
		public static T FirstOrDefault<T>(this System.Web.Caching.Cache cache, Func<T, bool> predicate)
		{
			var list = System.Web.HttpContext.Current.Cache.Cast<System.Collections.DictionaryEntry>()
				.Where(i => i.Value.GetType() == typeof(T))
				.Select(i => i.Value).Cast<T>();

			return list.FirstOrDefault(predicate);
		}

		/// <summary>
		/// Gets the list of.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="cache">The cache.</param>
		/// <param name="predicate">The predicate.</param>
		/// <returns></returns>
		public static IEnumerable<T> GetListOf<T>(this System.Web.Caching.Cache cache, Func<T, bool> predicate)
		{
			var list = System.Web.HttpContext.Current.Cache.Cast<System.Collections.DictionaryEntry>()
						.Where(i => i.Value.GetType() == typeof(T))
						.Select(i => i.Value).Cast<T>();

			return list.Where(predicate);
		}

		/// <summary>
		/// Gets the list of.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="cache">The cache.</param>
		/// <returns></returns>
		public static IQueryable<T> GetListOf<T>(this System.Web.Caching.Cache cache)
		{
			var list = System.Web.HttpContext.Current.Cache.Cast<System.Collections.DictionaryEntry>()
						.Where(i => i.Value.GetType() == typeof(T))
						.Select(i => i.Value).Cast<T>();

			return list.AsQueryable();
		}

		/// <summary>
		/// Récupération ou création de l'id unique du visiteur
		/// </summary>
		/// <param name="ctx">The HttpContext.</param>
		/// <param name="isNewVisitor">if set to <c>true</c> [is new visitor].</param>
		/// <returns>Id unique du visiteur</returns>
		/// <remarks>
		/// Le cookie dure 60 jours par defaut
		/// </remarks>
		public static string GetOrCreateVisitorId(this System.Web.HttpContextBase ctx, out bool isNewVisitor)
		{
			var visitorCookie = ctx.Request.Cookies["erpstorevid"];
			string visitorId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 30);
			isNewVisitor = (visitorCookie == null);
			bool needResponse = false;
			var tld = ctx.GetDomainAndTLD();
			SetupVisitorCookie(ref visitorCookie, out visitorId, out needResponse, tld);
			if (needResponse)
			{
				ctx.Response.Cookies.Add(visitorCookie);
			}
			return visitorId;
		}

		public static string GetDomainAndTLD(this System.Web.HttpContextBase ctx)
		{
			var tokens = ctx.Request.Url.Host.Split('.');
			return GetDomainAndTLD(tokens);
		}

		public static string GetDomainAndTLD(this System.Web.HttpContext ctx)
		{
			var tokens = ctx.Request.Url.Host.Split('.');
			return GetDomainAndTLD(tokens);
		}

		public static string GetDomainAndTLD(this string[] tokens)
		{
			if (tokens.Length < 2)
			{
				return null;
			}
			// TODO : Checker l'ip directe
			var result = string.Format("{0}.{1}", tokens[tokens.Length - 2], tokens[tokens.Length - 1]);
			return result;
		}

		/// <summary>
		/// Récupération ou création de l'id unique du visiteur
		/// </summary>
		/// <param name="ctx">The HttpContext.</param>
		/// <param name="isNewVisitor">if set to <c>true</c> [is new visitor].</param>
		/// <returns>Id unique du visiteur</returns>
		/// <remarks>
		/// Le cookie dure 60 jours par defaut
		/// </remarks>
		public static string GetOrCreateVisitorId(this System.Web.HttpContext ctx, out bool isNewVisitor)
		{
			var visitorCookie = ctx.Request.Cookies["erpstorevid"];
			isNewVisitor = (visitorCookie == null);
			string visitorId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 30); 
			bool needResponse = false;
			SetupVisitorCookie(ref visitorCookie, out visitorId, out needResponse, ctx.GetDomainAndTLD());
			if (needResponse)
			{
				ctx.Response.Cookies.Add(visitorCookie);
			}
			return visitorId;
		}

		public static string GetControllerActionName(this System.Web.HttpContextBase context)
		{
			return GetControllerActionName(context.Request.RawUrl);
		}

		public static string GetControllerActionName(this string url)
		{
			if (url.IsNullOrTrimmedEmpty())
			{
				return "home/index";
			}
			var fakeContext = new ERPStore.Web.MockHttpContext(url);
			var currentRoute = RouteTable.Routes.GetRouteData(fakeContext); // as System.Web.Routing.RouteData;
			string controller = currentRoute.Values["controller"].ToString();
			string action = currentRoute.Values["action"].ToString();
			var controllerActionName = string.Format("{0}/{1}", controller, action);
			return controllerActionName.ToLower();
		}


		private static void SetupVisitorCookie(ref System.Web.HttpCookie cookie, out string vid, out bool needResponse, string domainAndTld)
		{
			needResponse = false;
			if (cookie == null)
			{
				cookie = new System.Web.HttpCookie("erpstorevid");
				cookie.Expires = DateTime.Now.AddDays(60);
				cookie.Path = "/";
				var cookieDomain = ERPStore.Configuration.ConfigurationSettings.AppSettings["cookieDomain"];
				if (!cookieDomain.IsNullOrTrimmedEmpty())
				{
					cookie.Domain = cookieDomain;
				}

				vid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 30);
				var value = string.Format("{0}|{1:yyyy|MM|dd}", vid, DateTime.Now);

				cookie.Value = value;
				needResponse = true;
			}
			// Si le cookie expire dans moins de 5 jours, alors on ajoute 60 jours
			else
			{
				var tokens = cookie.Value.Split('|');
				DateTime expiration = DateTime.Today;
				if (tokens.Length == 4)
				{
					vid = tokens[0];
					try
					{
						expiration = new DateTime(Convert.ToInt32(tokens[1]), Convert.ToInt32(tokens[2]), Convert.ToInt32(tokens[3]));
					}
					catch
					{
						expiration = DateTime.Today;
					}
				}
				else
				{
					if (!tokens[0].IsNullOrTrimmedEmpty())
					{
						vid = tokens[0];
					}
					else
					{
						vid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 30);
					}
				}
				if (expiration.AddDays(-5) <= DateTime.Today)
				{
					cookie.Expires = DateTime.Today.AddDays(60);
					cookie.Value = string.Format("{0}|{1:yyyy|MM|dd}", vid, cookie.Expires);
					needResponse = true;
				}
			}
		}

		public static string GetSearchKeywords(this HttpContext context)
		{
			if (context.Request["qr"] != null)
			{
				return context.Request["qr"];
			}
			return context.Request.UrlReferrer.GetSearchKeywords();
		}

		public static string GetSearchKeywords(this HttpContextBase context)
		{
			if (context.Request["qr"] != null)
			{
				return context.Request["qr"];
			}
			return context.Request.UrlReferrer.GetSearchKeywords();
		}

		public static string GetSearchKeywords(this Uri referer)
		{
			if (referer == null)
			{
				return null;
			}

			/* Recherche du domaine */
			var domain = referer.Host;
			NameValueCollection prms = null;
			try
			{
				prms = System.Web.HttpUtility.ParseQueryString(referer.Query.ToLower());
			}
			catch (Exception ex)
			{
				var logger = DependencyResolver.Current.GetService<Logging.ILogger>();
				logger.Error(ex);
			}

			if (prms == null)
			{
				return null;
			}

			string k = null;
			if (domain.IndexOf("google") > -1)
			{
				k = prms["q"];
			}
			else if (domain.IndexOf("yahoo") > -1)
			{
				k = prms["p"];
			}
			else if (domain.IndexOf("msn") > -1)
			{
				k = prms["q"];
			}
			else if (domain.IndexOf("voila") > -1)
			{
				k = prms["kw"];
			}
			else if (domain.IndexOf("aliceadsl") > -1)
			{
				k = prms["qs"];
			}
			else if (domain.IndexOf("club-internet") > -1)
			{
				k = prms["q"];
			}
			else if (domain.IndexOf("sedoparking") > -1)
			{
				k = prms["keyword"];
			}
			else if (domain.IndexOf("aol") > -1)
			{
				k = prms["query"];
			}
			else if (domain.IndexOf("lycos") > -1)
			{
				k = prms["query"];
			}
			else if (domain.IndexOf("ask") > -1)
			{
				k = prms["q"];
			}
			else if (domain.IndexOf("altavista") > -1)
			{
				k = prms["q"];
			}
			else if (domain.IndexOf("mozbot") > -1)
			{
				k = prms["q"];
			}
			else if (domain.IndexOf("localhost") > -1)
			{
				k = "meuleuse";
				// k = prms["q"];
			}
			else if (domain.IndexOf("leguide") > -1)
			{
				k = System.Web.HttpUtility.UrlDecode(prms["ms"]);
				// k += " | " + prms["slkw"];
			}
			else if (domain.IndexOf("webmarchand.com") > -1)
			{
				if (referer.PathAndQuery.IndexOf("/") != -1)
				{
					var parts = referer.PathAndQuery.Split('/').ToList();
					foreach (var part in parts)
					{
						if (part.Equals("mot"))
						{
							k = parts[parts.IndexOf(part) + 1];
							k = k.Replace("_", " ");
							break;
						}
					}
				}
			}
			else if (domain.IndexOf("shopping.cherchons.com") > -1)
			{
				k = prms["parameter"];
			}
			else if (domain.IndexOf("cherchons.com") > -1)
			{
				k = prms["ms"];
			}
			else if (domain.IndexOf("bing.") > -1)
			{
				k = prms["q"];
			}

			if (k != null)
			{
				return k.Trim();
			}
			return null;

		}

		/// <summary>
		/// Indique si l'utilisateur en cours est associé à une société
		/// </summary>
		/// <param name="principal">The principal.</param>
		/// <returns>
		/// 	<c>true</c> if the specified principal has corporate; otherwise, <c>false</c>.
		/// </returns>
		public static bool HasCorporate(this System.Security.Principal.IPrincipal principal)
		{
			var user = principal.GetUserPrincipal().CurrentUser;
			return user != null && user.Corporate != null;
		}


		/// <summary>
		/// Ajout d'un cookie securisé contenant l'id du user
		/// </summary>
		/// <param name="response">The response.</param>
		/// <param name="userId">The user id.</param>
		/// <param name="rememberMe">The remember me.</param>
		public static void AddAuthenticatedCookie(this System.Web.HttpResponseBase response, int userId, bool? rememberMe)
		{
			var expirationDays = Convert.ToInt32(ERPStore.Configuration.ConfigurationSettings.AppSettings["cookieExpirationDays"] ?? "30");
			// Creation d'un ticket encrypté avec le machineKey du serveur en cours
			// La période de validité du cookie est de 30 jours par defaut
			var ticket = new FormsAuthenticationTicket(
				2,
				userId.ToString(),
				DateTime.Now,
				// TODO : indiquer cette valeur dans les paramètres
				DateTime.Now.AddDays(expirationDays),
				rememberMe.GetValueOrDefault(false),
				"",
				FormsAuthentication.FormsCookiePath);

			var ticketEncrypted = FormsAuthentication.Encrypt(ticket);

			var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketEncrypted);
			cookie.HttpOnly = true;
			cookie.Path = FormsAuthentication.FormsCookiePath;
			cookie.Secure = FormsAuthentication.RequireSSL;
			// TODO : Utiliser le paramètre de domaine (a tester)
			var cookieDomain = ERPStore.Configuration.ConfigurationSettings.AppSettings["cookieDomain"]
								?? FormsAuthentication.CookieDomain;
			if (cookieDomain != null)
			{
				cookie.Domain = cookieDomain;
			}
			cookie.Expires = ticket.Expiration;

			response.Cookies.Remove(FormsAuthentication.FormsCookieName); 
			response.Cookies.Add(cookie);
		}

		public static void SignOut(this System.Web.HttpContextBase context)
		{
			var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "empty");
			cookie.HttpOnly = true;
			cookie.Path = FormsAuthentication.FormsCookiePath;
			cookie.Secure = FormsAuthentication.RequireSSL;
			var cookieDomain = ERPStore.Configuration.ConfigurationSettings.AppSettings["cookieDomain"]
								?? FormsAuthentication.CookieDomain;
			if (cookieDomain != null)
			{
				cookie.Domain = cookieDomain;
			}
			cookie.Expires = DateTime.Now.AddDays(-5);
			context.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
			context.Response.Cookies.Add(cookie);
		}
	}
}