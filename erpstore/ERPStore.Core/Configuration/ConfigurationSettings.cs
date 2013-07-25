using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace ERPStore.Configuration
{
	public static class ConfigurationSettings
	{
		private static NameValueCollection m_AppSettings = null;

		public static NameValueCollection AppSettings
		{
			get
			{
				if (m_AppSettings == null)
				{
					m_AppSettings = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection("erpStore/erpStoreSettings");
					if (m_AppSettings == null)
					{
						m_AppSettings = new NameValueCollection();
					}
				}
				return m_AppSettings;
			}
		}
	}
}
