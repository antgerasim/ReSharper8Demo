using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	public interface ISettingsService
	{
		/// <summary>
		/// Chargement des infos de configuration du stie
		/// </summary>
		/// <param name="host">The host.</param>
		/// <returns></returns>
		Models.WebSiteSettings GetWebSiteSettings(string host);

		/// <summary>
		/// Gets the name of the configuration element by.
		/// </summary>
		/// <value>The payment settings.</value>
		/// <returns></returns>
		System.Collections.Specialized.NameValueCollection PaymentSettings { get; }

		/// <summary>
		/// configuration des transporteurs
		/// </summary>
		/// <returns></returns>
		void ConfigureConveyorList(ERPStore.Models.WebSiteSettings settings);
	}
}
