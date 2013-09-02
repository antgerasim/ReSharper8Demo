using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Routing;
using System.Collections.Specialized;

using ERPStore;

using Microsoft.Practices.Unity;

namespace ERPStore.Html
{
	public static partial class StoreExtensions
	{
		public static void RenderAction<T>(this HtmlHelper helper, Expression<Action<T>> expression)
			where T : Controller
		{
			var call = expression.Body as MethodCallExpression;
			var controllerName = typeof(T).Name.Replace("Controller", string.Empty);
			var actionName = call.Method.Name;

			var parameters = call.Method.GetParameters();
			var routeValues = new RouteValueDictionary();
			if (parameters.Length > 0)
			{
				for (int i = 0; i < parameters.Length; i++)
				{
					var parameter = call.Arguments[i];
					object value = null;
					var constantExpression = parameter as ConstantExpression;
					if (constantExpression != null)
					{
						value = constantExpression.Value;
					}
					else
					{
						var body = Expression.Convert(parameter, typeof(object));
						var exp = Expression.Lambda<Func<object>>(body, new ParameterExpression[0]);
						value = exp.Compile()();
					}
					routeValues.Add(parameters[i].Name, value);
				}
			}
			helper.RenderAction(actionName, controllerName, routeValues);
		}

		public static MvcHtmlString Action<T>(this HtmlHelper helper, Expression<Action<T>> expression)
			where T : Controller
		{
			var call = expression.Body as MethodCallExpression;
			var controllerName = typeof(T).Name.Replace("Controller", string.Empty);
			var actionName = call.Method.Name;

			var parameters = call.Method.GetParameters();
			var routeValues = new RouteValueDictionary();
			if (parameters.Length > 0)
			{
				for (int i = 0; i < parameters.Length; i++)
				{
					var parameter = call.Arguments[i];
					object value = null;
					var constantExpression = parameter as ConstantExpression;
					if (constantExpression != null)
					{
						value = constantExpression.Value;
					}
					else
					{
						var body = Expression.Convert(parameter, typeof(object));
						var exp = Expression.Lambda<Func<object>>(body, new ParameterExpression[0]);
						value = exp.Compile()();
					}
					routeValues.Add(parameters[i].Name, value);
				}
			}
			return helper.Action(actionName, controllerName, routeValues);
		}


		public static string CurrentLanguage(this HtmlHelper helper)
		{
			var language = (string)helper.ViewContext.HttpContext.Items["language"];
			return language ?? "fr";
		}

		public static string HomeHref(this UrlHelper helper)
		{
			var url = helper.RouteERPStoreUrl("Home", null);
			if (helper.RequestContext.HttpContext.Request.Url.Host.IndexOf("localhost") == -1)
			{
				var tokens = helper.RequestContext.HttpContext.Request.Url.Host.Split('.');
				var domainAndTLD = string.Format("{0}.{1}", tokens[tokens.Length - 2], tokens[tokens.Length - 1]);
				var subHost = helper.RequestContext.HttpContext.Request.Url.Host.Replace(domainAndTLD, "").Trim('.');

				if (subHost.IsNullOrTrimmedEmpty()
					|| subHost.Equals("www", StringComparison.InvariantCultureIgnoreCase))
				{
					return string.Format("http://{0}{1}", ERPStoreApplication.WebSiteSettings.DefaultUrl, url);
				}
				else
				{
					return url;
				}
			}
			else
			{
				return url;
			}
		}

		public static MvcHtmlString HomeLink(this HtmlHelper helper)
		{
			string title = "Accueil";
			switch (helper.CurrentLanguage())
			{
				case "en" :
					title = "Home";
					break;
				default:
					break;
			}

			var result = helper.RouteERPStoreLink(title, "Home", null);
			if (helper.ViewContext.HttpContext.Request.Url.Host.IndexOf("localhost") == -1)
			{
				return new MvcHtmlString(result.Replace("href=\"", string.Format("href=\"{0}", ERPStoreApplication.WebSiteSettings.DefaultUrl)));
			}
			else
			{
				return new MvcHtmlString(result);
			}
		}

		public static MvcHtmlString ShowHeader(this HtmlHelper helper)
		{
			return helper.ShowHeader("Header");
		}

		public static MvcHtmlString ShowHeader(this HtmlHelper helper, string viewName)
		{
			return helper.Action<Controllers.HomeController>(i => i.ShowHeader(viewName));
		}

		public static MvcHtmlString ShowFooter(this HtmlHelper helper)
		{
			return helper.ShowFooter("Footer");
		}

		public static MvcHtmlString ShowFooter(this HtmlHelper helper, string viewName)
		{
			return helper.Action<Controllers.HomeController>(i => i.ShowFooter(viewName));
		}

		public static MvcHtmlString AboutLink(this HtmlHelper helper, string title, string siteName)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.ABOUT, new { siteName = siteName }));
		}

		public static string AboutHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.ABOUT, new { siteName = ERPStoreApplication.WebSiteSettings.SiteName });
		}

		public static MvcHtmlString TermsAndConditionsLink(this HtmlHelper helper, string title)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.TERMS_AND_CONDITIONS, new { id = string.Empty }));
		}

		public static string TermsAndConditionsHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.TERMS_AND_CONDITIONS, new { id = string.Empty });
		}

		public static MvcHtmlString LegalInformationLink(this HtmlHelper helper, string title)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.LEGAL_INFORMATION, new { id = string.Empty }));
		}

		public static string LegalInformationHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.LEGAL_INFORMATION, new { id = string.Empty });
		}

		public static MvcHtmlString HelpLink(this HtmlHelper helper, string title)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.HELP, new { id = string.Empty }));
		}

		public static string HelpHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.HELP, new { id = string.Empty });
		}

		public static MvcHtmlString ContactLink(this HtmlHelper helper, string title)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.CONTACT, new { id = string.Empty }));
		}

		public static string ContactHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.CONTACT, new { id = string.Empty });
		}

		public static MvcForm BeginContactFrom(this HtmlHelper helper)
		{
			return helper.BeginERPStoreRouteForm(ERPStoreRoutes.CONTACT, FormMethod.Post);
		}

		/// <summary>
		/// Ajoute le paramètre de localization dans l'url en cours, permet de changer de langue
		/// dans n'importe quelle page
		/// </summary>
		/// <example>
		/// Exemple d'utilisation :
		/// <code>
		/// <![CDATA[
		/// <%=Url.LocalizeToHref("fr")%>
		/// 
		/// si l'url en cours est 
		/// 
		/// http://www.monsite.com/category?page=1
		/// 
		/// ajoute
		/// 
		/// http://www.monsite.com/category?page=1&language=1
		/// 
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="language">The language.</param>
		/// <returns></returns>
		public static string LocalizeToHref(this UrlHelper helper, string language)
		{
			return helper.AddParameter("language", language);
		}

		/// <summary>
		/// Ajoute un parametre et sa valeur dans une URL en tenant compte de sa presence eventuelle
		/// </summary>
		/// <example>
		/// Exemple d'utilisation
		/// <code>
		/// <![CDATA[
		/// 
		/// une url existante est de la forme 
		/// http://domain.com/test?param1=valeur1
		/// 
		/// <a href='<%=Url.AddParameter("param2", "valeur2")%>'>Mon lien</a>
		/// 
		/// Donnera le resultat suivant :
		/// 
		/// http://domain.com/test?param1=valeur1&param2=valeur2
		/// 
		/// <a href='<%=Url.AddParameter("param1", "valeur2")%>'>Mon lien</a>
		/// 
		/// Donnera le resultat suivant :
		/// 
		/// http://domain.com/test?param1=valeur2
		/// 
		/// ]]>
		/// </code>
		/// </example>
		/// <remarks>
		/// "value" est urlencodé par la methode, ne pas passer la valeur deja encodée
		/// </remarks>
		/// <param name="helper">The helper.</param>
		/// <param name="url">The URL.</param>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string AddParameter(this UrlHelper helper, string url, string key, string value)
		{
			return AddParameter(url, key, helper.Encode(value));
		}

		/// <summary>
		/// Ajoute un parametre et sa valeur dans une URL en tenant compte de sa presence eventuelle
		/// </summary>
		/// <example>
		/// Exemple d'utilisation
		/// <code>
		/// <![CDATA[
		/// 
		/// une url existante est de la forme 
		/// http://domain.com/test?param1=valeur1
		/// 
		/// <a href='<%=Url.AddParameter("param2", "valeur2")%>'>Mon lien</a>
		/// 
		/// Donnera le resultat suivant :
		/// 
		/// http://domain.com/test?param1=valeur1&param2=valeur2
		/// 
		/// <a href='<%=Url.AddParameter("param1", "valeur2")%>'>Mon lien</a>
		/// 
		/// Donnera le resultat suivant :
		/// 
		/// http://domain.com/test?param1=valeur2
		/// 
		/// ]]>
		/// </code>
		/// </example>
		/// <remarks>
		/// "value" est urlencodé par la methode, ne pas passer la valeur deja encodée
		/// </remarks>
		/// <param name="helper">The helper.</param>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		/// <returns>Le lien avec le couple key=value</returns>
		public static string AddParameter(this UrlHelper helper, string key, string value)
		{
			var link = helper.RequestContext.HttpContext.Request.Url.PathAndQuery;
			return AddParameter(link, key, helper.Encode(value));
		}

		/// <summary>
		/// Ajoute un parametre et sa valeur dans une URL en tenant compte de sa presence eventuelle
		/// </summary>
		/// <param name="pathAndQuery">The path and query.</param>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		/// <example>
		/// Exemple d'utilisation
		/// <code>
		/// 		<![CDATA[
		/// une url existante est de la forme
		/// http://domain.com/test?param1=valeur1
		/// <a href='<%=Url.AddParameter("param2", "valeur2")%>'>Mon lien</a>
		/// Donnera le resultat suivant :
		/// http://domain.com/test?param1=valeur1&param2=valeur2
		/// <a href='<%=Url.AddParameter("param1", "valeur2")%>'>Mon lien</a>
		/// Donnera le resultat suivant :
		/// http://domain.com/test?param1=valeur2
		/// ]]>
		/// 	</code>
		/// </example>
		/// <remarks>
		/// "value" est urlencodé par la methode, ne pas passer la valeur deja encodée
		/// </remarks>
		public static string AddParameter(this string pathAndQuery, string key, int value)
		{
			return AddParameter(pathAndQuery, key, value.ToString());
		}

		/// <summary>
		/// Ajoute un parametre et sa valeur dans une URL en tenant compte de sa presence eventuelle
		/// </summary>
		/// <param name="pathAndQuery">The path and query.</param>
		/// <param name="encodedKey">The encoded key.</param>
		/// <param name="encodedValue">The HtmlEncoded value.</param>
		/// <returns></returns>
		/// <example>
		/// Exemple d'utilisation
		/// <code>
		/// 		<![CDATA[
		/// une url existante est de la forme
		/// http://domain.com/test?param1=valeur1
		/// <a href='<%=Url.AddParameter("param2", "valeur2")%>'>Mon lien</a>
		/// Donnera le resultat suivant :
		/// http://domain.com/test?param1=valeur1&param2=valeur2
		/// <a href='<%=Url.AddParameter("param1", "valeur2")%>'>Mon lien</a>
		/// Donnera le resultat suivant :
		/// http://domain.com/test?param1=valeur2
		/// ]]>
		/// 	</code>
		/// </example>
		/// <remarks>
		/// "value" est urlencodé par la methode, ne pas passer la valeur deja encodée
		/// </remarks>
		public static string AddParameter(this string pathAndQuery, string encodedKey, string encodedValue)
		{
			string anchor = null;
			string separator = "?";
			pathAndQuery = ExcludeParameter(pathAndQuery, encodedKey, out separator, out anchor);
			pathAndQuery += string.Format("{0}{1}={2}", separator, encodedKey, encodedValue);
			if (!anchor.IsNullOrTrimmedEmpty())
			{
				pathAndQuery += anchor;
			}

			return pathAndQuery;
		}

		/// <summary>
		/// Ajoute un parametre et sa valeur dans une URL en tenant compte de sa presence eventuelle
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		/// <example>
		/// Exemple d'utilisation
		/// <code>
		/// 		<![CDATA[
		/// une url existante est de la forme
		/// http://domain.com/test?param1=valeur1
		/// <a href='<%=Url.AddParameter("param2", "valeur2")%>'>Mon lien</a>
		/// Donnera le resultat suivant :
		/// http://domain.com/test?param1=valeur1&param2=valeur2
		/// <a href='<%=Url.AddParameter("param1", "valeur2")%>'>Mon lien</a>
		/// Donnera le resultat suivant :
		/// http://domain.com/test?param1=valeur2
		/// ]]>
		/// 	</code>
		/// </example>
		/// <remarks>
		/// "value" est urlencodé par la methode, ne pas passer la valeur deja encodée
		/// </remarks>
		public static string AddParameterWithHtmlEncoding(this UrlHelper helper, string key, string value)
		{
			return helper.AddParameter(System.Web.HttpUtility.UrlEncode(key), System.Web.HttpUtility.UrlEncode(value));
		}

		public static string AddParameterWithHtmlEncoding(this string pathAndQuery, string key, string value)
		{
			return AddParameter(pathAndQuery, key, value);
		}


		/// <summary>
		/// Permet de supprimer un parametre et sa valeur dans l'url en cours
		/// </summary>
		/// <example>
		/// Exemple d'utilisation
		/// <code>
		/// <![CDATA[
		/// <%=Url.RemoveParameter("key")%>
		/// 
		/// 
		/// Si l'url en cours est :
		/// http://monsite.com/search?s=test&key=1
		/// 
		/// le resultat sera :
		/// 
		/// http://monsite.com/search?s=test
		/// 
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public static string RemoveParameter(this UrlHelper helper, string key)
		{
			var link = helper.RequestContext.HttpContext.Request.Url.PathAndQuery;
			return RemoveParameter(link, key);
		}

		public static string RemoveParameter(this string pathAndQuery, string key)
		{
			string anchor = null;
			string separator = "?";
			pathAndQuery = ExcludeParameter(pathAndQuery, key, out separator, out anchor);
			if (!anchor.IsNullOrTrimmedEmpty())
			{
				pathAndQuery += anchor;
			}
			return pathAndQuery;
		}

		private static string ExcludeParameter(this string pathAndQuery, string key, out string separator, out string anchor)
		{
			var parts = pathAndQuery.Split('?');
			var anchorParts = pathAndQuery.Split('#');
			anchor = string.Empty;
			var path = parts[0];
			separator = "?";

			if (anchorParts.Length > 1)
			{
				anchor = string.Format("#{0}", anchorParts[1]);
				path = path.Replace(anchor, string.Empty);
			}

			string parameters = null;
			NameValueCollection nvc = new NameValueCollection();
			if (parts.Length > 1)
			{
				parameters = parts[1];
				if (anchorParts.Length > 1)
				{
					parameters = parameters.Replace(anchor, string.Empty);
				}
				var list = parameters.Split('&');
				foreach (var token in list)
				{
					var keyvalue = token.Split('=');
					if (keyvalue.Length == 2)
					{
						var k = keyvalue[0];
						var value = keyvalue[1];
						nvc.Add(k, value);
					}
				}
			}

			pathAndQuery = path;
			foreach (var item in nvc.AllKeys)
			{
				if (item.Equals(key, StringComparison.InvariantCultureIgnoreCase))
				{
					continue;
				}
				var value = nvc[item];
				pathAndQuery += string.Format("{0}{1}={2}", separator, item, value);
				separator = "&";
			}
			return pathAndQuery;
		}

		/// <summary>
		/// Retourne la liste de tous les pays gérés par ERPStore
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static IEnumerable<SelectListItem> CountryList(this HtmlHelper helper)
		{
			return from deliveryCountry in ERPStoreApplication.WebSiteSettings.Shipping.DeliveryCountryList
				   select new SelectListItem()
				   {
					   Text = deliveryCountry.Country.LocalizedName,
					   Value = deliveryCountry.Country.Id.ToString()
				   };

		}

		public static IEnumerable<SelectListItem> CountryList(this HtmlHelper helper, Models.Country defautCountry)
		{
			return from deliveryCountry in ERPStoreApplication.WebSiteSettings.Shipping.DeliveryCountryList
				   select new SelectListItem()
				   {
					   Text = deliveryCountry.Country.LocalizedName,
					   Value = deliveryCountry.Country.Id.ToString(),
					   Selected = (deliveryCountry.Country.LocalizedName == defautCountry.LocalizedName),
				   };
		}

		public static MvcHtmlString ShowLogo(this HtmlHelper helper)
		{
			var sb = new System.Text.StringBuilder();
			if (ERPStoreApplication.WebSiteSettings.LogoImageFileName.IsNullOrTrimmedEmpty())
			{
				sb.AppendFormat("<a href=\"/\" title=\"{1}\">{0}</a><br />", ERPStoreApplication.WebSiteSettings.SiteName, ERPStoreApplication.WebSiteSettings.Sloggan);
				sb.AppendFormat("<div>{0}</div>", ERPStoreApplication.WebSiteSettings.Sloggan);
			} 
			else  
			{
				sb.AppendFormat("<a href=\"/\"><img src=\"{0}\" alt=\"{1}\" border=\"0\" /></a>", ERPStoreApplication.WebSiteSettings.LogoImageFileName, ERPStoreApplication.WebSiteSettings.Sloggan);
			}
			return new MvcHtmlString(sb.ToString());
		}

		[Obsolete("use Html.ShowMenu('anonyouseView', 'connectedView') instead", false)]
		public static MvcHtmlString ShowMenu(this HtmlHelper helper)
		{
			return helper.ShowMenu("menu");
		}

		public static MvcHtmlString ShowMenu(this HtmlHelper helper, string viewName)
		{ 
			return helper.Action<Controllers.HomeController>(i => i.ShowMenu(viewName, helper.ViewContext.RouteData));
		}

		public static MvcHtmlString ShowMenu(this HtmlHelper helper, string anonymousMenuView, string connectedMenuView)
		{
			if (helper.ViewContext.HttpContext.User.Identity.IsAuthenticated)
			{
				return helper.Partial(connectedMenuView);
			}
			else
			{
				return helper.Partial(anonymousMenuView);
			}
		}

		public static string Href(this UrlHelper helper, Models.MenuItem menuItem)
		{
			return helper.RouteERPStoreUrl(menuItem.RouteName, new { });
		}

		public static MvcHtmlString ShowValidationSummary(this HtmlHelper helper, string partialViewName)
		{
			return helper.Partial(partialViewName);
		}

		/// <summary>
		/// Retourne les infos d'une propriété etendues.
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="propertyGroupId">The property group id.</param>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns></returns>
		public static Models.PropertyInfo GetExtendedPropertyInfo(this HtmlHelper helper, int propertyGroupId, string propertyName)
		{
            var connectorService = DependencyResolver.Current.GetService<Services.IConnectorService>();
			var pi = connectorService.GetPropertyInfoList().SingleOrDefault(i => i.PropertyGroupId == propertyGroupId && i.Id.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));
			if (pi == null)
			{
				return null;
			}
			return pi;
		}

        [Obsolete("use javascript instead", true)]
		public static string Scripts(this HtmlHelper helper)
		{
			var sb = new System.Text.StringBuilder();

			var urlhelper = new UrlHelper(helper.ViewContext.RequestContext);

			// Script du panier de type commande
			sb.AppendFormat("<script type=\"text/javascript\" src=\"{0}\"></script>", urlhelper.RouteERPStoreUrl(ERPStoreRoutes.CART_SCRIPT, null));
			// Script du panier de type devis
			sb.AppendFormat("<script type=\"text/javascript\" src=\"{0}\"></script>", urlhelper.RouteERPStoreUrl(ERPStoreRoutes.QUOTECART_SCRIPT, null));
			return sb.ToString();
		}

		public static string StaticHref(this UrlHelper helper, string viewName)
		{
			var result = helper.RouteERPStoreUrl(ERPStoreRoutes.STATIC_PAGE, new { viewName = viewName });
			return result;
		}

		public static string HideRefererHref(this UrlHelper helper, string targetUrl)
		{
			var bts = System.Text.Encoding.UTF8.GetBytes(targetUrl);
			var encodedTargetUrl = System.Convert.ToBase64String(bts);
			var result = string.Format("/redirect?url={0}", encodedTargetUrl);
			return result;
		}

		public static string LogoSrc(this UrlHelper helper)
		{
			var result = ERPStoreApplication.WebSiteSettings.LogoImageFileName;
			return result;
		}

		public static string FullUrl(this string virtualPath)
		{
			return string.Format("http://{0}{1}", ERPStoreApplication.WebSiteSettings.DefaultUrl, virtualPath);
		}
	}
}
