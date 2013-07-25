using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.Mvc;

using Microsoft.Practices.Unity;

namespace ERPStore.Controllers.ActionFilters
{
	/// <summary>
	/// Indique qu'une action est accessible uniquement à un adminsitrateur du site
	/// </summary>
	public sealed class AuthorizeActionFilter : ActionFilterAttribute
	{
		public AuthorizeActionFilter()
		{
		}

		[Dependency]
		public Services.CryptoService CryptoService { get; set; }

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			// Recherche du cookie
			var cookie = filterContext.HttpContext.Request.Cookies["AdminErpStore"];
			string url = string.Format("/admin/authenticate?returnUrl={0}", filterContext.HttpContext.Request.RawUrl);
			if (cookie == null)
			{
				filterContext.HttpContext.Response.Redirect(url);
				return;
			}
			var encryptedKey = cookie.Value;

			string apiToken = null;
			DateTime expirationDate = DateTime.MinValue;

			try
			{
				this.CryptoService.DecryptAdminKey(encryptedKey, out apiToken, out expirationDate);
			}
			catch
			{
				// suppression du cookie
				cookie.Expires = DateTime.Now.AddDays(-1);
				filterContext.HttpContext.Response.AppendCookie(cookie);
				filterContext.HttpContext.Response.Redirect(url);
				return;
			}

			if (!apiToken.Equals(ERPStoreApplication.WebSiteSettings.ApiToken, StringComparison.InvariantCultureIgnoreCase)
				|| expirationDate < DateTime.Now)
			{
				filterContext.HttpContext.Response.Redirect(url);
				return;
			}
			base.OnActionExecuting(filterContext);
		}
	}
}
