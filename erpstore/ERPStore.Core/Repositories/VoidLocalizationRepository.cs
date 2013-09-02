using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Repositories
{
	public class VoidLocalizationRepository : ILocalizationRepository
	{
		#region ILocalizationRepository Members

		public Dictionary<int, IEnumerable<ERPStore.Models.EntityLocalization>> GetLocalizationByEntitIdList(string entityName, IEnumerable<int> entityIdList)
		{
			return null;
		}

		public IEnumerable<ERPStore.Models.EntityLocalization> GetLocalizationByEntityId(string entityName, int entityId)
		{
			return null;
		}

		#endregion
	}
}
