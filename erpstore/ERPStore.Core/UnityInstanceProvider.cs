using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;

using Microsoft.Practices.Unity;

namespace ERPStore
{
	internal class UnityInstanceProvider : IInstanceProvider
	{
		private readonly IUnityContainer container;

		public UnityInstanceProvider(IUnityContainer container)
		{
			this.container = container;
		}

		public Type ServiceType { get; set; }

		public object GetInstance(InstanceContext instanceContext)
		{
			return GetInstance(instanceContext, null);
		}

		public object GetInstance(InstanceContext instanceContext, Message message)
		{
			return container.Resolve(ServiceType);
		}

		public void ReleaseInstance(InstanceContext instanceContext, object instance)
		{
			container.Teardown(instance);
		}
	}
}
