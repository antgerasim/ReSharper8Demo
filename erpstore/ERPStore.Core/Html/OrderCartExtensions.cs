using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ERPStore.Html
{
	/// <summary>
	/// Methodes d'extension pemettant la gestion d'affichage de tout ce qui est lié
	/// au panier de commande
	/// </summary>
	public static class OrderCartExtensions
	{
		/// <summary>
		/// Affiche le statut du panier dans un controle
		/// </summary>
		/// <remarks>
		/// Le controle utilisé se trouve ici : /views/cart/status.ascx
		/// </remarks>
		/// <example>Exemple de code d'appel
		/// <code>
		/// <![CDATA[<%Html.ShowStatus();%>]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		public static MvcHtmlString ShowCartStatus(this HtmlHelper helper)
		{
			return helper.ShowCartStatus("status");
		}

		/// <summary>
		/// Retourne le lien pour l'affichage du statut du panier de commande
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string CartStatusHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.SHOW_CART_STATUS, null);
		}

		/// <summary>
		/// Affiche le statut du panier dans un controle
		/// </summary>
		/// <remarks>
		/// Le controle doit etre present dans le repertoire /views/cart
		/// il faut indiquer l'extension du fichier
		/// </remarks>
		/// <example>Exemple de code d'appel
		/// <code>
		/// <![CDATA[<%Html.ShowStatus("le nom du controle.ascx");%>]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="viewName">Name of the view.</param>
		public static MvcHtmlString ShowCartStatus(this HtmlHelper helper, string viewName)
		{
			return helper.Action<Controllers.CartController>(c => c.ShowStatus(viewName));
		}

		/// <summary>
		/// Retourne un element html anchor contenant le lien vers la page du panier
		/// </summary>
		/// <example>
		/// <code>
		/// <![CDATA[<%=Html.CartLink("titre du lien");%>]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <returns>un elment anchor</returns>
		public static MvcHtmlString CartLink(this HtmlHelper helper, string title)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.CART, new { id = string.Empty }));
		}

		//[Obsolete("Utiliser Url.CartHref() a la place de cet appel", true)]
		//public static string CartUrl(this HtmlHelper helper)
		//{
		//    return helper.RouteLocalizedUrl(ERPStoreRoutes.CART, new { id = string.Empty });
		//}

		/// <summary>
		/// Retourne le lien vers la page du panier
		/// </summary>
		/// <example>Exemple d'appel
		/// <code>
		/// <![CDATA[<%=Url.CartHref()%>]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string CartHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.CART , new { id = string.Empty });
		}

		/// <summary>
		/// Retourne un lien vers le panier en cours
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="cart">The cart.</param>
		/// <returns></returns>
		public static string CurrentCartHref(this UrlHelper helper, Models.OrderCart cart)
		{
			if (cart == null)
			{
				return helper.RouteERPStoreUrl(ERPStoreRoutes.CART, new { id = string.Empty });
			}
			return helper.RouteERPStoreUrl(ERPStoreRoutes.CART_SHOW, new { cartId = cart.Code });
		}

		/// <summary>
		/// Retourne un element html form, permettant d'ajouter un produit dans le panier
		/// </summary>
		/// <example>
		/// <code>
		/// <![CDATA[<%Html.BeginAddToCartRouteForm();%>]]>
		/// ...
		/// Ici les données du formulaire
		/// ...
		/// <![CDATA[<%Html.EndForm();%>]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		public static void BeginAddToCartRouteForm(this HtmlHelper helper)
		{
			helper.BeginERPStoreRouteForm(ERPStoreRoutes.CART_ADD, FormMethod.Post);
		}

		/// <summary>
		/// Retourne un element html anchor permetant de vider tous les elements du panier
		/// </summary>
		/// <example>Exemple d'appel :
		/// <code>
		/// <![CDATA[<%=Html.ClearCartLink("titre du lien")%>]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		public static MvcHtmlString ClearCartLink(this HtmlHelper helper, string title)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.CART_CLEAR, new { action = "Clear" }));
		}

		/// <summary>
		/// Retourne l'url pour vider le panier 
		/// </summary>
		/// <example>Exemple d'appel :
		/// <code>
		/// <![CDATA[<%=Url.ClearCartHref()%>]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string ClearCartHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl( ERPStoreRoutes.CART_CLEAR, new { action = "Clear" });
		}

		//[Obsolete("Utliser Url.ClearCartHref() a la place de cet appel", true)]
		//public static string ClearCartUrl(this HtmlHelper helper)
		//{
		//    return helper.RouteLocalizedUrl(ERPStoreRoutes.CART_CLEAR, new { action = "Clear" });
		//}

		/// <summary>
		/// Retourne un element html form, permettant d'ajouter un produit dans le panier
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns>un element html form</returns>
		/// <example>
		/// 	<code>
		/// <![CDATA[<%=Html.BeginCartForm()%>]]>
		/// ...
		/// Ici les données du formulaire
		/// ...
		/// <![CDATA[<%Html.EndForm();%>]]>
		/// 	</code>
		/// </example>
		public static MvcForm BeginCartForm(this HtmlHelper helper)
		{
			return helper.BeginERPStoreRouteForm(ERPStoreRoutes.CART_RECALC, FormMethod.Post);
		}

		/// <summary>
		/// Retourne un element html form, permettant d'ajouter un produit dans le panier
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="formId">The form id.</param>
		/// <returns>un element html form</returns>
		/// <example>
		/// 	<code>
		/// 		<![CDATA[<%=Html.BeginCartForm("formId")%>]]>
		/// ...
		/// Ici les données du formulaire
		/// ...
		/// <![CDATA[<%Html.EndForm();%>]]>
		/// 	</code>
		/// </example>
		public static MvcForm BeginCartForm(this HtmlHelper helper, string formId)
		{
			return helper.BeginERPStoreRouteForm(ERPStoreRoutes.CART_RECALC, FormMethod.Post, new { id = formId });
		}

		/// <summary>
		/// Retourne un element html anchor permettant la suppression d'un item du panier
		/// </summary>
		/// <example>Exemple d'appel
		/// <code>
		///		<![CDATA[
		///		<%=Html.DeleteCartItemLink("titre du lien", 0)%> (permet la suppression de la premiere ligne)
		///		]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <param name="itemIndex">Index of the item.</param>
		/// <returns></returns>
		public static MvcHtmlString DeleteCartItemLink(this HtmlHelper helper, string title, int itemIndex)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.CART_ITEM_DELETE, new { index = itemIndex }));
		}

		//[Obsolete("Utiliser Url.DeleteCartItemHref(itemIndex) a la place de cet appel", true)]
		//public static string DeleteCartItemUrl(this HtmlHelper helper, string title, int itemIndex)
		//{
		//    return helper.RouteLocalizedUrl(ERPStoreRoutes.CART_ITEM_DELETE, new { index = itemIndex });
		//}

		/// <summary>
		/// Retourne l'url avec laquelle il est possible de supprimer un item du panier en fonction de sa position dans la list
		/// </summary>
		/// <example>Exemple d'appel
		/// <code>
		/// <![CDATA[
		/// <%=Url.DeleteCartItemHref(0)%> (Supprimer la première ligne du panier)
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="itemIndex">Index of the item.</param>
		/// <returns></returns>
		public static string DeleteCartItemHref(this UrlHelper helper, int itemIndex)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.CART_ITEM_DELETE, new { index = itemIndex });
		}

		/// <summary>
		/// Affiche la liste des paniers en cours (non converti en commande)
		/// </summary>
		/// <example>
		/// Exemple d'appel
		/// <code>
		/// <![CDATA[
		/// <%Html.ShowCurrentCartList();%>
		/// ]]>
		/// </code>
		/// </example>
		/// <remarks>
		/// Par defaut la vue se trouve dans /views/cart/cartlist.ascx
		/// </remarks>
		/// <param name="helper">The helper.</param>
		public static MvcHtmlString ShowCurrentCartList(this HtmlHelper helper)
		{
			return helper.ShowCurrentCartList("cartlist");
		}

		/// <summary>
		/// Affiche la liste des paniers en cours (non converti en commande)
		/// dans une vue donnée
		/// </summary>
		/// <example>
		/// Exemple d'appel
		/// <code>
		/// <![CDATA[
		/// <%Html.ShowCurrentCartList("nom de la vue.ascx");%>
		/// ]]>
		/// </code>
		/// </example>
		/// <remarks>
		/// La vue doit se trouver dans le repertoire /views/cart/
		/// </remarks>
		/// <param name="helper">The helper.</param>
		/// <param name="viewName">Name of the view.</param>
		public static MvcHtmlString ShowCurrentCartList(this HtmlHelper helper, string viewName)
		{
			return helper.Action<Controllers.CartController>(i => i.ShowCurrentCartList(viewName));
		}

		/// <summary>
		/// Retourne un element anchor permettant de supprimer un panier
		/// </summary>
		/// <example>
		/// Exemple d'appel :
		/// <code>
		/// <![CDATA[
		/// <%=Html.DeleteCartLink("supprimer", "xxxx")%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <param name="cartId">The cart id.</param>
		/// <returns></returns>
		public static MvcHtmlString DeleteCartLink(this HtmlHelper helper, string title, string cartId)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title,ERPStoreRoutes.CART_DELETE, new { cartId = cartId }));
		}

		/// <summary>
		/// Retourne l'url pour supprimer un panier donné
		/// </summary>
		/// <example>
		/// Exemple d'appel :
		/// <code>
		/// <![CDATA[
		/// <%=Url.DeleteCartHref("cartId")%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="cartId">The cart id.</param>
		/// <returns></returns>
		public static string DeleteCartHref(this UrlHelper helper, string cartId)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.CART_DELETE, new { cartId = cartId });
		}

		/// <summary>
		/// Retourne un element anchor permettant de changer de panier
		/// </summary>
		/// <example>
		/// Exemple d'appel :
		/// <code>
		/// <![CDATA[
		/// <%=Html.ChangeCartLink("supprimer", "xxxx")%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <param name="cartId">The cart id.</param>
		/// <returns></returns>
		public static MvcHtmlString ChangeCartLink(this HtmlHelper helper, string title, string cartId)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.CART_CHANGE, new { cartId = cartId }));
		}

		/// <summary>
		/// Retourne l'url pour changer de panier 
		/// </summary>
		/// <example>
		/// Exemple d'appel :
		/// <code>
		/// <![CDATA[
		/// <%=Url.ChangeCartHref("cartId")%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="cartId">The cart id.</param>
		/// <returns></returns>
		public static string ChangeCartHref(this UrlHelper helper, string cartId)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.CART_CHANGE, new { cartId = cartId });
		}

		/// <summary>
		/// Retourne un element anchor permettant de voir un panier enregistré
		/// </summary>
		/// <example>
		/// Exemple d'appel :
		/// <code>
		/// <![CDATA[
		/// <%=Html.ShowCartLink("supprimer", "xxxx")%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <param name="cartId">The cart id.</param>
		/// <returns></returns>
		public static MvcHtmlString ShowCartLink(this HtmlHelper helper, string title, string cartId)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.CART_SHOW, new { cartId = cartId }));
		}

		/// <summary>
		/// Retourne l'url pour changer de panier 
		/// </summary>
		/// <example>
		/// Exemple d'appel :
		/// <code>
		/// <![CDATA[
		/// <%=Url.ShowCartHref("cartId")%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="cartId">The cart id.</param>
		/// <returns></returns>
		public static string ShowCartHref(this UrlHelper helper, string cartId)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.CART_SHOW, new { cartId = cartId });
		}

		/// <summary>
		/// Retourne un element anchor permettant de commencer un nouveau panier
		/// </summary>
		/// <example>
		/// Exemple d'utilisation
		/// <code>
		/// <![CDATA[
		/// <%=Html.CreateNewCartLink("titre")%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		public static MvcHtmlString CreateNewCartLink(this HtmlHelper helper, string title)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.CART_CREATE, new { id = string.Empty }));
		}

		/// <summary>
		/// Retourne l'url pour creer un nouveau panier
		/// </summary>
		/// <example>
		/// Exemple d'utilisation :
		/// <code>
		/// <![CDATA[
		/// <%=Url.CreateNewCartHref()%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string CreateNewCartHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.CART_CREATE, new { id = string.Empty });
		}


		/// <summary>
		/// Determine si un panier de commande est complètement livrable
		/// </summary>
		/// <param name="cart">The cart.</param>
		/// <returns>
		/// 	<c>true</c> if [is fully deliverable] [the specified cart]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsFullyDeliverable(this Models.OrderCart cart)
		{
			if (cart == null
				|| cart.Items.IsNullOrEmpty())
			{
				return false;
			}

			var deliverable = true;

			foreach (var item in cart.Items)
			{
				if (item.ProductStockInfo == null)
				{
					deliverable =false;
					break;
				}
				if (item.Quantity >= item.ProductStockInfo.AvailableStock)
				{
					deliverable = false;
					break;
				}
			}
			return deliverable;
		}

		/// <summary>
		/// Affiche la liste des derniers items placés dans un panier
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="itemCount">The item count.</param>
		/// <param name="viewName">Name of the view.</param>
		public static MvcHtmlString ShowLastCartItemList(this HtmlHelper helper, int itemCount, string viewName)
		{
			return helper.Action<Controllers.CartController>(i => i.ShowLastCartItemList(itemCount, viewName));
		}

		/// <summary>
		/// Lien vers la page des derniers items ajoutés aux paniers de commande
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string LastCartItemHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.LAST_CART_ITEM_LIST);
		}

		/// <summary>
		/// Permet l'ajout direct d'un produit dans le panier en Get
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="productCode">The product code.</param>
		/// <returns></returns>
		public static string AddCartItemHref(this UrlHelper helper, string productCode)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.CART_ADD, new { ProductCode = productCode });
		}

		/// <summary>
		/// Affiche la liste des commentaires d'un panier
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="viewName">Name of the view.</param>
		public static MvcHtmlString ShowCommentListByCart(this HtmlHelper helper, Models.OrderCart cart, string viewName)
		{
			return helper.Action<Controllers.CartController>(a => a.ShowCommentByCart(cart, viewName));
		}
	}
}
