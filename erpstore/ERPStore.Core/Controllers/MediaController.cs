using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Microsoft.Practices.Unity;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace ERPStore.Controllers
{
	// [HandleError(View = "500")]
	public class MediaController : StoreController
	{
		#region MimetypeList

		public static Dictionary<string, string> MIMETypeDictionary
			= new Dictionary<string, string>()
		{
   {"323", "text/h323"},
   {"3gp", "video/3gpp"},
   {"3gpp", "video/3gpp"},
   {"acp", "audio/x-mei-aac"},
   {"act", "text/xml"},
   {"actproj", "text/plain"},
   {"ade", "application/msaccess"},
   {"adp", "application/msaccess"},
   {"ai", "application/postscript"},
   {"aif", "audio/aiff"},
   {"aifc", "audio/aiff"},
   {"aiff", "audio/aiff"},
   {"asf", "video/x-ms-asf"},
   {"asm", "text/plain"},
   {"asx", "video/x-ms-asf"},
   {"au", "audio/basic"},
   {"avi", "video/avi"},
   {"bmp", "image/bmp"},
   {"bwp", "application/x-bwpreview"},
   {"c", "text/plain"},
   {"cat", "application/vnd.ms-pki.seccat"},
   {"cc", "text/plain"},
   {"cdf", "application/x-cdf"},
   {"cer", "application/x-x509-ca-cert"},
   {"cod", "text/plain"},
   {"cpp", "text/plain"},
   {"crl", "application/pkix-crl"},
   {"crt", "application/x-x509-ca-cert"},
   {"cs", "text/plain"},
   {"css", "text/css"},
   {"csv", "application/vnd.ms-excel"},
   {"cxx", "text/plain"},
   {"dbs", "text/plain"},
   {"def", "text/plain"},
   {"der", "application/x-x509-ca-cert"},
   {"dib", "image/bmp"},
   {"dif", "video/x-dv"},
   {"dll", "application/x-msdownload"},
   {"doc", "application/msword"},
   {"dot", "application/msword"},
   {"dsp", "text/plain"},
   {"dsw", "text/plain"},
   {"dv", "video/x-dv"},
   {"edn", "application/vnd.adobe.edn"},
   {"eml", "message/rfc822"},
   {"eps", "application/postscript"},
   {"etd", "application/x-ebx"},
   {"etp", "text/plain"},
   {"exe", "application/x-msdownload"},
   {"ext", "text/plain"},
   {"fdf", "application/vnd.fdf"},
   {"fif", "application/fractals"},
   {"fky", "text/plain"},
   {"gif", "image/gif"},
   {"gz", "application/x-gzip"},
   {"h", "text/plain"},
   {"hpp", "text/plain"},
   {"hqx", "application/mac-binhex40"},
   {"hta", "application/hta"},
   {"htc", "text/x-component"},
   {"htm", "text/html"},
   {"html", "text/html"},
   {"htt", "text/webviewhtml"},
   {"hxx", "text/plain"},
   {"i", "text/plain"},
   {"iad", "application/x-iad"},
   {"ico", "image/x-icon"},
   {"ics", "text/calendar"},
   {"idl", "text/plain"},
   {"iii", "application/x-iphone"},
   {"inc", "text/plain"},
   {"infopathxml", "application/ms-infopath.xml"},
   {"inl", "text/plain"},
   {"ins", "application/x-internet-signup"},
   {"iqy", "text/x-ms-iqy"},
   {"isp", "application/x-internet-signup"},
   {"java", "text/java"},
   {"jnlp", "application/x-java-jnlp-file"},
   {"jpg", "image/jpeg"},
   {"jpe", "image/jpeg"},
   {"jpeg", "image/jpeg"},
   {"jfif", "image/jpeg"},
   {"jsl", "text/plain"},
   {"kci", "text/plain"},
   {"la1", "audio/x-liquid-file"},
   {"lar", "application/x-laplayer-reg"},
   {"latex", "application/x-latex"},
   {"lavs", "audio/x-liquid-secure"},
   {"lgn", "text/plain"},
   {"lmsff", "audio/x-la-lms"},
   {"lqt", "audio/x-la-lqt"},
   {"lst", "text/plain"},
   {"m1v", "video/mpeg"},
   {"m3u", "audio/mpegurl"},
   {"m4e", "video/mpeg4"},
   {"MAC", "image/x-macpaint"},
   {"mak", "text/plain"},
   {"man", "application/x-troff-man"},
   {"map", "text/plain"},
   {"mda", "application/msaccess"},
   {"mdb", "application/msaccess"},
   {"mde", "application/msaccess"},
   {"mdi", "image/vnd.ms-modi"},
   {"mfp", "application/x-shockwave-flash"},
   {"mht", "message/rfc822"},
   {"mhtml", "message/rfc822"},
   {"mid", "audio/mid"},
   {"midi", "audio/mid"},
   {"mk", "text/plain"},
   {"mnd", "audio/x-musicnet-download"},
   {"mns", "audio/x-musicnet-stream"},
   {"MP1", "audio/mp1"},
   {"mp2", "video/mpeg"},
   {"mp2v", "video/mpeg"},
   {"mp3", "audio/mpeg"},
   {"mp4", "video/mp4"},
   {"mpa", "video/mpeg"},
   {"mpe", "video/mpeg"},
   {"mpeg", "video/mpeg"},
   {"mpf", "application/vnd.ms-mediapackage"},
   {"mpg", "video/mpeg"},
   {"mpg4", "video/mp4"},
   {"mpga", "audio/rn-mpeg"},
   {"mpv2", "video/mpeg"},
   {"NMW", "application/nmwb"},
   {"nws", "message/rfc822"},
   {"odc", "text/x-ms-odc"},
   {"odh", "text/plain"},
   {"odl", "text/plain"},
   {"p10", "application/pkcs10"},
   {"p12", "application/x-pkcs12"},
   {"p7b", "application/x-pkcs7-certificates"},
   {"p7c", "application/pkcs7-mime"},
   {"p7m", "application/pkcs7-mime"},
   {"p7r", "application/x-pkcs7-certreqresp"},
   {"p7s", "application/pkcs7-signature"},
   {"PCT", "image/pict"},
   {"pdf", "application/pdf"},
   {"pdx", "application/vnd.adobe.pdx"},
   {"pfx", "application/x-pkcs12"},
   {"pic", "image/pict"},
   {"PICT", "image/pict"},
   {"pko", "application/vnd.ms-pki.pko"},
   {"png", "image/png"},
   {"pnt", "image/x-macpaint"},
   {"pntg", "image/x-macpaint"},
   {"pot", "application/vnd.ms-powerpoint"},
   {"ppa", "application/vnd.ms-powerpoint"},
   {"pps", "application/vnd.ms-powerpoint"},
   {"ppt", "application/vnd.ms-powerpoint"},
   {"prc", "text/plain"},
   {"prf", "application/pics-rules"},
   {"ps", "application/postscript"},
   {"pub", "application/vnd.ms-publisher"},
   {"pwz", "application/vnd.ms-powerpoint"},
   {"qt", "video/quicktime"},
   {"qti", "image/x-quicktime"},
   {"qtif", "image/x-quicktime"},
   {"qtl", "application/x-quicktimeplayer"},
   {"qup", "application/x-quicktimeupdater"},
   {"r1m", "application/vnd.rn-recording"},
   {"r3t", "text/vnd.rn-realtext3d"},
   {"RA", "audio/vnd.rn-realaudio"},
   {"RAM", "audio/x-pn-realaudio"},
   {"rat", "application/rat-file"},
   {"rc", "text/plain"},
   {"rc2", "text/plain"},
   {"rct", "text/plain"},
   {"rec", "application/vnd.rn-recording"},
   {"rgs", "text/plain"},
   {"rjs", "application/vnd.rn-realsystem-rjs"},
   {"rjt", "application/vnd.rn-realsystem-rjt"},
   {"RM", "application/vnd.rn-realmedia"},
   {"rmf", "application/vnd.adobe.rmf"},
   {"rmi", "audio/mid"},
   {"RMJ", "application/vnd.rn-realsystem-rmj"},
   {"RMM", "audio/x-pn-realaudio"},
   {"rms", "application/vnd.rn-realmedia-secure"},
   {"rmvb", "application/vnd.rn-realmedia-vbr"},
   {"RMX", "application/vnd.rn-realsystem-rmx"},
   {"RNX", "application/vnd.rn-realplayer"},
   {"rp", "image/vnd.rn-realpix"},
   {"RPM", "audio/x-pn-realaudio-plugin"},
   {"rqy", "text/x-ms-rqy"},
   {"rsml", "application/vnd.rn-rsml"},
   {"rt", "text/vnd.rn-realtext"},
   {"rtf", "application/msword"},
   {"rul", "text/plain"},
   {"RV", "video/vnd.rn-realvideo"},
   {"s", "text/plain"},
   {"sc2", "application/schdpl32"},
   {"scd", "application/schdpl32"},
   {"sch", "application/schdpl32"},
   {"sct", "text/scriptlet"},
   {"sd2", "audio/x-sd2"},
   {"sdp", "application/sdp"},
   {"sit", "application/x-stuffit"},
   {"slk", "application/vnd.ms-excel"},
   {"sln", "application/octet-stream"},
   {"SMI", "application/smil"},
   {"smil", "application/smil"},
   {"snd", "audio/basic"},
   {"snp", "application/msaccess"},
   {"spc", "application/x-pkcs7-certificates"},
   {"spl", "application/futuresplash"},
   {"sql", "text/plain"},
   {"srf", "text/plain"},
   {"ssm", "application/streamingmedia"},
   {"sst", "application/vnd.ms-pki.certstore"},
   {"stl", "application/vnd.ms-pki.stl"},
   {"swf", "application/x-shockwave-flash"},
   {"tab", "text/plain"},
   {"tar", "application/x-tar"},
   {"tdl", "text/xml"},
   {"tgz", "application/x-compressed"},
   {"tif", "image/tiff"},
   {"tiff", "image/tiff"},
   {"tlh", "text/plain"},
   {"tli", "text/plain"},
   {"torrent", "application/x-bittorrent"},
   {"trg", "text/plain"},
   {"txt", "text/plain"},
   {"udf", "text/plain"},
   {"udt", "text/plain"},
   {"uls", "text/iuls"},
   {"user", "text/plain"},
   {"usr", "text/plain"},
   {"vb", "text/plain"},
   {"vcf", "text/x-vcard"},
   {"vcproj", "text/plain"},
   {"viw", "text/plain"},
   {"vpg", "application/x-vpeg005"},
   {"vspscc", "text/plain"},
   {"vsscc", "text/plain"},
   {"vssscc", "text/plain"},
   {"wav", "audio/wav"},
   {"wax", "audio/x-ms-wax"},
   {"wbk", "application/msword"},
   {"wiz", "application/msword"},
   {"wm", "video/x-ms-wm"},
   {"wma", "audio/x-ms-wma"},
   {"wmd", "application/x-ms-wmd"},
   {"wmv", "video/x-ms-wmv"},
   {"wmx", "video/x-ms-wmx"},
   {"wmz", "application/x-ms-wmz"},
   {"wpl", "application/vnd.ms-wpl"},
   {"wprj", "application/webzip"},
   {"wsc", "text/scriptlet"},
   {"wvx", "video/x-ms-wvx"},
   {"XBM", "image/x-xbitmap"},
   {"xdp", "application/vnd.adobe.xdp+xml"},
   {"xfd", "application/vnd.adobe.xfd+xml"},
   {"xfdf", "application/vnd.adobe.xfdf"},
   {"xla", "application/vnd.ms-excel"},
   {"xlb", "application/vnd.ms-excel"},
   {"xlc", "application/vnd.ms-excel"},
   {"xld", "application/vnd.ms-excel"},
   {"xlk", "application/vnd.ms-excel"},
   {"xll", "application/vnd.ms-excel"},
   {"xlm", "application/vnd.ms-excel"},
   {"xls", "application/vnd.ms-excel"},
   {"xlt", "application/vnd.ms-excel"},
   {"xlv", "application/vnd.ms-excel"},
   {"xlw", "application/vnd.ms-excel"},
   {"xml", "text/xml"},
   {"xpl", "audio/scpls"},
   {"xsl", "text/xml"},
   {"z", "application/x-compress"},
   {"zip", "application/x-zip-compressed"}
  };

		#endregion

		private static object m_Lock = new object();

		public MediaController(
			Services.ICacheService cacheService
			, Services.IDocumentService documentService
			) 
		{
			CacheService = cacheService;
			DocumentService = documentService;
		}

		#region Properties

		public Services.ICacheService CacheService { get; set; }

		public Services.IDocumentService DocumentService { get; set; }

		protected List<Models.BrokenImage> BrokenImageList
		{
			get
			{
				var list = CacheService["BrokenImageList"] as List<Models.BrokenImage>;
				if (list == null)
				{
					list = new List<ERPStore.Models.BrokenImage>();
					CacheService.Add("BrokenImageList", list, DateTime.Now.AddDays(1));
				}
				return list;
			}
		}

		#endregion

		public ActionResult Image(string externalDocumentId, int width, int height, string fileName)
		{
			Logger.Debug("Process picture : {0} : size : {1}", externalDocumentId, width);
			if (string.IsNullOrEmpty(externalDocumentId))
			{
				Logger.Warn("Picture not found");
				return ImageFileDoesNotExists(fileName);
			}

			var localFileName = string.Format(@"{0}\{1}\{2}", ERPStoreApplication.WebSiteSettings.TempPath.TrimEnd('\\').TrimEnd('/'), "{0}x{1}", externalDocumentId);
			var fullFileName = string.Format(localFileName, width, height);
			var fileInfo = new System.IO.FileInfo(fullFileName);

			Logger.Debug("Picture to show : {0}", fileInfo.FullName);

			// Verification de la presence du fichier
			if (!fileInfo.Exists)
			{
				// CreateFolders(ERPStoreApplication.WebSiteSettings.TempPath, string.Format(@"{0}x{1}\{2}", width, height, externalDocumentId));

				// Chemin vers le fichier
				var media = (Models.Media)CacheService[externalDocumentId];
				if (media == null)
				{
					media = DocumentService.GetByCode(externalDocumentId);
					if (media == null)
					{
						Logger.Warn("Picture not found");
						return ImageFileDoesNotExists(fileName);
					}
					CacheService.Add(externalDocumentId, media, DateTime.Now.AddHours(1));
				}

				Logger.Debug("Picture does not exists in temp folder : {0}", fileInfo.FullName);
				bool result = false;
				try
				{
					result = CreateFile(media, width, height, fileInfo, externalDocumentId, fileName, localFileName);
				}
				catch (Exception ex)
				{
					var brokenImage = BrokenImageList.FirstOrDefault(i => i.DocumentId.Equals(externalDocumentId, StringComparison.InvariantCultureIgnoreCase));
					if (brokenImage == null)
					{
						brokenImage = new ERPStore.Models.BrokenImage();
						brokenImage.DocumentId = externalDocumentId;
						if (!brokenImage.FailedMessages.ContainsKey(ex.Message.GetHashCode()))
						{
							brokenImage.FailedMessages.Add(ex.Message.GetHashCode(), ex.Message);
						}
						if (ex.Data.Contains("DocumentService:WriteImage:Url"))
						{
							brokenImage.Url = (string)ex.Data["DocumentService:WriteImage:Url"];
						}
						brokenImage.FullFileName = fullFileName;
						BrokenImageList.Add(brokenImage);
					}
					brokenImage.HitCount++;
					media.IsMissing = true;
				}

				if (!result)
				{
					media.IsMissing = true;
					return ImageFileDoesNotExists(fileName);
				}
			}

			if (!System.IO.File.Exists(fullFileName))
			{
				return ImageFileDoesNotExists(fileName);
			}

			// Ajout du cache d'image
			Response.Cache.SetCacheability(HttpCacheability.Public);
			Response.Cache.SetExpires(DateTime.Now.AddDays(1));
			Response.Cache.SetLastModified(DateTime.Now);

			string mimeType = "image/png";
			var extension = System.IO.Path.GetExtension(fullFileName);
			if (extension != null)
			{
				extension = extension.Trim('.');
				var mime = MIMETypeDictionary.FirstOrDefault(i => i.Key.Equals(extension, StringComparison.InvariantCultureIgnoreCase));
				if (mime.Value != null)
				{
					mimeType = mime.Value;
				}
			}

			return this.File(fullFileName, mimeType);
		}

		public ActionResult Download(string externalDocumentId, string fileName)
		{
			if (string.IsNullOrEmpty(externalDocumentId))
			{
				return ImageFileDoesNotExists(fileName);
			}

			var doc = DocumentService.GetByCode(externalDocumentId);
			if (doc == null)
			{
				return ImageFileDoesNotExists(fileName);
			}

			var content = DocumentService.GetDocumentContentByCode(externalDocumentId);

			Response.AddHeader("content-disposition", string.Format("attachement; filename={0}", fileName));
			Response.Charset = string.Empty;
			Response.Cache.SetCacheability(HttpCacheability.NoCache);

			if (content != null)
			{
				string mimeType = doc.MimeType;
				if (mimeType.IsNullOrTrimmedEmpty())
				{
					mimeType = "image/gif";
				}
				return this.File(content, mimeType);
			}
			return Content(string.Empty, "text/plain");
		}

		public ActionResult DocumentDownload(string title, string key)
		{
			byte[] content = DocumentService.GetDocumentContentByKey(key);
			Response.AddHeader("content-disposition", string.Format("attachement; filename={0}.pdf", title));
			Response.Charset = "";
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			return this.File(content, "application/pdf");
		}

		public ActionResult ImageFileDoesNotExists(string fileName)
		{
			var missingProductImage = ERPStore.Configuration.ConfigurationSettings.AppSettings["missingproductimage"];
			if (missingProductImage != null)
			{
				var fullFileName = HttpContext.Server.MapPath(missingProductImage);
				var ext = System.IO.Path.GetExtension(fullFileName);
				var mimeType = "image/png";
				if (ext != null
					&& ext.Length > 0)
				{
					mimeType = MIMETypeDictionary.FirstOrDefault(i => i.Key.Equals(ext.Trim('.'), StringComparison.InvariantCultureIgnoreCase)).Value;
					if (mimeType == null)
					{
						mimeType = "image/png";
					}
				}

				Response.Cache.SetCacheability(HttpCacheability.Public);
				Response.Cache.SetExpires(DateTime.Now.AddDays(1));
				Response.Cache.SetLastModified(DateTime.Now);

				return this.File(fullFileName, mimeType);
			}
			// Response.Status = string.Format("File not found : {0}", fileName);
			Response.StatusCode = 404;
			return this.Content(string.Format("le fichier {0} est introuvable", fileName), "text/plain");
		}

		public ActionResult RoundedCorner(string cornerType, int radius, int thickness, string outsideColor, string insideColor, string curveColor)
		{
			byte[] result = null;
			using (var bmp = new Bitmap(radius, radius, PixelFormat.Format32bppArgb))
			{
				bmp.MakeTransparent();
				using (var g = Graphics.FromImage(bmp))
				{
					g.SmoothingMode = SmoothingMode.None;
					g.Clear(ColorTranslator.FromHtml(outsideColor));
					Brush insideBrush = new SolidBrush(ColorTranslator.FromHtml(insideColor));
					Brush borderBrush = new SolidBrush(ColorTranslator.FromHtml(curveColor));
					Pen borderPen = new Pen(borderBrush, 1f);
					switch (cornerType.ToLower())
					{
						case "tl":
							g.FillEllipse(insideBrush, 0, 0, radius * 2, radius * 2);
							g.DrawEllipse(borderPen, 0, 0, radius * 2, radius * 2);
							break;

						case "tr":
							g.FillEllipse(insideBrush, -radius - 1, 0, radius * 2, radius * 2);
							g.DrawEllipse(borderPen, -radius - 1, 0, radius * 2, radius * 2);
							break;

						case "bl":
							g.FillEllipse(insideBrush, 0, (radius - (radius * 2)) - 1, radius * 2, radius * 2);
							g.DrawEllipse(borderPen, 0, (radius - (radius * 2)) - 1, radius * 2, radius * 2);
							break;

						case "br":
							g.FillEllipse(insideBrush, (int)(-radius - 1), (int)((radius - (radius * 2)) - 1), (int)(radius * 2), (int)(radius * 2));
							g.DrawEllipse(borderPen, (int)(-radius - 1), (int)((radius - (radius * 2)) - 1), (int)(radius * 2), (int)(radius * 2));
							break;

						default:
							g.FillRectangle(insideBrush, 0, 0, radius, radius - radius);
							break;
					}
					var ms = new MemoryStream();
					bmp.Save(ms, ImageFormat.Gif);
					result = ms.GetBuffer();
				}
			}
			Response.Cache.SetCacheability(HttpCacheability.Public);
			Response.Cache.SetExpires(DateTime.Now.AddDays(1));
			return this.File(result, "image/gif");
		}

		#region Partial Rendering

		public ActionResult ShowAttachmentList(Models.Product product, string viewName)
		{
			var list = DocumentService.GetMediaList(product);
			list.RemoveAll(i => i.MimeType == null 
								|| i.MimeType.StartsWith("image/")
								);
			ViewData.Model = list;
			return PartialView(viewName);
		}

		#endregion

		private void CreateFolders(string root, string path)
		{
			path = path.Trim('\\').Trim('/');
			// Il existe un sub folder
			if (path.IndexOf(@"\") != -1)
			{
				string[] folders = path.Split('\\');
				for (int i = 0; i < folders.Length - 1; i++)
				{
					string folder = folders[i];
					root = root + @"\" + folder;
					if (!System.IO.Directory.Exists(root))
					{
						System.IO.Directory.CreateDirectory(root);
					}
				}
			}

		}

		private void CreateDeepDirectory(string path)
		{
			if (System.IO.Directory.Exists(path))
			{
				return;
			}

			var parts = path.Split('\\').ToList();
			var paths = new List<string>();
			while(parts.Count() != 1)
			{
				var part = parts.Last();
				paths.Insert(0,path);
				path = path.Replace(part, "");
				parts.RemoveAt(parts.Count - 1);
			}

			foreach (var item in paths)
			{
				if (!System.IO.Directory.Exists(item))
				{
					System.IO.Directory.CreateDirectory(item);
				}
			}
		}

		private bool CreateFile(Models.Media media, int width, int height, FileInfo fileInfo, string externalDocumentId, string fileName, string localFileName)
		{
			byte[] content = null;
			if (media.FileName != null 
				&& media.FileName.StartsWith("file://"))
			{
				if (ERPStoreApplication.WebSiteSettings.DocumentPath != null
					&& System.IO.Directory.Exists(ERPStoreApplication.WebSiteSettings.DocumentPath))
				{
					var filePath = media.FileName.Replace("file://", "").Replace(externalDocumentId, "").Replace("/", @"\").TrimStart('\\');
					filePath = System.IO.Path.Combine(ERPStoreApplication.WebSiteSettings.DocumentPath, filePath);
					var image = System.Drawing.Image.FromFile(filePath);
					Logger.Info("Locale picture : {0}", filePath);
					using (var ms = new MemoryStream())
					{
						image.Save(ms, image.RawFormat);
						content = ms.GetBuffer();
						ms.Close();
					}
					Logger.Debug("Picture in memory size : {0}", content.Length);
				}
				else
				{
					// Photo a distance 
					media.ExternalUrl = string.Format("{0}/doc.ashx?id={1}", ERPStoreApplication.WebSiteSettings.ExtranetUrl, externalDocumentId);
					Logger.Debug("Calling from extranet : {0}", media.ExternalUrl);
				}
			}
			// Traitment d'url externe
			// On charge le fichier que l'on ecrit localement
			if (!media.ExternalUrl.IsNullOrTrimmedEmpty())
			{
				Logger.Debug("Process external url : {0}", media.ExternalUrl);
				string mimeType = null;
				var folder = System.IO.Path.Combine(ERPStoreApplication.WebSiteSettings.TempPath, "0x0");
				CreateDeepDirectory(folder);
				int retryCount = 0;
retry_download:
				try
				{
					content = DocumentService.DownloadImageAndSave(media.ExternalUrl, string.Format(localFileName, "0", "0"), out mimeType);
					if (media.MimeType == null)
					{
						media.MimeType = mimeType;
						DocumentService.SaveMedia(media);
					}
				}
				catch (System.IO.IOException)
				{
					if (retryCount < 3)
					{
						System.Threading.Thread.Sleep(200);
						goto retry_download;
					}
					retryCount++;
					throw;
				}
				catch (Exception ex)
				{
					ex.Data.Add("MediaController:ExternalUrl", media.ExternalUrl);
					throw(ex);				
				}
			}
			else if (content == null)
			{
				Logger.Debug("Process db blob : {0}", externalDocumentId);
				// Chargement du contenu dans la base de donnée
				content = DocumentService.GetDocumentContentByCode(externalDocumentId);
				if (content == null)
				{
					var ex = new Exception("Blob is empty");
					throw ex;
				}
			}

			if (content == null 
				|| content.Length == 0)
			{
				return false;
			}
			if (width > 0 
				&& width < 1000)
			{
				if (height == 0)
				{
					content = ResizeProportionalFromWith(content, width);
				}
				else
				{
					content = ResizeAndCrop(content, width, height);
				}
			}

			var f = fileInfo.FullName;
			// Suppression du fichier s'il existe
			if (System.IO.File.Exists(f))
			{
				Logger.Debug("Delete picture : {0}", f);
				System.IO.File.Delete(f);
			}

			CreateDeepDirectory(fileInfo.DirectoryName);

			// Creation du fichier
			using (var fs = new System.IO.FileStream(f, System.IO.FileMode.CreateNew))
			{
				Logger.Debug("Create picture : {0}", f);
				fs.Write(content, 0, content.Length);
				fs.Flush();
				fs.Close();
			}

			return true;
		}

		private static byte[] ResizeProportionalFromWith(byte[] buffer, int newWidth)
		{
			var ms = new MemoryStream(buffer);
			var img = System.Drawing.Image.FromStream(ms);
			//on recupere la largeur et la hauteur de 
			//l'image que l'on souhaite redimensionner
			int height = img.Height;
			int width = img.Width;

			// Calcul le rapport hauteur largeur.
			// On multiplie notre nouvelle hauteur par le rapport 
			// pour avoir la nouvelle largeur.
			// Math.ceiling permet d'arrondir le chiffre retourné
			var dif = Convert.ToDouble(Decimal.Divide(width, height));
			height = Convert.ToInt32(Math.Ceiling(newWidth / dif));

			// Nouvelle image dans laquelle on va ecrire
			// en redimentionnant
			var resultImage = new Bitmap(newWidth, height);

			using (var resizer = Graphics.FromImage(resultImage))
			{
				resizer.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				resizer.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
				resizer.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
				resizer.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				resizer.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

				resizer.DrawImage(img, 0, 0, newWidth, height);
			}

			byte[] result = null;
			using (ms = new MemoryStream())
			{
				resultImage.Save(ms, img.RawFormat);
				result = ms.GetBuffer();
				ms.Close();
			}
			return result;
		}

		private static byte[] ResizeAndCrop(byte[] buffer, int Width, int Height)
		{
			if (buffer == null
				|| buffer.Length == 0)
			{
				return buffer;
			}

			var ms = new MemoryStream(buffer);
			var imgPhoto = System.Drawing.Image.FromStream(ms);

			int sourceWidth = imgPhoto.Width;
			int sourceHeight = imgPhoto.Height;
			int sourceX = 0;
			int sourceY = 0;
			int destX = 0;
			int destY = 0;

			float nPercent = 0;
			float nPercentW = 0;
			float nPercentH = 0;

			nPercentW = ((float)Width / (float)sourceWidth);
			nPercentH = ((float)Height / (float)sourceHeight);
			if (nPercentH < nPercentW)
			{
				nPercent = nPercentH;
				destX = System.Convert.ToInt16((Width - (sourceWidth * nPercent)) / 2);
			}
			else
			{
				nPercent = nPercentW;
				destY = System.Convert.ToInt16((Height - (sourceHeight * nPercent)) / 2);
			}

			int destWidth = (int)(sourceWidth * nPercent);
			int destHeight = (int)(sourceHeight * nPercent);

			var bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
			bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

			var grPhoto = Graphics.FromImage(bmPhoto);
			grPhoto.Clear(Color.White); 
			grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

			grPhoto.DrawImage(imgPhoto,
				new Rectangle(destX, destY, destWidth, destHeight),
				new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
				GraphicsUnit.Pixel);

			byte[] result = null;
			using (ms = new MemoryStream())
			{
				bmPhoto.Save(ms, imgPhoto.RawFormat);
				result = ms.GetBuffer();
				ms.Close();
			}
			grPhoto.Dispose();
			return result;
		}
	}
}
