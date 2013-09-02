using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ERPStore
{
	public class ERPStoreViewEngine : WebFormViewEngine
	{
		public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
		{
			ViewEngineResult result = null;

			var browser = controllerContext.HttpContext.Request.Browser;

			if ((browser.IsMobileDevice 
				&& ERPStoreApplication.WebSiteSettings.AllowMobileViews)
				|| ERPStoreApplication.WebSiteSettings.ForceMobileViews)
			{
				result = base.FindView(controllerContext, string.Format("mobile/{0}", viewName), masterName, useCache);
			}
			
			return result ?? base.FindView(controllerContext, viewName, masterName, useCache);
		}
	}
}
