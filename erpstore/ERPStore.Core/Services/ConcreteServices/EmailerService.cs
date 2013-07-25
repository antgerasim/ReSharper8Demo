using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Web.Mvc;

using Microsoft.Practices.Unity;

using ERPStore;
using ERPStore.Html;

namespace ERPStore.Services
{
	public class EmailerService : ERPStore.Services.IEmailerService
	{
		private System.Net.Mail.MailAddress m_MailFrom;
		private System.Net.Mail.MailAddressCollection m_Bcc;

		public EmailerService(Logging.ILogger logger
			, Services.IConnectorService connectorService
			, Services.IAccountService accountService
			, Services.CryptoService cryptoService
			, Services.IScheduledTaskService taskService
			)
		{
			this.Logger = logger;
			this.ConnectorService = connectorService;
			this.CryptoService = cryptoService;
			this.AccountService = accountService;
			this.TaskService = taskService;

			var smtpSection = System.Configuration.ConfigurationManager.GetSection("system.net/mailSettings/smtp") as System.Net.Configuration.SmtpSection;
			if (!smtpSection.From.IsNullOrTrimmedEmpty())
			{
				m_MailFrom = new MailAddress(smtpSection.From);
			}
			else
			{
				m_MailFrom = new MailAddress(ERPStoreApplication.WebSiteSettings.Contact.EmailSender,
							   ERPStoreApplication.WebSiteSettings.Contact.EmailSenderName);
			}

			var bccEmail = ERPStoreApplication.WebSiteSettings.Contact.BCCEmail ?? ERPStoreApplication.WebSiteSettings.Contact.ContactEmail;
			m_Bcc = new MailAddressCollection();
			if (bccEmail != null)
			{
				var emailList = bccEmail.Split(';');
				foreach (var email in emailList)
				{
					m_Bcc.Add(email.Trim());
				}
			}
		}

	    protected IConnectorService ConnectorService { get; set; }

	    protected Logging.ILogger Logger { get; set; }

	    internal CryptoService CryptoService { get; set; }

		protected IAccountService AccountService { get; set; }

		protected IScheduledTaskService TaskService { get; set; }

		#region IEmailerService Members

		public virtual void SendAccountConfirmation(System.Web.Mvc.Controller controller, ERPStore.Models.User user)
		{
			var urlHelper = new UrlHelper(controller.ControllerContext.RequestContext);

			var mailKey = new
			{
				UserId = user.Id,
				Salt = Guid.NewGuid().ToString(),
			};
			var encrytpedMailKey = CryptoService.Encrypt(mailKey);
			string encrytpedEmailUrl = string.Format("http://{0}{1}", controller.HttpContext.Request.Url.Host, urlHelper.RouteERPStoreUrl(ERPStoreRoutes.EMAILER, new { action = "DirectAccountConfirmation", key = encrytpedMailKey, }));

			var body = controller.GetActionOutput<Controllers.EmailerController>(i => i.AccountConfirmation(user, encrytpedEmailUrl));
			if (body == null)
			{
				return;
			}

			var message = new MailMessage();
			message.Body = body;
			message.To.Add(new MailAddress(user.Email, user.FullName));
			message.Subject = string.Format("[{0}] Confirmation de votre compte", ERPStoreApplication.WebSiteSettings.SiteName);
			message.IsBodyHtml = true;

			Send(message);
		}

		public virtual void SendOrderConfirmation(System.Web.Mvc.Controller controller, ERPStore.Models.ISaleDocument order)
		{
			if (order.User.Email.IsNullOrTrimmedEmpty())
			{
				return;
			}

			var urlHelper = new UrlHelper(controller.ControllerContext.RequestContext);

			var mailKey = new
			{
				Code = order.Code,
				Type = (order is Models.Order) ? "order" : "quote",
				Salt = DateTime.Now,
			};
			var encrytpedMailKey = CryptoService.Encrypt(mailKey);
			string encrytpedEmailUrl = string.Format("http://{0}{1}", controller.HttpContext.Request.Url.Host, urlHelper.RouteERPStoreUrl(ERPStoreRoutes.EMAILER, new { action = "DirectOrderConfirmation", key = encrytpedMailKey, }));

			var body = controller.GetActionOutput<ERPStore.Controllers.EmailerController>(i => i.OrderConfirmation(order, encrytpedEmailUrl));
			if (body == null)
			{
				return;
			}

			var message = new MailMessage();
			message.Body = body;
			message.To.Add(new MailAddress(order.User.Email, order.User.FullName));
			message.Subject = string.Format("[{0}] Votre commande N°{1}", ERPStoreApplication.WebSiteSettings.SiteName, order.Code);
			message.IsBodyHtml = true;

			Send(message);
		}

	    public virtual void SendQuoteCanceled(System.Web.Mvc.Controller controller, ERPStore.Models.Quote quote, ERPStore.Models.CancelQuoteReason reason, string message)
	    {
	        var body = controller.GetActionOutput<ERPStore.Controllers.EmailerController>(i => i.QuoteCanceled(quote, reason, message));

	        var email = new MailMessage();
	        email.Body = body;
	        email.To.Add(new MailAddress(quote.User.Email, quote.User.FullName));
	        email.Subject = string.Format("[{0}] Annulation de votre devis N°{1}",ERPStoreApplication.WebSiteSettings.SiteName, quote.Code);
	        email.IsBodyHtml = true;

	        Send(email);
	    }

	    public virtual void SendContactNeeded(ERPStore.Models.ContactInfo contactInfo)
		{
			string body = string.Format(@"
Contact site web ERPStore
Nom : {0}
Société : {1}
Email : {2}
Téléphone : {3}
----------------------------------------------
Message : {4}
", contactInfo.FullName, contactInfo.CorporateName, contactInfo.Email, contactInfo.PhoneNumber, contactInfo.Message);

			var settings = ERPStoreApplication.WebSiteSettings;

			var message = new MailMessage();
			message.Body = body;
			message.To.Add(new MailAddress(settings.Contact.ContactEmail));
			message.Subject = "Contact site web";
			message.IsBodyHtml = false;
			if (!contactInfo.Email.IsNullOrTrimmedEmpty())
			{
				message.ReplyTo = new MailAddress(contactInfo.Email);
			}

			Send(message);
		}

		public virtual void SendOrderModificationRequest(ERPStore.Models.Order order, string message)
		{
			string body = string.Format(@"
Commande : {0}
Client : {1}
Email : {2}
Téléphone : {3}
-------------------------------------
Message : {4}
", order.Code, order.User.FullName, order.User.Email, order.User.PhoneNumber, message);

			var settings = ERPStoreApplication.WebSiteSettings;

			var email = new MailMessage();
			email.Body = body;
			email.To.Add(new MailAddress(settings.Contact.ContactEmail));
			email.Subject = string.Format("[{0}] Demande de modification de commande N°{1}", ERPStoreApplication.WebSiteSettings.SiteName, order.Code);
			email.IsBodyHtml = false;
			email.ReplyTo = new MailAddress(order.User.Email, order.User.FullName);

			Send(email);
		}

		public virtual void SendOrderModification(System.Web.Mvc.Controller controller, ERPStore.Models.Order order, string message)
		{
			var body = controller.GetActionOutput<ERPStore.Controllers.EmailerController>(i => i.OrderModification(order, message));

			var email = new MailMessage();
			email.Body = body;
			email.To.Add(new MailAddress(order.User.Email, order.User.FullName));
			email.Subject = string.Format("[{0}] Demande de modification de votre commande N°{1}", ERPStoreApplication.WebSiteSettings.SiteName, order.Code);
			email.IsBodyHtml = true;

			Send(email);
		}

	    public virtual void SendChangePassword(System.Web.Mvc.Controller controller, ERPStore.Models.User user, string callbackUrl)
		{
			var urlHelper = new UrlHelper(controller.ControllerContext.RequestContext);

			var mailKey = new
			{
				UserId = user.Id,
				ExpirationDate = DateTime.Now,
			};
			var encrytpedMailKey = CryptoService.Encrypt(mailKey);
			string encrytpedEmailUrl = string.Format("http://{0}{1}", controller.HttpContext.Request.Url.Host, urlHelper.RouteERPStoreUrl(ERPStoreRoutes.EMAILER, new { action = "DirectSendChangePassword", key = encrytpedMailKey, }));

			var body = controller.GetActionOutput<Controllers.EmailerController>(i => i.ChangePassword(user.FullName, callbackUrl, encrytpedEmailUrl));

			var email = new MailMessage();
			email.Body = body;
			email.To.Add(new MailAddress(user.Email, user.FullName));
			email.Subject = string.Format("[{0}] Demande de changement de mot de passe", ERPStoreApplication.WebSiteSettings.SiteName);
			email.IsBodyHtml = true;

			Send(email);
		}

		public virtual void SendQuoteRequest(System.Web.Mvc.Controller controller, Models.QuoteCart cart, string email, string userFulleName)
		{
			var body = controller.GetActionOutput<Controllers.EmailerController>(i => i.QuoteConfirmation(cart));

			var mailMessage = new MailMessage();
			mailMessage.Body = body;
			if (!userFulleName.IsNullOrTrimmedEmpty())
			{
				mailMessage.To.Add(new MailAddress(email, userFulleName));
			}
			else
			{
				mailMessage.To.Add(new MailAddress(email));
			}
			mailMessage.Subject = string.Format("[{0}] Votre demande de prix", ERPStore.ERPStoreApplication.WebSiteSettings.SiteName);
			mailMessage.IsBodyHtml = true;

			Send(mailMessage);
		}

		public virtual void SendPaymentByCardFailed(Controller controller, ERPStore.Models.OrderCart cart, string message)
		{
			Models.User user = null;
			string body = null;
			string email = null;
			string fullName = null;
			if (cart.CustomerId.GetValueOrDefault(0) != 0)
			{
				user = AccountService.GetUserById(cart.CustomerId.Value);

				if (user != null
					&& user.Email.IsNullOrTrimmedEmpty())
				{
					return;
				}
				body = controller.GetActionOutput<Controllers.EmailerController>(i => i.PaymentByCardFailed(cart, user, message));
				if (body == null)
				{
					return;
				}
				email = user.Email;
				fullName = user.FullName;
			}
			else
			{
				var registrationUser = AccountService.GetRegistrationUser(cart.VisitorId);
				if (registrationUser == null)
				{
					return;
				}
				body = controller.GetActionOutput<Controllers.EmailerController>(i => i.AnonymousPaymentByCardFailed(cart, registrationUser, message));
				if (body == null)
				{
					return;
				}
				email = registrationUser.Email;
				fullName = registrationUser.FullName;
			}

			var mail = new MailMessage();
			mail.Body = body;
			mail.To.Add(new MailAddress(email, fullName));
			mail.Subject = string.Format("[{0}] Echec du reglement de votre commande", ERPStoreApplication.WebSiteSettings.SiteName);
			mail.IsBodyHtml = true;

			Send(mail);
		}

		public virtual void SendNewCustomerOrderConfirmation(System.Web.Mvc.Controller controller, ERPStore.Models.ISaleDocument order, string password)
		{
			if (order.User.Email.IsNullOrTrimmedEmpty())
			{
				return;
			}
			var host = controller.Request.Url.Host;
			var urlHelper = new UrlHelper(controller.ControllerContext.RequestContext);

			var mailKey = new { 
				Code = order.Code, 
				Type = (order is Models.Order) ? "order" : "quote", 
				Password = password,
				Salt = DateTime.Now,
			};
			var encrytpedMailKey = CryptoService.Encrypt(mailKey);
			string encrytpedMailUrl = string.Format("http://{0}{1}", host, urlHelper.RouteERPStoreUrl(ERPStoreRoutes.EMAILER, new { action = "DirectNewCustomerOrderConfirmation", key = encrytpedMailKey, }));

			var body = controller.GetActionOutput<ERPStore.Controllers.EmailerController>(i => i.NewCustomerOrderConfirmation(order, encrytpedMailUrl, password));
			if (body == null)
			{
				return;
			}

			var message = new MailMessage();
			message.Body = body;
			message.To.Add(new MailAddress(order.User.Email, order.User.FullName));
			message.Subject = string.Format("[{0}] Votre commande N°{1}", ERPStoreApplication.WebSiteSettings.SiteName, order.Code);
			message.IsBodyHtml = true;

			Send(message);
		}

		#endregion

		public virtual void Send(System.Net.Mail.MailMessage message)
		{
			Logger.Info("Send message : {0}", message.Subject);

			if (message.From == null)
			{
				message.From = m_MailFrom;
			}

			if (m_Bcc != null)
			{
				foreach (var item in m_Bcc)
				{
					message.Bcc.Add(item);
				}
			}

			System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((object sender) =>
			{
				try
				{
					using (var client = GetCurrentSmtpClient())
					{
						client.Send(message);
					}
				}
				catch (Exception ex)
				{
					Logger.Error(ex);
				}
			}));
		}

		/// <summary>
		/// Gets the current SMTP client.
		/// </summary>
		/// <value>The current SMTP client.</value>
		protected virtual System.Net.Mail.SmtpClient GetCurrentSmtpClient()
		{
			var client = new System.Net.Mail.SmtpClient();

			return client;
		}

	}
}
