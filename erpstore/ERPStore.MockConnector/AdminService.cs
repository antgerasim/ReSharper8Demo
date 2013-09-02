using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.MockConnector
{
	public class AdminService : Services.IAdminService
	{
		#region IAdminService Members

		public void ReloadProductCategories()
		{
			throw new NotImplementedException();
		}

		public void ReloadBrands()
		{
			throw new NotImplementedException();
		}

		public void ReloadSettings()
		{
			throw new NotImplementedException();
		}

		public void ClearAllCache()
		{
		}

		public void Initialize()
		{
 
		}


		#endregion
	}
}
