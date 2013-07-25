using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Extensibility
{
	public interface IPluginLoaderService
	{
		void Load(PluginInfo[] pluginInfos, System.Web.HttpContextBase context, Models.WebSiteSettings webSiteSettings);
	}
}
