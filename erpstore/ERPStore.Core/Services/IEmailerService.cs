using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	/// <summary>
	/// Service d'envoi de mail
	/// </summary>
	public interface IEmailerService
	{
		void Send(System.Net.Mail.MailMessage message);

		/// <summary>
		/// Envoi d'une confirmation de creation de compte
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="user">The user.</param>
		void SendAccountConfirmation(System.Web.Mvc.Controller controller, Models.User user);

		/// <summary>
		/// Envoi d'une confirmation de commande
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="order">The order.</param>
		void SendOrderConfirmation(System.Web.Mvc.Controller controller, Models.ISaleDocument order);

		/// <summary>
		/// Envoi une demande de contact
		/// </summary>
		/// <param name="contactInfo">The contact info.</param>
		void SendContactNeeded(Models.ContactInfo contactInfo);

		/// <summary>
		/// Envoi une demande de modification de commande au gestionnaire de compte
		/// </summary>
		/// <param name="order">The order.</param>
		/// <param name="message">The message.</param>
		void SendOrderModificationRequest(ERPStore.Models.Order order, string message);

		/// <summary>
		/// Envoi la confirmation de prise en compte de la demande de modification d'une commande
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="order">The order.</param>
		/// <param name="message">The message.</param>
		void SendOrderModification(System.Web.Mvc.Controller controller, ERPStore.Models.Order order, string message);

		/// <summary>
		/// Envoi de la notification d'annulation de commande
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="quote">The quote.</param>
		/// <param name="reason">The reason.</param>
		/// <param name="comment">The comment.</param>
		void SendQuoteCanceled(System.Web.Mvc.Controller controller, ERPStore.Models.Quote quote, ERPStore.Models.CancelQuoteReason reason, string comment);

		/// <summary>
		/// Envoi un mail pour changer son mot de passe
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="user">The user.</param>
		/// <param name="callBackUrl">The call back URL.</param>
		void SendChangePassword(System.Web.Mvc.Controller controller, ERPStore.Models.User user, string callBackUrl);

		/// <summary>
		/// Envoi un mail de demande de devis
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="cart">The cart.</param>
		void SendQuoteRequest(System.Web.Mvc.Controller controller, ERPStore.Models.QuoteCart cart, string email, string userFulleName);

		/// <summary>
		/// Envoi d'un mail d'echec de payment cb
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="cart">The cart.</param>
		/// <param name="message">The message.</param>
		void SendPaymentByCardFailed(System.Web.Mvc.Controller controller, ERPStore.Models.OrderCart cart, string message);

		/// <summary>
		/// Envoi d'un mail de confirmation de commande vers un nouveau client
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="order">The order.</param>
		/// <param name="password">The password.</param>
		void SendNewCustomerOrderConfirmation(System.Web.Mvc.Controller controller, ERPStore.Models.ISaleDocument order, string password);
	}
}
