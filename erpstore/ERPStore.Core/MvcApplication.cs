using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

using ERPStore.Html;

namespace ERPStore
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static ERPStoreApplication StoreApplication { get; private set; }

		protected void Application_Start()
		{
			StoreApplication = new ERPStoreApplication();
			StoreApplication.Start(Context);
		}

		protected void Application_End()
		{
			StoreApplication.Stop();
		}

		protected void Application_Error(object sender, EventArgs e)
		{
			StoreApplication.Error(Context);
		}

		protected void Application_AuthenticateRequest(object sender, EventArgs args)
		{
			StoreApplication.AuthenticateRequest(Context);
		}

		//public void Application_AcquireRequestState(object sender, EventArgs e)
		//{
		//    StoreApplication.AcquireRequestState(Context);
		//}

		protected void Application_BeginRequest(object sender, EventArgs args)
		{
			StoreApplication.BeginRequest(Context);
		}

		public override string GetVaryByCustomString(HttpContext context, string custom)
		{
			if (custom == "IsAnonymous")
			{
				return context.User.Identity.IsAuthenticated ? context.User.Identity.Name : "anon";
			}
			return base.GetVaryByCustomString(context, custom);
		}

	}
}
