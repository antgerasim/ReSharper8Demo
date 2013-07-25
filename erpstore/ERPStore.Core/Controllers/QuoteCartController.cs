using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Microsoft.Practices.Unity;

using ERPStore.Html;

namespace ERPStore.Controllers
{
    /// <summary>
    /// Controller du panier de type Devis
    /// </summary>
	//[HandleError(View = "500")]
	public class QuoteCartController : StoreController
    {
		public QuoteCartController(
			Services.ICartService cartService
			,Services.ICatalogService catalogService
			,Services.ISalesService salesService
			,Services.IAccountService accountService
			,Services.IEmailerService emailerService
            ,Services.CryptoService cryptoService
			)
		{
			this.CartService = cartService;
			this.CatalogService = catalogService;
			this.SalesService = salesService;
			this.AccountService = accountService;
			this.EmailerService = emailerService;
            this.CryptoService = cryptoService;
		}

		#region Properties

		protected Services.ICartService CartService { get; set; }

		protected Services.ICatalogService CatalogService { get; set; }

		protected Services.ISalesService SalesService { get; set; }

		protected Services.IAccountService AccountService { get; set; }

		protected Services.IEmailerService EmailerService { get; set; }

        protected Services.CryptoService CryptoService { get; set; }

		#endregion

		[ActionFilters.TrackerActionFilter]
        public ActionResult Index()
        {
			var cart = CartService.GetOrCreateQuoteCart(User.GetUserPrincipal());
            var registration = AccountService.GetRegistrationUser(User.GetUserPrincipal().VisitorId);
            if (registration == null)
            {
                registration = AccountService.CreateRegistrationUser();
                registration.BillingAddressCountryId = ERPStoreApplication.WebSiteSettings.Country.Id;
            }
            ViewData["RegistrationUser"] = registration;
			ViewData.Model = cart;
			if (cart.ItemCount == 0)
			{
				return View("EmptyCart");
			}
			return View();
        }

		/// <summary>
		/// Indexes the specified user full name.
		/// </summary>
		/// <param name="form">The form.</param>
		/// <returns></returns>
		[AcceptVerbs(HttpVerbs.Post)]
		// [System.Web.Mvc.ValidateAntiForgeryToken]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Index(FormCollection form)
		{
			if (form == null || form.Count == 0)
			{
				return Redirect("/");
			}
			var principal = User.GetUserPrincipal();
			var user = principal.CurrentUser;
			var cart = CartService.GetOrCreateQuoteCart(principal);
            var registration = AccountService.GetRegistrationUser(User.GetUserPrincipal().VisitorId);
            if (registration == null)
            {
                registration = AccountService.CreateRegistrationUser();
                registration.BillingAddressCountryId = ERPStoreApplication.WebSiteSettings.Country.Id;
            }

            ViewData["RegistrationUser"] = registration;
            ViewData.Model = cart;
			if (cart == null 
                || cart.ItemCount == 0)
			{
				return View("EmptyCart");
			}

			if (cart.IsSent 
                || cart.ConvertedEntityId.HasValue)
			{
				ModelState.AddModelError("_FORM", "cette demande de devis à déjà été envoyée");
				return View();
			}

			var nomailForm = form["nomail"];
			var messageForm = form["message"];
			var documentReferenceForm = form["documentReference"];
			var lastNameForm = form["lastname"];
			var firstNameForm = form["firstname"];
			var presentationIdForm = form["presentationId"];
			var corporateNameForm = form["corporatename"];
			var emailForm = form["email"];
			var phoneNumberForm = form["phonenumber"];
			var faxNumberForm = form["faxnumber"];
			var countryIdForm = form["countryId"];
			var zipCodeForm = form["zipcode"];

			if (user == null)
			{
				var country = Models.Country.Default;
				if (!countryIdForm.IsNullOrTrimmedEmpty())
				{
					try
					{
						country = Models.Country.GetByKey(Convert.ToInt32(countryIdForm));
					}
					catch (Exception ex)
					{
						LogError(Logger, ex);
					}
				}

				registration.FirstName = firstNameForm;
                registration.LastName = lastNameForm;
				var presentationId = 3;
				int.TryParse(presentationIdForm, out presentationId);
                registration.PresentationId = presentationId;
                registration.Email = emailForm;
                registration.PhoneNumber = phoneNumberForm;
                registration.FaxNumber = faxNumberForm;
                registration.ShippingAddressCountryId = country.Id;
                registration.ShippingAddressZipCode = zipCodeForm;
				registration.CorporateName = corporateNameForm;
			}

			cart.Message = messageForm;
			cart.CustomerDocumentReference = documentReferenceForm;

			var quantityForm = form["quantity"];
			var values = quantityForm.Split(',');

			for (int i = 0; i < cart.ItemCount; i++)
			{
				try
				{
					var cartItem = cart.Items[i];
					cartItem.Quantity = GetQuantity(cartItem.Product, values[i]);
				}
				catch (Exception ex)
				{
					Logger.Warn(ex.Message);
					continue;
				}
			}

			var brokenRules = SalesService.ValidateQuoteCart(cart, HttpContext);
			ModelState.AddModelErrors(brokenRules);

			if (!ModelState.IsValid)
			{
				return View();
			}

			try
			{
                using (var ts = TransactionHelper.GetNewReadCommitted())
                {
                    // Enregistrement de la personne
                    if (registration != null)
                    {
                        this.AccountService.SaveRegistrationUser(User.GetUserPrincipal().VisitorId, registration);
                    }

                    // Mise a jour du panier en attente de traitement 
                    // par un vendeur
                    cart.IsSent = true;
                    CartService.Save(cart);
                    ts.Complete();
                }
			}
			catch (Exception ex)
			{
				LogError(Logger, ex);
				ModelState.AddModelError("_FORM", "un problème technique empêche cette opération de se dérouler correctement, veuillez rééssayer ultérieurement");
			}

			if (!ModelState.IsValid)
			{
				return View();
			}

			return RedirectToERPStoreRoute(ERPStoreRoutes.QUOTECART_SENT, new { nomail = nomailForm, cartId = cart.Code });
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult QuoteSent(string nomail, string cartId)
		{
			var cart = CartService.GetCartById(cartId) as Models.QuoteCart;
			var user = User.GetUserPrincipal().CurrentUser;
			string emailTo = null;
			string fullName = null;
			if (user != null)
			{
				emailTo = user.Email;
				fullName = user.FullName;
			}
			else
			{
				var registration = AccountService.GetRegistrationUser(User.GetUserPrincipal().VisitorId);
				emailTo = registration.Email;
				fullName = registration.FullName;
			}

            try
            {
                EmailerService.SendQuoteRequest(this, cart, emailTo, fullName);
                // EmailerService.SendAdminQuoteRequest(cartId);
            }
            catch (Exception ex)
            {
				this.LogError(Logger, ex);
            }

			ViewData.Model = cart;
			return View("quotesent");
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.TrackerActionFilter]
		public ActionResult AddItem(string productCode)
		{
			var product = CatalogService.GetProductByCode(productCode);
			if (product == null)
			{
				return RedirectToERPStoreRoute("Default");
			}

			var cart = CartService.GetOrCreateQuoteCart(User.GetUserPrincipal());
			var quantity = GetQuantity(product, 1);
			bool isCustomerPriceApplied = false;
			var price = SalesService.GetProductSalePrice(product, User.GetUserPrincipal().CurrentUser, quantity, out isCustomerPriceApplied);
			CartService.AddItem(cart, product, quantity, price, isCustomerPriceApplied);
			if (HttpContext != null && HttpContext.Request.UrlReferrer != null)
			{
				cart.LastPage = HttpContext.Request.UrlReferrer.PathAndQuery;
			}
			else
			{
				cart.LastPage = "/";
			}
			ViewData.Model = cart;
            using (var ts = TransactionHelper.GetNewReadCommitted())
            {
                CartService.Save(cart);
                ts.Complete();
            }
			return View("Index");
		}

		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult JsAdd(string productCode)
		{
			if (productCode.IsNullOrTrimmedEmpty())
			{
				return new JsonResult();
			}
			var product = CatalogService.GetProductByCode(productCode);
			if (product == null)
			{
				return new JsonResult();
			}
			return JsAddItemToCart(product, product.Packaging.Value);
		}

		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult JsAddItemWithQuantity(string productCode, int quantity)
		{
			if (productCode.IsNullOrTrimmedEmpty())
			{
				return new JsonResult();
			}
			var product = CatalogService.GetProductByCode(productCode);
			if (product == null)
			{
				return new JsonResult();
			}
			return JsAddItemToCart(product, quantity);
		}

		private ActionResult JsAddItemToCart(Models.Product product, int quantity)
		{
			if (product == null)
			{
				return new JsonResult();
			}
			quantity = GetQuantity(product, quantity);
			bool isCustomerPriceApplied = false;
			var price = SalesService.GetProductSalePrice(product, User.GetUserPrincipal().CurrentUser, quantity, out isCustomerPriceApplied);
			var cart = CartService.GetOrCreateQuoteCart(User.GetUserPrincipal());

            if (this.Request.UrlReferrer != null)
            {
                cart.LastPage = this.Request.UrlReferrer.PathAndQuery;
            }
            else
            {
                cart.LastPage = "/";
            }

            using (var ts = TransactionHelper.GetNewReadCommitted())
            {
                CartService.AddItem(cart, product, quantity, price, isCustomerPriceApplied);
                CartService.Save(cart);
                ts.Complete();
            }
			
			var urlHelper = new UrlHelper(this.ControllerContext.RequestContext);

			return Json(new
			{
				title = product.Title,
				quantity = quantity,
				cartUrl = urlHelper.QuoteCartHref(),
				productImage = product.DefaultImage != null ? HttpUtility.UrlEncode(product.DefaultImage.Url) : string.Empty,
			});
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult Clear()
		{
			var cart = CartService.GetCurrentQuoteCart(User.GetUserPrincipal());
            using (var ts = TransactionHelper.GetNewReadCommitted())
            {
                CartService.Clear(cart);
                CartService.Save(cart);
                ts.Complete();
            }
			ViewData.Model = cart;
			return View("EmptyCart");
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Recalc(FormCollection form)
		{
			var cart = CartService.GetCurrentQuoteCart(User.GetUserPrincipal());
			if (form == null || form["quantity"] == null)
			{
				ViewData.Model = cart;
				return View("Index");
			}
			var values = form["quantity"].Split(',');

			for (int i = 0; i < cart.ItemCount; i++)
			{
				var cartItem = cart.Items[i];
				cartItem.Quantity = GetQuantity(cartItem.Product, values[i]);
			}
			ViewData.Model = cart;
            using (var ts = TransactionHelper.GetNewReadCommitted())
            {
                CartService.Save(cart);
                ts.Complete();
            }
			return View("Index");
		}

		[AcceptVerbs(HttpVerbs.Get)]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Remove(int index)
		{
			var cart = CartService.GetCurrentQuoteCart(User.GetUserPrincipal());
			CartService.RemoveItem(cart,index);
            using (var ts = TransactionHelper.GetNewReadCommitted())
            {
                CartService.Save(cart);
                ts.Complete();
            }
			if (cart == null 
				|| cart.ItemCount == 0)
			{
				return View("EmptyCart");
			}
			ViewData.Model = cart;
			return View("Index");
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult Delete(string cartId)
		{
			CartService.DeleteCart(cartId, User.GetUserPrincipal());
			// return RedirectToAction("Index");
			return RedirectToERPStoreRoute(ERPStoreRoutes.QUOTECART);
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult Change(string cartId)
		{
			CartService.ChangeCurrentCart(cartId, User.GetUserPrincipal());
			// return RedirectToAction("Index");
			return RedirectToERPStoreRoute(ERPStoreRoutes.QUOTECART);
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult Show(string cartId)
		{
			var cart = CartService.GetActiveCartById(cartId);
			if (cart == null || cart.ItemCount == 0)
			{
				return View("EmptyCart");
			}
			ViewData.Model = cart;
			return View("Index");
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult Create()
		{
			var cart = CartService.CreateQuoteCart(User.GetUserPrincipal());
			CartService.AddCart(cart);
			CartService.ChangeCurrentCart(cart.Code, User.GetUserPrincipal());
			return Redirect("/");
		}

        [Obsolete("use js instead", true)]
		public ActionResult Script()
		{
			Response.Cache.SetCacheability(HttpCacheability.Public);
			Response.Cache.SetExpires(DateTime.Now.AddDays(1));
			Response.ContentType = "text/javascript";
			return View("script");
		}

		#region Partial Rendering

		public ActionResult ShowStatus(string viewName)
		{
			var cart = CartService.GetCurrentQuoteCart(User.GetUserPrincipal());
			ViewData.Model = cart;
			return PartialView(viewName ?? "_status");
		}

		public ActionResult ShowCurrentCartList(string viewName)
		{
			var list = CartService.GetCurrentQuoteList(User.GetUserPrincipal());
			ViewData.Model = list;
			return PartialView(viewName ?? "_cartlist");
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult ShowAddToCart(string productCode, int? quantity, string viewName)
		{
			if (productCode.IsNullOrEmpty())
			{
				return new EmptyResult();
			}
			var product = CatalogService.GetProductByCode(productCode);
			if (product == null)
			{
				return PartialView("_unknownproduct");
			}
			var urlHelper = new UrlHelper(this.ControllerContext.RequestContext);
			var qty = GetQuantity(product, quantity.GetValueOrDefault(1));
			bool isCustomerPriceApplied = false;
			var price = SalesService.GetProductSalePrice(product, User.GetUserPrincipal().CurrentUser, qty, out isCustomerPriceApplied);

			JsAddItemToCart(product, qty);

			ViewData.Model = new Models.CartItemNeeded()
			{
				Product = product,
				CartUrl = urlHelper.CartHref(),
				Quantity = qty,
				Price = price,
			};
            viewName = viewName ?? "_addtocart";

			return PartialView(viewName);
		}

		#endregion

		#region Private

		private int GetQuantity(Models.Product product, object qty)
		{
			var quantity = product.Packaging.Value;
			if (!int.TryParse(qty.ToString(), out quantity))
			{
				quantity = product.Packaging.Value;
			}
			quantity = Math.Max(Math.Max(product.MinimumSaleQuantity, 1), quantity);
			var mod = quantity % product.Packaging.Value;
			mod = (mod == 0 ? 0 : 1);
			var ratio = (quantity / product.Packaging.Value);
			quantity = (ratio + mod) * product.Packaging.Value;
			return quantity;
		}

		#endregion

	}
}
