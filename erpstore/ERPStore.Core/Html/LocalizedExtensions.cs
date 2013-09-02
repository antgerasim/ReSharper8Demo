using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.Mvc.Ajax;
using System.Web;

using Microsoft.Practices.Unity;

namespace ERPStore.Html
{
	public static class LocalizedExtensions
	{
		private static string[] SupportedLanguageNames = new[] { "fr", "en" }; 

		public static MvcForm BeginLocalizedRouteForm(this HtmlHelper htmlHelper, string routeName, FormMethod method)
		{
			var currentLanguage = GetCurrentLanguage(htmlHelper);
			routeName = string.Format("{0}-{1}", routeName, currentLanguage);
			return htmlHelper.BeginRouteForm(routeName, method);
		}

		public static MvcForm BeginLocalizedRouteForm(this HtmlHelper htmlHelper, string routeName, FormMethod method, object htmlAttributes)
		{
			var currentLanguage = GetCurrentLanguage(htmlHelper);
			routeName = string.Format("{0}-{1}", routeName, currentLanguage);
			return htmlHelper.BeginRouteForm(routeName, method, htmlAttributes);
		}

		public static MvcForm BeginLocalizedRouteForm(this HtmlHelper htmlHelper, string routeName, object routeValues,  FormMethod method)
		{
			var currentLanguage = GetCurrentLanguage(htmlHelper);
			routeName = string.Format("{0}-{1}", routeName, currentLanguage);
			return htmlHelper.BeginRouteForm(routeName, routeValues, method);
		}

		public static MvcForm BeginLocalizedRouteForm(this AjaxHelper helper, string routeName, AjaxOptions options)
		{
			var currentLanguage = GetCurrentLanguage(helper);
			routeName = string.Format("{0}-{1}", routeName, currentLanguage);
			return helper.BeginRouteForm(routeName, options);
		}

		public static string RouteLocalizedLink(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues)
		{
			var currentLanguage = GetCurrentLanguage(htmlHelper);
			routeName = string.Format("{0}-{1}", routeName, currentLanguage);
			return htmlHelper.RouteLink(linkText, routeName, routeValues).ToHtmlString();
		}

		public static string RouteLocalizedLink(this HtmlHelper htmlHelper, string linkText, string routeName, RouteValueDictionary routeValues)
		{
			var currentLanguage = GetCurrentLanguage(htmlHelper);
			routeName = string.Format("{0}-{1}", routeName, currentLanguage);
			return htmlHelper.RouteLink(linkText, routeName, routeValues, routeValues).ToHtmlString();
		}

		public static string RouteLocalizedLink(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues, object htmlAttributes)
		{
			var currentLanguage = GetCurrentLanguage(htmlHelper);
			routeName = string.Format("{0}-{1}", routeName, currentLanguage);
			return htmlHelper.RouteLink(linkText, routeName, routeValues, htmlAttributes).ToHtmlString();
		}

		public static string RouteLocalizedLink(this HtmlHelper htmlHelper, string linkText, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			var currentLanguage = GetCurrentLanguage(htmlHelper);
			routeName = string.Format("{0}-{1}", routeName, currentLanguage);
			return htmlHelper.RouteLink(linkText, routeName, routeValues, htmlAttributes).ToHtmlString();
		}

		public static string RouteLocalizedLink(this HtmlHelper htmlHelper, string linkText, string routeName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes)
		{
			var currentLanguage = GetCurrentLanguage(htmlHelper);
			routeName = string.Format("{0}-{1}", routeName, currentLanguage);
			return htmlHelper.RouteLink(linkText, routeName, protocol, hostName, fragment, routeValues, htmlAttributes).ToHtmlString();
		}

		public static string RouteLocalizedLink(this HtmlHelper htmlHelper
			, string linkText
			, string routeName
			, string protocol
			, string hostName
			, string fragment
			, RouteValueDictionary routeValues
			, IDictionary<string, object> htmlAttributes)
		{
			var currentLanguage = GetCurrentLanguage(htmlHelper);
			routeName = string.Format("{0}-{1}", routeName, currentLanguage);
			string result = "#";
			try
			{ 
				result = htmlHelper.RouteLink(linkText, routeName, protocol, hostName, fragment, routeValues, htmlAttributes).ToHtmlString();
			}
			catch(Exception ex)
			{
				var logger = ERPStoreApplication.Container.Resolve<Logging.ILogger>();
				logger.Error(ex);
			}
			return result;
		}

		public static string RouteLocalizedUrl(this HtmlHelper helper, string routeName, object routeValues)
		{
			var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
			var currentLanguage = GetCurrentLanguage(helper);
			routeName = string.Format("{0}-{1}", routeName, currentLanguage);
			string result = "#";
			try
			{
				result = urlHelper.RouteUrl(routeName, routeValues);
			}
			catch (Exception ex)
			{
				var logger = ERPStoreApplication.Container.Resolve<Logging.ILogger>();
				logger.Error(ex);
			}
			return result;
		}

		public static string RouteLocalizedUrl(this UrlHelper helper, string routeName, object routeValues)
		{
			var urlHelper = new UrlHelper(helper.RequestContext);
			var currentLanguage = GetCurrentLanguage(helper);
			routeName = string.Format("{0}-{1}", routeName, currentLanguage);
			string result = "#";
			try
			{
				result = urlHelper.RouteUrl(routeName, routeValues);
			}
			catch(Exception ex)
			{
				var logger = ERPStoreApplication.Container.Resolve<Logging.ILogger>();
				logger.Error(ex);
			}
			return result;
		}

		public static void MapLocalizedRoute(this RouteCollection routes, string name, Dictionary<string, string> url)
		{
			MapLocalizedRoute(routes, name, url, null, null);
		}

		public static void MapLocalizedRoute(this RouteCollection routes, string name, Dictionary<string, string> url, object defaults)
		{
			MapLocalizedRoute(routes, name, url, defaults, null);
		}

		public static void MapLocalizedRoute(this RouteCollection routes, string name, Dictionary<string, string> url, string[] namespaces)
		{
			MapLocalizedRoute(routes, name, url, null, null, namespaces);
		}

		public static void MapLocalizedRoute(this RouteCollection routes, string name, Dictionary<string, string> url, object defaults, object constraints)
		{
			MapLocalizedRoute(routes, name, url, defaults, constraints, null);
		}

		public static void MapLocalizedRoute(this RouteCollection routes, string name, Dictionary<string, string> url, object defaults, string[] namespaces)
		{
			MapLocalizedRoute(routes, name, url, defaults, null, namespaces);
		}

		public static void MapLocalizedRoute(this RouteCollection routes, string name, Dictionary<string, string> urls, object defaults, object constraints, string[] namespaces)
		{
			foreach (KeyValuePair<string, string> item in urls)
			{	
				var routeName = string.Format("{0}-{1}", name, item.Key);

				if (routes.GetLocalizedRoutes().Any(i => i.LocalizedName.Equals(routeName, StringComparison.InvariantCultureIgnoreCase)))
				{
					continue;
				}

				routes.Add("test", new Route("xxx", null));

				var route = routes.MapERPStoreRoute(
					routeName
					, item.Value
					, defaults 
					, constraints 
					, namespaces);

				route.Defaults.Add("language", item.Key);
				route.Defaults.Add("name", name);
				route.Defaults.Add("localizedName", routeName);
			}
		}

		internal static string GetCurrentLanguage(this AjaxHelper helper)
		{
			return GetCurrentLanguage(helper.ViewContext.HttpContext, helper.ViewContext.RouteData);
		}

		internal static string GetCurrentLanguage(this UrlHelper helper)
		{
			return GetCurrentLanguage(helper.RequestContext.HttpContext, helper.RequestContext.RouteData);
		}

		internal static string GetCurrentLanguage(this HtmlHelper helper)
		{
			return GetCurrentLanguage(helper.ViewContext.RequestContext.HttpContext, helper.ViewContext.RouteData);
		}

		internal static string GetCurrentLanguage(HttpContextBase context, RouteData routeData)
		{
			if (context.Items != null 
				&& context.Items["language"] != null)
			{
				return (string) context.Items["language"];
			}

			return "fr";
		}

		public static IEnumerable<LocalizedRoute> GetLocalizedRoutes(this RouteCollection routes)
		{
			foreach (var item in routes)
			{
				if (item is Route)
				{
					var localizedRoute = new LocalizedRoute(item as Route);
					if (localizedRoute.Language != null)
					{
						yield return localizedRoute;
					}
				}
			}
			yield break;
		}

	}
}
