using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x0200008B RID: 139
	internal static class CanvasListPool
	{
		// Token: 0x060004BD RID: 1213 RVA: 0x00013C20 File Offset: 0x00011E20
		public static List<Canvas> Get()
		{
			return CanvasListPool.s_CanvasListPool.Get();
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00013C2C File Offset: 0x00011E2C
		public static void Release(List<Canvas> toRelease)
		{
			CanvasListPool.s_CanvasListPool.Release(toRelease);
		}

		// Token: 0x04000247 RID: 583
		private static readonly ObjectPool<List<Canvas>> s_CanvasListPool = new ObjectPool<List<Canvas>>(null, delegate(List<Canvas> l)
		{
			l.Clear();
		});
	}
}
