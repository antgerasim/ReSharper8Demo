using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ERPStore.Models
{
	/// <summary>
	/// Traduction des textes dans les pages
	/// </summary>
	[Serializable]
	public class ContentLocalization
	{
		/// <summary>
		/// Indique le fichier dans lequel se trouve le texte à traduire
		/// </summary>
		/// <value>The path.</value>
		public string Path { get; set; }
		/// <summary>
		/// Indique l'identifiant de traduction dans la vue 
		/// </summary>
		/// <remarks>
		/// La vue peut etre partielle
		/// </remarks>
		/// <value>The key.</value>
		public string Key { get; set; }
		/// <summary>
		/// La langue de traduction
		/// </summary>
		/// <value>The language.</value>
		public string Language { get; set; }
		/// <summary>
		/// La valeur traduite
		/// </summary>
		/// <value>The value.</value>
		public string Value { get; set; }
	}
}
