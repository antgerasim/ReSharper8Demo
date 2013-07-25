<%@ WebHandler Language="C#" Class="ERPStore.DocumentHandler" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ERPStore
{
	public class DocumentHandler : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
			string Id = context.Request["Id"];
			if (string.IsNullOrEmpty(Id))
			{
				FileDoesNotExists(context, Id);
			}
			
			var CacheService = ERPStoreApplication.Container.Resolve<Services.ICacheService>();
			var DocumentService = ERPStoreApplication.Container.Resolve<Services.IDocumentService>();
			var Logger = ERPStoreApplication.Container.Resolve<Logging.ILogger>();

			// Chemin vers le fichier
			var localFileName = string.Format(@"{0}\{1}\{2}", ERPStoreApplication.WebSiteSettings.TempPath.TrimEnd('\\').TrimEnd('/'), "{0}", Id);
			CreateFolders(ERPStoreApplication.WebSiteSettings.TempPath, string.Format(@"0\{0}", Id));
			var fileInfo = new System.IO.FileInfo(string.Format(localFileName, 0));
			var media = (Models.Media)CacheService[Id];
			if (media == null)
			{
				media = DocumentService.GetByCode(Id);
				if (media == null)
				{
					FileDoesNotExists(context, Id);
				}
				CacheService.Add(Id, media, DateTime.Now.AddHours(1));
			}

			byte[] content = null;
			var width = 0;

			// Verification de la presence du fichier
			if (!fileInfo.Exists)
			{
				// On charge le fichier que l'on ecrit localement
				if (media.FileName != null && media.FileName.StartsWith("file://"))
				{
					CreateFolders(ERPStoreApplication.WebSiteSettings.TempPath, string.Format(@"{0}\{1}", 0, Id));
					var filePath = media.FileName.Replace("file://", "").Replace(Id, "").Replace("/", @"\").TrimStart('\\'); 
					filePath = System.IO.Path.Combine(ERPStoreApplication.WebSiteSettings.DocumentPath, filePath);
					try
					{
						using (var fs = System.IO.File.OpenRead(filePath))
						{
							fs.Read(content, 0, (int)fs.Length);
							fs.Close();
						}
					}
					catch(Exception ex)
					{
						Logger.Warn(ex.Message);
					}
				}
				// Traitement d'url externe
				else if (!media.ExternalUrl.IsNullOrTrimmedEmpty())
				{
					string mimeType = null;
					CreateFolders(ERPStoreApplication.WebSiteSettings.TempPath, string.Format(@"{0}\{1}", 0, Id));
					try
					{
						content = DocumentService.DownloadImageAndSave(media.ExternalUrl, string.Format(localFileName, "0"), out mimeType);
						if (media.MimeType == null)
						{
							media.MimeType = mimeType;
							DocumentService.SaveMedia(media);
						}
					}
					catch (Exception ex)
					{
						Logger.Warn(ex.Message);
					}
				}
				else
				{
					// Chargement du contenu dans la base de donnée
					content = DocumentService.GetDocumentContentByCode(Id);
				}

				if (content != null)
				{
					if (width > 0 && width < 1000)
					{
						try
						{
							content = ResizeProportionalFromWith(content, width);
						}
						catch
						{
						}
					}

					var f = string.Format(localFileName, width);
					// Suppression du fichier s'il existe
					if (System.IO.File.Exists(f))
					{
						System.IO.File.Delete(f);
					}

					// Creation du fichier
					using (var fs = new System.IO.FileStream(f, System.IO.FileMode.CreateNew))
					{
						fs.Write(content, 0, content.Length);
						fs.Flush();
						fs.Close();
					}
				}
				else
				{
					FileDoesNotExists(context, Id);
				}
			}

			context.Response.Cache.SetCacheability(HttpCacheability.Public);
			context.Response.Cache.SetExpires(DateTime.Now.AddDays(1));
			context.Response.Write(content);
		}

		public void FileDoesNotExists(HttpContext context, string fileName)
		{
			context.Response.StatusCode = 404;
			context.Response.ContentType = "text/plain";
			context.Response.Write(string.Format("le fichier {0} n'existe pas", fileName));
			context.Response.End();
		}

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
		
		private static byte[] ResizeProportionalFromWith(byte[] buffer, int newWidth)
		{
			var ms = new System.IO.MemoryStream(buffer);
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
			using (ms = new System.IO.MemoryStream())
			{
				resultImage.Save(ms, img.RawFormat);
				result = ms.GetBuffer();
				ms.Close();
			}
			return result;
		}

		
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}
	}
}