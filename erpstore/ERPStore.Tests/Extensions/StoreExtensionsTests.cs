using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Web.Mvc;
using System.Web.Routing;

using ERPStore;
using ERPStore.Html;

namespace ERPStore.Tests.Extensions
{
	[TestFixture]
	public class StoreExtensionsTests : TestBase
	{

		[Test]
		public void Add_Parameter_To_Url()
		{
			var url = "/test";

			url = ERPStore.Html.StoreExtensions.AddParameter(url, "key", "2");

			Assert.AreEqual("/test?key=2", url);
		}

		[Test]
		public void Replace_Parameter_To_Url()
		{
			var url = "/test?key=1";

			url = ERPStore.Html.StoreExtensions.AddParameter(url, "key", "2");

			Assert.AreEqual("/test?key=2", url);
		}

		[Test]
		public void Replace_Parameter_To_Url_With_Existing_Parameters()
		{
			var url = "/test?key=1&key2=1";

			url = ERPStore.Html.StoreExtensions.AddParameter(url, "key", "2");

			Assert.AreEqual("/test?key2=1&key=2", url);
		}

		[Test]
		public void Remove_Parameter_To_Url()
		{
			var url = "/test?key=1&key2=1";

			url = ERPStore.Html.StoreExtensions.RemoveParameter(url, "key");

			Assert.AreEqual("/test?key2=1", url);
		}

		[Test]
		public void Remove_Parameter_To_Url_With_Anchor()
		{
			var url = "/test?key=1&key2=1#anchor";

			url = ERPStore.Html.StoreExtensions.RemoveParameter(url, "key");

			Assert.AreEqual("/test?key2=1#anchor", url);
		}

		[Test]
		public void Remove_One_Parameter_To_Url_With_Anchor()
		{
			var url = "/test?key=1#anchor";

			url = ERPStore.Html.StoreExtensions.RemoveParameter(url, "key");

			Assert.AreEqual("/test#anchor", url);
		}


		[Test]
		public void Add_Parameter_With_Existing_Anchor()
		{
			var url = "/test#anchor";

			url = ERPStore.Html.StoreExtensions.AddParameter(url, "key", "2");

			Assert.AreEqual("/test?key=2#anchor", url);
		}

		[Test]
		public void Add_Parameter_With_Existing_Anchor2()
		{
			var url = "/test?key1=5#anchor";

			url = ERPStore.Html.StoreExtensions.AddParameter(url, "key", "2");

			Assert.AreEqual("/test?key1=5&key=2#anchor", url);
		}

		[Test]
		public void Localize_To_Href()
		{
			// var context = Controllers.ControllerExtensions.GetMockHttpContext();
			// var urlHelper = new UrlHelper(new RequestContext(context.Object, new RouteData()));
			var url = "/";
			url = ERPStore.Html.StoreExtensions.AddParameter(url, "language", "fr");
			Assert.AreEqual(url, "/?language=fr");
		}

		[Test]
		public void Add_Paramter_And_Value_With_Space()
		{
			var url = "/test";

			url = ERPStore.Html.StoreExtensions.AddParameter(url, "key 1", "a b");

			var prms = url.ToNameValueDictionary();

			Assert.AreEqual(prms.GetKey(0), "key 1");
			Assert.AreEqual(prms[0], "a b");
		}

		[Test]
		public void Add_Paramter_With_Special_Character()
		{
			var url = "/test";

			url = ERPStore.Html.StoreExtensions.AddParameterWithHtmlEncoding(url, "key", "a+b");

			var prms = url.ToNameValueDictionary();

			Assert.AreEqual(prms.GetKey(0), "key");
			Assert.AreEqual(prms[0], "a+b");
		}

		[Test]
		public void Add_Paramter_With_Existing_Special_Character()
		{
			var url = "/test?key1=1+2";

			url = ERPStore.Html.StoreExtensions.AddParameterWithHtmlEncoding(url, "key", "a+b");

			var prms = url.ToNameValueDictionary();

			Assert.AreEqual(prms.GetKey(1), "key");
			Assert.AreEqual(prms[1], "a+b");
		}

	}
}
