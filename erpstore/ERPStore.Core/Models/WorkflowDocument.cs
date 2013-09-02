using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Interface pour la gestion des document de vente
	/// pour l'affichage du workflow
	/// </summary>
	[Serializable]
	public class WorkflowDocument
	{
		/// <summary>
		/// Identifiant interne
		/// </summary>
		/// <value>The id.</value>
		public int Id { get; set;  }
		/// <summary>
		/// Code public du document
		/// </summary>
		/// <value>The code.</value>
		public string Code { get; set; }
		/// <summary>
		/// Date de création du document
		/// </summary>
		/// <value>The creation date.</value>
		public DateTime CreationDate { get; set; }
		/// <summary>
		/// Titre du document
		/// </summary>
		/// <value>The title.</value>
		public string Title { get; set; }
		/// <summary>
		/// Le document est selectionné
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
		/// </value>
		public bool IsSelected { get; set; }
		/// <summary>
		/// Type de document
		/// </summary>
		/// <value>The type.</value>
		public SaleDocumentType Type { get; set; }
		/// <summary>
		/// Lien vers le document
		/// </summary>
		/// <value>The URL.</value>
		public string Url { get; set; }
		/// <summary>
		/// Document attaché par defaut
		/// </summary>
		/// <value>The default document.</value>
		public string DownloadUrl { get; set; }
	}
}
