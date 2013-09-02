using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	/// <summary>
	/// Service de notification pour les plugins
	/// 
	/// source :
	/// http://elegantcode.com/2010/01/06/event-driven-architecture-publishing-events-using-an-ioc-container/
	/// </summary>
	public interface IEventSubscriptionService
	{
		/// <summary>
		/// Retourne tous les messages qui ont souscrit au type d'evenement
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		IEnumerable<IConsumer<T>> GetSubscriptions<T>();
	}
}
