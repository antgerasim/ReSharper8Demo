using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Repositories
{
	public interface ICouponRepository
	{
		Models.Coupon GetByCode(string code);
	}
}
