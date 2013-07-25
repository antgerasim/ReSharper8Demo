using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	public interface IPayment
	{
		/// <summary>
		/// Nom de mode de paiement
		/// </summary>
		/// <value>The name of the payment mode.</value>
		string Name { get; }
		/// <summary>
		/// Nom de la vue utilisée pour l'affichage de la confirmation
		/// </summary>
		/// <value>The name of the confirmation view.</value>
		string ConfirmationViewName { get; }
		/// <summary>
		/// Nom de la vue utilisée pour confirmation de prise de la commande
		/// </summary>
		/// <value>The name of the finalized view.</value>
		string FinalizedViewName { get; }
		/// <summary>
		/// Texte descriptif du mode de paiement
		/// </summary>
		/// <value>The description.</value>
		string Description { get; }
		/// <summary>
		/// Emplacement du logo
		/// </summary>
		/// <value>The picto URL.</value>
		string PictoUrl { get; }
		/// <summary>
		/// Nom de la route pour la confirmation
		/// </summary>
		/// <value>The name of the confirmation route.</value>
		string ConfirmationRouteName { get; }
		/// <summary>
		/// Nom de la route pour la confirmation de la prise de commande
		/// </summary>
		/// <value>The name of the finalized route.</value>
		string FinalizedRouteName { get; }
	}

	public class Payment : IPayment
	{
		/// <summary>
		/// Nom de mode de paiement
		/// </summary>
		/// <value>The name of the payment mode.</value>
		public string Name { get; set; }
		/// <summary>
		/// Nom de la vue utilisée pour l'affichage de la confirmation
		/// </summary>
		/// <value>The name of the confirmation view.</value>
		public string ConfirmationViewName { get; set; }
		/// <summary>
		/// Nom de la vue utilisée pour confirmation de prise de la commande
		/// </summary>
		/// <value>The name of the finalized view.</value>
		public string FinalizedViewName { get; set; }
		/// <summary>
		/// Texte descriptif du mode de paiement
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; }
		/// <summary>
		/// Emplacement du logo
		/// </summary>
		/// <value>The picto URL.</value>
		public string PictoUrl { get; set; }
		/// <summary>
		/// Nom de la route pour la confirmation
		/// </summary>
		/// <value>The name of the confirmation route.</value>
		public string ConfirmationRouteName { get; set; }
		/// <summary>
		/// Nom de la route pour la confirmation de la prise de commande
		/// </summary>
		/// <value>The name of the finalized route.</value>
		public string FinalizedRouteName { get; set; }
		/// <summary>
		/// Indique si l'element est déjà selectionné
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
		/// </value>
		public bool IsSelected { get; set; }
	}
}
