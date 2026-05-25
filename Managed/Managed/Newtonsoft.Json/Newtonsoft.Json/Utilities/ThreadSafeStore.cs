using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000A7 RID: 167
	internal class ThreadSafeStore<TKey, TValue>
	{
		// Token: 0x060007B4 RID: 1972 RVA: 0x0001BF16 File Offset: 0x0001A116
		public ThreadSafeStore(Func<TKey, TValue> creator)
		{
			if (creator == null)
			{
				throw new ArgumentNullException("creator");
			}
			this._creator = creator;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001BF40 File Offset: 0x0001A140
		public TValue Get(TKey key)
		{
			if (this._store == null)
			{
				return this.AddValue(key);
			}
			TValue tvalue;
			if (!this._store.TryGetValue(key, out tvalue))
			{
				return this.AddValue(key);
			}
			return tvalue;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001BF78 File Offset: 0x0001A178
		private TValue AddValue(TKey key)
		{
			TValue tvalue = this._creator(key);
			TValue tvalue3;
			lock (this._lock)
			{
				if (this._store == null)
				{
					this._store = new Dictionary<TKey, TValue>();
					this._store[key] = tvalue;
				}
				else
				{
					TValue tvalue2;
					if (this._store.TryGetValue(key, out tvalue2))
					{
						return tvalue2;
					}
					Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(this._store);
					dictionary[key] = tvalue;
					this._store = dictionary;
				}
				tvalue3 = tvalue;
			}
			return tvalue3;
		}

		// Token: 0x04000264 RID: 612
		private readonly object _lock = new object();

		// Token: 0x04000265 RID: 613
		private Dictionary<TKey, TValue> _store;

		// Token: 0x04000266 RID: 614
		private readonly Func<TKey, TValue> _creator;
	}
}
