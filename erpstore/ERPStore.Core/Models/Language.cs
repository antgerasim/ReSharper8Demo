using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace ERPStore.Models
{
	/// <summary>
	/// Gère les langues via la norme iso 639.
	/// http://fr.wikipedia.org/wiki/ISO_639
	/// L’ISO 639 (ICS n° 01.140.20) est une norme internationale qui définit des codes pour la représentation des noms de langues.
	/// le lcid est renseigné en tant qu'id. (http://en.wikipedia.org/wiki/Lcid)
	/// http://www.microsoft.com/globaldev/reference/lcid-all.mspx
	/// </summary>
	[Serializable]
	public class Language : EnumBaseType<Language>
	{
		public static readonly Language French = new Language(1036, "FR", "Français - France", "French - France");
		public static readonly Language English = new Language(1033, "EN", "Anglais - USA", "English - United States");
		public static readonly Language Spanish = new Language(3082, "ES", "Espagnol", "Spanish");
		public static readonly Language Italian = new Language(1040, "IT", "Italien", "Italian");
		public static readonly Language German = new Language(1031, "DE", "Allemand", "German");
		public static readonly Language Nederlands = new Language(1043, "NL", "Néerlandais", "Nederlands");

		public Language(int id, string name, string fr, string en)
			: base(id, name, fr, en)
		{

		}

		public static Language GetCurrent()
		{
			var language = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
			var result = enumValues.SingleOrDefault(i => i.Name.Equals(language, StringComparison.InvariantCultureIgnoreCase));
			if (result != null)
			{
				return result;
			}
			return French;
		}

		/// <summary>
		/// Gets the values.
		/// </summary>
		/// <returns></returns>
		public static ReadOnlyCollection<Language> GetValues()
		{
			return GetBaseValues();
		}

		/// <summary>
		/// Gets the by key.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public static Language GetByKey(int id)
		{
			return GetBaseByKey(id);
		}

		public static Language GetByName(string name)
		{
			return GetBaseByName(name);
		}
	}
}
