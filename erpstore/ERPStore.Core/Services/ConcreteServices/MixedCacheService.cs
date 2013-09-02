using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using System.Timers;

using ERPStore;

namespace ERPStore.Services
{
	public class MixedCacheService : ERPStore.Services.ICacheService
	{
		private List<CacheEntry> m_CachedList;
		Timer m_Timer;

		public MixedCacheService()
		{
			if (System.Web.HttpContext.Current == null)
			{
				m_CachedList = new List<CacheEntry>();
				m_Timer = new Timer(1000 * 60); // Toutes les 60 secondes
				m_Timer.Elapsed += new ElapsedEventHandler(m_Timer_Elapsed);
				m_Timer.Start();
			}
		}
		
		// [Dependency]
		// public Logging.ILogger Logger { get; set; }

		void m_Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			m_CachedList.RemoveAll(i => i.ExpirationDate > e.SignalTime);
		}

		#region ICacheService Members

		public int ItemCount
		{
			get
			{
				return m_CachedList.Count;
			}
		}

		public object this[string key]
		{
			get 
			{
				if (System.Web.HttpContext.Current == null)
				{
					var item = m_CachedList.SingleOrDefault(i => i.Key == key);
					if (item != null)
					{
						return item.Value;
					}
					return null;
				}
				return System.Web.HttpContext.Current.Cache[key];
			}
		}

		public void Add(string key, object value, DateTime absoluteExpiration)
		{
			if (System.Web.HttpContext.Current == null)
			{
				var cacheEntry = m_CachedList.SingleOrDefault(i => i.Key == key);

				if (cacheEntry == null)
				{
					m_CachedList.Add(new CacheEntry()
					{
						Key = key,
						Value = value,
						ExpirationDate = absoluteExpiration
					});
				}
				else
				{
					cacheEntry.Value = value;
					cacheEntry.ExpirationDate = absoluteExpiration;
				}
				return;
			}

			System.Web.HttpContext.Current.Cache.Insert(key, value, null, absoluteExpiration, TimeSpan.Zero);
		}

		public object Get(string key)
		{
			return this[key];
		}

		public void Remove(string key)
		{
			if (System.Web.HttpContext.Current == null)
			{
				var cacheEntry = m_CachedList.SingleOrDefault(i => i.Key == key);
				if (cacheEntry != null)
				{
					m_CachedList.Remove(cacheEntry);
				}
				return;
			}

			System.Web.HttpContext.Current.Cache.Remove(key);
		}

		public void ClearAll()
		{
			if (System.Web.HttpContext.Current != null)
			{
				foreach (System.Collections.DictionaryEntry item in System.Web.HttpContext.Current.Cache)
				{
					System.Web.HttpContext.Current.Cache.Remove(item.Key.ToString());
				}
			}
			else
			{
				m_CachedList.Clear();
			}
		}

		public T FirstOrDefault<T>(Func<T, bool> predicate)
		{
			if (System.Web.HttpContext.Current != null)
			{
				var list = m_CachedList.Where(i => i.Value.GetType() == typeof(T)).Select(i => i.Value).Cast<T>();
				return list.FirstOrDefault(predicate);
			}
			else
			{
				return System.Web.HttpContext.Current.Cache.FirstOrDefault(predicate);
			}
		}

		public IEnumerable<T> GetListOf<T>(Func<T, bool> predicate)
		{
			if (System.Web.HttpContext.Current != null)
			{
				var list = m_CachedList.Where(i => i.Value.GetType() == typeof(T)).Select(i => i.Value).Cast<T>();
				return list.Where(predicate);
			}
			else
			{
				return System.Web.HttpContext.Current.Cache.GetListOf(predicate);
			}
		}

		public IQueryable<T> GetListOf<T>()
		{
			if (System.Web.HttpContext.Current != null)
			{
				var list = m_CachedList.Where(i => i.Value.GetType() == typeof(T)).Select(i => i.Value).Cast<T>();
				return list.AsQueryable();
			}
			else
			{
				return System.Web.HttpContext.Current.Cache.GetListOf<T>();
			}
		}

		public IQueryable<CacheEntry> GetAllItem()
		{
			return null;
		}

		#endregion
	}
}
