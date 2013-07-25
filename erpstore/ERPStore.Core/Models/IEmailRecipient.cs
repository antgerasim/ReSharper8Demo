using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Definition des propriétés d'un user pour exploiter son Email
	/// </summary>
	public interface IEmailRecipient
	{
		/// <summary>
		/// Identifiant interne du user
		/// </summary>
		/// <value>The id.</value>
		int Id { get;  }
		/// <summary>
		/// Nom complet
		/// </summary>
		/// <value>The full name.</value>
		string FullName { get; }
		/// <summary>
		/// Adresse email
		/// </summary>
		/// <value>The email.</value>
		string Email { get;  }
		/// <summary>
		/// Presentation (sexe)
		/// </summary>
		/// <value>The presentation.</value>
		UserPresentation Presentation { get; }
		/// <summary>
		/// Statut de verification de l'email
		/// </summary>
		/// <value>The email verification status.</value>
		EmailVerificationStatus EmailVerificationStatus { get; }
		/// <summary>
		/// Nom de la société eventuel à laquelle est rattaché le user
		/// </summary>
		/// <value>The name of the corporate.</value>
		string CorporateName { get; }
	}
}
