using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Image inaccessible
	/// </summary>
	[Serializable]
	public class BrokenImage
	{
		public BrokenImage()
		{
			this.FailedMessages = new Dictionary<int, string>();
		}
		/// <summary>
		/// Identifiant externe de l'image
		/// </summary>
		/// <value>The document id.</value>
		public string DocumentId { get; set; }
		/// <summary>
		/// Chemin complet vers le fichier
		/// </summary>
		/// <value>The full name of the file.</value>
		public string FullFileName { get; set; }
		/// <summary>
		/// Adresse externe de l'image
		/// </summary>
		/// <value>The URL.</value>
		public string Url { get; set; }
		/// <summary>
		/// Raison de l'echec
		/// </summary>
		/// <value>The failed message.</value>
		public Dictionary<int, string> FailedMessages { get; private set; }
		/// <summary>
		/// Nombre de demande de l'image
		/// </summary>
		/// <value>The count.</value>
		public int HitCount { get; set; }
		/// <summary>
		/// Gets or sets the product id.
		/// </summary>
		/// <value>The product id.</value>
		public int? ProductId { get; set; }
	}
}
