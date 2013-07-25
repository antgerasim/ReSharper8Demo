using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	/// <summary>
	/// Service de gestion des coupons de reduction
	/// </summary>
	public class IncentiveService : IIncentiveService
	{
		public IncentiveService(Logging.ILogger logger,
			Repositories.ICouponRepository couponRepository,
			IAccountService accountService
			)
		{
			this.CouponRepository = couponRepository;
			this.Logger = logger;
			this.AccountService = accountService;
		}

		#region Depending Services

		protected Repositories.ICouponRepository CouponRepository { get; private set; }

		protected Logging.ILogger Logger { get; private set; }

		protected IAccountService AccountService { get; private set; }

		#endregion

		#region IIncentiveService Members

		public virtual IEnumerable<Models.BrokenRule> ValidateUse(Models.OrderCart cart, string couponCode)
		{
			var result = new List<Models.BrokenRule>();
			Models.Coupon existingCoupon = null;
			if (cart.Coupon != null)
			{
				existingCoupon = (Models.Coupon)cart.Coupon.Clone();
			}
			Models.Coupon coupon = existingCoupon;
			if (coupon == null)
			{
				coupon = GetCoupon(couponCode);
				if (coupon == null)
				{
					cart.Coupon = existingCoupon;
					return null;
				}
			}
			cart.Coupon = null;
			if (coupon.ExpirationDate.HasValue
				&& coupon.ExpirationDate.Value <= DateTime.Today)
			{
				result.AddBrokenRule("_Form",string.Format("La date d'expiration ({0:dddd dd MMMM yyyy}) est atteinte pour beneficier d'une reduction", coupon.ExpirationDate.Value));
			}
			if (coupon.MininumPurchase.HasValue
				&& cart.GrandTotal <= coupon.MininumPurchase.Value)
			{
				result.AddBrokenRule("_Form", string.Format("Le montant total n'est pas atteint ({0}) pour beneficier d'une reduction", coupon.MininumPurchase.Value.ToCurrency()));
			}
			if (coupon.MinimumItemCount.HasValue
				&& cart.ItemCount <= coupon.MinimumItemCount)
			{
				result.AddBrokenRule("_Form",string.Format("Le nombre de lignes n'est pas atteint ({0}) pour beneficier d'une reduction", coupon.MinimumItemCount.Value));
			}
			if (coupon.MaximumUseCount.HasValue
				&& coupon.UsedCount >= coupon.MaximumUseCount)
			{
				result.AddBrokenRule( "_Form",string.Format("Ce coupon a atteint son nombre maximal d'utilisation ({0}) pour beneficier d'une reduction", coupon.MaximumUseCount.Value));
			}
			switch (coupon.Type)
			{
				case ERPStore.Models.CouponType.VendorCode:
					break;
				case ERPStore.Models.CouponType.PercentOfOrder:
					break;
				case ERPStore.Models.CouponType.AmountOfOrder:
					break;
				case ERPStore.Models.CouponType.FreeTransport:
					break;
				case ERPStore.Models.CouponType.PercentOfProduct:
					break;
				case ERPStore.Models.CouponType.AmoutOfProduct:
					break;
				case ERPStore.Models.CouponType.PercentOfProductCategory:
					break;
				case ERPStore.Models.CouponType.AmountOfProductCategory:
					break;
				case ERPStore.Models.CouponType.PercentOfBrand:
					break;
				case ERPStore.Models.CouponType.AmountOfBrand:
					break;
				default:
					break;
			}
			cart.Coupon = existingCoupon;
			return result;
		}

		public virtual void ProcessCoupon(Models.OrderCart cart, string couponCode)
		{
			var coupon = GetCoupon(couponCode);
			if (couponCode == null)
			{
				return;
			}
			cart.Coupon = coupon;
		}

		public virtual ERPStore.Models.Coupon GetCoupon(string couponCode)
		{
			return CouponRepository.GetByCode(couponCode);
		}

		public virtual void ApplyCoupon(Models.ISaleDocument order, string couponCode)
		{

		}

		#endregion
	}
}
