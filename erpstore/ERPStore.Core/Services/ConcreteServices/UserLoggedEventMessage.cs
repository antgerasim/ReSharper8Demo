using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	internal class UserLoggedEventMessage : ERPStore.Services.IConsumer<ERPStore.Models.Events.UserAuthenticatedEvent>
	{
		public UserLoggedEventMessage(ICartService cartService)
		{
			this.CartService = cartService;
		}

		protected ICartService CartService { get; private set; }

		#region IConsumer<ProductSearchEvent> Members

		public void Handle(ERPStore.Models.Events.UserAuthenticatedEvent eventMessage)
		{
			CartService.SetCurrentCart(eventMessage.UserId, eventMessage.VisitorId);
		}

		#endregion
	}
}
