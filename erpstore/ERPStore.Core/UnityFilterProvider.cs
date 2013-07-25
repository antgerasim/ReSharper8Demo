using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using Microsoft.Practices.Unity;

namespace ERPStore
{
	public class UnityFilterProvider : FilterAttributeFilterProvider
	{
		Microsoft.Practices.Unity.IUnityContainer m_Container;

		public UnityFilterProvider(Microsoft.Practices.Unity.IUnityContainer container)
		{
			m_Container = container;
		}

		#region IFilterProvider Membres

		public override IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			var list = base.GetFilters(controllerContext, actionDescriptor);

			if (list != null)
			{
				foreach (var item in list)
				{
					m_Container.BuildUp(item.Instance.GetType(), item.Instance);
				}
			}
			return list;
		}

		#endregion
	}
}
