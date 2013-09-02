using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using Microsoft.Practices.Unity;

namespace ERPStore.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	// [HandleError(View = "500")]
	public class OrderController : StoreController
    {
		public OrderController(Services.ISalesService salesService
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

        //[Authorize(Roles="customer")]
        //[ActionFilters.TrackerActionFilter]
        //public ActionResult Index()
        //{
        //    return GetDefaultView("index");
        //}

        [Authorize(Roles = "customer")]
        [ActionFilters.TrackerActionFilter]
        public ActionResult Index(Models.OrderListFilter filter, int? page, int? size)
        {
            var user = User.GetUserPrincipal().CurrentUser;
            // var filterList = SalesService.GetPeriodFilterList();
            int pageId = GetPageId(page);

            int count = 0;
            var orderList = SalesService.GetOrderList(user, filter, pageId, size.GetValueOrDefault(10), out count);
            var result = new Models.OrderList(orderList);
            result.PageIndex = pageId + 1;
            result.ItemCount = count;
            result.PageSize = size.GetValueOrDefault(10);
            ViewData.Model = result;
            return View();
        }

        public class Abc
        {
            
        }


		[Authorize(Roles = "customer")]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult EditAddress(int index)
		{
			var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());
			var user = (User as Models.UserPrincipal).CurrentUser;
			if (index == -1)
			{
				ViewData.Model = cart.BillingAddress;
			}
			else
			{
			    ViewData.Model = user.DeliveryAddressList[index];
			}
		    return View();
		}

		// [Authorize(Roles = "customer")]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult OrderDetail(string orderCode)
		{
			Models.Order order = null;
			if (orderCode.IsNullOrTrimmedEmpty())
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}
			if (!User.Identity.IsAuthenticated 
				&& orderCode.Length < 20)
			{
                return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}
			if (orderCode.Length > 20)
			{
				string code = null;
				DateTime expirationDate = DateTime.MaxValue;
				bool needNotification = false;
				CryptoService.DecryptOrderConfirmation(orderCode, out code, out expirationDate, out needNotification);

				// TODO : Verification de l'expiration et envoi du message de notification

				orderCode = code;
			}

			order = SalesService.GetOrderByCode(orderCode);
			if (order == null)
			{
                return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			if (User.Identity.IsAuthenticated)
			{
				var user = User.GetUserPrincipal().CurrentUser;
				// verifier que le user est bien propriétaire de la commande
				if (!order.User.IsMaster && order.User.Id != user.Id)
				{
                    return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
				}
			}
			ViewData.Model = order;
			return View();
		}

		[Authorize(Roles = "customer")]
		[ActionFilters.TrackerActionFilter]
		public ActionResult EditOrder(string orderCode)
		{
			var user = User.GetUserPrincipal().CurrentUser;
			var order = SalesService.GetOrderByCode(orderCode);
			if (order == null)
			{
				return Redirect("/");
			}
			ViewData.Model = order;
			return View();
		}

		[Authorize(Roles = "customer")]
		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.TrackerActionFilter]
		public ActionResult EditOrder(string orderCode, string message)
		{
			var user = User.GetUserPrincipal().CurrentUser;
			var order = SalesService.GetOrderByCode(orderCode);
			if (order == null)
			{
                return RedirectToERPStoreRoute(ERPStoreRoutes.HOME); 
			}
			ViewData.Model = order;

			if (message.IsNullOrTrimmedEmpty())
			{
				ModelState.AddModelError("message", "Vous devez indiquer ce que vous voulez modifier dans la commande");
			}

			try
			{
				if (ModelState.IsValid)
				{
					EmailerService.SendOrderModificationRequest(order, message);
					EmailerService.SendOrderModification(this, order, message);
					ViewData["MessageSent"] = true;
				}
			}
			catch(Exception ex)
			{
				LogError(Logger, ex);
				ModelState.AddModelError("_FORM", "pour des raisons techniques, cette action ne peut aboutir, veuillez rééssayer ultérieurement");
			}

			return View();
		}

		[Authorize(Roles = "customer")]
		[ActionFilters.TrackerActionFilter]
		public ActionResult AddCommentToOrder(string orderCode)
		{
			var user = User.GetUserPrincipal().CurrentUser;
			var order = SalesService.GetOrderByCode(orderCode);
			if (order == null || order.User.Id != user.Id)
			{
				return Redirect("/");
			}

			ViewData.Model = order;
			return View();
		}

		[Authorize(Roles = "customer")]
		[AcceptVerbs( HttpVerbs.Post)]
		[ActionFilters.TrackerActionFilter]
		public ActionResult AddCommentToOrder(string orderCode, string comment)
		{
			var user = User.GetUserPrincipal().CurrentUser;
			var order = SalesService.GetOrderByCode(orderCode);
			if (order == null || order.User.Id != user.Id)
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
					SalesService.AddCommentToOrder(order, comment);
				}
			}
			catch(Exception ex)
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

		#region Partial Views

        [Authorize(Roles = "customer")]
        [ActionFilters.TrackerActionFilter]
        public ActionResult ShowLastOrder(string lastOrderViewName, string waitingViewName)
        {
            lastOrderViewName = lastOrderViewName ?? "_lastorder";
            waitingViewName = waitingViewName ?? "_waitingorder";

            var user = User.GetUserPrincipal().CurrentUser;

            var lastOrder = SalesService.GetLastOrder(user);
            if (lastOrder == null)
            {
                return PartialView(waitingViewName, lastOrder);
            }
            ViewData.Model = lastOrder;
            return PartialView(lastOrderViewName);
        }

		public ActionResult ShowOrderDocuments(ERPStore.Models.Order order, string viewName)
		{
			var list = DocumentService.GetMediaList(order);
			ViewData.Model = list;
			return PartialView(viewName);
		}

		// Search engine only
		[Authorize(Roles = "customer")]
		public ActionResult ShowOrderList()
		{
			return new EmptyResult();
		}

		[Authorize(Roles = "customer")]
		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.ZeroCacheActionFilter]
		public ActionResult ShowOrderList(string viewName, Models.OrderListFilter filter, int? page, int? size)
		{
			var filterList = SalesService.GetPeriodFilterList();
			var user = User.GetUserPrincipal().CurrentUser;
			var pageId = GetPageId(page);

			int count = 0;
			if (!size.HasValue)
			{
				size = size.GetValueOrDefault(ERPStoreApplication.WebSiteSettings.Catalog.PageSize);
			}
			var orderList = SalesService.GetOrderList(user, filter, pageId, size.Value, out count);

			var result = new Models.OrderList(orderList);
			result.PageIndex = pageId + 1;
			result.ItemCount = count;
			result.PageSize = size.Value;
			result.Name = string.Empty;

			ViewData.Model = result;
			return PartialView(viewName);
		}

		#endregion

	}
}
