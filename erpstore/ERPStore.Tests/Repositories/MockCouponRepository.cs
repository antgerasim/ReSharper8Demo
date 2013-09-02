using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Tests.Repositories
{
	public class MockCouponRepository : ERPStore.Repositories.ICouponRepository
	{
		public IList<ERPStore.Models.Coupon> m_List;

		public MockCouponRepository()
		{
			m_List = new List<ERPStore.Models.Coupon>();

			var vendorCoupon = new ERPStore.Models.Coupon();
			vendorCoupon.Code = "Vendor1";
			vendorCoupon.Type = ERPStore.Models.CouponType.VendorCode;

			m_List.Add(vendorCoupon);
		}


		#region ICouponRepository Members

		public ERPStore.Models.Coupon GetByCode(string code)
		{
			var coupon = m_List.SingleOrDefault(i => i.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
			return coupon;
		}

		#endregion
	}
}
