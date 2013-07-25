using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models.Events
{
	public class Error404Event
	{
		public string Url { get; set; }
		public DateTime Date { get; set; }
		public string IP { get; set; }
		public string UserAgent { get; set; }
		public Uri Referer { get; set; }
		public string ApplicationPath { get; set; }
		public string Method { get; set; }
		public string Cookie { get; set; }
	}
}
