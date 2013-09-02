using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Lien d'un menu
	/// </summary>
	[Serializable]
	public class LocalizedMenuLink
	{
		/// <summary>
		/// Language
		/// </summary>
		/// <value>The language.</value>
		public string Language { get; set; }
		/// <summary>
		/// Texte
		/// </summary>
		/// <value>The caption.</value>
		public string Caption { get; set; }
		/// <summary>
		/// Lien relatif
		/// </summary>
		/// <value>The link.</value>
		public string Link { get; set; }
	}
}
