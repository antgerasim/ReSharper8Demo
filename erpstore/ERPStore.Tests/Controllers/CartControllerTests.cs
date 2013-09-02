using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using NUnit.Framework;

using Microsoft.Practices.Unity;

namespace ERPStore.Tests.Controllers
{
	/// <summary>
	/// Summary description for CartControllerTests
	/// </summary>
	[TestFixture]
	public class CartControllerTests : TestBase
	{
		public CartControllerTests()
		{
		}

		[SetUp]
		public override void Initialize()
		{
			base.Initialize();
		}

		private ERPStore.Controllers.CartController CreateCartContorller()
		{
			var result = CreateController<ERPStore.Controllers.CartController>();
			return result;
		}

		private ERPStore.Controllers.CartController CreateCartControllerWithReferer()
		{
			var httpContext2 = CreateMockHttpContext();
			httpContext2.Setup(ctrl => ctrl.Request.UrlReferrer).Returns(new Uri("http://www.referer.com"));
			m_Container.RegisterInstance(typeof(System.Web.HttpContextBase), httpContext2.Object);

			var result = m_Container.Resolve<ERPStore.Controllers.CartController>();
			result.SetupControllerContext(httpContext2.Object);
			return result;
		}

		[Test]
		public void Index()
		{
			var m_Controller = CreateCartContorller();
			m_Controller.AddCartItem("XBOX360");

			var result = (System.Web.Mvc.ViewResult)m_Controller.Index();
			var currentCart = result.ViewData.Model as ERPStore.Models.OrderCart;

			Assert.IsNotNull(currentCart);
		}

		[Test]
		public void Empty_Cart()
		{
			var m_Controller = CreateCartContorller();
			var result = (System.Web.Mvc.ViewResult)m_Controller.Index();
			Assert.AreEqual(result.ViewName, "EmptyCart");
		}

		[Test]
		public void Add_Product_To_Cart()
		{
			var m_Controller = CreateCartContorller();
			var result = (System.Web.Mvc.ViewResult)m_Controller.AddCartItem("XBOX360");
			var currentCart = result.ViewData.Model as ERPStore.Models.OrderCart;

			Assert.IsNotNull(currentCart);
			Assert.AreEqual(currentCart.ItemCount, 1);
			Assert.AreEqual(currentCart.Total, 199.99);
			Assert.AreEqual(currentCart.TotalTax, Convert.ToDecimal(199.99 * 0.196));
		}

		[Test]
		public void Add_Same_Product_To_Cart()
		{
			var m_Controller = CreateCartContorller();
			var result = (System.Web.Mvc.ViewResult)m_Controller.AddCartItem("XBOX360");
			var currentCart = result.ViewData.Model as ERPStore.Models.OrderCart;

			Assert.IsNotNull(currentCart);
			Assert.AreEqual(currentCart.ItemCount, 1);
			Assert.AreEqual(currentCart.Total, 199.99);
			Assert.AreEqual(currentCart.TotalTax, Convert.ToDecimal(199.99 * 0.196));

			result = (System.Web.Mvc.ViewResult)m_Controller.AddCartItem("XBOX360");
			currentCart = result.ViewData.Model as ERPStore.Models.OrderCart;

			Assert.AreEqual(currentCart.ItemCount, 1);
			Assert.AreEqual(currentCart.Total, 199.99 * 2);
			Assert.AreEqual(currentCart.TotalTax, Convert.ToDecimal(199.99 * 0.196 * 2));
		}


		[Test]
		public void Add_Product_To_Cart_With_Referer()
		{
			var m_ControllerWithReferer = CreateCartControllerWithReferer();
			var result = (System.Web.Mvc.ViewResult)m_ControllerWithReferer.AddCartItem("XBOX360");
			var currentCart = result.ViewData.Model as ERPStore.Models.OrderCart;

			Assert.IsNotNull(currentCart);
			Assert.AreEqual(currentCart.ItemCount, 1);
			Assert.AreEqual(currentCart.Total, 199.99);
			Assert.AreEqual(currentCart.TotalTax, Convert.ToDecimal(199.99 * 0.196));
		}


		[Test]
		public void Add_FakeProductCode_To_Cart()
		{
			var m_Controller = CreateCartContorller();
			var result = m_Controller.AddCartItem(Guid.NewGuid().ToString()) as RedirectToRouteResult;
			Assert.IsNotNull(result);

			Assert.AreEqual(result.RouteName, "Default");
		}

		[Test]
		public void JSAdd_To_Cart()
		{
			var m_Controller = CreateCartContorller();
			var result = (System.Web.Mvc.JsonResult)m_Controller.JsAddItem("XBOX360");
			Assert.IsNotNull(result.Data);
		}

		[Test]
		public void JSAdd_To_Cart_With_Referer()
		{
			var m_ControllerWithReferer = CreateCartControllerWithReferer();
			var result = (System.Web.Mvc.JsonResult)m_ControllerWithReferer.JsAddItem("XBOX360");
			Assert.IsNotNull(result.Data);
		}


		[Test]
		public void JSAdd_Fake_Product_To_Cart()
		{
			var m_Controller = CreateCartContorller();
			var result = (System.Web.Mvc.JsonResult)m_Controller.JsAddItem(Guid.NewGuid().ToString());
			Assert.IsNull(result.Data);
		}

		[Test]
		public void JSAdd_Product_With_Quantity_To_Cart()
		{
			var m_Controller = CreateCartContorller();
			var result = (System.Web.Mvc.JsonResult)m_Controller.JsAddItemWithQuantity("XBOX360", 5);

			var json = result.Data;
			Assert.IsNotNull(result.Data);

			var quantity = json.GetType().GetProperty("quantity").GetValue(json, null);
			Assert.IsNotNull(quantity);
			Assert.AreEqual((int)quantity, 5);
		}


		[Test]
		public void Clear_Cart()
		{
			var m_Controller = CreateCartContorller();
			var result = m_Controller.AddCartItem("XBOX360") as System.Web.Mvc.ViewResult;
			var cart = result.ViewData.Model as ERPStore.Models.OrderCart;

			Assert.IsNotNull(cart);

			var clearResult = m_Controller.Clear() as System.Web.Mvc.ViewResult;
			cart = clearResult.ViewData.Model as ERPStore.Models.OrderCart;

			Assert.AreEqual(cart.ItemCount, 0);
			Assert.AreEqual(clearResult.ViewName, "EmptyCart");
		}

		[Test]
		public void Clear_Empty_Cart()
		{
			var m_Controller = CreateCartContorller();
			var result = (System.Web.Mvc.ViewResult)m_Controller.Clear();

			Assert.IsNull(result.ViewData.Model);
			Assert.AreEqual(result.ViewName, "EmptyCart");
		}

		[Test]
		public void Recalc_Empty_Cart()
		{
			var m_Controller = CreateCartContorller();
			m_Controller.AddCartItem("XBOX360");
			var result = (System.Web.Mvc.ViewResult)m_Controller.Recalc(null);
			Assert.IsNotNull(result.ViewData.Model);
			Assert.AreEqual(result.ViewName, "Index");
		}

		[Test]
		public void Recalc_Existing_Cart()
		{
			var formCollection = new FormCollection();
			formCollection.Add("quantity", "5");
			var m_Controller = CreateCartContorller();
			m_Controller.AddCartItem("XBOX360");
			var result = (System.Web.Mvc.ViewResult)m_Controller.Recalc(formCollection);
			var cart = result.ViewData.Model as ERPStore.Models.OrderCart;
			Assert.IsNotNull(cart);
			Assert.AreEqual(result.ViewName, "Index");

			var item = cart.Items.First();

			Assert.AreEqual(item.Quantity, 5);
		}

		[Test]
		public void Recalc_Existing_Cart_With_Bad_Data()
		{
			var m_Controller = CreateCartContorller();
			m_Controller.AddCartItem("XBOX360");
			var formCollection = new FormCollection();
			formCollection.Add("quantity", "a");
			var result = (System.Web.Mvc.ViewResult)m_Controller.Recalc(formCollection);
			var cart = result.ViewData.Model as ERPStore.Models.OrderCart;
			Assert.IsNotNull(cart);
			Assert.AreEqual(result.ViewName, "Index");

			var item = cart.Items.First();

			Assert.AreEqual(item.Quantity, 1);
		}


		[Test]
		public void Recalc_Existing_Cart_With_Bad_Quantity()
		{
			var m_Controller = CreateCartContorller();
			m_Controller.AddCartItem("XBOX360");
			var formCollection = new FormCollection();
			formCollection.Add("quantity", "xxx");
			var result = (System.Web.Mvc.ViewResult)m_Controller.Recalc(formCollection);
			var cart = result.ViewData.Model as ERPStore.Models.OrderCart;
			Assert.IsNotNull(cart);
			Assert.AreEqual(result.ViewName, "Index");

			var item = cart.Items.First();

			Assert.AreEqual(item.Quantity, 1);
		}

		[Test]
		public void Recalc_Existing_Cart_With_2_Items()
		{
			var m_Controller = CreateCartContorller();
			m_Controller.AddCartItem("XBOX360");
			m_Controller.AddCartItem("XBOXWC");
			var formCollection = new FormCollection();
			formCollection.Add("quantity", "5,4");
			var result = (System.Web.Mvc.ViewResult)m_Controller.Recalc(formCollection);
			var cart = result.ViewData.Model as ERPStore.Models.OrderCart;
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
			var m_Controller = CreateCartContorller();
			var result = (System.Web.Mvc.RedirectToRouteResult)m_Controller.Remove(0);
			Assert.AreEqual(result.RouteName, ERPStoreRoutes.HOME);
		}

		[Test]
		public void Remove_Only_One_Item_To_Cart()
		{
			var m_Controller = CreateCartContorller();
			m_Controller.AddCartItem("XBOX360");
			var result = (System.Web.Mvc.ViewResult)m_Controller.Remove(0);

			var cart = result.ViewData.Model as ERPStore.Models.OrderCart;
			Assert.IsNotNull(cart);

			Assert.AreEqual(result.ViewName, "EmptyCart");
		}

		[Test]
		public void Remove_Item_To_Cart_With_Bad_Index()
		{
			var m_Controller = CreateCartContorller();
			m_Controller.AddCartItem("XBOX360");
			var result = (System.Web.Mvc.ViewResult)m_Controller.Remove(2);

			var cart = result.ViewData.Model as ERPStore.Models.OrderCart;
			Assert.IsNotNull(cart);

			Assert.AreEqual(result.ViewName, "Index");
		}

		[Test]
		public void Remove_Item_To_Cart()
		{
			var m_Controller = CreateCartContorller();
			m_Controller.AddCartItem("XBOX360");
			m_Controller.AddCartItem("XBOXWC");
			var result = (System.Web.Mvc.ViewResult)m_Controller.Remove(0);
			var cart = result.ViewData.Model as ERPStore.Models.OrderCart;
			Assert.IsNotNull(cart);

			Assert.AreEqual(cart.ItemCount, 1);

			var item = cart.Items.First();

			Assert.AreEqual(item.Product.Code, "XBOXWC");
			Assert.AreEqual(result.ViewName, "Index");
		}

		[Test]
		public void Delete_Cart()
		{
			var m_Controller = CreateCartContorller();
			var result = (System.Web.Mvc.ViewResult)m_Controller.AddCartItem("XBOX360");
			var cart = result.ViewData.Model as ERPStore.Models.OrderCart;
			Assert.IsNotNull(cart);

			var result2 = (System.Web.Mvc.RedirectToRouteResult)m_Controller.Delete(cart.Code);
			Assert.AreEqual(result2.RouteName, ERPStore.ERPStoreRoutes.CART);
		}

		[Test]
		public void Change_Cart()
		{
			var m_Controller = CreateCartContorller();
			var result = (System.Web.Mvc.ViewResult)m_Controller.AddCartItem("XBOX360");
			var cart1 = result.ViewData.Model as ERPStore.Models.OrderCart;

			m_Controller.Create();

			result = (System.Web.Mvc.ViewResult)m_Controller.Index();
			var cart2 = result.ViewData.Model as ERPStore.Models.OrderCart;

			Assert.AreNotSame(cart1, cart2);

			var result2 = (System.Web.Mvc.RedirectToRouteResult)m_Controller.Change(cart1.Code);
			Assert.AreEqual(result2.RouteName, ERPStore.ERPStoreRoutes.CART);

			result = (System.Web.Mvc.ViewResult)m_Controller.Index();
			var cart3 = result.ViewData.Model as ERPStore.Models.OrderCart;

			Assert.AreEqual(cart1.Code, cart3.Code);
		}

		[Test]
		public void Show_Empty_Cart()
		{
			var m_Controller = CreateCartContorller();
			var result = (System.Web.Mvc.ViewResult)m_Controller.Show(null);
			Assert.AreEqual(result.ViewName, "EmptyCart");
		}

		[Test]
		public void Show_Cart()
		{
			var m_Controller = CreateCartContorller();
			var result = (System.Web.Mvc.ViewResult)m_Controller.AddCartItem("XBOX360");
			var cart = result.ViewData.Model as ERPStore.Models.OrderCart;

			result = (System.Web.Mvc.ViewResult)m_Controller.Show(cart.Code);
			cart = result.ViewData.Model as ERPStore.Models.OrderCart;

			Assert.AreEqual(result.ViewName, "Index");
			Assert.IsNotNull(cart);
		}

		[Test]
		public void Create_Cart()
		{
			var m_Controller = CreateCartContorller();
			var result = (System.Web.Mvc.RedirectToRouteResult)m_Controller.Create();
			Assert.AreEqual(result.RouteName, ERPStoreRoutes.HOME);
		}

		[Test]
		public void Show_Current_Empty_Cart_List()
		{
			var m_Controller = CreateCartContorller();
			var result = (System.Web.Mvc.ViewResult)m_Controller.ShowCurrentCartList("test");
			var list = result.ViewData.Model as List<ERPStore.Models.OrderCart>;
			Assert.AreEqual(result.ViewName, "~/views/cart/test");
			Assert.AreEqual(list.Count, 0);
		}

		[Test]
		public void Show_Current_Cart_List()
		{
			var m_Controller = CreateCartContorller();
			m_Controller.AddCartItem("XBOX360");
			var result = (System.Web.Mvc.ViewResult)m_Controller.ShowCurrentCartList("test");
			var list = result.ViewData.Model as List<ERPStore.Models.OrderCart>;
			Assert.AreEqual(result.ViewName, "~/views/cart/test");
			Assert.AreEqual(list.Count, 1);
		}

		[Test]
		public void Show_Status_Empty()
		{
			var m_Controller = CreateCartContorller();
			var result = (System.Web.Mvc.ViewResult)m_Controller.ShowStatus("test");
			var cart = result.ViewData.Model as ERPStore.Models.OrderCart;
			Assert.IsNull(cart);
			Assert.AreEqual(result.ViewName, "~/views/cart/test");
		}

		[Test]
		public void Show_Status()
		{
			var m_Controller = CreateCartContorller();
			m_Controller.AddCartItem("XBOX360");
			var result = (System.Web.Mvc.ViewResult)m_Controller.ShowStatus("test");
			var cart = result.ViewData.Model as ERPStore.Models.OrderCart;
			Assert.IsNotNull(cart);
			Assert.AreEqual(result.ViewName, "~/views/cart/test");
		}



	}
}
