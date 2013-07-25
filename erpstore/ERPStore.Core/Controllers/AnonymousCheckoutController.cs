using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ERPStore.Controllers
{
	/// <summary>
	/// Controller pour la phase de commande avec inscription transparente
	/// </summary>
	public class AnonymousCheckoutController : StoreController
	{
		public AnonymousCheckoutController(
			Services.ISalesService salesService
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

		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Shipping()
		{
			Logger.Debug("Choix de l'adresse de livraison");
			var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());
			if (cart == null 
				|| cart.ItemCount == 0)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			var brokenRules = SalesService.ValidateOrderCart(cart, HttpContext);
			ModelState.AddModelErrors(brokenRules);

			if (!ModelState.IsValid)
			{
				ViewData.Model = cart;
				return RedirectToERPStoreRoute(ERPStoreRoutes.CART);
			}

			var user = User.GetUserPrincipal().CurrentUser;
			ViewData.Model = cart;

			var viewName = "shipping";
			int index = -1;
			if (user == null)
			{
				var registration = AccountService.GetRegistrationUser(User.GetUserPrincipal().VisitorId);
				if (registration == null)
				{
					registration = AccountService.CreateRegistrationUser();
					bool isNewCustomer = false;
					registration.VisitorId = HttpContext.GetOrCreateVisitorId(out isNewCustomer);
					AccountService.SaveRegistrationUser(User.GetUserPrincipal().VisitorId, registration);
				}
				user = AccountService.CreateUserFromRegistration(registration);
				ViewData["RegistrationUser"] = registration;
			}
			else
			{
				viewName = "connectedshipping";
			}

			if (cart.BillingAddress == null)
			{
				cart.BillingAddress = user.DefaultAddress;
			}

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

			return View(viewName);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Shipping(string shippingRecipientName, string shippingStreet, string shippingZipCode, string shippingCity, int shippingCountryId
									, bool sameBillingAddress
									, int addressIndex
									, string billingRecipientName, string billingStreet, string billingZipCode, string billingCity, int billingCountryId
									, Models.RegistrationUser registrationUser
									, string emailConfirmation)
		{

			var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());
			if (cart == null || cart.ItemCount == 0)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			var user = User.GetUserPrincipal().CurrentUser;
			ViewData.Model = cart;

			var shippingAddress = new Models.Address();
			var billingAddress = new Models.Address();
			Models.RegistrationUser registration = null;

			if (user == null)
			{
				shippingAddress.RecipientName = shippingRecipientName;
				shippingAddress.Street = shippingStreet;
				shippingAddress.ZipCode = shippingZipCode;
				shippingAddress.CountryId = shippingCountryId;
				shippingAddress.City = shippingCity;

				var shippingAddressBrokenrules = AccountService.ValidateUserAddress(shippingAddress, HttpContext);
				foreach (var item in shippingAddressBrokenrules)
				{
					item.PropertyName = "shipping" + item.PropertyName;
				}
				ViewData.ModelState.AddModelErrors(shippingAddressBrokenrules);

				if (!sameBillingAddress)
				{
					billingAddress.RecipientName = billingRecipientName;
					billingAddress.Street = billingStreet;
					billingAddress.ZipCode = billingZipCode;
					billingAddress.CountryId = billingCountryId;
					billingAddress.City = billingCity;

					var billingAddressBrokenrules = AccountService.ValidateUserAddress(billingAddress, HttpContext);
					foreach (var item in billingAddressBrokenrules)
					{
						item.PropertyName = "billing" + item.PropertyName;
					}
					ModelState.AddModelErrors(billingAddressBrokenrules);
				}

				// Pour passer tout test
				registrationUser.Password = (registrationUser.Password.IsNullOrTrimmedEmpty()) ? "1234567489abcdefg" : registrationUser.Password;

				var registrationUserBrokenRules = AccountService.ValidateRegistrationUser(registrationUser, HttpContext);
				ModelState.AddModelErrors(registrationUserBrokenRules);

				registration = AccountService.GetRegistrationUser(User.GetUserPrincipal().VisitorId);
				if (registration == null)
				{
					// Ce cas ne doit etre possible normalement
					registration = AccountService.CreateRegistrationUser();
				}

				if (registration.Email.IsNullOrTrimmedEmpty()
					&& (emailConfirmation.IsNullOrTrimmedEmpty()
					|| !registrationUser.Email.Equals(emailConfirmation, StringComparison.InvariantCultureIgnoreCase)))
				{
					ModelState.AddModelError("emailConfirmation", "L'Email indiqué n'est pas confirmé");
				}

				// Adresse de livraison
				registration.ShippingAddressCity = shippingAddress.City;
				registration.ShippingAddressCountryId = shippingAddress.CountryId;
				registration.ShippingAddressRecipientName = shippingAddress.RecipientName;
				registration.ShippingAddressRegion = shippingAddress.Region;
				registration.ShippingAddressStreet = shippingAddress.Street;
				registration.ShippingAddressZipCode = shippingAddress.ZipCode;

				registration.IsSameBillingAddress = sameBillingAddress;
				if (!sameBillingAddress)
				{
					// Adresse de facturation
					registration.BillingAddressCity = billingAddress.City;
					registration.BillingAddressCountryId = billingAddress.CountryId;
					registration.BillingAddressRecipientName = billingAddress.RecipientName;
					registration.BillingAddressRegion = billingAddress.Region;
					registration.BillingAddressStreet = billingAddress.Street;
					registration.BillingAddressZipCode = billingAddress.ZipCode;
				}

				// Informations sur la société
				registration.CorporateEmail = registrationUser.CorporateEmail;
				registration.CorporateFaxNumber = registrationUser.CorporateFaxNumber;
				registration.CorporateName = registrationUser.CorporateName;
				registration.CorporatePhoneNumber = registrationUser.CorporatePhoneNumber;
				registration.CorporateSocialStatus = registrationUser.CorporateSocialStatus;
				registration.CorporateWebSite = registrationUser.CorporateWebSite;
				registration.FaxNumber = registrationUser.FaxNumber;
				registration.NAFCode = registrationUser.NAFCode;
				registration.SiretNumber = registrationUser.SiretNumber;
				registration.VATNumber = registrationUser.TVANumber;
				registration.VatMandatory = registrationUser.VatMandatory;
				registration.RcsNumber = registrationUser.RcsNumber;

				// Informations personnelles
				registration.Email = registrationUser.Email;
				registration.FirstName = registrationUser.FirstName;
				registration.LastName = registrationUser.LastName;
				registration.MobileNumber = registrationUser.MobileNumber;
				// registration.Password = registrationUser.Password;
				registration.PhoneNumber = registrationUser.PhoneNumber;
				registration.PresentationId = registrationUser.PresentationId;
				registration.ReturnUrl = registrationUser.ReturnUrl;

				AccountService.SaveRegistrationUser(User.GetUserPrincipal().VisitorId, registration);
				user = AccountService.CreateUserFromRegistration(registration);
			}
			else
			{
				if (addressIndex == -1) // Cas d'une nouvelle adresse
				{
					shippingAddress.RecipientName = shippingRecipientName;
					shippingAddress.Street = shippingStreet;
					shippingAddress.ZipCode = shippingZipCode;
					shippingAddress.CountryId = shippingCountryId;
					shippingAddress.City = shippingCity;

					var shippingAddressBrokenrules = AccountService.ValidateUserAddress(shippingAddress, HttpContext);
					foreach (var item in shippingAddressBrokenrules)
					{
						item.PropertyName = "shipping" + item.PropertyName;
					}
					ViewData.ModelState.AddModelErrors(shippingAddressBrokenrules);

					if (ModelState.IsValid)
					{
						AddressService.SaveAddress(user, shippingAddress, true);
					}
				}
			}

			ViewData["SelectedAddressId"] = addressIndex;

			Models.Address address = null;
			if (addressIndex == -1) // Nouvelle adresse
			{
				address = shippingAddress;
				user.DeliveryAddressList.Add(address);
			}
			else
			{
				address = user.DeliveryAddressList[addressIndex];
			}

			cart.DeliveryAddress = address;

			if (sameBillingAddress)
			{
				cart.BillingAddress = cart.DeliveryAddress;
			}
			else
			{
				cart.BillingAddress = user.DefaultAddress;
			}

			if (!ModelState.IsValid)
			{
				ViewData["RegistrationUser"] = registration;
				if (registration != null)
				{
					return View("shipping");
				}
				else
				{
					return View("connectedshipping");
				}
			}

			using (var ts = TransactionHelper.GetNewReadCommitted())
			{
				CartService.Save(cart);
				ts.Complete();
			}

			return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT_CONFIGURATION);
		}

		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Configuration()
		{
			Logger.Debug("Configuration of order");
			var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());
			if (cart == null 
				|| cart.ItemCount == 0)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			var user = User.GetUserPrincipal().CurrentUser;

			if (user != null)
			{
				if (cart.DeliveryAddress == null
					|| cart.BillingAddress == null)
				{
					// Dans le cas d'un user connecté l'etape de saisie
					// des adresses n'a pas été réalisée, on y retourne
					return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT);
				}
			}
			else
			{
				// Cas d'un vistieur anonyme
				var registration = AccountService.GetRegistrationUser(User.GetUserPrincipal().VisitorId);
				if (registration == null)
				{
					// La session à expirée ou tout autre chose
					// on retourne sur l'etape de saisie des adresses 
					return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT);
				}
			}

			ViewData.Model = cart;
			return View();
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Configuration(string message, string documentReference, string partialDelivery, int conveyorIndex)
		{
			var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());
			if (cart == null 
				|| cart.ItemCount == 0)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT);
			}

			var user = User.GetUserPrincipal().CurrentUser;
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

			return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT_PAYMENT);
		}

		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Payment()
		{
			Logger.Debug("Choosing payment mode");
			var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());
			if (cart == null 
				|| cart.ItemCount == 0)
			{
				return RedirectToERPStoreRoute(ERPStore.ERPStoreRoutes.HOME);
			}

			var user = User.GetUserPrincipal().CurrentUser;

			if (user != null 
				&& (cart.DeliveryAddress == null 
				|| cart.BillingAddress == null))
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT);
			}

			var paymentList = SalesService.GetPaymentList(cart, User.GetUserPrincipal());
			ViewData["paymentList"] = paymentList;

			ViewData.Model = cart;
			return View();
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Payment(string paymentModeName)
		{
			var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());

			if (cart == null
				|| cart.ItemCount == 0)
			{
				return RedirectToERPStoreRoute(ERPStore.ERPStoreRoutes.CHECKOUT);
			}

			var user = User.GetUserPrincipal().CurrentUser;

			if (user != null 
				&& (cart.DeliveryAddress == null
				|| cart.BillingAddress == null))
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT);
			}

			if (paymentModeName.IsNullOrTrimmedEmpty())
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT_PAYMENT);
			}

			ViewData.Model = cart;

			var paymentList = SalesService.GetPaymentList(cart, User.GetUserPrincipal());
			ViewData["paymentList"] = paymentList;
			var selectedPayment = paymentList.SingleOrDefault(i => i.Name.Equals(paymentModeName, StringComparison.InvariantCultureIgnoreCase));

			if (selectedPayment == null)
			{
				Logger.Warn("Payment mode not authorized");
				ViewData.ModelState.AddModelError("_FORM", "Vous devez selectionner un mode de règlement valide");
				return View("payment");
			}

			cart.PaymentModeName = selectedPayment.Name;
			Logger.Info("Choosing payment mode : {0}", selectedPayment.Name);
			using (var ts = TransactionHelper.GetNewReadCommitted())
			{
				CartService.Save(cart);
				ts.Complete();
			}

			string routeName = selectedPayment.ConfirmationRouteName;
			return RedirectToERPStoreRoute(routeName);
		}

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

			var user = User.GetUserPrincipal().CurrentUser;

			if (user != null)
			{
				if (cart.DeliveryAddress == null
				|| cart.BillingAddress == null)
				{
					return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT);
				}
			}
			else
			{
				var registration = AccountService.GetRegistrationUser(User.GetUserPrincipal().VisitorId);
				if (registration == null)
				{
					return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT);
				}
				user = AccountService.CreateUserFromRegistration(registration);
				cart.BillingAddress = user.DefaultAddress;
				cart.DeliveryAddress = user.LastDeliveredAddress;
			}

			if (cart.PaymentModeName.IsNullOrTrimmedEmpty())
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT_PAYMENT);
			}

			var paymentList = SalesService.GetPaymentList(cart, User.GetUserPrincipal());
			var selectedPayment = paymentList.SingleOrDefault(i => i.Name.Equals(cart.PaymentModeName, StringComparison.InvariantCultureIgnoreCase));

			if (selectedPayment == null)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT_PAYMENT);
			}

			SalesService.ProcessExport(cart, User.GetUserPrincipal().CurrentUser);
			CartService.ApplyProductStockInfoList(cart as Models.OrderCart);

			ViewData.Model = cart;
			if (selectedPayment.ConfirmationViewName.StartsWith("~"))
			{
				return View(selectedPayment.ConfirmationViewName);
			}
			return View(selectedPayment.ConfirmationViewName);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult DirectConfirmation(string cartId)
		{
			var cart = CartService.GetActiveCartById(cartId) as Models.OrderCart;
			if (cart == null || cart.ItemCount == 0)
			{
				return View("EmptyCart");
			}
			if (!cart.CustomerId.HasValue)
			{
				bool isNewVisitor = false;
				var userId = this.HttpContext.GetOrCreateVisitorId(out isNewVisitor);
				if (cart.VisitorId != userId)
				{
					var registrationUser = AccountService.GetRegistrationUser(cart.VisitorId);
					if (registrationUser != null
						&& !registrationUser.UserId.HasValue)
					{
						registrationUser.VisitorId = userId;
						AccountService.SaveRegistrationUser(userId, registrationUser);
					}

					cart.VisitorId = userId;
					CartService.Save(cart);
					CartService.ChangeCurrentCart(cart.Code, User.GetUserPrincipal());
				}
			}
			else
			{
				Response.AddAuthenticatedCookie(cart.CustomerId.Value, true);
			}

			var paymentList = SalesService.GetPaymentList(cart, User.GetUserPrincipal());
			var selectedPayment = paymentList.SingleOrDefault(i => i.Name.Equals(cart.PaymentModeName, StringComparison.InvariantCultureIgnoreCase));

			string routeName = selectedPayment.ConfirmationRouteName;
			return RedirectToERPStoreRoute(routeName);
		}

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

			var user = User.GetUserPrincipal().CurrentUser;

			if (user != null)
			{
				if (cart.DeliveryAddress == null
					|| cart.BillingAddress == null)
				{
					return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT);
				}
			}
			else
			{
				var registration = AccountService.GetRegistrationUser(User.GetUserPrincipal().VisitorId);
				if (registration == null)
				{
					return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT);
				}
				user = AccountService.CreateUserFromRegistration(registration);
				cart.BillingAddress = user.DefaultAddress;
				cart.DeliveryAddress = user.LastDeliveredAddress;
			}

			var brokenRules = SalesService.ValidateOrderCart(cart, HttpContext);
			ModelState.AddModelErrors(brokenRules);

			if (!ModelState.IsValid)
			{
				ViewData.Model = cart;
                return RedirectToERPStoreRoute(ERPStoreRoutes.CART);
			}

			var paymentList = SalesService.GetPaymentList(cart, User.GetUserPrincipal());
			var selectedPayment = paymentList.SingleOrDefault(i => i.Name.Equals(cart.PaymentModeName, StringComparison.InvariantCultureIgnoreCase));

			if (!confirmation)
			{
				ViewData.Model = cart;
				ModelState.AddModelError("condition", "Vous devez accepter nos conditions de ventes pour pouvoir enregistrer cette commande");

				return View(selectedPayment.ConfirmationViewName);
			}

			// Dans le cas ou un petit malin sauterait
			// l'etape de la confirmation on recalcule les frais de port
			SalesService.CalculateShippingFee(cart, User.GetUserPrincipal().CurrentUser);

			cart.AcceptCondition = true;
			using (var ts = TransactionHelper.GetNewReadCommitted())
			{
				CartService.Save(cart);
				ts.Complete();
			}
			return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT_FINALIZE);
		}

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

			var user = User.GetUserPrincipal().CurrentUser;

			if (user != null
				&& (cart.DeliveryAddress == null
				|| cart.BillingAddress == null))
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT);
			}

			if (!cart.AcceptCondition)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT_CONFIRMATION);
			}

			var brokenRules = SalesService.ValidateOrderCart(cart, HttpContext);
			ModelState.AddModelErrors(brokenRules);

			if (!ModelState.IsValid)
			{
                return RedirectToERPStoreRoute(ERPStoreRoutes.CART);
			}

			bool isNewCustomer = false;
			string password = string.Empty;
			// l'etape de la confirmation on recalcule les frais de port
			if (user == null)
			{
				var registration = AccountService.GetRegistrationUser(User.GetUserPrincipal().VisitorId);
				if (registration == null)
				{
					return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT);
				}

				password = registration.Password;
				if (registration.IsSameBillingAddress)
				{
					registration.BillingAddressCity = registration.ShippingAddressCity;
					registration.BillingAddressCountryId = registration.ShippingAddressCountryId;
					registration.BillingAddressRecipientName = registration.ShippingAddressRecipientName;
					registration.BillingAddressRegion = registration.ShippingAddressRegion;
					registration.BillingAddressStreet = registration.ShippingAddressStreet;
					registration.BillingAddressZipCode = registration.ShippingAddressZipCode;
				}

				try
				{
					user = AccountService.RegisterUser(registration);
					Response.AddAuthenticatedCookie(user.Id, true);
					isNewCustomer = true;

					bool isNewVisitor = false;
					EventPublisherService.Publish(new Models.Events.UserAuthenticatedEvent()
					{
						UserId = user.Id,
						VisitorId = HttpContext.GetOrCreateVisitorId(out isNewVisitor),
					});
				}
				catch(Exception ex)
				{
					LogError(Logger, ex);
					ModelState.AddModelError("_FORM", "Un problème technique empèche la creation de votre commande, veuillez reessayer ultérieurement");
				}

				// Affectation de l'adresse de livraison
				cart.DeliveryAddress = new ERPStore.Models.Address();
				cart.DeliveryAddress.City = registration.ShippingAddressCity;
				cart.DeliveryAddress.CountryId = registration.ShippingAddressCountryId;
				cart.DeliveryAddress.RecipientName = registration.ShippingAddressRecipientName;
				cart.DeliveryAddress.Region = registration.ShippingAddressRegion;
				cart.DeliveryAddress.Street = registration.ShippingAddressStreet;
				cart.DeliveryAddress.ZipCode = registration.ShippingAddressZipCode;

				// Affectation de l'adresse de facturation
				cart.BillingAddress = user.DefaultAddress;
				cart.CustomerId = user.Id;
			}

			var paymentList = SalesService.GetPaymentList(cart, User.GetUserPrincipal());
			var selectedPayment = paymentList.SingleOrDefault(i => i.Name.Equals(cart.PaymentModeName, StringComparison.InvariantCultureIgnoreCase));

			if (!ModelState.IsValid)
			{
				return View(selectedPayment.ConfirmationViewName);
			}

			try
			{
				// Calcul des taxes
				SalesService.CalculateShippingFee(cart, user);
				// Traitement de l'export
				SalesService.ProcessExport(cart, user);
				// création de la commande
				order = SalesService.CreateOrderFromCart(user, cart);
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

			if (isNewCustomer)
			{
				AccountService.CloseRegistrationUser(User.GetUserPrincipal().VisitorId, user.Id);
			}

			if (!ModelState.IsValid)
			{
				return View(selectedPayment.ConfirmationViewName);
			}

			// Préparation d'un paramètre encrypté
			var subject = new 
			{ 
				OrderCode = order.Code, 
				DocumentType = (order.Document == ERPStore.Models.SaleDocumentType.Order) ? "1" : "2",
				ExpirationDate = DateTime.Now.AddDays(1),
				IsNewCustomer = isNewCustomer,
				Password = password,
			};
			var key = CryptoService.Encrypt(subject);

			return RedirectToERPStoreRoute(selectedPayment.FinalizedRouteName, new { key = key });
		}

		[Authorize(Roles = "customer")]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Finalized(string key)
		{
			Models.ISaleDocument order = null;
			var subject = new 
			{ 
				Code = string.Empty, 
				DocumentType = string.Empty,
				ExpirationDate = DateTime.MinValue,
				IsNewCustomer = false,
				Password = string.Empty,
			};
			var result = CryptoService.Decrypt(key, subject);
			var code = Convert.ToString(result[0]);
			var documentType = Convert.ToString(result[1]);
			var expirationDate = Convert.ToDateTime(result[2]);
			var isNewCustomer = Convert.ToBoolean(result[3]);
			var password = Convert.ToString(result[4]);
			if (expirationDate < DateTime.Now)
			{
				// Tentative probable de hack
				Logger.Warn("Hack on order {0}", code);
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			if (documentType == "1")
			{
				order = SalesService.GetOrderByCode(code);
			}
			else
			{
				order = SalesService.GetQuoteByCode(code);
			}

			if (order == null)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			//var user = User.GetUserPrincipal().CurrentUser;

			//if (order.User.Id != user.Id)
			//{
			//    return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			//}

			// Envoi du mail de confirmation de commande
			try
			{
				if (isNewCustomer)
				{
					EmailerService.SendNewCustomerOrderConfirmation(this, order, password);
				}
				else
				{
					EmailerService.SendOrderConfirmation(this, order);
				}
			}
			catch (Exception ex)
			{
				LogError(Logger, ex);
			}

			var paymentList = SalesService.GetPaymentList();
			var selectedPayment = paymentList.SingleOrDefault(i => i.Name.Equals(order.PaymentModeName, StringComparison.InvariantCultureIgnoreCase));

			ViewData.Model = order;
			return View(selectedPayment.FinalizedViewName);
		}
	}
}
