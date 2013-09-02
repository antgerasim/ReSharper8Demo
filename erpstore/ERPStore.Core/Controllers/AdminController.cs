using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace ERPStore.Controllers
{
	public class AdminController : Controller
	{
		public AdminController(Logging.ILogger logger
			// , Services.IAdminService adminService
			, Services.CryptoService cryptoService
			, Services.ICacheService cacheService
			, Services.IDocumentService documentService
			)
		{
			this.Logger = logger;
			// this.AdminService = adminService;
			this.CryptoService = cryptoService;
			this.CacheService = cacheService;
			this.DocumentService = documentService;
		}

		protected Logging.ILogger Logger { get; private set; }

		// protected Services.IAdminService AdminService { get; private set; }

		protected Services.CryptoService CryptoService { get; private set; }

		protected Services.ICacheService CacheService { get; private set; }

		protected Services.IDocumentService DocumentService { get; private set; }

        protected Services.ISalesService SalesService { get; private set; }

		[ActionFilters.AuthorizeActionFilter()]
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Authenticate()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Authenticate(string key)
		{
			if (!key.Equals(ERPStoreApplication.WebSiteSettings.ApiToken))
			{
				ViewData.ModelState.AddModelError("key", "This key is invalid");
			}

			if (ModelState.IsValid)
			{
				Response.Cookies.Remove("AdminErpStore");

				var encryptedKey = CryptoService.EncryptAdminKey(key, DateTime.Now.AddMonths(1));
				var cookie = new System.Web.HttpCookie("AdminErpStore");
				cookie.HttpOnly = true;
				cookie.Path = FormsAuthentication.FormsCookiePath;
				cookie.Secure = FormsAuthentication.RequireSSL;
				var cookieDomain = ERPStore.Configuration.ConfigurationSettings.AppSettings["cookieDomain"]
									?? FormsAuthentication.CookieDomain;
				if (!cookieDomain.IsNullOrTrimmedEmpty()
					&& !HttpContext.Request.IsLocal)
				{
					cookie.Domain = cookieDomain;
				}
				cookie.Expires = DateTime.Now.AddMonths(1);
				cookie.Value = encryptedKey;
				Response.Cookies.Add(cookie);

				var returnUrl = Request["returnUrl"];
				if (!returnUrl.IsNullOrTrimmedEmpty())
				{
					return Redirect(returnUrl);
				}

				return RedirectToAction("index");
			}


			return View();
		}

		[ActionFilters.AuthorizeActionFilter()]
		public ActionResult ProductCategoryUrlMovedPermanently(string badUrl)
		{
			return View();
		}

		[ActionFilters.AuthorizeActionFilter()]
		[HttpPost]
		public ActionResult ProductCategoryUrlMovedPermanently(string badUrl, string movedUrl)
		{
			if (badUrl.IsNullOrTrimmedEmpty()
				|| movedUrl.IsNullOrTrimmedEmpty())
			{
				ModelState.AddModelError("badUrl", "Bad Url or Moved Url is empty");
				return View();
			}
			string fileName = Server.MapPath(@"/app_data/categories-compensation.txt");
			var categoryCompensationListFileInfo = new System.IO.FileInfo(fileName);

			var lines = new Dictionary<string, string>();


			if (System.IO.File.Exists(fileName))
			{
				// Test de l'existence du lien
				using (var content = categoryCompensationListFileInfo.OpenText())
				{
					while (true)
					{
						var line = content.ReadLine();
						if (line.IsNullOrTrimmedEmpty())
						{
							break;
						}
						string[] compensation = line.Split(':');
						lines.Add(compensation[0], compensation[1]);
					}
				}
				System.IO.File.Delete(fileName);
			}

			if (lines.Any(i => i.Key.Equals(badUrl, StringComparison.InvariantCultureIgnoreCase)))
			{
				lines.Remove(badUrl);
			}

			lines.Add(badUrl, movedUrl);

			using (var content = new System.IO.FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.Write))
			{
				foreach (var item in lines)
				{
					var line = string.Format("{0}:{1}\r\n", item.Key, item.Value);
					var b = System.Text.Encoding.UTF8.GetBytes(line);
					content.Write(b, 0, b.Length);
				}
				content.Close();
			}

			return View();
		}

		[ActionFilters.AuthorizeActionFilter()]
		public ActionResult ClearAllCache()
		{
			CacheService.ClearAll();
			ViewData["result"] = "cache was cleared";
			return View("Index");
		}

		[ActionFilters.AuthorizeActionFilter()]
		public ActionResult BrokenImageList()
		{
			var content = new StringBuilder();
			content.Append("<head>");
			content.Append("<title>Liste des images cassées</title>");
			content.Append("</head>");
			content.Append("<body>");
			var brokenImageList = CacheService["BrokenImageList"] as List<Models.BrokenImage>;
			if (brokenImageList.IsNotNullOrEmpty())
			{
				content.Append("<table>");
				content.Append("<tr>");
				content.Append("<td>Id</td>");
				content.Append("<td>Url</td>");
				content.Append("<td>FileName</td>");
				content.Append("<td>Hit Count</td>");
				content.Append("<td>Messages</td>");
				content.Append("</tr>");
				foreach (var item in brokenImageList.OrderByDescending(i => i.HitCount))
				{
					content.Append("<tr>");
					content.AppendFormat("<td><a href='/admin/ImageInfo/{0}'>{0}</a></td>", item.DocumentId);
					content.AppendFormat("<td>{0}</td>", item.Url);
					content.AppendFormat("<td>{0}</td>", item.FullFileName);
					content.AppendFormat("<td>{0}</td>", item.HitCount);
					content.Append("<td>");
					foreach (var message in item.FailedMessages)
					{
						content.Append(message.Value);
						content.Append("<hr/>");
					}
					content.Append("</td>");
					content.Append("</tr>");
				}
				content.Append("</table>");
			}
			content.Append("</body>");
			return Content(content.ToString(), "text/html");
		}

		[ActionFilters.AuthorizeActionFilter()]
		public ActionResult ImageInfo(string Id)
		{
			var document = DocumentService.GetByCode(Id);
			var links = DocumentService.GetModelListByExternalDocumentId(Id);
			var content = new StringBuilder();
			content.Append("<head>");
			content.Append("<title>Détail images cassée</title>");
			content.Append("</head>");
			content.Append("<body>");
			content.AppendFormat("Id:{0}<br/>", document.Id);
			content.AppendFormat("ExternalUrl:{0}<br/>", document.ExternalUrl);
			content.AppendFormat("FileName:{0}<br/>", document.FileName);
			content.AppendFormat("FileSize:{0}<br/>", document.FileSize);
			content.AppendFormat("IconeSrc:{0}<br/>", document.IconeSrc);
			content.AppendFormat("LastUpdate:{0}<br/>", document.LastUpdate);
			content.AppendFormat("Length:{0}<br/>", document.Length);
			content.AppendFormat("MimeType:{0}<br/>", document.MimeType);
			content.AppendFormat("Url:{0}<br/>", document.Url);
			content.Append("<hr/>");
			content.Append("Model<br/>");
			foreach (var item in links)
			{
				content.AppendFormat("Model:{0} Code:{1} Id:{2} CreationDate:{3}<br/>", item.Name, item.Code, item.Id, item.CreationDate );
			}
			content.Append("<hr/>");
			return Content(content.ToString(), "text/html");
		}

	}
}
