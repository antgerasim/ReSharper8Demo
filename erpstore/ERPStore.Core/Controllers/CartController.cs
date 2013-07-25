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
	// [HandleError(View = "500")]
	public class CartController : StoreController
    {
		public CartController(
			Services.ICacheService cacheService
			,Services.ICartService cartService
			,Services.ICatalogService catalogService
			,Services.ISalesService salesService
			,Services.IAccountService accountService
			,Services.IIncentiveService incentiveService
			)
		{
			this.CartService = cartService;
			this.CatalogService = catalogService;
			this.SalesService = salesService;
			this.AccountService = accountService;
			this.IncentiveService = incentiveService;
			this.CacheService = cacheService;
		}

		#region Properties

		protected Services.ICartService CartService { get; private set; }

		protected Services.ICatalogService CatalogService { get; private set; }

		protected Services.ISalesService SalesService { get; private set; }

		protected Services.IAccountService AccountService { get; private set; }

		protected Services.IIncentiveService IncentiveService { get; private set; }

		// Pb de résolution avec Unity (ne sert pas)
		protected Services.ICacheService CacheService { get; private set; }

		#endregion

		[ActionFilters.TrackerActionFilter]
        public ActionResult Index()
        {
			var cart = CartService.GetOrCreateOrderCart(User.GetUserPrincipal());
			ViewData.Model = cart;

			SalesService.ProcessExport(cart, User.GetUserPrincipal().CurrentUser);
			CartService.ApplyProductStockInfoList(cart);

			if (cart.ItemCount == 0)
			{
				return View("EmptyCart");
			}

			return View();
        }

		// [AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.TrackerActionFilter]
		public ActionResult AddCartItem(string productCode)
		{
			return AddItemWithQuantity(productCode, 1);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.TrackerActionFilter]
		public ActionResult AddItemWithQuantity(string productCode, int quantity)
		{
			var product = CatalogService.GetProductByCode(productCode);
			if (product == null
				|| product.SaleMode != ERPStore.Models.ProductSaleMode.Sellable)
			{
				return RedirectToERPStoreRoute("Default");
			}
			return AddItem(product, quantity);
		}

		private ActionResult AddItem(Models.Product product, int quantity)
		{
			var cart = AddToCart(product, quantity);
			ViewData.Model = cart;
			SalesService.ProcessExport(cart, User.GetUserPrincipal().CurrentUser);
			CartService.ApplyProductStockInfoList(cart);

			return View("Index");
		}

		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult JsAddItem(string productCode)
		{
			var product = CatalogService.GetProductByCode(productCode);
			var quantity = 1;
			if (product != null)
			{
				quantity = product.Packaging.Value;
			}
			return JsAddToCart(product, quantity);
		}

		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult JsAddItemWithQuantity(string productCode, int quantity)
		{
			var product = CatalogService.GetProductByCode(productCode);
			return JsAddToCart(product, quantity);
		}

		private ActionResult JsAddToCart(Models.Product product, int quantity)
		{
			if (product == null
				|| product.SaleMode != ERPStore.Models.ProductSaleMode.Sellable)
			{
				return new JsonResult();
			}

			var cart = AddToCart(product, quantity);
			
			var urlHelper = new UrlHelper(this.ControllerContext.RequestContext);
			SalesService.ProcessExport(cart, User.GetUserPrincipal().CurrentUser);

			var data = new 
			{
				status = cart.GetStatusText(),
				cartGrandTotal = cart.GrandTotal.ToString("#,#0.00"),
				cartGrandTaxTotal = cart.GrandTaxTotal.ToString("#,#0.00"),
				cartGrandTotalWithTax = cart.GrandTotalWithTax.ToString("#,#0.00"),
				title = product.Title,
				quantity = quantity,
				cartUrl = urlHelper.CartHref(),
				productImage = product.DefaultImage != null ? HttpUtility.UrlEncode(product.DefaultImage.Url) : string.Empty,
			};

			var result = Json(data);

			return result;
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult Clear()
		{
			var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());
			using (var ts = TransactionHelper.GetNewReadCommitted())
			{
				CartService.Clear(cart);
				CartService.Save(cart);
				ts.Complete();
			}
			ViewData.Model = cart;
			return View("EmptyCart");
		}

		/// <summary>
		/// Pour les moteurs de recherche
		/// </summary>
		/// <returns></returns>
		public ActionResult Recalc()
		{
			return RedirectToERPStoreRoute(ERPStoreRoutes.CART);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Recalc(FormCollection form)
		{
			var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());
			if (cart == null) // Cas d'un partage
			{
				if (Request.UrlReferrer != null)
				{
					return Redirect(Request.UrlReferrer.PathAndQuery);
				}
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			if (form == null || form["quantity"] == null)
			{
				ViewData.Model = cart;
				return View("Index");
			}
			var values = form["quantity"].Split(',');

			var productList = cart.Items.Select(i => i.Product).ToList();
			CatalogService.ApplyBestPrice(productList, User.GetUserPrincipal().CurrentUser);

			for (int i = 0; i < cart.ItemCount; i++)
			{
                if (i + 1 > values.Count())
                {
                    continue;
                }
				var quantity = values[i];
				var cartItem = cart.Items[i];
				cartItem.Product = productList.Single(p => p.Id == cartItem.Product.Id);
				CartService.RecalcCartItem(cartItem, GetQuantity(cartItem.Product, quantity));
			}

			var couponCode = form["couponcode"];
			if (!couponCode.IsNullOrTrimmedEmpty())
			{
				var brokenRules = IncentiveService.ValidateUse(cart, couponCode);
				if (brokenRules.IsNotNullOrEmpty())
				{
					foreach (var item in brokenRules)
					{
						foreach (var error in item.ErrorList)
						{
							ViewData.ModelState.AddModelError(item.PropertyName, error);
						}
					}
				}
			}
			ViewData.Model = cart;

			if (!ViewData.ModelState.IsValid)
			{
				return View("Index");
			}

			SalesService.CalculateShippingFee(cart, User.GetUserPrincipal().CurrentUser);
			IncentiveService.ProcessCoupon(cart, couponCode);

			using (var ts = TransactionHelper.GetNewReadCommitted())
			{
				CartService.Save(cart);
				ts.Complete();
			}

			SalesService.ProcessExport(cart, User.GetUserPrincipal().CurrentUser);
			CartService.ApplyProductStockInfoList(cart);

			return View("Index");
		}

		[AcceptVerbs(HttpVerbs.Get)]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Remove(int index)
		{
			var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());
			if (cart == null)
			{
				if (Request.UrlReferrer != null)
				{
					return Redirect(Request.UrlReferrer.PathAndQuery);
				}
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			CartService.RemoveItem(cart,index);
			SalesService.CalculateShippingFee(cart, User.GetUserPrincipal().CurrentUser);

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
			SalesService.ProcessExport(cart, User.GetUserPrincipal().CurrentUser);
			CartService.ApplyProductStockInfoList(cart);

			return View("Index");
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult Delete(string cartId)
		{
			CartService.DeleteCart(cartId, User.GetUserPrincipal());
			// return RedirectToAction("Index");
			return RedirectToERPStoreRoute(ERPStoreRoutes.CART);
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult Change(string cartId)
		{
			CartService.ChangeCurrentCart(cartId, User.GetUserPrincipal());
			// return RedirectToAction("Index");
			return RedirectToERPStoreRoute(ERPStoreRoutes.CART);
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
			CartService.ApplyProductStockInfoList(cart as Models.OrderCart);
			return View("Index");
		}


		[ActionFilters.TrackerActionFilter]
		public ActionResult Assign(string cartId)
		{
			var cart = CartService.GetOrderCartById(cartId);
			if (cart == null
				|| cart.ItemCount == 0)
			{
				return View("EmptyCart");
			}

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
			}

			CartService.Save(cart);
			CartService.ChangeCurrentCart(cart.Code, userId);

			ViewData.Model = cart;
			CartService.ApplyProductStockInfoList(cart as Models.OrderCart);
			return View("Index");
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult Create()
		{
			var cart = CartService.CreateOrderCart(User.GetUserPrincipal());
			CartService.AddCart(cart);
			CartService.ChangeCurrentCart(cart.Code, User.GetUserPrincipal());
			return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
		}

        [Obsolete("use js instead", true)]
		public ActionResult Script()
		{
			Response.Cache.SetCacheability(HttpCacheability.Public);
			Response.Cache.SetExpires(DateTime.Now.AddDays(1));
			Response.ContentType = "text/javascript";
			return View("script");
		}

		public ActionResult ConvertToQuoteCart(string cartId)
		{
			if (cartId.IsNullOrTrimmedEmpty())
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			var orderCart = CartService.GetCartById(cartId) as Models.OrderCart;
			if (orderCart == null)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			var quoteCart = CartService.ConvertToQuoteCart(orderCart, User.GetUserPrincipal());
			CartService.Save(quoteCart);
			CartService.ChangeCurrentCart(quoteCart.Code, User.GetUserPrincipal());

			// return RedirectToAction("Index");
			return RedirectToERPStoreRoute(ERPStoreRoutes.CART);
		}

        #region Partial Rendering

        public ActionResult ShowStatus(string viewName)
		{
			var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());
			ViewData.Model = cart;
			viewName = viewName ?? "status";
			return PartialView(viewName);
		}

		public ActionResult ShowCurrentCartList(string viewName)
		{
			var list = CartService.GetCurrentOrderList(User.GetUserPrincipal());
			ViewData.Model = list;
			viewName = viewName ?? "cartlist";
			return PartialView(viewName);
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

			var qty = GetQuantity(product, quantity.GetValueOrDefault(1));
			var cart = AddToCart(product, qty);

			// Application du tarif client
			// var list = new List<Models.Product>() { product };
			// CatalogService.ApplyBestPrice(list, User.GetUserPrincipal().CurrentUser);
			var urlHelper = new UrlHelper(this.ControllerContext.RequestContext);
			bool isCustomerPriceApplied = false;
			var price = SalesService.GetProductSalePrice(product, User.GetUserPrincipal().CurrentUser, qty, out isCustomerPriceApplied);
			//var cart = CartService.GetCurrentOrderCart(User.GetUserPrincipal());

			ViewData.Model = new Models.CartItemNeeded()
			{
				Product = product,
				CartUrl = urlHelper.CartHref(),
				Quantity = qty,
				Price = price,
				Cart = cart,
			};
			viewName = viewName ?? "_addtocart";
			return PartialView(viewName);
		}

		//  Pour les moteurs de recherche
		public ActionResult ShowLastCartItemList()
		{
		    return new EmptyResult();
		}

		[HttpPost]
		public ActionResult ShowLastCartItemList(int itemCount, string viewName)
		{
			var list = CartService.GetLastCartItem(itemCount);

			ViewData.Model = list;

			return PartialView(viewName);
		}

		public ActionResult ShowCommentByCart(Models.OrderCart cart, string viewName)
		{
			var commentList = CartService.GetCommentListByCart(cart);
			if (commentList != null)
			{
				foreach (var item in commentList)
				{
					item.Message = item.Message.Replace("\r", "<br/>");
				}
			}
			ViewData.Model = commentList;

			return PartialView(viewName);
		}

		[HttpPost]
		public ActionResult ShowCrossSellingByCart(string cartId, string viewName)
		{
			var cart = CartService.GetCartById(cartId) as Models.OrderCart;
			if (cart == null)
			{
				return new EmptyResult();
			}

			var productList = CatalogService.GetCrossSellingList(cart);
			if (productList == null)
			{
				productList = new List<Models.Product>();
			}
			ViewData.Model = productList;
			return PartialView(viewName);
		}

		#endregion

		#region Private

		private Models.OrderCart AddToCart(Models.Product product, int quantity)
		{
			var list = new List<Models.Product>() { product };

			CatalogService.ApplyBestPrice(list, User.GetUserPrincipal().CurrentUser);

			quantity = GetQuantity(product, quantity);
			// bool isCustomerPriceApplied = false;
			// var price = SalesService.GetProductSalePrice(product, User.GetUserPrincipal().CurrentUser, quantity, out isCustomerPriceApplied);
			var cart = CartService.GetOrCreateOrderCart(User.GetUserPrincipal());

			CartService.AddItem(cart, product, quantity, product.BestPrice, (product.SelectedPrice == ERPStore.Models.PriceType.Customer));
			SalesService.CalculateShippingFee(cart, User.GetUserPrincipal().CurrentUser);

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
				CartService.Save(cart);
				ts.Complete();
			}

			return cart;
		}

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
