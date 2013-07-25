using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Represente un element 
	/// </summary>
	[Serializable]
	public class Media : ICloneable
	{
		public Media()
		{
			IsMissing = false;
		}

		/// <summary>
		/// Identifiant interne
		/// </summary>
		/// <value>The id.</value>
		public string Id { get; set; }
		/// <summary>
		/// Type mime du document
		/// </summary>
		/// <value>The type of the MIME.</value>
		public string MimeType { get; set; }
		/// <summary>
		/// Url relative
		/// </summary>
		/// <value>The URL.</value>
		public string Url { get; set; }
		///// <summary>
		///// Contenu du document
		///// </summary>
		///// <value>The content.</value>
		//public byte[] Content { get; set; }
		/// <summary>
		/// Date de la dernière mise à jour
		/// </summary>
		/// <value>The last update.</value>
		public DateTime LastUpdate { get; set; }
		/// <summary>
		/// Lien externe dans le cas d'une url
		/// </summary>
		/// <value>The external URL.</value>
		public string ExternalUrl { get; set; }
		/// <summary>
		/// Lien relatif vers l'icone de l'url
		/// </summary>
		/// <value>The icone URL.</value>
		public string IconeSrc { get; set; }
		/// <summary>
		/// Nom du fichier
		/// </summary>
		/// <value>The name of the file.</value>
		public string FileName { get; set; }
		/// <summary>
		/// Gets or sets the length.
		/// </summary>
		/// <value>The length.</value>
		public long Length { get; set; }
		/// <summary>
		/// Le media est innaccessible 
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is missing; otherwise, <c>false</c>.
		/// </value>
		public bool IsMissing { get; set; }

		/// <summary>
		/// Taille du fichier en Ko
		/// </summary>
		/// <value>The size of the file.</value>
		public string FileSize
		{
			get
			{
				return string.Format("{0:#,##0.00} Ko", Length / 1024);
			}
		}

		#region ICloneable Members

		public object Clone()
		{
			return this.MemberwiseClone();
		}

		#endregion
	}
}
