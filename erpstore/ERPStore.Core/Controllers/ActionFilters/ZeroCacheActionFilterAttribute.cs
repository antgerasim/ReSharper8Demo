using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace ERPStore.Controllers.ActionFilters
{
	/// <summary>
	/// Supprime tout cache dans le navigateur pour l'action marquée par cet attribut
	/// </summary>
	public sealed class ZeroCacheActionFilterAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);

			filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
			filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
			filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
			filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			filterContext.HttpContext.Response.Cache.SetNoStore();
		}
	}
}
