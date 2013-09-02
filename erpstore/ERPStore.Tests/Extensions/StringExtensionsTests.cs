using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using ERPStore;

namespace ERPStore.Tests.Extensions
{
	[TestFixture]
	public class StringExtensionsTests : TestBase
	{
		public StringExtensionsTests()
		{

		}

		[Test]
		public void Add_Parameter_To_Url()
		{
			var url = "http://2dgroup.dyndns.org/catalogue/recherche?s=%&BarCode2=4.00832e%252b012";

			var list = url.ToNameValueDictionary();

			Assert.AreEqual(list.Count, 2);
		}

		[Test]
		public void Add_Parameter_To_Encoded_Url()
		{
			var url = "http://2dgroup.dyndns.org/catalogue/recherche?s=%&BarCode2=4.00832e%2b2b012";

			var list = url.ToHtmlDecodedNameValueDictionary();

			var result = System.Web.HttpUtility.UrlDecode("recherche?s=%&BarCode2=4.00832e%2b2b012");

			Assert.AreEqual(list["BarCode2"], "4.00832e+2b012");
		}


	}
}
