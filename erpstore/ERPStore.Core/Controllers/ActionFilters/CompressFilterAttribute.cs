using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Compression;
using System.Web.Mvc;

namespace ERPStore.Controllers.ActionFilters
{
	/// <summary>
	/// Permet la compression GZip d'une page
	/// </summary>
	[Obsolete("Use IIS Settings Instead", true)]
	public sealed class CompressFilter : ActionFilterAttribute
	{
		/// <summary>
		/// Called before the action method executes.
		/// </summary>
		/// <param name="filterContext">The filter context.</param>
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var request = filterContext.HttpContext.Request;

			string acceptEncoding = request.Headers["Accept-Encoding"];

			if (string.IsNullOrEmpty(acceptEncoding)
				|| filterContext.HttpContext.Request.Url.Host.IndexOf("localhost") != -1
				|| !ERPStore.ERPStoreApplication.WebSiteSettings.EnableGZip)
			{
				return;
			}

			acceptEncoding = acceptEncoding.ToUpperInvariant();

			var response = filterContext.HttpContext.Response;

			if (acceptEncoding.IndexOf("GZIP", StringComparison.InvariantCultureIgnoreCase) != -1)
			{
				response.AppendHeader("Content-encoding", "gzip");
				response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
			}
			else if (acceptEncoding.IndexOf("DEFLATE",  StringComparison.InvariantCultureIgnoreCase) != -1)
			{
				response.AppendHeader("Content-encoding", "deflate");
				response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
			}
		}
	}
}
