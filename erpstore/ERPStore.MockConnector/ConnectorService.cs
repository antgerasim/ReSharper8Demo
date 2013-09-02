using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.Routing;
using System.Web.Mvc;

using ERPStore.Html;

namespace ERPStore.MockConnector
{
	public class ConnectorService : Services.IConnectorService
	{
		private Models.WebSiteSettings m_Settings;

		public ConnectorService()
		{
		}

		#region IStoreService Members

		public ERPStore.Models.WebSiteSettings GetWebSiteSettings(string host)
		{
			m_Settings = new Models.WebSiteSettings();
			m_Settings.Contact.ContactEmail = "contact@erpstore.net";
			m_Settings.Contact.ContactFaxNumber = "0102030405";
			m_Settings.Contact.ContactPhoneNumber = "0102030405";
			m_Settings.Texts.Copyright = "copyright";
			m_Settings.HomeMetaDescription = "1st ECommerce in the world";
			m_Settings.HomeMetaKeywords = "erpstore, erp360, serialcoder";
			m_Settings.LogoImageFileName = "";
			m_Settings.Contact.OfficeHours = "officehours";
			m_Settings.Payment.ShowPriceWithTax = false;
			m_Settings.SiteName = "ERPStore-DefaultSkin";
			m_Settings.Sloggan = "MVC ECommerce by erpstore";
			m_Settings.Texts.TermsAndConditions = "terms and conditions";
			m_Settings.TempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), m_Settings.SiteName);
			m_Settings.UseFullTextIndex = false;
			m_Settings.AllowGenerateSitemaps = false;
			// m_Settings.Menus = GetMenu();

			//m_Settings.Payment. = new ERPStore.Models.OgoneSettings()
			//{
			//    PSPID = "erpstore",
			//    SHASignIn = "v4FmLR/SSQ6Q/wvKbVtN",
			//    SHASignOut = @"BV<pMY(%YJ%s:TFBNkRSG*\SN",
			//    Target = ERPStore.Models.OgoneTargetPlatform.Test,
			//};

			var conveyor = new Models.Conveyor();
			conveyor.Id = 1;
			conveyor.Name = "MockConveyor";
			conveyor.UnitPriceByTransportLevel = 0;

			m_Settings.Shipping.DefaultConveyor = conveyor;
			m_Settings.Shipping.ConveyorList.Add(conveyor);

			var cryptoProvider = new System.Security.Cryptography.RC2CryptoServiceProvider();
			cryptoProvider.GenerateKey();
			cryptoProvider.GenerateIV();

			m_Settings.CryptoIV = cryptoProvider.IV;
			m_Settings.CryptoKey = cryptoProvider.Key;

			return m_Settings;
		}

		public void RegisterServices()
		{
		}

		public List<Models.MenuItem> GetMenu()
		{
			var list = new List<Models.MenuItem>();

			var homeMemu = new ERPStore.Models.MenuItem()
			{
				RouteName = "Default",
				Enabled = true,
			};

			list.Add(homeMemu);

			var computerMenu = new ERPStore.Models.MenuItem()
			{
				RouteName = "Computer",
				Enabled = true,
			};
			list.Add(computerMenu);

			var eletronicMenu = new ERPStore.Models.MenuItem()
			{
				RouteName = "Electronic",
				Enabled = true,
			};
			list.Add(eletronicMenu);

			var accessoriesMenu = new ERPStore.Models.MenuItem()
			{
				RouteName = "Accessories",
				Enabled = true,
			};
			list.Add(accessoriesMenu);

			var cartMenu = new ERPStore.Models.MenuItem()
			{
				RouteName = "Cart",
				Enabled = true,
			};
			list.Add(cartMenu);

			return list;
		}

		public void RegisterServices(Microsoft.Practices.Unity.IUnityContainer container)
		{
		}

		public void RegisterCatalogService()
		{
		}

		public IEnumerable<ERPStore.Models.PropertyInfo> GetPropertyInfoList()
		{
			throw new NotImplementedException();
		}

		public System.Collections.Specialized.NameValueCollection PaymentSettings
		{
			get 
			{
				return new System.Collections.Specialized.NameValueCollection();
			}
		}

		#endregion
	}
}
