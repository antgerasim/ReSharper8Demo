using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace ERPStore
{
	public class ERPStoreRoute : Route
	{
		public ERPStoreRoute(string url, IRouteHandler routeHandler)
			: base(url, routeHandler)
		{

		}

		public ERPStoreRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
			: base(url, defaults, routeHandler)
		{

		}

		public ERPStoreRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
			: base(url, defaults, constraints, routeHandler)
		{

		}

		public ERPStoreRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
			: base(url, defaults, constraints, dataTokens, routeHandler)
		{

		}

		public string Name { get; set; }

	}
}
