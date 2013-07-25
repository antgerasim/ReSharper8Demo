using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using System.Timers;

using ERPStore;

namespace ERPStore.Services
{
	public class HttpCacheService : ERPStore.Services.ICacheService
	{

		public HttpCacheService()
		{
		}
		

		#region ICacheService Members

		public int ItemCount
		{
			get
			{
				return System.Web.HttpContext.Current.Cache.Count;
			}
		}

		public object this[string key]
		{
			get 
			{
				return System.Web.HttpContext.Current.Cache[key];
			}
		}

		public void Add(string key, object value, DateTime absoluteExpiration)
		{
			System.Web.HttpContext.Current.Cache.Insert(key, value, null, absoluteExpiration, TimeSpan.Zero);
		}

		public object Get(string key)
		{
			return System.Web.HttpContext.Current.Cache[key];
		}

		public void Remove(string key)
		{
			System.Web.HttpContext.Current.Cache.Remove(key);
		}

		public void ClearAll()
		{
			foreach (System.Collections.DictionaryEntry item in System.Web.HttpContext.Current.Cache)
			{
				System.Web.HttpContext.Current.Cache.Remove(item.Key.ToString());
			}
		}

		public T FirstOrDefault<T>(Func<T, bool> predicate)
		{
			return System.Web.HttpContext.Current.Cache.FirstOrDefault(predicate);
		}


		public IEnumerable<T> GetListOf<T>(Func<T, bool> predicate)
		{
			return System.Web.HttpContext.Current.Cache.GetListOf(predicate);
		}

		public IQueryable<T> GetListOf<T>()
		{
			return System.Web.HttpContext.Current.Cache.GetListOf<T>();
		}

		public IQueryable<CacheEntry> GetAllItem()
		{
			return null;
		}


		#endregion
	}
}
