using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web.Mvc;

using Microsoft.Practices.Unity;

namespace ERPStore.Services
{
	/// <summary>
	/// Opération liées à l'animation du site via ERP360
	/// et opération courantes
	/// </summary>
	public class AdminService : IAdminService
	{
		public AdminService()
		{
            CacheService = DependencyResolver.Current.GetService<ICacheService>();
            CatalogService = DependencyResolver.Current.GetService<ICatalogService>();
            EmailerService = DependencyResolver.Current.GetService<IEmailerService>();
		}

        public AdminService(ICacheService cacheService
            , ICatalogService catalogService)
        {
            this.CacheService = cacheService;
            this.CatalogService = catalogService;
			this.EmailerService = DependencyResolver.Current.GetService<IEmailerService>();
        }

		protected ICacheService CacheService { get; set; }
		protected ICatalogService CatalogService { get; set; }
        protected IEmailerService EmailerService { get; set; }

		private void AuthenticateRequest()
		{
			string apiKey = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("apiKey", "ns");
			bool authenticated = apiKey == ERPStoreApplication.WebSiteSettings.ApiToken;
			if (!authenticated)
			{
				throw new System.Security.SecurityException("this request is not authenticated");
			}
		}

		#region IAdminService Members

		public void ReloadProductCategories()
		{
			AuthenticateRequest();
			CatalogService.ReloadCategories();
		}

		public void ReloadBrands()
		{
			AuthenticateRequest();
			CatalogService.ReloadBrands();
		}

		public void ReloadSettings()
		{
			AuthenticateRequest();
			CacheService.Remove("WebSiteSettings");
			MvcApplication.StoreApplication.ReloadSettings();
		}

		public void ClearAllCache()
		{
			CacheService.ClearAll();
		}

		#endregion
	}
}
