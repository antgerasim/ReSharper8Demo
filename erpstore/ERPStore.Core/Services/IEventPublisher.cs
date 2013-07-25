using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	/// <summary>
	/// Publication d'un evenement pour l'event broker
	/// 
	/// source :
	/// http://elegantcode.com/2010/01/06/event-driven-architecture-publishing-events-using-an-ioc-container/
	/// </summary>
	public interface IEventPublisher
	{
		/// <summary>
		/// Publication de l'evenement spécifié
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="eventMessage">The event message.</param>
		void Publish<T>(T eventMessage);
	}
}
