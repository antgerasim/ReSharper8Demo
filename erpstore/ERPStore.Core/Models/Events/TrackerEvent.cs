using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models.Events
{
	public class TrackerEvent
	{
		public System.Web.HttpContextBase HttpContextBase { get; set; }
		public System.Web.Mvc.ActionResult ActionResult { get; set; }
		public string ControllerName { get; set; }
	}
}
