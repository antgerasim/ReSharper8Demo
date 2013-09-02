using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Microsoft.Practices.Unity;

namespace ERPStore.Tests.Controllers
{
	[TestFixture]
	public class AnonymousCheckoutControllerTests : TestBase
	{
		[SetUp]
		public override void Initialize()
		{
			base.Initialize();
			m_FolderView = "~/views/anonymouscheckout/{0}";
		}

		[Test]
		public void Shipping_With_Empty_Cart()
		{
			var m_Controller = CreateController<ERPStore.Controllers.AnonymousCheckoutController>();

			var result = m_Controller.Shipping() as System.Web.Mvc.RedirectToRouteResult;
			Assert.AreEqual(result.RouteName, ERPStore.ERPStoreRoutes.HOME);
		}

		[Test]
		public void AnonymousShipping_With_Cashcustomer()
		{
			var anonymousCheckoutController = CreateController<ERPStore.Controllers.AnonymousCheckoutController>();
			var cartController = CreateController<ERPStore.Controllers.CartController>();
			var authenticatedanonymousCheckoutController = CreateAuthenticatedController<ERPStore.Controllers.AnonymousCheckoutController>();

			cartController.AddCartItem("XBOX360");

			var result = anonymousCheckoutController.Shipping() as System.Web.Mvc.ViewResult;
			var cart = result.ViewData.Model as ERPStore.Models.OrderCart;

			var folder = "~/views/anonymouscheckout/{0}";

			Assert.AreEqual(result.ViewName, GetViewName("shipping.aspx"));

			var registrationService = m_Container.Resolve<ERPStore.Services.IAccountService>();
			var registration = registrationService.GetRegistrationUser(anonymousCheckoutController.HttpContext.User.GetUserPrincipal().VisitorId);

			Assert.IsNotNull(registration);

			registration.Email = "test@email.com";
			registration.FaxNumber = "0102030405";
			registration.FirstName = "firstname";
			registration.LastName = "lastname";
			registration.PhoneNumber = "0102030405";
			registration.Password = "changemoi";

			var postShippingResult = anonymousCheckoutController.Shipping(
				"shippingRecipientName",
				"shippingStreetName",
				"shippingZipCode",
				"shippingCity",
				ERPStore.Models.Country.Default.Id,
				false,
				-1,
				"billingRecipientName",
				"billingStreet",
				"billingZipCode",
				"billingCity",
				ERPStore.Models.Country.Default.Id,
				registration
				,registration.Email
				) as System.Web.Mvc.RedirectToRouteResult;

			Assert.AreEqual(postShippingResult.RouteName, ERPStore.ERPStoreRoutes.CHECKOUT_CONFIGURATION);

			registration = registrationService.GetRegistrationUser(anonymousCheckoutController.HttpContext.User.GetUserPrincipal().VisitorId);

			Assert.AreEqual(registration.FirstName, "firstname");

			var configurationResult = anonymousCheckoutController.Configuration() as System.Web.Mvc.ViewResult;
			cart = configurationResult.ViewData.Model as ERPStore.Models.OrderCart;

			Assert.AreEqual(cart.BillingAddress.RecipientName, "billingRecipientName");
			Assert.AreEqual(cart.DeliveryAddress.RecipientName, "shippingRecipientName");

			Assert.AreEqual(configurationResult.ViewName, string.Format(folder,"configuration.aspx"));

			var postConfigurationResult = anonymousCheckoutController.Configuration(
				"message",
				"documentreference",
				"false",
				0) as System.Web.Mvc.RedirectToRouteResult;

			Assert.AreEqual(postConfigurationResult.RouteName, ERPStore.ERPStoreRoutes.CHECKOUT_PAYMENT);

			var paymentResult = anonymousCheckoutController.Payment() as System.Web.Mvc.ViewResult;
			cart = paymentResult.ViewData.Model as ERPStore.Models.OrderCart;
			var paymentList = paymentResult.ViewData["paymentList"] as List<ERPStore.Models.Payment>;

			Assert.IsNotNull(paymentList);
			Assert.AreEqual(cart.Message, "message");

			Assert.AreEqual(paymentResult.ViewName, string.Format(folder,"payment.aspx"));

			var selectedPayment = paymentList.First();
			var postPaymentResult = anonymousCheckoutController.Payment(selectedPayment.Name) as System.Web.Mvc.RedirectToRouteResult;

			Assert.AreEqual(postPaymentResult.RouteName, selectedPayment.ConfirmationRouteName);

			var confirmationResult = anonymousCheckoutController.Confirmation() as System.Web.Mvc.ViewResult;
			cart = confirmationResult.ViewData.Model as ERPStore.Models.OrderCart;

			Assert.AreEqual(cart.PaymentModeName, selectedPayment.Name);
			Assert.AreEqual(confirmationResult.ViewName, string.Format(folder, selectedPayment.ConfirmationViewName));

			var postConfirmationResult = anonymousCheckoutController.Confirmation("on") as System.Web.Mvc.RedirectToRouteResult;

			Assert.AreEqual(postConfirmationResult.RouteName, ERPStoreRoutes.CHECKOUT_FINALIZE);

			var finalizeResult = anonymousCheckoutController.Finalize() as System.Web.Mvc.RedirectToRouteResult;

			Assert.AreEqual(finalizeResult.RouteName, ERPStoreRoutes.CHECKOUT_FINALIZED);
			var key = Convert.ToString(finalizeResult.RouteValues["key"]);

			// Simulation de l'evenement OnAuthenticateRequest de HttpApplication
			authenticatedanonymousCheckoutController.HttpContext.User = anonymousCheckoutController.User;

			var finalizedResult = authenticatedanonymousCheckoutController.Finalized(key) as System.Web.Mvc.ViewResult;
			var order = finalizedResult.ViewData.Model as ERPStore.Models.ISaleDocument;

			Assert.IsNotNull(order);
			Assert.AreEqual(finalizedResult.ViewName, string.Format(folder,selectedPayment.FinalizedViewName));
		}

	}
}
