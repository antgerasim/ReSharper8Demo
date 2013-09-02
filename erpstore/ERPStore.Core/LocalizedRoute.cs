using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace ERPStore
{
	/// <summary>
	/// Represente une route localisée
	/// </summary>
	public class LocalizedRoute 
	{
		Route m_Decorated;

		public LocalizedRoute(Route route)
        {
			m_Decorated = route;
        }

		/// <summary>
		/// Nom de la route
		/// </summary>
		/// <value>The name.</value>
        public string Name 
		{
			get
			{
				if (m_Decorated.Defaults.IsNotNullOrEmpty())
				{
					return (string)m_Decorated.Defaults["name"];
				}
				return null;
			}
		}

		/// <summary>
		/// Le language de la route
		/// </summary>
		/// <value>The language.</value>
		public string Language 
		{
			get
			{
				if (m_Decorated.Defaults.IsNotNullOrEmpty())
				{
					return (string)m_Decorated.Defaults["language"];
				}
				return null;
			}
		}

		public string LocalizedName
		{
			get
			{
				if (m_Decorated.Defaults.IsNotNullOrEmpty())
				{
					return (string)m_Decorated.Defaults["localizedName"];
				}
				return null;
			}
		}

		public string Url
		{
			get
			{
				return m_Decorated.Url;
			}
		}
	}
}
