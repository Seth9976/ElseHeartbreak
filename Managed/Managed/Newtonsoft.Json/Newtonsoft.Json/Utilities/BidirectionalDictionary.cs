using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000A9 RID: 169
	internal class BidirectionalDictionary<TFirst, TSecond>
	{
		// Token: 0x060007B7 RID: 1975 RVA: 0x0001C010 File Offset: 0x0001A210
		public BidirectionalDictionary()
			: this(EqualityComparer<TFirst>.Default, EqualityComparer<TSecond>.Default)
		{
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001C022 File Offset: 0x0001A222
		public BidirectionalDictionary(IEqualityComparer<TFirst> firstEqualityComparer, IEqualityComparer<TSecond> secondEqualityComparer)
		{
			this._firstToSecond = new Dictionary<TFirst, TSecond>(firstEqualityComparer);
			this._secondToFirst = new Dictionary<TSecond, TFirst>(secondEqualityComparer);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001C044 File Offset: 0x0001A244
		public void Add(TFirst first, TSecond second)
		{
			if (this._firstToSecond.ContainsKey(first) || this._secondToFirst.ContainsKey(second))
			{
				throw new ArgumentException("Duplicate first or second");
			}
			this._firstToSecond.Add(first, second);
			this._secondToFirst.Add(second, first);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001C092 File Offset: 0x0001A292
		public bool TryGetByFirst(TFirst first, out TSecond second)
		{
			return this._firstToSecond.TryGetValue(first, out second);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001C0A1 File Offset: 0x0001A2A1
		public bool TryGetBySecond(TSecond second, out TFirst first)
		{
			return this._secondToFirst.TryGetValue(second, out first);
		}

		// Token: 0x0400026D RID: 621
		private readonly IDictionary<TFirst, TSecond> _firstToSecond;

		// Token: 0x0400026E RID: 622
		private readonly IDictionary<TSecond, TFirst> _secondToFirst;
	}
}
