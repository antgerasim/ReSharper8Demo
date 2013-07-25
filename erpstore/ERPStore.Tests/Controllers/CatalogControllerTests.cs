using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using NUnit.Framework;

using Microsoft.Practices.Unity;

namespace ERPStore.Tests.Controllers
{
	[TestFixture]
	public class CatalogControllerTests : TestBase
	{
		[SetUp]
		public override void Initialize()
		{
			base.Initialize();
		}

		[Test]
		public void Index()
		{
			var m_Controller = CreateController<ERPStore.Controllers.CatalogController>();
			var result = (System.Web.Mvc.ViewResult) m_Controller.Index();
			Assert.AreEqual(result.ViewName, string.Empty);
		}
	}
}
