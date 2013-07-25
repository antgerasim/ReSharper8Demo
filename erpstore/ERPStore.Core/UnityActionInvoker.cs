using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using Microsoft.Practices.Unity;

namespace ERPStore
{
	public class UnityActionInvoker : ControllerActionInvoker
	{
		public UnityActionInvoker(IUnityContainer container)
		{
			this.Container = container;
		}

		protected IUnityContainer Container { get; private set; }

		protected override ActionExecutedContext InvokeActionMethodWithFilters(ControllerContext controllerContext, IList<IActionFilter> filters, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
		{
			foreach (var filter in filters)
			{
				Container.BuildUp(filter.GetType(), filter);
			}

			return base.InvokeActionMethodWithFilters(controllerContext, filters, actionDescriptor, parameters);
		}
	}
}
