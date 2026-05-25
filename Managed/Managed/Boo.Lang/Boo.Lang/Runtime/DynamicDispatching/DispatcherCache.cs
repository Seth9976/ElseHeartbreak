using System;
using System.Collections.Generic;

namespace Boo.Lang.Runtime.DynamicDispatching
{
	// Token: 0x0200002A RID: 42
	public class DispatcherCache
	{
		// Token: 0x06000109 RID: 265 RVA: 0x000045D8 File Offset: 0x000027D8
		public Dispatcher Get(DispatcherKey key, DispatcherCache.DispatcherFactory factory)
		{
			Dispatcher dispatcher;
			if (!DispatcherCache._cache.TryGetValue(key, ref dispatcher))
			{
				Dictionary<DispatcherKey, Dispatcher> cache = DispatcherCache._cache;
				lock (cache)
				{
					if (!DispatcherCache._cache.TryGetValue(key, ref dispatcher))
					{
						dispatcher = factory();
						DispatcherCache._cache.Add(key, dispatcher);
					}
				}
			}
			return dispatcher;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004654 File Offset: 0x00002854
		public void Clear()
		{
			Dictionary<DispatcherKey, Dispatcher> cache = DispatcherCache._cache;
			lock (cache)
			{
				DispatcherCache._cache.Clear();
			}
		}

		// Token: 0x04000133 RID: 307
		private static Dictionary<DispatcherKey, Dispatcher> _cache = new Dictionary<DispatcherKey, Dispatcher>(DispatcherKey.EqualityComparer);

		// Token: 0x02000044 RID: 68
		// (Invoke) Token: 0x06000272 RID: 626
		public delegate Dispatcher DispatcherFactory();
	}
}
