using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Repositories
{
	public class NullCouponRepository : ICouponRepository
	{
		#region ICouponRepository Members

		public ERPStore.Models.Coupon GetByCode(string code)
		{
			return null;
		}

		#endregion
	}
}
