using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	[Serializable]
	public class Comment
	{
		public string Message { get; set; }
		public DateTime CreationDate { get; set; }
	}
}
