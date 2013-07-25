using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models.Events
{
	public class UserAuthenticatedEvent
	{
		public int UserId { get; set; }
		public string VisitorId { get; set; }
	}
}
