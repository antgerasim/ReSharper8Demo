using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ERPStore.Models
{
	[Serializable]
	public class ContactInfo 
	{
		public string FullName { get; set; }

		public string CorporateName { get; set; }

		public string Email { get; set; }

		public string PhoneNumber { get; set; }

		public string Message { get; set; }

    }
}
