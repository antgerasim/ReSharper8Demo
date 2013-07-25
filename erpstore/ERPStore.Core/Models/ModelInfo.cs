using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	public class ModelInfo
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public DateTime? CreationDate { get; set; }
	}
}
