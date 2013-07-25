using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace ERPStore.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class AnonymousIdentity : IIdentity
	{
		private string m_Name;

		public AnonymousIdentity(string name)
		{
			m_Name = name;
		}

		#region IIdentity Members

		public string AuthenticationType
		{
			get { return "anonymous"; }
		}

		public bool IsAuthenticated
		{
			get { return false; }
		}

		public string Name
		{
			get { return m_Name; }
		}

		#endregion
	}
}
