using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Extensibility
{
	public class PluginLoaderService : IPluginLoaderService
	{
		Microsoft.Practices.Unity.IUnityContainer m_Container;

		public PluginLoaderService(Logging.ILogger logger
			, Microsoft.Practices.Unity.IUnityContainer container)
		{
			this.Logger = logger;
			this.m_Container = container;
		}

		protected Logging.ILogger Logger { get; private set; }

		#region IPluginService Members

		public void Load(PluginInfo[] plugins, System.Web.HttpContextBase context, Models.WebSiteSettings webSiteSettings)
		{
			foreach (var plugin in plugins)
			{
				var asm = System.Reflection.Assembly.LoadFrom(plugin.AssemblyFile);
				var pluginMetaData = new PluginBuilder(asm, m_Container, context, webSiteSettings);

				pluginMetaData.LoadServices();
				pluginMetaData.InitializeModuleClasses();
				pluginMetaData.RegisterRoutes();
			}
		}

		#endregion

	}
}
