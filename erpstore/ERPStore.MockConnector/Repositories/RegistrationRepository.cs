using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.MockConnector.Repositories
{
	public class RegistrationRepository : ERPStore.Repositories.IRegistrationRepository
	{
		public RegistrationRepository()
		{
			Sessions = new Dictionary<string, object>();
		}

		protected Dictionary<string, object> Sessions { get; private set; }

		#region IRegistrationRepository Members

		public void SaveRegistrationUser(string visitorId, ERPStore.Models.RegistrationUser registration)
		{
			Sessions[visitorId] = registration;
		}

		public ERPStore.Models.RegistrationUser GetRegistrationUser(string visitorId)
		{
			if (Sessions.ContainsKey(visitorId))
			{
				return Sessions[visitorId] as ERPStore.Models.RegistrationUser;
			}
			return null;
		}

		public void CloseRegistrationUser(string visitorId, int userId)
		{
			if (Sessions.ContainsKey(visitorId))
			{
				Sessions.Remove(visitorId);
			}
		}

		public IQueryable<ERPStore.Models.RegistrationUser> GetAll()
		{
			return Sessions.Select(i => i.Value).Cast<ERPStore.Models.RegistrationUser>().AsQueryable();
		}

		#endregion
	}
}
