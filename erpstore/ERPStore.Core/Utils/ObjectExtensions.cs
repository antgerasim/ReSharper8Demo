using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ERPStore;

namespace ERPStore
{
	/// <summary>
	/// Extensions de methodes
	/// </summary>
	public static class ObjectExtensions
	{
		public static object GetPropertyValue(this object value, string propertyName)
		{
			var propertyInfo = value.GetType().GetProperty(propertyName);
			return propertyInfo.GetValue(value, null);
		}

		/// <summary>
		/// Retourne l'objet serialisé au format xml
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string SerializeToXml(this object value)
		{
			StringBuilder sb = new StringBuilder();
			using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
			{
				System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(value.GetType());
				serializer.Serialize(ms, value);
				sb.Append(System.Text.Encoding.UTF8.GetString(ms.GetBuffer()));
				ms.Close();
			}
			return sb.ToString();
		}

		public static Models.UserPrincipal GetUserPrincipal(this System.Security.Principal.IPrincipal principal)
		{
			return principal as Models.UserPrincipal;
		}

		public static void AddWebContext(this Exception ex, System.Web.HttpContextBase ctx)
		{
			if (ctx.Request.Url.Host.IndexOf("localhost") == -1)
			{
				var isNewVisitor = false;
				var visitorId = ctx.ApplicationInstance.Context.GetOrCreateVisitorId(out isNewVisitor);
				ex.Data.Add("machineName", Environment.MachineName);
				ex.Data.Add("host", ctx.Request.Url.Host);
				ex.Data.Add("visitorId", visitorId);
				ex.Data.Add("userHostAddress", ctx.Request.UserHostAddress);
				ex.Data.Add("userHostName", ctx.Request.UserHostName);
				// ex.Data.Add("language", Locale.CurrentCultureName);
				ex.Data.Add("url", ctx.Request.RawUrl);
				ex.Data.Add("referer", ctx.Request.UrlReferrer);
				ex.Data.Add("applicationPath", ctx.Request.ApplicationPath);
				ex.Data.Add("user-agent", ctx.Request.Headers["User-Agent"]);
				ex.Data.Add("cookie", ctx.Request.Headers["Cookie"]);
				if (ctx.Request.Form.Count > 0)
				{
					ex.Data.Add("begin-form", "-----------------------");
					foreach (var item in ctx.Request.Form.AllKeys)
					{
						ex.Data.Add(item, ctx.Request.Form[item]);
					}
					ex.Data.Add("end-form", "-----------------------");
				}
				if (ctx.User.Identity.IsAuthenticated)
				{
					ex.Data.Add("user", ctx.User.GetUserPrincipal().CurrentUser.Login);
				}
				else
				{
					ex.Data.Add("user", "anonymous");
				}
			}
		}

		/// <summary>
		/// Return the unmanaged size of an object, in bytes.
		/// </summary>
		/// <param name="value">the object to measure</param>
		/// <returns>The unmanaged size of an object in bytes.</returns>
		public static long SizeOf(this object value)
		{
			long size = 0;
			using (var m = new System.IO.MemoryStream())
			{
				var b = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				b.Serialize(m, value);
				size = m.Length;
				m.Close();
			}
			return size;
		}

		public static List<string> ToWordList(this string value)
		{
			if (value.IsNullOrTrimmedEmpty())
			{
				return new List<string>();
			}

			// Suppression des espaces avant/après
			var workingValue = value.Trim();
			var result = new List<string>();

			// Suppression des doubles espaces
			while (workingValue.IndexOf("  ") != -1)
			{
				workingValue = workingValue.Replace("  ", " ");
			}
			result = workingValue.Split(' ').ToList();
			return result;
		}

		public static string ToLocalizedName(this Models.ProductRelationType relationType)
		{
			switch (relationType)
			{
				case ERPStore.Models.ProductRelationType.Similar:
					return "Les produits similaires";
				case ERPStore.Models.ProductRelationType.Complementary:
					return "Les produits complémentaires";
				case ERPStore.Models.ProductRelationType.Variant:
					return "Les variations";
				case ERPStore.Models.ProductRelationType.Substitute:
					return "Les produits substituables";
				default:
					break;
			}
			return null;
		}

	}
}
