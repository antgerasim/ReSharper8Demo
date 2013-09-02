using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

using Microsoft.Practices.Unity;

namespace ERPStore.Html
{
	/// <summary>
	/// Methode d'extension concernant la manipulation d'affichage de tout
	/// ce qui est lié aux categories et aux marques
	/// </summary>
	public static class CatalogExtensions
	{
		/// <summary>
		/// Insersion du controle contenant la liste des categories
		/// </summary>
		/// <remarks>
		/// Le fichier appelé est /views/catalog/categories.ascx
		/// Retourne la liste de toutes les categories
		/// </remarks>
		/// <example>Exemple d'appel :
		/// <code>
		/// <![CDATA[
		/// <%Html.ShowProductCategories();%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		public static MvcHtmlString ShowProductCategories(this HtmlHelper helper)
		{
			return helper.ShowProductCategories("_categories");
		}

		/// <summary>
		/// Retoune le lien vers la vue partielle pour une sous-categorie
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string SubProductCategoryHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.PRODUCT_SUB_CATEGORIES);
		}

		/// <summary>
		/// Affiche la vues partielle pour les sous categories d'une categorie parente.
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="parentCategoryCode">The parent category code.</param>
		/// <param name="viewName">Name of the view.</param>
		public static void ShowProductSubCategories(this HtmlHelper helper, string parentCategoryCode, string viewName)
		{
			helper.RenderAction<Controllers.CatalogController>(i => i.ShowSubCategories(parentCategoryCode, viewName));
		}

		/// <summary>
		/// Insersion du controle contenant la liste des categories
		/// </summary>
		/// <remarks>
		/// Le fichier appelé est doit se trouver dans le repertoire /views/catalog/
		/// Retourne la liste de toutes les categories
		/// </remarks>
		/// <example>Exemple d'appel :
		/// <code>
		/// <![CDATA[
		/// <%Html.ShowProductCategories("nom de la vue.ascx");%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="viewName">Name of the view.</param>
		public static MvcHtmlString ShowProductCategories(this HtmlHelper helper, string viewName)
		{
			return helper.Action<Controllers.CatalogController>(i => i.ShowCategoriesBox(viewName, helper.ViewContext.RouteData));
		}

        /// <summary>
        /// Vue partielle contenant la liste des produits d'une selection
        /// </summary>
        /// <remarks>
        /// Le fichier appelé est doit se trouver dans le repertoire /views/catalog/
        /// Retourne la liste de toutes les categories
        /// </remarks>
        /// <example>Exemple d'appel :
        /// <code>
        /// <![CDATA[
        /// <%Html.ShowProductListBySelection("nom de la vue.ascx");%>
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="helper">The helper.</param>
        /// <param name="viewName">Name of the view.</param>
        public static MvcHtmlString ShowProductListBySelection(this HtmlHelper helper, string viewName, string selectionName, int productCount)
        {
            return helper.Action<Controllers.CatalogController>(i => i.ShowProductListBySelection(viewName, selectionName, productCount));
        }

		public static MvcHtmlString ProductCategoryLink(this HtmlHelper helper, Models.ProductCategory productCategory)
		{
			if (productCategory == null)
			{
				return null;
			}
			return helper.ProductCategoryLink(productCategory, productCategory.Name);
		}

		public static MvcHtmlString ProductCategoryLink(this HtmlHelper helper, Models.ProductCategory productCategory, string title)
		{
			if (productCategory == null)
			{
				return null;
			}
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.PRODUCT_BY_CATEGORY, new { link = productCategory.Link }));
		}

		public static string Href(this UrlHelper helper, Models.ProductCategory productCategory)
		{
			string link = productCategory.Link;
			return helper.RouteERPStoreUrl(ERPStoreRoutes.PRODUCT_BY_CATEGORY, new { link = link, });
		}

		public static MvcHtmlString ShowProductCategoryListForefront(this HtmlHelper helper)
		{
			return helper.ShowProductCategoryListForefront("CategoriesForefront");
		}

		public static MvcHtmlString ShowProductCategoryListForefront(this HtmlHelper helper, string viewName)
		{
			// helper.RenderAction("ShowCategorieListForefront", "Catalog", new { viewName = viewName, routeData = helper.ViewContext.RouteData });
			return helper.Action<Controllers.CatalogController>(m => m.ShowCategorieListForefront(viewName, helper.ViewContext.RouteData));
		}

		public static MvcHtmlString ShowHeadProductOfCategory(this HtmlHelper helper, Models.ProductCategory category)
		{
			return helper.ShowHeadProductOfCategory(category, "_Product-Head");
		}

		public static MvcHtmlString ShowHeadProductOfCategory(this HtmlHelper helper, Models.ProductCategory category, string viewName)
		{
			return helper.Action<Controllers.CatalogController>(m => m.ShowHeadProductOfCategory(category.Id, viewName));
		}

		public static MvcHtmlString BrandLink(this HtmlHelper helper, Models.Brand brand)
		{
			if (brand.Link.IsNullOrTrimmedEmpty())
			{
				brand.Link = SEOHelper.SEOUrlEncode(brand.Name);
			}
			return new MvcHtmlString(helper.RouteERPStoreLink(brand.Name, ERPStoreRoutes.PRODUCT_BY_BRAND, new { link = brand.Link }));
		}

		public static string Href(this UrlHelper helper, Models.Brand brand)
		{
			if (brand.Link.IsNullOrTrimmedEmpty())
			{
				brand.Link = SEOHelper.SEOUrlEncode(brand.Name.ToLower());
			}
			return helper.RouteERPStoreUrl(ERPStoreRoutes.PRODUCT_BY_BRAND, new { link = brand.Link });
		}

		public static MvcHtmlString ShowBrandList(this HtmlHelper helper, string viewName)
		{
			return helper.Action<Controllers.CatalogController>(i => i.ShowBrandListView(viewName));
		}

		public static MvcHtmlString ShowBrandListForefront(this HtmlHelper helper)
		{
			return helper.ShowBrandListForefront("brandlistforefront");
		}

		public static MvcHtmlString ShowBrandListForefront(this HtmlHelper helper, string viewName)
		{
			return helper.Action<Controllers.CatalogController>(i => i.ShowBrandListForefront(viewName, helper.ViewContext.RouteData));
		}

		public static MvcHtmlString ShowProductListByCategory(this HtmlHelper helper, Models.ProductListType type, int categoryId, string viewName)
		{
			return helper.ShowProductListByCategory(type, categoryId, viewName, 0);
		}

		public static MvcHtmlString ShowProductListByCategory(this HtmlHelper helper, Models.ProductListType type, int categoryId, string viewName, int productCount)
		{
			return helper.Action<Controllers.CatalogController>(i => i.ShowProductListView(type, viewName, productCount, null));
		}

		/// <summary>
		/// Permet le rendu d'une vue partielle pour une categorie donnée
		/// </summary>
		/// <example>
		/// Exemple d'appel
		/// <code>
		/// <![CDATA[
		/// <%Html.ShowProductCategoryByCode("nom de la vue", "xxx")%>
		/// ]]>
		/// </code>
		/// </example>
		/// <remarks>
		/// La vue doit se trouver dans le repertoire /views/catalog
		/// </remarks>
		/// <param name="helper">The helper.</param>
		/// <param name="viewName">Name of the view.</param>
		/// <param name="categoryCode">The category code.</param>
		public static MvcHtmlString ShowProductCategoryByCode(this HtmlHelper helper, string viewName, string categoryCode)
		{
			return helper.Action<Controllers.CatalogController>(i => i.ShowProductCategoryByCode(viewName, categoryCode));
		}

		/// <summary>
		/// Retourne un objet ProductCategory via son code
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="categoryCode">The category code.</param>
		/// <returns></returns>
		public static Models.ProductCategory GetProductCategoryByCode(this HtmlHelper helper, string categoryCode)
		{
			var catalogService = DependencyResolver.Current.GetService<Services.ICatalogService>();
			return catalogService.GetCategoryByCode(categoryCode);
		}

		/// <summary>
		/// Retourne un element html img permettant d'afficher l'image par defaut d'une catégorie de produit
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="productCategoryImage">The product category image.</param>
		/// <param name="width">The width.</param>
		/// <param name="defaultUrImage">The default ur image.</param>
		/// <returns></returns>
		/// <example>Exemple d'appel
		/// <code>
		/// 		<![CDATA[
		/// <%=Html.ProductCategoryImage(category, 0, "imagepardefaut.png")%>
		/// ]]>
		/// 	</code>
		/// </example>
		/// <remarks>
		/// si l'on veut garder la taille originale de l'image il faut passer 0 comme parametre de largeur
		/// si la marque n'a pas d'image , c'est l'image par defaut qui est retournée
		/// </remarks>
		public static MvcHtmlString ProductCategoryImage(this HtmlHelper helper, Models.ProductCategory productCategoryImage, int width, string defaultUrImage)
		{
			return helper.ProductCategoryImage(productCategoryImage, width, 0, defaultUrImage);
		}

		/// <summary>
		/// Retourne un element html img permettant d'afficher l'image par defaut d'une catégorie de produit
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="productCategoryImage">The product category image.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="defaultUrImage">The default ur image.</param>
		/// <returns></returns>
		/// <example>Exemple d'appel
		/// <code>
		/// 		<![CDATA[
		/// <%=Html.ProductCategoryImage(category, 0, 0, "imagepardefaut.png")%>
		/// ]]>
		/// 	</code>
		/// </example>
		/// <remarks>
		/// si l'on veut garder la taille originale de l'image il faut passer 0 comme parametre de largeur
		/// si la marque n'a pas d'image , c'est l'image par defaut qui est retournée
		/// </remarks>
		public static MvcHtmlString ProductCategoryImage(this HtmlHelper helper, Models.ProductCategory productCategoryImage, int width, int height, string defaultUrImage)
		{
			if (productCategoryImage != null && productCategoryImage.DefaultImage != null)
			{
				string widthattb = (width == 0) ? "" : string.Format("width=\"{0}\"", width);
				string heightattb = (height == 0) ? "" : string.Format("height=\"{0}\"", height);
				return new MvcHtmlString(string.Format("<img src=\"{0}\" {1} {2} alt=\"{3}\" border=\"0\" />", string.Format(productCategoryImage.DefaultImage.Url, width, height), widthattb, heightattb, productCategoryImage.Name));
			}
			else if (defaultUrImage != null)
			{
				return new MvcHtmlString(string.Format("<img src=\"{0}\" alt=\"{1}\" border=\"0\" />", defaultUrImage, productCategoryImage.Name));
			}
			return null;
		}


		/// <summary>
		/// Retourne un element html img permettant d'afficher l'image par defaut d'une catégorie de produit
		/// </summary>
		/// <example>Exemple d'appel
		/// <code>
		/// 		<![CDATA[
		/// <%=Url.ImageSrc(category, 0, "imagepardefaut.png")%>
		/// ]]>
		/// 	</code>
		/// </example>
		/// <remarks>
		/// si l'on veut garder la taille originale de l'image il faut passer 0 comme parametre de largeur
		/// si la marque n'a pas d'image , c'est l'image par defaut qui est retournée
		/// </remarks>
		/// <param name="helper">The helper.</param>
		/// <param name="productCategoryImage">The product category image.</param>
		/// <param name="width">The width.</param>
		/// <param name="defaultUrImage">The default ur image.</param>
		/// <returns></returns>
		public static string ImageSrc(this UrlHelper helper, Models.ProductCategory productCategoryImage, int width, string defaultUrImage)
		{
			return helper.ImageSrc(productCategoryImage, width, 0, defaultUrImage);
		}

		/// <summary>
		/// Retourne un element html img permettant d'afficher l'image par defaut d'une catégorie de produit
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="productCategoryImage">The product category image.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="defaultUrImage">The default ur image.</param>
		/// <returns></returns>
		/// <example>Exemple d'appel
		/// <code>
		/// 		<![CDATA[
		/// <%=Url.ImageSrc(category, 0, 0, "imagepardefaut.png")%>
		/// ]]>
		/// 	</code>
		/// </example>
		/// <remarks>
		/// si l'on veut garder la taille originale de l'image il faut passer 0 comme parametre de largeur
		/// si la marque n'a pas d'image , c'est l'image par defaut qui est retournée
		/// </remarks>
		public static string ImageSrc(this UrlHelper helper, Models.ProductCategory productCategoryImage, int width, int height, string defaultUrImage)
		{
			if (productCategoryImage != null && productCategoryImage.DefaultImage != null)
			{
				return string.Format(productCategoryImage.DefaultImage.Url, width, height);
			}
			else if (defaultUrImage != null)
			{
				return defaultUrImage;
			}
			return null;
		}

		/// <summary>
		/// Retourne un element html img permettant d'afficher l'image par defaut d'une marque 
		/// </summary>
		/// <example>Exemple d'appel
		/// <code>
		/// <![CDATA[
		/// <%=Html.BrandImage(brand, 0, "imagepardefaut.png")%>
		/// ]]>
		/// </code>
		/// </example>
		/// <remarks>
		/// si l'on veut garder la taille originale de l'image il faut passer 0 comme parametre de largeur
		/// si la marque n'a pas d'image , c'est l'image par defaut qui est retournée
		/// </remarks>
		/// <param name="helper">The helper.</param>
		/// <param name="brand">The brand.</param>
		/// <param name="width">The width.</param>
		/// <param name="defaultUrImage">The default ur image.</param>
		/// <returns></returns>
		public static MvcHtmlString BrandImage(this HtmlHelper helper, Models.Brand brand, int width, string defaultUrImage)
		{
			return helper.BrandImage(brand, width, 0, defaultUrImage);
		}

		/// <summary>
		/// Retourne un element html img permettant d'afficher l'image par defaut d'une marque
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="brand">The brand.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="defaultUrImage">The default ur image.</param>
		/// <returns></returns>
		/// <example>Exemple d'appel
		/// <code>
		/// 		<![CDATA[
		/// <%=Html.BrandImage(brand, 0, 0, "imagepardefaut.png")%>
		/// ]]>
		/// 	</code>
		/// </example>
		/// <remarks>
		/// si l'on veut garder la taille originale de l'image il faut passer 0 comme parametre de largeur
		/// si la marque n'a pas d'image , c'est l'image par defaut qui est retournée
		/// </remarks>
		public static MvcHtmlString BrandImage(this HtmlHelper helper, Models.Brand brand, int width, int height, string defaultUrImage)
		{
			if (brand != null && brand.DefaultImage != null)
			{
				string widthattb = (width == 0) ? "" : string.Format("width=\"{0}\"", width);
				string heightattb = (height == 0) ? "" : string.Format("height=\"{0}\"", height);
				return new MvcHtmlString(string.Format("<img src=\"{0}\" {1} {2} alt=\"{3}\" class=\"brand-image\" border=\"0\" />", string.Format(brand.DefaultImage.Url, width, height), widthattb, heightattb, brand.Name));
			}
			else if (defaultUrImage != null)
			{
				return new MvcHtmlString(string.Format("<img src=\"{0}\" alt=\"{1}\" class=\"brand-image\" border=\"0\" />", defaultUrImage, brand.Name));
			}
			return null;
		}

		/// <summary>
		/// Retourne l'url pour l'affichage de l'image d'une marque
		/// </summary>
		/// <example>Exemple d'appel
		/// <code>
		/// <![CDATA[<%=Url.ImageSrc(brand, 0, "imagepardefaut.png")%>]]>
		/// </code>
		/// </example>
		/// <remarks>
		/// Si la marque ne possede pas d'image, alors l'image par defaut est retournée
		/// </remarks>
		/// <param name="url">The URL.</param>
		/// <param name="brand">The brand.</param>
		/// <param name="width">The width.</param>
		/// <param name="defaultUrlImage">The default URL image.</param>
		/// <returns></returns>
		public static string ImageSrc(this UrlHelper url, Models.Brand brand, int width, string defaultUrlImage)
		{
			return url.ImageSrc(brand, width, 0, defaultUrlImage);
		}

		/// <summary>
		/// Retourne l'url pour l'affichage de l'image d'une marque
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="brand">The brand.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="defaultUrlImage">The default URL image.</param>
		/// <returns></returns>
		/// <example>Exemple d'appel
		/// <code>
		/// 		<![CDATA[<%=Url.ImageSrc(brand, 0, "imagepardefaut.png")%>]]>
		/// 	</code>
		/// </example>
		/// <remarks>
		/// Si la marque ne possede pas d'image, alors l'image par defaut est retournée
		/// </remarks>
		public static string ImageSrc(this UrlHelper url, Models.Brand brand, int width, int height, string defaultUrlImage)
		{
			if (brand != null && brand.DefaultImage != null)
			{
				return string.Format(brand.DefaultImage.Url, width, height);
			}
			else if (defaultUrlImage != null)
			{
				return defaultUrlImage;
			}
			return null;
		}

		/// <summary>
		/// Affiche la liste de tous groupes de propriétés etendus des produits
		/// </summary>
		/// <remarks>
		/// le fichier par defaut est ~/views/catalog/extendedpropertygrouplist.ascx
		/// la vue traite une liste de PropertyGroup
		/// </remarks>
		/// <example>Exemple d'appel
		/// <code>
		/// <![CDATA[
		/// <% Html.ShowProductExtendedPropertyGroupList(); %>
		/// ]]>
		/// </code>
		/// </example>
		/// 
		/// <param name="helper">The helper.</param>
		public static MvcHtmlString ShowProductExtendedPropertyGroupList(this HtmlHelper helper)
		{
			return helper.ShowProductExtendedPropertyGroupList("extendedpropertygrouplist");
		}

		/// <summary>
		/// Affiche la liste de tous groupes de propriétés etendus des produits dans la vue de son choix
		/// </summary>
		/// <remarks>
		/// La vue doit se trouver dans le repertoire ~/views/catalog/
		/// la vue traite une liste de PropertyGroup
		/// </remarks>
		/// <example>Exemple d'appel
		/// <code>
		/// <![CDATA[
		/// <% Html.ShowProductExtendedPropertyGroupList("nom de la vue.ascx"); %>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="viewName">Name of the view.</param>
		public static MvcHtmlString ShowProductExtendedPropertyGroupList(this HtmlHelper helper, string viewName)
		{
			return helper.Action("ShowProductExtendedPropertyList", "Catalog", new { viewName = viewName });
		}

		/// <summary>
		/// Affiche la liste de tous groupes de propriétés etendus des produits dans la vue de son choix
		/// en fonction du contexte (Rechechre / Categorie / Marque)
		/// </summary>
		/// <remarks>
		/// La vue doit se trouver dans le repertoire ~/views/catalog/
		/// la vue traite une liste de PropertyGroup
		/// </remarks>
		/// <example>Exemple d'appel
		/// <code>
		/// <![CDATA[
		/// <% Html.ShowContextualProductExtendedPropertyList("nom de la vue.ascx"); %>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="viewName">Name of the view.</param>
		public static MvcHtmlString ShowContextualProductExtendedPropertyList(this HtmlHelper helper, string viewName)
		{
			return helper.Action<Controllers.CatalogController>(i => i.ShowContextualProductExtendedPropertyList(viewName));
		}

		public static MvcHtmlString ShowContextualProductExtendedPropertyListByCategory(this HtmlHelper helper, string viewName, Models.ProductCategory category)
		{
			return helper.Action<Controllers.CatalogController>(i => i.ShowProductExtendedPropertyListByCategory(viewName, category));
		}

		/// <summary>
		/// Affiche la liste des marques associées a une categorie de produit via les produits
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="category">The category.</param>
		/// <param name="viewName">Name of the view.</param>
		public static MvcHtmlString ShowBrandListByProductCategory(this HtmlHelper helper, Models.ProductCategory category, string viewName)
		{
			return helper.Action<Controllers.CatalogController>(i => i.ShowBrandListByProductCategory(category, viewName));
		}

		/// <summary>
		/// Affiche la liste des categories de produit associées à une marque via les produits
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="brand">The brand.</param>
		/// <param name="viewName">Name of the view.</param>
		public static MvcHtmlString ShowProductCategoryListByBrand(this HtmlHelper helper, Models.Brand brand, string viewName)
		{
			return helper.Action<Controllers.CatalogController>(i => i.ShowProductCategoryListByBrand(brand, viewName));
		}

		/// <summary>
		/// Retourne une url pour une recherche donnée
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="search">The search.</param>
		/// <returns></returns>
		public static string ProductSearchUrl(this UrlHelper helper, string search)
		{
			var url = helper.RouteERPStoreUrl(ERPStoreRoutes.PRODUCT_SEARCH, null);
			return url + "?s=" + helper.Encode(search);
		}

		/// <summary>
		/// Affiche la liste des catagories distinctes associées aux produits recherchés 
		/// et injecte le resultat dans la vue partielle "viewName"
		/// </summary>
		/// <example>
		/// <code>
		/// <![CDATA[
		/// 
		/// Appel dans la page :
		/// <%Html.ShowProductCategoryListBySearch("categories.ascx", Request["s"]);%>
		/// 
		/// Appel Ajax via jQuery
		/// <div id="categories" class="xxx"/>
		/// <script type="text/javascript">
		/// $(document).ready(function(){
		///		$('#categories').load('/catalog/ShowProductCategoryListBySearch', { viewName: 'categories.ascx', search: '<%=Request["s"]%>' }, function(html) {
		///			$('#categories')[0].value = html;
		///		}
		///	});
		///	</script>
		/// le model pour la vue partielle "categorie.ascx" est IList<Models.ProductCategory>
		/// 
		/// ]]>
		/// </code>
		/// </example>
		/// <remarks>
		/// la vue partielle viewName doit se trouver dans le repertoire /views/catalog
		/// </remarks>
		/// <param name="helper">The helper.</param>
		/// <param name="viewName">Name of the view.</param>
		/// <param name="search">The search.</param>
		public static MvcHtmlString ShowProductCategoryListBySearch(this HtmlHelper helper, string viewName, string search)
		{
			return helper.Action<Controllers.CatalogController>(i => i.ShowProductCategoryListBySearch(viewName, search));
		}

		/// <summary>
		/// Affiche la liste des catagories distinctes associées aux produits affichés
		/// et injecte le resultat dans la vue partielle "viewName"
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="viewName">Name of the view.</param>
		/// <param name="listType">Type of the list.</param>
		/// <example>
		/// 	<![CDATA[
		/// Appel dans la page :
		/// <%Html.ShowProductCategoryListByProductList("categories.ascx");%>
		/// le model pour la vue partielle "categorie.ascx" est IList<Models.ProductCategory>
		/// ]]>
		/// </example>
		/// <remarks>
		/// la vue partielle viewName doit se trouver dans le repertoire /views/catalog
		/// </remarks>
		public static MvcHtmlString ShowProductCategoryListByProductList(this HtmlHelper helper, string viewName, Models.ProductListType listType)
		{
			var filter = CreateProductListFilter(helper);
			filter.ListType = listType;
			return helper.Action<Controllers.CatalogController>(i => i.ShowProductCategoryListByProductList(filter, viewName));
		}

		public static MvcHtmlString ShowProductCategoryListByProductList(this HtmlHelper helper, string viewName, Models.ProductList list)
		{
			var filter = CreateProductListFilter(helper);
			filter.ListType = list.ListType;
			if (!filter.BrandId.HasValue
				&& list.Brand != null)
			{
				filter.BrandId = list.Brand.Id;
			}
			if (!filter.ProductCategoryId.HasValue
				&& list.Category != null)
			{
				filter.ProductCategoryId = list.Category.Id;
			}

			return helper.Action<Controllers.CatalogController>(i => i.ShowProductCategoryListByProductList(filter, viewName));
		}

		/// <summary>
		/// Affiche la liste des marques distinctes associées aux produits recherchés
		/// et injecte le resultat dans la vue partielle "viewName"
		/// </summary>
		/// <example>
		/// <code>
		/// <![CDATA[
		/// 
		/// Appel dans la page :
		/// <%Html.ShowBrandListBySearch("brands.ascx", Request["s"]);%>
		/// 
		/// Appel Ajax via jQuery
		/// <div id="brands" class="xxx"/>
		/// <script type="text/javascript">
		/// $(document).ready(function(){
		///		$('#brands').load('/catalog/ShowBrandListBySearch', { viewName: 'brands.ascx', search: '<%=Request["s"]%>' }, function(html) {
		///			$('#brands')[0].value = html;
		///		}
		///	});
		///	</script>
		/// 
		/// le model pour la vue partielle "viewName" est IList<Models.Brand>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="viewName">Name of the view.</param>
		/// <param name="search">The search.</param>
		public static MvcHtmlString ShowBrandListBySearch(this HtmlHelper helper, string viewName, string search)
		{
			return helper.Action<Controllers.CatalogController>(i => i.ShowBrandListBySearch(viewName, search));
		}

		public static MvcHtmlString ShowBrandListByProductList(this HtmlHelper helper, string viewName, Models.ProductListType listType)
		{
			var filter = CreateProductListFilter(helper);
			filter.ListType = listType;
			return helper.Action<Controllers.CatalogController>(i => i.ShowBrandListByProductList(viewName, filter));
		}

		public static MvcHtmlString ShowBrandListByProductList(this HtmlHelper helper, string viewName, Models.ProductList list)
		{
			var filter = CreateProductListFilter(helper);
			filter.ListType = list.ListType;
			if (!filter.BrandId.HasValue
				&& list.Brand != null)
			{
				filter.BrandId = list.Brand.Id;
			}
			if (!filter.ProductCategoryId.HasValue
				&& list.Category != null)
			{
				filter.ProductCategoryId = list.Category.Id;
			}

			// helper.RenderAction("ShowBrandListByProductList", "Catalog", new { viewName = viewName, filter = filter });
			return helper.Action<Controllers.CatalogController>(i => i.ShowBrandListByProductList(viewName, filter));
		}

		/// <summary>
		/// Retourne la liste de toutes les marques pour une liste de selection
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="selectedBrandName">The selected brand id.</param>
		/// <returns></returns>
		/// <example>
		/// 	<code>
		/// 		<![CDATA[
		/// <%= Html.DropDownList("brand", Html.BrandList("currentBrandName"))%>
		/// ]]>
		/// 	</code>
		/// </example>
		public static IEnumerable<SelectListItem> BrandList(this HtmlHelper helper, string selectedBrandName)
		{
            var catalogService = DependencyResolver.Current.GetService<Services.ICatalogService>();

			return from brand in catalogService.GetBrands()
				   orderby brand.Name
				   select new SelectListItem()
				   {
					   Text = brand.Name,
					   Value = brand.Name,
					   Selected = (selectedBrandName != null) ? brand.Name.Equals(selectedBrandName, StringComparison.InvariantCultureIgnoreCase) : false,
				   };
		}

		/// <summary>
		/// Lien vers la page des produits les plus vendus
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string TopSellsProductListHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.TOP_SELLS, null);
		}

		/// <summary>
		/// Lien vers la page des nouveaux produits
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string NewProductsListHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.NEW_PRODUCTS, null);
		}

		/// <summary>
		/// Lien vers la page des produits en promotion
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string PromotionsProductListHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.PROMOTIONS, null);
		}

		/// <summary>
		/// Lien vers la page des produits en destockage
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string DestockProductListHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.DESTOCK, null);
		}

		/// <summary>
		/// Lien vers la page des produits les moins cher du marché
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string FirstPriceProductListHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.FIRST_PRICE, null);
		}

		/// <summary>
		/// Lien vers la page des marques
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string BrandsHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.BRANDS, null);
		}

		/// <summary>
		/// Lien vers la page des categories
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string ProductCategoriesHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.PRODUCT_CATEGORIES, null);
		}

		/// <summary>
		/// Affiche un rendu partiel de la liste des professions
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="viewName">Name of the view.</param>
		public static MvcHtmlString ShowProfessionList(this HtmlHelper helper, string viewName)
		{
			return helper.Action<Controllers.CatalogController>(i => i.ShowProfessionList(viewName));
		}

		/// <summary>
		/// Retourne le lien SEO vers un terme de recherche
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="searchTerm">The search term.</param>
		/// <returns></returns>
		public static string Href(this UrlHelper helper, Models.SearchTerm searchTerm)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.SEO_PRODUCT_SEARCH, new { s = searchTerm.Name, });
		}

		public static MvcHtmlString ShowTopSearchTermList(this HtmlHelper helper, string viewName, int count)
		{
			return helper.Action<Controllers.CatalogController>(i => i.ShowTopSearchTermList(viewName, count));
		}

		/// <summary>
		/// Retourne le lien pour l'affichage des informations concernant le stock d'un produit
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string ShowProductStockInfoHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.PRODUCT_STOCK_INFO);
		}

		private static Models.ProductListFilter CreateProductListFilter(this HtmlHelper helper)
		{
            var catalogService = DependencyResolver.Current.GetService<Services.ICatalogService>();

			var parameters = catalogService.RemoveNotFilteredParameters(helper.ViewContext.HttpContext.Request.Url.Query.ToHtmlDecodedNameValueDictionary());
			var filter = catalogService.CreateProductListFilter(helper.ViewContext.HttpContext);
			filter.ExtendedParameters = parameters;
			filter.Search = helper.ViewContext.HttpContext.Request["s"];

			var brandName = helper.ViewContext.HttpContext.Request["brand"];
			var brand = catalogService.GetBrandByName(brandName); 
			if (brand != null)
			{
				filter.BrandId = brand.Id;
			}

			var category = catalogService.GetCategoryByCode(helper.ViewContext.HttpContext.Request["category"]);
			if (category != null)
			{
				filter.ProductCategoryId = category.Id;
			}

			return filter;
		}
	}
}
