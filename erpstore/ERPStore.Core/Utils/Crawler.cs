using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ERPStore
{
	public static class Crawler
	{
		public static string CrawlContent(string url)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "GET";
			request.UserAgent = string.Format("Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0; ERP360; {0})", Guid.NewGuid());
			request.KeepAlive = true;
			request.Accept = @"*/*";
			request.UseDefaultCredentials = true;
			request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
			request.AllowAutoRedirect = false;

			HttpWebResponse response = null;
			try
			{
				response = (HttpWebResponse)request.GetResponse();
			}
			catch 
			{
				if (response != null)
				{
					response.Close();
					response = null;
				}
				request = null;
				return string.Empty;
			}

			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw new WebException(string.Format("Status code: {0}", response.StatusCode));
			}

			StringBuilder sb = null;
			if (response.ContentLength > 0)
			{
				sb = new StringBuilder((int)response.ContentLength);
			}
			else
			{
				sb = new StringBuilder();
			}
			using (var str = response.GetResponseStream())
			{
				using (var reader = new StreamReader(str, System.Text.Encoding.UTF8))
				{
					int bufferSize = 1024;
					char[] buffer = new char[bufferSize];
					int pos = 0;
					while ((pos = reader.Read(buffer, 0, bufferSize)) > 0)
					{
						sb.Append(buffer, 0, pos);
					}
					// content = reader.ReadToEnd();
					reader.Close();
				}
			}

			if (response != null)
			{
				response.Close();
				response = null;
			}
			request = null;
			return sb.ToString();
		}

		public static string Ping(string url)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "GET";
			request.UserAgent = string.Format("Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0; ERP360; {0})", Guid.NewGuid());
			request.KeepAlive = true;
			request.Accept = @"*/*";
			request.UseDefaultCredentials = true;
			request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
			request.AllowAutoRedirect = false;
			request.Timeout = 10 * 1000;

			HttpWebResponse response = null;
			try
			{
				response = (HttpWebResponse)request.GetResponse();
			}
			catch
			{
				return System.Net.HttpStatusCode.RequestTimeout.ToString();
			}

			var result = response.StatusCode.ToString();
			if (response != null)
			{
				response.Close();
				response = null;
			}
			request = null;

			return result;
		}

	}
}
