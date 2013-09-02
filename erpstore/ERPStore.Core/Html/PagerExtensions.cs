using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ERPStore.Html
{
	/// <summary>
	/// Methodes d'extension concernant la gestion de l'affichage du paging des listes
	/// </summary>
	public static class PagerExtensions
	{
		/// <summary>
		/// Insere la vue pager, les données sont basées sur une liste paginable
		/// </summary>
		/// <remarks>
		/// la vue se trouve dans le repertoire /views/shared/pager.ascx
		/// si le nombre d'item de la liste est inférieur à la taille de la page, la vue n'est pas 
		/// insérée
		/// </remarks>
		/// <param name="helper">The helper.</param>
		/// <param name="list">The list.</param>
		public static MvcHtmlString ShowPager(this HtmlHelper helper, Models.IPaginable list)
		{
			return helper.ShowPager(list, "pager");
		}

		/// <summary>
		/// Insere la vue pager, les données sont basées sur une liste paginable
		/// </summary>
		/// <remarks>
		/// si la vue se trouve dans un autre repertoire que celui ci /view/shared
		/// il faut indiquer le nom de la vue formelement ~/view/folder/pagerview.ascx
		/// 
		/// si le nombre d'item de la liste est inférieur à la taille de la page, la vue n'est pas 
		/// insérée
		/// </remarks>
		/// <example>
		/// <![CDATA[
		/// <%Html.ShowPager(Model,"nom de la vue.ascx");%>
		/// ]]>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="list">The list.</param>
		/// <param name="viewName">Name of the view.</param>
        public static MvcHtmlString ShowPager(this HtmlHelper helper, Models.IPaginable list, string viewName)
		{
			var index = list.PageIndex;
			var itemCount = list.ItemCount;

			if ((index + 1) * list.PageSize < itemCount
				|| itemCount > list.PageSize)
			{
				return helper.Partial(viewName, list);
			}
            return null;
		}

		/// <summary>
		/// Insere la vue pager
		/// </summary>
		/// <remarks>
		/// la vue se trouve dans le repertoire /views/shared/pager.ascx
		/// si le nombre d'item de la liste est inférieur à la taille de la page, la vue n'est pas 
		/// insérée
		/// </remarks>
		/// <example>
		/// <![CDATA[
		/// <%Html.ShowPager();%>
		/// ]]>
		/// </example>
		/// <param name="helper">The helper.</param>
        public static MvcHtmlString ShowPager(this HtmlHelper helper)
		{
			return helper.ShowPager("pager");
		}

		/// <summary>
		/// Insere la vue (viewName) permettant de traiter le paging
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="viewName">Name of the view.</param>
		/// <remarks>
		/// si la vue se trouve dans un autre repertoire que celui ci /view/shared
		/// il faut indiquer le nom de la vue formelement ~/view/folder/pagerview.ascx
		/// si le nombre d'item de la liste est inférieur à la taille de la page, la vue n'est pas
		/// insérée
		/// </remarks>
		/// <example>
		/// 	<![CDATA[
		/// <%Html.ShowPager("nom de la vue.ascx");%>
		/// ]]>
		/// </example>
        public static MvcHtmlString ShowPager(this HtmlHelper helper, string viewName)
		{
			var index = Convert.ToInt32(helper.ViewData["PageIndex"]);
			var itemCount = Convert.ToInt32(helper.ViewData["ItemCount"]);

			if ((index + 1) * ERPStoreApplication.WebSiteSettings.Catalog.PageSize < itemCount
				|| itemCount > ERPStoreApplication.WebSiteSettings.Catalog.PageSize)
			{
				return helper.Partial(viewName);
			}
            return null;
		}

		/// <summary>
		/// Retourne un element html anchor pointant vers la premiere page de la liste
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="list">The list.</param>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		/// <remarks>
		/// S'il s'agit du premier lien l'element retourné est uniquement du texte
		/// </remarks>
		public static MvcHtmlString FirstPageLink(this HtmlHelper helper, Models.IPaginable list, string title)
		{
			var pathAndQuery = HttpContext.Current.Request.RawUrl;
			pathAndQuery = StoreExtensions.RemoveParameter(pathAndQuery, ERPStoreApplication.WebSiteSettings.Catalog.PageParameterName); // System.Text.RegularExpressions.Regex.Replace(pathAndQuery, @"page=\d+", "");
			if (list.PageIndex <= 1)
			{
				return new MvcHtmlString(title);
			}
			else
			{
				return new MvcHtmlString(string.Format("<a href=\"{0}\" class=\"pager-link\">{1}</a>", pathAndQuery, title));
			}
		}

		public static MvcHtmlString FirstPageHref(this UrlHelper helper, Models.IPaginable list)
		{
			var pathAndQuery = HttpContext.Current.Request.RawUrl;
			pathAndQuery = StoreExtensions.RemoveParameter(pathAndQuery, ERPStoreApplication.WebSiteSettings.Catalog.PageParameterName);
			return new MvcHtmlString(pathAndQuery);
		}

		/// <summary>
		/// Retourne un element html anchor pointant vers la premiere page de la liste
		/// </summary>
		/// <remarks>
		/// S'il s'agit du premier lien l'element retourné est uniquement du texte
		/// </remarks>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		public static MvcHtmlString FirstPageLink(this HtmlHelper helper, string title)
		{
			int pageIndex = Convert.ToInt32(helper.ViewData["PageIndex"]);
			var pathAndQuery = HttpContext.Current.Request.RawUrl;
			pathAndQuery = StoreExtensions.RemoveParameter(pathAndQuery, ERPStoreApplication.WebSiteSettings.Catalog.PageParameterName);
			if (pageIndex <= 1)
			{
				return new MvcHtmlString(title);
			}
			else
			{
				return new MvcHtmlString(string.Format("<a href=\"{0}\" class=\"pager-link\">{1}</a>", pathAndQuery, title));
			}
		}

		/// <summary>
		/// Retourne l'url de la première page d'une list
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="list">The list.</param>
		/// <param name="pageIndex">Index of the page.</param>
		/// <returns></returns>
		/// <remarks>
		/// S'il s'agit de la première page de la liste la valeur # est retournée
		/// pour passer en paramètre le pageIndex il est possible de le retrouver comme ceci :
		/// Html.ViewData["PageIndex"]
		/// </remarks>
		public static MvcHtmlString FirstPageHref(this UrlHelper helper, Models.IPaginable list, int pageIndex)
		{
			var pathAndQuery = HttpContext.Current.Request.RawUrl;
			pathAndQuery = StoreExtensions.RemoveParameter(pathAndQuery, ERPStoreApplication.WebSiteSettings.Catalog.PageParameterName);
			if (pageIndex <= 1)
			{
				return new MvcHtmlString("#");
			}
			else
			{
				return new MvcHtmlString(pathAndQuery);
			}
		}


		/// <summary>
		/// Retourne l'url de la première page d'une list
		/// </summary>
		/// <remarks>
		/// S'il s'agit de la première page de la liste la valeur # est retournée
		/// pour passer en paramètre le pageIndex il est possible de le retrouver comme ceci :
		/// Html.ViewData["PageIndex"]
		/// </remarks>
		/// <param name="helper">The helper.</param>
		/// <param name="pageIndex">Index of the page.</param>
		/// <returns></returns>
		public static MvcHtmlString FirstPageHref(this UrlHelper helper, int pageIndex)
		{
			var pathAndQuery = HttpContext.Current.Request.RawUrl;
			pathAndQuery = StoreExtensions.RemoveParameter(pathAndQuery, ERPStoreApplication.WebSiteSettings.Catalog.PageParameterName); // System.Text.RegularExpressions.Regex.Replace(pathAndQuery, @"(\?|\&)page=\d+", "");
			if (pageIndex <= 1)
			{
				return new MvcHtmlString("#");
			}
			else
			{
				return new MvcHtmlString(pathAndQuery);
			}
		}

		/// <summary>
		/// Retourne un element html anchor pointant vers la page precedente de la liste
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="list">The list.</param>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		/// <remarks>
		/// S'il s'agit de la première page l'element retourné est uniquement du texte
		/// </remarks>
		public static MvcHtmlString PreviousPageLink(this HtmlHelper helper, Models.IPaginable list, string title)
		{
			var pathAndQuery = HttpContext.Current.Request.RawUrl;
			//pathAndQuery = System.Text.RegularExpressions.Regex.Replace(pathAndQuery, @"(\?|\&)page=\d+", "");
			//string separator = (pathAndQuery.IndexOf("?") == -1) ? "?" : "&";
			//pathAndQuery += string.Format("{0}page={1}", separator, Math.Max(list.PageIndex - 1, 1));
			pathAndQuery = StoreExtensions.AddParameter(pathAndQuery, ERPStoreApplication.WebSiteSettings.Catalog.PageParameterName, Math.Max(list.PageIndex - 1, 1));
			if (list.PageIndex <= 1)
			{
				return new MvcHtmlString(title);
			}
			else
			{
				return new MvcHtmlString(string.Format("<a href=\"{0}\" class=\"pager-link\">{1}</a>", pathAndQuery, title));
			}
		}


		/// <summary>
		/// Retourne un element html anchor pointant vers la page precedente de la liste
		/// </summary>
		/// <remarks>
		/// S'il s'agit de la première page l'element retourné est uniquement du texte
		/// </remarks>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		public static MvcHtmlString PreviousPageLink(this HtmlHelper helper, string title)
		{
			int pageIndex = Convert.ToInt32(helper.ViewData["PageIndex"]);
			var pathAndQuery = HttpContext.Current.Request.RawUrl;
			//pathAndQuery = System.Text.RegularExpressions.Regex.Replace(pathAndQuery, @"(\?|\&)page=\d+", "");
			//string separator = (pathAndQuery.IndexOf("?") == -1) ? "?" : "&";
			//pathAndQuery += string.Format("{0}page={1}", separator, Math.Max(pageIndex - 1, 1));
			pathAndQuery = StoreExtensions.AddParameter(pathAndQuery, ERPStoreApplication.WebSiteSettings.Catalog.PageParameterName, Math.Max(pageIndex - 1, 1));
			if (pageIndex <= 1)
			{
				return new MvcHtmlString(title);
			}
			else
			{
				return new MvcHtmlString(string.Format("<a href=\"{0}\" class=\"pager-link\">{1}</a>", pathAndQuery, title));
			}
		}

		public static MvcHtmlString PreviousPageHref(this HtmlHelper helper, Models.IPaginable pager)
		{
			var pathAndQuery = HttpContext.Current.Request.RawUrl;
			pathAndQuery = StoreExtensions.AddParameter(pathAndQuery, ERPStoreApplication.WebSiteSettings.Catalog.PageParameterName, Math.Max(pager.PageIndex - 1, 1));
			return new MvcHtmlString(pathAndQuery);
		}

		/// <summary>
		/// Retourne un element html anchor pointant vers la page suivante de la liste
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="list">The list.</param>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		/// <remarks>
		/// S'il s'agit de la dernière page l'element retourné est uniquement du texte
		/// </remarks>
		public static MvcHtmlString NextPageLink(this HtmlHelper helper, Models.IPaginable list, string title)
		{
			var pathAndQuery = HttpContext.Current.Request.RawUrl;
			//pathAndQuery = System.Text.RegularExpressions.Regex.Replace(pathAndQuery, @"(\?|\&)page=\d+", "");
			//string separator = (pathAndQuery.IndexOf("?") == -1) ? "?" : "&";
			int maxPage = (int)Math.Ceiling(list.ItemCount / (list.PageSize * 1.0));
			//pathAndQuery += string.Format("{0}page={1}", separator, Math.Min(list.PageIndex + 1, maxPage));
			pathAndQuery = StoreExtensions.AddParameter(pathAndQuery, ERPStoreApplication.WebSiteSettings.Catalog.PageParameterName, Math.Min(list.PageIndex + 1, maxPage));
			if (list.PageIndex >= maxPage)
			{
				return new MvcHtmlString(title);
			}
			else
			{
				return new MvcHtmlString(string.Format("<a href=\"{0}\" class=\"pager-link\">{1}</a>", pathAndQuery, title));
			}
		}

		public static MvcHtmlString NextPageHref(this HtmlHelper helper, Models.IPaginable list)
		{
			var pathAndQuery = HttpContext.Current.Request.RawUrl;
			int maxPage = (int)Math.Ceiling(list.ItemCount / (list.PageSize * 1.0));
			pathAndQuery = StoreExtensions.AddParameter(pathAndQuery, ERPStoreApplication.WebSiteSettings.Catalog.PageParameterName, Math.Min(list.PageIndex + 1, maxPage));
			return new MvcHtmlString(pathAndQuery);
		}

		/// <summary>
		/// Retourne un element html anchor pointant vers la page suivante de la liste
		/// </summary>
		/// <remarks>
		/// S'il s'agit de la dernière page l'element retourné est uniquement du texte
		/// </remarks>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		public static MvcHtmlString NextPageLink(this HtmlHelper helper, string title)
		{
			int pageIndex = Convert.ToInt32(helper.ViewData["PageIndex"]);
			int productCount = Convert.ToInt32(helper.ViewData["ItemCount"]);
			var pathAndQuery = HttpContext.Current.Request.RawUrl;
			//pathAndQuery = System.Text.RegularExpressions.Regex.Replace(pathAndQuery, @"(\?|\&)page=\d+", "");
			// string separator = (pathAndQuery.IndexOf("?") == -1) ? "?" : "&";
			int maxPage = (int)Math.Ceiling(productCount / (ERPStoreApplication.WebSiteSettings.Catalog.PageSize * 1.0));
			// pathAndQuery += string.Format("{0}page={1}", separator, Math.Min(pageIndex + 1, maxPage));
			pathAndQuery = StoreExtensions.AddParameter(pathAndQuery, ERPStoreApplication.WebSiteSettings.Catalog.PageParameterName, Math.Min(pageIndex + 1, maxPage));
			if (pageIndex >= maxPage)
			{
				return new MvcHtmlString(title);
			}
			else
			{
				return new MvcHtmlString(string.Format("<a href=\"{0}\" class=\"pager-link\">{1}</a>", pathAndQuery, title));
			}
		}

		/// <summary>
		/// Retourne un element html anchor pointant vers la dernière page de la liste
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="list">The list.</param>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		/// <remarks>
		/// S'il s'agit de la dernière page l'element retourné est uniquement du texte
		/// </remarks>
		public static MvcHtmlString LastPageLink(this HtmlHelper helper, Models.IPaginable list, string title)
		{
			var pathAndQuery = HttpContext.Current.Request.RawUrl;
			//pathAndQuery = System.Text.RegularExpressions.Regex.Replace(pathAndQuery, @"(\?|\&)page=\d+", "");
			//string separator = (pathAndQuery.IndexOf("?") == -1) ? "?" : "&";
			int maxPage = (int)Math.Ceiling(list.ItemCount / (list.PageSize * 1.0));
			// pathAndQuery += string.Format("{0}page={1}", separator, maxPage);
			pathAndQuery = StoreExtensions.AddParameter(pathAndQuery, ERPStoreApplication.WebSiteSettings.Catalog.PageParameterName, maxPage);
			if (list.PageIndex >= maxPage)
			{
				return new MvcHtmlString(title);
			}
			else
			{
				return new MvcHtmlString(string.Format("<a href=\"{0}\" class=\"pager-link\">{1}</a>", pathAndQuery, title));
			}
		}

		/// <summary>
		/// Retourne un element html anchor pointant vers la dernière page de la liste
		/// </summary>
		/// <remarks>
		/// S'il s'agit de la dernière page l'element retourné est uniquement du texte
		/// </remarks>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		public static MvcHtmlString LastPageLink(this HtmlHelper helper, string title)
		{
			int pageIndex = Convert.ToInt32(helper.ViewData["PageIndex"]);
			int productCount = Convert.ToInt32(helper.ViewData["ItemCount"]);
			var pathAndQuery = HttpContext.Current.Request.RawUrl;
			// pathAndQuery = System.Text.RegularExpressions.Regex.Replace(pathAndQuery, @"(\?|\&)page=\d+", "");
			// string separator = (pathAndQuery.IndexOf("?") == -1) ? "?" : "&";
			int maxPage = (int)Math.Ceiling(productCount / (ERPStoreApplication.WebSiteSettings.Catalog.PageSize * 1.0));
			// pathAndQuery += string.Format("{0}page={1}", separator, maxPage);
			pathAndQuery = StoreExtensions.AddParameter(pathAndQuery, ERPStoreApplication.WebSiteSettings.Catalog.PageParameterName, maxPage);
			if (pageIndex >= maxPage)
			{
				return new MvcHtmlString(title);
			}
			else
			{
				return new MvcHtmlString(string.Format("<a href=\"{0}\" class=\"pager-link\">{1}</a>", pathAndQuery, title));
			}
		}

		public static MvcHtmlString GoToPageHref(this UrlHelper helper, int pageId)
		{
			var pathAndQuery = HttpContext.Current.Request.RawUrl;
			pathAndQuery = StoreExtensions.AddParameter(pathAndQuery, ERPStoreApplication.WebSiteSettings.Catalog.PageParameterName, pageId); 
			return new MvcHtmlString(pathAndQuery);
		}
	}
}
