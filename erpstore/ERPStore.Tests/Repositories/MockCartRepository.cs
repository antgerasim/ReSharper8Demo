using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Tests.Repositories
{
	class MockCartRepository : ERPStore.Repositories.ICartRepository
	{
		private List<ERPStore.Models.CartBase> m_List;

		public MockCartRepository()
		{
			m_List = new List<ERPStore.Models.CartBase>();
		}

		#region ICartRepository Members

		public string GetCurrentCartId(ERPStore.Models.CartType cartType, ERPStore.Models.UserPrincipal user)
		{
			switch (cartType)
			{
				case ERPStore.Models.CartType.Order:
					if (m_List.IsNotNullOrEmpty())
					{
						return GetOrderList(user).First().Code;
					}
					break;
				case ERPStore.Models.CartType.Quote:
					if (m_List.IsNotNullOrEmpty())
					{
						return GetQuoteList(user).First().Code;
					}
					break;
				default:
					break;
			}
			return Guid.NewGuid().ToString();
		}

		public ERPStore.Models.CartBase GetCartById(string cartId)
		{
			return m_List.SingleOrDefault(i => i.Code.Equals(cartId, StringComparison.InvariantCultureIgnoreCase));
		}

		public void Save(ERPStore.Models.CartBase cart)
		{
			if (!m_List.Contains(cart))
			{
				m_List.Add(cart);
			}
		}

		public void Remove(ERPStore.Models.CartBase cart)
		{
			if (m_List.Contains(cart))
			{
				m_List.Remove(cart);
			}
		}

		public void Remove(string cartId)
		{
			m_List.RemoveAll(i => i.Code.Equals(cartId, StringComparison.InvariantCultureIgnoreCase));
		}

		public IQueryable<ERPStore.Models.CartBase> GetList(ERPStore.Models.UserPrincipal user)
		{
			return m_List.AsQueryable();
		}

		public IList<ERPStore.Models.OrderCart> GetOrderList(ERPStore.Models.UserPrincipal user)
		{
			return m_List.Where(i => i.GetType() == typeof(ERPStore.Models.OrderCart)).Cast<ERPStore.Models.OrderCart>().ToList();
		}

		public IList<ERPStore.Models.QuoteCart> GetQuoteList(ERPStore.Models.UserPrincipal user)
		{
			return m_List.Where(i => i.GetType() == typeof(ERPStore.Models.QuoteCart)).Cast<ERPStore.Models.QuoteCart>().ToList();
		}

		public void ChangeCurrent(string cartId, ERPStore.Models.CartType cartType, ERPStore.Models.UserPrincipal user)
		{
			var cart = GetCartById(cartId);
			if (cart != null)
			{
				m_List.Remove(cart);
				m_List.Insert(0, cart);
			}
		}

        public ERPStore.Models.CartBase GetQuoteCartByConvertedEntityId(int entityId)
        {
            throw new NotImplementedException();
        }

        public void ChangeType(ERPStore.Models.CartBase cart, ERPStore.Models.CartType cartType)
        {
            throw new NotImplementedException();
        }


		public ERPStore.Models.CartBase GetActiveCartById(string cartId)
		{
			return m_List.SingleOrDefault(i => i.Code == cartId);
		}

		public string GetCurrentCartIdByVisitorId(ERPStore.Models.CartType cartType, string visitorId)
		{
			var list = GetActiveCartListByVisitorId(visitorId);
			ERPStore.Models.CartBase cart = null;
			switch (cartType)
			{
				case ERPStore.Models.CartType.Order:
					cart = list.Where(i => i is ERPStore.Models.OrderCart).FirstOrDefault();
					break;
				case ERPStore.Models.CartType.Quote:
					cart = list.Where(i => i is ERPStore.Models.QuoteCart).FirstOrDefault();
					break;
				default:
					break;
			}

			if (cart == null)
			{
				return null;
			}
			
			return cart.Code;
		}

		public List<ERPStore.Models.CartBase> GetActiveCartListByVisitorId(string visitorId)
		{
			return m_List.Where(i => i.VisitorId == visitorId).ToList();
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
			throw new NotImplementedException();
		}

		#endregion
	}
}
