using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	public class CacheEntry
	{
		internal CacheEntry()
		{
		}

		public string Key { get; set; }
		public object Value { get; set; }
		public DateTime ExpirationDate { get; set; }
	}
}
