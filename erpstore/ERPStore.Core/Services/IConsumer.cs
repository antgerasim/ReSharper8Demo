using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	/// <summary>
	/// Interface pour les objets devant etre notifié par un evenement
	/// 
	/// source :
	/// http://elegantcode.com/2010/01/06/event-driven-architecture-publishing-events-using-an-ioc-container/
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IConsumer<T>
	{
		/// <summary>
		/// Notifie l'objet qu'un evement vient d'avoir lieu
		/// </summary>
		/// <param name="eventMessage">The event message.</param>
		void Handle(T eventMessage);
	}
}
