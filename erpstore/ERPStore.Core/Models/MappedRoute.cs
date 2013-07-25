using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	[Serializable]
	internal class MappedRoute
	{
		public string RouteName { get; set; }
		public string Language { get; set; }
		public System.Web.Routing.Route Route { get; set; }
		public string LocalizedRouteName
		{
			get
			{
				return string.Format("{0}-{1}", RouteName, Language);
			}
		}
	}
}
