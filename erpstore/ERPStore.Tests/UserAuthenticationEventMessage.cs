using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using System.Web.Security;

namespace ERPStore.Tests
{
	public class UserAuthenticationEventMessage : ERPStore.Services.IConsumer<ERPStore.Models.Events.UserAuthenticatedEvent>
	{
		public UserAuthenticationEventMessage()
		{
		}

		[Dependency]
		public ERPStore.Services.IAccountService AccountService { get; set; }

		public System.Web.Mvc.Controller Controller { get; set; }

		#region IConsumer<UserAuthenticatedEvent> Members

		public void Handle(ERPStore.Models.Events.UserAuthenticatedEvent eventMessage)
		{
			var user = AccountService.GetUserById(eventMessage.UserId);
			var ticket = new FormsAuthenticationTicket("mock", false, 100);
			var id = new FormsIdentity(ticket);
			var userPrincipal = new ERPStore.Models.UserPrincipal(id, Guid.NewGuid().ToString());
			userPrincipal.CurrentUser = user;
			user.Roles.Add("customer");
			Controller.HttpContext.User = userPrincipal;
		}

		#endregion
	}
}
