using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ERPStore.Controllers
{
	public class NoTempDataProvider : ITempDataProvider
	{
		public IDictionary<string, object> LoadTempData(ControllerContext controllerContext)
		{
			return new Dictionary<string, object>();
		}

		public void SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values)
		{
			if (values.Count != 0)
				throw new NotImplementedException("Can not set tempdata, no session state available");
		}
	}
}
