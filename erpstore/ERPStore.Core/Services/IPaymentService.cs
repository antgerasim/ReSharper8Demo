using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	/// <summary>
	/// Interface pour les services de paiement
	/// </summary>
	public interface IPaymentService : Models.IPayment
	{
		/// <summary>
		/// Indique si le mode de reglement est autorisé
		/// </summary>
		/// <param name="cart">The cart.</param>
		/// <param name="principal">The principal.</param>
		/// <returns>
		/// 	<c>true</c> if [is allowed for] [the specified cart]; otherwise, <c>false</c>.
		/// </returns>
		bool IsAllowedFor(Models.OrderCart cart, Models.UserPrincipal principal);
	}
}
