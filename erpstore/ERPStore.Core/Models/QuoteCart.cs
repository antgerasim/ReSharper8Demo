using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	[Serializable]
	public class QuoteCart : CartBase
	{
		public QuoteCart()
		{
			IsSent = false;
            PresentationId = 1; // Homme par defaut
		}

		/// <summary>
		/// Nom complet de l'utilisateur
		/// </summary>
		/// <value>The full name of the user.</value>
		public string UserFullName 
		{
			get
			{
				var presentation = string.Empty;
				if (PresentationId == 1)
				{
					presentation = "M.";
				}
				else if (PresentationId == 2)
				{
					presentation = "Mlle";
				}
				else if (PresentationId == 3)
				{
					presentation = "Mme";
				}

				if (FirstName.IsNullOrTrimmedEmpty())
				{
					return string.Format("{0} {1}", presentation, LastName);
				}

				return string.Format("{0} {1} {2}", presentation, FirstName, LastName);
			}
		}

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>The email.</value>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the phone number.
		/// </summary>
		/// <value>The phone number.</value>
		public string PhoneNumber { get; set; }

		/// <summary>
		/// Gets or sets the fax number.
		/// </summary>
		/// <value>The fax number.</value>
		public string FaxNumber { get; set; }

		/// <summary>
		/// Indique si le panier de type devis vient d'etre envoyé
		/// </summary>
		/// <value><c>true</c> if this instance is sent; otherwise, <c>false</c>.</value>
		public bool IsSent { get; set; }

		/// <summary>
		/// Nom de la société 
		/// </summary>
		/// <value>The name of the corporate.</value>
		public string CorporateName { get; set; }

		/// <summary>
		/// Indique la présentation de la personne (M, Mme, Mlle)
		/// </summary>
		/// <value>The presentation id.</value>
		public int PresentationId { get; set; }
		/// <summary>
		/// Prénom
		/// </summary>
		/// <value>The first name.</value>
		public string FirstName { get; set; }
		/// <summary>
		/// Nom de famille
		/// </summary>
		/// <value>The last name.</value>
		public string LastName { get; set; }

        /// <summary>
        /// Pays de livraison
        /// </summary>
        /// <value>The country.</value>
        public Country Country { get; set; }

        /// <summary>
        /// Code postal
        /// </summary>
        /// <value>The postal code.</value>
        public string ZipCode { get; set; }


	}
}
