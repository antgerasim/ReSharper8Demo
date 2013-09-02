using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using ERPStore.Html;

namespace ERPStore.Html
{
	/// <summary>
	/// Methodes d'extension permettant la gestion d'un chemin de fer
	/// </summary>
	public static class BreadCrumbExtensions
	{
		/// <summary>
		/// Retourne la liste des liens sous la forme Clé/Url pour former un 
		/// chemin de fer lorsque le model en cours est une liste de categorie de produits
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="list">The list.</param>
		/// <returns></returns>
		public static Dictionary<string, string> GetBreadcrumb(this UrlHelper helper, IList<Models.ProductCategory> list)
		{
			var result = CreateDefaultBreadcrumb();
			result.Add("categories", helper.RouteERPStoreUrl(ERPStoreRoutes.PRODUCT_CATEGORIES));
			return result;
		}

		/// <summary>
		/// Retourne la liste des liens sous la forme Clé/Url pour former un
		/// chemin de fer lorsque le model en cours est une categorie de produits
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="category">The category.</param>
		/// <returns></returns>
		public static Dictionary<string, string> GetBreadcrumb(this UrlHelper helper, Models.ProductCategory category)
		{
			var result = helper.GetBreadcrumb(new List<Models.ProductCategory>());
			if (category == null)
			{
				return result;
			}

			// On depile le path
			var path = new Dictionary<string, string>();
			path.Add(category.Name, helper.Href(category));
			var c = category.Parent;
			while(true)
			{
				if (c != null)
				{
					path.Add(c.Name, helper.Href(c));
				}
				else
				{
					break;
				}
				c = c.Parent;
			}

			// Ajout inverse du plus grand au plus petit
			var reverse = path.Keys.Reverse();
			foreach (var item in reverse)
			{
				result.Add(item, path[item]);
			}

			return result;
		}

		/// <summary>
		/// Retourne la liste des liens sous la forme Clé/Url pour former un
		/// chemin de fer lorsque le model en cours est un produit
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <returns></returns>
		public static Dictionary<string, string> GetBreadcrumb(this UrlHelper helper, Models.Product product)
		{
			var result = CreateDefaultBreadcrumb();
			if (product.Category != null)
			{
				var list = helper.GetBreadcrumb(product.Category);
				foreach (var item in list)
				{
					if (!result.ContainsKey(item.Key))
					{
						result.Add(item.Key, item.Value);
					}
				}
			}

			if (!result.ContainsKey(product.Title))
			{
				result.Add(product.Title, helper.Href(product));
			}

			return result;
		}

		/// <summary>
		/// Retourne la liste des liens sous la forme Clé/Url pour former un 
		/// chemin de fer lorsque le model en cours est une liste de marque
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="list">The list.</param>
		/// <returns></returns>
		public static Dictionary<string, string> GetBreadcrumb(this UrlHelper helper, IList<Models.Brand> list)
		{
			var result = CreateDefaultBreadcrumb();
			return result;
		}

		/// <summary>
		/// Retourne la liste des liens sous la forme Clé/Url pour former un
		/// chemin de fer lorsque le model en cours est une marque
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="brand">The brand.</param>
		/// <returns></returns>
		public static Dictionary<string, string> GetBreadcrumb(this UrlHelper helper, Models.Brand brand)
		{
			var result = helper.GetBreadcrumb(new List<Models.Brand>());

			result.Add(brand.Name, helper.Href(brand));

			return result;
		}

		/// <summary>
		/// Retourne la liste des liens sous la forme Clé/Url pour former un 
		/// chemin de fer lorsque le model en cours est une recherche de produits
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="search">The search.</param>
		/// <returns></returns>
		public static Dictionary<string, string> GetBreadcrumb(this UrlHelper helper, Models.ProductList search)
		{
			var result = CreateDefaultBreadcrumb();
			result.Add(helper.Encode(search.Query), helper.RouteERPStoreUrl(ERPStoreRoutes.PRODUCT_SEARCH, new { s = helper.Encode(search.Query) }));

			return result;
		}

		private static Dictionary<string, string> CreateDefaultBreadcrumb()
		{
			var result = new Dictionary<string, string>();
			result.Add("accueil", "/");
			return result;
		}

	}
}
