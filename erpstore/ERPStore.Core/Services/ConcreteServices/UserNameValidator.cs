using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	public class UserNameValidator : System.IdentityModel.Selectors.UserNamePasswordValidator
	{
		public override void Validate(string userName, string password)
		{
			if (null == userName || null == password)
			{
				throw new ArgumentNullException();
			}
			// Passoire
		}
	}
}
