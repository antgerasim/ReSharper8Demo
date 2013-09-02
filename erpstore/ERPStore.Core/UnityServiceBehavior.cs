using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;

using Microsoft.Practices.Unity;

namespace ERPStore
{
	public class UnityServiceBehavior : IServiceBehavior
	{
		private ServiceHost m_ServiceHost;

		public UnityServiceBehavior(IUnityContainer container)
		{
			InstanceProvider = new UnityInstanceProvider(container);
		}

		internal UnityInstanceProvider InstanceProvider { get; set; }

		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
			{
				var cd = channelDispatcher as ChannelDispatcher;
				if (cd != null)
				{
					foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
					{
						InstanceProvider.ServiceType = serviceDescription.ServiceType;
						endpointDispatcher.DispatchRuntime.InstanceProvider = InstanceProvider;
					}
				}
			}
		}

		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		public void AddToHost(ServiceHost host)
		{
			if (m_ServiceHost != null)
			{
				return;
			}
			host.Description.Behaviors.Add(this);
			m_ServiceHost = host;
		}
	}
}
