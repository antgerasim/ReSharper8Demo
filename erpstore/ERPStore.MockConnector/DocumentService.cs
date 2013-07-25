using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.MockConnector
{
	public class DocumentService : ERPStore.Services.IDocumentService
	{
		#region IDocumentService Members

		public ERPStore.Models.Media GetByCode(string externalDocumentId)
		{
			throw new NotImplementedException();
		}

		public byte[] DownloadImageAndSave(string uri, string fileName, out string contentType)
		{
			throw new NotImplementedException();
		}

		public void WriteImage(string url, System.IO.Stream outputStream, out string contentType)
		{
			throw new NotImplementedException();
		}

		public string GetFileNameFromUrl(string url)
		{
			throw new NotImplementedException();
		}

		public void SaveMedia(ERPStore.Models.Media media)
		{
			throw new NotImplementedException();
		}

		public byte[] ResizeImageTo(byte[] content, int width)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Media> GetMediaList(ERPStore.Models.Product product)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Media> GetMediaList(ERPStore.Models.ProductCategory product)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Media> GetMediaList(ERPStore.Models.Brand product)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Media> GetMediaList(ERPStore.Models.Order product)
		{
			throw new NotImplementedException();
		}

		public byte[] GetDocumentContentByCode(string externalDocumentId)
		{
			throw new NotImplementedException();
		}

		public byte[] GetDocumentContentByKey(string key)
		{
			throw new NotImplementedException();
		}

		bool ERPStore.Services.IDocumentService.WriteImage(string url, System.IO.Stream outputStream, out string contentType)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
