using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore
{
	public static class ViewExtensions
	{
		/// <summary>
		/// Retourne les paramètres de configuration du site
		/// </summary>
		/// <param name="control">The control.</param>
		/// <returns></returns>
		public static Models.WebSiteSettings Settings(this System.Web.Mvc.ViewUserControl control)
		{
			return ERPStore.ERPStoreApplication.WebSiteSettings;
		}
	}
}
