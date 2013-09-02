using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	public class SettingsService : ISettingsService
	{
		protected ERPStore.Models.WebSiteSettings m_Settings;
		protected System.Collections.Specialized.NameValueCollection m_PaymentSettings;

		public SettingsService(ERPStore.Services.ICacheService cacheService
			, Logging.ILogger logger
			, Microsoft.Practices.Unity.IUnityContainer container)
		{
			this.CacheService = cacheService;
			this.Logger = logger;
			this.Container = container;
		}

		protected ERPStore.Services.ICacheService CacheService { get; set; }
		protected Logging.ILogger Logger { get; set; }
		protected Microsoft.Practices.Unity.IUnityContainer Container { get; private set; }

		#region ISettingsService Members

		public virtual ERPStore.Models.WebSiteSettings GetWebSiteSettings(string host)
		{
			var webSiteSettings = new ERPStore.Models.WebSiteSettings();
			var cryptoProvider = new System.Security.Cryptography.RC2CryptoServiceProvider();
			cryptoProvider.GenerateKey();
			cryptoProvider.GenerateIV();
			webSiteSettings.CryptoIV = cryptoProvider.IV;
			webSiteSettings.CryptoKey = cryptoProvider.Key;
			return webSiteSettings;
		}

		public virtual System.Collections.Specialized.NameValueCollection PaymentSettings
		{
			get 
			{
				return null;
			}
		}

		public virtual void ConfigureConveyorList(ERPStore.Models.WebSiteSettings settings)
		{
			// Do nothing
		}

		#endregion

		protected Models.CatalogSettingsFilter GetCatalogSettingsFilter()
		{
			Models.CatalogSettingsFilter result = null;
			var productCategoryIdSetting = Configuration.ConfigurationSettings.AppSettings["catalogFilter.ProductCategoryId"];
			var brandIdSetting = Configuration.ConfigurationSettings.AppSettings["catalogFilter.BrandId"];
			var productSelectionName = Configuration.ConfigurationSettings.AppSettings["catalogFilter.ProductSelectionName"];

			if (productCategoryIdSetting != null
				|| brandIdSetting != null
				|| productSelectionName != null)
			{
				result = new ERPStore.Models.CatalogSettingsFilter();
				if (productCategoryIdSetting != null)
				{
					result.ProductCategoryId = Convert.ToInt32(productCategoryIdSetting);
				}
				if (brandIdSetting != null)
				{
					result.BrandId = Convert.ToInt32(brandIdSetting);
				}
				if (productSelectionName != null)
				{
					result.ProductSelectionName = productSelectionName;
				}
			}
			return result;
		}
	}
}
