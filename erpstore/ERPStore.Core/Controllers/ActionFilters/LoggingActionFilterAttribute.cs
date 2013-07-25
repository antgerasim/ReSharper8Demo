using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace ERPStore.Controllers.ActionFilters
{
	/// <summary>
	/// Permet de logger une methode
	/// </summary>
	public sealed class LogFilterAttribute : ActionFilterAttribute
	{
		[Dependency]
		public Logging.ILogger Logger { get; set; }

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Logger.Info(
				"{0} ActionMethod on {1}Controller executing...",
				filterContext.ActionDescriptor.ActionName,
				filterContext.ActionDescriptor.ControllerDescriptor.ControllerName);
		}
	}
}
