using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.MockConnector
{
	public class EmailerService : ERPStore.Services.EmailerService
	{
		public EmailerService(Logging.ILogger logger
			, ERPStore.Services.IConnectorService connectorService
			, ERPStore.Services.IAccountService accountService
			, ERPStore.Services.CryptoService cryptoService
			, ERPStore.Services.IScheduledTaskService taskService
			)
			: base(logger, connectorService, accountService, cryptoService, taskService)
		{

		}

		public override void Send(System.Net.Mail.MailMessage message)
		{
			// Do nothing
		}

		public override void SendAccountConfirmation(System.Web.Mvc.Controller controller, ERPStore.Models.User user)
		{
			
		}

		public override void SendChangePassword(System.Web.Mvc.Controller controller, ERPStore.Models.User user, string callbackUrl)
		{
			
		}

		public override void SendContactNeeded(ERPStore.Models.ContactInfo contactInfo)
		{
			
		}

		public override void SendOrderConfirmation(System.Web.Mvc.Controller controller, ERPStore.Models.ISaleDocument order)
		{
			
		}

		public override void SendOrderModification(System.Web.Mvc.Controller controller, ERPStore.Models.Order order, string message)
		{
			
		}

		public override void SendOrderModificationRequest(ERPStore.Models.Order order, string message)
		{
			
		}

		public override void SendPaymentByCardFailed(System.Web.Mvc.Controller controller, ERPStore.Models.OrderCart cart, string message)
		{
			
		}

		public override void SendQuoteCanceled(System.Web.Mvc.Controller controller, ERPStore.Models.Quote quote, ERPStore.Models.CancelQuoteReason reason, string message)
		{
			
		}

		public override void SendQuoteRequest(System.Web.Mvc.Controller controller, ERPStore.Models.QuoteCart cart)
		{
			
		}

		public override void SendNewCustomerOrderConfirmation(System.Web.Mvc.Controller controller, ERPStore.Models.ISaleDocument order, string password)
		{
			
		}

	}
}
