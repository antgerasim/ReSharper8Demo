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
	// [Serializable]
	public class UserPrincipal : IPrincipal
	{
		private System.Security.Principal.IIdentity m_Identity;
		private string m_VisitorId;

		public UserPrincipal(string visitorId)
		{
			m_Identity = new Models.AnonymousIdentity("anonymousUser");
			m_VisitorId = visitorId;
		}

		public UserPrincipal(System.Security.Principal.IIdentity identity, string visitorId)
		{
			m_Identity = identity;
			m_VisitorId = visitorId;
		}

		public System.Security.Principal.IIdentity Identity
		{
			get { return m_Identity; }
		}

		private User m_User;

		public User CurrentUser
		{
			get
			{
				return m_User;
			}
			set
			{
				m_User = value;
			}
		}

		public bool IsInRole(string role)
		{
			if (CurrentUser != null)
			{
				return CurrentUser.Roles.Exists(i => i.Equals(role, StringComparison.InvariantCultureIgnoreCase));
			}
			return false;
		}

		public string VisitorId
		{
			get
			{
				return m_VisitorId;
			}
			set
			{
				m_VisitorId = value;
			}
		}
	}
}
