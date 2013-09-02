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
	/// Methodes d'extension permettant la manipulation d'affichage de tout
	/// ce qui est lié au produit
	/// </summary>
	public static class ProductExtensions
	{
		/// <summary>
		/// Retourne un element html anchor permettant d'atteindre la page du produit
		/// </summary>
		/// <remarks>
 		/// il faut passer le produit en paramètre, dans une boucle foreach il faudra passer "item"
		/// </remarks>
		/// <example>Voici un exemple d'appel :
		/// <code>
		/// <![CDATA[
		/// <%=Html.ProductLink(product)%>;
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <returns></returns>
		public static MvcHtmlString ProductLink(this HtmlHelper helper, Models.Product product)
		{
			return helper.ProductLink(product, product.Code);
		}

		/// <summary>
		/// Retourne un element html anchor permettant d'atteindre la page du produit
		/// </summary>
		/// <remarks>
		/// il faut passer le produit en paramètre, dans une boucle foreach il faudra passer "item"
		/// si l'on veut afficher un titre different de celui du produit il faut utiliser cette methode
		/// </remarks>
		/// <example>
		/// <code>
		/// <![CDATA[
		/// <%=Html.ProductLink(product, "titre du produit")%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <param name="title">The title.</param>
		/// <returns>element html anchor</returns>
		public static MvcHtmlString ProductLink(this HtmlHelper helper, Models.Product product, string title)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.PRODUCT, new { code = product.EncodedCode(), name = product.EncodedLink()}));
		}

		/// <summary>
		/// Retourne l'url pour atteindre la page du produit
		/// </summary>
		/// <remarks>
		/// il faut passer le produit en paramètre, dans une boucle foreach il faudra passer "item"
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// <%=Html.Href(product)%>
		/// ]]></code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <returns></returns>
		public static string Href(this UrlHelper helper, Models.Product product)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.PRODUCT, new { code = product.EncodedCode(), name = product.EncodedLink() });
		}

		/// <summary>
		/// Retourne un element html image permettant l'affichage de la photo par defaut d'un produit
		/// </summary>
		/// <remarks>
		/// il faut passer le produit en paramètre, dans une boucle foreach il faudra passer "item"
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// <%=Html.ProductImage(product)%>
		/// ]]></code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <returns></returns>
		public static MvcHtmlString ProductImage(this HtmlHelper helper, Models.Product product)
		{
			return helper.ProductImage(product, 0);
		}

		/// <summary>
		/// Retourne un element html image permettant l'affichage de la photo par defaut d'un produit,
		/// si le produit n'a pas de photo, on affiche la photo passé par défaut
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <param name="defaultUrlImage">The default URL image.</param>
		/// <returns></returns>
		/// <remarks>
		/// il faut passer le produit en paramètre, dans une boucle foreach il faudra passer "item"
		/// </remarks>
		/// <example>
		/// 	<code><![CDATA[
		/// <%=Html.ProductImage(product, "/content/images/imagepardefaut.gif")%>
		/// ]]></code>
		/// </example>
		public static MvcHtmlString ProductImage(this HtmlHelper helper, Models.Product product, string defaultUrlImage)
		{
			return helper.ProductImage(product, 0, defaultUrlImage);
		}

		/// <summary>
		/// Retourne un element html image permettant l'affichage de la photo par defaut d'un produit,
		/// </summary>
		/// <remarks>
		/// il faut passer le produit en paramètre, dans une boucle foreach il faudra passer "item"
		/// le deuxième parametre est la taille en largeur de l'image 
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// <%=Html.ProductImage(product, 100)%>
		/// ]]></code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <param name="width">The width.</param>
		/// <returns></returns>
		public static MvcHtmlString ProductImage(this HtmlHelper helper, Models.Product product, int width)
		{
			return helper.ProductImage(product, width, null);
		}

		/// <summary>
		/// Retourne un element html image permettant l'affichage de la photo par defaut d'un produit,
		/// si le produit n'a pas de photo, on affiche la photo passé par défaut
		/// </summary>
		/// <remarks>
		/// il faut passer le produit en paramètre, dans une boucle foreach il faudra passer "item"
		/// le deuxième parametre est la taille en largeur de l'image 
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// <%=Html.ProductImage(product, 100, "/content/images/imagepardefaut.gif")%>
		/// ]]></code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <param name="width">The width.</param>
		/// <param name="defaultUrlImage">The default URL image.</param>
		/// <returns></returns>
		public static MvcHtmlString ProductImage(this HtmlHelper helper, Models.Product product, int width, string defaultUrlImage)
		{
			return helper.ProductImage(product, width, 0, defaultUrlImage);
		}

		/// <summary>
		/// Retourne un element html image permettant l'affichage de la photo par defaut d'un produit,
		/// si le produit n'a pas de photo, on affiche la photo passé par défaut
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="defaultUrlImage">The default URL image.</param>
		/// <returns></returns>
		/// <remarks>
		/// il faut passer le produit en paramètre, dans une boucle foreach il faudra passer "item"
		/// le deuxième parametre est la taille en largeur de l'image
		/// </remarks>
		/// <example>
		/// 	<code><![CDATA[
		/// <%=Html.ProductImage(product, 100, "/content/images/imagepardefaut.gif")%>
		/// ]]></code>
		/// </example>
		public static MvcHtmlString ProductImage(this HtmlHelper helper, Models.Product product, int width, int height, string defaultUrlImage)
		{
			string widthattb = (width == 0) ? "" : string.Format("width=\"{0}\"", width);
			string heighattb = (height == 0) ? "" : string.Format("height=\"{0}\"", height);
			var categoryMedia = product.Category.GetFirstMedia();
			if (product != null && product.DefaultImage != null)
			{
				return new MvcHtmlString(string.Format("<img src=\"{0}\" {1} {2} alt=\"{3}\" class=\"product-image\" border=\"0\" />", string.Format(product.DefaultImage.Url, width, height), widthattb, heighattb, product.Title));
			}
			else if (categoryMedia != null)
			{
				return new MvcHtmlString(string.Format("<img src=\"{0}\" {1} {2} alt=\"{3}\" class=\"product-image\" border=\"0\" />", string.Format(categoryMedia.Url, width, height), widthattb, heighattb, product.Title));
			}
			else if (defaultUrlImage != null)
			{
				return new MvcHtmlString(string.Format("<img src=\"{0}\" alt=\"{1}\" class=\"product-image\" border=\"0\" />", defaultUrlImage, product.Title));
			}
			return new MvcHtmlString(string.Empty);
		}

		/// <summary>
		/// Products the image SRC.
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="productCode">The product code.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <returns></returns>
		public static string ProductImageSrc(this UrlHelper helper, string productCode, int width, int height)
		{
            var catalogService = DependencyResolver.Current.GetService<Services.ICatalogService>();
			var product = catalogService.GetProductByCode(productCode);
			return helper.ImageSrc(product, width, height, null);
		}

		/// <summary>
		/// Retourne l'url de l'image d'un produit
		/// si le produit n'a pas de photo, on affiche la photo passé par défaut
		/// </summary>
		/// <remarks>
		/// il faut passer le produit en paramètre, dans une boucle foreach il faudra passer "item"
		/// le deuxième parametre est la taille en largeur de l'image , si la valeur de la largeur
		/// est 0 alors la taille originale de l'image est conservée.
		/// </remarks>
		/// <example>
		/// <code>
		/// <![CDATA[
		/// <%=Url.ImageSrc(product, 100, "/content/images/imagepardefaut.gif")%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="url">The URL.</param>
		/// <param name="product">The product.</param>
		/// <param name="width">The width.</param>
		/// <param name="defaultUrlImage">The default URL image.</param>
		/// <returns></returns>
		public static string ImageSrc(this UrlHelper url, Models.Product product, int width, string defaultUrlImage)
		{
			return url.ImageSrc(product, width, 0, defaultUrlImage);
		}

		/// <summary>
		/// Retourne l'url de l'image d'un produit
		/// si le produit n'a pas de photo, on affiche la photo passé par défaut
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="product">The product.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="defaultUrlImage">The default URL image.</param>
		/// <returns></returns>
		/// <remarks>
		/// il faut passer le produit en paramètre, dans une boucle foreach il faudra passer "item"
		/// le deuxième parametre est la taille en largeur de l'image , si la valeur de la largeur
		/// est 0 alors la taille originale de l'image est conservée.
		/// le 3eme parametre est la hauteur
		/// </remarks>
		/// <example>
		/// 	<code>
		/// 		<![CDATA[
		/// <%=Url.ImageSrc(product, 100, 70, "/content/images/imagepardefaut.gif")%>
		/// ]]>
		/// 	</code>
		/// </example>
		public static string ImageSrc(this UrlHelper url, Models.Product product, int width, int height, string defaultUrlImage)
		{
			string src = null;
			if (product != null && product.DefaultImage != null)
			{
				src = string.Format(product.DefaultImage.Url, width, height);
			}
			else if (product.Category != null && product.Category.DefaultImage != null)
			{
				src = string.Format(product.Category.DefaultImage.Url, width, height);
			}
			else if (defaultUrlImage != null)
			{
				src = defaultUrlImage;
			}
			return src;
		}

		/// <summary>
		/// Insere la vue indiquée en passant en parametre les produits crossellé d'un produit en référence
		/// </summary>
		/// <remarks>
		/// La vue doit toujour se trouver dans le repertoire /views/catalog
		/// il faut indiquer l'extension ascx ou aspx
		/// si le produit n'a pas de resultat, une liste vide est passée à la vue
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// <%Html.ShowCrossSellingList(product, "nom de la vue.ascx");%>
		/// ]]></code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <param name="viewName">Name of the view.</param>
		public static MvcHtmlString ShowCrossSellingList(this HtmlHelper helper, Models.Product product, string viewName)
		{
			// helper.RenderPartial<Controllers.CatalogController>(m => m.ShowCrossSellingList(product.Id, viewName));
			return helper.ShowCrossSellingList(product, 5, viewName);
		}

		public static MvcHtmlString ShowCrossSellingList(this HtmlHelper helper, Models.Product product, int productCount, string viewName)
		{
			return helper.Action<Controllers.CatalogController>(c => c.ShowCrossSellingList( product.Id,  viewName,  productCount ));
		}

		/// <summary>
		/// Retourne des elements html indiquant le prix de vente d'un produit
		/// </summary>
		/// <remarks>
		/// si le produit à une unité de vente supérieure à 1 elle est indiquée
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// <%=Html.ShowPrice(product)%>
		/// ]]></code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <returns></returns>
		public static MvcHtmlString ShowPrice(this HtmlHelper helper, Models.Product product)
		{
			var price = string.Format("<span class=\"price-ip\">{0}</span><span class=\"price-comma\">,<span><span class=\"price-dp\">{1}</span>&nbsp;<span class=\"price-euro\">€</span>"
					, product.SalePrice.IntegerPart
					, product.SalePrice.DecimalPart);
			if (product.SaleUnitValue == 1)
			{
				return new MvcHtmlString(price);
			}
			else
			{
				return new MvcHtmlString(string.Format("{0} pour {1}"
					, price
					, product.SaleUnitValue));
			}
		}

		/// <summary>
		/// Retourne la valeur string du packaging d'un produit
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <returns></returns>
		public static MvcHtmlString ShowPackaging(this HtmlHelper helper, Models.Product product)
		{
			return new MvcHtmlString(string.Format("{0}", product.Packaging.Value));
		}

		/// <summary>
		/// Retourne la valeur string de la disponibilité d'un produit
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <returns></returns>
		public static MvcHtmlString GetDisponibility(this HtmlHelper helper, Models.Product product)
		{
            var catalogController = DependencyResolver.Current.GetService<Controllers.CatalogController>();
			string result = null;
			try
			{
				result = catalogController.GetProductDisponibility(product.Code);
			}
			catch
			{
				result = "Sur commande";
			}
			return new MvcHtmlString(result);
		}

		public static Models.ProductStockInfo GetProductStockInfo(this HtmlHelper helper, Models.Product product)
		{
            var catalogController = DependencyResolver.Current.GetService<Controllers.CatalogController>();
			var result = catalogController.GetProductStockInfoByCode(product.Code);
			return result;
		}

		/// <summary>
		/// Insere du code Ajax pour l'affichage de la disponibilité du produit
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// <%=Html.ShowProductDisponibility(Model)%>
		/// ]]></code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <param name="elmId">The elm id.</param>
		/// <returns></returns>
        // [Obsolete("use jscatalog.helper instead", false)]
        public static MvcHtmlString ShowProductDisponibility(this HtmlHelper helper, string viewName, Models.Product product)
		{
            //var script = "<script type=\"text/JavaScript\"><!--\r\n";
            //script += "GetProductDisponibility('" + product.Code.Replace("'", "''") + "','" + elmId + "');\r\n";
            //script += "-->\r\n";
            //script += "</script>";
            //return script;
            return helper.Action<Controllers.CatalogController>(c => c.ShowProductDisponibility(product, viewName));
		}

		/// <summary>
		/// Insere la vue indiquée en passant une liste de produit comme demandée par le paramètre type
		/// </summary>
		/// <remarks>
		/// la vue doit etre dans le repertoire /views/catalog
		/// il faut indiquer l'extension pour la vue ascx ou aspx
		/// 
		/// la liste des types est la suivante :
		/// 
		/// 
		/// Promotional (Les produits en promotion)
		/// New (Les nouveaux produits)
		/// Destock (Les produits en destockage)
		/// TopSell (Les produits les plus vendus)
		/// FirstPrice (Les produits avec le prix le plus bas du marché)
		/// 
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// <%Html.ShowProductList(Model.ProductListType.Promotional,"nom de la vue.ascx");%>
		/// ]]></code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="type">The type.</param>
		/// <param name="viewName">Name of the view.</param>
		public static MvcHtmlString ShowProductList(this HtmlHelper helper, Models.ProductListType type, string viewName)
		{
			return helper.ShowProductList(type, viewName, 0);
		}

		/// <summary>
		/// Insere la vue indiquée en passant une liste de produit comme demandée par le paramètre type
		/// et en ne retournant que le nombre maximum de produits indiqués
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="type">The type.</param>
		/// <param name="viewName">Name of the view.</param>
		/// <param name="productCount">The product count.</param>
		/// <remarks>
		/// la vue doit etre dans le repertoire /views/catalog
		/// il faut indiquer l'extension pour la vue ascx ou aspx
		/// la liste des types est la suivante :
		/// Promotional (Les produits en promotion)
		/// New (Les nouveaux produits)
		/// Destock (Les produits en destockage)
		/// TopSell (Les produits les plus vendus)
		/// FirstPrice (Les produits avec le prix le plus bas du marché)
		/// </remarks>
		/// <example>
		/// 	<code><![CDATA[
		/// <%Html.ShowProductList(Model.ProductListType.Promotional,"nom de la vue.ascx",10);%>
		/// ]]></code>
		/// </example>
		public static MvcHtmlString ShowProductList(this HtmlHelper helper, Models.ProductListType type, string viewName, int productCount)
		{
			return helper.ShowProductList(type, viewName, productCount, null);
		}

		public static MvcHtmlString ShowProductList(this HtmlHelper helper, Models.ProductListType type, string viewName, int productCount, List<Models.SortProductList> sort)
		{
			return helper.Action<Controllers.CatalogController>(i => i.ShowProductListView(type, viewName, productCount, sort));
		}

		/// <summary>
		/// Insere la bonne vue en fonction du type de tarif du produit
		/// </summary>
		/// <remarks>
		/// Si le produit est en promotion, alors la vue utilisée sera promotionnalprice.ascx
		/// si le produit est un meilleur prix marché alors la vue sera firstprice.ascx
		/// sinon la vue utilisée est basicprice.ascx
		/// </remarks>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		public static MvcHtmlString ShowProductPrice(this HtmlHelper helper, Models.Product product)
		{
            return helper.Action<Controllers.CatalogController>(c => c.ShowProductPrice(product));
		}

		/// <summary>
		/// Insere la vue des produits similaires a un produit donné
		/// </summary>
		/// <remarks>
		/// La vue doit se trouver dans le repertoire /views/catalog
		/// il faut indique l'extension de la vue
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// <%Html.ShowSimilarProductList(product, "nom de la vue.ascx");%>
		/// ]]></code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <param name="viewName">Name of the view.</param>
        [Obsolete("use ShowRelationalProductList instead", true)]
		public static MvcHtmlString ShowSimilarProductList(this HtmlHelper helper, Models.Product product, string viewName)
		{
			return helper.Action<Controllers.CatalogController>(c => c.ShowRelationalProductList(product.Code, viewName ));
		}

		/// <summary>
		/// Insere la vue des produits complémentaires a un produit donné
		/// </summary>
		/// <remarks>
		/// La vue doit se trouver dans le repertoire /views/catalog
		/// il faut indique l'extension de la vue
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// <%Html.ShowComplementaryProductList(product, "nom de la vue.ascx");%>
		/// ]]></code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <param name="viewName">Name of the view.</param>
        [Obsolete("use ShowRelationalProductList instead", true)]
		public static void ShowComplementaryProductList(this HtmlHelper helper, Models.Product product, string viewName)
		{
			// helper.RenderPartial<Controllers.CatalogController>(i => i.ShowRelationalProductList(Models.ProductRelationType.Complementary, product.Id, viewName));
			helper.RenderAction("ShowRelationalProductList", "Catalog", new { productRelationType = Models.ProductRelationType.Complementary, productRelationProductId = product.Id, productRelationViewName = viewName });
		}

		/// <summary>
		/// Insere la vue des variation de produits d'un produit donné
		/// </summary>
		/// <remarks>
		/// La vue doit se trouver dans le repertoire /views/catalog
		/// il faut indique l'extension de la vue
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// <%Html.ShowVariantProductList(product, "nom de la vue.ascx");%>
		/// ]]></code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <param name="viewName">Name of the view.</param>
        [Obsolete("use ShowRelationalProductList instead", true)]
		public static void ShowVariantProductList(this HtmlHelper helper, Models.Product product, string viewName)
		{
			// helper.RenderPartial<Controllers.CatalogController>(i => i.ShowRelationalProductList(Models.ProductRelationType.Variant, product.Id, viewName));
			helper.RenderAction("ShowRelationalProductList", "Catalog", new { productRelationType = Models.ProductRelationType.Variant, productRelationProductId = product.Id, productRelationViewName = viewName });
		}

		/// <summary>
		/// Insere la vue des produits de substitution d'un produit donné
		/// </summary>
		/// <remarks>
		/// La vue doit se trouver dans le repertoire /views/catalog
		/// il faut indique l'extension de la vue
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// <%Html.ShowSubstituteProductList(product, "nom de la vue.ascx");%>
		/// ]]></code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <param name="viewName">Name of the view.</param>
        [Obsolete("use ShowRelationalProductList instead", true)]
		public static void ShowSubstituteProductList(this HtmlHelper helper, Models.Product product, string viewName)
		{
			// helper.RenderPartial<Controllers.CatalogController>(i => i.ShowRelationalProductList(Models.ProductRelationType.Substitute, product.Id, viewName));
			helper.RenderAction("ShowRelationalProductList", "Catalog", new { productRelationType = Models.ProductRelationType.Substitute, productRelationProductId = product.Id, productRelationViewName = viewName });
		}

		/// <summary>
		/// Insere la vue des documents attachés d'un produit
		/// </summary>
		/// <remarks>
		/// La vue se trouve dans le repertoire /views/shared/medialist.ascx
		/// il faut indique l'extension de la vue
		/// Toutes les images sont exclues de la liste
		/// </remarks>
		/// <example>
		/// <![CDATA[
		/// <%Html.ShowAttachmentList(produt, "nom de la vue.ascx")%>
		/// ]]>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		public static MvcHtmlString ShowAttachmentList(this HtmlHelper helper, Models.Product product)
		{
			return helper.ShowAttachmentList(product, "_medialist");
		}

		public static string SimilarProductListHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.SIMILAR_PRODUCT_LIST,null);
		}

		public static string ComplementaryProductListHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.COMPLEMENTARY_PRODUCT_LIST, null);
		}

		public static string VariantProductListHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.VARIANT_PRODUCT_LIST, null);
		}

		public static string SubstituteProductListHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.SUBSTITUTE_PRODUCT_LIST, null);
		}

		public static string RelationsProductListHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.RELATIONS_PRODUCT_LIST, null);
		}

		public static string ContextualProductListHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.CONTEXTUAL_PRODUCT_LIST, null);
		}

		/// <summary>
		/// Insere la vue des documents attachés d'un produit
		/// </summary>
		/// <remarks>
		/// La vue doit se trouver dans le repertoire /views/shared
		/// il faut indique l'extension de la vue
		/// Toutes les images sont exclues de la liste
		/// </remarks>
		/// <example>
		/// <code>
		/// <![CDATA[
		/// <%Html.ShowAttachmentList(produt, "nom de la vue.ascx")%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <param name="viewName">Name of the view.</param>
		public static MvcHtmlString ShowAttachmentList(this HtmlHelper helper, Models.Product product, string viewName)
		{
			return helper.Action<Controllers.MediaController>(i => i.ShowAttachmentList(product, viewName));
			// helper.RenderAction("ShowAttachmentList", "Media", new { product = product, viewName = viewName });
		}

		/// <summary>
		/// Insere la vue des images attachées à un produit
		/// </summary>
		/// <remarks>
		/// La vue par defaut se trouve dans le repertoire /views/catalog/productpicturelist.ascx
		/// la taille par defaut des images est le maximum
		/// </remarks>
		/// <example>
		/// <code>
		/// <![CDATA[
		/// 
		/// <%Html.ShowPictureList(product);%>
		/// 
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		public static MvcHtmlString ShowPictureList(this HtmlHelper helper, Models.Product product)
		{
			return helper.ShowPictureList(product, 0, 0, 0,  "productpicturelist");
		}


		/// <summary>
		/// Insere la vue des images attachées à un produit
		/// </summary>
		/// <remarks>
		/// La vue par defaut se trouve dans le repertoire /views/catalog/productpicturelist.ascx
		/// </remarks>
		/// <example>
		/// <code>
		/// <![CDATA[
		/// 
		/// <%Html.ShowPictureList(product, 100);%>
		/// 
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <param name="width">The width.</param>
		public static MvcHtmlString ShowPictureList(this HtmlHelper helper, Models.Product product, int width)
		{
			return helper.ShowPictureList(product, width, 0, 0, "productpicturelist");
		}

		/// <summary>
		/// Insere la vue des images attachées à un produit
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="product">The product.</param>
		/// <param name="width">The width.</param>
		/// <param name="normalWidth">Width of the normal.</param>
		/// <param name="normalHeight">Height of the normal.</param>
		/// <param name="viewName">Name of the view.</param>
		/// <remarks>
		/// La vue doit se trouver dans le repertoire /views/catalog/productpicturelist.ascx
		/// </remarks>
		/// <example>
		/// 	<code>
		/// 		<![CDATA[
		/// <%Html.ShowPictureList(product, "nom de la vue.ascx");%>
		/// ]]>
		/// 	</code>
		/// </example>
		public static MvcHtmlString ShowPictureList(this HtmlHelper helper, Models.Product product, int width, int normalWidth, int normalHeight, string viewName)
		{
			return helper.Action<Controllers.CatalogController>(i => i.ShowPictureList(product, width, normalWidth, normalHeight, viewName));
		}

		/// <summary>
		/// Url pour la liste des produits déjà commandés par un client
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string CustomerProductListHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.CUSTOMER_PRODUCT, null);
		}

		public static string CrossProductListUrl(this UrlHelper helper, Models.Product product)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.CROSS_PRODUCT_LIST, null);
		}

		public static MvcHtmlString ShowToDiscoverProductList(this HtmlHelper helper, string viewName, List<Models.Product> exclusionList)
		{
			return helper.Action<Controllers.CatalogController>(c => c.ShowContextualProductList(viewName, exclusionList ));
		}

	}
}
