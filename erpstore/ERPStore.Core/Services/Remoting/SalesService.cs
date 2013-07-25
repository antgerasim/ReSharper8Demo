using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Web.Mvc;

using Microsoft.Practices.Unity;

namespace ERPStore.Services.Remoting
{
	public class SalesService : ISalesService
	{
		public SalesService()
			: this(DependencyResolver.Current)
		{
		}

		public SalesService(IDependencyResolver container)
		{
			ConcreteSalesService = container.GetService<ISalesService>();
            AccountService = container.GetService<IAccountService>();
		}

		protected ISalesService ConcreteSalesService { get; private set; }

		protected IAccountService AccountService { get; private set; }

		#region Private

		private Models.User Authenticate()
		{
			string apiKey = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("apiKey", "ns");
			if (apiKey == null)
			{
				throw new System.Security.SecurityException("apiKey header not present");
			}
			var corporate = AccountService.GetCorporateByApiKey(apiKey);
			if (corporate == null)
			{
				throw new System.Security.SecurityException("apiKey does not exists");
			}
			string login = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("login", "ns");
			string password = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("password", "ns");

			var userId = AccountService.Authenticate(login, password);
			var user = AccountService.GetUserById(userId);

			if (user.Corporate == null)
			{
				throw new System.Security.SecurityException("a user must be associated with a company");
			}

			if (user.Corporate.Id != corporate.Id)
			{
				throw new System.Security.SecurityException("the user is not associated with the company indicated");
			}

			return user;
		}

		#endregion

		#region ISalesService Members

		public ERPStore.Models.ISaleDocument CreateOrderFromCart(ERPStore.Models.User user, ERPStore.Models.OrderCart cart)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.Order GetOrderByCode(string orderCode)
		{
			var currentUser = Authenticate();
			var result = ConcreteSalesService.GetOrderByCode(orderCode);
			if (result.User.Corporate.Id != currentUser.Corporate.Id)
			{
				return null;
			}
			return result;
		}

		public List<ERPStore.Models.Order> GetOrderList(ERPStore.Models.User user, ERPStore.Models.OrderListFilter filter, int pageIndex, int size, out int count)
		{
			throw new NotImplementedException();
		}

		public Dictionary<int, string> GetPeriodFilterList()
		{
			throw new NotImplementedException();
		}

		public Dictionary<int, string> GetQuoteStateList()
		{
			throw new NotImplementedException();
		}

		public bool IsCancelableQuote(ERPStore.Models.ISaleDocument order)
		{
			throw new NotImplementedException();
		}

		public void CancelQuote(ERPStore.Models.Quote quote, ERPStore.Models.CancelQuoteReason reason, string comment)
		{
			throw new NotImplementedException();
		}

		public void AddCommentToOrder(ERPStore.Models.Order order, string comment)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.Price GetProductSalePrice(ERPStore.Models.Product product, ERPStore.Models.User user, int Quantity, out bool isCustomerPriceApplied)
		{
			throw new NotImplementedException();
		}

		public void CreateQuoteFromQuoteCart(ERPStore.Models.QuoteCart cart)
		{
			throw new NotImplementedException();
		}

		public virtual void AddPaymentToOrder(ERPStore.Models.Order order, string paymentModeName, string transactionId, object message, decimal amount)
		{
			throw new NotImplementedException();
		}

		public virtual void AddPaymentToOrder(ERPStore.Models.Order order, string paymentModeName, string transactionId, object message)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.Order GetLastOrder(ERPStore.Models.User user)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.Quote GetQuoteById(int quoteId)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.Quote GetQuoteByCode(string code)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<ERPStore.Models.Quote> GetQuoteList(ERPStore.Models.User user, ERPStore.Models.QuoteListFilter filter, int pageIndex, int pageSize, out int count)
		{
			throw new NotImplementedException();
		}

		public void AddCommentToQuote(ERPStore.Models.Quote quote, string comment)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.Order ConvertQuoteToOrder(ERPStore.Models.Quote quote, out List<string> warnings)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.OrderCart CreateOrderCartFromQuote(ERPStore.Models.Quote quote)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.WorkflowList GetWorkflow(ERPStore.Models.Order order)
		{
			var user = Authenticate();
			if (user.Corporate.Id != order.User.Corporate.Id)
			{
				return null;
			}
			return ConcreteSalesService.GetWorkflow(order);
		}

		public ERPStore.Models.WorkflowList GetWorkflow(ERPStore.Models.Quote quote)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.WorkflowList GetWorkflow(ERPStore.Models.Invoice invoice)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.Invoice GetInvoiceByCode(string invoiceCode)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<ERPStore.Models.Invoice> GetInvoiceList(ERPStore.Models.User user, ERPStore.Models.InvoiceListFilter filter, int pageIndex, int pageSize, out int count)
		{
			throw new NotImplementedException();
		}

		public List<ERPStore.Models.BrokenRule> ValidateQuoteCart(ERPStore.Models.QuoteCart cart, System.Web.HttpContextBase context)
		{
			throw new NotImplementedException();
		}

		public List<ERPStore.Models.BrokenRule> ValidateOrderCart(ERPStore.Models.OrderCart cart, System.Web.HttpContextBase context)
		{
			throw new NotImplementedException();
		}

		public List<ERPStore.Models.Payment> GetPaymentList()
		{
			throw new NotImplementedException();
		}

		public List<ERPStore.Models.Payment> GetPaymentList(ERPStore.Models.OrderCart cart, ERPStore.Models.UserPrincipal principal)
		{
			throw new NotImplementedException();
		}

		public void CalculateShippingFee(ERPStore.Models.OrderCart cart, ERPStore.Models.User principal)
		{
			throw new NotImplementedException();
		}

		public void ProcessExport(ERPStore.Models.OrderCart cart, ERPStore.Models.User principal)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.Quote UpdateQuoteFromCart(ERPStore.Models.User user, ERPStore.Models.OrderCart cart, IList<ERPStore.Models.ProductStockInfo> productStockInfoList)
		{
			throw new NotImplementedException();
		}

		public IList<Models.Conveyor> GetConveyorList(Models.OrderCart cart)
		{
			return ConcreteSalesService.GetConveyorList(cart);
		}

		public void CancelOrder(Models.Order order, string reason)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
