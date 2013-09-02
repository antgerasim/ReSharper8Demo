using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	[Serializable]
	class WorkflowException : ApplicationException
	{
		public WorkflowException(Exception actualException)
			: base(actualException.Message, actualException)
		{

		}
	}
}
