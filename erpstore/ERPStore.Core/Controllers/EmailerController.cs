using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using ERPStore.Html;

namespace ERPStore.Controllers
{
	/// <summary>
	/// Controller pour les contenus des emails
	/// </summary>
	// [HandleError(View = "500")]
	public class EmailerController : StoreController
	{
		public EmailerController(Services.ISalesService salesService
			, Services.CryptoService cryptoService
			, Services.IAccountService accountService
			, Services.ICartService cartService
			) 




		{
			this.SalesService = salesService;
			this.CryptoService = cryptoService;
			this.AccountService = accountService;
			this.CartService = cartService;
		}

		protected Services.ISalesService SalesService { get; private set; }

		protected Services.CryptoService CryptoService { get; private set; }

		protected Services.IAccountService AccountService { get; private set; }

		protected Services.ICartService CartService { get; private set; }

		public ViewResult OrderConfirmation(Models.ISaleDocument order, string emailUrl)
		{
			var host = Request.Url.Host;

			var urlHelper = new UrlHelper(ControllerContext.RequestContext);
			string accountUrl = string.Format("http://{0}{1}", host, urlHelper.RouteERPStoreUrl(ERPStoreRoutes.ACCOUNT));

			ViewData["accountUrl"] = accountUrl;
			ViewData["encryptedUrl"] = emailUrl;
			ViewData.Model = order;
			return View();
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult DirectOrderConfirmation(string key)
		{
			var mailKey = new
			{
				Code = string.Empty,
				Type = string.Empty,
				Salt = DateTime.Now,
			};
			var result = CryptoService.Decrypt(key, mailKey);
			var code = Convert.ToString(result[0]);
			var type = Convert.ToString(result[1]);
			var salt = Convert.ToDateTime(result[2]);

			Models.ISaleDocument order = null;
			switch (type)
			{
				case "order":
					order = SalesService.GetOrderByCode(code);
					break;
				case "quote":
					order = SalesService.GetQuoteByCode(code);
					break;
				default:
					break;
			}

			var urlHelper = new UrlHelper(ControllerContext.RequestContext);
			string accountUrl = string.Format("http://{0}{1}", Request.Url.Host, urlHelper.RouteERPStoreUrl(ERPStoreRoutes.ACCOUNT));

			var encryptedTicket = CryptoService.EncryptOrderConfirmation(order.Code, DateTime.Now.AddDays(10), false);
			string encryptedUrl = string.Format("http://{0}{1}{2}", Request.Url.Host, urlHelper.RouteERPStoreUrl(ERPStoreRoutes.ORDER_DETAIL), encryptedTicket);

            ViewData.Model = order;

            ViewData["accountUrl"] = accountUrl;
			ViewData["encryptedUrl"] = encryptedUrl;
            ViewBag.EncryptedUrl = encryptedUrl;
            ViewBag.User = order.User;
            ViewBag.FullName = order.User.FullName;
            ViewBag.WebSiteSettings = ERPStoreApplication.WebSiteSettings;
            ViewBag.EncryptedUrl = encryptedUrl;

			return View("OrderConfirmation");
		}

		public ViewResult NewCustomerOrderConfirmation(Models.ISaleDocument order, string encrypteUrl, string password)
		{
			var urlHelper = new UrlHelper(ControllerContext.RequestContext);
			string accountUrl = string.Format("http://{0}{1}", Request.Url.Host, urlHelper.RouteERPStoreUrl(ERPStoreRoutes.ACCOUNT));

			ViewData["accountUrl"] = accountUrl;
			ViewData["encryptedUrl"] = encrypteUrl;
			ViewData["password"] = password;
			ViewData.Model = order;
			return View();
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult DirectNewCustomerOrderConfirmation(string key)
		{
			if (key.IsNullOrTrimmedEmpty())
			{
				return new EmptyResult();
			}
			var mailKey = new
			{
				Code = string.Empty,
				Type = string.Empty,
				Password = string.Empty,
				Salt = DateTime.Now,
			};
			var result = CryptoService.Decrypt(key, mailKey);
			var code = Convert.ToString(result[0]);
			var type = Convert.ToString(result[1]);
			var password = Convert.ToString(result[2]);
			var salt = Convert.ToDateTime(result[3]);

			Models.ISaleDocument order = null;
			switch (type)
			{
				case "order":
					order = SalesService.GetOrderByCode(code);
					break;
				case "quote":
					order = SalesService.GetQuoteByCode(code);
					break;
				default:
					break;
			}

			var host = this.Request.Url.Host;
			var encryptedTicket = CryptoService.EncryptOrderConfirmation(order.Code, DateTime.Now.AddDays(10), false);

			var urlHelper = new UrlHelper(this.ControllerContext.RequestContext);
			string accountUrl = string.Format("http://{0}{1}", host, urlHelper.RouteERPStoreUrl(ERPStoreRoutes.ACCOUNT));
			string encryptedUrl = string.Format("http://{0}{1}{2}", host, urlHelper.RouteERPStoreUrl(ERPStoreRoutes.ORDER_DETAIL), encryptedTicket);

			ViewData["accountUrl"] = accountUrl;
			ViewData["encryptedUrl"] = encryptedUrl;
			ViewData["password"] = password;

			ViewData.Model = order;
			return View("NewCustomerOrderConfirmation");
		}

		public ViewResult AccountConfirmation(Models.User user, string emailUrl)
		{
			var host = Request.Url.Host;
			var accountKey = CryptoService.EncryptAccountConfirmation(user.Email, user.Id);
			var urlHelper = new UrlHelper(ControllerContext.RequestContext);
			var accountConfirmationUrl = urlHelper.RouteERPStoreUrl(ERPStoreRoutes.ACCOUNT_CONFIRMATION, null);
			string confirmationUrl = string.Format("http://{0}{1}{2}", host, accountConfirmationUrl, accountKey);

			ViewData.Model = user;
			ViewData["confirmationUrl"] = confirmationUrl;
			ViewBag.ConfirmationUrl = confirmationUrl;
			ViewData["encryptedUrl"] = emailUrl;
			ViewBag.EncryptedUrl = emailUrl;
			ViewBag.User = user;
			ViewBag.WebSiteSettings = ERPStore.ERPStoreApplication.WebSiteSettings;
			return View();
		}

		[ActionFilters.TrackerActionFilter]
		public ViewResult DirectAccountConfirmation(string key)
		{
			var mailKey = new
			{
				UserId = 0,
				Salt = string.Empty,
			};

			var result = CryptoService.Decrypt(key, mailKey);
			var userId = Convert.ToInt32(result[0]);

			var user = AccountService.GetUserById(userId);

			var view = AccountConfirmation(user, user.Email);
			view.ViewName = "AccountConfirmation";
			return view;
		}

		public ViewResult OrderModification(ERPStore.Models.Order order, string message)
		{
			ViewData.Model = order;
			ViewData["message"] = message;
			ViewBag.Message = message;
			ViewBag.WebSiteSettings = ERPStore.ERPStoreApplication.WebSiteSettings;
			ViewBag.FullName = order.User.FullName;
			ViewBag.EncryptedUrl = "#";
			return View();
		}

		[ActionFilters.TrackerActionFilter]
		public ViewResult DirectOrderModification(string key)
		{
			var mailKey = new
			{
				Code = string.Empty,
				Salt = DateTime.Now,
			};

			var result = CryptoService.Decrypt(key, mailKey);
			var orderCode = Convert.ToString(result[0]);

			var order = SalesService.GetOrderByCode(orderCode);

			var view = OrderModification(order, string.Empty);
			view.ViewName = "OrderModification";
			return view;

		}

		public ViewResult QuoteCanceled(ERPStore.Models.Quote quote, Models.CancelQuoteReason reason,  string message)
		{
			ViewData.Model = quote;
			ViewData["message"] = message;
			ViewBag.Message = message;
			ViewData["reason"] = reason;
			ViewBag.Reason = reason;
			ViewBag.WebSiteSettings = ERPStore.ERPStoreApplication.WebSiteSettings;
			return View();
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult DirectChangePassword(string key)
		{
			var mailKey = new
			{
				UserId = 0,
				ExpirationDate = DateTime.MinValue,
			};

			var result = CryptoService.Decrypt(key, mailKey);
			var userId = Convert.ToInt32(result[0]);
			var expirationDate = Convert.ToDateTime(result[1]);

			if (expirationDate < DateTime.Today)
			{
				return Content("Clé invalide", "text/plain");
			}

			var user = AccountService.GetUserById(userId);

			var host = Request.Url.Host;
			var callbackKey = CryptoService.EncryptChangePassword(user.Id, user.Email);
			var urlHelper = new UrlHelper(ControllerContext.RequestContext);
			var callbackUrl = string.Format("http://{0}{1}/{2}", host, urlHelper.RouteERPStoreUrl(ERPStoreRoutes.ACCOUNT_CHANGE_PASSWORD), callbackKey);

			var view = ChangePassword(user.FullName, callbackUrl, "#");
			view.ViewName = "ChangePassword";
			return view;
		}

		public ViewResult ChangePassword(string personFullName, string callbackUrl, string encryptedUrl)
		{
			var urlHelper = new UrlHelper(ControllerContext.RequestContext);
			ViewData["FullName"] = personFullName;
			ViewBag.FullName = personFullName;
			ViewData["EncryptedUrl"] = callbackUrl;
			ViewBag.CallbackUrl = callbackUrl;
			ViewData["EncryptedUrl2"] = encryptedUrl;
			ViewBag.EncryptedUrl = encryptedUrl;
			ViewData["accountUrl"] = urlHelper.RouteERPStoreUrl(ERPStoreRoutes.ACCOUNT, null);
			ViewBag.WebSiteSettings = ERPStore.ERPStoreApplication.WebSiteSettings;
			return View();
		}

		public ActionResult QuoteConfirmation(ERPStore.Models.QuoteCart cart)
		{
			if (cart == null)
			{
				return this.Redirect("/");
			}
			ViewData.Model = cart;
			return View("quoterequest");
		}

		public ActionResult PaymentByCardFailed(Models.OrderCart cart, Models.User user, string message)
		{
			if (cart == null)
			{
				return new EmptyResult();
			}
			ViewData.Model = cart;
			ViewBag.Message = message;
			ViewData["mesage"] = message;
			ViewBag.User = user;
			ViewData["user"] = user;
			ViewBag.EncryptedUrl = "#";
			ViewBag.FullName = user.FullName;
			ViewBag.WebSiteSettings = ERPStore.ERPStoreApplication.WebSiteSettings;

			return View("paymentfailed");
		}

		public ActionResult AnonymousPaymentByCardFailed(Models.OrderCart cart, Models.RegistrationUser registration, string message)
		{
			if (cart == null)
			{
				return new EmptyResult();
			}
			ViewData.Model = cart;
			ViewBag.Message = message;
			ViewData["message"] = message;
			ViewBag.Registration = registration;
			ViewData["registration"] = registration;
			ViewBag.EncryptedUrl = "#";
			ViewBag.FullName = registration.FullName;
			ViewBag.WebSiteSettings = ERPStore.ERPStoreApplication.WebSiteSettings;
			return View("paymentfailed");
		}

		public ActionResult DirectSendPaymentByCardFailed(string key)
		{
			var mailKey = new
			{
				CartCode = string.Empty,
				Email = string.Empty,
				FullName = string.Empty,
				ExpirationDate = DateTime.Now,
				Message = string.Empty,
			};

			var result = CryptoService.Decrypt(key, mailKey);
			Models.User user = null;
			string email = Convert.ToString(result[1]);
			string fullName = Convert.ToString(result[2]);
			var cartCode = Convert.ToString(result[0]);
			var message = Convert.ToString(result[4]);

			var cart = CartService.GetCartById(cartCode) as Models.OrderCart;
			if (cart.CustomerId.GetValueOrDefault(0) != 0)
			{
				user = AccountService.GetUserById(cart.CustomerId.Value);

				if (user != null
					&& user.Email.IsNullOrTrimmedEmpty())
				{
					return new EmptyResult();
				}
				var v = PaymentByCardFailed(cart as Models.OrderCart, user, message) as ViewResult;
				v.ViewName = "PaymentByCardFailed";
				return v;
			}
			else
			{
				var registrationUser = AccountService.GetRegistrationUser(cart.VisitorId);
				if (registrationUser == null)
				{
					return new EmptyResult();
				}
				var v = AnonymousPaymentByCardFailed(cart, registrationUser, message) as ViewResult;
				v.ViewName = "PaymentByCardFailed";
				return v;
			}
		}
	}
}
