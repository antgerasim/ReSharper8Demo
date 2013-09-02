using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.Unity;

namespace ERPStore.Services
{
	/// <summary>
	/// Service de gestion des souscriptions à des evenement "globaux"
	/// 
	/// source :
	/// http://elegantcode.com/2010/01/06/event-driven-architecture-publishing-events-using-an-ioc-container/
	/// </summary>
	public class EventSubscriptionService : IEventSubscriptionService
	{
		public EventSubscriptionService(Microsoft.Practices.Unity.IUnityContainer container)
		{
			this.Container = container;
		}

		protected Microsoft.Practices.Unity.IUnityContainer Container { get; private set; }

		public IEnumerable<IConsumer<T>> GetSubscriptions<T>()
		{
			#region Debug

			// var consumers = new List<object>();
			//Container.Registrations
			//.Where(x => x.RegisteredType.IsGenericType)
			//.Where(x => x.RegisteredType.GetGenericTypeDefinition() == typeof(IConsumer<>))
			//.ToList();

			//foreach (var item in Container.Registrations)
			//{
			//    var interfaces = item.RegisteredType.GetInterfaces();
			//    if (interfaces.IsNullOrEmpty())
			//    {
			//        continue;
			//    }
			//    foreach (var itrf in interfaces)
			//    {
			//        if (itrf.FullName == consumerInterfaceName)
			//        {
			//            consumers.Add(Container.Resolve(item.RegisteredType));
			//        }
			//    }
			//}

			#endregion

            var consumerInterfaceName = typeof(IConsumer<T>).FullName;
            var consumers = from type in Container.Registrations.Select(i => i.RegisteredType)
                            from itf in type.GetInterfaces()
                            where itf.FullName.Equals(consumerInterfaceName, StringComparison.InvariantCultureIgnoreCase)
                            select Container.Resolve(type);

            var result = consumers.Cast<IConsumer<T>>();
            // etrange ne fonctionne pas :(
			// var result = Container.ResolveAll<IConsumer<T>>();

			return result;
		}
	}
}
