using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.MockConnector
{
	public class SalesService : ERPStore.Services.SalesService
	{
		private List<ERPStore.Models.ISaleDocument> m_OrderList;

		public SalesService()
		{
			m_OrderList = new List<ERPStore.Models.ISaleDocument>();
		}

		#region ISalesService Members

		public override ERPStore.Models.ISaleDocument CreateOrderFromCart(ERPStore.Models.User user, ERPStore.Models.OrderCart cart)
		{
			var order = new Models.Order();
			order.Id = 1;
			order.Code = Guid.NewGuid().ToString();
			order.User = user;
			order.PaymentModeName = cart.PaymentModeName;
			m_OrderList.Add(order);
			return order;
		}

		public override void CreateQuoteFromQuoteCart(ERPStore.Models.QuoteCart cart)
		{
			Console.WriteLine("CreateQuotFormQuoteCart");
		}

		public override void AddPaymentToOrder(ERPStore.Models.Order order, string paymentModeName, string transactionId, object message)
		{

		}

		public override ERPStore.Models.Order GetLastOrder(ERPStore.Models.User user)
		{
			return null;
		}

		public override ERPStore.Models.Order GetOrderByCode(string orderCode)
		{
			var doc = m_OrderList.SingleOrDefault(i => i.Code == orderCode);
			return doc as ERPStore.Models.Order;
		}

		public override ERPStore.Models.Price GetProductSalePrice(ERPStore.Models.Product product, ERPStore.Models.User user, int Quantity, out bool isCustomerPriceApplied)
		{
			isCustomerPriceApplied = false;
			return product.SalePrice;
		}

		public override List<ERPStore.Models.BrokenRule> ValidateQuoteCart(ERPStore.Models.QuoteCart cart, System.Web.HttpContextBase context)
		{
			var result = new List<ERPStore.Models.BrokenRule>();

			var form = context.Request.Form;
			var lastNameForm = form["lastname"];
			var emailForm = form["email"];

			if (lastNameForm.IsNullOrTrimmedEmpty())
			{
				ERPStore.IEnumerableExtension.AddBrokenRule(result, "lastname", "Le nom doit etre indiqué");
			}

			if (!ERPStore.Services.EmailValidator.IsValidEmail(emailForm))
			{
				ERPStore.IEnumerableExtension.AddBrokenRule(result, "email", "L'email est invalide");
			}

			return result;
		}

		public override List<ERPStore.Models.BrokenRule> ValidateOrderCart(ERPStore.Models.OrderCart cart, System.Web.HttpContextBase context)
		{
			var result = new List<ERPStore.Models.BrokenRule>();

			if (ERPStore.ERPStoreApplication.WebSiteSettings.Payment.MinimalOrderAmount != null 
				&& cart.GrandTotal < ERPStore.ERPStoreApplication.WebSiteSettings.Payment.MinimalOrderAmount)
			{
				ERPStore.IEnumerableExtension.AddBrokenRule(result, "GrandTotal", string.Format("Vous n'avez pas atteint le montant minimal de commande qui est de {0:F2} Euros", ERPStore.ERPStoreApplication.WebSiteSettings.Payment.MinimalOrderAmount));
			}

			return result;
		}

		public override void CalculateShippingFee(ERPStore.Models.OrderCart cart, ERPStore.Models.User principal)
		{
			// Do nothing
		}

		public override void ProcessExport(ERPStore.Models.OrderCart cart, ERPStore.Models.User principal)
		{
			// Do nothing
		}

		#endregion
	}
}
