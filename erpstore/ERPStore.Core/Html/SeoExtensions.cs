using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

using Microsoft.Practices.Unity;

namespace ERPStore.Html
{
	/// <summary>
	/// 
	/// </summary>
	public static class SeoExtensions
	{
		/// <summary>
		/// Balise méta de description de la page
		/// doit etre comprise entre 25 et 150 caractères
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="description">The description.</param>
		/// <returns></returns>
		public static MvcHtmlString MetaDescription(this HtmlHelper helper, string description)
		{
			if (description == null || description.Length < 25)
			{
				description += " : " + ERPStoreApplication.WebSiteSettings.SiteName;
			}
			return new MvcHtmlString(string.Format("<meta name=\"description\" content=\"{0}\" />", description.Replace("\"", "''").EllipsisAt(150)));
		}

		public static MvcHtmlString MetaKeywords(this HtmlHelper helper, string keywords)
		{
			return new MvcHtmlString(string.Format("<meta name=\"keywords\" content=\"{0}\" />", keywords));
		}

		/// <summary>
		/// Titre de la page 
		/// doit etre compris entre 5 et 65 caractères
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		public static MvcHtmlString PageTitle(this HtmlHelper helper, string title)
		{
			if (title == null)
			{
				title = string.Empty;
			}
			return new MvcHtmlString(string.Format("<title>{0}</title>", title.EllipsisAt(65).Trim()));
		}

		public static MvcHtmlString PageTitle(this HtmlHelper helper, Models.Product product)
		{
			if (!product.PageTitle.IsNullOrTrimmedEmpty())
			{
				return helper.PageTitle(product.PageTitle);
			}
			return helper.PageTitle(product.Title);
		}

		public static MvcHtmlString MetaInformations(this HtmlHelper helper)
		{
			var currentRoute = helper.ViewContext.RouteData.Route as ERPStoreRoute;
			var result = new StringBuilder();
			if (currentRoute == null)
			{
                var logger = DependencyResolver.Current.GetService<Logging.ILogger>();
				logger.Warn("No metainformations for currentRoute {0}", helper.ViewContext.HttpContext.Request.RawUrl);
			}
			else
			{
				var controller = currentRoute.Defaults["Controller"];
				var action = currentRoute.Defaults["Action"];
				var language = currentRoute.Defaults["Language"];
				var name = currentRoute.Name;
				var uniqueName = string.Format("{0}/{1}", controller, name);
				string categoryName = null;
				string brandName = null;
				switch (uniqueName.ToLower())
				{
					case "catalog/destock" :
						var destockList = helper.ViewData.Model as Models.ProductList;
						if (destockList.Category != null)
						{
							categoryName = string.Format(" categorie {0}", destockList.Category.Name);
						}
						if (destockList.Brand != null)
						{
							brandName = string.Format(" marque {0}", destockList.Brand.Name);
						}
						var destockProductList = destockList.Take(3).Select(i => i.Title).JoinString(",");
						result.Append(helper.PageTitle(string.Format("Destockage : {0} page {1}/{2}{3}{4}", ERPStoreApplication.WebSiteSettings.SiteName, destockList.PageIndex, destockList.GetPageCount(), brandName, categoryName)));
						result.Append(helper.MetaDescription(string.Format("Le déstockage {0}{1}{2}", destockProductList, brandName, categoryName)));
						break;
					case "catalog/topsells":
						var topSellsList = helper.ViewData.Model as Models.ProductList;
						if (topSellsList.Category != null)
						{
							categoryName = string.Format(" categorie {0}", topSellsList.Category.Name);
						}
						if (topSellsList.Brand != null)
						{
							brandName = string.Format(" marque {0}", topSellsList.Brand.Name);
						}
						var topSellsListProductList = topSellsList.Take(3).Select(i => i.Title).JoinString(",");
						result.Append(helper.PageTitle(string.Format("Top ventes : {0} page {1}/{2}{3}{4}", ERPStoreApplication.WebSiteSettings.SiteName, topSellsList.PageIndex, topSellsList.GetPageCount(), brandName, categoryName)));
						result.Append(helper.MetaDescription(string.Format("Les produits les plus vendus {0}{1}{2}", topSellsListProductList, brandName, categoryName)));
						break;
					case "catalog/promotions":
						var promotionList = helper.ViewData.Model as Models.ProductList;
						if (promotionList.Category != null)
						{
							categoryName = string.Format(" categorie {0}", promotionList.Category.Name);
						}
						if (promotionList.Brand != null)
						{
							brandName = string.Format(" marque {0}", promotionList.Brand.Name);
						}
						var promotionListProductList = promotionList.Take(3).Select(i => i.Title).JoinString(",");
						result.Append(helper.PageTitle(string.Format("Promotions : {0} page {1}/{2}{3}{4}", ERPStoreApplication.WebSiteSettings.SiteName, promotionList.PageIndex, promotionList.GetPageCount(), brandName, categoryName)));
						result.Append(helper.MetaDescription(string.Format("Les promotions {0}{1}{2}", promotionListProductList, brandName, categoryName)));
						break;
					case "catalog/newproducts":
						var newList = helper.ViewData.Model as Models.ProductList;
						if (newList.Category != null)
						{
							categoryName = string.Format(" categorie {0}", newList.Category.Name);
						}
						if (newList.Brand != null)
						{
							brandName = string.Format(" marque {0}", newList.Brand.Name);
						}
						var newProductList = newList.Take(3).Select(i => i.Title).JoinString(",");
						result.Append(helper.PageTitle(string.Format("Nouveautés produits : {0} page {1}/{2}{3}{4}", ERPStoreApplication.WebSiteSettings.SiteName, newList.PageIndex, newList.GetPageCount(), brandName, categoryName)));
						result.Append(helper.MetaDescription(string.Format("Les nouveautés produit {0}{1}{2}", newProductList, brandName, categoryName)));
						break;
					case "catalog/brands":
					case "catalog/brandlist" : // Liste des marques
						result.Append(helper.PageTitle(string.Format("Les marques proposées par {0}", ERPStoreApplication.WebSiteSettings.SiteName)));
						var brands = helper.ViewData.Model as IList<Models.Brand>;
						var brandList = brands.OrderByDescending(i => i.ProductCount)
												.Take(10)
												.Select(i => i.Name)
												.JoinString(",");

						result.Append(helper.MetaDescription(string.Format("Les marques proposées par {0}, {1}",ERPStoreApplication.WebSiteSettings.SiteName, brandList)));
						break;
					case "catalog/productcategories" :
					case "catalog/categorylist": // Liste des categories
						result.Append(helper.PageTitle(string.Format("Les catégories de produit proposées par {0}", ERPStoreApplication.WebSiteSettings.SiteName)));
						var categories = helper.ViewData.Model as IList<Models.ProductCategory>;
						var categoryList = categories.Take(10).Select(i => i.Name).JoinString(",");
						result.Append(helper.MetaDescription(string.Format("Les catégories proposées par {0}, {1}", ERPStoreApplication.WebSiteSettings.SiteName, categoryList)));
						break;
					case "catalog/productbycategory": 
					case "catalog/category":
						var productByCategorylist = helper.ViewData.Model as Models.ProductList;
						if (productByCategorylist.Brand != null)
						{
							brandName = string.Format(" marque {0}", productByCategorylist.Brand.Name);
						}
						result.Append(helper.PageTitle(string.Format("Categorie {0} ,page {1}/{2}{3}", productByCategorylist.Category.Name, productByCategorylist.PageIndex, productByCategorylist.GetPageCount(), brandName)));
						if (!productByCategorylist.Category.PageDescription.IsNullOrTrimmedEmpty())
						{
							result.Append(helper.MetaDescription(string.Format("{0}{1}",productByCategorylist.Category.PageDescription, brandName)));
						}
						else
						{
							result.Append(helper.MetaDescription(string.Format("Les produits de la categorie {0}{1}", productByCategorylist.Category.Name, brandName)));
						}
						if (!productByCategorylist.Category.Keywords.IsNullOrTrimmedEmpty())
						{
							result.Append(helper.MetaKeywords(productByCategorylist.Category.Keywords));
						}
						break;
					case "catalog/productbybrand":
					case "catalog/brand":
						var productByBrandList = helper.ViewData.Model as Models.ProductList;
						if (productByBrandList.Category != null)
						{
							categoryName = string.Format(" categorie {0}", productByBrandList.Category.Name);
						}
						result.Append(helper.PageTitle(string.Format("Marque {0} ,page {1}/{2}{3}", productByBrandList.Brand.Name, productByBrandList.PageIndex, productByBrandList.GetPageCount(), categoryName)));
						if (!productByBrandList.Brand.PageDescription.IsNullOrTrimmedEmpty())
						{
							result.Append(helper.MetaDescription(string.Format("{0}{1}", productByBrandList.Brand.PageDescription, categoryName)));
						}
						else
						{
							result.Append(helper.MetaDescription(string.Format("Les produits de la marque {0}{1}", productByBrandList.Brand.Name, categoryName)));
						}
						if (!productByBrandList.Brand.Keywords.IsNullOrTrimmedEmpty())
						{
							result.Append(helper.MetaKeywords(productByBrandList.Brand.Keywords));
						}
						break;
					case "catalog/product" : // Fiche produit
						var product = helper.ViewData.Model as Models.Product;
						result.Append(helper.PageTitle(product));
						if (!product.PageDescription.IsNullOrTrimmedEmpty())
						{
							result.Append(helper.MetaDescription(product.PageDescription));
						}
						else if (!product.ShortDescription.IsNullOrTrimmedEmpty())
						{
							result.Append(helper.MetaDescription(product.ShortDescription));
						}
						else
						{
							result.Append(helper.MetaDescription(product.Title));
						}
						if (!product.Keywords.IsNullOrTrimmedEmpty())
						{
							result.Append(helper.MetaKeywords(product.Keywords));
						}
						else
						{
							result.Append(helper.MetaKeywords(product.Title));
						}
						break;
					default:
						break;
				}
			}
			return new MvcHtmlString(result.ToString());
		}

		public static string Tracker(this HtmlHelper helper)
		{
			var currentRoute = helper.ViewContext.RouteData.Route as System.Web.Routing.Route;
			var result = new StringBuilder();
			if (currentRoute == null)
			{
				return null;
			}
			var controller = currentRoute.Defaults["Controller"];
			var action = currentRoute.Defaults["Action"];
			var language = currentRoute.Defaults["Language"];
			var name = currentRoute.Defaults["Name"];
			var uniqueName = string.Format("{0}/{1}", controller, name);
			switch (uniqueName.ToLower())
			{
				case "quotecart/quotesent":

					break;
			}

			return result.ToString();
		}

		public static MvcHtmlString MetaInformations(this HtmlHelper helper, Models.Product product)
		{
			var sb = new StringBuilder();
			sb.Append(helper.PageTitle(product));
			if (!product.PageDescription.IsNullOrTrimmedEmpty())
			{
				sb.Append(helper.MetaDescription(product.PageDescription));
			}
			else if (product.ShortDescription.IsNullOrTrimmedEmpty())
			{
				sb.Append(helper.MetaDescription(product.ShortDescription));
			}
			else
			{
				sb.Append(helper.MetaDescription(product.Title));
			}
			if (!product.Keywords.IsNullOrTrimmedEmpty())
			{
				sb.Append(helper.MetaKeywords(product.Keywords));
			}
			else
			{
				sb.Append(helper.MetaKeywords(product.Title));
			}
			return new MvcHtmlString(sb.ToString());
		}

		public static MvcHtmlString Canonical(this UrlHelper helper)
		{
			var result = "<link rel=\"canonical\" href=\"http://{0}{1}\"/>";
			// var language = helper.GetCurrentLanguage();
			var host = ERPStoreApplication.WebSiteSettings.DefaultUrl;
			return new MvcHtmlString(string.Format(result, host, helper.RequestContext.HttpContext.Request.RawUrl));
		}

		public static MvcHtmlString Canonical(this UrlHelper helper, string RouteName)
		{
			var result = "<link rel=\"canonical\" href=\"http://{0}{1}\"/>";
			// var language = helper.GetCurrentLanguage();
			var host = helper.RequestContext.HttpContext.Request.Url.Host;
			var url = helper.RouteERPStoreUrl(RouteName, null);
			return new MvcHtmlString(string.Format(result, host, url));
		}
	}
}
