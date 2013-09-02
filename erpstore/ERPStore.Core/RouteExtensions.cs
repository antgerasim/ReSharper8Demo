using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

using Microsoft.Practices.Unity;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc.Ajax;

namespace ERPStore
{
	/// <summary>
	/// Méthodes d'extensions pour la determination des routes
	/// </summary>
	public static class RouteExtensions
	{
		/// <summary>
		/// Resolution du nom de la route indiquée en fonction du contexte pour l'objet HtmlHelper
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="routeName">Name of the route.</param>
		/// <returns></returns>
		public static string ResolveRouteName(this HtmlHelper htmlHelper, string routeName)
		{
			return ResolveRouteName(htmlHelper.ViewContext.HttpContext, routeName);
		}

		/// <summary>
		/// Résolution du nom de la route indiquée en fonction du contexte pour l'objet AjaxHelper
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="routeName">Name of the route.</param>
		/// <returns></returns>
		public static string ResolveRouteName(this AjaxHelper helper, string routeName)
		{
			return ResolveRouteName(helper.ViewContext.HttpContext, routeName);
		}

		/// <summary>
		/// Résolution du nom de la route indiquée en fonction du contexte pour l'objet UrlHelper
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="routeName">Name of the route.</param>
		/// <returns></returns>
		public static string ResolveRouteName(this UrlHelper helper, string routeName)
		{
			return ResolveRouteName(helper.RequestContext.HttpContext, routeName);
		}

		/// <summary>
		/// Résolution du nom de la route indiquée en fonction du contexte pour l'objet HttpContextBase
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="routeName">Name of the route.</param>
		/// <returns></returns>
		public static string ResolveRouteName(this HttpContextBase context, string routeName)
		{
			var currentLanguage = (string)context.Items["language"];
			if (currentLanguage != null
                && !currentLanguage.Equals("fr", StringComparison.InvariantCultureIgnoreCase))
			{
				routeName = string.Format("{0}-{1}", routeName, currentLanguage);
			}
			return routeName;
		}

		public static MvcForm BeginERPStoreRouteForm(this HtmlHelper htmlHelper, string routeName, FormMethod method)
		{
			routeName = htmlHelper.ResolveRouteName(routeName);
			return htmlHelper.BeginRouteForm(routeName, method);
		}

		public static MvcForm BeginERPStoreRouteForm(this HtmlHelper htmlHelper, string routeName, FormMethod method, object htmlAttributes)
		{
			routeName = htmlHelper.ResolveRouteName(routeName);
			return htmlHelper.BeginRouteForm(routeName, method, htmlAttributes);
		}

		public static MvcForm BeginERPStoreRouteForm(this HtmlHelper htmlHelper, string routeName, object routeValues, FormMethod method)
		{
			routeName = htmlHelper.ResolveRouteName(routeName);
			return htmlHelper.BeginRouteForm(routeName, routeValues, method);
		}

		public static MvcForm BeginERPStoreRouteForm(this AjaxHelper helper, string routeName, AjaxOptions options)
		{
			routeName = helper.ResolveRouteName(routeName);
			return helper.BeginRouteForm(routeName, options);
		}

		public static string RouteERPStoreLink(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues)
		{
			routeName = htmlHelper.ResolveRouteName(routeName); 
			return htmlHelper.RouteLink(linkText, routeName, routeValues).ToHtmlString();
		}

		public static string RouteERPStoreLink(this HtmlHelper htmlHelper, string linkText, string routeName, RouteValueDictionary routeValues)
		{
			routeName = htmlHelper.ResolveRouteName(routeName);
			return htmlHelper.RouteLink(linkText, routeName, routeValues, routeValues).ToHtmlString();
		}

		public static string RouteERPStoreLink(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues, object htmlAttributes)
		{
			routeName = htmlHelper.ResolveRouteName(routeName);
			return htmlHelper.RouteLink(linkText, routeName, routeValues, htmlAttributes).ToHtmlString();
		}

		public static string RouteERPStoreLink(this HtmlHelper htmlHelper, string linkText, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			routeName = htmlHelper.ResolveRouteName(routeName);
			return htmlHelper.RouteLink(linkText, routeName, routeValues, htmlAttributes).ToHtmlString();
		}

		public static string RouteERPStoreLink(this HtmlHelper htmlHelper, string linkText, string routeName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes)
		{
			routeName = htmlHelper.ResolveRouteName(routeName);
			return htmlHelper.RouteLink(linkText, routeName, protocol, hostName, fragment, routeValues, htmlAttributes).ToHtmlString();
		}

		public static string RouteERPStoreLink(this HtmlHelper htmlHelper
			, string linkText
			, string routeName
			, string protocol
			, string hostName
			, string fragment
			, RouteValueDictionary routeValues
			, IDictionary<string, object> htmlAttributes)
		{
			routeName = htmlHelper.ResolveRouteName(routeName);
			string result = "#";
			try
			{
				result = htmlHelper.RouteLink(linkText, routeName, protocol, hostName, fragment, routeValues, htmlAttributes).ToHtmlString();
			}
			catch (Exception ex)
			{
                var logger = DependencyResolver.Current.GetService<Logging.ILogger>();
				logger.Error(ex);
			}
			return result;
		}

		public static string RouteERPStoreUrl(this UrlHelper helper, string routeName)
		{
			return helper.RouteERPStoreUrl(routeName, null);
		}

		public static string RouteERPStoreUrl(this UrlHelper helper, string routeName, object routeValues)
		{
			var resolvedRoute = helper.ResolveRouteName(routeName);
			string result = "#";
            while (true)
            {
                try
                {
                    result = helper.RouteUrl(resolvedRoute, routeValues);
                    break;
                }
                catch (Exception ex)
                {
                    if (resolvedRoute.Equals(routeName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        var logger = DependencyResolver.Current.GetService<Logging.ILogger>();
                        logger.Error(ex);
                        break;
                    }
                    resolvedRoute = routeName;
                }
            }
			return result;
		}

	}
}
