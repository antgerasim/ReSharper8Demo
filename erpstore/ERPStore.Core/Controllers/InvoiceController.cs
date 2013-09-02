using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using ERPStore.Html;

namespace ERPStore.Controllers
{
	/// <summary>
	/// Controller des factures
	/// </summary>
	[HandleError(View = "500")]
	public class InvoiceController : StoreController
	{
        private string m_ViewPath;

		public InvoiceController(
			Services.ICatalogService catalogService
			, Services.ISalesService salesService
			, Services.IAccountService accountService
			, Services.IEmailerService emailerService
			, Services.CryptoService cryptoService
			)
		{
			this.CatalogService = catalogService;
			this.SalesService = salesService;
			this.AccountService = accountService;
			this.EmailerService = emailerService;
			this.CryptoService = cryptoService;

            this.m_ViewPath = ERPStore.Configuration.ConfigurationSettings.AppSettings["invoiceVirtualPath"];
		}

		protected Services.ICartService CartService { get; set; }

		protected Services.ICatalogService CatalogService { get; set; }

		protected Services.ISalesService SalesService { get; set; }

		protected Services.IAccountService AccountService { get; set; }

		protected Services.IEmailerService EmailerService { get; set; }

		protected Services.CryptoService CryptoService { get; set; }

        [Authorize(Roles = "customer")]
        [ActionFilters.TrackerActionFilter]
        public ActionResult Index(Models.InvoiceListFilter filter, int? page, int? size)
		{
			var pageId = GetPageId(page);

			if (!size.HasValue)
			{
				size = 10;
			}

			int count = 0;
			var list = SalesService.GetInvoiceList(User.GetUserPrincipal().CurrentUser, filter, pageId, size.Value, out count);

			var model = new Models.InvoiceList(list);
			model.ItemCount = count;
			model.PageIndex = pageId + 1;
			model.PageSize = size.Value;

			ViewData.Model = model;
			return GetDefaultView("invoicelist");
		}

		[ActionFilters.ZeroCacheActionFilter]
		public ActionResult Detail(string key)
		{
			Models.Invoice invoice = null;
			string invoiceCode = key;
			if (key.IsNullOrTrimmedEmpty())
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}
			if (!User.Identity.IsAuthenticated && key.Length < 20)
			{
                return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}
			if (key.Length > 20)
			{
				string code = null;
				DateTime expirationDate;
				bool notification = false;
				CryptoService.DecryptInvoiceDetail(key, out code, out expirationDate, out notification);
				invoiceCode = code;
			}

			invoice = SalesService.GetInvoiceByCode(invoiceCode);
			ViewData.Model = invoice;
			return View("invoicedetail");
		}

		#region Partial Rendering

		[AcceptVerbs(HttpVerbs.Post)]
		[Authorize(Roles = "customer")]
		public ActionResult ShowInvoiceList(string viewName, Models.InvoiceListFilter filter, int? size)
		{
			if (!size.HasValue)
			{
				size = 10;
			}
			int count = 0;
			var list = SalesService.GetInvoiceList(User.GetUserPrincipal().CurrentUser, filter, 0, size.Value, out count);

			var model = new Models.InvoiceList(list);
			model.ItemCount = count;
			model.PageIndex = 0;
			model.PageSize = size.Value;

			ViewData.Model = model;
			return PartialView(viewName);
		}

		#endregion

        private ActionResult GetDefaultView(string defaultView)
        {
            if (m_ViewPath.IsNullOrEmpty())
            {
                return View();
            }

            return View(string.Format(m_ViewPath, defaultView));
        }

        private ActionResult GetDefaultPartialView(string defaultView)
        {
            if (m_ViewPath.IsNullOrEmpty())
            {
                return PartialView();
            }

            return PartialView(string.Format(m_ViewPath, defaultView));
        }

	}
}
