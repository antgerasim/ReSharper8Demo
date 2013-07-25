using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.MockConnector
{
	public class SettingsService : ERPStore.Services.SettingsService
	{
		public SettingsService(ERPStore.Services.ICacheService cacheService
							, ERPStore.Logging.ILogger logger
							, Microsoft.Practices.Unity.IUnityContainer container)
			: base(cacheService, logger, container)
		{
		}

		public override ERPStore.Models.WebSiteSettings GetWebSiteSettings(string host)
		{
			var result = base.GetWebSiteSettings(host);
			result.Contact.ContactEmail = "test@test.com";
			result.Contact.BCCEmail = "test@test.com";
			result.Contact.ContactEmailName = "test";
			result.Contact.ContactFaxNumber = "0102030405";
			result.Contact.ContactPhoneNumber = "0102030405";
			result.Contact.CorporateName = "TEST";
			result.Contact.DefaultAddress = new ERPStore.Models.Address()
			{
				City = "test",
				CorporateName = "TEST",
				CountryId = ERPStore.Models.Country.Default.Id,
				Id = 1,
				RecipientName = "TEST",
				Street = "Street",
				ZipCode = "12345",
			};
			result.Contact.EmailSender = "testsender@test.com";
			result.Contact.EmailSenderName = "testsender";

			var defaultConveyor = new ERPStore.Models.Conveyor()
			{
				Code = "TRP01",
				Id = 1,
				Name = "Tranporteur 1",
				UnitPriceByTransportLevel = 1,
			};

			defaultConveyor.CountryList.Add(ERPStore.Models.Country.Default);

			result.Shipping.ConveyorList.Add(defaultConveyor);
			result.Shipping.DefaultConveyor = defaultConveyor;

			return result;
		}

	}
}
