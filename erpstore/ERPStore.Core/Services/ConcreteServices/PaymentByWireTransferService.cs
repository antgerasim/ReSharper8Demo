using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace ERPStore.Services
{
	public class PaymentByWireTransferService : IPaymentService
	{
		private NameValueCollection m_AppSettings = null;
		public NameValueCollection AppSettings
		{
			get
			{
				if (m_AppSettings == null)
				{
					m_AppSettings = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection("paymentByWireTransferSettings");
					if (m_AppSettings == null)
					{
						m_AppSettings = new NameValueCollection();
					}
				}
				return m_AppSettings;
			}
		}

		#region IPaymentService Members

		public string Name
		{
			get 
			{
				return "wiretransfer";
			}
		}

		public string ConfirmationViewName
		{
			get 
			{
				return AppSettings["confirmationViewName"] ?? "wiretransfer-confirmation";
			}
		}

		public string FinalizedViewName
		{
			get
			{
				return AppSettings["finalizedViewName"] ?? "wiretransfer-finalized";
			}
		}

		public string Description
		{
			get 
			{
				return AppSettings["description"] ?? "Reglement par virement bancaire";
			}
		}

		public string PictoUrl
		{
			get 
			{
				return AppSettings["pictourl"] ?? "/content/images/icos/ico_wiretransfer.png";
			}
		}

		public string ConfirmationRouteName
		{
			get
			{
				return ERPStoreRoutes.CHECKOUT_CONFIRMATION;
			}
		}

		public string FinalizedRouteName
		{
			get
			{
				return ERPStoreRoutes.CHECKOUT_FINALIZED;
			}
		}

		public virtual bool IsAllowedFor(ERPStore.Models.OrderCart cart, ERPStore.Models.UserPrincipal principal)
		{
			return true;
		}

		#endregion
	}
}
