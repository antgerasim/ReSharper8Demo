using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	public interface ILocalizationService
	{
		/// <summary>
		/// Initializes this instance.
		/// </summary>
		void Initialize(System.Web.HttpContextBase context);
		/// <summary>
		/// Gets the translating key.
		/// </summary>
		/// <value>The translating key.</value>
		string Token { get; }

		/// <summary>
		/// Gets the supported language list.
		/// </summary>
		/// <value>The supported language list.</value>
		IEnumerable<string> SupportedLanguageList { get; }

		/// <summary>
		/// Emplacement raçine de fichiers de ressource
		/// correspondants aux vues
		/// </summary>
		/// <value>The resources root path.</value>
		string ResourcesPath { get; }

		/// <summary>
		/// Retourne une traduction en fonction du chemin de la vue et de la clé de traduction
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="language">The language.</param>
		/// <param name="token">The token.</param>
		/// <param name="key">The key.</param>
		/// <param name="defaultContent">The default content.</param>
		/// <param name="contentType">Type of the content.</param>
		/// <returns></returns>
		string GetLocalizedContent(string path, string language, string token, string key, string defaultContent, Models.LocalizedContentType contentType);

		/// <summary>
		/// Sauvegarde une liste de traduction.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="language">The language.</param>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		void Save(string path, string language, string key, string value);

		/// <summary>
		/// Recupere la liste des traductions pour une liste d'entité donnée 
		/// </summary>
		/// <param name="entityName">Name of the entity.</param>
		/// <param name="list">The list.</param>
		/// <returns></returns>
		Dictionary<int, IEnumerable<Models.EntityLocalization>> GetLocalizationByEntityIdList(string entityName, IEnumerable<int> list);

		/// <summary>
		/// Recupere la liste des traductions pour une entité donnée 
		/// </summary>
		/// <param name="entityName">Name of the entity.</param>
		/// <param name="entityId">The entity id.</param>
		/// <returns></returns>
		IEnumerable<Models.EntityLocalization> GetLocalizationByEntityId(string entityName, int entityId);

		/// <summary>
		/// Retourne la langue en cours
		/// </summary>
		/// <param name="httpContextBase">The HTTP context base.</param>
		/// <returns></returns>
		string GetCurrentLanguage(System.Web.HttpContextBase httpContextBase);
	}
}
