using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	[Serializable]
	public class MenuItem
	{
		public MenuItem()
		{
			Links = new List<LocalizedMenuLink>();
		}

		public MenuItem(string routeName, List<LocalizedMenuLink> localizedLinks, object parameters)
			: this()
		{
			this.RouteName = routeName;
			this.Links = localizedLinks;
			this.Parameters = parameters;
		}

		public string RouteName { get; set; }
		public List<LocalizedMenuLink> Links { get; set; }
		public object Parameters { get; set; }
		//public bool IsActive { get; set; }
		public bool Enabled { get; set; }
	}
}
