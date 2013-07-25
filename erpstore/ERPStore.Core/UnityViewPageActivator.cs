using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ERPStore
{
	public class UnityViewPageActivator : IViewPageActivator
	{
		public object Create(ControllerContext controllerContext, Type type)
		{
			return DependencyResolver.Current.GetService(type);
		}
	}
}
