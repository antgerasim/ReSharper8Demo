using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ERPStore
{
	public static class ModelStateExtensions
	{
		public static void AddModelErrors(this ModelStateDictionary modelState, IEnumerable<Models.RuleViolation> errors)
		{
			foreach (Models.RuleViolation issue in errors)
			{
				modelState.AddModelError(issue.PropertyName, issue.ErrorMessage);
			}
		}

		public static void AddModelErrors(this ModelStateDictionary modelState, List<Models.BrokenRule> brokenRules)
		{
			if (brokenRules.IsNotNullOrEmpty())
			{
				foreach (var item in brokenRules)
				{
					foreach (var error in item.ErrorList)
					{
						modelState.AddModelError(item.PropertyName, error);
					}
				}
			}
		}

		/// <summary>
		/// Retourne la liste de toutes les erreurs de validation d'un formulaire
		/// </summary>
		/// <param name="modelState">State of the model.</param>
		/// <returns></returns>
		public static IEnumerable<string> GetAllErrors(this ModelStateDictionary modelState)
		{
			var errors = from state in modelState
						 from error in state.Value.Errors
						 where state.Value.Errors.Count > 0
						 select error.ErrorMessage;

			return errors;
		}

		public static IList<Models.ErrorReasonInfo> GetAllAjaxErrors(this ModelStateDictionary modelState)
		{
			var errors = new List<Models.ErrorReasonInfo>();

			foreach (var state in modelState)
			{
				if (state.Value.Errors.Count > 0)
				{
					var err = new Models.ErrorReasonInfo { PropertyName = state.Key };
					foreach (var error in state.Value.Errors)
					{
						err.Error += error.ErrorMessage;
					}

					errors.Add(err);
				}
			}

			return errors;
		}

	}
}
