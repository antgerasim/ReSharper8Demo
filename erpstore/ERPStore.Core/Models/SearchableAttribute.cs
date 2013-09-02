using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class SearchableAttribute : Attribute
	{
		public SearchableAttribute()
		{

		}

		public SearchableAttribute(int level)
		{
			this.Level = level;
		}

		public int Level { get; set; }
	}
}
