using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Microsoft.Practices.Unity;

namespace ERPStore.Tests.Services
{
	[TestFixture]
	public class CatalogServicesTests:TestBase
	{
		private ERPStore.Services.ICatalogService m_CatalogService;

		[SetUp]
		public override void Initialize()
		{
			base.Initialize();
			m_CatalogService = m_Container.Resolve<ERPStore.Services.ICatalogService>();
		}

		[Test]
		public void Get_Product_By_Code()
		{
			var product = m_CatalogService.GetProductByCode("xbox360");

			Assert.IsNotNull(product);
		}

	}
}
