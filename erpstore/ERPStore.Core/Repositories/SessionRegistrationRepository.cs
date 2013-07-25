using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace ERPStore.Repositories
{
	public class SessionRegistrationRepository : IRegistrationRepository
	{
		public SessionRegistrationRepository(Services.ICacheService cacheService)
		{
			this.CacheService = cacheService;
		}

		protected Services.ICacheService CacheService { get; set; }

		#region IRegistrationRepository Members

		public void SaveRegistrationUser(string visitorId, ERPStore.Models.RegistrationUser registration)
		{
			string key = string.Format("registration:{0}", visitorId);
			CacheService.Add(key, registration, DateTime.Now.AddDays(1));
		}

		public ERPStore.Models.RegistrationUser GetRegistrationUser(string visitorId)
		{
			string key = string.Format("registration:{0}", visitorId);
			var result = CacheService[key] as ERPStore.Models.RegistrationUser;
			return result;
		}

		public void CloseRegistrationUser(string visitorId, int userId)
		{
			string key = string.Format("registration:{0}", visitorId);
			CacheService.Remove(key);
		}

		public IQueryable<ERPStore.Models.RegistrationUser> GetAll()
		{
			return CacheService.GetListOf<ERPStore.Models.RegistrationUser>().AsQueryable();
		}

		#endregion
	}
}
