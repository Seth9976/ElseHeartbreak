using System;
using System.Collections.Generic;

namespace Boo.Lang.Environments
{
	// Token: 0x0200000D RID: 13
	public class CachingEnvironment : IEnvironment
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public CachingEnvironment(IEnvironment source)
		{
			this._source = source;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000041 RID: 65 RVA: 0x00002B10 File Offset: 0x00000D10
		// (remove) Token: 0x06000042 RID: 66 RVA: 0x00002B2C File Offset: 0x00000D2C
		public event Action<object> InstanceCached;

		// Token: 0x06000043 RID: 67 RVA: 0x00002B48 File Offset: 0x00000D48
		public TNeed Provide<TNeed>() where TNeed : class
		{
			object obj;
			if (this._cache.TryGetValue(typeof(TNeed), ref obj))
			{
				return (TNeed)((object)obj);
			}
			foreach (object obj2 in this._cache.Values)
			{
				if (obj2 is TNeed)
				{
					this._cache.Add(typeof(TNeed), obj2);
					return (TNeed)((object)obj2);
				}
			}
			TNeed tneed = this._source.Provide<TNeed>();
			if (tneed != null)
			{
				this.Add(typeof(TNeed), tneed);
			}
			return tneed;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002C30 File Offset: 0x00000E30
		public void Add(Type type, object instance)
		{
			if (!type.IsInstanceOfType(instance))
			{
				throw new ArgumentException(string.Format("{0} is not an instance of {1}", instance, type));
			}
			this._cache.Add(type, instance);
			if (this.InstanceCached != null)
			{
				this.InstanceCached.Invoke(instance);
			}
		}

		// Token: 0x04000009 RID: 9
		private readonly Dictionary<Type, object> _cache = new Dictionary<Type, object>();

		// Token: 0x0400000A RID: 10
		private readonly IEnvironment _source;
	}
}
