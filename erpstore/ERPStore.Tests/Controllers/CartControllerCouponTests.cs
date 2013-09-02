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
	public class CartControllerCouponTests : TestBase
	{
		public CartControllerCouponTests()
		{

		}

		[SetUp]
		public override void Initialize()
		{
			base.Initialize();
		}

		[Test]
		public void Recalc_With_VendorCoupon()
		{
			var m_Controller = CreateController<ERPStore.Controllers.CartController>();
			m_Controller.AddCartItem("XBOX360");

			var formCollection = new FormCollection();
			formCollection.Add("quantity", "5");
			formCollection.Add("couponcode", "Vendor1");
			var result = (System.Web.Mvc.ViewResult)m_Controller.Recalc(formCollection);
			var cart = result.ViewData.Model as ERPStore.Models.OrderCart;
			Assert.IsNotNull(cart);
			Assert.IsNotNull(cart.Coupon);

			Assert.AreEqual(cart.Coupon.Type, ERPStore.Models.CouponType.VendorCode);

			Assert.AreEqual(result.ViewName, "Index");
		}

	}
}
