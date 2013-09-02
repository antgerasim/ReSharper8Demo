using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Security.Cryptography;

namespace ERPStore.Controllers
{
	/// <summary>
	/// Controlleur pour les reglements
	/// </summary>
	//[HandleError(View = "500")]
	public class PaymentController : StoreController
	{
		public PaymentController(
			Services.ICartService cartService
			, Services.ISalesService salesService
			, Services.IAccountService accountService
			, Services.IEmailerService emailerService
			)
		{
			this.CartService = cartService;
			this.SalesService = salesService;
			this.AccountService = accountService;
			this.EmailerService = emailerService;
		}

		// protected Services.IPaymentService PaymentService { get; set; }
		protected Services.ICartService CartService { get; set; }
		protected Services.ISalesService SalesService { get; set; }
		protected Services.IAccountService AccountService { get; set; }
		protected Services.IEmailerService EmailerService { get; set; }

		[Authorize(Roles="customer")]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Accepted()
		{
			Logger.Info("Callback checkout with success");
			return View("postsale");
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult Declined()
		{
			Logger.Warn("Transaction declined");
			ViewData["message"] = "Erreur lors de la transaction";
			var view = Configuration.ConfigurationSettings.AppSettings["paymentDeclinedView"] ?? "paymentdeclined";
			return View(view);
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult Error()
		{
			Logger.Warn("Transaction error");
			ViewData["message"] = "Erreur lors de la transaction";
			var view = Configuration.ConfigurationSettings.AppSettings["paymentErrorView"] ?? "paymenterror";
			return View(view);
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult Canceled()
		{
			Logger.Warn("Transaction canceled");
			ViewData["message"] = "Annulation de la transaction";
			var view = Configuration.ConfigurationSettings.AppSettings["paymentCanceledView"] ?? "paymentcanceled";
			return View(view);
		}

		protected ERPStore.Models.ISaleDocument ProcessResponse(string cartCode, out bool isNewUser, out string password)
		{
			var cart = CartService.GetActiveCartById(cartCode) as Models.OrderCart;
			Models.ISaleDocument order = null;
			isNewUser = false;
			password = null;
			if (cart == null)
			{
				cart = CartService.GetCartById(cartCode) as Models.OrderCart;
				if (cart.ConvertedEntityId.HasValue)
				{
					return null;
				}
				Logger.Error("Tentative de paiement avec un panier inconnu : {0}", cartCode);
				return null;
			}

			// La reponse à déjà été traitée
			if (cart.ConvertedEntityId.HasValue)
			{
				Logger.Error("Tentative de paiement avec un panier deja converti : {0} {1}", cartCode, cart.ConvertedEntityId.Value);
				return null;
			}

			Models.User user = null;
			Models.RegistrationUser registration = null;

			// Dans le cas d'un achat sans inscription
			if (cart.CustomerId.GetValueOrDefault(0) == 0)
			{
				registration = AccountService.GetRegistrationUser(cart.VisitorId);
				if (registration == null)
				{
					// Traitement en cours par un autre thread
					Logger.Error("Impossible de trouver le propriétaire du panier : {0} alors qu'un reglement existe", cart.Code);
					return null;
				}

				isNewUser = true;
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
				user = AccountService.RegisterUser(registration);

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
			else
			{
				user = AccountService.GetUserById(cart.CustomerId.Value);
			}

			try
			{
				SalesService.ProcessExport(cart, user);
				// création de la commande
				order = SalesService.CreateOrderFromCart(user, cart);
				Logger.Debug("Add payment to order N°{0}", order.Code);
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
				return null;
			}

			if (registration != null)
			{
				AccountService.CloseRegistrationUser(registration.VisitorId, user.Id);
			}

			return order;
		}

		protected ERPStore.Models.ISaleDocument ProcessResponse(string cartCode, string paymentModeName, string transactionId, object serverResponse, out bool isNewUser, out string password)
		{
			var order = ProcessResponse(cartCode, out isNewUser, out password);
			SalesService.AddPaymentToOrder((Models.Order)order, paymentModeName, transactionId, serverResponse);
			return order;
		}
	}
}
