using System;
using System.Collections;
using System.Collections.Generic;

namespace Boo.Lang.Environments
{
	// Token: 0x0200000F RID: 15
	public class DeferredEnvironment : IEnumerable, IEnvironment, IEnumerable<KeyValuePair<Type, ObjectFactory>>
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002CE8 File Offset: 0x00000EE8
		IEnumerator<KeyValuePair<Type, ObjectFactory>> IEnumerable<KeyValuePair<Type, ObjectFactory>>.GetEnumerator()
		{
			return this._bindings.GetEnumerator();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002CF8 File Offset: 0x00000EF8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._bindings.GetEnumerator();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D08 File Offset: 0x00000F08
		TNeed IEnvironment.Provide<TNeed>()
		{
			foreach (KeyValuePair<Type, ObjectFactory> keyValuePair in this._bindings)
			{
				if (typeof(TNeed).IsAssignableFrom(keyValuePair.Key))
				{
					return (TNeed)((object)keyValuePair.Value());
				}
			}
			return (TNeed)((object)null);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002DA0 File Offset: 0x00000FA0
		public void Add(Type need, ObjectFactory binder)
		{
			this._bindings.Add(new KeyValuePair<Type, ObjectFactory>(need, binder));
		}

		// Token: 0x0400000D RID: 13
		private readonly List<KeyValuePair<Type, ObjectFactory>> _bindings = new List<KeyValuePair<Type, ObjectFactory>>();
	}
}
