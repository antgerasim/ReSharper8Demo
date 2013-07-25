using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using NUnit.Framework;

namespace ERPStore.Tests
{
	public class TestControllerActionInvoker<Result>
		: ERPStore.UnityActionInvoker where Result : ActionResult
	{
		public TestControllerActionInvoker(Microsoft.Practices.Unity.IUnityContainer container)
			: base(container)
		{

		}

		public override bool InvokeAction(ControllerContext controllerContext, string actionName)
		{
			bool result = base.InvokeAction(controllerContext, actionName);
			return result;
		}

		protected override void InvokeActionResult(ControllerContext controllerContext, ActionResult actionResult)
		{
			Assert.IsInstanceOfType(typeof(Result), actionResult);
		}
	}
}
