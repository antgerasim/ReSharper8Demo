using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ERPStore
{
	public class UnityControllerActivator : IControllerActivator
	{
		public IController Create(System.Web.Routing.RequestContext requestContext, Type controllerType)
		{
			return DependencyResolver.Current.GetService(controllerType) as IController;


		}
	}
}
