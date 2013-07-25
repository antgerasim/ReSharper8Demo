using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Activation;

using Microsoft.Practices.Unity;

namespace ERPStore
{
	public class UnityServiceHostFactory : ServiceHostFactory
	{
		public UnityServiceHostFactory()
		{

		}

		protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
		{
			var pathAndQuery = baseAddresses.First().PathAndQuery;
			var defaultAddress = new Uri(string.Format("http://{0}{1}", ERPStoreApplication.WebSiteSettings.DefaultUrl, pathAndQuery));
			var addresses = new Uri[] { defaultAddress };
			var serviceHost = new UnityServiceHost(serviceType, addresses);
			return serviceHost;
		}
	}
}
