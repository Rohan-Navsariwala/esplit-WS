using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Biz.Services
{
	public class CacheService
	{
		private IMemoryCache _cache;
		public CacheService(IMemoryCache cache)
		{
			_cache = cache;
		}

		public object GetFromCache(string key)
		{
			return _cache.Get(key) ?? null;
		}

		public T InsertIntoCache<T>(string key, T value)
		{
			return _cache.Set<T>(key, value, TimeSpan.FromMinutes(15));
		}
	}
}