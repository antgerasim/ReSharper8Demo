using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Moq;
using ERPStore.Models;
using System.Web.Routing;
using NUnit.Framework;
using System.Linq.Expressions;

namespace ERPStore.Tests.Controllers
{
	public static class ControllerExtensions
	{
		public static void SetupControllerContext(this Controller controller, System.Web.HttpContextBase httpContext)
		{
			// var httpContext = MockHttpContextFactory.CreateHttpContext();
			var context = new ControllerContext(httpContext, new RouteData(), controller);
			controller.ControllerContext = context;
		}

		//public static void SetupAuthenticatedControllerContext(this Controller controller, System.Web.HttpContextBase httpContext, ERPStore.Models.UserPrincipal principal)
		//{
		//    // var httpContext = MockHttpContextFactory.CreateHttpContext(principal);
		//    var context = new ControllerContext(httpContext, new RouteData(), controller);
		//    controller.ControllerContext = context;
		//}

		//public static void SetupControllerContextWithForm(this Controller controller, System.Web.HttpContextBase httpContext, FormCollection form)
		//{
		//    // var mockContext = MockHttpContextFactory.CreateHttpContext();
		//    mockContext.Setup(i => i.Request.Form).Returns(form);
		//    var context = new ControllerContext(mockContext.Object, new RouteData(), controller);
		//    controller.ControllerContext = context;
		//}
	}
}
