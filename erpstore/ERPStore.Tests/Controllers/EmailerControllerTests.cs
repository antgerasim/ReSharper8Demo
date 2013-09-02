using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Web.Mvc;

using Microsoft.Practices.Unity;

namespace ERPStore.Tests.Controllers
{
	[TestFixture]
	public class EmailerControllerTests : TestBase
	{
		[SetUp]
		public override void Initialize()
		{
			base.Initialize();
		}

		[Test]
		public void Send_Null_Quote_Request()
		{
			var controller = CreateController<ERPStore.Controllers.EmailerController>();
			var result = (System.Web.Mvc.RedirectResult)controller.QuoteConfirmation(null);
			Assert.AreEqual(result.Url, "/");
		}
	}
}
