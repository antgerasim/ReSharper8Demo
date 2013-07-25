using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	public interface IDocumentService
	{
		Models.Media GetByCode(string externalDocumentId);
		byte[] DownloadImageAndSave(string uri, string fileName, out string contentType);
		bool WriteImage(string url, System.IO.Stream outputStream, out string contentType);
		string GetFileNameFromUrl(string url);
		void SaveMedia(Models.Media media);
		byte[] ResizeImageTo(byte[] content, int width);
		byte[] GetDocumentContentByCode(string externalDocumentId);
		IList<Models.Media> GetMediaList(Models.Product product);
		IList<Models.Media> GetMediaList(Models.ProductCategory product);
		IList<Models.Media> GetMediaList(Models.Brand product);
		IList<Models.Media> GetMediaList(Models.Order product);
		byte[] GetDocumentContentByKey(string key);
		IList<Models.ModelInfo> GetModelListByExternalDocumentId(string externalDocumentId);
	}
}
