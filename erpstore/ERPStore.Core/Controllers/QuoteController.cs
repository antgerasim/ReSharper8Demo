using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using ERPStore.Html;
using System.Web;

namespace ERPStore.Controllers
{
	/// <summary>
	/// Controller des devis
	/// </summary>
	//[HandleError(View = "500")]
	public class QuoteController : StoreController
	{
        private string m_ViewPath;

		public QuoteController(
			Services.ICatalogService catalogService
			, Services.ISalesService salesService
			, Services.IAccountService accountService
			, Services.IEmailerService emailerService
			, Services.CryptoService cryptoService
			, Services.CartService cartService
			)
		{
			this.CatalogService = catalogService;
			this.SalesService = salesService;
			this.AccountService = accountService;
			this.EmailerService = emailerService;
			this.CryptoService = cryptoService;
			this.CartService = cartService;

            this.m_ViewPath = ERPStore.Configuration.ConfigurationSettings.AppSettings["quoteVirtualPath"];
		}

		protected Services.ICatalogService CatalogService { get; set; }

		protected Services.ISalesService SalesService { get; set; }

		protected Services.IAccountService AccountService { get; set; }

		protected Services.IEmailerService EmailerService { get; set; }

		protected Services.CryptoService CryptoService { get; set; }

		protected Services.CartService CartService { get; private set; }

		[Authorize(Roles = "customer")]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Index(Models.QuoteListFilter filter, int? page)
		{
			var pageId = GetPageId(page);

			if (filter == null)
			{
				filter = new ERPStore.Models.QuoteListFilter();
				filter.StatusId = 1;
			}

			int count = 0;
			var list = SalesService.GetQuoteList(User.GetUserPrincipal().CurrentUser, filter, pageId, 10, out count);

			var model = new Models.QuoteList(list);
			model.ItemCount = count;
			model.PageIndex = pageId + 1;
			model.PageSize = 10;

			ViewData.Model = model;

			return GetDefaultView("quoteList");
		}

		[ActionFilters.TrackerActionFilter]
        public ActionResult QuoteDetail(string key)
        {
			if (key.IsNullOrTrimmedEmpty())
			{
				// return RedirectToAction("Index");
				return RedirectToERPStoreRoute(ERPStoreRoutes.QUOTE_LIST);
			}
            string code = null;
			Models.Quote quote = null;
			if (!User.GetUserPrincipal().Identity.IsAuthenticated 
				|| key.Length > 15)
			{
				CryptoService.DecryptQuoteConfirmation(key, out code);
				quote = SalesService.GetQuoteByCode(code);
				ViewData.Model = quote;
				if (quote == null)
				{
					Logger.Notification("Tentative de visualisation du devis {0} qui n'existe pas, clé : {1}", code, key);
					return Redirect("/");
				}

				if (!ViewData.ModelState.IsValid)
				{
					return GetDefaultView("quoteerror");
				}
			}
			else
			{
				var user = User.GetUserPrincipal().CurrentUser;
				quote = SalesService.GetQuoteByCode(key);
				// Il faut que le user en cours soit 
				// le destinataire du devis
				if (quote == null || quote.User.Id != user.Id)
				{
					return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
				}
				ViewData.Model = quote;
				key = CryptoService.EncryptQuoteConfirmation(quote.Code);
			}

			if (!quote.Status.IsConverted())
			{
				ViewData["key"] = key;
			}

			if (quote.Status.IsWaiting())
			{
				// Recherche de la disponibilité des produits
				var productIdList = quote.Items.Select(i => i.Product.Id).Distinct();
				var stockList = CatalogService.GetProductStockInfoList(productIdList);

				foreach (var item in stockList)
				{
					var quoteItemList = quote.Items.Where(i => i.Product.Code == item.ProductCode);
					foreach (Models.QuoteItem quoteItem in quoteItemList)
					{
						if (quoteItem.Quantity <= item.AvailableStock)
						{
							quoteItem.Disponibility = "Disponible";
						}
						else if (item.AvailableStock > 0)
						{
							quoteItem.Disponibility = string.Format("Partiel ({0}) dispo.", item.AvailableStock);
						}
						else if (item.MostProvisionningDate.HasValue)
						{
							quoteItem.Disponibility = string.Format("Approvisionné le {0:dd/MM/yyyy}", item.MostProvisionningDate);
						}
						else
						{
							quoteItem.Disponibility = item.Disponibility;
						}
					}
				}
			}

			return GetDefaultView("quotedetail");
        }

        [AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.TrackerActionFilter]
        public ActionResult Accept(string key, string condition)
        {
            if (key.IsNullOrTrimmedEmpty())
            {
                return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
            }
            string code = null;
            CryptoService.DecryptQuoteConfirmation(key, out code);
            var quote = SalesService.GetQuoteByCode(code);
            if (quote == null)
            {
                ViewData.ModelState.AddModelError("_FORM", "Ce devis n'existe pas");
				return GetDefaultView("quotedetail");
            }

			ViewData.Model = quote;
			var user = User.GetUserPrincipal().CurrentUser;

			// Est-ce que l'utilisateur courrant est deja loggé
			if (User.GetUserPrincipal().Identity.IsAuthenticated)
			{
				//if (user.Id != quote.User.Id)
				//{
				//    ViewData.ModelState.AddModelError("_FORM", "Ce devis ne peut etre converti que par son propriétaire");
				//    return View("~/views/account/quote/quotedetail.aspx");
				//}

				Models.Order order = null;
				try
				{
					using (var ts = TransactionHelper.GetNewReadCommitted())
					{
						var warnings = new List<string>();
						order = SalesService.ConvertQuoteToOrder(quote, out warnings);
						ts.Complete();
					}
				}
				catch (Exception ex)
				{
					LogError(Logger, ex);
					ViewData.ModelState.AddModelError("_FORM", "Un problème technique empeche cette action de se derouler correctement, veuiller reessayer ultérieurement");
				}

				if (!ViewData.ModelState.IsValid)
				{
					return GetDefaultView("quotedetail");
				}

				return RedirectToERPStoreRoute(ERPStoreRoutes.ORDER_DETAIL, new { orderCode = order.Code });
			}
			else
			{
				// creation du panier issu du panier de type devis
				var orderCart = SalesService.CreateOrderCartFromQuote(quote);

				// Application du vistorId 
				orderCart.VisitorId = User.GetUserPrincipal().VisitorId;

				// Nouveau panier de commande issu 
				// du panier devis
				CartService.Save(orderCart);
				CartService.ChangeCurrentCart(orderCart.Code, User.GetUserPrincipal());

				user = quote.User;

				if (user.State == ERPStore.Models.UserState.Uncompleted)
				{
					var urlHelper = new UrlHelper(this.ControllerContext.RequestContext);
					var returnUrl = urlHelper.CheckOutHref();

					// Il est redirigé vers l'inscription
					var encryptedTicket = CryptoService.EncryptCompleteAccount(user.Id);
					return RedirectToERPStoreRoute(ERPStoreRoutes.COMPLETE_ACCOUNT, new { key = encryptedTicket, returnUrl = returnUrl });
				}

				return RedirectToERPStoreRoute(ERPStoreRoutes.CHECKOUT);
			}
        }

        // [AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.TrackerActionFilter]
        public ActionResult Cancel(string key)
        {
			string code = null;
			if (key.IsNullOrTrimmedEmpty())
			{
                return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}
			if (key.Length > 25)
			{
				CryptoService.DecryptQuoteConfirmation(key, out code);
			}
			else
			{
				code = key;
			}
			var quote = SalesService.GetQuoteByCode(code);
			if (quote == null)
			{
				return Redirect("/");
			}

			ViewData.Model = quote;
			ViewData["key"] = key;
			return GetDefaultView("cancelquote");
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Cancel(string key, ERPStore.Models.CancelQuoteReason reason, string comment)
		{
			string code = key;
			if (key.IsNullOrTrimmedEmpty())
			{
                return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}
			if (key.Length > 25)
			{
				CryptoService.DecryptQuoteConfirmation(key, out code);
			}
			var quote = SalesService.GetQuoteByCode(code);

			if (quote == null)
			{
                return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			var user = User.GetUserPrincipal().CurrentUser;
			if (User.GetUserPrincipal().Identity.IsAuthenticated)
			{
				if (quote.User.Id != user.Id)
				{
					ViewData.ModelState.AddModelError("_FORM", "Le classement de ce devis n'est pas possible car vous n'êtes pas le destinataire");
				}
			}

			if (reason == ERPStore.Models.CancelQuoteReason.Other
				&& comment.IsNullOrTrimmedEmpty())
			{
				ViewData.ModelState.AddModelError("comment", "Pouvez-vous indiquer la raison du classement svp");
			}
			ViewData.Model = quote;

			if (ViewData.ModelState.IsValid)
			{
				try
				{
					SalesService.CancelQuote(quote, reason, comment);
				}
				catch(Exception ex)
				{
					LogError(Logger, ex);
					ModelState.AddModelError("_FORM", "Un problème technique empeche cette action, veuillez reessayer ultérieurement");
				}
				if (ViewData.ModelState.IsValid)
				{
					ViewData.Model = quote;
					ViewData["reason"] = reason;
					ViewData["message"] = comment;
					return GetDefaultView("canceledquote");
				}
			}

			return GetDefaultView("cancelquote");
		}

		[Authorize(Roles = "customer")]
		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.TrackerActionFilter]
		public ActionResult AddCommentToQuote(string quoteCode, string comment)
		{
			var user = User.GetUserPrincipal().CurrentUser;
			var quote = SalesService.GetQuoteByCode(quoteCode);
			if (quote == null || quote.User.Id != user.Id)
			{
                return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			if (comment.IsNullOrTrimmedEmpty())
			{
				ModelState.AddModelError("comment", "Vous devez indiquer un commentaire");
			}

			try
			{
				if (ModelState.IsValid)
				{
					SalesService.AddCommentToQuote(quote, comment);
				}
			}
			catch (Exception ex)
			{
				LogError(Logger, ex);
				ModelState.AddModelError("_Form", "pour des raisons techniques internes, cette action ne peut aboutir, veuillez rééssayer ultérieurement");
			}

			if (!ModelState.IsValid)
			{
				return Json(new Models.AddCommentOperationResultInfo
				{
					Successfull = false,
					Message = "Les informations indiquées sont incorrectes.",
					Errors = ModelState.GetAllAjaxErrors()
				});
			}

			return Json(new Models.AddCommentOperationResultInfo
			{
				Successfull = true,
				Message = "Votre commentaire vient d'etre ajouté.",
				CommentDate = string.Format("{0:dddd dd MMMM yyyy}", DateTime.Now),
				CommentMessage = comment,
			});
		}

		[Authorize(Roles = "customer")]
		[ActionFilters.TrackerActionFilter]
		public ActionResult CancelQuote(string quoteCode)
		{
			var user = User.GetUserPrincipal().CurrentUser;
			var quote = SalesService.GetQuoteByCode(quoteCode);
			if (quote == null || quote.User.Id != user.Id)
			{
                return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}
			bool result = SalesService.IsCancelableQuote(quote);
			if (result)
			{
				ViewData["IsCancelable"] = true;
			}
			ViewData.Model = quote;
			return GetDefaultView("cancelquote");
		}

		[Authorize(Roles = "customer")]
		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.TrackerActionFilter]
		public ActionResult CancelQuote(string quoteCode, ERPStore.Models.CancelQuoteReason reason, string comment)
		{
			var user = User.GetUserPrincipal().CurrentUser;
			var quote = SalesService.GetQuoteByCode(quoteCode);
			if (quote == null || quote.User.Id != user.Id)
			{
                return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			if (reason == ERPStore.Models.CancelQuoteReason.Other && comment.IsNullOrTrimmedEmpty())
			{
				ModelState.AddModelError("comment", "vous devez indiquer une raison pour l'annulation");
			}

			if (!ModelState.IsValid)
			{
				ViewData.Model = quote;
				return View();
			}

			try
			{
				using (var ts = TransactionHelper.GetNewReadCommitted())
				{
					SalesService.CancelQuote(quote, reason, comment);
					ts.Complete();
				}
			}
			catch (Exception ex)
			{
				LogError(Logger, ex);
				ModelState.AddModelError("_FORM", "pour des raisons techniques interne, cette action ne peut aboutir, veuillez rééssayer ultérieurement");
				return View();
			}

			try
			{
				EmailerService.SendQuoteCanceled(this, quote, reason, comment);
			}
			catch (Exception ex)
			{
				LogError(Logger, ex);
			}

			ViewData.Model = quote;
			return GetDefaultView("canceledquote");
		}

		#region Partial Rendering

		[Authorize(Roles = "customer")]
		public ActionResult ShowQuoteList()
		{
			return new EmptyResult();
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[Authorize(Roles = "customer")]
		public ActionResult ShowQuoteList(string viewName, Models.QuoteListFilter filter, int? size)
		{
			if (!size.HasValue)
			{
				size = 10;
			}
			int count = 0;
			var list = SalesService.GetQuoteList(User.GetUserPrincipal().CurrentUser, filter, 0, size.Value, out count);

			var model = new Models.QuoteList(list);
			model.ItemCount = count;
			model.PageIndex = 0;
			model.PageSize = size.Value;

			ViewData.Model = model;
			return PartialView(viewName);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		// [Authorize(Roles = "customer")]
		[ActionFilters.TrackerActionFilter]
		public ActionResult AcceptConfirmation(string key)
		{
			ViewData["key"] = key;
			return GetDefaultPartialView("AcceptConfirmation");
		}

		#endregion

        private ActionResult GetDefaultView(string defaultView)
        {
            if (m_ViewPath.IsNullOrEmpty())
            {
                return View();
            }

            return View(string.Format(m_ViewPath, defaultView));
        }

        private ActionResult GetDefaultPartialView(string defaultView)
        {
            if (m_ViewPath.IsNullOrEmpty())
            {
                return PartialView();
            }

            return PartialView(string.Format(m_ViewPath, defaultView));
        }

	}
}
