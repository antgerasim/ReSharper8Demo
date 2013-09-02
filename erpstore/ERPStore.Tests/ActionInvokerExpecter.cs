﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;

namespace ERPStore.Tests
{
	/// <summary>
	/// Used for testing. When the action is executed the Result type should be match 
	/// with the <see cref="TResult"/> generic parameter.
	/// </summary>
	/// <typeparam name="TResult">Expected Result type.</typeparam>
	public class ActionInvokerExpecter<TResult> : ControllerActionInvoker 
		where TResult : ActionResult
	{
		/// <summary>
		/// The point here is just to invoke que Action and do nothing with the result.
		/// </summary>
		/// <param name="controllerContext"></param>
		/// <param name="actionResult"></param>
		protected override void InvokeActionResult(ControllerContext controllerContext, ActionResult actionResult)
		{
			Assert.IsInstanceOfType(typeof(TResult), actionResult);
		}
	}
}
