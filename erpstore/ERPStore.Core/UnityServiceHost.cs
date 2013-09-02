using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

using Microsoft.Practices.Unity;

namespace ERPStore
{
	public class UnityServiceHost : ServiceHost
	{
		private IUnityContainer m_UnityContainer;

		public UnityServiceHost(Type serviceType, params Uri[] baseAddresses)
			: base(serviceType, baseAddresses)
		{
            var resolver = System.Web.Mvc.DependencyResolver.Current as UnityDependencyResolver;
            this.m_UnityContainer = resolver.Container;
		}

		protected override void OnOpening()
		{
			new UnityServiceBehavior(this.m_UnityContainer).AddToHost(this);
			base.OnOpening();
		}
	}
}
