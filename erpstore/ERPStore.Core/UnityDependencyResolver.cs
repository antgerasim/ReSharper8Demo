using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.Unity;

namespace ERPStore
{
	public class UnityDependencyResolver : System.Web.Mvc.IDependencyResolver
	{
		public UnityDependencyResolver(Microsoft.Practices.Unity.IUnityContainer container)
		{
			this.Container = container;
		}

		public Microsoft.Practices.Unity.IUnityContainer Container { get; private set; }

		public object GetService(Type serviceType)
		{
			object instance = null;
			try
			{
				instance = Container.Resolve(serviceType);
			}
			catch(Exception ex)
			{
				System.Diagnostics.Trace.TraceWarning(ex.ToString());
			}
			return instance;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			try
			{
				return Container.ResolveAll(serviceType);
			}
			catch
			{
				return new List<object>();
			}
		}
	}
}
