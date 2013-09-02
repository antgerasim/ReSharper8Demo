using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web.Mvc;
using System.Reflection;

namespace ERPStore
{
	public static class RouteCollectionExtensions
	{
		private static IRouteHandler m_RouteHandler;

		public static ERPStoreRoute MapERPStoreRoute(this RouteCollection routes, string name, string url)
		{
			return MapERPStoreRoute(routes, name, url, null, null);
		}

		public static ERPStoreRoute MapERPStoreRoute(this RouteCollection routes, string name, string url, object defaults)
		{
			return MapERPStoreRoute(routes, name, url, defaults, null);
		}

		public static ERPStoreRoute MapERPStoreRoute(this RouteCollection routes, string name, string url, string[] namespaces)
		{
			return MapERPStoreRoute(routes, name, url, null, null, namespaces);
		}

		public static ERPStoreRoute MapERPStoreRoute(this RouteCollection routes, string name, string url, object defaults, object constraints)
		{
			return MapERPStoreRoute(routes, name, url, defaults, constraints, null);
		}

		public static ERPStoreRoute MapERPStoreRoute(this RouteCollection routes, string name, string url, object defaults, string[] namespaces)
		{
			return MapERPStoreRoute(routes, name, url, defaults, null, namespaces);
		}

		public static ERPStoreRoute MapERPStoreRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
		{
			var route = routes.CreateERPStoreRoute(name, url, defaults, constraints, namespaces);
			routes.Add(name, route);
			return route;
		}

		public static ERPStoreRoute CreateERPStoreRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
		{
			if (m_RouteHandler == null)
			{
				m_RouteHandler = new MvcRouteHandler();
			}

			var route = new ERPStoreRoute(url, m_RouteHandler)
			{
				Name = name,
			};

			if (constraints is RouteValueDictionary)
			{
				route.Constraints = constraints as RouteValueDictionary;
			}
			else
			{
				route.Constraints = new RouteValueDictionary(constraints);
			}

			if (defaults is RouteValueDictionary)
			{
				route.Defaults = defaults as RouteValueDictionary;
			}
			else
			{
				route.Defaults = new RouteValueDictionary(defaults);
			}

			if ((namespaces != null) && (namespaces.Length > 0))
			{
				route.DataTokens = new RouteValueDictionary();
				route.DataTokens["Namespaces"] = namespaces;
			}

			return route;
		}

		public static ERPStoreRoute GetByName(this RouteCollection routes, string name)
		{
			foreach (var item in routes)
			{
				var route = item as ERPStoreRoute;
				if (route == null)
				{
					continue;
				}

				if (route.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
				{
					return route;
				}
			}
			return null;
		}

		/// <summary>
		/// Permet l'insersion d'une route à un endroit donné
		/// Voir l'explication de Hernan Garcia @ http://blog.dynamicprogrammer.com/2010/01/02/InsertRouteRouteRegistrationAfterTheFactOnASPNETMVC.aspx
		/// </summary>
		/// <param name="routes">The routes.</param>
		/// <param name="index">The index.</param>
		/// <param name="routeName">Name of the route.</param>
		/// <param name="newRoute">The new route.</param>
		public static void InsertRoute(this RouteCollection routes, int index, string routeName, Route newRoute)
		{
            var fieldInfo = routes.GetType()
                .GetField("_namedMap", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            var dict = fieldInfo.GetValue(routes) as Dictionary<string, RouteBase>;
            dict.Add(routeName, newRoute);
            //dict.GetType()
            //    .GetMethod("Add", BindingFlags.Public | BindingFlags.Instance)
            //    .Invoke(dict, new object[] { routeName, newRoute });

            fieldInfo.SetValue(routes, dict);

			routes.GetType()
				.GetMethod("InsertItem", BindingFlags.NonPublic | BindingFlags.Instance)
				.Invoke(routes, new object[] { index, newRoute });
		}
	}
}
