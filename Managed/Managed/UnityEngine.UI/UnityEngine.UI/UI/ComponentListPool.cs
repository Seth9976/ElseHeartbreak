using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x0200008C RID: 140
	internal static class ComponentListPool
	{
		// Token: 0x060004C1 RID: 1217 RVA: 0x00013C7C File Offset: 0x00011E7C
		public static List<Component> Get()
		{
			return ComponentListPool.s_ComponentListPool.Get();
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00013C88 File Offset: 0x00011E88
		public static void Release(List<Component> toRelease)
		{
			ComponentListPool.s_ComponentListPool.Release(toRelease);
		}

		// Token: 0x04000249 RID: 585
		private static readonly ObjectPool<List<Component>> s_ComponentListPool = new ObjectPool<List<Component>>(null, delegate(List<Component> l)
		{
			l.Clear();
		});
	}
}
