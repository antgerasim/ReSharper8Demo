using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using Microsoft.Practices.Unity;

namespace ERPStore.Controllers.ActionFilters
{
	/// <summary>
	/// Filtre permettant de mettre en cache un rendu partiel
	/// </summary>
	[Obsolete("Use standard asp.net caching attribute instead",true)]
	public sealed class CacheActionFilterAttribute : ActionFilterAttribute
	{
		public CacheActionFilterAttribute()
		{

		}

		public string CacheKey { get; set; }

		public int Duration { get; set; }

		private string InternalKey { get; set; }

		public string VaryByParam { get; set; }

		private string GetKey(ActionExecutingContext filterContext)
		{
			var result = string.Format("{0}:{1}", filterContext.ActionDescriptor.ActionName, this.CacheKey);
			string[] prms = null;
			if (!string.IsNullOrEmpty(this.VaryByParam))
			{
				if (this.VaryByParam.Equals("*", StringComparison.InvariantCultureIgnoreCase))
				{
					prms = filterContext.ActionParameters.Select(i => i.Key.ToLower()).ToArray();
				}
				else if (this.VaryByParam.Equals("none", StringComparison.InvariantCultureIgnoreCase))
				{
					return result;
				}
				else
				{
					prms = this.VaryByParam.Trim().ToLower().Split(',');
				}
			}
			foreach (KeyValuePair<string, object> item in filterContext.ActionParameters)
			{
				if (prms != null && !prms.Contains(item.Key.ToLower()))
				{
					continue;
				}
				result += string.Format("{0}:{1}", item.Key, item.Value);
			}
			return result;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (ERPStoreApplication.WebSiteSettings.UseActionCache)
			{
				this.InternalKey = GetKey(filterContext);
				var cacheService = ERPStoreApplication.Container.Resolve<Services.ICacheService>();

				if (cacheService[InternalKey] != null && filterContext.HttpContext.Request["language"] == null)
				{
					// Setting the result prevents the action itself to be executed
					filterContext.Result = (ActionResult)cacheService[this.InternalKey];
				}
			}

			base.OnActionExecuting(filterContext);
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			if (ERPStoreApplication.WebSiteSettings.UseActionCache)
			{
				var cacheService = ERPStoreApplication.Container.Resolve<Services.ICacheService>();
				// Add the ActionResult to cache	
				cacheService.Add(this.InternalKey, filterContext.Result, DateTime.Now.AddSeconds(this.Duration));
			}
			base.OnActionExecuted(filterContext);
		}
	}
}
