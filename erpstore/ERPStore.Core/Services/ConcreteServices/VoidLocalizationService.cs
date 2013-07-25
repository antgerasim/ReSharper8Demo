using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	public class VoidLocalizationService : ILocalizationService
	{
		#region ILocalizationService Members

		public void Initialize(System.Web.HttpContextBase context)
		{
			if (context.Items["language"] != null)
			{
				return;
			}
			string currentLanguage = "fr";
			//if (context.Request.UserLanguages.IsNotNullOrEmpty())
			//{
			//    var userLanguage = context.Request.UserLanguages.First();
			//    if (userLanguage != null
			//        && userLanguage.Length > 2)
			//    {
			//        currentLanguage = userLanguage.Substring(0, 2).ToLower();
			//    }
			//}

			//if (currentLanguage.IsNullOrTrimmedEmpty())
			//{
			//    return;
			//}

			context.Items["language"] = currentLanguage;

			var language = ERPStore.Models.Language.French;
			try
			{
				language = ERPStore.Models.Language.GetByName(currentLanguage);
			}
			catch { }
			var ci = new System.Globalization.CultureInfo(language.Id);

			// Fixe la culture du thread en cours
			// Change le format de date et autres
			System.Threading.Thread.CurrentThread.CurrentCulture = ci;
			System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
		}

		public Dictionary<int, IEnumerable<ERPStore.Models.EntityLocalization>> GetLocalizationByEntityIdList(string entityName, IEnumerable<int> list)
		{
			return null;
		}

		public IEnumerable<ERPStore.Models.EntityLocalization> GetLocalizationByEntityId(string entityName, int entityId)
		{
			return null;
		}

		public string Token
		{
			get 
			{
				return Guid.NewGuid().ToString();
			}
		}

		public IEnumerable<string> SupportedLanguageList
		{
			get 
			{
				return Models.Language.GetValues().Select(i => i.Name);
			}
		}

		public string ResourcesPath
		{
			get 
			{
				return null;
			}
		}

		public string GetLocalizedContent(string path, string language, string token, string key, string defaultContent, Models.LocalizedContentType contentType)
		{
			return defaultContent;
		}

		public void Save(string path, string language, string key, string value)
		{
			
		}

		public string GetCurrentLanguage(System.Web.HttpContextBase context)
		{
			return null;
		}

		#endregion
	}
}
