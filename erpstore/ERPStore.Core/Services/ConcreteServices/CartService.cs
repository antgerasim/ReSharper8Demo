using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web.Mvc;

using Microsoft.Practices.Unity;

using ERPStore.Models;
using ERPStore.Html;

namespace ERPStore.Services
{
	/// <summary>
	/// Service de gestion des paniers
	/// </summary>
	public class CartService : ICartService 
	{
		private SynchronizedCollection<CartItem> m_LastCartItems;

		public CartService(Logging.ILogger logger
			, Repositories.ICartRepository cartRepository
            , Services.ICatalogService catalogService
			, Services.IAccountService accountService
			, Services.IEventPublisher eventPublisherService
			, Repositories.ICommentRepository commentRepository
			)
		{
			m_LastCartItems = new System.Collections.Generic.SynchronizedCollection<CartItem>();

			this.Logger = logger;
			this.CartRepository = cartRepository;
            this.CatalogService = catalogService;
			this.AccountService = accountService;
			this.EventPublisherService = eventPublisherService;
			this.CommentRepository = commentRepository;
		}

		protected Logging.ILogger Logger { get; private set; }

		protected Repositories.ICartRepository CartRepository { get; private set; }

        protected Services.ICatalogService CatalogService { get; private set; }

		protected Services.IAccountService AccountService { get; private set; }

		protected Services.IEventPublisher EventPublisherService { get; private set; }

		protected Repositories.ICommentRepository CommentRepository { get; private set; }

		public virtual OrderCart GetOrCreateOrderCart(Models.UserPrincipal user)
		{
			if (user.VisitorId.IsNullOrTrimmedEmpty())
			{
				throw new ArgumentException("Visitor does not be null or empty");
			}
			var cartId = CartRepository.GetCurrentCartId(Models.CartType.Order, user);
			var cart = CartRepository.GetActiveCartById(cartId) as Models.OrderCart;
			if (cart != null)
			{
				return cart;
			}
			// Recherche dans le cache sur le visitorId
			cart = CartRepository.GetOrderList(user).FirstOrDefault();
			if (cart != null)
			{
				return cart;
			}
			cart = CreateOrderCart(user);
			// cart.Code = cartId;
			return cart;
		}

		public virtual QuoteCart GetOrCreateQuoteCart(Models.UserPrincipal user)
		{
			if (user.VisitorId.IsNullOrTrimmedEmpty())
			{
				throw new ArgumentException("Visitor does not be null or empty");
			}
			var cartId = CartRepository.GetCurrentCartId(Models.CartType.Quote, user);
			var cart = CartRepository.GetActiveCartById(cartId) as Models.QuoteCart;
			if (cart != null)
			{
				return cart;
			}
			// Recherche dans le cache sur le visitorId
			cart = CartRepository.GetQuoteList(user).FirstOrDefault();
			if (cart != null)
			{
				return cart;
			}
			cart = CreateQuoteCart(user);
			// cart.Code = cartId;
			return cart;
		}

		public virtual OrderCart GetCurrentOrderCart(Models.UserPrincipal user)
		{
			var cartId = CartRepository.GetCurrentCartId(Models.CartType.Order, user);
			var cart = GetActiveCartById(cartId);
			return cart as OrderCart;
		}

		public virtual QuoteCart GetCurrentQuoteCart(Models.UserPrincipal user)
		{
			var cartId = CartRepository.GetCurrentCartId(Models.CartType.Quote, user);
			var cart = GetActiveCartById(cartId);
			return cart as QuoteCart;
		}

		public virtual OrderCart CreateOrderCart(Models.UserPrincipal user)
		{
			if (user.VisitorId.IsNullOrTrimmedEmpty())
			{
				throw new ArgumentException("Visitor does not be null or empty");
			}

			var cart = new OrderCart();
			cart.VisitorId = user.VisitorId;
			cart.Conveyor = ERPStoreApplication.WebSiteSettings.Shipping.DefaultConveyor;
			cart.AllowPartialDelivery = ERPStoreApplication.WebSiteSettings.Shipping.AllowPartialDelivery;
			cart.DiscountTotal = 0;
			cart.DiscountTotalTaxRate = ERPStoreApplication.WebSiteSettings.Payment.DefaultTaxRate;
			cart.CreationDate = DateTime.Now;
			cart.ShippingFeeLocked = false;

			EventPublisherService.Publish(new Models.Events.OrderCartCreatedEvent()
			{
				Cart = cart,
				UserPrincipal = user,
			});

			return cart;
		}

		public virtual OrderCart CreateOrderCart(Models.OrderCart cart, string visitorId)
		{
			var c = new OrderCart();
			c.AcceptCondition = false;
			c.AcceptConversion = false;
			c.AllowPartialDelivery = cart.AllowPartialDelivery;
			c.BillingAddress = null;
			c.CreationDate = DateTime.Now;
			c.DiscountTotal = cart.DiscountTotal;
			c.Message = cart.Message;
			c.ShippingFee = cart.ShippingFee;
			c.VisitorId = visitorId;
			c.ShippingFeeLocked = cart.ShippingFeeLocked;

			foreach (var item in cart.Items)
			{
				var i = new CartItem();
				i.Packaging = item.Packaging;
				i.Product = item.Product;
				i.Quantity = item.Quantity;
				i.RecyclePrice = item.RecyclePrice;
				i.SalePrice = item.SalePrice;
				i.SaleUnitValue = item.SaleUnitValue;
				i.CreationDate = DateTime.Now;
				i.LastUpdate = DateTime.Now;
				i.Discount = item.Discount;
				i.CatalogPrice = item.CatalogPrice;
				i.AllowPartialDelivery = item.AllowPartialDelivery;
				i.IsLocked = item.IsLocked;
				i.PriceType = item.PriceType;
				i.ProductStockInfo = item.ProductStockInfo;
				c.Items.Add(i);
			}
			return c;
		}

		public virtual QuoteCart CreateQuoteCart(Models.UserPrincipal user)
		{
			if (user.VisitorId.IsNullOrTrimmedEmpty())
			{
				throw new ArgumentException("Visitor does not be null or empty");
			}

			var cart = new QuoteCart();
			cart.VisitorId = user.VisitorId;
			return cart;
		}

		public virtual OrderCart CreateAndSaveOrderCart(Models.UserPrincipal user)
		{
			var cart = CreateOrderCart(user);
			CartRepository.Save(cart);
			return cart;
		}

		public virtual QuoteCart CreateAndSaveQuoteCart(Models.UserPrincipal user)
		{
			var cart = CreateQuoteCart(user);
			CartRepository.Save(cart);
			return cart;
		}

		public virtual CartBase GetActiveCartById(string id)
		{
			return CartRepository.GetActiveCartById(id);
		}

		public virtual CartBase GetCartById(string id)
		{
			return CartRepository.GetCartById(id);
		}

		public virtual OrderCart GetOrderCartById(string id)
		{
			return CartRepository.GetOrderCartById(id);
		}

		public virtual IList<CartBase> GetCurrentList(Models.UserPrincipal user)
		{
			if (user.VisitorId.IsNullOrTrimmedEmpty())
			{
				throw new ArgumentException("Visitor does not be null or empty");
			}

			var list = CartRepository.GetList(user);
			return list.ToList();
		}

		public virtual IList<OrderCart> GetCurrentOrderList(Models.UserPrincipal user)
		{
			if (user.VisitorId.IsNullOrTrimmedEmpty())
			{
				throw new ArgumentException("Visitor does not be null or empty");
			}

			var list = CartRepository.GetOrderList(user);
			return list.ToList();
		}

		public virtual IList<QuoteCart> GetCurrentQuoteList(Models.UserPrincipal user)
		{
			if (user.VisitorId.IsNullOrTrimmedEmpty())
			{
				throw new ArgumentException("Visitor does not be null or empty");
			}

			var list = CartRepository.GetQuoteList(user);
			return list.ToList();
		}

		public virtual void RemoveCart(CartBase cart)
		{
			Logger.Info("Delete ordercart {0}", cart.Code);
			CartRepository.Remove(cart);
		}

		public virtual void AddItem(CartBase cart, Product product, int quantity, Price salePrice, bool isCustomerPriceApplied)
		{
			// TODO : Placer ces règles d'ajout dans SalesService
			// checker la possibilité d'ajout
			if (product.SaleMode == ProductSaleMode.NotSellable)
			{
				// TODO : Convert to throw exception
				Logger.Warn("Add not sellable product {0} in order cart", product.Code);
				return;
			}
			quantity = Math.Max(1, quantity);
			var existing = cart.Items.SingleOrDefault(i => i.Product.Id == product.Id);
			salePrice = GetPriceByQuantity(product, quantity, salePrice);

			if (product.SaleMode == ProductSaleMode.EndOfLife)
			{
				var productStockInfo = CatalogService.GetProductStockInfo(product);
				if (productStockInfo == null)
				{
					// TODO : Convert to throw exception
					Logger.Warn("Add end of life product {0} in order cart", product.Code);
					return;
				}
				var q = quantity;
				if (existing != null)
				{
					q = existing.Quantity + quantity;
				}
				if (productStockInfo.AvailableStock < q)
				{
					// TODO : Convert to throw exception
					Logger.Warn("Add end of life product {0} in order cart , quantity {1}", product.Code, quantity);
					return;
				}
			}

			if (existing == null)
			{
				Logger.Info("Add product {0} quantity {1} cart {2}", product.Code, quantity, cart.Code);

				var cartItem = CreateCartItem();
				cartItem.Product = product;
				cartItem.Quantity = quantity;
				cartItem.SalePrice = salePrice;
				cartItem.SaleUnitValue = product.SaleUnitValue;
				cartItem.Packaging = product.Packaging.Value;
				cartItem.RecyclePrice = product.RecyclePrice;
				cartItem.IsCustomerPriceApplied = isCustomerPriceApplied;

				cart.Items.Add(cartItem);
				AddLastCartItem(cartItem);
			}
			else if (!existing.IsLocked)
			{
				Logger.Info("Update cartitem product {0} quantity {1} cart {2}", product.Code, quantity, cart.Code);
				existing.Quantity += quantity;
			}
		}

		public virtual void RecalcCartItem(CartItem item, int quantity)
		{
			if (item.IsLocked)
			{
				return;
			}
			item.Quantity = quantity;
			item.SalePrice = GetPriceByQuantity(item.Product, quantity, item.SalePrice);
		}
			
		public virtual void RemoveItem(CartBase cart, int index)
		{
			if (cart == null)
			{
				return;
			}
			if (index >= cart.ItemCount)
			{
				return;
			}
			var item = cart.Items[index];
			Logger.Info("Delete cartitem product {0} cart {1}", item.Product.Code, cart.Code);
			cart.Items.Remove(item);
		}

		public virtual void Clear(CartBase cart)
		{
			if (cart == null)
			{
				return;
			}
			Logger.Info("Empty cart {0}", cart.Code);
			cart.Items.Clear();
		}

		public virtual void Save(CartBase cart)
		{
			if (cart == null)
			{
				return;
			}
			CartRepository.Save(cart);
		}

		public virtual void ChangeCurrentCart(string cartId, Models.UserPrincipal user)
		{
			var cart = CartRepository.GetActiveCartById(cartId);
			if (cart is OrderCart)
			{
				CartRepository.ChangeCurrent(cartId, CartType.Order, user);
			}
			else
			{
				CartRepository.ChangeCurrent(cartId, CartType.Quote, user);
			}
		}

		public virtual void ChangeCurrentCart(string cartId, string visitorId)
		{
			CartRepository.ChangeCurrent(cartId, visitorId);
		}

		public virtual void DeleteCart(string cartId, Models.UserPrincipal user)
		{
			var cart = CartRepository.GetActiveCartById(cartId);

			if (cart == null)
			{
				return;
			}

			if (cart is Models.OrderCart)
			{
				var currentOrderCart = GetCurrentOrderCart(user);
				if (currentOrderCart != null 
					&& currentOrderCart.Code.Equals(cartId, StringComparison.InvariantCultureIgnoreCase))
				{
					CartRepository.Remove(cartId);
				}
				else
				{
					var list = GetCurrentOrderList(user);
					if (list.IsNotNullOrEmpty())
					{
						ChangeCurrentCart(list.First().Code, user);
					}
				}
			}
			else if (cart is Models.QuoteCart)
			{
				var currentQuoteCart = GetCurrentQuoteCart(user);
				if (currentQuoteCart != null && currentQuoteCart.Code.Equals(cartId, StringComparison.InvariantCultureIgnoreCase))
				{
					CartRepository.Remove(cartId);
				}
				else
				{
					var list = GetCurrentQuoteList(user);
					if (list.IsNotNullOrEmpty())
					{
						ChangeCurrentCart(list.First().Code, user);
					}
				}
			}			
			CartRepository.Remove(cartId);
		}

		public virtual void AddCart(Models.CartBase cart)
		{
			CartRepository.Save(cart);
		}


        public OrderCart GetOrderCartByConvertedEntityId(int entityId, Models.UserPrincipal user)
        {
            var cartList = CartRepository.GetOrderList(user);
            return cartList.SingleOrDefault(i => i.ConvertedEntityId == entityId);
        }

		public QuoteCart GetQuoteCartByConvertedEntityId(int entityId, Models.UserPrincipal user)
		{
			var cartList = CartRepository.GetQuoteList(user);
			return cartList.SingleOrDefault(i => i.ConvertedEntityId == entityId);
		}

		public void SetCurrentCart(int userId, string visitorId)
		{
			var currentCart = CartRepository.GetCurrentCartIdByVisitorId(CartType.Order, visitorId);
			if (currentCart == null)
			{
				return;
			}

			// Recherche des paniers en cours
			var cartList = CartRepository.GetActiveCartListByVisitorId(visitorId);

			var user = AccountService.GetUserById(userId);

			// Affectation du user
			// et mise à jour des prix
			foreach (var cart in cartList)
			{
				var cartType = (cart is OrderCart) ? CartType.Order : CartType.Quote;
				if (cartType != CartType.Quote)
				{
					continue;
				}

				cart.CustomerId = userId;

				var productList = cart.Items.Select(i => i.Product).Distinct().ToList();
				CatalogService.ApplyBestPrice(productList, user);

				foreach (var cartItem in cart.Items)
				{
					var product = productList.Single(i => i.Id == cartItem.Product.Id);
					cartItem.SalePrice = product.BestPrice;
					cartItem.IsCustomerPriceApplied = product.SelectedPrice == PriceType.Customer;
				}

				CartRepository.Save(cart);

				if (cart.Code == currentCart)
				{
					CartRepository.ChangeCurrent(cart.Code, visitorId);
				}
			}

		}

		public Models.QuoteCart ConvertToQuoteCart(Models.OrderCart orderCart, Models.UserPrincipal principal)
		{
			var quoteCart = CreateQuoteCart(principal);
			var user = principal.CurrentUser;
			if (user != null)
			{
				if (user.Corporate != null)
				{
					quoteCart.CorporateName = user.Corporate.Name;
				}
				quoteCart.Country = user.DefaultAddress.Country;
				quoteCart.CustomerId = user.Id;
				quoteCart.Email = user.Email;
			}
			return quoteCart;
		}

		public IEnumerable<CartItem> GetLastCartItem(int itemCount)
		{
			if (m_LastCartItems.IsNullOrEmpty())
			{
				var list = CartRepository.GetLastCartItem(itemCount).Take(20);
				lock (m_LastCartItems.SyncRoot)
				{
					foreach (var item in list)
					{
						m_LastCartItems.Add(item);
					}
				}
			}

			return m_LastCartItems.Take(itemCount);
		}

		public Models.CartItem CreateCartItem()
		{
			var result = new CartItem();
			// result.Id = Guid.NewGuid().ToString();
			result.IsCustomerPriceApplied = false;
			result.CreationDate = DateTime.Now;
			result.LastUpdate = DateTime.Now;
			result.Discount = 0;
			result.PriceType = PriceType.Normal;
			result.ShippingType = ShippingType.WhenAvailable;
			result.AllowPartialDelivery = false;

			return result;
		}

		public IList<Models.Comment> GetCommentListByCart(Models.OrderCart cart)
		{
			if (cart == null
				|| !cart.Id.HasValue)
			{
				return null;
			}
			var list = CommentRepository.GetCommentListByModelAndId("Cart", cart.Id.Value);

			return list;
		}

		public void ApplyProductStockInfoList(Models.OrderCart cart)
		{
			// Guard
			if (cart == null
				|| cart.Items.IsNullOrEmpty())
			{
				return;
			}

			// On recherche la liste des infos de stock
			var productIdList = cart.Items.Select(i => i.Product.Id).Distinct();
			var list = CatalogService.GetProductStockInfoList(productIdList);

			foreach (var item in cart.Items)
			{
				var psi = list.SingleOrDefault(i => i.ProductCode.Equals(item.Product.Code, StringComparison.InvariantCultureIgnoreCase));
				item.ProductStockInfo = psi;
			}
		}

		private void AddLastCartItem(Models.CartItem cartItem)
		{
			EventPublisherService.Publish(new Models.Events.AddCartItemEvent()
			{
				Product = cartItem.Product,
				Quantity = cartItem.Quantity,
				SalePrice = cartItem.SalePrice,
			});

			// Conservation des 20 derniers dans la liste
			lock (m_LastCartItems.SyncRoot)
			{
				m_LastCartItems.Insert(0, cartItem);

				if (m_LastCartItems.Count > 20)
				{
					m_LastCartItems.RemoveAt(20);
				}
			}
		}

		private Price GetPriceByQuantity(Product product, int quantity, Price defaultSalePrice)
		{
			Price result = defaultSalePrice;
			if (product.PriceByQuantityList.IsNotNullOrEmpty())
			{
				var price = product.PriceByQuantityList.First(i => quantity >= i.From
																&& quantity < i.To.GetValueOrDefault(int.MaxValue));
				if (price != null)
				{
					result = price.SalePrice;
				}
			}
			return result;
		}

	}
}
