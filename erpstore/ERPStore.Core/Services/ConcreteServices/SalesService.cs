using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.Unity;
using System.Web.Mvc;

namespace ERPStore.Services
{
	public class SalesService : ISalesService
	{
		#region ISalesService Members

		public virtual ERPStore.Models.ISaleDocument CreateOrderFromCart(ERPStore.Models.User user, ERPStore.Models.OrderCart cart)
		{
			throw new NotImplementedException();
		}

		public virtual ERPStore.Models.Order GetOrderByCode(string orderCode)
		{
			throw new NotImplementedException();
		}

		public virtual List<ERPStore.Models.Order> GetOrderList(ERPStore.Models.User user, ERPStore.Models.OrderListFilter filter, int pageIndex, int size, out int count)
		{
			throw new NotImplementedException();
		}

		public virtual Dictionary<int, string> GetPeriodFilterList()
		{
			throw new NotImplementedException();
		}

		public virtual Dictionary<int, string> GetQuoteStateList()
		{
			throw new NotImplementedException();
		}

		public virtual bool IsCancelableQuote(ERPStore.Models.ISaleDocument order)
		{
			throw new NotImplementedException();
		}

		public virtual void CancelQuote(ERPStore.Models.Quote quote, ERPStore.Models.CancelQuoteReason reason, string comment)
		{
			throw new NotImplementedException();
		}

		public virtual void AddCommentToOrder(ERPStore.Models.Order order, string comment)
		{
			throw new NotImplementedException();
		}

		public virtual ERPStore.Models.Price GetProductSalePrice(ERPStore.Models.Product product, ERPStore.Models.User user, int Quantity, out bool isCustomerPriceApplied)
		{
			throw new NotImplementedException();
		}

		public virtual void CreateQuoteFromQuoteCart(ERPStore.Models.QuoteCart cart)
		{
			throw new NotImplementedException();
		}

		public virtual void AddPaymentToOrder(ERPStore.Models.Order order, string paymentModeName, string transactionId, object message)
		{
			throw new NotImplementedException();
		}

		public virtual void AddPaymentToOrder(ERPStore.Models.Order order, string paymentModeName, string transactionId, object message, decimal amount)
		{
			throw new NotImplementedException();
		}

		public virtual ERPStore.Models.Order GetLastOrder(ERPStore.Models.User user)
		{
			throw new NotImplementedException();
		}

		public virtual ERPStore.Models.Quote GetQuoteById(int quoteId)
		{
			throw new NotImplementedException();
		}

		public virtual ERPStore.Models.Quote GetQuoteByCode(string code)
		{
			throw new NotImplementedException();
		}

		public virtual IEnumerable<ERPStore.Models.Quote> GetQuoteList(ERPStore.Models.User user, ERPStore.Models.QuoteListFilter filter, int pageIndex, int pageSize, out int count)
		{
			throw new NotImplementedException();
		}

		public virtual void AddCommentToQuote(ERPStore.Models.Quote quote, string comment)
		{
			throw new NotImplementedException();
		}

		public virtual ERPStore.Models.Order ConvertQuoteToOrder(ERPStore.Models.Quote quote, out List<string> warnings)
		{
			throw new NotImplementedException();
		}

		public virtual ERPStore.Models.OrderCart CreateOrderCartFromQuote(ERPStore.Models.Quote quote)
		{
			throw new NotImplementedException();
		}

		public virtual ERPStore.Models.WorkflowList GetWorkflow(ERPStore.Models.Order order)
		{
			throw new NotImplementedException();
		}

		public virtual ERPStore.Models.WorkflowList GetWorkflow(ERPStore.Models.Quote quote)
		{
			throw new NotImplementedException();
		}

		public virtual ERPStore.Models.WorkflowList GetWorkflow(ERPStore.Models.Invoice invoice)
		{
			throw new NotImplementedException();
		}

		public virtual ERPStore.Models.Invoice GetInvoiceByCode(string invoiceCode)
		{
			throw new NotImplementedException();
		}

		public virtual IEnumerable<ERPStore.Models.Invoice> GetInvoiceList(ERPStore.Models.User user, ERPStore.Models.InvoiceListFilter filter, int pageIndex, int pageSize, out int count)
		{
			throw new NotImplementedException();
		}

		public virtual List<ERPStore.Models.BrokenRule> ValidateQuoteCart(ERPStore.Models.QuoteCart cart, System.Web.HttpContextBase context)
		{
			throw new NotImplementedException();
		}

		public virtual List<ERPStore.Models.BrokenRule> ValidateOrderCart(ERPStore.Models.OrderCart cart, System.Web.HttpContextBase context)
		{
			var result = new List<ERPStore.Models.BrokenRule>();

			if (cart.GrandTotal < ERPStore.ERPStoreApplication.WebSiteSettings.Payment.MinimalOrderAmount)
			{
				ERPStore.IEnumerableExtension.AddBrokenRule(result, "GrandTotal", string.Format("Vous n'avez pas atteint le montant minimal de commande qui est de {0:F2} Euros", ERPStore.ERPStoreApplication.WebSiteSettings.Payment.MinimalOrderAmount));
			}

			return result;
		}

		public virtual List<ERPStore.Models.Payment> GetPaymentList()
		{
			// TODO : il y a peut etre mieux a faire plutot que dans la config
			var section = System.Configuration.ConfigurationManager.GetSection("erpStore/erpStorePayments") as ERPStore.Configuration.PaymentConfigurationSection;

            var services = DependencyResolver.Current.GetServices<Services.IPaymentService>();

			var result = new List<ERPStore.Models.Payment>();
			foreach (ERPStore.Configuration.PaymentConfigurationElement item in section.Payments)
			{
				var service = services.SingleOrDefault(i => i.Name.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase));
				if (service == null)
				{
					// Le service indiqué n'est pas enregistré
					continue;
				}

				result.Add(new ERPStore.Models.Payment()
				{
					Name = item.Name,
					PictoUrl = service.PictoUrl,
					Description = service.Description,
					ConfirmationRouteName = service.ConfirmationRouteName,
					ConfirmationViewName = service.ConfirmationViewName,
					FinalizedRouteName = service.FinalizedRouteName,
					FinalizedViewName = service.FinalizedViewName,
				});
			}

			return result;

		}

		public virtual List<ERPStore.Models.Payment> GetPaymentList(ERPStore.Models.OrderCart cart, ERPStore.Models.UserPrincipal principal)
		{
			var section = System.Configuration.ConfigurationManager.GetSection("erpStore/erpStorePayments") as ERPStore.Configuration.PaymentConfigurationSection;

            var services = DependencyResolver.Current.GetServices<Services.IPaymentService>();

			var result = new List<ERPStore.Models.Payment>();
			foreach (ERPStore.Configuration.PaymentConfigurationElement item in section.Payments)
			{
				var service = services.SingleOrDefault(i => i.Name.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase));
				if (service == null)
				{
					// Le service indiqué n'est pas enregistré
					continue;
				}

				var allowed = service.IsAllowedFor(cart, principal);
				if (!allowed)
				{
					continue;
				}

				result.Add(new ERPStore.Models.Payment()
				{
					Name = item.Name,
					IsSelected = cart.PaymentModeName != null && cart.PaymentModeName.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase),
					PictoUrl = service.PictoUrl,
					Description = service.Description,
					ConfirmationRouteName = service.ConfirmationRouteName,
					ConfirmationViewName = service.ConfirmationViewName,
					FinalizedRouteName = service.FinalizedRouteName,
					FinalizedViewName = service.FinalizedViewName,
				});
			}

			return result;
		}

		public virtual void CalculateShippingFee(ERPStore.Models.OrderCart cart, ERPStore.Models.User principal)
		{
			throw new NotImplementedException();
		}

		public virtual void ProcessExport(ERPStore.Models.OrderCart cart, ERPStore.Models.User principal)
		{
			throw new NotImplementedException();
		}

		public virtual ERPStore.Models.Quote UpdateQuoteFromCart(ERPStore.Models.User user, ERPStore.Models.OrderCart cart, IList<ERPStore.Models.ProductStockInfo> productStockInfoList)
		{
			throw new NotImplementedException();
		}

		public virtual IList<ERPStore.Models.Conveyor> GetConveyorList(ERPStore.Models.OrderCart cart)
		{
			throw new NotImplementedException();
		}

		public virtual void CancelOrder(Models.Order order, string reason)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
