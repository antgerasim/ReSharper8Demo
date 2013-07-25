using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ERPStore;

namespace ERPStore.Repositories
{
	public class HttpContextCartRepository : ICartRepository
	{
		public const string ORDER_CART_COOKIE_NAME = "cartId";
		public const string QUOTE_CART_COOKIE_NAME = "quoteCartId";

		public HttpContextCartRepository(System.Web.HttpContext ctx
			, Services.ICacheService cacheService
			, Logging.ILogger logger)
		{
			HttpContext = ctx;
			CacheService = cacheService;
			this.Logger = logger;
		}

		protected System.Web.HttpContext HttpContext { get; set; }

		protected Services.ICacheService CacheService { get; set; }

		protected Logging.ILogger Logger { get; set; }

		#region ICartRepository Members

		public string GetCurrentCartId(Models.CartType cartType, Models.UserPrincipal user)
		{
			switch (cartType)
			{
				case ERPStore.Models.CartType.Order:
					return GetCartId(ORDER_CART_COOKIE_NAME);
				case ERPStore.Models.CartType.Quote:
					return GetCartId(QUOTE_CART_COOKIE_NAME);
			}
			return null;
		}

		private string GetCartId(string cookieName)
		{
			var cookie = HttpContext.Request.Cookies[cookieName];
			if (cookie == null)
			{
				cookie = new System.Web.HttpCookie(cookieName);
				cookie.Expires = DateTime.Now.AddDays(30);
				cookie.Path = "/";
				cookie.Value = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
				try
				{
					HttpContext.Response.Cookies.Add(cookie);
				}
				catch(Exception ex)
				{
					Logger.Warn(ex.ToString());
				}
			}
			return cookie.Value;
		}

		public void ChangeCurrent(string cartId, Models.CartType cartType, Models.UserPrincipal user)
		{
			var cookieName = (cartType == ERPStore.Models.CartType.Order) ? ORDER_CART_COOKIE_NAME : QUOTE_CART_COOKIE_NAME;
			var cookie = new System.Web.HttpCookie(cookieName);
			cookie.Expires = DateTime.Now.AddDays(30);
			cookie.Path = "/";
			cookie.Value = cartId;
			HttpContext.Response.Cookies.Add(cookie);
		}

		public void Remove(string cartId)
		{
			var key = GetKey(cartId);
			HttpContext.Response.Cookies.Remove(ORDER_CART_COOKIE_NAME);
		}

		public void Remove(Models.CartBase cart)
		{
			var key = GetKey(cart.Code);
			CacheService.Remove(key);
		}

		public Models.CartBase GetActiveCartById(string cartId)
		{
			var key = GetKey(cartId);
			return CacheService[key] as Models.CartBase;
		}

		public Models.CartBase GetCartById(string cartId)
		{
			var key = GetKey(cartId);
			return CacheService[key] as Models.CartBase;
		}

		public void Save(Models.CartBase cart)
		{
			var key = GetKey(cart.Code);
			CacheService.Add(key, cart, DateTime.Now.AddDays(1));
		}

		public IQueryable<Models.CartBase> GetList(Models.UserPrincipal user)
		{
			return CacheService.GetListOf<Models.CartBase>().Where(i => (i.CustomerId.HasValue && i.CustomerId.Value == user.CurrentUser.Id) 
				|| i.VisitorId.Equals(user.VisitorId, StringComparison.InvariantCultureIgnoreCase));
		}

		public IList<Models.OrderCart> GetOrderList(Models.UserPrincipal user)
		{
			return (from cart in GetList(user)
				   where cart is Models.OrderCart
				   select cart).Cast<Models.OrderCart>().ToList();
		}

		public IList<Models.QuoteCart> GetQuoteList(Models.UserPrincipal user)
		{
			return (from cart in GetList(user)
					where cart is Models.QuoteCart
					select cart).Cast<Models.QuoteCart>().ToList();
		}

        public Models.CartBase GetQuoteCartByConvertedEntityId(int entityId)
        {
            return null;
        }

        public void ChangeType(Models.CartBase cart, Models.CartType type)
        {

        }

		public string GetCurrentCartIdByVisitorId(ERPStore.Models.CartType cartType, string visitorId)
		{
			throw new NotImplementedException();
		}

		public List<ERPStore.Models.CartBase> GetActiveCartListByVisitorId(string visitorId)
		{
			throw new NotImplementedException();
		}

		public void ChangeCurrent(string cartId, string visitorId)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.CartItem> GetLastCartItem(int itemCount)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.OrderCart> GetAllOrderCartList(int index, int pageSize)
		{
			return null;
		}

		public Models.OrderCart GetOrderCartById(string cartId)
		{
			return null;
		}

		#endregion

		private string GetKey(string cartId)
		{
			return string.Format("cart:{0}", cartId);
		}

	}
}
