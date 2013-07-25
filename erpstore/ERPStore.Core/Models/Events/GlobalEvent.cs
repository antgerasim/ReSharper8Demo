using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models.Events
{
	/// <summary>
	/// Evenement global nommé
	/// </summary>
	public class GlobalEvent
	{
		/// <summary>
		/// Nom de l'evenement
		/// </summary>
		/// <value>The name of the event.</value>
		public string EventName { get; set; }
		/// <summary>
		/// Source de l'evenement
		/// </summary>
		/// <value>The source.</value>
		public object Source { get; set; }
	}
}
