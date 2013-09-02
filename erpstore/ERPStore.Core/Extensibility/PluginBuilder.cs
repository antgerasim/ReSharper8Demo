using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

using Microsoft.Practices.Unity;

namespace ERPStore.Extensibility
{
	internal class PluginBuilder
	{
		Assembly assembly;
		bool loadedServices = false;
		bool initialized = false;
		bool routesRegistered = false;
		string name = null;
		Microsoft.Practices.Unity.IUnityContainer m_Container;
		System.Web.HttpContextBase m_HttpContext;
		Models.WebSiteSettings m_WebSiteSettings;

		List<Type> pluginTypes = new List<Type>();
		List<PluginInit> pluginClasses = new List<PluginInit>();

		public PluginBuilder(Assembly assembly
			, Microsoft.Practices.Unity.IUnityContainer container
			, System.Web.HttpContextBase context
			, Models.WebSiteSettings webSiteSettings)
		{
			this.assembly = assembly;
			this.m_Container = container;
			this.m_HttpContext = context;
			this.m_WebSiteSettings = webSiteSettings;

			foreach (Type type in assembly.GetExportedTypes())
			{
				if (!type.IsAbstract && typeof(PluginInit).IsAssignableFrom(type))
				{
					pluginTypes.Add(type);
				}
			}
		}

		public string Name
		{
			get
			{
				if (name == null)
					name = assembly.FullName;

				return name;
			}
			set { name = value; }
		}

		public void LoadServices()
		{
			if (loadedServices)
				return;

			loadedServices = true;
			EnsurePluginClassesExist();
			foreach (var pluginClass in pluginClasses)
			{
				pluginClass.AddServices();
			}
		}

		public void InitializeModuleClasses()
		{
			if (initialized)
				return;

			initialized = true;
			EnsurePluginClassesExist();

			foreach (PluginInit plugin in pluginClasses)
			{
				plugin.Load();
			}
		}

		public void RegisterRoutes()
		{
			if (routesRegistered)
			{
				return;
			}
			routesRegistered = true;
			foreach (PluginInit plugin in pluginClasses)
			{
				plugin.RegisterRoutes();
			}
		}

		private void EnsurePluginClassesExist()
		{
			if (pluginClasses.Count == pluginTypes.Count)
			{
				return;
			}

			foreach (Type pluginType in pluginTypes)
			{
				var pluginEnumeratorInjectionMembers = new InjectionMember[] 
				{
					new InjectionConstructor(
							m_Container
							, m_HttpContext
							, m_WebSiteSettings
					)
				};
				m_Container.RegisterType(pluginType, new ContainerControlledLifetimeManager(), pluginEnumeratorInjectionMembers);
				var plugin = m_Container.Resolve(pluginType) as PluginInit;
				pluginClasses.Add(plugin);
			}
		}
	}
}
