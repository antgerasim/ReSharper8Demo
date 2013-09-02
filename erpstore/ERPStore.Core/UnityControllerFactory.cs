using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;

using Microsoft.Practices.Unity;

namespace ERPStore
{
	public class UnityControllerFactory : DefaultControllerFactory
	{
		public UnityControllerFactory()
		{
		}

		[Dependency]
		public IUnityContainer Container { get; set; }

		protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
			{
				throw new HttpException(404, String.Format("The controller for path '{0}' could not be found or it does not implement IController.",
									requestContext.HttpContext.Request.Path));
			}

			// Container.RegisterInstance(typeof(HttpContextBase), requestContext.HttpContext, new PerThreadLifetimeManager());

			var controller = Container.Resolve(controllerType) as Controller;
			controller.ActionInvoker = new UnityActionInvoker(Container);

			return controller;
		}
	}
}