using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace ERPStore.Controllers.ActionFilters
{
	/// <summary>
	/// Permet de tracker l'utilisation d'une action
	/// </summary>
	public sealed class TrackerActionFilterAttribute : ActionFilterAttribute
	{
		public TrackerActionFilterAttribute()
		{
		}

		[Dependency]
		public Services.IEventPublisher EventPublisher { get; set; }

		public override void OnResultExecuted(ResultExecutedContext filterContext)
		{
			base.OnResultExecuted(filterContext);
			if (filterContext.HttpContext.Request["trace"] == "off")
			{
				return;
			}
			var @event = new Models.Events.TrackerEvent() 
			{ 
				HttpContextBase = filterContext.HttpContext ,
				ActionResult = filterContext.Result,
				ControllerName = filterContext.Controller.GetType().Name,
			};

			if (EventPublisher != null)
			{
				EventPublisher.Publish(@event);
			}
		}
	}
}
