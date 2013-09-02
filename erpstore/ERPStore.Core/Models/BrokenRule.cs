using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	[Serializable]
	public class BrokenRule
	{
		public BrokenRule()
		{
			ErrorList = new List<string>();
		}
		public string PropertyName { get; set; }
		public List<string> ErrorList { get; private set; }
	}
}
