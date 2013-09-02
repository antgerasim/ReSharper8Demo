using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Panier de commande
	/// </summary>
	[Serializable]
	public abstract class CartBase
	{
		private List<CartItem> m_CartItemList;

		public CartBase()
		{
			CreationDate = DateTime.Now;
			m_CartItemList = new List<CartItem>();
			Code = Guid.NewGuid().ToString().Replace("-","").Substring(0,25);
		}

		/// <summary>
		/// Numéro interne du panier
		/// </summary>
		/// <value>The id.</value>
		public int? Id { get; set; }
		/// <summary>
		/// Code du panier généralement un GUID
		/// </summary>
		/// <value>The code.</value>
		public string Code { get; set; }
		/// <summary>
		/// Date de création du panier
		/// </summary>
		/// <value>The creation date.</value>
		public DateTime CreationDate { get; set; }
		/// <summary>
		/// Identifiant interne du client s'il est identifié
		/// </summary>
		/// <value>The customer id.</value>
		public int? CustomerId { get; set; }
		/// <summary>
		/// Identitifiant du visiteur
		/// </summary>
		/// <value>The visitor id.</value>
		public string VisitorId { get; set; }
		/// <summary>
		/// Retourne le nombre d'item dans le panier
		/// </summary>
		/// <value>The item count.</value>
		public int ItemCount
		{
			get
			{
				if (m_CartItemList == null)
				{
					return 0;
				}
				return m_CartItemList.Count();
			}
		}

		/// <summary>
		/// Liste des items du panier
		/// </summary>
		/// <value>The items.</value>
		public List<CartItem> Items
		{
			get
			{
				return m_CartItemList;
			}
		}

		/// <summary>
		/// Dernière page affichée avant d'arriver sur la page du panier
		/// </summary>
		/// <value>The last page.</value>
		public string LastPage { get; set; }
		/// <summary>
		/// Message à ajouter à la commande par l'utilisateur
		/// </summary>
		/// <value>The message.</value>
		public string Message { get; set; }
		/// <summary>
		/// L'utlisateur peut ajouter un code à lui pour retrouver plus tard celui ci dans la commande
		/// </summary>
		/// <value>The customer document reference.</value>
		public string CustomerDocumentReference { get; set; }
		/// <summary>
		/// Indique qu'il s'agit du panier en cours
		/// </summary>
		/// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
		public bool IsActive { get; set; }
		/// <summary>
		/// Document de conversion (Commande ou Devis)
		/// </summary>
		/// <value>The converted entity id.</value>
		public int? ConvertedEntityId { get; set; }
		/// <summary>
		/// Date de conversion du panier
		/// </summary>
		/// <value>The conversion date.</value>
		public DateTime? ConversionDate { get; set; }

		/// <summary>
		/// Nom de la source etant à l'origine de la création du panier
		/// </summary>
		/// <value>The name of the lead source.</value>
		public string LeadSourceName { get; set; }

		/// <summary>
		/// Identifiant de la source de creéation du panier
		/// </summary>
		/// <value>The lead source id.</value>
		public string LeadSourceId { get; set; }
	}
}