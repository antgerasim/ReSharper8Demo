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
	/// au panier de type devis
	/// </summary>
	public static class QuoteCartExtensions
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
		public static MvcHtmlString ShowQuoteCartStatus(this HtmlHelper helper)
		{
			return helper.ShowQuoteCartStatus("status");
		}

		/// <summary>
		/// Retourne le lien vers l'affichage du statut d'un panier de type devis
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string QuoteCartStatusHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.SHOW_QUOTECART_STATUS, null);
		}

		/// <summary>
		/// Affiche le statut du panier de type devis dans un controle
		/// </summary>
		/// <remarks>
		/// Le controle doit etre present dans le repertoire /views/quote
		/// il faut indiquer l'extension du fichier
		/// </remarks>
		/// <example>Exemple de code d'appel
		/// <code>
		/// <![CDATA[<%Html.ShowStatus("le nom du controle.ascx");%>]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="viewName">Name of the view.</param>
		public static MvcHtmlString ShowQuoteCartStatus(this HtmlHelper helper, string viewName)
		{
			return helper.Action<Controllers.QuoteCartController>(c => c.ShowStatus(viewName));
		}

		/// <summary>
		/// Retourne un element html anchor contenant le lien vers la page du panier de type devis
		/// </summary>
		/// <example>
		/// <code>
		/// <![CDATA[<%=Html.CartLink("titre du lien");%>]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <returns>un elment anchor</returns>
		public static MvcHtmlString QuoteCartLink(this HtmlHelper helper, string title)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.QUOTECART, new { id = string.Empty }));
		}

		/// <summary>
		/// Retourne le lien vers la page du panier de type devis
		/// </summary>
		/// <example>Exemple d'appel
		/// <code>
		/// <![CDATA[<%=Url.CartHref()%>]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string QuoteCartHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.QUOTECART, new { id = string.Empty });
		}

		/// <summary>
		/// Retourne un element html form, permettant d'ajouter un produit dans le panier de type devis
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
		public static MvcForm BeginAddToQuoteCartRouteForm(this HtmlHelper helper)
		{
			return helper.BeginERPStoreRouteForm(ERPStoreRoutes.QUOTECART_ADD_ITEM, FormMethod.Post);
		}

		/// <summary>
		/// Retourne un element html anchor permetant de vider tous les elements du panier
		/// de type devis
		/// </summary>
		/// <example>Exemple d'appel :
		/// <code>
		/// <![CDATA[<%=Html.ClearCartLink("titre du lien")%>]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		public static MvcHtmlString ClearQuoteCartLink(this HtmlHelper helper, string title)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.QUOTECART_CLEAR, new { action = "Clear" }));
		}

		/// <summary>
		/// Retourne l'url pour vider le panier de type devis
		/// </summary>
		/// <example>Exemple d'appel :
		/// <code>
		/// <![CDATA[<%=Url.ClearCartHref()%>]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string ClearQuoteCartHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.QUOTECART_CLEAR, new { action = "Clear" });
		}

		/// <summary>
		/// Retourne un element html form, permettant d'ajouter un produit dans le panier
		/// de type devis
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
		public static MvcForm BeginQuoteCartForm(this HtmlHelper helper)
		{
			return helper.BeginERPStoreRouteForm(ERPStoreRoutes.QUOTECART, FormMethod.Post);
		}

		public static MvcForm BeginQuoteCartForm(this HtmlHelper helper, string formId)
		{
			return helper.BeginERPStoreRouteForm(ERPStoreRoutes.QUOTECART, FormMethod.Post, new { id = formId, });
		}

		/// <summary>
		/// Retourne un element html anchor permettant la suppression d'un item du panier
		/// de type devis
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
		public static MvcHtmlString DeleteQuoteCartItemLink(this HtmlHelper helper, string title, int itemIndex)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.QUOTECART_ITEM_DELETE, new { index = itemIndex }));
		}

		/// <summary>
		/// Retourne l'url avec laquelle il est possible de supprimer un item du panier de type devis 
		/// en fonction de sa position dans la liste
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
		public static string DeleteQuoteCartItemHref(this UrlHelper helper, int itemIndex)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.QUOTECART_ITEM_DELETE, new { index = itemIndex });
		}

		/// <summary>
		/// Affiche la liste des paniers de type devis en cours (non converti en commande)
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
		public static MvcHtmlString ShowCurrentQuoteCartList(this HtmlHelper helper)
		{
			return helper.ShowCurrentQuoteCartList("cartlist");
		}

		/// <summary>
		/// Affiche la liste des paniers de type devis en cours (non converti en commande)
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
		public static MvcHtmlString ShowCurrentQuoteCartList(this HtmlHelper helper, string viewName)
		{
			return helper.Action<Controllers.QuoteCartController>(i => i.ShowCurrentCartList(viewName));
		}

		/// <summary>
		/// Retourne un element anchor permettant de supprimer un panier de type devis
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
		public static MvcHtmlString DeleteQuoteCartLink(this HtmlHelper helper, string title, string cartId)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.QUOTECART_DELETE, new { cartId = cartId }));
		}

		/// <summary>
		/// Retourne l'url pour supprimer un panier de type devis donné
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
		public static string DeleteQuoteCartHref(this UrlHelper helper, string cartId)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.QUOTECART_DELETE, new { cartId = cartId });
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
		public static MvcHtmlString ChangeQuoteCartLink(this HtmlHelper helper, string title, string cartId)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.QUOTECART_CHANGE, new { cartId = cartId }));
		}

		/// <summary>
		/// Retourne l'url pour changer de panier de type devis
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
		public static string ChangeQuoteCartHref(this UrlHelper helper, string cartId)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.QUOTECART_CHANGE, new { cartId = cartId });
		}

		/// <summary>
		/// Retourne un element anchor permettant de voir un panier de type devis enregistré
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
		public static MvcHtmlString ShowQuoteCartLink(this HtmlHelper helper, string title, string cartId)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.QUOTECART_SHOW, new { cartId = cartId }));
		}

		/// <summary>
		/// Retourne l'url pour changer de panier de type devis
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
		public static string ShowQuoteCartHref(this UrlHelper helper, string cartId)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.QUOTECART_SHOW, new { cartId = cartId });
		}

		/// <summary>
		/// Retourne un element anchor permettant de commencer un nouveau panier de type devis
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
		public static MvcHtmlString CreateNewQuoteCartLink(this HtmlHelper helper, string title)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.QUOTECART_CREATE, new { id = string.Empty }));
		}

		/// <summary>
		/// Retourne l'url pour creer un nouveau panier de type devis
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
		public static string CreateNewQuoteCartHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.QUOTECART_CREATE, new { id = string.Empty });
		}
	}
}
