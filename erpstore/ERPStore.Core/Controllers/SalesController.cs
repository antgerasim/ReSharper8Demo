using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using ERPStore.Html;

namespace ERPStore.Controllers
{
	public class SalesController : StoreController
	{
		public SalesController(
			Services.ISalesService saleService
			)
		{
			this.SaleService = saleService;
		}



		#region Properties

		public Services.ISalesService SaleService { get; protected set; }

		#endregion

		#region Render Partial

		// [Authorize(Roles = "customer")]
		public ActionResult ShowOrderWorkflow(string viewName, Models.Order order)
		{
			var workflowList = SaleService.GetWorkflow(order);
			SetupUrl(workflowList);
			ViewData.Model = workflowList;
            return PartialView(viewName);
		}

		// [Authorize(Roles = "customer")]
		public ActionResult ShowQuoteWorkflow(string viewName, Models.Quote quote)
		{
			var workflowList = SaleService.GetWorkflow(quote);
			SetupUrl(workflowList);
			ViewData.Model = workflowList;
            return PartialView(viewName);
		}

		// [Authorize(Roles = "customer")]
		public ActionResult ShowInvoiceWorkflow(string viewName, Models.Invoice invoice)
		{
			var workflowList = SaleService.GetWorkflow(invoice);
			SetupUrl(workflowList);
			ViewData.Model = workflowList;
			return PartialView(viewName);
		}

		#endregion

		private void SetupUrl(Models.WorkflowList list)
		{
			var urlHelper = new UrlHelper(this.ControllerContext.RequestContext);
			foreach (var item in list)
			{
				item.DownloadUrl = urlHelper.DownloadDocumentHref(item);
				switch (item.Type)
				{
					case ERPStore.Models.SaleDocumentType.Order:
						item.Url = urlHelper.OrderHref(item.Code).ToHtmlString();
						break;
					case ERPStore.Models.SaleDocumentType.Quote:
						item.Url = urlHelper.QuoteHref(item.Code);
						break;
					case ERPStore.Models.SaleDocumentType.Delivery:
						break;
					case ERPStore.Models.SaleDocumentType.Invoice:
						item.Url = urlHelper.InvoiceHref(item.Code);
						break;
					default:
						break;
				}
			}
		}
	}
}
