using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	/// <summary>
	/// Gestion des coupons
	/// </summary>
	public interface IIncentiveService
	{
		/// <summary>
		/// Verification de l'utilisation du coupon et de son application
		/// sur le panier.
		/// </summary>
		/// <param name="cart">The cart.</param>
		/// <param name="couponCode">The coupon code.</param>
		/// <returns></returns>
		IEnumerable<Models.BrokenRule> ValidateUse(Models.OrderCart cart, string couponCode);
		/// <summary>
		/// Application du coupon sur le panier.
		/// </summary>
		/// <param name="cart">The cart.</param>
		/// <param name="couponCode">The coupon code.</param>
		void ProcessCoupon(Models.OrderCart cart, string couponCode);
		/// <summary>
		/// Retourne un coupon via son code.
		/// </summary>
		/// <param name="couponCode">The coupon code.</param>
		/// <returns></returns>
		Models.Coupon GetCoupon(string couponCode);
		/// <summary>
		/// Applique le coupon à la commande
		/// </summary>
		/// <param name="order">The order.</param>
		/// <param name="couponCode">The coupon code.</param>
		void ApplyCoupon(Models.ISaleDocument order, string couponCode);
	}
}
