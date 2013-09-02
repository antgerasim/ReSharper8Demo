using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using ERPStore.Models;

using NUnit.Framework;

using Microsoft.Practices.Unity;

namespace ERPStore.Tests.Controllers
{
	[TestFixture]
	public class QuoteCartControllerTests : TestBase
	{
		[SetUp]
		public override void Initialize()
		{
			base.Initialize();
		}

		[Test]
		public void Index()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			m_Controller.AddItem("XBOX360");

			var result = (System.Web.Mvc.ViewResult)m_Controller.Index();
			var currentCart = result.ViewData.Model as ERPStore.Models.QuoteCart;

			Assert.IsNotNull(currentCart);
		}

		[Test]
		public void Add_ProductCode_To_Cart()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			var result = (System.Web.Mvc.ViewResult)m_Controller.AddItem("XBOX360");

			var currentCart = result.ViewData.Model as ERPStore.Models.QuoteCart;

			Assert.IsNotNull(currentCart);
			Assert.AreEqual(currentCart.ItemCount, 1);
		}

		[Test]
		public void Add_BadProductCode_To_Cart()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			var result = (System.Web.Mvc.RedirectToRouteResult)m_Controller.AddItem(Guid.NewGuid().ToString());
			Assert.AreEqual(result.RouteName, "Default");
		}

		[Test]
		public void Add_ProductCode_To_Cart_And_Add_Product_To_Cart()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			var result = (System.Web.Mvc.ViewResult)m_Controller.AddItem("XBOX360");

			var currentCart = result.ViewData.Model as ERPStore.Models.QuoteCart;

			Assert.IsNotNull(currentCart);
			Assert.AreEqual(currentCart.ItemCount, 1);

			result = (System.Web.Mvc.ViewResult)m_Controller.AddItem("XBOXWC");

			currentCart = result.ViewData.Model as ERPStore.Models.QuoteCart;

			Assert.IsNotNull(currentCart);
			Assert.AreEqual(currentCart.ItemCount, 2);
		}

		[Test]
		public void JsAdd_ProductCode_ToCart()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			var result = (System.Web.Mvc.JsonResult)m_Controller.JsAdd("XBOX360");

			var json = result.Data;

			Assert.IsNotNull(json);
		}
		
		[Test]
		public void JsAdd_ProductCode_WithQuantity_ToCart()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			var result = (System.Web.Mvc.JsonResult)m_Controller.JsAddItemWithQuantity("XBOX360", 10);

			var json = result.Data;

			Assert.IsNotNull(json);

			var quantity = json.GetType().GetProperty("quantity").GetValue(json, null);
			Assert.IsNotNull(quantity);
			Assert.AreEqual((int)quantity, 10);
		}

		[Test]
		public void Clear_Empty_Cart()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			var result = (System.Web.Mvc.ViewResult)m_Controller.Clear();
			var cart = result.ViewData.Model as ERPStore.Models.QuoteCart;

			Assert.IsNull(cart);

			Assert.AreEqual(result.ViewName, "EmptyCart");
		}

		[Test]
		public void Clear_Existing_Cart()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			m_Controller.AddItem("XBOX360");
			var result = (System.Web.Mvc.ViewResult)m_Controller.Clear();
			var cart = result.ViewData.Model as ERPStore.Models.QuoteCart;

			Assert.AreEqual(cart.ItemCount, 0);
			Assert.AreEqual(result.ViewName, "EmptyCart");
		}

		[Test]
		public void Recalc_Empty_Cart()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			m_Controller.AddItem("XBOX360");
			var result = (System.Web.Mvc.ViewResult)m_Controller.Recalc(null);
			var cart = result.ViewData.Model as ERPStore.Models.QuoteCart;
			Assert.IsNotNull(cart);
			Assert.AreEqual(result.ViewName, "Index");
		}

		[Test]
		public void Recalc_Existing_Cart()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			m_Controller.AddItem("XBOX360");
			var formCollection = new FormCollection();
			formCollection.Add("quantity", "5");
			var result = (System.Web.Mvc.ViewResult)m_Controller.Recalc(formCollection);
			var cart = result.ViewData.Model as ERPStore.Models.QuoteCart;
			Assert.IsNotNull(cart);
			Assert.AreEqual(result.ViewName, "Index");

			var item = cart.Items.First();

			Assert.AreEqual(item.Quantity, 5);
		}

		[Test]
		public void Recalc_Existing_Cart_With_2_Items()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			m_Controller.AddItem("XBOX360");
			m_Controller.AddItem("XBOXWC");
			var formCollection = new FormCollection();
			formCollection.Add("quantity", "5,4");
			var result = (System.Web.Mvc.ViewResult)m_Controller.Recalc(formCollection);
			var cart = result.ViewData.Model as ERPStore.Models.QuoteCart;
			Assert.IsNotNull(cart);

			var item1 = cart.Items.First();
			var item2 = cart.Items[1];

			Assert.AreEqual(item1.Quantity, 5);
			Assert.AreEqual(item2.Quantity, 4);

			Assert.AreEqual(result.ViewName, "Index");
		}

		[Test]
		public void Remove_Item_To_Empty_Cart()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			var result = (System.Web.Mvc.ViewResult)m_Controller.Remove(0);
			Assert.AreEqual(result.ViewName, "EmptyCart");
		}

		[Test]
		public void Remove_Only_One_Item_To_Cart()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			m_Controller.AddItem("XBOX360");
			var result = (System.Web.Mvc.ViewResult)m_Controller.Remove(0);

			var cart = result.ViewData.Model as ERPStore.Models.QuoteCart;
			Assert.IsNotNull(cart);

			Assert.AreEqual(result.ViewName, "EmptyCart");
		}

		[Test]
		public void Remove_Item_To_Cart_With_Bad_Index()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			m_Controller.AddItem("XBOX360");
			var result = (System.Web.Mvc.ViewResult)m_Controller.Remove(2);

			var cart = result.ViewData.Model as ERPStore.Models.QuoteCart;
			Assert.IsNotNull(cart);

			Assert.AreEqual(result.ViewName, "Index");
		}

		[Test]
		public void Remove_Item_To_Cart()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			m_Controller.AddItem("XBOX360");
			m_Controller.AddItem("XBOXWC");
			var result = (System.Web.Mvc.ViewResult)m_Controller.Remove(0);
			var cart = result.ViewData.Model as ERPStore.Models.QuoteCart;
			Assert.IsNotNull(cart);

			Assert.AreEqual(cart.ItemCount, 1);

			var item = cart.Items.First();

			Assert.AreEqual(item.Product.Code, "XBOXWC");
			Assert.AreEqual(result.ViewName, "Index");
		}

		[Test]
		public void Delete_Cart()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			var result = (System.Web.Mvc.ViewResult)m_Controller.AddItem("XBOX360");
			var cart = result.ViewData.Model as ERPStore.Models.QuoteCart;
			Assert.IsNotNull(cart);

			var result2 = (System.Web.Mvc.RedirectToRouteResult)m_Controller.Delete(cart.Code);
			Assert.AreEqual(result2.RouteName, ERPStoreRoutes.QUOTECART);
		}

		[Test]
		public void Change_Cart()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			var result = (System.Web.Mvc.ViewResult)m_Controller.AddItem("XBOX360");
			var cart1 = result.ViewData.Model as ERPStore.Models.QuoteCart;

			m_Controller.Create();

			result = (System.Web.Mvc.ViewResult)m_Controller.Index();
			var cart2 = result.ViewData.Model as ERPStore.Models.QuoteCart;

			Assert.AreNotSame(cart1, cart2);

			var result2 = (System.Web.Mvc.RedirectToRouteResult)m_Controller.Change(cart1.Code);
			Assert.AreEqual(result2.RouteName, ERPStoreRoutes.QUOTECART);

			result = (System.Web.Mvc.ViewResult)m_Controller.Index();
			var cart3 = result.ViewData.Model as ERPStore.Models.QuoteCart;

			Assert.AreEqual(cart1.Code, cart3.Code);
		}

		[Test]
		public void Show_Empty_Cart()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			var result = (System.Web.Mvc.ViewResult)m_Controller.Show(null);
			Assert.AreEqual(result.ViewName, "EmptyCart");
		}

		[Test]
		public void Show_Cart()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			var result = (System.Web.Mvc.ViewResult)m_Controller.AddItem("XBOX360");
			var cart = result.ViewData.Model as ERPStore.Models.QuoteCart;

			result = (System.Web.Mvc.ViewResult)m_Controller.Show(cart.Code);
			cart = result.ViewData.Model as ERPStore.Models.QuoteCart;

			Assert.AreEqual(result.ViewName, "Index");
			Assert.IsNotNull(cart);
		}

		[Test]
		public void Create_Cart()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			var result = (System.Web.Mvc.RedirectResult)m_Controller.Create();
			Assert.AreEqual(result.Url, "/");
		}

		[Test]
		public void Show_Current_Empty_Cart_List()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			var result = (System.Web.Mvc.ViewResult)m_Controller.ShowCurrentCartList("test");
			var list = result.ViewData.Model as List<ERPStore.Models.QuoteCart>;
			Assert.AreEqual(result.ViewName, "~/views/quotecart/test");
			Assert.AreEqual(list.Count, 0);
		}

		[Test]
		public void Show_Current_Cart_List()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			m_Controller.AddItem("XBOX360");
			var result = (System.Web.Mvc.ViewResult)m_Controller.ShowCurrentCartList("test");
			var list = result.ViewData.Model as List<ERPStore.Models.QuoteCart>;
			Assert.AreEqual(result.ViewName, "~/views/quotecart/test");
			Assert.AreEqual(list.Count, 1);
		}

		[Test]
		public void Show_Status_Empty()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			var result = (System.Web.Mvc.ViewResult)m_Controller.ShowStatus("test");
			var cart = result.ViewData.Model as ERPStore.Models.QuoteCart;
			Assert.IsNull(cart);
			Assert.AreEqual(result.ViewName, "~/views/quotecart/test");
		}

		[Test]
		public void Show_Status()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			m_Controller.AddItem("XBOX360");
			var result = (System.Web.Mvc.ViewResult)m_Controller.ShowStatus("test");
			var cart = result.ViewData.Model as ERPStore.Models.QuoteCart;
			Assert.IsNotNull(cart);
			Assert.AreEqual(result.ViewName, "~/views/quotecart/test");
		}

		[Test]
		public void Submit_Null_From()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			var result = (System.Web.Mvc.RedirectResult)m_Controller.Index(null);
			Assert.AreEqual(result.Url, "/");
		}

		[Test]
		public void Submit_Empty_Quote()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			m_Controller.Create();
			var formCollection = new FormCollection();
			formCollection.Add("dummy", null);
			var result = (System.Web.Mvc.ViewResult)m_Controller.Index(formCollection);
			Assert.AreEqual(result.ViewName, "EmptyCart");
		}

		[Test]
		public void Submit_Quote_With_Bad_Form()
		{
			var m_Controller = CreateController<ERPStore.Controllers.QuoteCartController>();
			m_Controller.AddItem("XBOX360");
			var formCollection = new FormCollection();
			var result = (System.Web.Mvc.RedirectResult)m_Controller.Index(formCollection);
		}


		[Test]
		public void Submit_Quote_With_Bad_Email_And_LastName_In_Form()
		{
			var formCollection = new FormCollection();
			formCollection.Add("firstname", null);
			formCollection.Add("lastname", null);
			formCollection.Add("presentationid", "3");
			formCollection.Add("email", "@email.com");
			formCollection.Add("phonenumber", "0102030405");
			formCollection.Add("faxnumber", null);
			formCollection.Add("message", "ici un message de test");
			formCollection.Add("quantity", "5");

			var m_Controller = CreateControllerWithForm<ERPStore.Controllers.QuoteCartController>(formCollection);

			m_Controller.AddItem("XBOX360");
			var result = (System.Web.Mvc.ViewResult)m_Controller.Index(formCollection);
			Assert.AreEqual(result.ViewData.ModelState.IsValid, false);
			Assert.AreEqual(result.ViewData.ModelState.Keys.Contains("email"), true);
		}

		[Test]
		public void Submit_Quote_Form()
		{
			var formCollection = new FormCollection();
			formCollection.Add("firstname", null);
			formCollection.Add("lastname", "test");
			formCollection.Add("presentationid", "3");
			formCollection.Add("email", "test@email.com");
			formCollection.Add("phonenumber", "0102030405");
			formCollection.Add("faxnumber", null);
			formCollection.Add("message", "ici un message de test");
			formCollection.Add("quantity", "5");
			formCollection.Add("zipcode", "12345");

			var m_Controller = CreateControllerWithForm<ERPStore.Controllers.QuoteCartController>(formCollection);
			m_Controller.AddItem("XBOX360");
			var result = (System.Web.Mvc.RedirectToRouteResult)m_Controller.Index(formCollection);
			Assert.AreEqual(result.RouteName, ERPStoreRoutes.QUOTECART_SENT);
			// var cart = result.ViewData.Model as ERPStore.Models.QuoteCart;
			// Assert.AreEqual(cart.IsSent, true);
		}


	}
}
