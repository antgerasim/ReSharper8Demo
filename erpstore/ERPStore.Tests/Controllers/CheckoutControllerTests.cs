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
	public class CheckoutControllerTests : TestBase
	{
		[SetUp]
		public override void Initialize()
		{
			base.Initialize();
		}

		[Test]
		public void Anonymous_Shipping()
		{
			var m_AnonymousController = CreateController<ERPStore.Controllers.CheckoutController>();
			Assert.AreEqual(true, ControllerActionInvoker<HttpUnauthorizedResult>().InvokeAction(m_AnonymousController.ControllerContext, "Shipping"));
		}

		[Test]
		public void Authenticated_Shipping_With_EmptyCart()
		{
			var m_AuthenticatedController = CreateAuthenticatedController<ERPStore.Controllers.CheckoutController>();
			var m_AnonymousController = CreateController<ERPStore.Controllers.CheckoutController>();

			Assert.AreEqual(true, ControllerActionInvoker<HttpUnauthorizedResult>().InvokeAction(m_AnonymousController.ControllerContext, "Shipping"));

			var result = m_AuthenticatedController.Shipping() as System.Web.Mvc.RedirectToRouteResult;
			Assert.AreEqual(result.RouteName, ERPStoreRoutes.HOME);
		}

		[Test]
		public void Authenticated_Shipping_With_Total_Too_Low()
		{
			var m_AnonymousController = CreateController<ERPStore.Controllers.CheckoutController>();
			var m_AuthenticatedController = CreateAuthenticatedController<ERPStore.Controllers.CheckoutController>();
			var m_CartController = CreateController<ERPStore.Controllers.CartController>();

			Assert.AreEqual(true, ControllerActionInvoker<HttpUnauthorizedResult>().InvokeAction(m_AnonymousController.ControllerContext, "Shipping"));

			m_CartController.AddCartItem("XBOX360");
			ERPStoreApplication.WebSiteSettings.Payment.MinimalOrderAmount = 300;
			var user = GetCustomerUserPrincipal();
			var result = m_AuthenticatedController.Shipping() as System.Web.Mvc.ViewResult;
			Assert.AreEqual(result.ViewData.ModelState.IsValid, false);
		}

		[Test]
		public void Authenticated_Shipping()
		{
			var m_AnonymousController = CreateController<ERPStore.Controllers.CheckoutController>();
			var m_AuthenticatedController = CreateAuthenticatedController<ERPStore.Controllers.CheckoutController>();
			var m_CartController = CreateController<ERPStore.Controllers.CartController>();

			Assert.AreEqual(true, ControllerActionInvoker<HttpUnauthorizedResult>().InvokeAction(m_AnonymousController.ControllerContext, "Shipping"));

			m_CartController.AddCartItem("XBOX360");
			var user = GetCustomerUserPrincipal();
			var result = m_AuthenticatedController.Shipping() as System.Web.Mvc.ViewResult;
			var cart = result.ViewData.Model as ERPStore.Models.OrderCart;
			Assert.AreEqual(result.ViewName, "~/views/account/order/shipping.aspx");
			Assert.IsNotNull(cart);
			Assert.AreEqual(cart.BillingAddress, user.CurrentUser.DefaultAddress);
			Assert.IsNotNull(cart.DeliveryAddress);
		}

		[Test]
		public void Setup_Shipping_With_New_Delivery_Address()
		{
			var m_AnonymousController = CreateController<ERPStore.Controllers.CheckoutController>();
			var m_AuthenticatedController = CreateAuthenticatedController<ERPStore.Controllers.CheckoutController>();
			var m_CartController = CreateController<ERPStore.Controllers.CartController>();

			m_CartController.AddCartItem("XBOX360");
			var user = GetCustomerUserPrincipal();
			var result = (System.Web.Mvc.RedirectToRouteResult)m_AuthenticatedController.Shipping(-1, "RecipientName", "Street", "ZipCode", "City", ERPStore.Models.Country.Default.Id);

			Assert.AreEqual(result.RouteName, ERPStoreRoutes.CHECKOUT_CONFIGURATION);
			var cartService = m_Container.Resolve<ERPStore.Services.ICartService>();

			var cart = cartService.GetCurrentOrderCart(user);

			Assert.AreEqual(cart.DeliveryAddress.RecipientName, "RecipientName");
		}

		[Test]
		public void Setup_Shipping_With_Existing_Delivery_Address()
		{
			var m_AnonymousController = CreateController<ERPStore.Controllers.CheckoutController>();
			var m_AuthenticatedController = CreateAuthenticatedController<ERPStore.Controllers.CheckoutController>();
			var m_CartController = CreateController<ERPStore.Controllers.CartController>();

			m_CartController.AddItemWithQuantity("XBOX360", 5);
			var user = GetCustomerUserPrincipal();
			var result = (System.Web.Mvc.RedirectToRouteResult)m_AuthenticatedController.Shipping(1, null, null, null, null, 0);

			Assert.AreEqual(result.RouteName, ERPStoreRoutes.CHECKOUT_CONFIGURATION);
			var cartService = m_Container.Resolve<ERPStore.Services.ICartService>();

			var cart = cartService.GetCurrentOrderCart(user);

			Assert.AreEqual(cart.DeliveryAddress.RecipientName, "M Test 1");
		}

		[Test]
		public void Setup_Payment_Anonymous()
		{
			var m_AnonymousController = CreateController<ERPStore.Controllers.CheckoutController>();

			Assert.AreEqual(true, ControllerActionInvoker<HttpUnauthorizedResult>().InvokeAction(m_AnonymousController.ControllerContext, "Shipping"));
		}

		[Test]
		public void Setup_Payment_With_Empty_Cart()
		{
			var m_AuthenticatedController = CreateAuthenticatedController<ERPStore.Controllers.CheckoutController>();

			var result2 = (System.Web.Mvc.RedirectToRouteResult)m_AuthenticatedController.Payment();

			Assert.AreEqual(result2.RouteName, ERPStoreRoutes.HOME);
		}

		[Test]
		public void Setup_Payment()
		{
			var m_AnonymousController = CreateController<ERPStore.Controllers.CheckoutController>();
			var m_AuthenticatedController = CreateAuthenticatedController<ERPStore.Controllers.CheckoutController>();
			var m_CartController = CreateController<ERPStore.Controllers.CartController>();

			m_CartController.AddItemWithQuantity("XBOX360", 5);
			var user = GetCustomerUserPrincipal();
			m_AuthenticatedController.Shipping();
			var result = (System.Web.Mvc.RedirectToRouteResult)m_AuthenticatedController.Shipping(1, null, null, null, null, 0);

			Assert.AreEqual(result.RouteName, ERPStoreRoutes.CHECKOUT_CONFIGURATION);

			var result2 = (System.Web.Mvc.ViewResult)m_AuthenticatedController.Payment();

			var cart = result2.ViewData.Model as ERPStore.Models.OrderCart;

			Assert.AreEqual(result2.ViewName, "~/views/account/order/payment.aspx");
			Assert.IsNotNull(cart);
		}

		[Test]
		public void Setup_Payment_By_Unauthorized_Payment_Mode()
		{
			var m_AnonymousController = CreateController<ERPStore.Controllers.CheckoutController>();
			var m_AuthenticatedController = CreateAuthenticatedController<ERPStore.Controllers.CheckoutController>();
			var m_CartController = CreateController<ERPStore.Controllers.CartController>();

			m_CartController.AddItemWithQuantity("XBOX360", 5);
			var user = GetCustomerUserPrincipal();
			m_AuthenticatedController.Shipping();
			var result = (System.Web.Mvc.RedirectToRouteResult)m_AuthenticatedController.Shipping(1, null, null, null, null, 0);

			Assert.AreEqual(result.RouteName, ERPStoreRoutes.CHECKOUT_CONFIGURATION);

			var result2 = (System.Web.Mvc.ViewResult)m_AuthenticatedController.Payment("bidon");

			Assert.AreEqual(result2.ViewData.ModelState.IsValid, false);
		}

		[Test]
		public void Confirmation_With_Empty_Cart()
		{
			var m_AuthenticatedController = CreateAuthenticatedController<ERPStore.Controllers.CheckoutController>();

			var result = m_AuthenticatedController.Confirmation() as System.Web.Mvc.RedirectToRouteResult;
			Assert.AreEqual(result.RouteName, ERPStoreRoutes.HOME);
		}

		[Test]
		public void Confirmation_Anonymous()
		{
			var m_AnonymousController = CreateController<ERPStore.Controllers.CheckoutController>();
			Assert.AreEqual(true, ControllerActionInvoker<HttpUnauthorizedResult>().InvokeAction(m_AnonymousController.ControllerContext, "Confirmation"));
		}

		[Test]
		public void Confirmation_Without_Shipping_Step()
		{
			var m_AnonymousController = CreateController<ERPStore.Controllers.CheckoutController>();
			var m_AuthenticatedController = CreateAuthenticatedController<ERPStore.Controllers.CheckoutController>();
			var m_CartController = CreateController<ERPStore.Controllers.CartController>();

			m_CartController.AddCartItem("XBOX360");
			var result = m_AuthenticatedController.Confirmation() as System.Web.Mvc.RedirectToRouteResult;

			Assert.AreEqual(result.RouteName, ERPStoreRoutes.CHECKOUT);
		}

		[Test]
		public void Confirmation_NotChecked()
		{
			var m_AnonymousController = CreateController<ERPStore.Controllers.CheckoutController>();
			var m_AuthenticatedController = CreateAuthenticatedController<ERPStore.Controllers.CheckoutController>();
			var m_CartController = CreateController<ERPStore.Controllers.CartController>();

			m_CartController.AddCartItem("XBOX360");

			m_AuthenticatedController.Shipping();
			m_AuthenticatedController.Shipping(1, null, null, null, null, 0);
			var paymentResult = m_AuthenticatedController.Payment("check") as RedirectToRouteResult;

			var confirmationResult = m_AuthenticatedController.Confirmation() as ViewResult;
			var paymentList = confirmationResult.ViewData["paymentList"] as List<ERPStore.Models.Payment>;

			var selectedPayment = paymentList.Single(i => i.Name.Equals("check", StringComparison.InvariantCultureIgnoreCase));

			var result = m_AuthenticatedController.Confirmation("off") as System.Web.Mvc.ViewResult;

			Assert.AreEqual(result.ViewName, "~/views/account/order/" + selectedPayment.ConfirmationViewName);
			Assert.AreEqual(result.ViewData.ModelState.IsValid, false);
		}

		[Test]
		public void Confirmation_Checked()
		{
			var m_AnonymousController = CreateController<ERPStore.Controllers.CheckoutController>();
			var m_AuthenticatedController = CreateAuthenticatedController<ERPStore.Controllers.CheckoutController>();
			var m_CartController = CreateController<ERPStore.Controllers.CartController>();

			m_CartController.AddCartItem("XBOX360");

			m_AuthenticatedController.Shipping();
			m_AuthenticatedController.Shipping(1, null, null, null, null, 0);
			m_AuthenticatedController.Payment("check");

			var result = m_AuthenticatedController.Confirmation("on") as System.Web.Mvc.RedirectToRouteResult;

			Assert.AreEqual(result.RouteName, ERPStoreRoutes.CHECKOUT_FINALIZE);
		}

		[Test]
		public void Finalize_Anonymous()
		{
			var m_AnonymousController = CreateController<ERPStore.Controllers.CheckoutController>();

			Assert.AreEqual(true, ControllerActionInvoker<HttpUnauthorizedResult>().InvokeAction(m_AnonymousController.ControllerContext, "Finalize"));
		}

		[Test]
		public void Finalize_With_Empty_Cart()
		{
			var m_AuthenticatedController = CreateAuthenticatedController<ERPStore.Controllers.CheckoutController>();

			var result = m_AuthenticatedController.Finalize() as RedirectToRouteResult;
			Assert.AreEqual(result.RouteName, ERPStoreRoutes.HOME);
		}

	}
}
