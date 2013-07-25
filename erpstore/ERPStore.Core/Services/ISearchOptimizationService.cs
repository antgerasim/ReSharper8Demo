using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	public interface ISearchOptimizationService
	{
		void GenerateMetasInformations(System.Web.HttpContextBase context, object model);
	}
}
