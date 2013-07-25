using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	[Serializable]
	public class ErrorReasonInfo
	{
		public string PropertyName { get; set; }
		public string Error { get; set; }
	}
}
