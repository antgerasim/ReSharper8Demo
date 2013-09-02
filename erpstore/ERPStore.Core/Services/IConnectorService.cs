using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace ERPStore.Services
{
	/// <summary>
	/// Service de gestion du site
	/// </summary>
	public interface IConnectorService
	{
		/// <summary>
		/// Enregistre le service du catalogue lié au connecteur
		/// </summary>
		/// <returns></returns>
		void RegisterCatalogService();

		/// <summary>
		/// Retourne la liste des infos de propriétés etendues.
		/// </summary>
		/// <returns></returns>
		IEnumerable<Models.PropertyInfo> GetPropertyInfoList();

	}
}
