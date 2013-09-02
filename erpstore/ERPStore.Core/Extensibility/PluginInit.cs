using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Extensibility
{
	public abstract class PluginInit
	{
		public PluginInit(System.Web.HttpContextBase context
			, Microsoft.Practices.Unity.IUnityContainer container
			, ERPStore.Models.WebSiteSettings webSiteSettings)
		{
			this.HttpContext = context;
			this.Container = container;
			this.WebSiteSettings = webSiteSettings;
		}

		protected System.Web.HttpContextBase HttpContext { get; set; }

		protected Microsoft.Practices.Unity.IUnityContainer Container { get; set; }

		protected ERPStore.Models.WebSiteSettings WebSiteSettings { get; set; }

		public virtual void Load()
		{
		}
		public virtual void AddServices()
		{
		}
		public virtual void RegisterRoutes()
		{
		}
	}
}
