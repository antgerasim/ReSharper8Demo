using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	public class HttpSessionLifetimeManager<T> : Microsoft.Practices.Unity.LifetimeManager, IDisposable
	{
		private string m_SessionKey = typeof(T).AssemblyQualifiedName;

		public override object GetValue()
		{
			return System.Web.HttpContext.Current.Session[m_SessionKey];
		}

		public override void RemoveValue()
		{
			System.Web.HttpContext.Current.Session.Remove(m_SessionKey);
		}

		public override void SetValue(object newValue)
		{
			System.Web.HttpContext.Current.Session[m_SessionKey] = newValue;
		}

		#region IDisposable Members

		public void Dispose()
		{
			RemoveValue();
		}

		#endregion
	}
}
