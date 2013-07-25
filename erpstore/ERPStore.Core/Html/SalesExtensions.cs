using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Mvc.Ajax;

using Microsoft.Practices.Unity;

namespace ERPStore.Html
{
	/// <summary>
	/// Methodes d'extentions permettant la gestion de l'affichage concernant
	/// tout ce qui est lié à la commande
	/// </summary>
	public static class SalesExtensions
	{
		public static MvcHtmlString CheckoutLink(this HtmlHelper helper)
		{
			return CheckoutLink(helper, "Valider ma commande");
		}

		public static MvcHtmlString CheckoutLink(this HtmlHelper helper, string title)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.CHECKOUT, new { action = "Shipping" }));
		}

		public static MvcHtmlString CheckOutHref(this UrlHelper helper)
		{
			return new MvcHtmlString(helper.RouteERPStoreUrl(ERPStoreRoutes.CHECKOUT, new { action = "Shipping" }));
		}

		public static MvcHtmlString CheckOutConfigurationHref(this UrlHelper helper)
		{
			return new MvcHtmlString(helper.RouteERPStoreUrl(ERPStoreRoutes.CHECKOUT_CONFIGURATION, null));
		}

		public static MvcHtmlString CheckOutPaymentHref(this UrlHelper helper)
		{
			return new MvcHtmlString(helper.RouteERPStoreUrl(ERPStoreRoutes.CHECKOUT_PAYMENT, null));
		}

		public static MvcHtmlString QuoteIt(this HtmlHelper helper)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink("Faire une demande de prix et delais", "QuoteIt", new { step = "Confirm" }));
		}

		public static MvcHtmlString EditAddressLink(this HtmlHelper helper, string title, int index)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.EDIT_ADDRESS, new { index = index }));
		}

		public static MvcHtmlString Href(this UrlHelper helper, Models.Order order)
		{
			return helper.OrderHref(order.Code);
		}

		public static MvcHtmlString OrderHref(this UrlHelper helper, string orderCode)
		{
			return new MvcHtmlString(helper.RouteERPStoreUrl(ERPStoreRoutes.ORDER_DETAIL, new { orderCode = orderCode }));
		}

		public static string PublicHref(this UrlHelper helper, Models.Order order)
		{
            var cryptoService = DependencyResolver.Current.GetService<Services.CryptoService>();
			var ticket = cryptoService.GetPublicOrderTicket(order);
			var url = helper.RouteERPStoreUrl(ERPStoreRoutes.ORDER_DETAIL, null);
			string encryptedUrl = string.Format("http://{0}{1}{2}", helper.RequestContext.HttpContext.Request.Url.Host, url, ticket);
			return encryptedUrl;
		}

		/// <summary>
		/// Retourne la liste des périodes de filtrage
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static IEnumerable<SelectListItem> GetPeriodFilterOptionList(this HtmlHelper helper)
		{
            var salesService = DependencyResolver.Current.GetService<Services.ISalesService>();
			var periodId = 0;
			if (!int.TryParse(helper.ViewContext.HttpContext.Request["periodId"], out periodId))
			{
				periodId = 0;
			}
			var filterList = salesService.GetPeriodFilterList();
			var result = new List<SelectListItem>();
			foreach (var item in filterList)
			{
				result.Add(new SelectListItem()
				{
					Selected = (item.Key == periodId),
					Text = item.Value,
					Value = StoreExtensions.AddParameter(helper.ViewContext.HttpContext.Request.Url.PathAndQuery, "periodId", item.Key.ToString()),
				});
			}
			return result;
		}

		public static IEnumerable<SelectListItem> GetQuoteStateOptionList(this HtmlHelper helper)
		{
            var salesService = DependencyResolver.Current.GetService<Services.ISalesService>();
			var stateId = -1;
			if (!int.TryParse(helper.ViewContext.HttpContext.Request["stateId"], out stateId))
			{
				stateId = -1;
			}
			var stateList = salesService.GetQuoteStateList();
			var result = new List<SelectListItem>();
			foreach (var item in stateList)
			{
				result.Add(new SelectListItem()
				{
					Selected = (item.Key == stateId),
					Text = item.Value,
					Value = item.Key.ToString(),
				});
			}
			return result.OrderBy(i => i.Value);
		}

		public static MvcForm BeginOrderListForm(this HtmlHelper helper)
		{
			return helper.BeginERPStoreRouteForm("OrderListForm", FormMethod.Post);
		}

		public static MvcHtmlString EditOrderLink(this HtmlHelper helper, Models.Order order)
		{
			return helper.EditOrderLink(order,string.Format("Modifier la commande N°{0}", order.Code));
		}

		public static MvcHtmlString EditOrderLink(this HtmlHelper helper, Models.Order order, string title)
		{
			return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.EDIT_ORDER, new { orderCode = order.Code }));
		}

		public static string EditOrderHref(this UrlHelper helper, Models.Order order)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.EDIT_ORDER, new { orderCode = order.Code });
		}

		public static string OrderListHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.ORDER_LIST, null);
		}

		public static string OrderListHref(this UrlHelper helper, int statusId)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.ORDER_LIST, new { statusId = statusId });
		}

		public static MvcHtmlString CancelQuoteLink(this HtmlHelper helper, Models.Quote quote)
		{
			return helper.CancelQuoteLink(quote, string.Format("Annuler le devis N°{0}", quote.Code));
		}

		public static MvcHtmlString CancelQuoteLink(this HtmlHelper helper, Models.Quote quote, string title)
		{
			return new MvcHtmlString( helper.RouteERPStoreLink(title, ERPStoreRoutes.CANCEL_QUOTE, new { key = quote.Code }));
		}

		public static string CancelQuoteHref(this UrlHelper helper, Models.Quote quote)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.CANCEL_QUOTE, new { key = quote.Code });
		}

		public static MvcHtmlString AddCommentToOrderLink(this HtmlHelper helper, Models.Order order)
		{
			return helper.AddCommentToOrderLink(order, string.Format("Ajouter un commentaire", order.Code));
		}

		public static MvcHtmlString AddCommentToOrderLink(this HtmlHelper helper, Models.Order order, string title)
		{
            return new MvcHtmlString(helper.RouteERPStoreLink(title, ERPStoreRoutes.ADD_COMMENT_TO_ORDER, new { orderCode = order.Code }));
		}

		public static string AddCommentToOrderHref(this UrlHelper helper, Models.Order order)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.ADD_COMMENT_TO_ORDER, new { orderCode = order.Code });
		}

		public static MvcForm BeginEditOrderForm(this HtmlHelper helper)
		{
			return helper.BeginERPStoreRouteForm(ERPStoreRoutes.EDIT_ORDER, FormMethod.Post);
		}

		public static MvcForm BeginAddCommentToOrderForm(this HtmlHelper helper)
		{
			return helper.BeginERPStoreRouteForm(ERPStoreRoutes.ADD_COMMENT_TO_ORDER, FormMethod.Post);
		}

		public static MvcForm BeginAddCommentToOrderForm(this AjaxHelper helper, AjaxOptions options)
		{
			return helper.BeginERPStoreRouteForm(ERPStoreRoutes.ADD_COMMENT_TO_ORDER, options);
		}

		public static MvcHtmlString ShowOrderDocuments(this HtmlHelper helper, Models.Order order)
		{
			return helper.ShowOrderDocuments(order, "_medialist");
		}

		public static MvcHtmlString ShowOrderDocuments(this HtmlHelper helper, Models.Order order, string viewName)
		{
			return helper.Action<Controllers.OrderController>(c => c.ShowOrderDocuments(order, viewName ));
		}

		public static MvcForm BeginCancelQuoteForm(this HtmlHelper helper)
		{
			return helper.BeginERPStoreRouteForm(ERPStoreRoutes.CANCEL_QUOTE, FormMethod.Post);
		}

		public static IEnumerable<SelectListItem> CancelQuoteReasonDictionary(this HtmlHelper helper, string parameterName)
		{
			var result = new List<SelectListItem>();
			var list = Enum.GetValues(typeof(Models.CancelQuoteReason));
			var param = helper.ViewContext.RequestContext.HttpContext.Request[parameterName];
			int selected = 0;
			if (param != null)
			{
				int.TryParse(param, out selected);
			}
			foreach (Models.CancelQuoteReason item in list)
			{
				result.Add(new SelectListItem()
					{
						Text = item.GetLocalizedName(),
						Value = item.ToString(),
						Selected = (int)item == selected,						
					});
			}
			return result;
		}

		/// <summary>
		/// Adresse de la liste des commandes pour un appel ajax
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string AjaxOrderListHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.AJAX_ORDER_LIST, null );
		}

        public static MvcForm BeginAcceptQuoteForm(this HtmlHelper helper)
        {
            return helper.BeginERPStoreRouteForm(ERPStoreRoutes.ACCEPT_QUOTE, FormMethod.Post);
        }

		public static MvcHtmlString HiddenDefaultConveyor(this HtmlHelper helper)
		{
			return helper.Hidden("conveyorIndex", ERPStoreApplication.WebSiteSettings.Shipping.ConveyorList.IndexOf(ERPStoreApplication.WebSiteSettings.Shipping.DefaultConveyor));
		}

		/// <summary>
		/// Hrefs the specified helper.
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="quote">The quote.</param>
		/// <returns></returns>
		public static string Href(this UrlHelper helper, Models.Quote quote)
		{
			return helper.QuoteHref(quote.Code);
		}

		public static string QuoteHref(this UrlHelper helper, string quoteCode)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.QUOTE_DETAIL, new { key = quoteCode });
		}

		/// <summary>
		/// Retourne une url vers la liste des devis
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string QuoteListHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.QUOTE_LIST, null);
		}

		/// <summary>
		/// Retoune l'url d'acceptation d'un devis
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string AcceptQuoteConfirmationUrl(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.ACCEPT_QUOTE_CONFIRMATION, null);
		}

		/// <summary>
		/// Retourne une url vers la liste des devis
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string AjaxQuoteListHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.AJAX_QUOTE_LIST, null);
		}

		/// <summary>
		/// Retourne l'url de la photo par defaut d'un vendeur
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="vendor">The vendor.</param>
		/// <param name="width">The width.</param>
		/// <param name="defaultImageSrc">The default image SRC.</param>
		/// <returns></returns>
		public static string ImageSrc(this UrlHelper helper, Models.Vendor vendor, int width, string defaultImageSrc)
		{
			if (vendor != null 
                && vendor.DefaultImage != null)
			{
				return string.Format(vendor.DefaultImage.Url, width, width);
			}
			else if (defaultImageSrc != null)
			{
				return defaultImageSrc;
			}
			return null;

		}

		/// <summary>
		/// Formulaire pour l'ajout de commentaire dans un devis
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public static MvcForm BeginAddCommentToQuoteForm(this AjaxHelper helper, AjaxOptions options)
		{
			return helper.BeginERPStoreRouteForm(ERPStoreRoutes.ADD_COMMENT_TO_QUOTE, options);
		}

		/// <summary>
		/// Url de la liste des factures
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string AjaxInvoiceListHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.AJAX_INVOICE_LIST, null);
		}

		/// <summary>
		/// Affiche le workflow complet d'une vente
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="viewName">Name of the view.</param>
		/// <param name="order">The order.</param>
		public static MvcHtmlString ShowWorkflow(this HtmlHelper helper, string viewName, Models.Order order)
		{
			return helper.Action<Controllers.SalesController>(c => c.ShowOrderWorkflow(viewName ,order ));
		}

		/// <summary>
		/// Affiche le workflow complet de la vente a partir du devis
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="viewName">Name of the view.</param>
		/// <param name="quote">The quote.</param>
		public static MvcHtmlString ShowWorkflow(this HtmlHelper helper, string viewName, Models.Quote quote)
		{
			return helper.Action<Controllers.SalesController>(c => c.ShowQuoteWorkflow(viewName, quote ));
		}

		/// <summary>
		/// Affiche le workflow complet en amont de la facture
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="viewName">Name of the view.</param>
		/// <param name="invoice">The invoice.</param>
		public static MvcHtmlString ShowWorkflow(this HtmlHelper helper, string viewName, Models.Invoice invoice)
		{
			return helper.Action<Controllers.SalesController>(c => c.ShowInvoiceWorkflow(viewName , invoice)); 
		}

		/// <summary>
		/// Downloads the document href.
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="document">The document.</param>
		/// <returns></returns>
		public static string DownloadDocumentHref(this UrlHelper helper, Models.WorkflowDocument document)
		{
            var cryptoService = DependencyResolver.Current.GetService<Services.CryptoService>();

			var key = cryptoService.EncryptDocumentDownload(document.Type.ToString(), document.Id);

			return helper.RouteERPStoreUrl(ERPStoreRoutes.DOCUMENT_DOWNLOAD, new { key = key, title = document.Code });
		}

		public static string DownloadDocumentHref(this UrlHelper helper, Models.SaleDocumentType type, int documentId, string code)
		{
            var cryptoService = DependencyResolver.Current.GetService<Services.CryptoService>();

			var key = cryptoService.EncryptDocumentDownload(type.ToString(), documentId);

			return helper.RouteERPStoreUrl(ERPStoreRoutes.DOCUMENT_DOWNLOAD, new { key = key, title = code });
		}


		/// <summary>
		/// Hrefs the specified helper.
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="invoice">The invoice.</param>
		/// <returns></returns>
		public static string Href(this UrlHelper helper, Models.Invoice invoice)
		{
			return helper.InvoiceHref(invoice.Code);
		}

		/// <summary>
		/// Invoices the href.
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="invoiceCode">The invoice code.</param>
		/// <returns></returns>
		public static string InvoiceHref(this UrlHelper helper, string invoiceCode)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.INVOICE_DETAIL, new { key = invoiceCode });
		}

		/// <summary>
		/// Lien vers la liste des factures
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string InvoiceListHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.INVOICE_LIST, null);
		}

		/// <summary>
		/// Liste des factures en cours
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string CurrentInvoiceListHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.INVOICE_LIST, new { StatusId = 1, });
		}

		/// <summary>
		/// Lien vers la dernière commande
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string LastOrderHref(this UrlHelper helper)
		{
			return helper.RouteERPStoreUrl(ERPStoreRoutes.SHOW_LAST_ORDER);
		}

		/// <summary>
		/// Retourne la liste des transporteurs possibles pour un panier donné
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="cart">The cart.</param>
		/// <returns></returns>
		public static IList<ERPStore.Models.Conveyor> ConveyorList(this HtmlHelper helper, Models.OrderCart cart)
		{
            var salesService = DependencyResolver.Current.GetService<Services.ISalesService>();
			return salesService.GetConveyorList(cart);
		}
	}
}
