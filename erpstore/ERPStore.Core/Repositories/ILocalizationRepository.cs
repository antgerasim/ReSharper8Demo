using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Repositories
{
	public interface ILocalizationRepository
	{
		Dictionary<int, IEnumerable<ERPStore.Models.EntityLocalization>> GetLocalizationByEntitIdList(string entityName, IEnumerable<int> entityIdList);

		IEnumerable<Models.EntityLocalization> GetLocalizationByEntityId(string entityName, int entityId);
	}
}
