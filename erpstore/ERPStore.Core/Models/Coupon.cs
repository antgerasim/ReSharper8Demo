using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Coupon de reduction à appliquer sur le panier
	/// </summary>
	[Serializable]
	public class Coupon : ICloneable
	{
		public Coupon()
		{
			UsedCount = 0;
		}
		/// <summary>
		/// Code du coupon.
		/// </summary>
		/// <value>The code.</value>
		public string Code { get; set; }
		/// <summary>
		/// Description du coupon
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; }
		/// <summary>
		/// Date d'expiration du coupon.
		/// </summary>
		/// <value>The expiration date.</value>
		public DateTime? ExpirationDate { get; set; }
		/// <summary>
		/// Indique si le coupon a expiré.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is expired; otherwise, <c>false</c>.
		/// </value>
		public bool IsExpired 
		{
			get
			{
				if (!ExpirationDate.HasValue)
				{
					return false;
				}
				if (ExpirationDate.Value < DateTime.Today)
				{
					return true;
				}
				return false;
			}
		}
		/// <summary>
		/// Nombre minimum d'items pour appliquer la reduction.
		/// </summary>
		/// <value>The minimum items.</value>
		public int? MinimumItemCount { get; set; }
		/// <summary>
		/// Valeur minimal d'achat pour appliquer la reduction
		/// </summary>
		/// <value>The mininum purchase.</value>
		public decimal? MininumPurchase { get; set; }
		/// <summary>
		/// Nombre maximal de fois ou un coupon peut etre utilisé.
		/// </summary>
		/// <value>The maximum use count.</value>
		public int? MaximumUseCount { get; set; }
		/// <summary>
		/// Nombre de fois ou le coupon à été utilisé.
		/// </summary>
		/// <value>The used count.</value>
		public int UsedCount { get; set; }

		/// <summary>
		/// Liste des produits sur lesquels peut s'appliquer la remise
		/// </summary>
		/// <value>The product code list.</value>
		public List<int> ProductIdList { get; set; }

		/// <summary>
		/// Categorie de produit sur laquelle peut s'appliquer la remise.
		/// </summary>
		/// <value>The product category id.</value>
		public List<int> ProductCategoryIdList { get; set; }

		/// <summary>
		/// Marque sur laquelle peut s'appliquer la remise.
		/// </summary>
		/// <value>The brand id.</value>
		public List<int> BrandIdList { get; set; }

		/// <summary>
		/// Le code du vendeur associé au coupon
		/// </summary>
		/// <value>The vendor code.</value>
		public string VendorCode { get; set; }

		/// <summary>
		/// Type de coupon
		/// </summary>
		/// <value>The type.</value>
		public CouponType Type { get; set; }

		#region ICloneable Members

		public object Clone()
		{
			return this.MemberwiseClone();
		}

		#endregion
	}
}
