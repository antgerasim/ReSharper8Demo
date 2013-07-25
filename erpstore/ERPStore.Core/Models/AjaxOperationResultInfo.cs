using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	public class AjaxOperationResultInfo
	{
		public AjaxOperationResultInfo()
		{
			Errors = new List<ErrorReasonInfo>();
		}

		public bool Successfull { get; set; }

		public string Message { get; set; }

		public IList<ErrorReasonInfo> Errors { get; set; }
	}
}
