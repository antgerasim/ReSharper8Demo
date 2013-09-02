using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ERPStore.Controllers
{
	/// <summary>
	/// Controlleur pour la prise de commande pour un client inscrit
	/// </summary>
	// [HandleError(View = "500")]
	public class CheckoutController : StoreController
	{
		private string m_ViewPath = null;

		public CheckoutController(Services.ISalesService salesService
			, Services.ICartService cartService
			, Services.IAccountService accountService
			, Services.IEmailerService emailerService
			, Services.IDocumentService documentService
			, Services.ICacheService cacheService
			, Services.IAddressService addressService
			, Services.CryptoService cryptoService
			, Services.IIncentiveService IncentiveService
			)
		{
			this.SalesService = salesService;
			this.CartService = cartService;
			this.EmailerService = emailerService;
			this.AccountService = accountService;
			this.DocumentService = documentService;
			this.CacheService = cacheService;
			this.CryptoService = cryptoService;
			this.AddressService = addressService;

			m_ViewPath = "~/views/account/order/{0}";
		}

		protected Services.ISalesService SalesService { get; set; }

		protected Services.ICartService CartService { get; set; }

		protected Services.IAccountService AccountService { get; set; }

		protected Services.IEmailerService EmailerService { get; set; }

		protected Services.IDocumentService DocumentService { get; set; }

		protected Services.ICacheService CacheService { get; set; }

		internal Services.CryptoService CryptoService { get; set; }

		internal Services.IAddressService AddressService { get; set; }

		protected Services.IIncentiveService IncentiveService { get; private set; }

		[Authorize(Roles = "customer")]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Shipping()
		{
			Logger.Debug("Choosing delivery address");
			var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());

			if (cart == null)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			var brokenRules = SalesService.ValidateOrderCart(cart, HttpContext);
			ModelState.AddModelErrors(brokenRules);

			if (!ModelState.IsValid)
			{
				ViewData.Model = cart;
				return View("~/views/cart/index");
			}

			var user = (User as Models.UserPrincipal).CurrentUser;
			ViewData.Model = cart;

			if (cart.BillingAddress == null)
			{
				cart.BillingAddress = user.DefaultAddress;
			}

			int index = -1;
			if (cart.DeliveryAddress != null)
			{
				index = user.DeliveryAddressList.FindIndex(i => i.Id == cart.DeliveryAddress.Id);
			}
			else if (user.LastDeliveredAddress != null)
			{
				index = user.DeliveryAddressList.FindIndex(i => i.Id == user.LastDeliveredAddress.Id);
				cart.DeliveryAddress = user.LastDeliveredAddress;
			}
			else if (user.DeliveryAddressList.IsNotNullOrEmpty())
			{
				cart.DeliveryAddress = user.DeliveryAddressList[0];
				index = 0;
			}
			else
			{
				if (user.DefaultAddress != null)
				{
					cart.DeliveryAddress = user.DefaultAddress;
				}
				index = -1;
			}
			using (var ts = TransactionHelper.GetNewReadCommitted())
			{
				CartService.Save(cart);
				ts.Complete();
			}
			ViewData["SelectedAddressId"] = index;

			return View(string.Format(m_ViewPath,"shipping"));
		}

		[Authorize(Roles = "customer")]
		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Shipping(int addressIndex, string recipientName, string street, string zipCode, string city, int countryId)
		{
			Logger.Info("Delivery address selected");

			var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());

			if (cart == null 
				|| cart.ItemCount == 0)
			{
				// RedirectToAction("Shipping");
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			var user = (User as Models.UserPrincipal).CurrentUser;
			ViewData.Model = cart;

			if (cart.BillingAddress == null)
			{
				cart.BillingAddress = user.DefaultAddress;
			}

			ViewData["SelectedAddressId"] = addressIndex;

			Models.Address address = null;
			// Nouvelle adresse
			if (addressIndex == -1)
			{
				address = new ERPStore.Models.Address();
				address.RecipientName = recipientName;
				address.Street = street;
				address.ZipCode = zipCode;
				address.City = city;
				address.CountryId = countryId;
				ModelState.AddModelErrors(AccountService.ValidateUserAddress(address, HttpContext));
				if (!ModelState.IsValid)
				{
					return View(string.Format(m_ViewPath,"shipping"));
				}
				AddressService.SaveAddress(user, address, true);
				user.DeliveryAddressList.Add(address);
			}
			else
			{
				address = user.DeliveryAddressList[addressIndex];
			}

			cart.DeliveryAddress = address;
			using (var ts = TransactionHelper.GetNewReadCommitted())
			{
				CartService.Save(cart);
				ts.Complete();
			}
			// return RedirectToAction("Configuration");
			return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT_CONFIGURATION);
		}

		[Authorize(Roles = "customer")]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Configuration()
		{
			Logger.Debug("Order configuration");
			var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());
			if (cart == null 
				|| cart.ItemCount == 0)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			if (cart.DeliveryAddress == null 
				|| cart.BillingAddress == null)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT);
			}

			ViewData.Model = cart;
			return View(string.Format(m_ViewPath,"configuration"));
		}

		[Authorize(Roles = "customer")]
		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Configuration(string message, string documentReference, string partialDelivery, int conveyorIndex)
		{
			var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());

			if (cart == null 
				|| cart.ItemCount == 0)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			var user = (User as Models.UserPrincipal).CurrentUser;
			ViewData.Model = cart;

			Models.Conveyor conveyor = null;

			try
			{
				conveyor = ERPStoreApplication.WebSiteSettings.Shipping.ConveyorList[conveyorIndex];
			}
			catch (Exception ex)
			{
				conveyor = ERPStoreApplication.WebSiteSettings.Shipping.DefaultConveyor;
				Logger.Warn(ex.Message);
			}

			cart.Message = message;
			cart.CustomerDocumentReference = documentReference;
			cart.AllowPartialDelivery = (partialDelivery == "true");
			cart.Conveyor = conveyor;

			// On recalcule les frais de port si le transporteur à changé
			SalesService.CalculateShippingFee(cart, user);

			using (var ts = TransactionHelper.GetNewReadCommitted())
			{
				CartService.Save(cart);
				ts.Complete();
			}

			// return RedirectToAction("Payment");
			return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT_PAYMENT);
		}

		[Authorize(Roles = "customer")]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Payment()
		{
			Logger.Debug("Payment choice");
			var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());
			if (cart == null 
				|| cart.ItemCount == 0)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			if (cart.DeliveryAddress == null 
				|| cart.BillingAddress == null)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT_CONFIGURATION);
			}

			var paymentList = SalesService.GetPaymentList(cart, User.GetUserPrincipal());
			ViewData["paymentList"] = paymentList;

			ViewData.Model = cart;
			return View(string.Format(m_ViewPath,"payment"));
		}

		[Authorize(Roles = "customer")]
		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Payment(string paymentModeName)
		{
			var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());

			if (cart == null
				|| cart.ItemCount == 0)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			if (cart.DeliveryAddress == null
				|| cart.BillingAddress == null
				|| paymentModeName.IsNullOrTrimmedEmpty())
			{
				// return RedirectToAction("Shipping");
				return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT);
			}

			ViewData.Model = cart;

			var paymentList = SalesService.GetPaymentList(cart, User.GetUserPrincipal());
			ViewData["paymentList"] = paymentList;
			var selectedPayment = paymentList.SingleOrDefault(i => i.Name.Equals(paymentModeName, StringComparison.InvariantCultureIgnoreCase));

			if (selectedPayment == null)
			{
				Logger.Warn("Payment mode not authorized");
				ViewData.ModelState.AddModelError("_FORM", "Vous devez selectionner un mode de règlement valide");
				return View(string.Format(m_ViewPath,"payment"));
			}

			string routeName = selectedPayment.ConfirmationRouteName;

			cart.PaymentModeName = selectedPayment.Name;
			Logger.Debug("Selected payment mode : {0}", selectedPayment.Name);
			using (var ts = TransactionHelper.GetNewReadCommitted())
			{
				CartService.Save(cart);
				ts.Complete();
			}

			return RedirectToERPStoreRoute(routeName);
		}

		[Authorize(Roles = "customer")]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Confirmation()
		{
			var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());
			if (cart == null 
				|| cart.ItemCount == 0)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			if (cart.DeliveryAddress == null 
				|| cart.BillingAddress == null)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT);
			}

			var paymentList = SalesService.GetPaymentList(cart, User.GetUserPrincipal());
			var selectedPayment = paymentList.SingleOrDefault(i => i.Name.Equals(cart.PaymentModeName, StringComparison.InvariantCultureIgnoreCase));

			SalesService.ProcessExport(cart, User.GetUserPrincipal().CurrentUser);

			ViewData.Model = cart;
			return View(string.Format(m_ViewPath, selectedPayment.ConfirmationViewName));
		}

		[Authorize(Roles = "customer")]
		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Confirmation(string condition)
		{
			bool confirmation = (condition == "on");
			var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());

			if (cart == null 
				|| cart.ItemCount == 0)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			if (cart.DeliveryAddress == null 
				|| cart.BillingAddress == null)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT);
			}

			var paymentList = SalesService.GetPaymentList(cart, User.GetUserPrincipal());
			var selectedPayment = paymentList.SingleOrDefault(i => i.Name.Equals(cart.PaymentModeName, StringComparison.InvariantCultureIgnoreCase));

			if (confirmation)
			{
				// Dans le cas ou un petit malin sauterait
				// l'etape de la confirmation on recalcule les frais de port
				SalesService.CalculateShippingFee(cart, User.GetUserPrincipal().CurrentUser);

				cart.AcceptCondition = true;
				using (var ts = TransactionHelper.GetNewReadCommitted())
				{
					CartService.Save(cart);
					ts.Complete();
				}
				// return RedirectToAction("Finalize");
				return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT_FINALIZE);
			}
			else
			{
				ViewData.Model = cart;
				ModelState.AddModelError("confirmation", "Vous devez accepter nos conditions de ventes pour pouvoir enregistrer cette commande");
			}

			return View(string.Format(m_ViewPath, selectedPayment.ConfirmationViewName));
		}

		[Authorize(Roles = "customer")]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Finalize()
		{
			Models.ISaleDocument order = null;
			var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());
			if (cart == null 
				|| cart.ItemCount == 0)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			ViewData.Model = cart;

			if (cart.DeliveryAddress == null 
				|| cart.BillingAddress == null)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT);
			}

			if (!cart.AcceptCondition)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT_CONFIRMATION);
			}

			// Dans le cas ou un petit malin sauterait
			// l'etape de la confirmation on recalcule les frais de port
			var user = User.GetUserPrincipal().CurrentUser;
			SalesService.CalculateShippingFee(cart, user);

			var principal = User as Models.UserPrincipal;
			try
			{
				// Traitement de l'export
				SalesService.ProcessExport(cart, user);
				// création de la commande
				order = SalesService.CreateOrderFromCart(principal.CurrentUser, cart);
				using (var ts = TransactionHelper.GetNewReadCommitted())
				{
					cart.ConvertedEntityId = order.Id;
					CartService.Save(cart);
					ts.Complete();
				}
			}
			catch (Exception ex)
			{
				LogError(Logger, ex);
				ModelState.AddModelError("_FORM", "Un problème technique empèche la creation de votre commande, veuillez reessayer ultérieurement");
			}

			var paymentList = SalesService.GetPaymentList(cart, User.GetUserPrincipal());
			var selectedPayment = paymentList.SingleOrDefault(i => i.Name.Equals(cart.PaymentModeName, StringComparison.InvariantCultureIgnoreCase));

			if (!ModelState.IsValid)
			{
				return View(string.Format(m_ViewPath, selectedPayment.ConfirmationViewName));
			}

			return RedirectToERPStoreRoute( selectedPayment.FinalizedRouteName , new { d = order.Code, t = (order.Document == ERPStore.Models.SaleDocumentType.Order) ? "1" : "2" });
		}

		[Authorize(Roles = "customer")]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Finalized(string d, string t)
		{
			Models.ISaleDocument order = null;
			if (t == "1")
			{
				order = SalesService.GetOrderByCode(d);
			}
			else
			{
				order = SalesService.GetQuoteByCode(d);
			}
			if (order == null)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			var user = User.GetUserPrincipal().CurrentUser;

			if (order.User.Id != user.Id)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			// Envoi du mail de confirmation
			try
			{
				EmailerService.SendOrderConfirmation(this, order);
			}
			catch (Exception ex)
			{
				LogError(Logger, ex);
			}

			ViewData.Model = order;
			return View(string.Format(m_ViewPath,"finalize"));
		}

	}
}
