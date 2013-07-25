using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ERPStore.Services
{
	/// <summary>
	/// Service des commandes
	/// </summary>
	[ServiceContract(Name = "SalesService"
		, Namespace = "http://www.erpstore.net/2010/05/20")]
	public interface ISalesService
	{
		/// <summary>
		/// Conversion d'un panier en commande
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="cart">The cart.</param>
		Models.ISaleDocument CreateOrderFromCart(ERPStore.Models.User user, Models.OrderCart cart);

		/// <summary>
		/// Retourne une commande via son code
		/// </summary>
		/// <param name="orderCode">The order code.</param>
		/// <returns></returns>
		[OperationContract]
		Models.Order GetOrderByCode(string orderCode);

		/// <summary>
		/// Retourne la liste de toutes les commandes
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="filter">The filter.</param>
		/// <param name="pageIndex">Index of the page.</param>
		/// <param name="size">The size.</param>
		/// <param name="count">The count.</param>
		/// <returns></returns>
		[OperationContract]
		List<Models.Order> GetOrderList(Models.User user, Models.OrderListFilter filter, int pageIndex, int size, out int count);

		/// <summary>
		/// Retoune la liste des noms de filtre pour consulter les commandes 
		/// </summary>
		/// <returns></returns>
		[OperationContract]
		Dictionary<int, string> GetPeriodFilterList();

		/// <summary>
		/// Retourne la liste des etats d'un devis
		/// </summary>
		/// <returns></returns>
		[OperationContract]
		Dictionary<int, string> GetQuoteStateList();

		/// <summary>
		/// Indique si un devis est annulable
		/// </summary>
		/// <param name="order">The order.</param>
		/// <returns>
		/// 	<c>true</c> if [is cancelable order] [the specified order]; otherwise, <c>false</c>.
		/// </returns>
		[OperationContract]
		bool IsCancelableQuote(ERPStore.Models.ISaleDocument order);

		/// <summary>
		/// Annule un devis
		/// </summary>
		/// <param name="quote">The quote.</param>
		/// <param name="reason">The reason.</param>
		/// <param name="comment">The comment.</param>
		[OperationContract]
		void CancelQuote(ERPStore.Models.Quote quote, ERPStore.Models.CancelQuoteReason reason, string comment);

		/// <summary>
		/// Ajoute un commentaire du client dans la commande
		/// </summary>
		/// <param name="order">The order.</param>
		/// <param name="comment">The comment.</param>
		[OperationContract]
		void AddCommentToOrder(ERPStore.Models.Order order, string comment);

		/// <summary>
		/// Retourne le tarif du produit en fonction de l'utilisateur en cours.
		/// </summary>
		/// <param name="product">The product.</param>
		/// <param name="user">L'utilisateur connecté.</param>
		/// <param name="Quantity">The quantity.</param>
		/// <param name="isCustomerPriceApplied">if set to <c>true</c> [is customer price applied].</param>
		/// <returns>Le tarif du produit</returns>
		Models.Price GetProductSalePrice(Models.Product product, Models.User user, int Quantity, out bool isCustomerPriceApplied);

		/// <summary>
		/// Genere une demande de devis
		/// </summary>
		/// <param name="cart">The cart.</param>
		void CreateQuoteFromQuoteCart(ERPStore.Models.QuoteCart cart);

		/// <summary>
		/// Ajoute un reglement à une commande
		/// </summary>
		/// <param name="order">The order.</param>
		/// <param name="paymentModeName">Name of the payment mode.</param>
		/// <param name="transactionId">The transaction id.</param>
		/// <param name="message">The message.</param>
		void AddPaymentToOrder(ERPStore.Models.Order order, string paymentModeName, string transactionId, object message);

		/// <summary>
		/// Ajoute un reglement à une commande
		/// </summary>
		/// <param name="order">The order.</param>
		/// <param name="paymentModeName">Name of the payment mode.</param>
		/// <param name="transactionId">The transaction id.</param>
		/// <param name="message">The message.</param>
		/// <param name="amount">Le montant.</param>
		void AddPaymentToOrder(ERPStore.Models.Order order, string paymentModeName, string transactionId, object message, decimal amount);

		/// <summary>
		/// Retourne la dernière commande passée par un client
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		Models.Order GetLastOrder(ERPStore.Models.User user);

		#region Quotes

		/// <summary>
		/// Retourne un devis via son Id
		/// </summary>
		/// <param name="quoteId">The quote id.</param>
		/// <returns></returns>
		Models.Quote GetQuoteById(int quoteId);

		/// <summary>
        /// Retourne un devis via son code
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
		[OperationContract]
        Models.Quote GetQuoteByCode(string code);

		/// <summary>
		/// Retourne la liste paginée des devis d'un contact en fonction de filtres
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="filter">The filter.</param>
		/// <param name="pageIndex">Index of the page.</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <param name="count">The count.</param>
		/// <returns></returns>
		[OperationContract]
		IEnumerable<Models.Quote> GetQuoteList(Models.User user, Models.QuoteListFilter filter, int pageIndex, int pageSize, out int count);

		/// <summary>
		/// Ajout d'un commentaire dans un devis
		/// </summary>
		/// <param name="quote">The quote.</param>
		/// <param name="comment">The comment.</param>
		[OperationContract]
		void AddCommentToQuote(Models.Quote quote, string comment);

		/// <summary>
		/// Conversion directe d'un devis en commande
		/// </summary>
		/// <param name="quote">The quote.</param>
		/// <param name="warnings">The warnings.</param>
		/// <returns></returns>
		Models.Order ConvertQuoteToOrder(Models.Quote quote, out List<string> warnings);

		/// <summary>
		/// Creation d'un panier via un devis
		/// </summary>
		/// <param name="quote">The quote.</param>
		/// <returns></returns>
		Models.OrderCart CreateOrderCartFromQuote(Models.Quote quote);

		#endregion

		/// <summary>
		/// Retourne les pièces du dossier concernant la commande
		/// </summary>
		/// <param name="order">The order.</param>
		/// <returns></returns>
		[OperationContract]
		Models.WorkflowList GetWorkflow(Models.Order order);

		/// <summary>
		/// Retourne les pièces du dossier concernant le devis
		/// </summary>
		/// <param name="quote">The quote.</param>
		/// <returns></returns>
		[OperationContract]
		Models.WorkflowList GetWorkflow(Models.Quote quote);

		/// <summary>
		/// Retourne toutes les pieces du dossier en amont de la facture
		/// </summary>
		/// <param name="invoice">The invoice.</param>
		/// <returns></returns>
		[OperationContract]
		Models.WorkflowList GetWorkflow(Models.Invoice invoice);

		/// <summary>
		/// Retourne une facture en fonction de son code
		/// </summary>
		/// <param name="invoiceCode">The invoice code.</param>
		/// <returns></returns>
		[OperationContract]
		Models.Invoice GetInvoiceByCode(string invoiceCode);

		/// <summary>
		/// Retourne une liste de facture selon un filtre de manière paginée
		/// pour un utilisateur donné
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="filter">The filter.</param>
		/// <param name="pageIndex">Index of the page.</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <param name="count">The count.</param>
		/// <returns></returns>
		[OperationContract]
		IEnumerable<Models.Invoice> GetInvoiceList(Models.User user, Models.InvoiceListFilter filter, int pageIndex, int pageSize, out int count);

		/// <summary>
		/// Teste la validité de la demande de devis.
		/// </summary>
		/// <param name="cart">The cart.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		List<Models.BrokenRule> ValidateQuoteCart(Models.QuoteCart cart, System.Web.HttpContextBase context);

		/// <summary>
		/// Teste la validité du panier de commande.
		/// </summary>
		/// <param name="cart">The cart.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		List<Models.BrokenRule> ValidateOrderCart(Models.OrderCart cart, System.Web.HttpContextBase context);

		/// <summary>
		/// Retourne tous les modes de reglement disponibles
		/// </summary>
		/// <returns></returns>
		List<Models.Payment> GetPaymentList();

		/// <summary>
		/// Retourne la liste des modes de reglement possibles pour un panier de comamnde
		/// en fonction d'une personne
		/// </summary>
		/// <param name="cart">The cart.</param>
		/// <param name="principal">The principal.</param>
		/// <returns></returns>
		List<Models.Payment> GetPaymentList(Models.OrderCart cart, Models.UserPrincipal principal);

		/// <summary>
		/// Calcul des frais de livraison
		/// </summary>
		/// <param name="cart">The cart.</param>
		/// <param name="principal">The principal.</param>
		void CalculateShippingFee(Models.OrderCart cart, Models.User principal);

		/// <summary>
		/// Calcul le taux de tva en fonction du pays selectionné
		/// </summary>
		/// <param name="cart">The cart.</param>
		/// <param name="principal">The principal.</param>
		void ProcessExport(Models.OrderCart cart, Models.User principal);

		/// <summary>
		/// Mise à jour d'un devis de type "Demande" en "Attente de reglement"
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="cart">The cart.</param>
		/// <param name="productStockInfoList">The product stock info list.</param>
		/// <returns></returns>
		Models.Quote UpdateQuoteFromCart(Models.User user, Models.OrderCart cart, IList<Models.ProductStockInfo> productStockInfoList);

		/// <summary>
		/// Retourne la liste des transporteurs possibles pour un panier donné
		/// </summary>
		/// <param name="cart">The cart.</param>
		/// <returns></returns>
		IList<Models.Conveyor> GetConveyorList(Models.OrderCart cart);


		/// <summary>
		/// Annulation d'une commande
		/// </summary>
		/// <param name="order">The order.</param>
		/// <returns></returns>
		void CancelOrder(ERPStore.Models.Order order, string reason);
	}
}
