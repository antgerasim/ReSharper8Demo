using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	public class EventPublisher : IEventPublisher
	{
		public EventPublisher(IEventSubscriptionService subscriptionService
			, Logging.ILogger logger)
		{
			this.SubscriptionService = subscriptionService;
			this.Logger = logger;
		}

		protected IEventSubscriptionService SubscriptionService { get; private set; }
		protected Logging.ILogger Logger { get; private set; }


		public void Publish<T>(T eventMessage)
		{
			var subscriptions = SubscriptionService.GetSubscriptions<T>();
			if (subscriptions.IsNullOrEmpty())
			{
				return;
			}

			foreach (var subscription in subscriptions)
			{
				PublishToConsumer(subscription, eventMessage);
			}
		}

		private void PublishToConsumer<T>(IConsumer<T> x, T eventMessage)
		{
			try
			{
				x.Handle(eventMessage);
			}
			catch (Exception ex)
			{
				Logger.Error(ex);
			}
			finally
			{
				var instance = x as IDisposable;
				if (instance != null)
				{
					instance.Dispose();
				}
			}
		}
	}
}
