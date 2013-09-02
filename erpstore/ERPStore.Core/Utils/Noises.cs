using System;
using System.Collections.Generic;
using System.Text;

namespace ERPStore
{
	public class Noises
	{
		private static Dictionary<string,List<string>> m_list;
		private const string Resource_Key = "ERPStore.Resources.Noises";

		static Noises()
		{
			PopulateList();
		}

		public static List<string> GetList(string language)
		{
			return m_list[language];
		}

		public static bool IsNoise(string language, string keyword)
		{
			return m_list[language].Contains(keyword);
		}

		public static string Clean(string language, string keyword)
		{
			if (keyword.IsNullOrTrimmedEmpty())
			{
				return null;
			}
			var keywordList = keyword.ToWordList();
			Clean(language, keywordList);

			return keywordList.JoinString(" ");
		}

		public static void Clean(string language, List<string> keywordList)
		{
			foreach (var noise in m_list[language])
			{
				keywordList.RemoveAll(i => i.Equals(noise, StringComparison.InvariantCultureIgnoreCase));
			}
		}

		#region Private

		private static void PopulateList()
		{
			m_list = new Dictionary<string, List<string>>();

			m_list.Add("fr", new List<string>());

			foreach (string key in m_list.Keys)
			{
				var assembly = System.Reflection.Assembly.GetExecutingAssembly();
				using (var stream = assembly.GetManifestResourceStream(Resource_Key + "." + key + ".txt"))
				{
					var sr = new System.IO.StreamReader(stream);
					string line = null;
					while ((line = sr.ReadLine()) != null)
					{
						if (!string.IsNullOrEmpty(line))
						{
							line = line.Trim();
							m_list[key].Add(line);
						}
					}
				}
			}

		}

		#endregion
	}
}
