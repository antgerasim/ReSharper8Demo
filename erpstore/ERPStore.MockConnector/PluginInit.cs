using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.MockConnector
{
	public class PluginInit : ERPStore.Extensibility.PluginInit
	{
		public PluginInit(Microsoft.Practices.Unity.IUnityContainer container
	, System.Web.HttpContextBase context
	, ERPStore.Models.WebSiteSettings webSiteSettings)
			: base(context, container, webSiteSettings)
		{

		}

		public override void AddServices()
		{
			base.AddServices();
		}

		public override void RegisterRoutes()
		{
			base.RegisterRoutes();
		}

	}
}
