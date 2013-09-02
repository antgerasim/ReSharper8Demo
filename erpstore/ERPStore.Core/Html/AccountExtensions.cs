using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Mvc.Ajax;

namespace ERPStore.Html
{
	/// <summary>
	/// Methodes d'extensions permettant la gestion de tout ce qui est lié
	/// au compte utilisateur du site
	/// </summary>
	public static class AccountExtensions
	{
		/// <summary>
		/// Permet l'affichage du statut d'un visiteur
		/// <remarks>
		/// Va chercher le controle ~/Views/Account/Status.ascx et passe le modele "User"
		/// </remarks>
		/// <example>
		/// <![CDATA[
		/// <%Html.ShowAccountStatus()%>
		/// ]]>
		/// </example>
		/// </summary>
		/// <param name="helper">The helper.</param>
		public static void ShowAccountStatus(this HtmlHelper helper)
		{
			helper.ShowAccountStatus("status");
		}

		/// <summary>
		/// Permet l'affichage du statut d'un visiteur
		/// 
		/// </summary>
		/// <remarks>
		/// Va chercher le controle dans le repertoire ~/Views/Account/ 
		/// donc ne pas passer les noms de repertoire en paramètre
		/// </remarks>
		/// <example>
		/// <![CDATA[
		/// <%Html.ShowAccountStatus("nom de la vue.ascx")%>
		/// ]]>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="viewName">Name of the view.</param>
		public static MvcHtmlString ShowAccountStatus(this HtmlHelper helper, string viewName)
		{
			return helper.Action<Controllers.AccountController>(c => c.ShowStatus(viewName));
		}

        public static MvcHtmlString ShowAccountStatus(this HtmlHelper helper, string anonymousViewName, string connectedViewName)
        {
            string viewName = helper.ViewContext.HttpContext.User.Identity.IsAuthenticated ? connectedViewName : anonymousViewName;
            return helper.Action<Controllers.AccountController>(c => c.ShowStatus(viewName));
        }


		/// <summary>
		/// Retourne un anchor pointant vers la page de gestion du compte
		/// </summary>
		/// <example>
		/// <![CDATA[
		/// <%=Html.AccountLink("titre du lien")%>
		/// ]]>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		public static MvcHtmlString AccountLink(this HtmlHelper helper, string title)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.ACCOUNT, new { id = string.Empty }));
		}

		/// <summary>
		/// Lien vers la page de gestion du compte
		/// </summary>
		/// <example>
		/// <![CDATA[
		/// <%=Url.AccountHref()%>
		/// ]]>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string AccountHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.ACCOUNT, new { id = string.Empty });
		}

		/// <summary>
		/// Lien vers la page de la société d'un contact
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string EditCorporateHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.EDIT_CORPORATE, null);
		}

		/// <summary>
		/// Addresses the list href.
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string EditAddressListHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.EDIT_ADDRESS_LIST, null);
		}

		/// <summary>
		/// Retourne un lien pointant vers la page du compte
		/// </summary>
		/// <example>
		/// <![CDATA[
		/// <%=Html.RegisterAccountLink("texte du lien")%>
		/// ]]>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		public static MvcHtmlString RegisterAccountLink(this HtmlHelper helper, string title)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title,ERPStoreRoutes.REGISTER_ACCOUNT, new { action = "Register", ReturnUrl = System.Web.HttpContext.Current.Request["ReturnUrl"] }));
		}

		public static MvcHtmlString RegisterAccountHref(this UrlHelper helper)
		{
			return new MvcHtmlString(helper.RouteERPStoreUrl(ERPStoreRoutes.REGISTER_ACCOUNT, new { action = "Register", ReturnUrl = helper.RequestContext.HttpContext.Request["ReturnUrl"] }));
		}

		public static MvcHtmlString RegisterBillingAddressHref(this UrlHelper helper)
		{
			return new MvcHtmlString(helper.RouteERPStoreUrl(ERPStoreRoutes.REGISTER_BILLING_ADDRESS, null));
		}

		public static MvcHtmlString RegisterAccountCorporateHref(this UrlHelper helper)
		{
			return new MvcHtmlString(helper.RouteERPStoreUrl(ERPStoreRoutes.REGISTER_ACCOUNT_CORPORATE, null));
		}

		/// <summary>
		/// Retourne un element de type anchor (page de connexion)
		/// </summary>
		/// <example>
		/// <code>
		/// <![CDATA[
		/// 
		/// <%=Html.LoginLink("connexion")%>
		/// 
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		public static MvcHtmlString LoginLink(this HtmlHelper helper, string title)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.ACCOUNT_LOGIN, null));
		}

		/// <summary>
		/// Retourne l'url vers la page de connexion
		/// </summary>
		/// <example>
		/// <code>
		/// <![CDATA[
		/// 
		/// <a href="<%=Url.LoginHref%>">Connexion</a>
		/// 
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string LoginHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.ACCOUNT_LOGIN, null);
		}

		/// <summary>
		/// Retourne un anchor permettant de se deconnecter
		/// <example>
		/// <![CDATA[
		/// <%=Html.LogoffLink("texte du lien")%>
		/// ]]>
		/// </example>
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		public static MvcHtmlString LogoffLink(this HtmlHelper helper, string title)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.LOGOFF, new { id = string.Empty }));
		}

		/// <summary>
		/// Lien vers la page de deconnexion
		/// </summary>
		/// <example>Exemple d'appel
		/// <code>
		/// <![CDATA[
		/// <%=Url.LogoffHref()%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string LogoffHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.LOGOFF);
		}

		/// <summary>
		/// Lien de deconnexion, redirect vers la page indiquée
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="returnUrl">The return URL.</param>
		/// <returns></returns>
		/// <example>Exemple d'appel
		/// <code>
		/// 		<![CDATA[
		/// <%=Url.LogoffHref("/compte")%>
		/// ]]>
		/// 	</code>
		/// </example>
		public static string LogoffHref(this UrlHelper helper, string returnUrl)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.LOGOFF, new { returnUrl = returnUrl });
		}

		/// <summary>
		/// Retourne un anchor permettant d'editer une adresse
		/// </summary>
		/// <example>
		/// <code>
		/// <![CDATA[
		/// <%=Html.EditAddressLink(AddressNumber, "texte du lien")%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="id">The id.</param>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		public static MvcHtmlString EditAddressLink(this HtmlHelper helper, int id, string title)
		{
			var url = System.Web.HttpUtility.UrlEncode(System.Web.HttpContext.Current.Request.Url.PathAndQuery);
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.EDIT_ADDRESS, new { addressId = id, returnUrl = url }));
		}

		/// <summary>
		/// Retourne l'url d'edition d'une adresse d'un utilisateur
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// <%=Url.EditAddressHref(AddressNumber)%>
		/// ]]></code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public static string EditAdressHref(this UrlHelper helper, int id)
		{
			var url = System.Web.HttpUtility.UrlEncode(System.Web.HttpContext.Current.Request.Url.PathAndQuery);
			return helper.RouteERPStoreUrl(ERPStoreRoutes.EDIT_ADDRESS, new { addressId = id, returnUrl = url });
		}

		/// <summary>
		/// Modification des informations liées à l'utilisateur en cours
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string EditUserHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.EDIT_USER, null);
		}

		/// <summary>
		/// Retourne un anchor permettant d'aller sur la page de récuperation du mot de passe
		/// </summary>
		/// <example>
		/// <![CDATA[
		/// <%=Html.RecoverPasswordLink("texte du lien")%>
		/// ]]>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		public static MvcHtmlString RecoverPasswordLink(this HtmlHelper helper, string title)
		{
			var url = System.Web.HttpUtility.UrlEncode(System.Web.HttpContext.Current.Request.Url.PathAndQuery);
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.ACCOUNT_RECOVER_PASSWORD, new { returnUrl = url }));
		}

		public static string RecoverPasswordHref(this UrlHelper helper)
		{
			var url = System.Web.HttpUtility.UrlEncode(System.Web.HttpContext.Current.Request.Url.PathAndQuery);
			return helper.RouteERPStoreUrl(ERPStoreRoutes.ACCOUNT_RECOVER_PASSWORD, new { returnUrl = url });
		}

		/// <summary>
		/// Retourne le nom complet de l'utilisateur
		/// </summary>
		/// <remarks>
		/// Si l'utilisateur est anonyme une chaine vide est retournée
		/// </remarks>
		/// <example> Exemple d'appel
		/// <![CDATA[
		/// <%=Html.AccountFullName()%>
		/// ]]>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string UserFullName(this HtmlHelper helper)
		{
			var principal = System.Web.HttpContext.Current.User as ERPStore.Models.UserPrincipal;
			if (principal != null && principal.CurrentUser != null)
			{
				return principal.CurrentUser.FullName;
			}
			return string.Empty;
		}

		/// <summary>
		/// Genere un element Html form pour logger un utilisateur
		/// </summary>
		/// <example>Exemple d'appel
		/// <![CDATA[
		/// <%Html.BeginLoginForm();%>
		/// ]]>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static MvcForm BeginLoginForm(this HtmlHelper helper)
		{
			return helper.BeginERPStoreRouteForm(ERPStoreRoutes.ACCOUNT_LOGIN, FormMethod.Post);
		}

		/// <summary>
		/// Genere un élément HTML Form pour editer un utilisateur
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static MvcForm BeginEditUserForm(this HtmlHelper helper)
		{
			return helper.BeginERPStoreRouteForm(ERPStoreRoutes.EDIT_USER, FormMethod.Post);
		}

		/// <summary>
		/// Genere un élément HTML Form pour éditer une société
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static MvcForm BeginEditCorporateForm(this HtmlHelper helper)
		{
			return helper.BeginERPStoreRouteForm(ERPStoreRoutes.EDIT_CORPORATE, FormMethod.Post);
		}

		/// <summary>
		/// Genere un élément HTML Form pour pouvoir modifier un mot de passe
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static MvcForm BeginChangePasswordForm(this HtmlHelper helper)
		{
			return helper.BeginERPStoreRouteForm(ERPStoreRoutes.EDIT_PASSWORD, FormMethod.Post);
		}

		/// <summary>
		/// Genere un élément HTML Form pour pouvoir modifier une adresse
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static MvcForm BeginEditAddressForm(this HtmlHelper helper)
		{
			return helper.BeginERPStoreRouteForm(ERPStoreRoutes.ACCOUNT_EDIT_ADDRESS, FormMethod.Post);
		}

		/// <summary>
		/// Genere un formulaire de type ajax pour l'edition des adresses
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public static MvcForm BeginEditAddressForm(this AjaxHelper helper, AjaxOptions options)
		{
			return helper.BeginERPStoreRouteForm(ERPStoreRoutes.AJAX_EDIT_ADDRESS, options);
		}

		/// <summary>
		/// Genere un élément HTML Form permetant de supprimer une adresse de livraison
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static MvcForm BeginDeleteAddressForm(this HtmlHelper helper)
		{
			return helper.BeginERPStoreRouteForm(ERPStoreRoutes.ACCOUNT_DELETE_ADDRESS, FormMethod.Post);
		}


        /// <summary>
        /// Genere un élément HTML Form permetant de l'enregistrement d'un client
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <returns></returns>
        public static MvcForm BeginRegistrationForm(this HtmlHelper helper)
        {
            return helper.BeginERPStoreRouteForm(ERPStoreRoutes.REGISTER_ACCOUNT, FormMethod.Post);
        }

		/// <summary>
		/// Retourne l'url pour acceder au tableau de bord d'un
		/// client connecté
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string UserDashboardHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.USER_DASHBOARD, null);
		}

		/// <summary>
		/// Edits the password href.
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string EditPasswordHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.EDIT_PASSWORD, null);
		}

        public static MvcHtmlString ShowAjaxLoginBox(this HtmlHelper helper, string loginView, string loggedView)
        {
            if (helper.ViewContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var user = helper.ViewContext.HttpContext.User.GetUserPrincipal().CurrentUser;
                return helper.Partial(loggedView, user);
            }
            return helper.Partial(loginView);
        }

        public static MvcHtmlString ShowAccountMenu(this HtmlHelper helper, string view)
        {
            return helper.Action<Controllers.AccountController>(c => c.ShowAccountMenu(view));
        }
	}
}
