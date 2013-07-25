using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using Microsoft.Practices.Unity;

namespace ERPStore
{
	/// <summary>
	/// Fabrique de controllers dont la création est controlée par Unity
	/// </summary>
	public class ControllerFactory : IControllerFactory
	{
		[Dependency]
		public IUnityContainer Container { get; set; }

		#region IControllerFactory Members

		public IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
		{
			var controllerFactory = this.Container.Resolve<IControllerFactory>();
			var controller = controllerFactory.CreateController(requestContext, controllerName);

			return controller;
		}

		public void ReleaseController(IController controller)
		{
			var dispose = controller as IDisposable;
			if (dispose != null)
			{
				dispose.Dispose();
			}
		}

		#endregion
	}
}
